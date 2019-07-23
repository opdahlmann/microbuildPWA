using MicroBuild.Infrastructure.Models;
using MicroBuild.Management.Common.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroBuild.Management.Common.ViewObjects
{
    public class SyncViewModel : IEntity
    {
        public string Id { get; set; }

        public string ProjectId { get; set; }
        public bool IsInvalid { get; set; }
        public string Error { get; set; }
        public List<DoorSyncChange> DoorChanges { get; set; }

        public DateTime Timestamp { get; set; }
        public User User { get; set; }
        public string UserId { get; set; }

        public SyncViewModel(string projectId, string userId)
        {
            // Context
            ProjectId = projectId;
            UserId = userId;
            Timestamp = DateTime.UtcNow;
            User = User;
            // Default values
            IsInvalid = false;
            Error = null;
            DoorChanges = new List<DoorSyncChange>();
        }
    }
}
