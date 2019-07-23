using MicroBuild.Management.Common.DTO;
using MicroBuild.Management.Common.Models;
using MicroBuild.Management.Common.ViewObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroBuild.Management.Domain.Interfaces
{
    public interface IChecklistService
    {
        Task<Checklist> CreateChecklist(Checklist checklist);
        Task<Checklist> UpdateChecklist(Checklist checklist);
        Task<List<Checklist>> GetAll(string projectId);
        Task<bool> IsExist(string projectId, string checklistId);
        Task<object> IsUniqueName(string projectId, string checklistName);
        Task<long> AttachChecklistForHardwareCollectionItems(string checklistId, List<HardwareInDoorsRequestModel> hardwareList);
        Task<bool> DeleteChecklist(string checklistId);
		Task<Dictionary<string, string>> CreateBulk(string newProjectId, IEnumerable<Checklist> items);
		Task DeleteBulk(string[] ids);
        Task<Checklist> GetChecklistByChecklistId(string checklistId);

    }
}
