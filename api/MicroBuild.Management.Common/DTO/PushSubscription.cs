﻿using Lib.Net.Http.WebPush;
using MicroBuild.Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroBuild.Management.Common.DTO
{
    public class MBPushSubscription: IEntity
    {
        public  string Id { get; set; }
        public PushSubscription PushSubscription { get; set; }
    }
}
