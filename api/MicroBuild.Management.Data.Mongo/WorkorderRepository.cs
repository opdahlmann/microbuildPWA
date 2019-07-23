using MicroBuild.Management.Common.DTO;
using MicroBuild.Management.Data.API;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroBuild.Management.Data.Mongo
{
    public class WorkorderRepository : BaseRepository<Workorder>, IWorkorderRepository
    {
        public async Task<Workorder> CreateWorkorder(Workorder workorder)
        {
           return await GenericRepository.Add(workorder);
        }

        public async Task<List<Workorder>> GetAllWordordersNotStarted()
        {
            try
            {
                return await GenericRepository.GetAll(x=>x.StartDate < DateTime.Now && x.IsPreviewOnly);
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<List<Workorder>> GetAllWordordersByProjectId(string projectId)
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

        public async Task<Workorder> GetWorkorderById(string workorderId)
        {
            try
            {
                return await GenericRepository.GetById(workorderId);
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<Workorder> UpdateWorkorder(Workorder workorder)
        {
            try
            {
                return await GenericRepository.UpdateById(workorder);
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<List<Workorder>> GetWorkordersByTemplateId(string templateId)
        {
            try
            {
                return await GenericRepository.GetAll(x=>x.TemplateId.Equals(templateId));
            }
            catch (Exception)
            {
                return null;
            }
        }

		public async Task<List<Workorder>> GetUnfinishedWorkordersByTemplateId(string templateId)
		{
			try
			{
				return await GenericRepository.GetAll(x => x.TemplateId == templateId && x.FinishedDate == null);
			}
			catch (Exception)
			{
				return null;
			}
		}

		public async Task RemovePreviewWorkorders(string templateId)
		{
			await GenericRepository.RemoveAllByCriteria(x => x.TemplateId == templateId && x.IsPreviewOnly);
		}

		public async Task<Dictionary<string, string>> AddWithIdMapAsync(IEnumerable<Workorder> items)
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

		public async Task<Workorder> GetFirstWorkorderByTemplateId(string templateId)
		{
			return await Task.Run(() =>
			{
				var filter = Builders<Workorder>.Filter.Eq(d => d.TemplateId, templateId);

				return GenericRepository.Collection.Find(filter).Limit(1).FirstOrDefault();
			});
		}
	}
}