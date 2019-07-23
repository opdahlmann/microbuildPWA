using MicroBuild.Management.Common.DTO;
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
    public class WorkorderTemplateService : IWorkorderTemplateService
    {
		private IDataUnitOfWork dataUnitOfWork;
		private IObjectsLocator objectLocator;

		public WorkorderTemplateService(IDataUnitOfWork dataUnitOfWork, IObjectsLocator objectLocator)
		{
			this.dataUnitOfWork = dataUnitOfWork;
			this.objectLocator = objectLocator;
		}

		/*
			Creation 
			--------
			1. Save `WorkOrderTemplate`
			2. Save `TemplateHardwareCollection`
			3. Map `IsMaintainable` and `ChecklistId` to `TemplateDoors`
			4. Save `TemplateDoors`
			5. Save `WorkOrder` instances
		 */
		public async Task<WorkorderTemplate> CreateWordorderTemplate(
			string projectId, 
			WorkorderTemplate template, 
			IEnumerable<WorkorderTemplateDoor> templateDoors, 
			IEnumerable<WorkorderTemplateHardwareCollection> hardwareCollections, 
			IEnumerable<Workorder> workorders, 
			string userId
			)
        {
			template.ProjectId = projectId;

			template.DoorCount = (templateDoors != null) ? templateDoors.Count() : 0;

			template.ModifiedDate = DateTime.Now;
			template.LastModifiedUser = userId;

			template = await dataUnitOfWork.WorkorderTemplateRepository.CreateWorkorderTemplate(template);

			if (template != null)
			{
				if (templateDoors != null)
				{
					if (hardwareCollections != null)
					{
						hardwareCollections = await objectLocator.WorkorderTemplateHardwareCollectionService.CreateHardwareCollections(hardwareCollections, template.Id, projectId);

						templateDoors = objectLocator.WorkorderTemplateDoorService.SetHardwareOwnership(templateDoors, hardwareCollections);

						var isDoorCreationSuccess = await objectLocator.WorkorderTemplateDoorService.CreateWorkorderTemplateDoors(templateDoors, projectId, template.Id);
					}

					if (workorders != null)
					{
						await objectLocator.WorkorderService.CreateWorkorders(workorders, userId, template.Id, projectId);
					}
				}
			}

			return template;
		}

		/*
			Update
			------
			1. Update `WorkOrderTemplate`
			2. Remove and save all `TemplateHardwareCollection`
			3. Map `IsMaintainable` and `ChecklistId` to `TemplateDoors`
			4. Remove and save all `TemplateDoors`
			5. Save new `WorkOrder` instances 
		 */
		public async Task<WorkorderTemplate> UpdateWordorderTemplate(
			string projectId, 
			WorkorderTemplate template, 
			IEnumerable<WorkorderTemplateDoor> templateDoors, 
			IEnumerable<WorkorderTemplateHardwareCollection> hardwareCollections,
			IEnumerable<Workorder> workorders, 
			string userId
			)
		{
			template.ProjectId = projectId;

			template.DoorCount = (templateDoors != null) ? templateDoors.Count() : 0;

			template.ModifiedDate = DateTime.Now;
			template.LastModifiedUser = userId;

			template = await dataUnitOfWork.WorkorderTemplateRepository.UpdateWordorderTemplate(template);

			if (template != null)
			{
				if (templateDoors != null)
				{
					if (hardwareCollections != null)
					{
						// Remove all hardware-collections, and  re-create them from the request
						await objectLocator.WorkorderTemplateHardwareCollectionService.DeleteHardwareCollections(template.Id);
						hardwareCollections = await objectLocator.WorkorderTemplateHardwareCollectionService.CreateHardwareCollections(hardwareCollections, template.Id, projectId);

						templateDoors = objectLocator.WorkorderTemplateDoorService.SetHardwareOwnership(templateDoors, hardwareCollections);

						// Remove all template doors, and  re-create them from the request
						await objectLocator.WorkorderTemplateDoorService.DeleteWorkorderTemplateDoors(template.Id);
						var isDoorCreationSuccess = await objectLocator.WorkorderTemplateDoorService.CreateWorkorderTemplateDoors(templateDoors, projectId, template.Id);
					}

					if (workorders != null)
					{
						// Remove all unstarted workorder instances, and  re-create them from the request
						await objectLocator.WorkorderService.DeletePreviewWorkorders(projectId, template.Id, userId);
						var newWorkorders = workorders.Where(x => x.IsPreviewOnly);
						await objectLocator.WorkorderService.CreateWorkorders(newWorkorders, userId, template.Id, projectId);
					}
				}
			}

			return template;
		}

		public async Task<List<WorkorderTemplate>> GetAllWordordersByProjectId(string projectId)
        {
            return await dataUnitOfWork.WorkorderTemplateRepository.GetAllWordordersByProjectId(projectId);
        }

        public async Task<List<WorkorderTemplate>> GetAllWordorderTemplates()
        {
            return await dataUnitOfWork.WorkorderTemplateRepository.GetAllWordorderTemplates();
        }

        public async Task<WorkorderTemplate> GetWorkorderTemplateByTemplateId(string templateId)
        {
            return await dataUnitOfWork.WorkorderTemplateRepository.GetWorkorderTemplateByTemplateId(templateId);
        }

        public async Task<WorkorderTemplate> UpdateWordorderTemplate(WorkorderTemplate workorderTemplate)
        {
            return await dataUnitOfWork.WorkorderTemplateRepository.UpdateWordorderTemplate(workorderTemplate);
        }

		public async Task<Dictionary<string, string>> CreateBulkAsync(string newProjectId, IEnumerable<WorkorderTemplate> items)
		{
			foreach (var item in items)
			{
				item.ProjectId = newProjectId;
			}
			return await dataUnitOfWork.WorkorderTemplateRepository.AddWithIdMapAsync(items);
		}

		public async Task DeleteBulk(string[] ids)
		{
			await dataUnitOfWork.WorkorderTemplateRepository.DeleteBulk(ids);
		}

        public async Task<List<WorkorderTemplate>> GetProjectWorkOrderTemplatesByCompanyId(string projectId, string companyId)
        {
            var allWorkOrders =  await GetAllWordordersByProjectId(projectId);
            return allWorkOrders.FindAll(x => x.CompanyId == companyId);
        }

		public async Task<IEnumerable<WorkorderTemplateListViewModel>> GetProjectWorkordersOfUserForClient(string projectId, string userId)
		{
			var workorders = await objectLocator.WorkorderTemplateService.GetAllWordordersByProjectId(projectId);
			var users = await objectLocator.MbeUsersService.GetAllUsers();
			var company = await objectLocator.CompanyService.GetProjectCompanyForUser(projectId, userId);

			var userWorkorders = workorders
			.Where(wo => wo.CompanyId == company.Id)
			.Select(x => new WorkorderTemplateListViewModel
				{
					Id = x.Id,
					IsAdHoc = x.IsAdHoc,
					DoorCount = x.DoorCount,
					LastModifiedUser = x.LastModifiedUser,
					LastModifiedUserName = (users.Where(z => z.Id == x.LastModifiedUser).Count() > 0) ? (users.Where(z => z.Id == x.LastModifiedUser).FirstOrDefault().Name) : (string.Empty),
					ModifiedDate = x.ModifiedDate,
					Name = x.Name,
					CompanyId = x.CompanyId,
					CompanyName = company.Name,
					RemindBeforeDays = x.RemindBeforeDays
				}
			);

			return userWorkorders;
		}

		public async Task<IEnumerable<WorkorderTemplateListViewModel>> GetAllProjectWorkorders(string projectId, string userId)
		{
			var workorders = await objectLocator.WorkorderTemplateService.GetAllWordordersByProjectId(projectId);
			var projectCompanies = await objectLocator.CompanyService.GetCompaniesByProjectId(projectId);
			var users = await objectLocator.MbeUsersService.GetAllUsers();

			var userWorkorders = workorders.Select(x => 
			{
				var companies = projectCompanies.Where(y => y.Id == x.CompanyId);
				var lastModifierUser = users.FirstOrDefault(z => z.Id == x.LastModifiedUser);

				return new WorkorderTemplateListViewModel
				{
					Id = x.Id,
					IsAdHoc = x.IsAdHoc,
					DoorCount = x.DoorCount,
					LastModifiedUser = x.LastModifiedUser,
					LastModifiedUserName = lastModifierUser?.Name,
					ModifiedDate = x.ModifiedDate,
					Name = x.Name,
					CompanyId = x.CompanyId,
					CompanyName = (companies.FirstOrDefault() != null) ? companies.FirstOrDefault().Name : string.Empty,
					RemindBeforeDays = x.RemindBeforeDays
				};
			});

			return userWorkorders;
		}
	}
}