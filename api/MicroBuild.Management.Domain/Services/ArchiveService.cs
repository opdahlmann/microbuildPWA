using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.IO.Compression;

using Ionic.Zip;
using Newtonsoft.Json;
using MicroBuild.Management.Domain.Interfaces;
using MicroBuild.Management.Data.API;
using MicroBuild.Management.Domain.Services.Domain;
using MicroBuild.Management.Common.DTO;
using MicroBuild.Management.Common.Models;


namespace MicroBuild.Management.Domain.Services
{
    public partial class ArchiveService :IArchiveService
    {
        private static string folderName = string.Empty;

        private DomainObjectFactory domainObjectFactory;
        private IObjectsLocator ObjectLocator;

        public ArchiveService(IDataUnitOfWork dataUnitOfWork, IObjectsLocator objectLocator)
        {
            this.domainObjectFactory = objectLocator.DomainObjectFactory;
            this.ObjectLocator = objectLocator;
            folderName = ConfigurationManager.AppSettings.Get("ArchiveFolder");
        }

        #region Back-up project data

        public async Task<MemoryStream> CreateProjectBackup(string projectId, string userId, HttpRequestMessage httpReq)
        {
            var project = (await ObjectLocator.ProjectService.GetProjectsByUserIdAsync(userId))
                    .FirstOrDefault(proj => proj.Id.Equals(projectId));

            if (project != null)
            {
                var doors = await ObjectLocator.DoorService.GetDoorsByProjectIdAsync(projectId);
                var companies = await ObjectLocator.CompanyService.GetCompaniesByProjectId(projectId);
				var checklists = await ObjectLocator.ChecklistService.GetAll(projectId);

				var syncLogs = await ObjectLocator.SyncLogService.GetSyncLogItems(projectId);
				var emailLogs = await ObjectLocator.EmailLogService.GetEmailLogsAsync(projectId);

				var templates = await ObjectLocator.WorkorderTemplateService.GetAllWordordersByProjectId(projectId);
				var hardwareCollections = await ObjectLocator.WorkorderTemplateHardwareCollectionService.GetHardwareCollectionsByProjectId(projectId);
				var templateDoors = await ObjectLocator.WorkorderTemplateDoorService.GetTemplateDoorsByProjectIdAsync(projectId);

				var workorders = await ObjectLocator.WorkorderService.GetAllWorkordersByProjectId(projectId);
				var workorderDoors = await ObjectLocator.WorkorderDoorService.GetAllWordorderDoorsByProjectId(projectId);
                var doorNotes = await ObjectLocator.DoorNotesService.GetAllNotesByProjectId(projectId);

				var archive = new ProjectArchive
                {
                    Metadata = new ArchiveMetadata
                    {
                        CreatedAt = DateTime.UtcNow,
                        CreatedByMBEUserId = userId,
                        ProjectName = project.Name,
                        ProjectId = project.Id
                    },
                    
					Project = project,
                    Doors = doors,
                    Companies = companies,
					Checklists = checklists,

					SyncLogs = syncLogs,
					EmailLogs = emailLogs,

					Templates = templates,
					HardwareCollections = hardwareCollections,
					TemplateDoors = templateDoors,

					Workorders = workorders,
					WorkorderDoors = workorderDoors,

                    DoorNotes = doorNotes
				};

                return await CreateZipFile(project, archive, httpReq);
            }
            else
            {
                throw new FileNotFoundException("Project id did not match any project administrated by the user.");
            }
        }

        private async Task<MemoryStream> CreateZipFile(Project project, ProjectArchive projectArchive, HttpRequestMessage httpReq)
        {
            string dataFileName = CreateDataFile(project, projectArchive);
            string metaDataFileName = await CreateMeteDataFile(project, httpReq);

            var zipFileName = folderName + "\\" + Guid.NewGuid().ToString() + ".zip";

            ZipFile zipFile = new ZipFile();
            var tempDataStream = new FileStream(dataFileName, FileMode.Open);
            var tempMetaDataStream = new FileStream(metaDataFileName, FileMode.Open);
            zipFile.AddEntry("data.json", tempDataStream);
            zipFile.AddEntry("metadata.json", tempMetaDataStream);
            zipFile.Save(zipFileName);
            tempDataStream.Dispose();
            tempMetaDataStream.Dispose();
            File.Delete(dataFileName);
            File.Delete(metaDataFileName);

            byte[] fileData = File.ReadAllBytes(zipFileName);
            var fileStream = new MemoryStream(fileData);
            File.Delete(zipFileName);
            // NOTE: ^^^ flip this to get the download file in the givent temp directory
            return fileStream;
        }

