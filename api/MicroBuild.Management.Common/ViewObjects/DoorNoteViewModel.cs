using MicroBuild.Management.Common.DTO;

namespace MicroBuild.Management.Common.ViewObjects
{
	public class DoorNoteViewModel
	{
		public string DoorNo { get; set; }

		public DoorNote DoorNote { get; set; }

		public DoorNoteViewModel(string doorNo, DoorNote doorNote)
		{
			DoorNo = doorNo;
			DoorNote = doorNote;
		}
	}
}
