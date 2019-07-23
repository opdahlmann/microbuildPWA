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
        public string Id { get; set; }
        public PushSubscription PushSubscription { get; set; }
    }
}
