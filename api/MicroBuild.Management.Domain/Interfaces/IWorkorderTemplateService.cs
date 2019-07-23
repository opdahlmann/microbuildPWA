using System.Collections.Generic;
using System.Threading.Tasks;

using MicroBuild.Management.Common.DTO;
using MicroBuild.Management.Common.ViewObjects;

namespace MicroBuild.Management.Domain.Interfaces
{
    public interface IWorkorderTemplateService
    {
        Task<List<WorkorderTemplate>> GetAllWordordersByProjectId(string projectId);

        Task<WorkorderTemplate> GetWorkorderTemplateByTemplateId(string templateId);

        Task<List<WorkorderTemplate>> GetAllWordorderTemplates();

        Task<WorkorderTemplate> UpdateWordorderTemplate(WorkorderTemplate workorderTemplate);

		Task<WorkorderTemplate> CreateWordorderTemplate(
			string projectId,
			WorkorderTemplate template,
			IEnumerable<WorkorderTemplateDoor> templateDoors,
			IEnumerable<WorkorderTemplateHardwareCollection> hardwareCollections,
			IEnumerable<Workorder> workorders,
			string userId
		);

		Task<WorkorderTemplate> UpdateWordorderTemplate(
			string projectId,
			WorkorderTemplate template,
			IEnumerable<WorkorderTemplateDoor> templateDoors,
			IEnumerable<WorkorderTemplateHardwareCollection> hardwareCollections,
			IEnumerable<Workorder> workorders,
			string userId
		);

		Task<Dictionary<string, string>> CreateBulkAsync(string newProjectId, IEnumerable<WorkorderTemplate> items);

		Task DeleteBulk(string[] ids);

        Task<List<WorkorderTemplate>> GetProjectWorkOrderTemplatesByCompanyId(string projectId, string companyId);

		Task<IEnumerable<WorkorderTemplateListViewModel>> GetProjectWorkordersOfUserForClient(string projectId, string userId);
		
		Task<IEnumerable<WorkorderTemplateListViewModel>> GetAllProjectWorkorders(string projectId, string userId);
    }
}