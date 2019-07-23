using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using MicroBuild.Management.API.Models;
using MicroBuild.Management.Common.DTO;
using MicroBuild.Management.Common.ViewObjects;
using MicroBuild.Management.Domain.Interfaces;
using MicroBuild.WebApiUtils.Authentication;
using MicroBuild.WebApiUtils.Extensions;
using MicroBuild.WebApiUtils.Filters;

namespace MicroBuild.Management.API.Controllers
{
	[ControllerExceptionFilter("MicroBuild.Progress.Projects")]
	public class DoorController : ApiController
	{
		private IObjectsLocator ObjectsLocator;

		public DoorController(IObjectsLocator objectsLocator)
		{
			ObjectsLocator = objectsLocator;
		}

		[MicroBuildAuthorize]
		[HttpGet]
		[Route("projects/{projectId}/doors")]
		public async Task<HttpResponseMessage> GetAllDoorsInProject(string projectId)
		{
			string userId = RequestContext.GetUserId();
			var doors = await ObjectsLocator.DoorService.GetDoorsByProjectIdAsync(projectId);
			return Request.CreateResponse(HttpStatusCode.OK, doors);
		}

		[MicroBuildAuthorize]
		[HttpGet]
		[Route("projects/{projectId}/templates/{templateId}/doors")]
		public async Task<HttpResponseMessage> GetAllDoorsInTemplate(string projectId, string templateId)
		{
			string userId = RequestContext.GetUserId();
			var doors = await ObjectsLocator.WorkorderTemplateDoorService.GetTemplateDoors(templateId);
			return Request.CreateResponse(HttpStatusCode.OK, doors);
		}

		[MicroBuildAuthorize]
		[HttpGet]
		[Route("projects/{projectId}/templates/{templateId}/workorders/{workorderId}/doors")]
		public async Task<HttpResponseMessage> GetAllDoorsInWorkorderInstance(string projectId, string templateId, string workorderId)
		{
			string userId = RequestContext.GetUserId();
			var doors = await ObjectsLocator.WorkorderDoorService.GetDoorViewsInWorkorder(workorderId);
			return Request.CreateResponse(HttpStatusCode.OK, doors);
		}


		[MicroBuildAuthorize]
		[HttpGet]
		[Route("projects/{projectId}/templates/{templateId}/workorders/{workorderId}/doors/notifications")]
		public async Task<HttpResponseMessage> GetAllDoorsNotificationsInWorkorderInstance(string projectId, string templateId, string workorderId)
		{
			string userId = RequestContext.GetUserId();
			var doors = await ObjectsLocator.WorkorderDoorService.GetAllDoorsNotificationsInWorkorderInstance(workorderId, projectId);
			return Request.CreateResponse(HttpStatusCode.OK, doors);
		}

		[MicroBuildAuthorize]
		[HttpPost]
		[Route("projects/{projectId}/workorders/{workorderId}/doors/by/DoorNo/view/list")]
		public async Task<HttpResponseMessage> GetWorkorderDoorDetailsByDoorNo(
			string projectId, 
			string workorderId, 
			ByDoorNoRequestModels requestModel
		)
		{
			var userId = RequestContext.GetUserId();

			var workorderDoorView = await ObjectsLocator.WorkorderDoorService.GetDoorViewByDoorNo(workorderId, requestModel.DoorNo, requestModel.MbeProjectId, userId);

			return Request.CreateResponse(HttpStatusCode.OK, workorderDoorView);
		}

		[MicroBuildAuthorize]
		[HttpPost]
		[Route("projects/{projectId}/workorders/{workorderId}/doors/{doorId}/view/list")]
		public async Task<HttpResponseMessage> GetWorkorderDoorDetailsByDoorId(
			string projectId,
			string workorderId,
			string doorId,

			WithMbeProjectIdRequestModels requestModel
		)
		{
			var userId = RequestContext.GetUserId();

			var workorderDoorView = await ObjectsLocator.WorkorderDoorService.GetDoorViewByDoorId(workorderId, doorId, requestModel.MbeProjectId, userId);

			return Request.CreateResponse(HttpStatusCode.OK, workorderDoorView);
		}

		[MicroBuildAuthorize]
		[HttpPut]
		[Route("workorders/{workorderId}/doors/{doorId}/hardware/maintain")]
		public async Task<HttpResponseMessage> updateSelectedHardware(string workorderId, string doorId, DoorDetails selectedHardware)
		{
			string userId = RequestContext.GetUserId();
			var doorDetail = await ObjectsLocator.WorkorderDoorService.SetWorkorderDoorHardwareMaintain(workorderId, doorId, selectedHardware, userId);
			return Request.CreateResponse(HttpStatusCode.OK, doorDetail);
		}

        [MicroBuildAuthorize]
        [HttpPost]
        [Route("projects/{projectId}/projectdoor/by/DoorNo/view/list")]
        public async Task<HttpResponseMessage> GeProjectDoorByDoorNo(string projectId, ByDoorNoRequestModels requestModel)
        {
            string userId = RequestContext.GetUserId();
            var door = await ObjectsLocator.DoorService.GetDoorByDoorNo(projectId, requestModel.DoorNo);
            return Request.CreateResponse(HttpStatusCode.OK, door);
        }

    }
}
