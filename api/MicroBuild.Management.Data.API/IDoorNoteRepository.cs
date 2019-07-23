using System.Collections.Generic;
using System.Threading.Tasks;

using MicroBuild.Management.Common.DTO;

namespace MicroBuild.Management.Data.API
{
    public interface IDoorNoteRepository
	{
        Task<DoorNote> CreateDoorNote(DoorNote note);

		Task<IEnumerable<DoorNote>> GetAllDoorNotesByWorkorderDoorId(string doorId);

		Task<IEnumerable<DoorNote>> GetAllDoorNotesByWorkorderId(string workorderId);

		Task<long> GetNotesCountByDoorIdAsync(string id);

        Task DeleteDoorNote(string noteId);
        
        Task DeleteDoorNoteByProjectId(string projectId);

        Task<List<DoorNote>> GetDoorNotesByProjectId(string projectId);

        Task<Dictionary<string, string>> AddWithIdMapAsync(IEnumerable<DoorNote> items);

        Task DeleteBulk(string[] ids);

    }
}
