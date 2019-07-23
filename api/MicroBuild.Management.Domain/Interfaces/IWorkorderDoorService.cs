using System.Collections.Generic;
using System.Threading.Tasks;

using MicroBuild.Management.Common.DTO;
using MicroBuild.Management.Common.ViewObjects;

namespace MicroBuild.Management.Domain.Interfaces
{
    public interface IWorkorderDoorService
    {
        Task<List<WorkorderDoor>> GetAllWordorderDoorsByProjectId(string projectId);
        
		Task<WorkorderDoor> CreateWorkorderDoor(WorkorderDoor workorderDoor);
		
		Task<Dictionary<string, string>> CreateBulkWithIdMapAsync(string newProjectId, IEnumerable<WorkorderDoor> items);

		Task CreateBulkAsync(string newProjectId, IEnumerable<WorkorderDoor> items);

		IEnumerable<WorkorderDoor> SetHardwareOwnership(IEnumerable<WorkorderDoor> templateDoors, IEnumerable<WorkorderTemplateHardwareCollection> hardwareCollections);
		
		Task DeleteBulk(string[] ids);

		Task<WorkorderDoor> GetWorkOrderDoorByDoorNo(string workorderId, string doorNo);

		Task<WorkorderDoor> GetWorkOrderDoorByDoorId(string workorderId, string doorId);

		Task<DoorDetail> SetWorkorderDoorHardwareMaintain(string workorderId, string doorId, DoorDetails selectedHardware,string userId);

		Task<List<WorkorderDoorViewModel>> GetDoorViewsInWorkorder(string workorderId);

		Task<WorkorderDoor> GetFirstDoorInWorkorder(string workorderId);

		Task<List<DoorNotificationsViewModel>> GetAllDoorsNotificationsInWorkorderInstance(string workorderId,string projectId);
		
		Task<DoorDetailModel> GetDoorViewByDoorNo(string workorderId, string doorNo, string mbeProjectId, string userId);

		Task<DoorDetailModel> GetDoorViewByDoorId(string workorderId, string doorId, string mbeProjectId, string userId);

        Task<OverViewReportViewModel>  GetSimpleOverviewReport(string projectId, string templateId, string workorderId);

        Task<List<OverViewReportViewModel>> GetSimpleOverviewReportForIntances(string projectId, string templateId);
    }
}