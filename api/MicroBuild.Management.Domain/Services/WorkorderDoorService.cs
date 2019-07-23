using MicroBuild.Management.Common.DTO;
using MicroBuild.Management.Common.ViewObjects;
using MicroBuild.Management.Data.API;
using MicroBuild.Management.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MicroBuild.Management.Domain.Services
{
    public class WorkorderDoorService : IWorkorderDoorService
    {
        private IWorkorderDoorRepository WorkorderDoorRepository;
        private IObjectsLocator ObjectsLocator;

        public WorkorderDoorService(IDataUnitOfWork dataUnitOfWork,IObjectsLocator objectLocator)
        {
            WorkorderDoorRepository = dataUnitOfWork.WorkorderDoorRepository;
            ObjectsLocator = objectLocator;
        }

        public async Task<Dictionary<string, string>> CreateBulkWithIdMapAsync(string newProjectId, IEnumerable<WorkorderDoor> items)
        {
            foreach (var item in items)
            {
                item.ProjectId = newProjectId;
            }
            return await WorkorderDoorRepository.AddWithIdMapAsync(items);
        }

        public async Task CreateBulkAsync(string newProjectId, IEnumerable<WorkorderDoor> items)
        {
            foreach (var item in items)
            {
                item.ProjectId = newProjectId;
            }
            await WorkorderDoorRepository.Add(items);
        }

        public async Task<WorkorderDoor> CreateWorkorderDoor(WorkorderDoor workorderDoor)
        {
            return await WorkorderDoorRepository.CreateWorkorderDoor(workorderDoor);
        }

        public async Task DeleteBulk(string[] ids)
        {
            await WorkorderDoorRepository.DeleteBulk(ids);
        }

        public async Task<List<WorkorderDoor>> GetAllWordorderDoorsByProjectId(string projectId)
        {
            return await WorkorderDoorRepository.GetAllWordorderDoorsByProjectId(projectId);
        }

        public IEnumerable<WorkorderDoor> SetHardwareOwnership(IEnumerable<WorkorderDoor> workorderDoors, IEnumerable<WorkorderTemplateHardwareCollection> hardwareCollections)
        {
            // Same logic as ..SetHardwareOwnership(..) in:
            // WorkorderTemplateDoorService.cs

            try
            {
                foreach (var hardwareMapping in hardwareCollections)
                {
                    foreach (var workorderDoor in workorderDoors)
                    {
                        var hdw = (Hardware)workorderDoor.Door[hardwareMapping.FieldName];

                        if (hdw.Content != null)
                        {
                            if (hdw.Content == hardwareMapping.Content)
                            {
                                hdw.IsMaintainable = true;
                                hdw.IsMaintained = false;

                                if (hardwareMapping.ChecklistId != null)
                                {
                                    hdw.ChecklistId = hardwareMapping.ChecklistId;
                                }
                            }
                        }
                    }
                }

                return workorderDoors;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<List<DoorNotificationsViewModel>> GetAllDoorsNotificationsInWorkorderInstance(string workorderId, string projectId)
        {
            var doors = await WorkorderDoorRepository.GetAllDoorsNotificationsInWorkorderInstance(workorderId);
            foreach (var door in doors)
            {
                var serviceMessagesCount = await ObjectsLocator.MessageService.GetServiceMessagesCount(workorderId, door.DoorId);
                //var issueMessagesCount = await ObjectsLocator.MessageService.GetIssueMessagesCount(door.DoorNo, projectId);
                var notesCount = await ObjectsLocator.DoorNotesService.GetNotesCountByDoorIdAsync(door.DoorId);
                //door.MessagesCount = serviceMessagesCount+ issueMessagesCount;
                door.MessagesCount = serviceMessagesCount;
                door.NotesCount = notesCount;
            }
            return doors;
        }

        public async Task<List<WorkorderDoorViewModel>> GetDoorViewsInWorkorder(string workorderId)
        {
            List<WorkorderDoorViewModel> workODoors = new List<WorkorderDoorViewModel>();

            var doors =  await WorkorderDoorRepository.GetAllWordorderDoorsByWorkorderId(workorderId);

            foreach (var door in doors)
            {
				var workODoor = new WorkorderDoorViewModel() {
                    Id = door.Id,
                    Door = door.Door,
                    FinishedDate = door.FinishedDate,
                    MessagesCount = 0,
					NotesCount = 0,
					ProjectId = door.ProjectId,
                    WorkorderId = door.WorkorderId
                };

                workODoors.Add(workODoor);
            }

            return workODoors;
        }

		public async Task<WorkorderDoor> GetFirstDoorInWorkorder(string workorderId)
		{
			return await WorkorderDoorRepository.GetFirstDoorInWorkorder(workorderId);
		}

		public async Task<WorkorderDoor> GetWorkOrderDoorByDoorNo(string workorderId, string doorNo)
        {
            try
            {
                var door = await WorkorderDoorRepository.GetWorkOrderDoorByDoorNo(workorderId, doorNo);
                if (door != null)
                {
                    return door.FirstOrDefault();
                }
                return null;
            }
            catch (Exception)
            {
                return null;
            }
        }

		public async Task<WorkorderDoor> GetWorkOrderDoorByDoorId(string workorderId, string doorId)
		{
			try
			{
				var door = await WorkorderDoorRepository.GetDoorByDoorIdAsList(workorderId, doorId);
				if (door != null)
				{
					return door.FirstOrDefault();
				}
				return null;
			}
			catch (Exception)
			{
				return null;
			}
		}

		public async Task<DoorDetail> SetWorkorderDoorHardwareMaintain(string workorderId, string doorId, DoorDetails selectedHardware,string userId)
        {
            var workorderDoors = await WorkorderDoorRepository.GetDoorByDoorIdAsList(workorderId, doorId);
            var wo = await this.ObjectsLocator.WorkorderService.GetWorkorderById(workorderId);
            if (workorderDoors != null)
            {
                var workorderDoor = workorderDoors.FirstOrDefault();
                var hardware = workorderDoor.Door[selectedHardware.FieldName] as Hardware;

                if (hardware.IsMaintainable == true)
                {
                    hardware.IsMaintained = selectedHardware.IsMaintained;
                    var updatedDoor = await WorkorderDoorRepository.UpdateWordorderDoor(workorderDoor);

                    //log
                    if (wo != null)
					{
                        await ObjectsLocator.LogService.AddMaintainedLogEntry(
                            wo.ProjectId,
                            wo.TemplateId,
                            wo.Id,
                            userId,
                            new HardwareInDoorRequestModel {
                                Content = hardware.Content,
                                IsMaintained = hardware.IsMaintained,
                                FieldName = selectedHardware.FieldName,
                                Header = selectedHardware.Header,
                            },
                            doorId, 
							workorderDoor.Door.DoorNo, 
							hardware.ChecklistId
						);
                    }
                }

                //check all maintainables are maintained
                string[] PROPS_TO_IGNORE = { "DoorNo", "Id", "ProjectId", "DoorQty", "AttachedProjectDocumentList", "Comment", "Building", "Revision", "MbeProductionId" };
                PropertyInfo[] DOOR_PROP_INFO = typeof(Door).GetProperties();
                bool everthingMaintained = true;
                foreach (PropertyInfo info in DOOR_PROP_INFO)
                {
                    if (!PROPS_TO_IGNORE.Contains(info.Name))
                    {
                        if (info.PropertyType.Equals(typeof(Hardware)))
                        {
                            var value = info.GetValue(workorderDoor.Door);
                            Hardware hw = (Hardware)value;
                            if(hw.IsMaintainable)
                            {
                                if (!hw.IsMaintained)
                                {
                                    everthingMaintained = false;
                                }
                            }
                        }
                    }
                }

                if (everthingMaintained){
                    var log = new HardwareMaintainedDoorLog()
                    {
                        DoorId = workorderDoor.Door.Id,
                        DoorNo = workorderDoor.Door.DoorNo,
                        IsMaintained = true,
                        ProjectId = wo.ProjectId,
                        WorkorderTemplateId = wo.TemplateId,
                        WorkorderId = wo.Id,
                        Timestamp = DateTime.UtcNow,
                        UserId = userId
                    };
                    await this.ObjectsLocator.LogService.AddMaintainedDoorLogEntry(log);
                    workorderDoor.FinishedDate = DateTime.UtcNow;
                    await this.WorkorderDoorRepository.UpdateWordorderDoor(workorderDoor);
                }
                else
                {
                    var logs = await this.ObjectsLocator.LogService.GetMaintainedDoorLogEntries(wo.ProjectId, wo.TemplateId, wo.Id);
                    var log = logs.OrderByDescending(m => m.Timestamp).FirstOrDefault();
                    if(log!=null && log.IsMaintained)
                    {
                        var logObj = new HardwareMaintainedDoorLog()
                        {
                            DoorId = workorderDoor.Door.Id,
                            DoorNo = workorderDoor.Door.DoorNo,
                            IsMaintained = false,
                            ProjectId = wo.ProjectId,
                            WorkorderTemplateId = wo.TemplateId,
                            WorkorderId = wo.Id,
                            Timestamp = DateTime.UtcNow,
                            UserId = userId
                        };
                        await this.ObjectsLocator.LogService.AddMaintainedDoorLogEntry(logObj);
                    }

                    if (workorderDoor.FinishedDate!=null)
                    {
                        workorderDoor.FinishedDate = null;
                        await this.WorkorderDoorRepository.UpdateWordorderDoor(workorderDoor);
                    }
                }
                //---

                return new DoorDetail
                {
                    FieldName = selectedHardware.FieldName,
                    Header = selectedHardware.Header,
                    Content = hardware.Content,
                    IsMaintainable = hardware.IsMaintainable,
                    IsMaintained = hardware.IsMaintained,
                    ChecklistId = hardware.ChecklistId

                };
            }
            return null;
        }

		public async Task<DoorDetailModel> GetDoorViewByDoorNo(string workorderId, string doorNo, string mbeProjectId, string userId)
		{
			var workOrderDoor = await GetWorkOrderDoorByDoorNo(workorderId, doorNo);

			if (workOrderDoor != null)
			{
				var fieldHeaders = await ObjectsLocator.MbeProjectService.GetMbeProjectFieldHeaders(mbeProjectId);
				var user = await ObjectsLocator.CompanyService.GetUserByUserId(workOrderDoor.ProjectId, userId);
				var doorDetailsObj = ObjectsLocator.DoorDetailModelFactory.GetDoorDetailsViewWithCompanyPermissions(workOrderDoor, fieldHeaders, user.Name);
                List<DoorDetail> sortedDoorDetailsList = new List<DoorDetail>();
                foreach (var field in fieldHeaders)
                {
                    if ((field.FieldName.IndexOf(".Qty")==-1)&& (field.FieldName.IndexOf(".Surf") == -1)&& (field.FieldName!= "AttachedProjectDocumentList"))
                    {
                        var header = field.FieldName.Split('.').ToList<string>();

                        var detailsList = doorDetailsObj.DoorDetails.Select(x => x).Where(x => x.FieldName == header[0]).OrderBy(x => x.Content);
                        sortedDoorDetailsList.AddRange(detailsList);
                    }
                       
                }

                sortedDoorDetailsList.AddRange(doorDetailsObj.DoorDetails.Select(x => x).Where(x => x.FieldName == "ChangedDate"));
                sortedDoorDetailsList.AddRange(doorDetailsObj.DoorDetails.Select(x => x).Where(x => x.FieldName == "ChangedBy"));

                doorDetailsObj.DoorDetails = sortedDoorDetailsList;
                return doorDetailsObj;
			}

			return null;
		}

		public async Task<DoorDetailModel> GetDoorViewByDoorId(string workorderId, string doorId, string mbeProjectId, string userId)
		{
			var workOrderDoor = await WorkorderDoorRepository.GetDoorByDoorId(workorderId, doorId);

			if (workOrderDoor != null)
			{
				var fieldHeaders = await ObjectsLocator.MbeProjectService.GetMbeProjectFieldHeaders(mbeProjectId);
				var user = await ObjectsLocator.CompanyService.GetUserByUserId(workOrderDoor.ProjectId, userId);

				var doorDetailsObj = ObjectsLocator.DoorDetailModelFactory.GetDoorDetailsViewWithCompanyPermissions(workOrderDoor, fieldHeaders, user.Name);

				return doorDetailsObj;
			}

			return null;
		}

        public async Task<OverViewReportViewModel> GetSimpleOverviewReport(string projectId, string templateId, string workorderId)
        {
            try
            {
                var wo = await ObjectsLocator.WorkorderService.GetWorkorderById(workorderId);
                if (!wo.IsPreviewOnly)//started
                {
                    var allDoor = await WorkorderDoorRepository.GetAllWordorderDoorsByWorkorderId(workorderId);
                    var CompletedDoor = allDoor.Where(x => x.FinishedDate != null);
                    var issueMessages = await ObjectsLocator.MessageService.GetAllIssueMessages(projectId);
                    double ratio = (double)CompletedDoor.Count() / allDoor.Count();
                    return new OverViewReportViewModel()
                    {
                        AllDoorCount = allDoor.Count(),
                        CompletedDoorCount = CompletedDoor.Count(),
                        LeftDoorCount = allDoor.Count() - CompletedDoor.Count(),
                        IssueMessagesCount = issueMessages.Count(),
                        CompletedPercentage = ratio.ToString("0.0%")
                    };
                }
                else
                    return null;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<List<OverViewReportViewModel>> GetSimpleOverviewReportForIntances(string projectId, string templateId)
        {
            try
            {
                var wos = await ObjectsLocator.WorkorderService.GetWorkordersByTemplateId(templateId);
                List<OverViewReportViewModel> reports = new List<OverViewReportViewModel>();
                foreach (var wo in wos)
                {
                    if (!wo.IsPreviewOnly)//started
                    {
                        var allDoor = await WorkorderDoorRepository.GetAllWordorderDoorsByWorkorderId(wo.Id);
                        var CompletedDoor = allDoor.Where(x => x.FinishedDate != null);
                        var issueMessages = await ObjectsLocator.MessageService.GetAllIssueMessages(projectId);
                        double ratio = (double)CompletedDoor.Count() / allDoor.Count();
                        reports.Add(new OverViewReportViewModel()
                        {
                            AllDoorCount = allDoor.Count(),
                            CompletedDoorCount = CompletedDoor.Count(),
                            LeftDoorCount = allDoor.Count() - CompletedDoor.Count(),
                            IssueMessagesCount = issueMessages.Count(),
                            CompletedPercentage = ratio.ToString("0.0%"),
							InstanceId = wo.Id,
						});
                    }
                }
                return reports;
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}