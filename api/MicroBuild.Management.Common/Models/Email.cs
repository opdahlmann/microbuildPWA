using System.Collections.Generic;
using System.Net.Mime;

namespace MicroBuild.Management.Common.Models
{
    public class Email
    {
        public string From { get; set; }
        public string[] To { get; set; }
        public string[] Params { get; set; }
        public string Subject { get; set; }
        public bool SendAsHtml { get; set; }
        public List<string> Cc { get; set; }
        public List<string[]> Attachments { get; set; }
        public ContentType ContentType { get; set; }
		public List<string[]> Images { get; set; }
	}

    public class EmailRequest
    {
        public Email Email { get; set; }
        public EMailTypeEnum Type { get; set; }
    }

    public enum EMailTypeEnum
    {
        ACTIVATION_DEACTIVATION = 1,
        SERVICEORDER_NOT_STARTED = 2,
        SERVICEORDER_TEMPLATE_REMINDER_NOTIFICATION = 3,
        SERVICE_MESSAGE_MAIL = 4,
		SERVICE_MESSAGE_MAIL_WITH_PHOTO = 5,
        ISSUE_MESSAGE_MAIL= 6,
        ISSUE_MESSAGE_MAIL_WITH_PHOTO=7,

    }
}
