using System.Threading.Tasks;
using System.Collections.Generic;

using MicroBuild.Management.Common.DTO;
using MicroBuild.Management.Common.ViewObjects;

namespace MicroBuild.Management.Data.API
{
    public interface IWorkorderDoorRepository
    {
        Task<List<WorkorderDoor>> GetAllWordorderDoorsByProjectId(string projectId);
        
		Task<WorkorderDoor> CreateWorkorderDoor(WorkorderDoor workorderDoor);
		
		Task<Dictionary<string, string>> AddWithIdMapAsync(IEnumerable<WorkorderDoor> items);
		
		Task DeleteBulk(string[] ids);
		
		Task Add(IEnumerable<WorkorderDoor> items);
		
		Task<List<WorkorderDoor>> GetAllWordorderDoorsByWorkorderId(object workorderId);

		Task<WorkorderDoor> GetFirstDoorInWorkorder(string workorderId);

		Task<List<DoorNotificationsViewModel>> GetAllDoorsNotificationsInWorkorderInstance(object workorderId);
            
        Task<List<WorkorderDoor>> GetWorkOrderDoorByDoorNo(string workorderId, string doorNo);
        
		Task<List<WorkorderDoor>> GetDoorByDoorIdAsList(string workorderId, string doorId);

		Task<WorkorderDoor> GetDoorByDoorId(string workorderId, string doorId);

		Task<WorkorderDoor> GetDoorByDoorNo(string projectId, string doorNr);

		Task<WorkorderDoor> UpdateWordorderDoor(WorkorderDoor door);
    }
}