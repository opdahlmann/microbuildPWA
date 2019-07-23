namespace MicroBuild.Management.API.Models
{
	public class ByDoorNoRequestModels
	{
		public string DoorNo { get; set; }
		public string MbeProjectId { get; set; }
	}

	public class WithMbeProjectIdRequestModels
	{
		public string MbeProjectId { get; set; }
	}
}