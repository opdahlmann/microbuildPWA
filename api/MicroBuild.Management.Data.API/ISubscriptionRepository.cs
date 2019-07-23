using MicroBuild.Management.Common.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroBuild.Management.Data.API
{
    public interface ISubscriptionRepository
    {
        Task<MBPushSubscription> AddSubscription(MBPushSubscription subscription);
        Task<List<MBPushSubscription>> GetAllSubscriptionsAsync();
    }
}
