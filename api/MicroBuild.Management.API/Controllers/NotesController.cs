using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

using MicroBuild.Management.Common.DTO;
using MicroBuild.Management.Domain.Interfaces;
using MicroBuild.WebApiUtils.Authentication;
using MicroBuild.WebApiUtils.Extensions;

namespace MicroBuild.Management.API.Controllers
{
    public class NotesController : ApiController
    {
		private readonly IObjectsLocator ObjectsLocator;

        public NotesController(IObjectsLocator objectsLocator)
		{
			ObjectsLocator = objectsLocator;
		}

		[MicroBuildAuthorize]
		[HttpPost]
		[Route("projects/{projectId}/templates/{templateId}/workorders/{workorderId}/doors/{workorderDoorId}/notes")]
		public async Task<HttpResponseMessage> AddDoorNotes(
			string projectId,
			string templateId,
			string workorderId,
			string workorderDoorId,
			DoorNote note
		)
		{
			string userId = RequestContext.GetUserId();

			var addedNote = await ObjectsLocator.DoorNotesService.AddDoorNote(projectId, templateId, workorderId, workorderDoorId, note, userId);

			return Request.CreateResponse(HttpStatusCode.OK, addedNote);
		}

		[MicroBuildAuthorize]
		[HttpGet]
		[Route("projects/{projectId}/templates/{templateId}/workorders/{workorderId}/doors/{workorderDoorId}/notes")]
		public async Task<HttpResponseMessage> GetAllDoorNotesForDoor(
			string projectId,
			string templateId,
			string workorderId,
			string workorderDoorId
		)
		{
			string userId = RequestContext.GetUserId();

			var doorNotes = await ObjectsLocator.DoorNotesService.GetAllDoorNotesByWorkorderDoorId(workorderDoorId);

			return Request.CreateResponse(HttpStatusCode.OK, doorNotes);
		}

		[MicroBuildAuthorize]
		[HttpGet]
		[Route("projects/{projectId}/templates/{templateId}/workorders/{workorderId}/notes")]
		public async Task<HttpResponseMessage> GetAllDoorNotesForWorkorder(
			string projectId,
			string templateId,
			string workorderId
		)
		{
			string userId = RequestContext.GetUserId();

			var doorNotes = await ObjectsLocator.DoorNotesService.GetAllDoorNotesByWorkorderId(workorderId);

			return Request.CreateResponse(HttpStatusCode.OK, doorNotes);
		}

        [MicroBuildAuthorize]
        [HttpDelete]
        [Route("project/{projectId}/template/doors/{doorId}/notes/{noteId}")]
        public async Task<HttpResponseMessage>DeleteDoorNote(string projectId, string doorId, string noteId)
        {
            string userId = RequestContext.GetUserId();
            var result = await ObjectsLocator.DoorNotesService.DeleteDoorNote(noteId);
            return Request.CreateResponse(HttpStatusCode.OK, result);
        }
	}
}
