using MicroBuild.Management.Common.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroBuild.Management.Domain.Interfaces
{
    public interface IWorkorderTemplateDoorService
    {
        Task<bool> CreateWorkorderTemplateDoors(IEnumerable<WorkorderTemplateDoor> workorderTemplateDoor, string projectId, string templateId);
		
		Task<IEnumerable<WorkorderTemplateDoor>> GetTemplateDoors(string templateId);
		
		IEnumerable<WorkorderTemplateDoor> SetHardwareOwnership(IEnumerable<WorkorderTemplateDoor> templateDoors, IEnumerable<WorkorderTemplateHardwareCollection>  hardwareCollections);
		
		Task DeleteWorkorderTemplateDoors(string templateId);

		Task<IEnumerable<WorkorderTemplateDoor>> GetTemplateDoorsByProjectIdAsync(string projectId);

		Task<Dictionary<string, string>> CreateBulkAsync(string newProjectId, IEnumerable<WorkorderTemplateDoor> items);
		
		Task DeleteBulk(string[] ids);
	}
}