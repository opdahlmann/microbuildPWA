using MicroBuild.Infrastructure.Models;
using System;
using System.Collections.Generic;

namespace MicroBuild.Management.Common.DTO
{
    public class Workorder : IEntity
    {
        public string Id { get; set; }
        public string ProjectId { get; set; }
        public string TemplateId { get; set; }

        public bool IsPreviewOnly { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? FinishedDate { get; set; }

        public bool DueNotificationMailSent { get; set; }
        public bool ReminderNotificationMailSent { get; set; }

        public DateTime ModifiedDate { get; set; }
        public string LastModifiedUser { get; set; }
    }
}