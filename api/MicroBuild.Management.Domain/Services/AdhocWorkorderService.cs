using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using MicroBuild.Management.Common.DTO;
using MicroBuild.Management.Data.API;
using MicroBuild.Management.Domain.Interfaces;

namespace MicroBuild.Management.Domain.Services
{
	public class AdhocWorkorderService : IAdhocWorkorderService
	{
		private readonly IDataUnitOfWork DataUnitOfWork;
		private readonly IObjectsLocator ObjectsLocator;

		public AdhocWorkorderService(IDataUnitOfWork dataUnitOfWork, ObjectsLocator objectsLocator)
		{
			DataUnitOfWork = dataUnitOfWork;
			ObjectsLocator = objectsLocator;
		}

		public async Task<Workorder> CreateWordorderForDoor(string projectId, string companyId, string doorNr, string userId)
		{
			var projectDoor = await ObjectsLocator.DoorService.GetDoorByDoorNo(projectId, doorNr);

			if (projectDoor != null)
			{
				var __template = new WorkorderTemplate
				{
					IsAdHoc = true,
					DoorCount = 1,

					ProjectId = projectId,
					CompanyId = companyId,
					LastModifiedUser = userId,
					ModifiedDate = DateTime.UtcNow,

					Name = $"Ad-hoc / {projectDoor.DoorNo} / {DateTime.UtcNow.ToString("dd.mm.yyyy")}",
					
					RemindBeforeDays = -1,
					NotificationMailSent = true,
				};

				var template = await ObjectsLocator.WorkorderTemplateService.CreateWordorderTemplate(
					projectId,
					__template,
					new List<WorkorderTemplateDoor> 
					{ 
						new WorkorderTemplateDoor
						{
							ProjectId = projectId,
							Door = projectDoor,
						}
					},
					new List<WorkorderTemplateHardwareCollection> { },
					new List<Workorder>
					{
						new Workorder
						{
							ProjectId = projectId,
							IsPreviewOnly = false,
							DueNotificationMailSent = false,
                            ReminderNotificationMailSent = false,

							StartDate = DateTime.UtcNow,
							LastModifiedUser = userId,
							ModifiedDate = DateTime.UtcNow,
						}
					},
					userId
				);

				var __workorder = await ObjectsLocator.WorkorderService.GetFirstWorkorderByTemplateId(template.Id);

				var workorder = await ObjectsLocator.WorkorderService.StartWorkorder(__workorder.Id, userId);

				//var workorderDoor = await ObjectsLocator.WorkorderDoorService.GetFirstDoorInWorkorder(workorder.Id);

				//return workorderDoor;

				return workorder;
			}

			return null;
		}
	}
}
