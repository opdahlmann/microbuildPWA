using MicroBuild.Management.Common.DTO;
using MicroBuild.Management.Common.ViewObjects;
using MicroBuild.Management.Data.API;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroBuild.Management.Data.Mongo
{
    public class WorkorderDoorRepository : BaseRepository<WorkorderDoor>, IWorkorderDoorRepository
    {
		public async Task Add(IEnumerable<WorkorderDoor> items)
		{
			await GenericRepository.Add(items);
		}

		public async Task<Dictionary<string, string>> AddWithIdMapAsync(IEnumerable<WorkorderDoor> items)
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

		public async Task<WorkorderDoor> CreateWorkorderDoor(WorkorderDoor workorderDoor)
        {
            try
            {
                return await GenericRepository.Add(workorderDoor);
            }
            catch (Exception)
            {
                return null;
            }
        }

		public async Task DeleteBulk(string[] ids)
		{
			foreach (var id in ids)
			{
				await GenericRepository.Remove(id);
			}
		}

		public async Task<List<WorkorderDoor>> GetAllWordorderDoorsByProjectId(string projectId)
        {
            try
            {
                return await GenericRepository.GetAll(x=>x.ProjectId.Equals(projectId));
            }
            catch (Exception)
            {
                return null;
            }
        }

		public async Task<List<WorkorderDoor>> GetAllWordorderDoorsByWorkorderId(object workorderId)
		{
			try
			{
				return await GenericRepository.GetAll(x => x.WorkorderId.Equals(workorderId));
			}
			catch (Exception)
			{
				return null;
			}
		}

		public async Task<WorkorderDoor> GetFirstDoorInWorkorder(string workorderId)
		{
			return await Task.Run(() =>
			{
				var filter = Builders<WorkorderDoor>.Filter.Eq(d => d.WorkorderId, workorderId);

				return GenericRepository.Collection.Find(filter).Limit(1).FirstOrDefault();
			});
		}

		public async Task<List<DoorNotificationsViewModel>> GetAllDoorsNotificationsInWorkorderInstance(object workorderId)
        {
            try
            {
                var val = await GenericRepository.GetAll(x => x.WorkorderId.Equals(workorderId));
                return val.Select(x=> new DoorNotificationsViewModel
                {
                    DoorId = x.Id,
                    DoorNo =x.Door.DoorNo
                }).ToList();
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<List<WorkorderDoor>> GetWorkOrderDoorByDoorNo(string workorderId, string doorNo)
        {
            try
            {
                return await GenericRepository.GetAll(x => x.WorkorderId.Equals(workorderId) && x.Door.DoorNo.Equals(doorNo));
            }
            catch(Exception)
            {
                return null;
            }
        }

		public async Task<WorkorderDoor> GetDoorByDoorNo(string projectId, string doorNr)
		{
			return await Task.Run(() =>
			{
				var filter = Builders<WorkorderDoor>.Filter.Eq(d => d.ProjectId, projectId) & Builders<WorkorderDoor>.Filter.Eq(d => d.ProjectId, projectId);

				return GenericRepository.Collection.Find(filter).Limit(1).FirstOrDefault();
			});
		}

		public async Task<List<WorkorderDoor>> GetDoorByDoorIdAsList(string workorderId, string doorId)
        {
            try
            {

                return await GenericRepository.GetAll(x => x.WorkorderId.Equals(workorderId) && x.Id.Equals(doorId));
              
            }
            catch (Exception)
            {
                return null;
            }
        }

		public async Task<WorkorderDoor> GetDoorByDoorId(string workorderId, string doorId)
		{
			return await GenericRepository.Collection.Find(door => door.WorkorderId == workorderId && door.Id == doorId).SingleAsync();
		}

		public async Task<WorkorderDoor> UpdateWordorderDoor(WorkorderDoor door)
        {
            try
            {
                
                return await GenericRepository.UpdateById(door);
            }
            catch (Exception)
            {
                return null;
            }
        }
	}
}