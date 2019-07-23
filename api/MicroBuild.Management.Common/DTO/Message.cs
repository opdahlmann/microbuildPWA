using System;
using System.Collections.Generic;

using MicroBuild.Infrastructure.Models;

namespace MicroBuild.Management.Common.DTO
{
    public class Message: IEntity, IMessage
    {
        public string Id { get; set; }

        #region Fields visible to the client

        public string Subject { get; set; }
        public string Body { get; set; }
        public string Picture { get; set; }

        public string WorkOrderId { get; set; }
        public string ProjectId { get; set; }
        public string DoorNo { get; set; }
        public string DoorId { get; set; } // this is work order door Id

        public MessageUser Sender { get; set; } // sender, his company, sent timestamp

        #endregion

        public bool IsHandled { get; set; }
        public DateTime HandledTimestamp { get; set; } // set when handled

        public List<MessageUser> Recipients { get; set; } // recipients, their companies, sent timestamps
    }

    public class IssueMessage : IEntity, IMessage
    {
        public string Id { get; set; }

        #region Fields visible to the client

        public string Subject { get; set; }
        public string Body { get; set; }
        public string Picture { get; set; }

        public string ProjectId { get; set; }
        public string DoorNo { get; set; }
        public string DoorId { get; set; } // this is project door Id
        public List<string> RecipientCompanyIds { get; set; }
        public DateTime sentTime { get; set; }

        public MessageUser Sender { get; set; } // sender, his company, sent timestamp

        #endregion

        public bool IsHandled { get; set; }
        public DateTime HandledTimestamp { get; set; } // set when handled

        public List<MessageUser> Recipients { get; set; } // recipients, their companies, sent timestamps
    }

    public class MessageUser
    {
        public string CompanyId { get; set; }
        public string MBEUserId { get; set; }
        public DateTime Timestamp { get; set; }
    }

    public interface IMessage : IEntity
    {
        string Id { get; set; }

        #region Fields visible to the client

        string Subject { get; set; }
        string Body { get; set; }
        string Picture { get; set; }

        string ProjectId { get; set; }
        string DoorNo { get; set; }
        string DoorId { get; set; }

        MessageUser Sender { get; set; } // sender, his company, sent timestamp

        #endregion

        bool IsHandled { get; set; }
        DateTime HandledTimestamp { get; set; } // set when handled

        List<MessageUser> Recipients { get; set; } // recipients, their companies, sent timestamps
    }
}
