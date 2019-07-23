using MicroBuild.Management.Common.DTO;
using MicroBuild.Management.Data.API;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroBuild.Management.Data.Mongo
{
    public class WorkorderTemplateDoorRepository : BaseRepository<WorkorderTemplateDoor>, IWorkorderTemplateDoorRepository
    {
		public async Task<Dictionary<string, string>> AddWithIdMapAsync(IEnumerable<WorkorderTemplateDoor> items)
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

		public async Task<WorkorderTemplateDoor> CreateWorkorderTemplateDoor(WorkorderTemplateDoor workorderTemplateDoor)
        {
            try
            {
                return await GenericRepository.Add(workorderTemplateDoor);
            }
            catch (Exception)
            {
                return null;
            }
        }

		public async void DeleteBulk(string[] ids)
		{
			foreach (var id in ids)
			{
				await GenericRepository.Remove(id);
			}
		}

		public async Task DeleteTemplateDoors(string templateId)
		{
			await GenericRepository.RemoveAllByCriteria(x => x.TemplateId == templateId);
		}

		public async Task<IEnumerable<WorkorderTemplateDoor>> GetTemplateDoors(string templateId)
		{
			try
			{
				return await GenericRepository.GetAll(x => x.TemplateId == templateId);
			}
			catch (Exception)
			{
				return null;
			}
		}

		public async Task<IEnumerable<WorkorderTemplateDoor>> GetTemplateDoorsByProjectIdAsync(string projectId)
		{
			try
			{
				return await GenericRepository.GetAll(x => x.ProjectId == projectId);
			}
			catch (Exception)
			{
				return null;
			}
		}
	}
}