using Lib.Net.Http.WebPush;
using MicroBuild.Management.Common.DTO;
using MicroBuild.Management.Domain.Interfaces;
using MicroBuild.WebApiUtils.Authentication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace MicroBuild.Management.API.Controllers
{
    public class PushSubscriptionController : ApiController
    {
        private readonly IObjectsLocator ObjectLocator;

       public PushSubscriptionController(IObjectsLocator objectLocator)
        {
            ObjectLocator = objectLocator;
        }

        [HttpPost]
        [Route("subscriptions")]

        public async Task <HttpResponseMessage> AddSubscription (PushSubscription subscription)
        {
            var sub = new MBPushSubscription
            {
                PushSubscription = subscription
            };

            var result = await ObjectLocator.SubscriptionService.AddSubscription(sub);
            return Request.CreateResponse(HttpStatusCode.OK, result);
        }

        [HttpGet]
        [Route("pingxyz")]
        public async Task<HttpResponseMessage> GetAllProjectCompanies()
        {
         
            return Request.CreateResponse(HttpStatusCode.OK, "ABC");
        }
    }
}
