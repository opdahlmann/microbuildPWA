using System.Collections.Generic;
using System.Threading.Tasks;
using MicroBuild.Management.Common.DTO;
using MicroBuild.Management.Data.API;
using MicroBuild.Management.Domain.Util;
using MicroBuild.Management.Domain.Validation;
using MongoDB.Driver;

namespace MicroBuild.Management.Data.Mongo
{
    public class DoorRepository : BaseRepository<Door>, IDoorRepository
    {
        public async Task<List<Door>> GetDoorsByProjectIdAsync(string projectId)
        {
            return await GenericRepository.GetAll(
                d =>
                    d.ProjectId.Equals(projectId)
            );
        }


        public async Task<List<Door>> GetSpecificDoorsAsync(string projectId, List<string> doorIds)
        {
            return await GenericRepository.GetAll(
                d =>
                    d.ProjectId.Equals(projectId)
                    && doorIds.Contains(d.Id)
            );
        }

        public async Task<Door> GetSpecificDoorAsync(string doorId)
        {
            return await GenericRepository.GetById(doorId);
        }

        public async Task<long> UpdateManyAsync<T>(List<string> doorIds, string property, T value)
        {
            var collection = GenericRepository.GetCollection();
            var filter = Builders<Door>.Filter.In(d => d.Id, doorIds);
            var update = Builders<Door>.Update.Set(property, value);
            var updateResult = await collection.UpdateManyAsync(filter, update);

            return updateResult.ModifiedCount;
        }

        public async Task<Door> UpdateOneAsync(Door door)
        {
            return await GenericRepository.UpdateById(door);
        }

        public async Task UpdateManyAsync<T>(List<DoorPropertyUpdate<T>> doorPropertyUpdates, string property)
        {
            var collection = GenericRepository.GetCollection();

            foreach (var doorPropertyUpdate in doorPropertyUpdates)
            {
                var filter = Builders<Door>.Filter.Eq("Id", doorPropertyUpdate.DoorId);
                var update = Builders<Door>.Update.Set(property, doorPropertyUpdate.PropertyValue);
                await collection.UpdateOneAsync(filter, update);
            }
        }

        public async Task DeleteById(string doorId)
        {
            await GenericRepository.Remove(doorId);
        }

        public async Task AddDoorsAsync(List<Door> doors)
        {
            await GenericRepository.Add(doors);
        }

        public async Task<Dictionary<string, string>> AddDoorsAsyncWithIdMap(IEnumerable<Door> doors)
        {
            var doorIdMap = new Dictionary<string, string>();
            foreach (var door in doors)
            {
                var oldDoorId = door.Id;
                door.Id = null;
                var createdDoor = await GenericRepository.Add(door);
                doorIdMap.Add(oldDoorId, createdDoor.Id);
            }

            return doorIdMap;
        }

        public async Task<List<Door>> GetDoorsBySelectedDoorNo(string projectId, List<string> doorNos)
        {
            return await GenericRepository.GetAll(d => d.ProjectId.Equals(projectId) && doorNos.Contains(d.DoorNo));
        }

        public async Task<long> GetDoorsCountByProjectIdAsync(string projectId)
        {
            var filter = Builders<Door>.Filter.Eq("ProjectId", projectId);
            long count = await GenericRepository.GetCountByFilterAsync(filter);
            return count;
        }

        public async Task DeleteByProjectId(string projectId)
        {
            var collection = GenericRepository.GetCollection();

            await collection.DeleteManyAsync(Builders<Door>.Filter.Eq("ProjectId", projectId));
        }

        public async Task<Door> AttachCheckList<T>(string changingProperty, T valueForProperty, string changingDoorId, string hardwareFieldName)
        {
            Door changedDoor = await GetSpecificDoorAsync(changingDoorId);
            new DoorValidator().ValidateDoorUpdate(changedDoor, changingProperty);
            ObjectUtil.SetValue(changedDoor, changingProperty, valueForProperty);
            return await UpdateOneAsync(changedDoor);
        }

		public async Task<Door> GetDoorByDoorNo(string projectId, string doorNr)
		{
			return await Task.Run(() =>
			{
				var filter = Builders<Door>.Filter.Eq(d => d.ProjectId, projectId) & Builders<Door>.Filter.Eq(d => d.DoorNo, doorNr);

				return GenericRepository.Collection.Find(filter).Limit(1).FirstOrDefault();
			});
		}
	}
}
