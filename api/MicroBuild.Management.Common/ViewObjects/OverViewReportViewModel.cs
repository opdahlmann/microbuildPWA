using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroBuild.Management.Common.ViewObjects
{
    public class OverViewReportViewModel
    {
        public string InstanceId { get; set; }
        public int AllDoorCount { get; set; }
        public int CompletedDoorCount { get; set; }
        public int LeftDoorCount { get; set; }
        public int IssueMessagesCount { get; set; }
        public string CompletedPercentage { get; set; }
    }
}
