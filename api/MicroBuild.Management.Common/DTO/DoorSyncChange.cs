using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroBuild.Management.Common.DTO
{
    public class DoorSyncChange
    {
        public string DoorNo { get; set; }
        public string ProgressDoorId { get; set; }
        public List<DoorSyncChangeField> FieldChanges { get; set; }

        public bool IsDoorCreation { get; set; }
        public bool IsDoorDeletion { get; set; }

        public DoorSyncChange()
        {
            IsDoorCreation = false;
            IsDoorDeletion = false;
            FieldChanges = new List<DoorSyncChangeField>();
        }

        public DoorSyncChange(string mbeDoorNr, string progDoorId) : this()
        {
            DoorNo = mbeDoorNr;
            ProgressDoorId = progDoorId;
        }

        public DoorSyncChange(Door door) : this()
        {
            IsDoorCreation = true;
            DoorNo = door.DoorNo;
        }
    }

    public class DoorSyncChangeField
    {
        public string FieldName { get; set; }
        public string FieldHeader { get; set; }
        public object OldValue { get; set; }
        public object NewValue { get; set; }
        public bool IsValidUpdate { get; set; }

        public DoorSyncChangeField() { }

        public DoorSyncChangeField(string fieldName, string header, object oldValue, object newValue)
        {
            FieldName = fieldName;
            FieldHeader = header;
            OldValue = oldValue;
            NewValue = newValue;
            IsValidUpdate = true;
        }

        public DoorSyncChangeField(string fieldName, string header, object oldValue, object newValue, bool isValidUpdate)
        {
            FieldName = fieldName;
            FieldHeader = header;
            OldValue = oldValue;
            NewValue = newValue;
            IsValidUpdate = isValidUpdate;
        }
    }
}
