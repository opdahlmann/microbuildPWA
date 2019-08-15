using MicroBuild.PWA.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroBuild.PWA.Mongo
{
    public class MBSubscriptionRepo : BaseRepository<MBSubscription>
    {
        public async Task<MBSubscription> AddSubscription(MBSubscription subscription)
        {
            return await GenericRepository.Add(subscription);
        }
        public async Task<List<MBSubscription>> GetAllSubscriptionsAsync()
        {
            return await GenericRepository.GetAll();
        }

        public async Task <MBSubscription> GetSubscriptionById(string Id)
        {
            return await GenericRepository.GetById(Id);
        }
    }
}
