using MicroBuild.Management.Common.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MicroBuild.Management.Data.API
{
    public interface IChecklistRepository
    {
        Task<Checklist> CreateChecklist(Checklist checklist);

        Task<Checklist> UpdateChecklist(Checklist checklist);

        Task<List<Checklist>> GetAll(string projectId);

        bool IsExist(List<WorkorderTemplateHardwareCollection> tempalatreHardware, string checklistId);

        Task<object> IsUniqueName(string projectId, string checklistName);

        Task<bool> DeleteChecklist(string checklistId);

		Task<Dictionary<string, string>> AddWithIdMap(IEnumerable<Checklist> items);

        Task<Checklist> GetChecklistBychecklistId(string checkListId);
    }
}
