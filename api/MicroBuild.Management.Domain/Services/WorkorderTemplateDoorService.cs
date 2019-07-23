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
    public class WorkorderTemplateDoorService : IWorkorderTemplateDoorService
    {
        private IWorkorderTemplateDoorRepository _workorderTemplateDoorRepository;

        public WorkorderTemplateDoorService(IDataUnitOfWork dataUnitOfWork)
        {
            _workorderTemplateDoorRepository = dataUnitOfWork.WorkorderTemplateDoorRepository;
        }

		public async Task<Dictionary<string, string>> CreateBulkAsync(string newProjectId, IEnumerable<WorkorderTemplateDoor> items)
		{
			foreach (var item in items)
			{
				item.ProjectId = newProjectId;
			}
			return await _workorderTemplateDoorRepository.AddWithIdMapAsync(items);
		}

		public async Task<bool> CreateWorkorderTemplateDoors(IEnumerable<WorkorderTemplateDoor> workorderTemplateDoor, string projectId, string templateId)
        {
            try
            {
                foreach (WorkorderTemplateDoor templateDoor in workorderTemplateDoor)
                {
					templateDoor.TemplateId = templateId;
					templateDoor.ProjectId = projectId;

					await _workorderTemplateDoorRepository.CreateWorkorderTemplateDoor(templateDoor);
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

		public async Task DeleteBulk(string[] ids)
		{
			_workorderTemplateDoorRepository.DeleteBulk(ids);
		}

		public async Task DeleteWorkorderTemplateDoors(string templateId)
		{
			await _workorderTemplateDoorRepository.DeleteTemplateDoors(templateId);
		}

		public async Task<IEnumerable<WorkorderTemplateDoor>> GetTemplateDoors(string templateId)
		{
			try
			{
				return await _workorderTemplateDoorRepository.GetTemplateDoors(templateId);
			}
			catch (Exception)
			{
				return null;
			}
		}

		public async Task<IEnumerable<WorkorderTemplateDoor>> GetTemplateDoorsByProjectIdAsync(string projectId)
		{
			try
			{
				return await _workorderTemplateDoorRepository.GetTemplateDoorsByProjectIdAsync(projectId);
			}
			catch (Exception)
			{
				return null;
			}
		}

		public IEnumerable<WorkorderTemplateDoor> SetHardwareOwnership(IEnumerable<WorkorderTemplateDoor> templateDoors, IEnumerable<WorkorderTemplateHardwareCollection> hardwareCollections)
		{
			// Same logic as ..SetHardwareOwnership(..) in:
			// WorkorderDoorService.cs

			try
			{
				foreach (var hardwareMapping in hardwareCollections)
				{
					foreach (var templateDoor in templateDoors)
					{
						var hdw = (Hardware)templateDoor.Door[hardwareMapping.FieldName];
						
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

				return templateDoors;
			}
			catch (Exception)
			{
				return null;
			}
		}
	}
}