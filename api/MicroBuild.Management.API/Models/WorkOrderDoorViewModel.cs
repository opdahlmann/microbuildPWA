using MicroBuild.Management.Common.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MicroBuild.Management.API.Models
{
    public class WorkOrderDoorViewModel
    {
        public string Id { get; set; }
        public string WorkOrderId { get; set; }
        public string ProjectId { get; set; }
        public string DoorNo { get; set; }
        public int DoorQty { get; set; }
        public RoomType RoomType { get; set; }
        public string Floor { get; set; }
        public List<DoorDetails> DoorDetails { get; set; }

    }
}