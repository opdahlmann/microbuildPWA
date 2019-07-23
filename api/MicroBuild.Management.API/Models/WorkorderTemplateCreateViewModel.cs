using System.Collections.Generic;
using MicroBuild.Management.Common.DTO;

namespace MicroBuild.Management.API.Models
{
    public class WorkorderTemplateCreateViewModel
    {
        public WorkorderTemplate Template { get; set; }
        public List<WorkorderTemplateDoor> TemplateDoors { get; set; }
        public List<WorkorderTemplateHardwareCollection> HardwareCollections { get; set; }
        public List<Workorder> Workorders { get; set; }
    }
}