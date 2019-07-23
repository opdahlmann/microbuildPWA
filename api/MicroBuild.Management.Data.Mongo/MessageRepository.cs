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
    public class MessageRepository : BaseRepository<Message>, IMessageRepository
    {
        public async Task<Message> AddMessage(Message message)
        {
            var x = await GenericRepository.Add(message);
            return x;
        }

        public async Task<List<Message>> GetMessagesByDoorId(string doorId, string workorderId)
        {
            return await GenericRepository.GetAll(x=>x.DoorId.Equals(doorId) && x.WorkOrderId.Equals(workorderId));
        }

        public async Task<List<Message>> GetAllMessagesByWorkorderId(string workorderId)
        {
            return await GenericRepository.GetAll(x => x.WorkOrderId.Equals(workorderId));
        }

        public async Task<Message> GetMessageById(string messageId)
        {
            Message message = await GenericRepository.GetById(messageId);
            return message;
        }

        public async Task<long> GetMessagesCountByDoorId(string doorId, string workorderId)
        {
			var filter = Builders<Message>.Filter.Eq(_ => _.DoorId, doorId) & Builders<Message>.Filter.Eq(_ => _.WorkOrderId, workorderId);
			return await GenericRepository.Collection.CountAsync(filter);
		}
    }
}
