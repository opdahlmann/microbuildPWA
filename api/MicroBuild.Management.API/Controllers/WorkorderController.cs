using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

using MicroBuild.WebApiUtils.Authentication;
using MicroBuild.WebApiUtils.Extensions;

using MicroBuild.Management.API.Models;
using MicroBuild.Management.Domain.Interfaces;
using MicroBuild.Management.Common.ExceptionHandling;
using MicroBuild.Management.Common.ViewObjects;

namespace MicroBuild.Management.API.Controllers
{
    [ControllerExceptionFilter("Management.admin")]
    public class WorkorderController : ApiController
    {
        private IObjectsLocator ObjectsLocator;

        public WorkorderController(IObjectsLocator objectsLocator)
        {
            ObjectsLocator = objectsLocator;
        }

        [MicroBuildAuthorize]
        [HttpGet]
        [Route("projects/{projectId}/templates")]
        public async Task<HttpResponseMessage> GetAllWorkorders(string projectId)
        {
            var userId = RequestContext.GetUserId();

			var userWorkorders = await ObjectsLocator.WorkorderTemplateService.GetAllProjectWorkorders(projectId, userId);

			return Request.CreateResponse(HttpStatusCode.OK, userWorkorders);
        }

		[MicroBuildAuthorize]
		[HttpGet]
		[Route("projects/{projectId}/templates/view/client")]
		public async Task<HttpResponseMessage> GetUserCompanyWorkordersForClient(string projectId)
		{
			var userId = RequestContext.GetUserId();

			var userWorkorders = await ObjectsLocator.WorkorderTemplateService.GetProjectWorkordersOfUserForClient(projectId, userId);

			return Request.CreateResponse(HttpStatusCode.OK, userWorkorders);
		}

		[MicroBuildAuthorize]
        [HttpPost]
        [Route("projects/{ProjectId}/templates")]
        public async Task<HttpResponseMessage> CreateWorkorder(string projectId, WorkorderTemplateCreateViewModel workorderCreateRequestModel)
        {
            string userId = RequestContext.GetUserId();

            var template = await ObjectsLocator.WorkorderTemplateService.CreateWordorderTemplate(
                projectId,
                workorderCreateRequestModel.Template,
                workorderCreateRequestModel.TemplateDoors,
                workorderCreateRequestModel.HardwareCollections,
                workorderCreateRequestModel.Workorders,
                userId);

            return Request.CreateResponse(HttpStatusCode.OK, template);
        }

		[MicroBuildAuthorize]
		[HttpPut]
		[Route("projects/{ProjectId}/templates")]
		public async Task<HttpResponseMessage> UpdateWorkorder(string projectId, WorkorderTemplateCreateViewModel workorderCreateRequestModel)
		{
			string userId = RequestContext.GetUserId();

			var template = await ObjectsLocator.WorkorderTemplateService.UpdateWordorderTemplate(
				projectId,
				workorderCreateRequestModel.Template,
				workorderCreateRequestModel.TemplateDoors,
				workorderCreateRequestModel.HardwareCollections,
				workorderCreateRequestModel.Workorders,
				userId);

			return Request.CreateResponse(HttpStatusCode.OK, template);
		}

		[MicroBuildAuthorize]
		[HttpGet]
		[Route("projects/{projectId}/templates/{templateId}/workorders")]
		public async Task<HttpResponseMessage> GetWorkorderInstances(string projectId, string templateId)
		{
			string userId = RequestContext.GetUserId();

			var workorderInstances = await ObjectsLocator.WorkorderService.GetWorkordersByTemplateId(templateId);

			return Request.CreateResponse(HttpStatusCode.OK, workorderInstances);
		}

		[MicroBuildAuthorize]
		[HttpGet]
		[Route("projects/{projectId}/templates/{templateId}/workorders/view/mobile")]
		public async Task<HttpResponseMessage> GetWorkorderInstancesForMobile(string projectId, string templateId)
		{
			string userId = RequestContext.GetUserId();

			var workorderInstances = await ObjectsLocator.WorkorderService.GetWorkordersByTemplateIdForMobile(templateId);

			return Request.CreateResponse(HttpStatusCode.OK, workorderInstances);
		}

		[MicroBuildAuthorize]
		[HttpPost]
		[Route("projects/{projectId}/templates/{templateId}/workorders/{workorderId}/start")]
		public async Task<HttpResponseMessage> StartWorkorderInstance(string projectId, string templateId, string workorderId)
		{
			string userId = RequestContext.GetUserId();
			var workorderInstance = await ObjectsLocator.WorkorderService.StartWorkorder(workorderId, userId);
			return Request.CreateResponse(HttpStatusCode.OK, workorderInstance);
		}

        //moved to EmailNotificationController
        //[MicroBuildAuthorize]
        //      [HttpPost]
        //      [Route("projects/email/fornotstartedworkorders")]
        //      public async Task<HttpResponseMessage> SendEmailNotificationForNotStartedWorkorders()
        //      {
        //          string userId = RequestContext.GetUserId();
        //          var res = await this.ObjectsLocator.WorkorderService.SendEmailNotificationForNotStartedWorkorders(userId);
        //          return Request.CreateResponse(HttpStatusCode.OK, res);
        //      }

        //      [MicroBuildAuthorize]
        //      [HttpPost]
        //      [Route("projects/email/remindernotifications")]
        //      public async Task<HttpResponseMessage> SendEmailReminderNotifications()
        //      {
        //          string userId = RequestContext.GetUserId();
        //          var res = await this.ObjectsLocator.WorkorderService.SendEmailReminderNotifications(userId);
        //          return Request.CreateResponse(HttpStatusCode.OK, res);
        //      }

        [MicroBuildAuthorize]
        [HttpGet]
        [Route("projects/{projectId}/company/{companyId}/templates/view/mobile")]
        public async Task<HttpResponseMessage> GetProjectWorkOrderTemplatesByCompanyId(string projectId,string companyId)
        {
            string userId = RequestContext.GetUserId();
            var wos = await this.ObjectsLocator.WorkorderTemplateService.GetProjectWorkOrderTemplatesByCompanyId(projectId, companyId);
            var newWos = wos.Select(x => new WorkorderTemplateListViewModel
            {
                Id = x.Id,
                IsAdHoc = x.IsAdHoc,
                DoorCount = x.DoorCount,
                Name = x.Name,
                CompanyId = x.CompanyId,
            });
            return Request.CreateResponse(HttpStatusCode.OK, newWos);
        }

        [MicroBuildAuthorize]
        [HttpPost]
        [Route("projects/{projectId}/templates/{templateId}/workorders/{workorderId}/finalize")]
        public async Task<HttpResponseMessage> FinalizeWorkorderInstance(string projectId, string templateId, string workorderId)
        {
            string userId = RequestContext.GetUserId();

            var workorderInstance = await ObjectsLocator.WorkorderService.FinalizeWorkorder(workorderId, userId);

            return Request.CreateResponse(HttpStatusCode.OK, workorderInstance);
        }
    }
}