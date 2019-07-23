using MicroBuild.Management.Common.DTO;
using MicroBuild.Management.Data.API;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroBuild.Management.Data.Mongo
{
    public class IssueMessageRepository : BaseRepository<IssueMessage>, IIssueMessageRepository
    {
        public async Task<IssueMessage> AddIsssueMessage(IssueMessage issueMessage)
        {
            var x = await GenericRepository.Add(issueMessage);
            return x;
        }

        public async Task<List<IssueMessage>> GetIssueMessagesByDoorNo(string doorNo, string projectId)
        {
            return await GenericRepository.GetAll(x => x.DoorNo.Equals(doorNo) && x.ProjectId.Equals(projectId));
        }

        public async Task<List<IssueMessage>> GetAllIssueMessages(string projectId)
        {
            return await GenericRepository.GetAll(x=>x.ProjectId.Equals(projectId));
        }
        public async Task<IssueMessage> GetMessageById(string messageId)
        {
            IssueMessage message = await GenericRepository.GetById(messageId);
            return message;
        }

		public async Task<long> GetCountByProject(string projectId)
		{
			var filter = Builders<IssueMessage>.Filter.Eq(_ => _.ProjectId, projectId);
			return await GenericRepository.Collection.CountAsync(filter);
		}
	}

}
