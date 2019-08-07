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
        //this is used with lib.net.http.webpush library.
        [HttpPost]
        [Route("subscriptions")]

        public async Task<HttpResponseMessage> AddSubscription(PushSubscription subscription)
        {
            var sub = new MBSubscription
            {
                PushSubscription = subscription
            };
            var subscriptionService = new SubscriptionService();
            var result = await subscriptionService.AddSubscription(sub);
            return Request.CreateResponse(HttpStatusCode.OK, result);
        }

        // //this is used with webpush library.
        //[HttpPost]
        //[Route("subscriptions")]

        //public async Task<HttpResponseMessage> AddSubscription([FromBody]MBSubscription subscription)
        //{

        //    var subscriptionService = new SubscriptionService();
        //    var result = await subscriptionService.AddSubscription(subscription);
        //    return Request.CreateResponse(HttpStatusCode.OK, result);
        //}
    }
}
