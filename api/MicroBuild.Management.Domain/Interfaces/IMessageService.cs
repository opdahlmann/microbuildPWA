using MicroBuild.Management.Common.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroBuild.Management.Domain.Interfaces
{
    public interface IMessageService
    {
        Task<Message> AddServiceMessage(Message messages, string userId);
        Task<IssueMessage> AddIssueMessage(IssueMessage issueMessage, string userId);
        Task<List<Message>> GetMessages(string workorderId, string doorId);
        Task<long> GetServiceMessagesCount(string workorderId, string doorId);
        Task<long> GetIssueMessagesCount(string doorNo, string projectId);
        Task<List<Message>> GetAllMessages(string workorderId);
        Task<List<Message>> GetMessagesByDoorId(string workorderId, string doorId);
        Task<byte[]> GetServiceMessagePicture(string messageId);
        Task<byte[]> GetIssueMessagePicture(string messageId);
        Task<List<IssueMessage>> GetIssueMessagesByDoorNo(string doorNo, string projectId);
        Task<List<IssueMessage>> GetAllIssueMessages(string projectId);
    }
}
