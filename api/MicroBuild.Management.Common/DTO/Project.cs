using MicroBuild.Infrastructure.Models;
using System;
using System.Collections.Generic;

namespace MicroBuild.Management.Common.DTO
{
	public class Project : IEntity
	{
		public Project()
		{
			CreatedDate = DateTime.Now;
		}

		public string Id { get; set; }
		public string MBEProjectId { get; set; }
		public string Name { get; set; }
		public string Description { get; set; }
		public string ProjectNo { get; set; }
		public DateTime? CreatedDate { get; set; }

		public List<UserInProject> UsersInProject { get; set; }
	}

	public class UserInProject : IEntity
	{
		public string Id { get; set; }
		public string ProjectId { get; set; }

		public string RoleId { get; set; }
		public bool Active { get; set; }
		public string UserId { get; set; }
	}
}
