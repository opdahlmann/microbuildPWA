using MicroBuild.Infrastructure.Models;
using MicroBuild.Management.Common.Models;
using System;
using System.Collections.Generic;

namespace MicroBuild.Management.Common.DTO
{
    public class EmailLog : IEntity
    {
        public string Id { get; set; }
        public string ProjectId { get; set; }
        public string TemplateId { get; set; }

        public string ToEmail { get; set; }
        public string ToUser { get; set; }
        public DateTime SentDate { get; set; }
        public EMailTypeEnum Type { get; set; }
    }
}