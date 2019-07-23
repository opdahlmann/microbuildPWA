using MicroBuild.Infrastructure.Models;

namespace MicroBuild.Management.Common.DTO
{
    public class WorkorderDoor : IEntity
    {
        public WorkorderDoor()
        {
        }

        public WorkorderDoor(string workorderId, WorkorderTemplateDoor td)
		{
			ProjectId = td.ProjectId;
			WorkorderId = workorderId;

			Door = td.Door;
		}

		public string Id { get; set; }
        public string ProjectId { get; set; }
		public string WorkorderId { get; set; }

        public System.DateTime? FinishedDate { get; set; }
        public Door Door { get; set; }
    }
}