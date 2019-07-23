using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroBuild.Management.Common.DTO
{
    public class LogMetadata
    {
        public string DoorId { get; set; }
        public string ProjectId { get; set; }
        public string UserId { get; set; }
        public string WorkorderTemplateId { get; set; }
        public string WorkorderId { get; set; }
        public DateTime Timestamp { get; set; }
    }
}
