using MicroBuild.Infrastructure.Models;
using MicroBuild.Management.Common.DTO;
using System;
using System.Collections.Generic;

namespace MicroBuild.Management.Common.MBEModels
{
    public class MbeProject : IEntity
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string ProjectNo { get; set; }
        public string Description { get; set; }

        public ICollection<DoorTemplateStructure> ProjectDoorStructure { get; set; }
        public List<UserInProject> UsersInProject { get; set; }
    }

    public class DoorTemplateStructure : IEntity
    {
        public string Id { get; set; }
        public string FieldName { get; set; }
        public string Header { get; set; }
        public DoorFieldType FieldType { get; set; }
    }


    public enum DoorFieldType
    {
        X = 0,
        GENERAL = 1,
        HARDWAREPACKAGE = 2,
        SURF = 3,
        QTY = 4,
        DOORPACKAGE = 5
    }
}
