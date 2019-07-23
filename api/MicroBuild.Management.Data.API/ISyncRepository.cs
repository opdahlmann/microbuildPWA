using MicroBuild.Management.Common.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroBuild.Management.Data.API
{
    public interface ISyncRepository
    {
        Task<List<Sync>> GetByProjectId(string projectId);
        Task<Sync> GetById(string id);
        Task<List<Sync>> GetSyncMetaDataProjectionByProjectId(string projectId);
        Task<Sync> Save(Sync syncLog);
		Task<Dictionary<string, string>> AddWithIdMapAsync(IEnumerable<Sync> items);
		Task AddBulk(IEnumerable<Sync> items);
	}
}
