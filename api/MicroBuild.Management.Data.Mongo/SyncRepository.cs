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
    public class SyncRepository : BaseRepository<Sync>, ISyncRepository
    {
		public async Task AddBulk(IEnumerable<Sync> items)
		{
			foreach (var item in items)
			{
				item.Id = null;
			}

			await GenericRepository.Add(items);
		}

		public async Task<Dictionary<string, string>> AddWithIdMapAsync(IEnumerable<Sync> items)
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

		public async Task<Sync> GetById(string id)
        {
            return await GenericRepository.GetById(id);
        }

        public async Task<List<Sync>> GetByProjectId(string projectId)
        {
            return await GenericRepository.GetAll(x => x.ProjectId == projectId);
        }

        public async Task<List<Sync>> GetSyncMetaDataProjectionByProjectId(string projectId)
        {
            var condition = Builders<Sync>.Filter.Eq(x => x.ProjectId, projectId);
            var fields = Builders<Sync>.Projection
                .Include(x => x.Id)
                .Include(x => x.ProjectId)
                .Include(x => x.IsInvalid)
                .Include(x => x.Error)
                .Include(x => x.Timestamp)
                .Include(x => x.UserId)
                ;
            return await GenericRepository.Collection.Find(condition).Project<Sync>(fields).ToListAsync();
        }

        public async Task<Sync> Save(Sync syncLog)
        {
            return await GenericRepository.Add(syncLog);
        }
    }
}
