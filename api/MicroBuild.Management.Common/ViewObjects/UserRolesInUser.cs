using MicroBuild.Management.Common.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroBuild.Management.Common.ViewObjects
{
    public class UserRolesInUser
    {
        public bool IsAdmin { get; set; }
        public bool IsProjectLeader { get; set; }
        public bool IsCustomer { get; set; }
        //public bool IsServiceWorker { get; set; }
    }
}
