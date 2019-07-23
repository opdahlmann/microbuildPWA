using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroBuild.Management.Common.DTO
{
    public class DoorImportSuccessStatus
    {
        public string MBEProjectId { get; set; }
        public string ProjectId { get; set; }
        public bool IsSuccess { get; set; }
        public string Error { get; set; }

        public DoorImportSuccessStatus(string mbeProjectId)
        {
            IsSuccess = false;
            MBEProjectId = mbeProjectId;
        }
    }
}
