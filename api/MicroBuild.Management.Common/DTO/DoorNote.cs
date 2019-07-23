using System;

using MicroBuild.Infrastructure.Models;

namespace MicroBuild.Management.Common.DTO
{
    public class DoorNote : IEntity
    {
        public string Id { get; set; }

		public DateTime Timestamp { get; set; }
		public string MbeUserId { get; set; }

        public string ProjectId { get; set; }
        public string TemplateId { get; set; }
        public string WorkorderId { get; set; }
        public string DoorId { get; set; }

		public NoteType NoteType { get; set; }
		public string Photo { get; set; }
		public string Text { get; set; }
	}

	public enum NoteType {
		TEXT = 0,
		PHOTO = 1,
	}
}
