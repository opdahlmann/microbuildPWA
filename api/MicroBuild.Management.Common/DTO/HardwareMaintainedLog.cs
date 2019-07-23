using MicroBuild.Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroBuild.Management.Common.DTO
{
    public class HardwareMaintainedLog : IEntity
    {
        public string Id { get; set; }

        public string Header { get; set; }
        public string DoorNo { get; set; }
        public string FieldName { get; set; }

        public string Content { get; set; }
        public bool IsMaintained { get; set; }
        public string ChecklistId { get; set; }

        public LogMetadata Metadata { get; set; }
    }
}
