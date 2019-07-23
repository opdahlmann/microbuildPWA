using MicroBuild.Management.Common.DTO;
using MicroBuild.Management.Common.ViewObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroBuild.Management.Domain.Interfaces
{
    public interface ISyncLogService
    {
        Task<List<Sync>> GetSyncLogItems(string projectId);
        Task<List<SyncViewModel>> GetSyncLogItemsMetadataProjection(string projectId);
        Task<Sync> SaveSyncLogItem(Sync syncLogItem);
        Task<Sync> GetSyncLogItem(string projectId, string syncLogId);
		Task CreateBulkAsync(string newProjectId, IEnumerable<Sync> items);
	}
}
