using MicroBuild.Management.Domain.Interfaces;
using MicroBuild.Management.Data.API;
using System.Threading.Tasks;
using System.Collections.Generic;
using MicroBuild.Management.Common.DTO;
using MicroBuild.Management.Domain.Services.Domain;
using MicroBuild.Management.Common.DTO.Models;
using MicroBuild.Management.Common.MBEModels;
using System.Net.Http;
using System;
using MicroBuild.Management.Common.ViewObjects;
using System.Linq;

namespace MicroBuild.Management.Domain.Services
{
    public class ProjectsService : IProjectsService /* BaseService */
    {
        private DomainObjectFactory domainObjectFactory;

		private IDataUnitOfWork DataUnitOfWork;
		private IObjectsLocator ObjectLocator;

        public ProjectsService(IDataUnitOfWork dataUnitOfWork, IObjectsLocator objectLocator)
        {
            domainObjectFactory = objectLocator.DomainObjectFactory;

			DataUnitOfWork = dataUnitOfWork;
            ObjectLocator = objectLocator;
        }

        public async Task<List<Project>> GetProjectsByUserIdAsync(string userId)
        {
            return await domainObjectFactory.ProjectDomain.GetProjectsByUserIdAsync(userId);
        }

		public async Task<IEnumerable<ProjectsViewModelForMobile>> GetProjectsForMobile(string userId)
		{
			var companies = await ObjectLocator.CompanyService.GetCompaniesByUserId(userId);

			var projects = await domainObjectFactory.ProjectDomain.GetProjectsByIds(companies.Select(c => c.ProjectId).Distinct());

			return projects.Select(p => new ProjectsViewModelForMobile(p));
		}

		public async Task<Project> GetProject(string projectId)
        {
            return await domainObjectFactory.ProjectDomain.GetProject(projectId);
        }

        public async Task<long> AddProjectAdmins(string projectId, List<string> userIds, string currentUserId)
        {
            return await domainObjectFactory.ProjectDomain.AddProjectAdmins(projectId, userIds, currentUserId);
        }

        public async Task<bool> DeleteProjectAdmin(string projectId, string userId)
        {
            return await domainObjectFactory.ProjectDomain.DeleteProjectAdmin(projectId, userId);
        }

        public async Task<List<UserInProject>> GetProjectAdmins(string projectId)
        {
            return await domainObjectFactory.ProjectDomain.GetProjectAdmins(projectId);
        }

        public async Task<ProjectImportSuccessStatus> CreateProjectsAsync(string userId, Project project)
        {
            ProjectImportSuccessStatus result = await domainObjectFactory.ProjectDomain.AddNewProject(project, userId);
            try
            {
                if (result.IsSuccess)
                {
                    bool isActivationDeactivation = false;
                    //to do uncomments
                    //EmailService _emailService = new EmailService();
                    //_emailService.SendProjectNotificationMail(userId, project, isActivationDeactivation);
                }
                return result;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public async Task<ProjectImportSuccessStatus> Import(MbeProject engProject, string userId)
        {
            var _projectDomain = ObjectLocator.DomainObjectFactory.ProjectDomain;
            var mbeProjectClient = ObjectLocator.ProjectImportAdapter;
            //mbeProjectClient.
            //var _projectImportAdapter = this.ObjectLocator.ProjectImportAdapter;

            var createdProject = await _projectDomain.ImportAsync(engProject, userId);
            var projectMSResult = new ProjectImportSuccessStatus(createdProject.MBEProjectId);

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

                        Task addProjectDocuments = ObjectLocator.DomainObjectFactory.ProjectDocumentDomain.ImportProjectDocuments(createdProject.MBEProjectId, createdProject.Id, this.ObjectLocator.ProjectDocumentImportAdapter);
                        Task addFieldHeaders = _projectDomain.ImportFieldHeaders(createdProject.MBEProjectId, createdProject.Id, mbeProjectClient, userId);

                        await Task.WhenAll(addProjectDocuments, addFieldHeaders);
                    }
                    else
                    {
                        await _projectDomain.DeleteProject(engProject.Id, userId);

                        projectMSResult.IsSuccess = false;
                        projectMSResult.Error = doorMSResult.Error;
                    }
                }
                else
                {
                    await _projectDomain.DeleteProject(engProject.Id, userId);

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

        public async Task<List<Project>> GetAllProjects()
        {
            try
            {
                var result = await domainObjectFactory.ProjectDomain.GetAllProjects();
                return result;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public async Task DeleteProject(string projectId, string userId)
        {
            await domainObjectFactory.ProjectDomain.DeleteProject(projectId, userId);
            await ObjectLocator.DoorNotesService.DeleteNoteByProjectId(projectId);
        }

        public async Task<UserRolesInUser> GetUserRoles(string projectId, string userId)
        {
            var companies = await ObjectLocator.CompanyService.GetCompaniesByProjectId(projectId);

            var company = companies.FirstOrDefault(x => x.Users.Any(u => u.MBEUserId.Equals(userId)));
            var user = company.Users.FirstOrDefault(u => u.MBEUserId.Equals(userId));
            return new UserRolesInUser()
            {
                IsAdmin = user.IsAdmin,
                IsCustomer = user.IsCustomer,
                IsProjectLeader = user.IsProjectLeader
            };
        }

		public async Task<IEnumerable<ProjectNotifications>> GetProjectNotificationsMap(string userId, string[] projectIds)
		{
			var __issueMessageRepo = DataUnitOfWork.IssueMessageRepository;

			var issueMessageCounts = new List<long> { };

			foreach (var projectId in projectIds)
			{
				issueMessageCounts.Add(await __issueMessageRepo.GetCountByProject(projectId));
			}

			return projectIds.Select((projectId, id) => new ProjectNotifications
			{
				ProjectId = projectId,
				IssueMessagesCount = issueMessageCounts[id],
			});
		}
	}
}
