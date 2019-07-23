using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MicroBuild.Management.Common.DTO;
using MicroBuild.Management.Common.DTO.Models;
using MicroBuild.Management.Common.MBEModels;
using MicroBuild.Management.Common.ViewObjects;
using MicroBuild.Management.Data.API;
using MicroBuild.Management.Domain.ImportAdapters;

namespace MicroBuild.Management.Domain.Services.Domain
{
    public class ProjectDomain
    {
        private IProjectRepository _projectRepository;

        public ProjectDomain(IProjectRepository projectRepository)
        {
            _projectRepository = projectRepository;
        }

        public async Task<List<Project>> GetProjectsByUserIdAsync(string userId)
        {
            var res = await _projectRepository.GetProjectsByUserIdAsync(userId);
            return res;
        }

        public async Task<ProjectImportSuccessStatus> AddNewProject(Project project, string userId)
        {
            var proj = await _projectRepository.AddAsync(project);
            var result = new ProjectImportSuccessStatus(proj.MBEProjectId);
            result.IsSuccess = true;
            result.Project = proj;

            return result;
        }

        public async Task<Project> ImportAsync(MbeProject engProject, string userId)
        {
            if (!string.IsNullOrEmpty(engProject.Id))
            {
                var existingProject = await _projectRepository.GetProjectsByEngId(engProject.Id);

                if (existingProject != null)
                {
                    return null;
                }
            }

            var project = new Project
            {
                MBEProjectId = engProject.Id,
                Name = engProject.Name,
                Description = engProject.Description,
                ProjectNo = engProject.ProjectNo,
                UsersInProject = new List<UserInProject>
                {
                    new UserInProject {Id = userId, RoleId="1" }
                },
                CreatedDate = DateTime.UtcNow
            };

            return await _projectRepository.AddAsync(project);
        }

		public async Task<IEnumerable<Project>> GetProjectsByIds(IEnumerable<string> ids)
		{
			return await _projectRepository.GetProjectsByIds(ids);
		}

		public async Task ImportFieldHeaders(string mbeProjectId, string projectId, IProjectImportAdapter mbeProjectClient, string userId)
        {
            MbeProject mbeProject = await mbeProjectClient.GetProjectByIdAsync(mbeProjectId);

            var fieldHeaders = mbeProject.ProjectDoorStructure.Select(
                fieldStructure =>
                    new FieldHeader(fieldStructure.FieldName, fieldStructure.Header,fieldStructure.FieldType)
            ).ToList();
        }

        public async Task<long> DeleteProject(string projectId, string userId)
        {
            var project = await _projectRepository.GetProject(projectId);

            if (project != null)
            {
                ValidateUserActionPermission(project, userId);

                long deletedCount = await _projectRepository.DeleteProjectById(projectId);

                return deletedCount;
            }

            return -1;
        }

        private void ValidateUserActionPermission(Project project, string userId)
        {
            if (!project.UsersInProject.Any(u => u.Id.Equals(userId)))
                throw new Exception("User is not permitted to perform this action.");
        }

        public async Task<Project> GetProject(string projectId)
        {
            return await _projectRepository.GetProject(projectId);
        }

        public async Task<long> AddProjectAdmins(string projectId, List<string> userIds, string currentUserId)
        {
            var project = await _projectRepository.GetProject(projectId);
            int usersAdded = 0;

            if (project.UsersInProject.FirstOrDefault(u => u.Id.Equals(currentUserId)) == null)
            {
                return -1;
            }

            foreach (var id in userIds)
            {
                if (project.UsersInProject.FirstOrDefault(u => u.Id.Equals(id)) == null)
                {
                    project.UsersInProject.Add(new UserInProject
                    {
                        Id = id,
                        RoleId = "1",
                        Active = false
                    });

                    usersAdded++;
                }
            }

            await _projectRepository.UpdateProject(project);

            return usersAdded;
        }

        public async Task<bool> DeleteProjectAdmin(string projectId, string userId)
        {
            var project = await _projectRepository.GetProject(projectId);
            bool userRemoved = false;
            var user = project.UsersInProject.FirstOrDefault(u => u.Id.Equals(userId));
            if (user == null)
            {
                return userRemoved;
            }
            else
            {
                project.UsersInProject.Remove(user);
                userRemoved = true;
            }

            await _projectRepository.UpdateProject(project);

            return userRemoved;
        }

        public async Task<List<Project>> GetAllProjects()
        {
            return await _projectRepository.GetAllProjects();
        }

        public async Task<List<UserInProject>> GetProjectAdmins(string projectId)
        {
            var project = await _projectRepository.GetProject(projectId);
            if (project != null)
                return project.UsersInProject.Where(x => x.RoleId == "1").ToList();
            else
                return null;
        }
    }
}
