using MicroBuild.Infrastructure.Models;
using MicroBuild.Management.Common.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroBuild.Management.Domain.Interfaces
{
    public interface IEmailLogService
    {
        Task<EmailLog> LogSentEmail(EmailLog log);

		Task<List<EmailLog>> GetEmailLogsAsync(string projectId);

		Task CreateBulkAsync(string newProjectId, IEnumerable<EmailLog> items);
	}
}