using MicroBuild.Management.Common.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MicroBuild.Management.Data.API
{
    public interface IIssueMessageRepository
    {
        Task<IssueMessage> AddIsssueMessage(IssueMessage issueMessage);
        Task<List<IssueMessage>> GetIssueMessagesByDoorNo(string doorNo, string projectId);
        Task<List<IssueMessage>> GetAllIssueMessages(string projectId);
        Task<IssueMessage> GetMessageById(string messageId);
		Task<long> GetCountByProject(string projectId);
	}
}
