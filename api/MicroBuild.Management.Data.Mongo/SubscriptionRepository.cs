
using Lib.Net.Http.WebPush;
using MicroBuild.Management.Common.DTO;
using MicroBuild.Management.Common.ViewObjects;
using MicroBuild.Management.Data.API;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroBuild.Management.Data.Mongo
{
    public class SubscriptionRepository : BaseRepository<MBPushSubscription>, ISubscriptionRepository
    {
        public async Task<MBPushSubscription> AddSubscription(MBPushSubscription subscription)
        {
            return await GenericRepository.Add(subscription);
        }

        public async Task<List<MBPushSubscription>> GetAllSubscriptionsAsync()
        {
            return await GenericRepository.GetAll();
        }
    }
}
