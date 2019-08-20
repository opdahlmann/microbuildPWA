using Lib.Net.Http.WebPush;
using Lib.Net.Http.WebPush.Authentication;
using MicroBuild.PWA.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;


namespace MicroBuild.PWA.Domain
{
    public class NotificationService
    {
        private SubscriptionService _subscriptionsService;
        private PushServiceClient _pushClient;
        private const string WRAPPER_START = "{\"notification\":";
        private const string WRAPPER_END = "}";
        private static readonly JsonSerializerSettings _jsonSerializerSettings = new JsonSerializerSettings
        {
            ContractResolver = new CamelCasePropertyNamesContractResolver()
        };
        PushNotificationOptions options;

        public NotificationService(PushServiceClient pushClient)
        {
            options = new PushNotificationOptions()
            {
                PublicKey = ConfigurationManager.AppSettings["Publickey"],
                PrivateKey = ConfigurationManager.AppSettings["PrivateKey"]
            };

            _subscriptionsService = new SubscriptionService();
            _pushClient = pushClient;

            _pushClient.DefaultAuthentication = new VapidAuthentication(options.PublicKey, options.PrivateKey)
            {
                Subject = "https://angular-aspnetmvc-pushnotifications.demo.io"
            };
        }

        public async Task SendNotificationsAsync(Notification notificationObject)
        {
            var allSupscriptions = await _subscriptionsService.getAllSubscriptions();

            foreach (MBSubscription subscription in allSupscriptions)
            {
                try
                {
                    AngularPushNotification _notification = new AngularPushNotification
                    {
                        Title = notificationObject.Title,
                        Body = $"{notificationObject.Body}",
                        Icon = (notificationObject.Image != null) ? notificationObject.Image : ""
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

                    // fire-and-forget
                    var result = _pushClient.RequestPushMessageDeliveryAsync(subscription.PushSubscription, notification, CancellationToken.None);

                }
                catch (Exception e)
                {

                }
            }
        }

        public async Task SendNotificationsForUserGroup(Notification notificationObject)
        {
            var allSupscriptions = await _subscriptionsService.getAllSubscriptions();
            int val = 70;
            var filteredUserSubscriptions = allSupscriptions.Where(x => Int32.Parse(x.UserId) > val).Select(x => x);

            foreach (MBSubscription subscription in filteredUserSubscriptions)
            {
                try
                {
                    AngularPushNotification _notification = new AngularPushNotification
                    {
                        Title = notificationObject.Title,
                        Body = $"{notificationObject.Body}",
                        Icon = (notificationObject.Image != null) ? notificationObject.Image : ""
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

                    // fire-and-forget
                    var result = _pushClient.RequestPushMessageDeliveryAsync(subscription.PushSubscription, notification, CancellationToken.None);
                }
                catch (Exception e)
                {

                }
            }
        }
    }
}
