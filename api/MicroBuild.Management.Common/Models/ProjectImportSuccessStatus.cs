using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroBuild.Management.Common.DTO.Models
{
    public class ProjectImportSuccessStatus
    {
        public string MBEProjectId { get; set; }
        public bool IsSuccess { get; set; }
        public string Error { get; set; }
        public Project Project { get; set; }

        public ProjectImportSuccessStatus(string mbeProjectId)
        {
            MBEProjectId = mbeProjectId;
        }
    }
}
