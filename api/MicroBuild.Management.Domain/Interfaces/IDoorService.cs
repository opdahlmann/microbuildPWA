using MicroBuild.Management.Common.DTO;
using MicroBuild.Management.Common.ViewObjects;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MicroBuild.Management.Domain.Interfaces
{
    public interface IDoorService
    {
        Task<List<Door>> GetDoorsByProjectIdAsync(string projectId);
        Task<Door> GetDoorById(string doorId);
        Task<List<DoorDetailModel>> GetDoorsByProjectIdAsync(string projectId, string userId);
		Task<Door> GetDoorByDoorNo(string projectId, string doorNr);
        Task<long> BulkUpdateAsync<T>(string projectId, string changingProperty, T valueForProperty, List<string> changingDoorIds);
        Task<Dictionary<string, string>> CreateDoorsBulk(string projectId, IEnumerable<Door> doors);
        Task DeleteDoorsByProject(string projectId, string userId);
		Task DeleteBulk(string newProjectId);
	}
}
