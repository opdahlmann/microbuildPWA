using MicroBuild.Infrastructure.Models;
using System.Collections.Generic;

namespace MicroBuild.Management.Common.DTO
{
    public class WorkorderTemplateDoor : IEntity
    {
        public string Id { get; set; }
		public string ProjectId { get; set; }
        public string TemplateId { get; set; }

        public System.DateTime? FinishedDate { get; set; }
        public Door Door { get; set; }
	}
}