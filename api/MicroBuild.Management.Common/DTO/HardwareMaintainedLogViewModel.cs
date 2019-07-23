using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroBuild.Management.Common.DTO
{
    public class HardwareMaintainedLogViewModel
    {
        public string Id { get; set; }

        public string Header { get; set; }
        public string DoorNo { get; set; }
        public string FieldName { get; set; }

        public string Content { get; set; }
        public bool IsMaintained { get; set; }
        public string ChecklistId { get; set; }

        public string DoorId { get; set; }
        public string ProjectId { get; set; }
        public string UserId { get; set; }
        public string UserName { get; set; }
        public string WorkorderTemplateId { get; set; }
        public string WorkorderId { get; set; }
        public DateTime Timestamp { get; set; }
    }
}
