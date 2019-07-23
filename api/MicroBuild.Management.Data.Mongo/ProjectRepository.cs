using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MicroBuild.Management.Common.DTO;
using MicroBuild.Management.Data.API;

namespace MicroBuild.Management.Data.Mongo
{
    public class ProjectRepository : BaseRepository<Project>, IProjectRepository
    {
        public async Task<Project> AddAsync(Project project)
        {
            try
            {
                return await GenericRepository.Add(project);
            }
            catch (System.Exception ex)
            {
                return null;
            }
        }

        public async Task<Project> AddAsync(Project project, string userId)
        {
            if (!string.IsNullOrEmpty(project.MBEProjectId))
            {
                var existingProject = await GenericRepository.GetAll(x=>x.MBEProjectId.Equals(project.MBEProjectId));
                if (existingProject != null)
                {
                    return null;
                }
            }

            project.UsersInProject = new List<UserInProject> {
                new UserInProject {Id = userId, RoleId="1" }
            };

            project.CreatedDate = DateTime.UtcNow;
            return await GenericRepository.Add(project);
        }

        public async Task<long> DeleteProjectById(string projectId)
        {
            await GenericRepository.Remove(projectId);
            return 1;
        }

        public async Task UpdateProject(Project project)
        {
            await GenericRepository.UpdateById(project);
        }

        public async Task<Project> GetProject(string projectId)
        {
            return await GenericRepository.GetById(projectId);
        }

        public async Task<Project> GetProjectsByEngId(string engProjectId)
        {
            return await GenericRepository.GetByCriteria(x => engProjectId == x.MBEProjectId);
        }

        public async Task<List<Project>> GetProjectsByUserIdAsync(string userId)
        {
            List<Project> projects = await GenericRepository.GetAll(p => p.UsersInProject.Any(u => u.Id.Equals(userId)));
            return projects;
        }

        public async Task<List<Project>> GetAllProjects()
        {
            List<Project> projects = await GenericRepository.GetAll();
            return projects;
        }

		public async Task<IEnumerable<Project>> GetProjectsByIds(IEnumerable<string> ids)
		{
			return await GenericRepository.GetByIds(ids.ToList());
		}
	}
}
