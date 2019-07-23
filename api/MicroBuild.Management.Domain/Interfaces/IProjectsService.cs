using MicroBuild.Management.Common.DTO;
using MicroBuild.Management.Common.DTO.Models;
using MicroBuild.Management.Common.ViewObjects;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MicroBuild.Management.Domain.Interfaces
{
	public interface IProjectsService
	{
        Task<List<Project>> GetProjectsByUserIdAsync(string userId);

        Task<Project>GetProject(string projectId);

        Task<List<Project>> GetAllProjects();

        Task<List<UserInProject>> GetProjectAdmins(string projectId);

        Task<long> AddProjectAdmins(string projectId, List<string> userIds, string currentUserId);

        Task<bool> DeleteProjectAdmin(string projectId, string userId);

        Task<ProjectImportSuccessStatus> CreateProjectsAsync(string userId, Project project);

		Task DeleteProject(string projectId, string userId);

		Task<IEnumerable<ProjectsViewModelForMobile>> GetProjectsForMobile(string userId);

		Task<IEnumerable<ProjectNotifications>> GetProjectNotificationsMap(string userId, string[] projectIds);

		Task<UserRolesInUser> GetUserRoles(string projectId, string userId);
    }
}
