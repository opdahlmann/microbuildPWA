using Lib.Net.Http.WebPush;
using MicroBuild.PWA.Domain;
using MicroBuild.PWA.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace MicroBuild.PWA.API.Controllers
{
    public class NotificationController : ApiController
    {
        [HttpPost]
        [Route("notifications")]
        public async Task sendNotification()
        {
            var notificationService = new NotificationService(new PushServiceClient());
            await notificationService.SendNotificationsAsync();
      
        }

        [HttpPost]
        [Route("notifications/ByUserId")]
        public async Task sendNotificationForUserGroup()
        {
            var notificationService = new NotificationService(new PushServiceClient());
            await notificationService.SendNotificationsForUserGroup();

        }
    }
}
