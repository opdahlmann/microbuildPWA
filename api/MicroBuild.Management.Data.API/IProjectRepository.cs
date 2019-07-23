using System.Collections.Generic;
using System.Threading.Tasks;
using MicroBuild.Management.Common.DTO;

namespace MicroBuild.Management.Data.API
{
	public interface IProjectRepository
	{
        Task<List<Project>> GetProjectsByUserIdAsync(string userId);

        Task<Project> AddAsync(Project project);

        Task<Project> GetProject(string projectId);

        Task<List<Project>> GetAllProjects();

        Task<Project> GetProjectsByEngId(string engineeringProjectId);
       
		Task UpdateProject(Project project);

        Task<long> DeleteProjectById(string projectId);

		Task<IEnumerable<Project>> GetProjectsByIds(IEnumerable<string> ids);
	}
}
