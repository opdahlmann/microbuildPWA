using MicroBuild.Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroBuild.Management.Common.DTO
{
    public class Company : IEntity
    {
        public Company()
        {
            Users = new List<CompanyUser>();
            CurrentSprintIds = new List<string>();
        }

        public string ProjectId { get; set; }
        public string Id { get; set; }
        public string MBECompanyId { get; set; }
        public string Name { get; set; }
        public string Adress1 { get; set; }
        public string Adress2 { get; set; }
        public string PostNo { get; set; }
        public string City { get; set; }
        public string Phone1 { get; set; }

        public ICollection<CompanyUser> Users { get; set; }
        public bool IsActive { get; set; }

        public ICollection<string> CurrentSprintIds { get; set; }
    }
}
