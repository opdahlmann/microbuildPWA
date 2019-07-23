﻿using Lib.Net.Http.WebPush;
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
    public class SubscriptionController : ApiController
    {
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
    }
}
