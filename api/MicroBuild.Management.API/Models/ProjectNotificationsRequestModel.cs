namespace MicroBuild.Management.API.Models
{
	public class ProjectNotificationsRequestModel
	{
		public string UserId { get; set; }
		public string[] ProjectIds { get; set; }
	}
}