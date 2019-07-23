using MicroBuild.Management.Common.DTO;
using MicroBuild.Management.Common.ViewObjects;
using MicroBuild.Management.Data.API;
using MicroBuild.Management.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroBuild.Management.Domain.Services
{
    public class SyncLogService : ISyncLogService
    {
        public IDataUnitOfWork DataUnitOfWork;
        private IObjectsLocator ObjectLocator;

        public SyncLogService(IDataUnitOfWork dataUnitOfWork, IObjectsLocator objectLocator)
        {
            DataUnitOfWork = dataUnitOfWork;
            ObjectLocator = objectLocator;
        }

		public async Task CreateBulkAsync(string newProjectId, IEnumerable<Sync> items)
		{
			foreach (var item in items)
			{
				item.ProjectId = newProjectId;
			}
			await DataUnitOfWork.SyncRepository.AddBulk(items);
		}

		public async Task<Sync> GetSyncLogItem(string projectId, string syncLogId)
        {
            return await DataUnitOfWork.SyncRepository.GetById(syncLogId);
        }

        public async Task<List<Sync>> GetSyncLogItems(string projectId)
        {
            return await DataUnitOfWork.SyncRepository.GetByProjectId(projectId);
        }

        public async Task<List<SyncViewModel>> GetSyncLogItemsMetadataProjection(string projectId)
        {
            var users = await ObjectLocator.UserImportAdapter.GetUsersAsync();
            var logs = await DataUnitOfWork.SyncRepository.GetSyncMetaDataProjectionByProjectId(projectId);

            return logs.Select(x =>
            {
                var sync = new SyncViewModel(x.ProjectId, x.UserId);
                sync.User = users.FirstOrDefault(e => e.Id == x.UserId);
                sync.IsInvalid = x.IsInvalid;
                sync.Timestamp = x.Timestamp;
                sync.Id = x.Id;
                return sync;
            }).ToList();
        }

        public async Task<Sync> SaveSyncLogItem(Sync syncLogItem)
        {
            return await DataUnitOfWork.SyncRepository.Save(syncLogItem);
        }
    }
}
