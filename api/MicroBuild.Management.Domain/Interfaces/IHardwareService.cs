using MicroBuild.Management.Common.DTO;
using MicroBuild.Management.Common.ViewObjects;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MicroBuild.Management.Domain.Interfaces
{
    public interface IHardwareService
    {
        Task LogHardwareMounting(string projectId, string workorderTemplateId, string workOrderId, string userId,HardwareInDoorRequestModel hardwareList, string doorId, string DoorNo,string checklistId);
    }
}
