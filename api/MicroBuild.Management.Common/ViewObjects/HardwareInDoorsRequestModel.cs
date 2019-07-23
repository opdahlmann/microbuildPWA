using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroBuild.Management.Common.ViewObjects
{
    public class HardwareInDoorsRequestModel
    {
        public string FieldName { get; set; }
        public List<string> DoorIds { get; set; }
    }
}
