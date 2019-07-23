using MicroBuild.Management.Common.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroBuild.Management.Domain.Interfaces
{
    public interface ILogService
    {
        Task AddMaintainedLogEntry(string projectId, string workorderTemplateId, string workOrderId, string userId, HardwareInDoorRequestModel hardwareItem, string doorId, string DoorNo, string checklistId);
        Task AddMaintainedLogEntriesInBulk(List<HardwareMaintainedLog> logs);
        Task<List<HardwareMaintainedLogViewModel>> GetMaintainedLogEntries(string projectId, string workordertemplateId, string workorderId);

        Task AddMaintainedDoorLogEntry(HardwareMaintainedDoorLog log);
        Task<List<HardwareMaintainedDoorLogViewModel>> GetMaintainedDoorLogEntries(string projectId, string workordertemplateId, string workorderId);
    }
}
