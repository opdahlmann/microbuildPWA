using MicroBuild.Management.Common.DTO;
using MicroBuild.Management.Data.API;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroBuild.Management.Data.Mongo
{
    public class WorkorderTemplateRepository : BaseRepository<WorkorderTemplate>, IWorkorderTemplateRepository
    {
		public async Task<WorkorderTemplate> CreateWorkorderTemplate(WorkorderTemplate workorderTemplate)
		{
            try
            {
                return await GenericRepository.Add(workorderTemplate);
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<List<WorkorderTemplate>> GetAllWordorderTemplates()
        {
            try
            {
                return await GenericRepository.GetAll();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<List<WorkorderTemplate>> GetAllWordordersByProjectId(string projectId)
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

        public async Task<WorkorderTemplate> GetWorkorderTemplateByTemplateId(string templateId)
        {
            try
            {
                return await GenericRepository.GetById(templateId);
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<WorkorderTemplate> UpdateWordorderTemplate(WorkorderTemplate workorderTemplate)
        {
            try
            {
                return await GenericRepository.UpdateById(workorderTemplate);
            }
            catch (Exception)
            {
                return null;
            }
        }

		public async Task<Dictionary<string, string>> AddWithIdMapAsync(IEnumerable<WorkorderTemplate> items)
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

		public async Task DeleteBulk(string[] ids)
		{
			foreach (var id in ids)
			{
				await GenericRepository.Remove(id);
			}
		}
	}
}