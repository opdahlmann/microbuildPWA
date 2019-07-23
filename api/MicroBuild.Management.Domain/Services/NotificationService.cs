using Lib.Net.Http.WebPush;
using Lib.Net.Http.WebPush.Authentication;
using MicroBuild.Management.Common.DTO;
using MicroBuild.Management.Data.API;
using MicroBuild.Management.Domain.Interfaces;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MicroBuild.Management.Domain.Services
{
    public class NotificationService : INotificationService
    {
        private const string WRAPPER_START = "{\"notification\":";
        private const string WRAPPER_END = "}";
        private static readonly JsonSerializerSettings _jsonSerializerSettings = new JsonSerializerSettings
        {
            ContractResolver = new CamelCasePropertyNamesContractResolver()
        };


        private const int NOTIFICATION_FREQUENCY = 60000;
        private ISubscriptionService _subscriptionsService;
        private PushServiceClient _pushClient;

        private IDataUnitOfWork DataUnitOfWork { get; set; }
        private IObjectsLocator ObjectLocator { get; set; }

        public NotificationService(IDataUnitOfWork dataUnitOfWork, IObjectsLocator objectLocator)
        {
            this.DataUnitOfWork = dataUnitOfWork;
            this.ObjectLocator = objectLocator;
        }

        public void SetNotificationProperties( ISubscriptionService SubscriptionsService, PushServiceClient pushClient)
        {
             PushNotificationOptions options = new PushNotificationOptions()
        {
            PublicKey = System.Configuration.ConfigurationManager.AppSettings["Publickey"],
            PrivateKey = System.Configuration.ConfigurationManager.AppSettings["PrivateKey"]
        };

            _subscriptionsService = SubscriptionsService;
            _pushClient = pushClient;
           
                _pushClient.DefaultAuthentication = new VapidAuthentication(options.PublicKey, options.PrivateKey)
                {
                    Subject = "https://angular-aspnetmvc-pushnotifications.demo.io"
                };
        }

        protected  async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                await Task.Delay(NOTIFICATION_FREQUENCY, stoppingToken);
               // SendNotifications(stoppingToken);
            }
        }


        public async Task SendNotificationsAsync()
        {
            SetNotificationProperties(new SubscriptionService(DataUnitOfWork, ObjectLocator), new PushServiceClient());
            AngularPushNotification _notification = new AngularPushNotification
            {
                Title = "Test Notification",
                Body = $"This is test notification",
                Icon = ""
            };

            string topic = null;
            int? timeToLive = null;
            PushMessageUrgency urgency = PushMessageUrgency.Normal;

            PushMessage notification = new PushMessage(WRAPPER_START + JsonConvert.SerializeObject(_notification, _jsonSerializerSettings) + WRAPPER_END)
            {
                Topic = topic,
                TimeToLive = timeToLive,
                Urgency = urgency
            };

            var allSupscriptions = await _subscriptionsService.getAllSubscriptions();
            foreach (MBPushSubscription subscription in allSupscriptions)
            {
                // fire-and-forget
              await _pushClient.RequestPushMessageDeliveryAsync(subscription.PushSubscription, notification);
            }
        }

    }
}
