using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroBuild.Management.Common.DTO
{
    public class AngularPushNotification
    {
        public class NotificationAction
        {
            public string Action { get; }
            public string Title { get; }

            public NotificationAction(string action, string title)
            {
                Action = action;
                Title = title;
            }
        }

        public string Title { get; set; }
        public string Body { get; set; }
        public string Icon { get; set; }
        public IList<int> Vibrate { get; set; } = new List<int>();
        public IDictionary<string, object> Data { get; set; }
        public IList<NotificationAction> Actions { get; set; } = new List<NotificationAction>();
    }
}
