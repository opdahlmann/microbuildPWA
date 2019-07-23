using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using MicroBuild.Management.Common.DTO;
using MicroBuild.Management.Common.ViewObjects;

namespace MicroBuild.Management.Domain.Interfaces
{
    public interface IDoorNotesService
	{
        Task<DoorNote> AddDoorNote(
			string projectId,
			string templateId,
			string workorderId,
			string doorId,
			DoorNote note,
			string userId
		);

		Task<IEnumerable<DoorNote>> GetAllDoorNotesByWorkorderDoorId(string doorId);

		Task<IEnumerable<DoorNoteViewModel>> GetAllDoorNotesByWorkorderId(string workorderId);
		
		Task<long> GetNotesCountByDoorIdAsync(string id);

        Task<Boolean> DeleteDoorNote(string noteId);
        Task<bool> DeleteNoteByProjectId(string projectId);
        Task<IEnumerable<DoorNote>> GetAllNotesByProjectId(string ProjectId);
        Task<Dictionary<string, string>> CreateBulkAsync(IEnumerable<DoorNote> items);
        Task DeleteBulk(string[] ids);

    }
}
