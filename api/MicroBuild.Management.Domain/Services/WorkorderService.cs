using MicroBuild.Management.Common.DTO;
using MicroBuild.Management.Common.Models;
using MicroBuild.Management.Common.ViewObjects;
using MicroBuild.Management.Data.API;
using MicroBuild.Management.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroBuild.Management.Domain.Services
{
    public class WorkorderService : IWorkorderService
    {
        private IWorkorderRepository _workorderRepository;
        private IObjectsLocator _objectsLocator;

        public WorkorderService(IDataUnitOfWork dataUnitOfWork,ObjectsLocator ObjectsLocator)
        {
            _workorderRepository = dataUnitOfWork.WorkorderRepository;
            _objectsLocator = ObjectsLocator;
        }

        public async Task<bool> CreateWorkorders(IEnumerable<Workorder> workorders, string userId, string templateId, string projectId)
        {
            try
            {
                foreach (var wo in workorders)
                {
                    wo.IsPreviewOnly = true;
                    wo.ModifiedDate = DateTime.Now;
                    wo.LastModifiedUser = userId;
                    wo.TemplateId = templateId;
					wo.ProjectId = projectId;
                    wo.DueNotificationMailSent = false;
                    wo.ReminderNotificationMailSent = false;

                    await _workorderRepository.CreateWorkorder(wo);
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<BulkMailResponseViewModel> SendEmailNotificationForNotStartedWorkorders()
        {
            var res = new BulkMailResponseViewModel();
            try
            {
                var wos = await _workorderRepository.GetAllWordordersNotStarted();
                var users = await _objectsLocator.MbeUsersService.GetAllActiveUsersAnonymously();
                foreach (var wo in wos)
                {
                    var workorderTemplate = await _objectsLocator.WorkorderTemplateService.GetWorkorderTemplateByTemplateId(wo.TemplateId);
                    var projectAdmins = await _objectsLocator.ProjectService.GetProjectAdmins(wo.ProjectId);
                    if (projectAdmins != null)
                    {
                        var projectAdminUsers = users.FindAll(user =>
                        {
                            if (projectAdmins.Where(x => x.Id == user.Id).Count() > 0)
                                return true;
                            else
                                return false;
                        });

                        if (!wo.DueNotificationMailSent)
                        {
                            bool isMailSent = false;
                            foreach (var projectAdminUser in projectAdminUsers)
                            {
                                projectAdminUser.Email = "arunaxp@gmail.com";//remove later
                                res.total += 1;
                                var isSent = await _objectsLocator.EmailService.SendEmailNotificationForNotStartedWorkorders(projectAdminUser, workorderTemplate.Name, wo.StartDate);
                                if (isSent)
                                {
                                    isMailSent = true;
                                    res.sent += 1;
                                    var log = new EmailLog()
                                    {
                                        SentDate = DateTime.Now,
                                        TemplateId = workorderTemplate.Id,
                                        ProjectId = workorderTemplate.ProjectId,
                                        ToEmail = projectAdminUser.Email,
                                        ToUser = projectAdminUser.Id,
                                        Type = EMailTypeEnum.SERVICEORDER_NOT_STARTED
                                    };

                                    await _objectsLocator.EmailLogService.LogSentEmail(log);
                                }
                            }

                            if (isMailSent)
                            {
                                //update wo DueNotificationMailSent 
                                wo.DueNotificationMailSent = true;
                                await _workorderRepository.UpdateWorkorder(wo);
                            }
                        }
                    }
                }
                return res;
            }
            catch (Exception ex)
            {
                return res;
            }
        }

        public async Task<IEnumerable<Workorder>> GetAllWorkordersByProjectId(string projectId)
        {
            return await _workorderRepository.GetAllWordordersByProjectId(projectId);
        }

        public async Task<Workorder> GetWorkorderById(string workorderId)
        {
            return await _workorderRepository.GetWorkorderById(workorderId);
        }

        public async Task<IEnumerable<Workorder>> GetWorkordersByTemplateId(string templateId)
        {
            return await _workorderRepository.GetWorkordersByTemplateId(templateId);
        }

		public async Task<IEnumerable<Workorder>> GetWorkordersByTemplateIdForMobile(string templateId)
        {
            return await _workorderRepository.GetUnfinishedWorkordersByTemplateId(templateId);
        }

        public async Task<BulkMailResponseViewModel> SendEmailReminderNotifications()
        {
            var res = new BulkMailResponseViewModel();
            try
            {
                var users = await _objectsLocator.MbeUsersService.GetAllActiveUsersAnonymously();
                var wots = await _objectsLocator.WorkorderTemplateService.GetAllWordorderTemplates();
                foreach (var wot in wots)
                {
                    var wos = await _workorderRepository.GetWorkordersByTemplateId(wot.Id);
                    foreach (var wo in wos)
                    {
                        if ((DateTime.Now.Date - wo.StartDate.Date).Days == wot.RemindBeforeDays)
                        {
                            var projectAdmins = await _objectsLocator.ProjectService.GetProjectAdmins(wo.ProjectId);
                            if (projectAdmins != null)
                            {
                                var projectAdminUsers = users.FindAll(user =>
                                {
                                    if (projectAdmins.Where(x => x.Id == user.Id).Count() > 0)
                                        return true;
                                    else
                                        return false;
                                });


                                if (!wo.ReminderNotificationMailSent)
                                {
                                    bool isMailSent = false;
                                    foreach (var projectAdminUser in projectAdminUsers)
                                    {
                                        projectAdminUser.Email = "arunaxp@gmail.com";//remove later
                                        res.total += 1;
                                        var isSent = await _objectsLocator.EmailService.SendEmailReminderNotificationsForWorkorderTemplates(projectAdminUser, wot.Name, wo.StartDate);
                                        if (isSent)
                                        {
                                            isMailSent = true;
                                            res.sent += 1;

                                            var log = new EmailLog()
                                            {
                                                SentDate = DateTime.Now,
                                                TemplateId = wo.Id,
                                                ToEmail = projectAdminUser.Email,
                                                ToUser = projectAdminUser.Id,
                                                Type = EMailTypeEnum.SERVICEORDER_TEMPLATE_REMINDER_NOTIFICATION
                                            };

                                            await _objectsLocator.EmailLogService.LogSentEmail(log);
                                        }
                                    }

                                    //update wo NotificationMailSent 
                                    if (isMailSent)
                                    {
                                        wo.ReminderNotificationMailSent = true;
                                        await _workorderRepository.UpdateWorkorder(wo);
                                    }
                                }
                            }
                        }
                    }
                }
                return res;
            }
            catch (Exception)
            {
                return res;
            }
        }

		public async Task DeletePreviewWorkorders(string projectId, string templateId, string userId)
		{
			await _workorderRepository.RemovePreviewWorkorders(templateId);
		}

		public async Task<Dictionary<string, string>> CreateBulkAsync(string newProjectId, IEnumerable<Workorder> items)
		{
			foreach (var item in items)
			{
				item.ProjectId = newProjectId;
			}
			return await _workorderRepository.AddWithIdMapAsync(items);
		}

		public async Task DeleteBulk(string[] ids)
		{
			await _workorderRepository.DeleteBulk(ids);
		}

		public async Task<Workorder> StartWorkorder(string workorderId, string userId)
		{
			var workorder = await GetWorkorderById(workorderId);

			if (workorder.IsPreviewOnly)
			{
				workorder.IsPreviewOnly = false;
				workorder.LastModifiedUser = userId;
				workorder.ModifiedDate = DateTime.UtcNow;
				workorder.StartDate = DateTime.UtcNow;

				var templateDoors = await _objectsLocator.WorkorderTemplateDoorService.GetTemplateDoors(workorder.TemplateId);
				var instanceDoors = templateDoors.Select(td => new WorkorderDoor(workorder.Id, td));

				await _objectsLocator.WorkorderDoorService.CreateBulkAsync(workorder.ProjectId, instanceDoors);

				return await UpdateWorkorder(workorder);
			}
			else
			{
				return workorder;
			}
		}

		public async Task<Workorder> UpdateWorkorder(Workorder workorder)
		{
			return await _workorderRepository.UpdateWorkorder(workorder);
		}

		public async Task<Workorder> GetFirstWorkorderByTemplateId(string templateId)
		{
			return await _workorderRepository.GetFirstWorkorderByTemplateId(templateId);
		}

        public async Task<Workorder> FinalizeWorkorder(string workorderId, string userId)
        {
            var workorder = await GetWorkorderById(workorderId);
            if (!workorder.IsPreviewOnly)
            {
                workorder.LastModifiedUser = userId;
                workorder.ModifiedDate = DateTime.UtcNow;
                workorder.FinishedDate = DateTime.UtcNow;

                return await UpdateWorkorder(workorder);
            }
            else
            {
                return workorder;
            }

            
        }
    }
}