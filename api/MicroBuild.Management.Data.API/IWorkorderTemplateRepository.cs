using MicroBuild.Management.Common.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroBuild.Management.Data.API
{
    public interface IWorkorderTemplateRepository
    {
        Task<List<WorkorderTemplate>> GetAllWordordersByProjectId(string projectId);
        Task<List<WorkorderTemplate>> GetAllWordorderTemplates();
        Task<WorkorderTemplate> CreateWorkorderTemplate(WorkorderTemplate workorderTemplate);
        Task<WorkorderTemplate> GetWorkorderTemplateByTemplateId(string templateId);
        Task<WorkorderTemplate> UpdateWordorderTemplate(WorkorderTemplate workorderTemplate);
		Task<Dictionary<string, string>> AddWithIdMapAsync(IEnumerable<WorkorderTemplate> items);
		Task DeleteBulk(string[] ids);
	}
}