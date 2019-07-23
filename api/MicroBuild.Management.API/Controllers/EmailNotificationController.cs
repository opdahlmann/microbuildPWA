using MicroBuild.Management.Domain.Interfaces;
using MicroBuild.WebApiUtils.Authentication;
using MicroBuild.WebApiUtils.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace MicroBuild.Management.API.Controllers
{
    public class EmailNotificationController : ApiController
    {
        private IObjectsLocator ObjectsLocator;

        public EmailNotificationController(IObjectsLocator objectsLocator)
        {
            ObjectsLocator = objectsLocator;
        }

        [HttpPost]
        [Route("email/notifications/fornotstartedworkorders")]
        public async Task<HttpResponseMessage> SendEmailNotificationForNotStartedWorkorders()
        {
            var res = await this.ObjectsLocator.WorkorderService.SendEmailNotificationForNotStartedWorkorders();
            return Request.CreateResponse(HttpStatusCode.OK, res);
        }

        [HttpPost]
        [Route("email/notifications/remindernotifications")]
        public async Task<HttpResponseMessage> SendEmailReminderNotifications()
        {
            var res = await this.ObjectsLocator.WorkorderService.SendEmailReminderNotifications();
            return Request.CreateResponse(HttpStatusCode.OK, res);
        }

        [HttpGet]
        [Route("email/ping")]
        public async Task<HttpResponseMessage> SendEmailToStaff()
        {
            var response = await Task.Run(() =>
            {
                return Request.CreateResponse(HttpStatusCode.OK, "pong");
            });
            return response;
        }
    }
}
