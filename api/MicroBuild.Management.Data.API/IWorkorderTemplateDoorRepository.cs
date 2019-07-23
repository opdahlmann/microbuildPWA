using MicroBuild.Management.Common.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroBuild.Management.Data.API
{
    public interface IWorkorderTemplateDoorRepository
    {
        Task<WorkorderTemplateDoor> CreateWorkorderTemplateDoor(WorkorderTemplateDoor workorderTemplateDoor);
		
		Task<IEnumerable<WorkorderTemplateDoor>> GetTemplateDoors(string templateId);
		
		Task DeleteTemplateDoors(string templateId);
		
		Task<IEnumerable<WorkorderTemplateDoor>> GetTemplateDoorsByProjectIdAsync(string projectId);
		Task<Dictionary<string, string>> AddWithIdMapAsync(IEnumerable<WorkorderTemplateDoor> items);
		void DeleteBulk(string[] ids);
	}
}