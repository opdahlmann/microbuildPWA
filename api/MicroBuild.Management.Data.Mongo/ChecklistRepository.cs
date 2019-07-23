using MicroBuild.Management.Common.DTO;
using MicroBuild.Management.Data.API;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MicroBuild.Management.Data.Mongo
{
    public class ChecklistRepository : BaseRepository<Checklist>, IChecklistRepository
    {
		public async Task<Dictionary<string, string>> AddWithIdMap(IEnumerable<Checklist> items)
		{
			var idMap = new Dictionary<string, string>();

			foreach (var item in items)
			{
				var oldItemId = item.Id;
				item.Id = null;
				var createdItem = await GenericRepository.Add(item);
				idMap.Add(oldItemId, createdItem.Id);
			}

			return idMap;
		}

		public async Task<Checklist> CreateChecklist(Checklist checklist)
        {
            try
            {
                return await GenericRepository.Add(checklist);
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<bool> DeleteChecklist(string checklistId)
        {
            try
            {
                await GenericRepository.Remove(checklistId);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<List<Checklist>> GetAll(string projectId)
        {
            try
            {
                return await GenericRepository.GetAll(x => x.ProjectId.Equals(projectId));
            }
            catch (Exception)
            {
                return null;
            }
        }


        public bool IsExist(List<WorkorderTemplateHardwareCollection> tempalatreHardware, string checklistId)
        {
            try
            {
                foreach (var hw in tempalatreHardware)
                {
                    if (hw != null && !string.IsNullOrEmpty(hw.ChecklistId))
                    {
                        if (hw.ChecklistId.Equals(checklistId))
                        {
                            return true;
                        }
                    }
                }
                return false;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public async Task<object> IsUniqueName(string projectId, string checklistName)
        {
            try
            {
                var all = await GenericRepository.GetAll(x => x.Name.Equals(checklistName) && x.ProjectId.Equals(projectId));
                var reslts = new
                {
                    IsUnique = all.Count > 0 ? false : true,
                    items = all.Select(x => x.Name).ToArray()
                };
                return reslts;
            }
            catch (Exception)
            {
                return new
                {
                    IsUnique = false,
                    items = new string[] { }
                };
            }
        }

        public async Task<Checklist> UpdateChecklist(Checklist checklist)
        {
            try
            {
                return await GenericRepository.UpdateById(checklist);
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<Checklist> GetChecklistBychecklistId(string checkListId)
        {
            return await GenericRepository.GetById(checkListId);
        }
    }
}
