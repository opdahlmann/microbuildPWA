using MicroBuild.Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroBuild.Management.Common.DTO
{
    public class Checklist : IEntity
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string[] Items { get; set; }
        public string ProjectId { get; set; }
    }
}
