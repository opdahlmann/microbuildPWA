using Lib.Net.Http.WebPush;
using MicroBuild.Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroBuild.PWA.Models
{
    public class MBSubscription : IEntity
    {
        //this is used with lib.net.http.webpush library.
        public string Id { get; set; }
        public PushSubscription PushSubscription { get; set; }

        //this is used with webpush library.
        //public string Id { get; set; }

        //public string Endpoint { get; set; }

        //public IDictionary<string, string> Keys { get; set; }
    }
}
