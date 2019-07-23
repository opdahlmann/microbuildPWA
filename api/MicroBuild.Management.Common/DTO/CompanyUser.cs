using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroBuild.Management.Common.DTO
{
    public class CompanyUser
    {
        public string UserId { get; set; }
        public string Email { get; set; }
        public string Mobile { get; set; }
        public string MBEUserId { get; set; }
        public Role Role { get; set; }
        public string Name { get; set; }
        public bool IsAdmin { get; set; }
        public bool IsProjectLeader { get; set; }
        public bool IsCustomer { get; set; }
        public bool IsEmailRecipient { get; set; }
    }

    public enum Role
    {
        ADMIN = 1,
        READ_ONLY = 2,
        READ_WRITE = 3
    }
}
