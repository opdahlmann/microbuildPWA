using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using MicroBuild.Management.Common.DTO;
using MicroBuild.Management.Common.ViewObjects;
using MicroBuild.Management.Data.API;
using MicroBuild.Management.Domain.Interfaces;

namespace MicroBuild.Management.Domain.Services
{
    public class DoorNotesService : IDoorNotesService
    {
        public readonly IDataUnitOfWork DataUnitOfWork;
		public readonly IObjectsLocator ObjectsLocator;

		public DoorNotesService(IDataUnitOfWork dataUnitOfWork, IObjectsLocator objectsLocator)
        {
            DataUnitOfWork = dataUnitOfWork;
			ObjectsLocator = objectsLocator;
        }

		public async Task<DoorNote> AddDoorNote(string projectId, string templateId, string workorderId, string doorId, DoorNote note, string userId)
		{
			if (
				projectId != "null" && templateId != "null" && templateId != "null" && workorderId != "null" && doorId != "null" 
				&& projectId != "undefined" && templateId != "undefined" && templateId != "undefined" && workorderId != "undefined" && doorId != "undefined"
				&& note != null && userId != null
			)
			{
				note.Timestamp = DateTime.UtcNow;
				note.MbeUserId = userId;

				note.ProjectId = projectId;
				note.TemplateId = templateId;
				note.WorkorderId = workorderId;
				note.DoorId = doorId;

				if (note.Photo != null)
				{
					note.NoteType = NoteType.PHOTO;
				}
				else if (note.Text != null)
				{
					note.NoteType = NoteType.TEXT;
				}

				return await DataUnitOfWork.DoorNoteRepository.CreateDoorNote(note);
			}
			else
			{
				throw new FormatException("Arguments or model not valid");
			}
		}

		public async Task<IEnumerable<DoorNote>> GetAllDoorNotesByWorkorderDoorId(string doorId)
		{
			return await DataUnitOfWork.DoorNoteRepository.GetAllDoorNotesByWorkorderDoorId(doorId);
		}

		public async Task<IEnumerable<DoorNoteViewModel>> GetAllDoorNotesByWorkorderId(string workorderId)
		{
			var notes = await DataUnitOfWork.DoorNoteRepository.GetAllDoorNotesByWorkorderId(workorderId);
			var notesViewModels = new List<DoorNoteViewModel> { };

			foreach (var note in notes)
			{
				var door = await ObjectsLocator.WorkorderDoorService.GetWorkOrderDoorByDoorId(workorderId, note.DoorId);
				var noteViewModel = new DoorNoteViewModel(door.Door.DoorNo, note);

				notesViewModels.Add(noteViewModel);
			}

			return notesViewModels;
		}

		public async Task<long> GetNotesCountByDoorIdAsync(string id)
		{
			return await DataUnitOfWork.DoorNoteRepository.GetNotesCountByDoorIdAsync(id);
		}

        public async Task<Boolean>DeleteDoorNote(string noteId)
        {
            try
            {
                 await DataUnitOfWork.DoorNoteRepository.DeleteDoorNote(noteId);
                return true;
            }
            catch(Exception e)
            {
                return false;
            }
              
        }

        public async Task<bool> DeleteNoteByProjectId(string projectId)
        {
            try
            {
                await DataUnitOfWork.DoorNoteRepository.DeleteDoorNoteByProjectId(projectId);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task DeleteBulk(string[] ids)
        {
            await DataUnitOfWork.DoorNoteRepository.DeleteBulk(ids);
        }

        public async Task <IEnumerable<DoorNote>> GetAllNotesByProjectId(string ProjectId)
        {
            return await DataUnitOfWork.DoorNoteRepository.GetDoorNotesByProjectId(ProjectId);
        }

        public async Task<Dictionary<string, string>> CreateBulkAsync( IEnumerable<DoorNote> items)
        {
            return await DataUnitOfWork.DoorNoteRepository.AddWithIdMapAsync(items);
        }
    }
}
