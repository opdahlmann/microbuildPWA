using MicroBuild.Infrastructure.Models;
using System.Collections.Generic;

namespace MicroBuild.Management.Common.DTO
{
    public class WorkorderTemplateHardwareCollection : IEntity
    {
        public string Id { get; set; }
        public string ProjectId { get; set; }
        public string TemplateId { get; set; }

        public string FieldName { get; set; }
        public string Content { get; set; }
        public string ChecklistId { get; set; }
    }
}