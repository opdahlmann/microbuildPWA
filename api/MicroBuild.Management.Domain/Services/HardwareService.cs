using System;
using System.Threading.Tasks;
using MicroBuild.Management.Common.DTO;
using MicroBuild.Management.Domain.Interfaces;
namespace MicroBuild.Management.Domain.Services
{
    public partial class HardwareService: IHardwareService
    {
        private IObjectsLocator ObjectLocator;

        public HardwareService(IObjectsLocator objectLocator)
        {
            ObjectLocator = objectLocator;
        }

        public Task LogHardwareMounting(string projectId, string workorderTemplateId, string workOrderId, string userId, HardwareInDoorRequestModel hardwareList, string doorId, string DoorNo, string checklistId)
        {
            throw new NotImplementedException();
        }
    }
}
