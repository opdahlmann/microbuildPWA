using System;

namespace MicroBuild.Management.Common.ViewObjects
{
	public class WorkorderTemplateListViewModel
	{
		public string Id { get; set; }
		public string Name { get; set; }
		public bool IsAdHoc { get; set; }
		public int DoorCount { get; set; }
		public string CompanyId { get; set; }
		public string CompanyName { get; set; }

		public DateTime ModifiedDate { get; set; }
		public string LastModifiedUser { get; set; }
		public string LastModifiedUserName { get; set; }
		public int RemindBeforeDays { get; set; }
	}
}
