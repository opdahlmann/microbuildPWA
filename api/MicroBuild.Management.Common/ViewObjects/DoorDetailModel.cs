using MicroBuild.Management.Common.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroBuild.Management.Common.ViewObjects
{
    public class DoorDetailModel
    {
        public string ProjectId { get; set; }
        public string WorkOrderId { get; set;}
        public int? DoorQty { get; set; }
        public string DoorNo { get; set; }
        public string RoomType { get; set; }
        public string Floor { get; set; }
        public string Id { get; set; }
        public List<DoorDetail> DoorDetails { get; set; }
    }

    public class DoorDetail
    {
        public string FieldName { get; set; }
        public string Header { get; set; }
        public string Content { get; set; }
        public bool IsMaintainable { get; set; }
        public bool IsMaintained { get; set; }
        public string ChecklistId { get; set; }
        public int? Qty { get; set; }
    }
}
