//using Lib.Net.Http.WebPush;
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
//using WebPush;

namespace MicroBuild.PWA.API.Controllers
{
    public class SubscriptionController : ApiController
    {
        [HttpPost]
        [Route("subscriptions")]
        public async Task<HttpResponseMessage> AddSubscription(PushSubscription subscription)
        {
            Random random = new Random();
            int Id = random.Next(1000);
            var sub = new MBSubscription
            {
                UserId = Id.ToString(),
                PushSubscription = subscription
            };
            var subscriptionService = new SubscriptionService();
            var result = await subscriptionService.AddSubscription(sub);
            return Request.CreateResponse(HttpStatusCode.OK, result);
        }

        [HttpDelete]
        [Route("subscriptions/delete/{Id}")]
        public async Task<HttpResponseMessage> DeleteSubscription(string Id)
        {
            var subscriptionService = new SubscriptionService();
            bool isDeleted = await subscriptionService.DeleteSubscription(Id);

            return Request.CreateResponse(HttpStatusCode.OK, isDeleted);
        }
    }
}
