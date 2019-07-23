using System.Threading.Tasks;
using MicroBuild.Management.Common.DTO;

namespace MicroBuild.Management.Domain.Interfaces
{
    public interface IMBEDoorService
    {
		Task<DoorImportSuccessStatus> ImportDoors(string mbeProjectId, string projectId);
		Task<Sync> GetMbeSyncPreview(string projectId, string userId);
		Task<long> SyncFromMbe(string projectId, string userId);
	}
}
