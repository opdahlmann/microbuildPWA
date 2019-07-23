using MicroBuild.Infrastructure.Models;
using System;
using System.Collections.Generic;

namespace MicroBuild.Management.Common.DTO
{
    public class WorkorderTemplate : IEntity
    {
        public string Id { get; set; }
        public string ProjectId { get; set; }
        public string Name { get; set; }
        public bool IsAdHoc { get; set; }
        public int DoorCount { get; set; }
        public string CompanyId { get; set; }

        public int RemindBeforeDays { get; set; }
        public bool NotificationMailSent { get; set; }

        public DateTime ModifiedDate { get; set; }
        public string LastModifiedUser { get; set; }
    }
}