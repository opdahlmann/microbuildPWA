using MicroBuild.Management.Common.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroBuild.Management.Common.ViewObjects
{
    public class DoorNotificationsViewModel
    {
        public string DoorId { get; set; }
        public string DoorNo { get; set; }
        public long MessagesCount { get; set; }
        public long NotesCount { get; set; }
    }

}
