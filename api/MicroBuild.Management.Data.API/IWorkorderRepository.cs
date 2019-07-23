using System.Collections.Generic;
using System.Threading.Tasks;

using MicroBuild.Management.Common.DTO;

namespace MicroBuild.Management.Data.API
{
    public interface IWorkorderRepository
    {
        Task<List<Workorder>> GetAllWordordersByProjectId(string projectId);
        Task<Workorder> GetWorkorderById(string workorderId);
        Task<Workorder> CreateWorkorder(Workorder workorder);
        Task<Workorder> UpdateWorkorder(Workorder workorder);
        Task<List<Workorder>> GetAllWordordersNotStarted();
        Task<List<Workorder>> GetWorkordersByTemplateId(string templateId);
		Task<List<Workorder>> GetUnfinishedWorkordersByTemplateId(string templateId);
		Task RemovePreviewWorkorders(string templateId);
		Task<Dictionary<string, string>> AddWithIdMapAsync(IEnumerable<Workorder> items);
		Task DeleteBulk(string[] ids);
		Task<Workorder> GetFirstWorkorderByTemplateId(string templateId);
	}
}
