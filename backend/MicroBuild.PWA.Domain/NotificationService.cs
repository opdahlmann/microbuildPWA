using MicroBuild.PWA.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebPush;

namespace MicroBuild.PWA.Domain
{
    public class NotificationService
    {

        private readonly PushNotificationOptions options;
        private readonly WebPushClient _pushClient;
        private SubscriptionService _subscriptionsService;
        public NotificationService()
        {
            options = new PushNotificationOptions()
            {
                PublicKey = ConfigurationManager.AppSettings["Publickey"],
                PrivateKey = ConfigurationManager.AppSettings["PrivateKey"],
                Subject = "https://angular-aspnetmvc-pushnotifications.demo.io"
            };

            _subscriptionsService = new SubscriptionService();
            _pushClient = new WebPushClient();

            _pushClient.SetVapidDetails(options.Subject, options.PublicKey, options.PrivateKey);

        }

        public async Task SendNotificationAsync()
        {
            AngularPushNotification _notification = new AngularPushNotification
            {
                Title = "Test Notification",
                Body = $"This is test notification",
                Icon = ""
            };

            
            var allSupscriptions = await _subscriptionsService.getAllSubscriptions();

            foreach (MBSubscription subscription in allSupscriptions)
            {
                try
                {
                    var webPushSubscription = new PushSubscription(
                   subscription.Endpoint,
                   subscription.Keys["p256dh"],
                   subscription.Keys["auth"]);

                    _pushClient.SendNotification(webPushSubscription, JsonConvert.SerializeObject(_notification));
                }catch (WebPushException e)
                {

                }
               
            }
        }


























        //private SubscriptionService _subscriptionsService;
        //private PushServiceClient _pushClient;
        //private const string WRAPPER_START = "{\"notification\":";
        //private const string WRAPPER_END = "}";
        //private static readonly JsonSerializerSettings _jsonSerializerSettings = new JsonSerializerSettings
        //{
        //    ContractResolver = new CamelCasePropertyNamesContractResolver()
        //};
        //PushNotificationOptions options;

        //public NotificationService(PushServiceClient pushClient)
        //{
        //     options = new PushNotificationOptions()
        //    {
        //        PublicKey =ConfigurationManager.AppSettings["Publickey"],
        //        PrivateKey = ConfigurationManager.AppSettings["PrivateKey"]
        //    };

        //    _subscriptionsService = new SubscriptionService();
        //    _pushClient = pushClient;

        //    _pushClient.DefaultAuthentication = new VapidAuthentication(options.PublicKey, options.PrivateKey)
        //    {
        //        Subject = "https://angular-aspnetmvc-pushnotifications.demo.io"
        //    };
        //}

        //public async Task SendNotificationsAsync()
        //{

        //    AngularPushNotification _notification = new AngularPushNotification
        //    {
        //        Title = "Test Notification",
        //        Body = $"This is test notification",
        //        Icon = ""
        //    };

        //    string topic = null;
        //    int? timeToLive = null;
        //    PushMessageUrgency urgency = PushMessageUrgency.Normal;

        //    PushMessage notification = new PushMessage(WRAPPER_START + JsonConvert.SerializeObject(_notification, _jsonSerializerSettings) + WRAPPER_END)
        //    {
        //        Topic = topic,
        //        TimeToLive = timeToLive,
        //        Urgency = urgency
        //    };

        //        var allSupscriptions = await _subscriptionsService.getAllSubscriptions();

        //        foreach (MBSubscription subscription in allSupscriptions)
        //        {
        //        try
        //        {
        //            var push_Client = new PushServiceClient();
        //            push_Client.DefaultAuthentication = new VapidAuthentication(options.PublicKey, options.PrivateKey)
        //            {
        //                Subject = "https://angular-aspnetmvc-pushnotifications.demo.io"
        //            };
        //            // fire-and-forget
        //            push_Client.RequestPushMessageDeliveryAsync(subscription.PushSubscription, notification);
        //        }
        //        catch (Exception e)
        //        {

        //        }
        //    }
        // }



    }
}
