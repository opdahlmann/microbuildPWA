﻿using Lib.Net.Http.WebPush;
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

        public async Task<MBSubscription> AddSubscription(MBSubscription subscription)
        {
            var result = await this.supscriptionRepo.AddSubscription(subscription);
            return result;
        }
        public async Task<List<MBSubscription>> getAllSubscriptions()
        {
            return await this.supscriptionRepo.GetAllSubscriptionsAsync();
        }

        public async Task <MBSubscription> getSubscriptionById(string Id)
        {
            return await this.supscriptionRepo.GetSubscriptionById(Id);
        }

        public async Task<bool> DeleteSubscription(string Id)
        {
            try
            {
                await this.supscriptionRepo.DeleteSubscription(Id);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
