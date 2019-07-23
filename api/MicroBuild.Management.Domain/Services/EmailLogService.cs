using MicroBuild.Infrastructure.Models;
using MicroBuild.Management.Common.DTO;
using MicroBuild.Management.Common.Models;
using MicroBuild.Management.Data.API;
using MicroBuild.Management.Domain.Interfaces;
using MicroBuild.Management.Domain.ServiceClients;
using MicroBuild.Management.Domain.Services.Domain;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace MicroBuild.Management.Domain.Services
{
    public class EmailLogService : IEmailLogService
    {
        private IEmailLogRepository _emailLogRepository;
        private IObjectsLocator ObjectLocator;

        public EmailLogService(IDataUnitOfWork dataUnitOfWork, IObjectsLocator objectLocator)
        {
            _emailLogRepository = dataUnitOfWork.EmailLogRepository;
            this.ObjectLocator = objectLocator;
        }

        public async Task<EmailLog> LogSentEmail(EmailLog log)
        {
            return await _emailLogRepository.LogSentEmail(log);
        }

		public async Task<List<EmailLog>> GetEmailLogsAsync(string projectId)
		{
			return await _emailLogRepository.GetEmailLogsAsync(projectId);
		}

		public async Task CreateBulkAsync(string newProjectId, IEnumerable<EmailLog> items)
		{
			foreach (var item in items)
			{
				item.ProjectId = newProjectId;
			}
			await _emailLogRepository.CreateBulkAsync(items);
		}
	}
}