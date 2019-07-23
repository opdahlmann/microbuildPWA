using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroBuild.Management.Common.DTO
{
    public class MBECompany
    {
        public MBECompany()
        {
            Users = new List<CompanyUser>();
        }

        public string Id { get; set; }
        public string Name { get; set; }
        public string Adress1 { get; set; }
        public string Adress2 { get; set; }
        public string PostNo { get; set; }
        public string City { get; set; }
        public string Phone1 { get; set; }
        public ICollection<CompanyUser> Users { get; set; }
    }
}
