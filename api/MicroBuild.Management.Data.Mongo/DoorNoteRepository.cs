using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using MicroBuild.Management.Common.DTO;
using MicroBuild.Management.Data.API;
using MongoDB.Driver;

namespace MicroBuild.Management.Data.Mongo
{
	public class DoorNoteRepository : BaseRepository<DoorNote>, IDoorNoteRepository
	{
		public async Task<DoorNote> CreateDoorNote(DoorNote note)
		{
			return await GenericRepository.Add(note);
		}

		public async Task<IEnumerable<DoorNote>> GetAllDoorNotesByWorkorderDoorId(string doorId)
		{
			return await GenericRepository.GetAll(x => x.DoorId == doorId);
		}

		public async Task<IEnumerable<DoorNote>> GetAllDoorNotesByWorkorderId(string workorderId)
		{
			return await GenericRepository.GetAll(x => x.WorkorderId == workorderId);
		}

		public async Task<long> GetNotesCountByDoorIdAsync(string doorId)
		{
			var filter = Builders<DoorNote>.Filter.Eq(_ => _.DoorId, doorId);
			return await GenericRepository.Collection.CountAsync(filter);
		}

        public async Task DeleteDoorNote(string noteId)
        {
                await GenericRepository.Remove(noteId);
        }

        public async Task DeleteDoorNoteByProjectId(string projectId)
        {
            var collection = GenericRepository.GetCollection();

            await collection.DeleteManyAsync(Builders<DoorNote>.Filter.Eq("ProjectId", projectId));
        }

        public async Task<List<DoorNote>> GetDoorNotesByProjectId(string projectId)
        {
            return await GenericRepository.GetAll(x => x.ProjectId == projectId);
        }

        public async Task<Dictionary<string, string>> AddWithIdMapAsync(IEnumerable<DoorNote> items)
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
