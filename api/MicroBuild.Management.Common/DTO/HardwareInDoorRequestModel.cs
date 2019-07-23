using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroBuild.Management.Common.DTO
{
    public class HardwareInDoorRequestModel
    {
        public string FieldName { get; set; }
        public string Content { get; set; }
        public string Header { get; set; }
        
        //public string Surf { get; set; }
        public DateTime MaintainedDate { get; set; }
        public bool IsMaintained { get; set; }
    }
}