        private static string CreateDataFile(Project project, ProjectArchive projectArchive)
        {
            bool exists = Directory.Exists(folderName);
            if (!exists)
            {
                Directory.CreateDirectory(folderName);
            }
            var json = JsonConvert.SerializeObject(projectArchive);
            var dataFileName = folderName + "\\" + project.Name + " " + Guid.NewGuid().ToString() + ".json";
            File.WriteAllText(dataFileName, json);
            return dataFileName;
        }

        private async Task<string> CreateMeteDataFile(Project project, HttpRequestMessage httpReq)
        {
            var metaDataFileName = folderName + "\\" + project.Name + " " + Guid.NewGuid().ToString() + " meta-data.json";
            var user = await this.ObjectLocator.MbeUsersService.GetLoggedInUserAsync(httpReq);
            var dict = new Dictionary<string, string>();
            dict.Add("Version", GetAssemblyVersion());
            dict.Add("UserName", user.Name);
            dict.Add("AssemblyVersion", GetAssemblyVersion());
            dict.Add("Date", DateTime.Now.ToString());
            var jsonMeta = JsonConvert.SerializeObject(dict);
            File.WriteAllText(metaDataFileName, jsonMeta);
            return metaDataFileName;
        }

        private string GetAssemblyVersion()
        {
            System.Reflection.Assembly assembly = System.Reflection.Assembly.GetExecutingAssembly();
            FileVersionInfo fvi = FileVersionInfo.GetVersionInfo(assembly.Location);
            return fvi.FileVersion;
        }

        #endregion

        #region Restore from back-up

		public async Task<bool> RestoreArchive(string contentType, Stream fileContent, string userId)
        {
            var projectArchive = new ProjectArchive();

            MemoryStream memoryStream = new MemoryStream();

            byte[] data = new byte[fileContent.Length];
            int bytesRead;
            while ((bytesRead = fileContent.Read(data, 0, data.Length)) > 0)
            {
                memoryStream.Write(data, 0, bytesRead);
            }

            using (ZipArchive zip = new ZipArchive(memoryStream, ZipArchiveMode.Read, true))
            {
                var entries = zip.Entries;
                var dataFile = entries[0];

                using (var stream = dataFile.Open())
                {
                    var streamReader = new StreamReader(stream);

                    var json = streamReader.ReadToEnd();
                   
                        projectArchive = JsonConvert.DeserializeObject<ProjectArchive>(json);
                  
                    if (projectArchive != null)
                    {
                        string newProjectId = await RestoreProject(projectArchive, userId);

                        if (!string.IsNullOrEmpty(newProjectId))
                        {
							var idMaps = new RestoreIdMaps();

							try
							{
								idMaps.Companies = await RestoreCompaniesAndGetMap(projectArchive, newProjectId, userId);
								idMaps.Doors = await RestoreDoorsAndGetMap(projectArchive, newProjectId, idMaps, userId);
								idMaps.Checklists = await RestoreChecklistsAndGetMap(projectArchive, newProjectId, idMaps, userId);

								await RestoreSyncLog(projectArchive, newProjectId, idMaps, userId);
								await RestoreEmailLog(projectArchive, newProjectId, idMaps, userId);

								idMaps.Templates = await RestoreTemplatesAndGetMapAsync(projectArchive, newProjectId, idMaps, userId);
								idMaps.HardwareCollections = await RestoreHardwareCollectionsAndGetMapAsync(projectArchive, newProjectId, idMaps, userId);
								idMaps.TemplateDoors = await RestoreTemplateDoorsAndGetMapAsync(projectArchive, newProjectId, idMaps, userId);

								idMaps.Workorders = await RestoreWorkordersAndGetMapAsync(projectArchive, newProjectId, idMaps, userId);
								idMaps.WorkorderDoors = await RestoreWorkorderDoorsAndGetMapAsync(projectArchive, newProjectId, idMaps, userId);
                                idMaps.DoorNotes = await RestoreDoorNotesAndGetMapAsync(projectArchive, newProjectId, idMaps, userId);

								streamReader.Dispose();
								return true;
							}
							catch (Exception ex)
							{
								if (idMaps.Companies != null)
								{
									await ObjectLocator.CompanyService.DeleteBulk(idMaps.Companies.Values.ToArray(), newProjectId);
								}
								if (idMaps.Doors != null)
								{
									await ObjectLocator.DoorService.DeleteBulk(newProjectId);
								}
								if (idMaps.Checklists != null)
								{
									await ObjectLocator.ChecklistService.DeleteBulk(idMaps.Checklists.Values.ToArray());
								}

								if (idMaps.Templates != null)
								{
									await ObjectLocator.WorkorderTemplateService.DeleteBulk(idMaps.Templates.Values.ToArray());
								}
								if (idMaps.HardwareCollections != null)
								{
									await ObjectLocator.WorkorderTemplateHardwareCollectionService.DeleteBulk(idMaps.HardwareCollections.Values.ToArray());
								}
								if (idMaps.TemplateDoors != null)
								{
									await ObjectLocator.WorkorderTemplateDoorService.DeleteBulk(idMaps.TemplateDoors.Values.ToArray());
								}

								if (idMaps.Workorders != null)
								{
									await ObjectLocator.WorkorderService.DeleteBulk(idMaps.Workorders.Values.ToArray());
								}
								if (idMaps.DoorNotes != null)
								{
									await ObjectLocator.DoorNotesService.DeleteBulk(idMaps.WorkorderDoors.Values.ToArray());
								}

								streamReader.Dispose();
								return false;
							}
                        }
                    }

                    return false;
                }
            }
        }

