using MicroBuild.Management.Common.DTO;
using MicroBuild.Management.Common.DTO.Models;
using MicroBuild.Management.Common.MBEModels;
using MicroBuild.Management.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace MicroBuild.Management.Domain.Services
{
	public class MbeProjectService : BaseService, IMbeProjectService
	{
		public MbeProjectService(IObjectsLocator objectLocator) : base(objectLocator) { }


        public async Task<List<MbeProject>> GetMbeProjectsAsync(string userId, HttpRequestMessage request)
        {
            return await ObjectLocator.ProjectImportAdapter.GetUserProjectsAsync();
        }

        public async Task<List<FieldHeader>> GetMbeProjectFieldHeaders(string mbeProjectId)
        {
            try
            {
                var mbeProject = await ObjectLocator.ProjectImportAdapter.GetProjectByIdAsync(mbeProjectId);
				return mbeProject.ProjectDoorStructure.Select(
					fieldStructure =>
						new FieldHeader(fieldStructure.FieldName, fieldStructure.Header,fieldStructure.FieldType)
				).ToList();
			}
            catch (Exception ex)
            {
				var masterTemplate = await ObjectLocator.ProjectImportAdapter.GetMasterTemplate();
				var headersList = masterTemplate.DoorTemplateStructure.Select(
					t =>
						new FieldHeader(
							t.FieldName,
							// Only Norwegian is supported right now 
							t.LanguageHeaders.FirstOrDefault(h => h.Language == 1).Header,
                            t.FieldType
						)
				);
				return headersList.ToList();
			}
        }

		public async Task<ProjectImportSuccessStatus> ImportFromMbe(MbeProject engProject, string userId)
		{
			var _projectDomain = ObjectLocator.DomainObjectFactory.ProjectDomain;
			var mbeProjectClient = ObjectLocator.ProjectImportAdapter;
			//mbeProjectClient.
			//var _projectImportAdapter = this.ObjectLocator.ProjectImportAdapter;

			var createdProject = await _projectDomain.ImportAsync(engProject, userId);
			var projectMSResult = new ProjectImportSuccessStatus((createdProject != null) ? createdProject.MBEProjectId : engProject.Id);

			if (createdProject != null)
			{
				// Set Progress project id in Engineering project (only a single Progress project is allowed per Engineering project)
				var isUpdateSuccessfull = await mbeProjectClient.SetManagementProjectId(createdProject.MBEProjectId, createdProject.Id);

				if (isUpdateSuccessfull)
				{
					var doorMSResult = await ObjectLocator.MbeDoorService.ImportDoors(createdProject.MBEProjectId, createdProject.Id);

					if (doorMSResult.IsSuccess)
					{
						projectMSResult.Project = createdProject;
						projectMSResult.IsSuccess = true;

						Task addProjectDocuments = ObjectLocator.DomainObjectFactory.ProjectDocumentDomain.ImportProjectDocuments(createdProject.MBEProjectId, createdProject.Id, ObjectLocator.ProjectDocumentImportAdapter);
						Task addFieldHeaders = _projectDomain.ImportFieldHeaders(createdProject.MBEProjectId, createdProject.Id, mbeProjectClient, userId);

						await Task.WhenAll(addProjectDocuments, addFieldHeaders);
					}
					else
					{
						await _projectDomain.DeleteProject(createdProject.Id, userId);

						projectMSResult.IsSuccess = false;
						projectMSResult.Error = doorMSResult.Error;
					}
				}
				else
				{
					await _projectDomain.DeleteProject(createdProject.Id, userId);

					projectMSResult.IsSuccess = false;
					//projectMSResult.Error = "Project creation failed. Please try again or consult the developers.";
					projectMSResult.Error = "Project creation failed. Please try again or consult the developers.";
				}
			}
			else
			{
				projectMSResult.IsSuccess = false;
				//projectMSResult.Error = "Project creation failed. Please try again or consult the developers.";
				projectMSResult.Error = "Project creation failed. Please try again or consult the developers.";
			}

			return projectMSResult;
		}
	}
}
