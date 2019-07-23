using MicroBuild.Management.Common.DTO;
using MicroBuild.Management.Common.ViewObjects;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MicroBuild.Management.Domain.Interfaces
{
    public interface IWorkorderService
    {
        Task<IEnumerable<Workorder>> GetAllWorkordersByProjectId(string projectId);
        Task<Workorder> GetWorkorderById(string workorderId);
		Task<IEnumerable<Workorder>> GetWorkordersByTemplateId(string templateId);
		Task<IEnumerable<Workorder>> GetWorkordersByTemplateIdForMobile(string templateId);
		Task<bool> CreateWorkorders(IEnumerable<Workorder> workorders, string userId, string templateId, string projectId);
        Task<BulkMailResponseViewModel> SendEmailNotificationForNotStartedWorkorders();
        Task<BulkMailResponseViewModel> SendEmailReminderNotifications();
		Task DeletePreviewWorkorders(string projectId, string templateId, string userId);
		Task<Dictionary<string, string>> CreateBulkAsync(string newProjectId, IEnumerable<Workorder> items);
		Task DeleteBulk(string[] ids);
		Task<Workorder> StartWorkorder(string workorderId, string userId);
		Task<Workorder> UpdateWorkorder(Workorder workorder);
		Task<Workorder> GetFirstWorkorderByTemplateId(string id);
        Task<Workorder> FinalizeWorkorder(string workorderId, string userId);

    }
}