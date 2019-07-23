using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MicroBuild.Management.API.Models
{
    public class DoorMessageDisplayViewModel
    {
        public string Id { get; set; }
        public string DoorNo { get; set; }

        public string Subject { get; set; }
        public string Body { get; set; }

        public bool HasPicture { get; set; }
        public bool IsHandled { get; set; }

        public string SenderName { get; set; }
        public string SenderCompanyName { get; set; }
        public DateTime SentTime { get; set; }

        public bool IsIssueMessage { get; set; }
    }
}