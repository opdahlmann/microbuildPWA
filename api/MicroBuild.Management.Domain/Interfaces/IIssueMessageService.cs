using MicroBuild.Management.Common.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroBuild.Management.Domain.Interfaces
{
    public interface IIssueMessageService
    {
        //Task<Message> AddServiceMessage(Message messages, string userId);
        //Task<List<Message>> GetMessages(string workorderId, string doorId);
        //Task<long> GetServiceMessagesCount(string workorderId, string doorId);
        //Task<long> GetIssueMessagesCount(string doorNo, string projectId);
        //Task<List<Message>> GetAllMessages(string workorderId);
        //Task<List<Message>> GetMessagesByDoorId(string workorderId, string doorId);
        //Task<byte[]> GetServiceMessagePicture(string messageId);

        //Task<List<IssueMessage>> GetIssueMessagesByDoorNo(string doorNo, string projectId);


        Task<IssueMessage> AddIssueMessage(IssueMessage issueMessage, string userId);
        Task<List<IssueMessage>> GetAllIssueMessages(string projectId);
        Task<byte[]> GetIssueMessagePicture(string messageId);
    }
}
