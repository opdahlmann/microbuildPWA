using MicroBuild.Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroBuild.Management.Common.DTO
{
    public class StatusMessages : IEntity
    {
        public string Id { get; set; }
        public string ShortName { get; set; }
        public string Description { get; set; }
    }
}