using MicroBuild.Management.Common.DTO;
using System;

namespace MicroBuild.Management.Common.ViewObjects
{
	public class ProjectsViewModelForMobile
	{
		public ProjectsViewModelForMobile(Project p)
		{
			Id = p.Id;
			MBEProjectId = p.MBEProjectId;
			Name = p.Name;
			Description = p.Description;
			ProjectNo = p.ProjectNo;
			CreatedDate = p.CreatedDate;
		}

		public string Id { get; set; }
		public string MBEProjectId { get; set; }
		public string Name { get; set; }
		public string Description { get; set; }
		public string ProjectNo { get; set; }
		public DateTime? CreatedDate { get; set; }
	}
}
