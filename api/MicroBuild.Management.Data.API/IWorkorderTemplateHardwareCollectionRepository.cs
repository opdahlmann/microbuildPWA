using MicroBuild.Management.Common.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroBuild.Management.Data.API
{
    public interface IWorkorderTemplateHardwareCollectionRepository
    {
        Task<WorkorderTemplateHardwareCollection> CreateHardwareCollection(WorkorderTemplateHardwareCollection workorderTemplateHardwareCollection);
		
		Task RemoveAllHardwareCollections(string templateId);
		
		Task<IEnumerable<WorkorderTemplateHardwareCollection>> GetHardwareCollections(string templateId);

		Task<IEnumerable<WorkorderTemplateHardwareCollection>> GetHardwareCollectionsByProjectId(string projectId);
		Task<Dictionary<string, string>> AddWithIdMapAsync(IEnumerable<WorkorderTemplateHardwareCollection> items);
		
		Task DeleteBulk(string[] ids);
	}
}