using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MicroBuild.Management.Common.DTO;
using System.Threading.Tasks;

namespace MicroBuild.Management.Data.API
{
    public interface IMessageRepository
    {
        Task<Message> AddMessage(Message message);
        Task<List<Message>> GetMessagesByDoorId(string doorId, string workorderId);
        Task<long> GetMessagesCountByDoorId(string doorId, string workorderId);
        Task<List<Message>> GetAllMessagesByWorkorderId(string workorderId);
        Task<Message> GetMessageById(string messageId);
    }
}
