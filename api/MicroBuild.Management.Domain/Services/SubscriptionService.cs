using MicroBuild.Management.Common.DTO;
using MicroBuild.Management.Data.API;
using MicroBuild.Management.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MicroBuild.Management.Domain.Services
{
    public class SubscriptionService : ISubscriptionService

    {
        private IDataUnitOfWork DataUnitOfWork { get; set; }
        private IObjectsLocator ObjectLocator { get; set; }

        public SubscriptionService(IDataUnitOfWork dataUnitOfWork, IObjectsLocator objectLocator)
        {
            this.DataUnitOfWork = dataUnitOfWork;
            this.ObjectLocator = objectLocator;
        }

        public async Task<bool> AddSubscription(MBPushSubscription subscription)
        {
            var result = await this.DataUnitOfWork.SubscriptionRepository.AddSubscription(subscription);
            if (result != null)
            {
                await this.ObjectLocator.NotificationService.SendNotificationsAsync();
                return true;
            }
            return false;
        }


        public async Task <List<MBPushSubscription>> getAllSubscriptions()
        {
           return  await this.DataUnitOfWork.SubscriptionRepository.GetAllSubscriptionsAsync();
        }
    }
}
