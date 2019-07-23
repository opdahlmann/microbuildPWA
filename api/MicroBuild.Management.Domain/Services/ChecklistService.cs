using MicroBuild.Management.Common.DTO;
using MicroBuild.Management.Common.Models;
using MicroBuild.Management.Common.ViewObjects;
using MicroBuild.Management.Data.API;
using MicroBuild.Management.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroBuild.Management.Domain.Services
{
    public class ChecklistService: IChecklistService
    {
        private IChecklistRepository _checklistRepository;
        private IDoorRepository _doorRepository;
        private IWorkorderTemplateHardwareCollectionRepository _workorderTemplateHardwareCollectionRepository;

        public ChecklistService(IDataUnitOfWork dataUnitOfWork)
        {
            _checklistRepository = dataUnitOfWork.ChecklistRepository;
            _doorRepository = dataUnitOfWork.DoorRepository;
            _workorderTemplateHardwareCollectionRepository = dataUnitOfWork.WorkorderTemplateHardwareCollectionRepository;
        }

        public async Task<long> AttachChecklistForHardwareCollectionItems(string checklistId, List<HardwareInDoorsRequestModel> hardwareList)
        {
            long updatedItemCount = 0;
            foreach (var hardware in hardwareList)
            {
                foreach (var doorId in hardware.DoorIds)
                {
                    await this._doorRepository.AttachCheckList(hardware.FieldName + ".ChecklistId", checklistId, doorId, hardware.FieldName);
                    updatedItemCount += 1;
                }
            }
            return updatedItemCount;
        }


        #region Checklists CRUD

        public async Task<Checklist> CreateChecklist(Checklist checklist)
        {
            return await this._checklistRepository.CreateChecklist(checklist);
        }

		public async Task<Dictionary<string, string>> CreateBulk(string newProjectId, IEnumerable<Checklist> items)
		{
			foreach (var item in items)
			{
				item.ProjectId = newProjectId;
			}
			return await _checklistRepository.AddWithIdMap(items);
		}

		public async Task<bool> DeleteChecklist(string checklistId)
        {
            return await this._checklistRepository.DeleteChecklist(checklistId);
        }

        public async Task<List<Checklist>> GetAll(string projectId)
        {
            return await this._checklistRepository.GetAll(projectId);
        }

        public async Task<bool> IsExist(string projectId, string checklistId)
        {
            var templaHW = await this._workorderTemplateHardwareCollectionRepository.GetHardwareCollectionsByProjectId(projectId);
            return this._checklistRepository.IsExist(templaHW.ToList(), checklistId);
        }

        public async Task<object> IsUniqueName(string projectId, string checklistName)
        {
            return await this._checklistRepository.IsUniqueName(projectId, checklistName);
        }

        public async Task<Checklist> UpdateChecklist(Checklist checklist)
        {
            return await this._checklistRepository.UpdateChecklist(checklist);
        }

		public async Task DeleteBulk(string[] ids)
		{
			foreach (var id in ids)
			{
				await _checklistRepository.DeleteChecklist(id);
			}
		}

        public async Task<Checklist> GetChecklistByChecklistId(string checklistId)
        {
            return await this._checklistRepository.GetChecklistBychecklistId(checklistId);
        }

		#endregion
	}
}
