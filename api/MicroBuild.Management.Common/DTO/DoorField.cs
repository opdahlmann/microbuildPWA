using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroBuild.Management.Common.DTO
{
    public class DoorField
    {
        public string Content { get; set; }
        public string CompanyId { get; set; }
        public bool Mounted { get; set; }

        public override string ToString()
        {
            return Content;
        }

        public override bool Equals(object h)
        {
            var hh = h as DoorField;
            return AreEqual(Content, hh.Content);
        }

        private bool AreEqual(object x, object y)
        {
            return !(x != y && (x == null || !x.Equals(y)));
        }
    }
}