		private async Task<string> RestoreProject(ProjectArchive archive, string userId)
		{
			var bup = archive.Project;

			var project = bup;

			project.Id = null;
			project.MBEProjectId = null;
			project.Name = $"{bup.Name} (backup)";
           if(project.UsersInProject?.Find(x => x.Id == userId) == null)
            {
                project.UsersInProject.Add(new UserInProject()
                {
                    Id =userId,
                    RoleId="1",
                });
            }
			var result = await this.ObjectLocator.ProjectService.CreateProjectsAsync(userId, project);
			project = result.Project;
			return project?.Id;
		}

		private async Task<Dictionary<string, string>> RestoreCompaniesAndGetMap(ProjectArchive projectArchive, string newProjectId, string userId)
		{
			var companies = projectArchive.Companies;
			if (companies != null)
			{
				var companyIdMap = new Dictionary<string, string>();

				foreach (var company in companies)
				{
					var oldCompanyId = company.Id;
					company.Id = null;
					company.ProjectId = newProjectId;
					var newCompany = await this.ObjectLocator.CompanyService.AddCompany(company, newProjectId);

					companyIdMap.Add(oldCompanyId, newCompany.Id);
				}

				return companyIdMap;
			}

			return null;
		}

		private async Task<Dictionary<string, string>> RestoreDoorsAndGetMap(ProjectArchive projectArchive, string newProjectId, RestoreIdMaps idMaps, string userId)
		{
			var doors = projectArchive.Doors;
			if (doors != null)
			{
				foreach (var door in doors)
				{
					door.ProjectId = newProjectId;
				}
				return await ObjectLocator.DoorService.CreateDoorsBulk(newProjectId, doors);
			}

			return null;
		}

		private async Task<Dictionary<string, string>> RestoreChecklistsAndGetMap(ProjectArchive projectArchive, string newProjectId, RestoreIdMaps idMaps, string userId)
		{
			var items = projectArchive.Checklists;
			if (items != null)
			{
				foreach (var item in items)
				{
					item.ProjectId = newProjectId;
				}
				return await ObjectLocator.ChecklistService.CreateBulk(newProjectId, items);
			}

			return null;
		}

		private async Task RestoreSyncLog(ProjectArchive projectArchive, string newProjectId, RestoreIdMaps idMaps, string userId)
		{
			var items = projectArchive.SyncLogs;
			if (items != null)
			{
				await ObjectLocator.SyncLogService.CreateBulkAsync(newProjectId, items);
			}
		}

		private async Task RestoreEmailLog(ProjectArchive projectArchive, string newProjectId, RestoreIdMaps idMaps, string userId)
		{
			var items = projectArchive.EmailLogs;
			if (items != null)
			{
				await ObjectLocator.EmailLogService.CreateBulkAsync(newProjectId, items);
			}
		}

		private async Task<Dictionary<string, string>> RestoreWorkorderDoorsAndGetMapAsync(ProjectArchive projectArchive, string newProjectId, RestoreIdMaps idMaps, string userId)
		{
			var items = projectArchive.WorkorderDoors;
			if (items != null)
			{
				var _service = ObjectLocator.WorkorderDoorService;

				_service.SetHardwareOwnership(items, projectArchive.HardwareCollections);

				foreach (var item in items)
				{
                    if (idMaps.Workorders.ContainsKey(item.WorkorderId))
                    {
                        item.WorkorderId = idMaps.Workorders[item.WorkorderId];
                    }
                    if (idMaps.Doors.ContainsKey(item.Door.Id))
                    {
                        item.Door.Id = idMaps.Doors[item.Door.Id];
                    }
                       
				}

				return await _service.CreateBulkWithIdMapAsync(newProjectId, items);
			}

			return null;
		}

