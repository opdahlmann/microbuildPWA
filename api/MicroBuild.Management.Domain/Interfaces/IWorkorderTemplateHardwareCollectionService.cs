using MicroBuild.Management.Common.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroBuild.Management.Domain.Interfaces
{
    public interface IWorkorderTemplateHardwareCollectionService
    {
        Task<IEnumerable<WorkorderTemplateHardwareCollection>> CreateHardwareCollections(IEnumerable<WorkorderTemplateHardwareCollection> workorderTemplateHardwareCollections,string workorderTemplateId,string projectId);
		
		Task DeleteHardwareCollections(string templateId);

		Task<IEnumerable<WorkorderTemplateHardwareCollection>> GetHardwareCollections(string templateId);

		Task<IEnumerable<WorkorderTemplateHardwareCollection>> GetHardwareCollectionsByProjectId(string projectId);
		Task<Dictionary<string, string>> CreateBulkAsync(string newProjectId, IEnumerable<WorkorderTemplateHardwareCollection> items);
		Task DeleteBulk(string[] ids);
	}
}