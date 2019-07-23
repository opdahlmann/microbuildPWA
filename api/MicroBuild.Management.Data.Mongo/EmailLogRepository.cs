using MicroBuild.Management.Common.DTO;
using MicroBuild.Management.Data.API;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroBuild.Management.Data.Mongo
{
    public class EmailLogRepository : BaseRepository<EmailLog>, IEmailLogRepository
    {
        public async Task<EmailLog> LogSentEmail(EmailLog log)
        {
            try
            {
                return await GenericRepository.Add(log);
            }
            catch (Exception)
            {
                return null;
            }
        }

		public async Task<List<EmailLog>> GetEmailLogsAsync(string projectId)
		{
			try
			{
				return await GenericRepository.GetAll(x => x.ProjectId == projectId);
			}
			catch (Exception)
			{
				return null;
			}
		}

		public async Task<Dictionary<string, string>> AddWithIdMapAsync(IEnumerable<EmailLog> items)
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

		public async Task CreateBulkAsync(IEnumerable<EmailLog> items)
		{
			foreach (var item in items)
			{
				item.Id = null;
			}

			await GenericRepository.Add(items);
		}
	}
}