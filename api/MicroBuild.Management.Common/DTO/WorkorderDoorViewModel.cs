using MicroBuild.Infrastructure.Models;
using System.Collections.Generic;

namespace MicroBuild.Management.Common.DTO
{
    public class WorkorderDoorViewModel
    {
		public string Id { get; set; }
        public string ProjectId { get; set; }
		public string WorkorderId { get; set; }
        public long MessagesCount { get; set; }
        public long NotesCount { get; set; }
		public System.DateTime? FinishedDate { get; set; }
        public Door Door { get; set; }
    }
}