		private async Task<Dictionary<string, string>> RestoreWorkordersAndGetMapAsync(ProjectArchive projectArchive, string newProjectId, RestoreIdMaps idMaps, string userId)
		{
			var items = projectArchive.Workorders;
			if (items != null)
			{
				foreach (var item in items)
				{
                    if (idMaps.Templates.ContainsKey(item.TemplateId))
                    {
                        item.TemplateId = idMaps.Templates[item.TemplateId];
                    }
				}

				return await ObjectLocator.WorkorderService.CreateBulkAsync(newProjectId, items);
			}

			return null;
		}

		private async Task<Dictionary<string, string>> RestoreTemplateDoorsAndGetMapAsync(ProjectArchive projectArchive, string newProjectId, RestoreIdMaps idMaps, string userId)
		{
			var items = projectArchive.TemplateDoors;
			if (items != null)
			{
				var _service = ObjectLocator.WorkorderTemplateDoorService;

				_service.SetHardwareOwnership(items, projectArchive.HardwareCollections);

				foreach (var item in items)
				{
                    if (idMaps.Templates.ContainsKey(item.TemplateId))
                    {
                        item.TemplateId = idMaps.Templates[item.TemplateId];
                    }
                    if (idMaps.Doors.ContainsKey(item.Door.Id))
                    {
                        item.Door.Id = idMaps.Doors[item.Door.Id];
                    }
				}

				return await _service.CreateBulkAsync(newProjectId, items);
			}

			return null;
		}

		private async Task<Dictionary<string, string>> RestoreHardwareCollectionsAndGetMapAsync(ProjectArchive projectArchive, string newProjectId, RestoreIdMaps idMaps, string userId)
		{
			var items = projectArchive.HardwareCollections;
			if (items != null)
			{
				foreach (var item in items)
				{
                    if (idMaps.Templates.ContainsKey(item.TemplateId))
                    {
                        item.TemplateId = idMaps.Templates[item.TemplateId];
                    }
                    if (item.ChecklistId != null && idMaps.Checklists.ContainsKey(item.ChecklistId))
                    {
                       
                            item.ChecklistId = idMaps.Checklists[item.ChecklistId];
                    }
				}
				return await ObjectLocator.WorkorderTemplateHardwareCollectionService.CreateBulkAsync(newProjectId, items);
			}

			return null;
		}

		private async Task<Dictionary<string, string>> RestoreTemplatesAndGetMapAsync(ProjectArchive projectArchive, string newProjectId, RestoreIdMaps idMaps, string userId)
		{
			var items = projectArchive.Templates;
			if (items != null)
			{
                try
                {
                    foreach (var item in items)
                    {
                       if( idMaps.Companies.ContainsKey(item.CompanyId))
                        {
                            item.CompanyId = idMaps.Companies[item.CompanyId];
                        }
                    }

                    return await ObjectLocator.WorkorderTemplateService.CreateBulkAsync(newProjectId, items);
                }
                catch(Exception e)
                {
                    return null;
                }
				
			}

			return null;
		}
        private async Task<Dictionary<string, string>> RestoreDoorNotesAndGetMapAsync(ProjectArchive projectArchive, string newProjectId, RestoreIdMaps idMaps, string userId)
        {
            var items = projectArchive.DoorNotes;
            if (items != null)
            {
                foreach (var item in items)
                {
                    if (idMaps.Templates.ContainsKey(item.TemplateId))
                    {
                        item.TemplateId = idMaps.Templates[item.TemplateId];
                    }
                    if (idMaps.Workorders.ContainsKey(item.WorkorderId))
                    {
                        item.WorkorderId = idMaps.Workorders[item.WorkorderId];
                    }
                    if (idMaps.WorkorderDoors.ContainsKey(item.DoorId))
                    {
                        item.DoorId = idMaps.WorkorderDoors[item.DoorId];
                    }
                    item.ProjectId = newProjectId;
                }
                return await ObjectLocator.DoorNotesService.CreateBulkAsync(items);
            }

            return null;
        }

        #endregion
    }

	public class RestoreIdMaps
	{
		public Dictionary<string, string> Companies { get; set; }
		public Dictionary<string, string> Doors { get; set; }
		public Dictionary<string, string> Checklists { get; set; }

		public Dictionary<string, string> Templates { get; set; }
		public Dictionary<string, string> HardwareCollections { get; set; }
		public Dictionary<string, string> TemplateDoors { get; set; }

		public Dictionary<string, string> Workorders { get; set; }
		public Dictionary<string, string> WorkorderDoors { get; set; }
        public Dictionary<string, string> DoorNotes { get; set; }
    }
}
