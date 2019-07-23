using MicroBuild.PWA.Models;
using MicroBuild.PWA.Mongo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroBuild.PWA.Domain
{
    public class SubscriptionService
    {
        private MBSubscriptionRepo supscriptionRepo;
        public SubscriptionService()
        {
            this.supscriptionRepo = new MBSubscriptionRepo();
        }

        public async Task<bool> AddSubscription(MBSubscription subscription)
        {
            var result = await this.supscriptionRepo.AddSubscription(subscription);
            if (result != null)
            {
                return true;
            }
            return false;
        }
    }
}
