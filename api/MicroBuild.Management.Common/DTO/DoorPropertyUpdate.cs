using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroBuild.Management.Common.DTO
{
    public class DoorPropertyUpdate<T>
    {
        public string DoorId;
        public T PropertyValue;
    }
}
