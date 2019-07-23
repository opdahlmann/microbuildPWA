using MicroBuild.Management.Common.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroBuild.Management.Data.API
{
    public interface IEmailLogRepository
    {
        Task<EmailLog> LogSentEmail(EmailLog log);

		Task<List<EmailLog>> GetEmailLogsAsync(string projectId);
		
		Task<Dictionary<string, string>> AddWithIdMapAsync(IEnumerable<EmailLog> items);
		
		Task CreateBulkAsync(IEnumerable<EmailLog> items);
	}
}