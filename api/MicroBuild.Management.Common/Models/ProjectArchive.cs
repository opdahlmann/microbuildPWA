using MicroBuild.Management.Common.DTO;
using System;
using System.Collections.Generic;

namespace MicroBuild.Management.Common.Models
{
    public class ProjectArchive
    {
        public ArchiveMetadata Metadata { get; set; }

        public Project Project { get; set; }
        public IEnumerable<Door> Doors { get; set; }
        public IEnumerable<Company> Companies { get; set; }
        public IEnumerable<Checklist> Checklists { get; set; }

        public IEnumerable<Sync> SyncLogs { get; set; }
        public IEnumerable<EmailLog> EmailLogs { get; set; }

		public IEnumerable<WorkorderTemplate> Templates { get; set; }
        public IEnumerable<WorkorderTemplateHardwareCollection> HardwareCollections { get; set; }
        public IEnumerable<WorkorderTemplateDoor> TemplateDoors { get; set; }

		public IEnumerable<Workorder> Workorders { get; set; }
		public IEnumerable<WorkorderDoor> WorkorderDoors { get; set; }
        public IEnumerable<DoorNote> DoorNotes { get; set; }
    }

    public class ArchiveMetadata
    {
        public DateTime CreatedAt { get; set; }
        public string CreatedByMBEUserId { get; set; }
        public string ProjectName { get; set; }
        public string ProjectId { get; set; }

        public string ArchiveVersion = "1";
        public string CreatedIn = "MicroBuild Management Admin (Web)";
    }
}
