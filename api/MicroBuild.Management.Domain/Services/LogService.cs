using MicroBuild.Management.Common.DTO;
using MicroBuild.Management.Data.API;
using MicroBuild.Management.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroBuild.Management.Domain.Services
{
    public class LogService : ILogService
    {
        private IDataUnitOfWork DataUnitOfWork;
        private IObjectsLocator ObjectLocator;
        public LogService(IDataUnitOfWork dataUnitOfWork,IObjectsLocator objectLocator)
        {
            this.DataUnitOfWork = dataUnitOfWork;
            this.ObjectLocator = objectLocator;
        }

        public Task AddMaintainedLogEntriesInBulk(List<HardwareMaintainedLog> logs)
        {
            throw new NotImplementedException();
        }

        public async Task AddMaintainedLogEntry(string projectId, string workorderTemplateId, string workOrderId, string userId, HardwareInDoorRequestModel hardwareItem, string doorId, string DoorNo, string checklistId)
        {
            var log = new HardwareMaintainedLog()
            {
                DoorNo = DoorNo,
                Header = hardwareItem.Header,
                FieldName = hardwareItem.FieldName,
                Content = hardwareItem.Content,
                ChecklistId = checklistId,
                IsMaintained = hardwareItem.IsMaintained,

                Metadata = new LogMetadata()
                {
                    ProjectId = projectId,
                    WorkorderTemplateId = workorderTemplateId,
                    WorkorderId = workOrderId,
                    DoorId = doorId,
                    UserId = userId,
                    Timestamp = DateTime.UtcNow,
                }
            };

            await this.DataUnitOfWork.HardwareMaintainedLogRepository.AddLogEntry(log);
        }

        public async Task AddMaintainedDoorLogEntry(HardwareMaintainedDoorLog log)
        {
            await this.DataUnitOfWork.HardwareMaintainedDoorLogRepository.AddLogEntry(log);
        }

        public async Task<List<HardwareMaintainedDoorLogViewModel>> GetMaintainedDoorLogEntries(string projectId, string workordertemplateId, string workorderId)
        {
            var users = await this.ObjectLocator.MbeUsersService.GetAllUsers();
            var entries = await this.DataUnitOfWork.HardwareMaintainedDoorLogRepository.GetLogEntries(projectId, workordertemplateId, workorderId);
            var wotemplate = await this.ObjectLocator.WorkorderTemplateService.GetWorkorderTemplateByTemplateId(workordertemplateId);
            return entries.Select(x => new HardwareMaintainedDoorLogViewModel()
            {
                Id = x.Id,
                DoorId = x.DoorId,
                DoorNo = x.DoorNo,
                ProjectId = x.ProjectId,
                Timestamp = x.Timestamp,
                UserId = x.UserId,
                IsMaintained = x.IsMaintained,

                UserName = users.Where(y => y.Id.Equals(x.UserId)).FirstOrDefault().Name,
                WorkorderId = x.WorkorderId,
                WorkorderTemplateId = x.WorkorderTemplateId,
                WorkorderTemplateName = wotemplate.Name,
            }).ToList();
        }

        public async Task<List<HardwareMaintainedLogViewModel>> GetMaintainedLogEntries(string projectId, string workordertemplateId, string workorderId)
        {
           var users = await this.ObjectLocator.MbeUsersService.GetAllUsers();
           var entries =  await this.DataUnitOfWork.HardwareMaintainedLogRepository.GetLogEntries(projectId, workordertemplateId, workorderId);
           return entries.Select(x => new HardwareMaintainedLogViewModel()
           {
               Id = x.Id,
               ChecklistId = x.ChecklistId,
               Content = x.Content,
               DoorNo = x.DoorNo,
               FieldName = x.FieldName,
               Header = x.Header,
               IsMaintained = x.IsMaintained,

               DoorId = x.Metadata.DoorId,
               ProjectId = x.Metadata.ProjectId,
               Timestamp = x.Metadata.Timestamp,
               UserId = x.Metadata.UserId,
               UserName = users.Where(y => y.Id.Equals(x.Metadata.UserId)).FirstOrDefault().Name,
               WorkorderId = x.Metadata.WorkorderId,
               WorkorderTemplateId = x.Metadata.WorkorderTemplateId,

           }).ToList();
        }
    }
}
