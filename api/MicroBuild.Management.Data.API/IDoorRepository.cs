using MicroBuild.Management.Common.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroBuild.Management.Data.API
{
    public interface IDoorRepository
    {
        Task AddDoorsAsync(List<Door> doors);

        Task<Dictionary<string, string>> AddDoorsAsyncWithIdMap(IEnumerable<Door> doors);

        Task<List<Door>> GetDoorsByProjectIdAsync(string projectId);

        Task<List<Door>> GetDoorsBySelectedDoorNo(string projectId, List<string> doorNos);

        Task<List<Door>> GetSpecificDoorsAsync(string projectId, List<string> doorIds);

        Task<Door> GetSpecificDoorAsync(string doorId);

        Task<long> UpdateManyAsync<T>(List<string> doorIds, string property, T value);

        Task<Door> UpdateOneAsync(Door door);

        Task UpdateManyAsync<T>(List<DoorPropertyUpdate<T>> doorPropertyUpdates, string property);

        Task DeleteById(string doorId);

        Task DeleteByProjectId(string projectId);

        Task<long> GetDoorsCountByProjectIdAsync(string id);

        Task<Door> AttachCheckList<T>(string changingProperty, T valueForProperty, string changingDoorId, string hardwareFieldName);
		
		Task<Door> GetDoorByDoorNo(string projectId, string doorNr);
	}
}
