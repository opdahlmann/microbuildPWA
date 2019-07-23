using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Collections.Generic;
using MicroBuild.Management.Common.DTO;
using MicroBuild.Management.Common.MBEModels;
using MicroBuild.Management.Domain.Interfaces;
using MicroBuild.WebApiUtils.Authentication;
using MicroBuild.WebApiUtils.Extensions;
using MicroBuild.WebApiUtils.Filters;
using System.Linq;
using MicroBuild.Management.Common.ViewObjects;
using MicroBuild.Management.API.Models;

namespace MicroBuild.Management.API.Controllers
{
    [ControllerExceptionFilter("MicroBuild.Progress.Projects")]
    public class ProjectController : ApiController
    {
        private IObjectsLocator ObjectsLocator;

        public ProjectController(IObjectsLocator objectsLocator)
        {
            ObjectsLocator = objectsLocator;
        }

		#region [ ADMIN, generic ]

		[MicroBuildAuthorize]
        [HttpGet]
        [Route("projects")]
        public async Task<HttpResponseMessage> GetProjectsForAdmin()
        {
            string userId = RequestContext.GetUserId();
            List<Project> projects = await ObjectsLocator.ProjectService.GetProjectsByUserIdAsync(userId);
            projects.Sort(delegate (Project x, Project y)
            {
                return System.DateTime.Compare(
                    x.CreatedDate == null ? System.DateTime.Now : (System.DateTime)x.CreatedDate,
                    y.CreatedDate == null ? System.DateTime.Now : (System.DateTime)y.CreatedDate
					);
            });
            return Request.CreateResponse(HttpStatusCode.OK, projects);
        }

        [MicroBuildAuthorize]
        [HttpGet]
        [Route("projects/{projectId}")]
        public async Task<HttpResponseMessage> GetProjectByProjectId(string projectId)
        {
            string userId = RequestContext.GetUserId();
            Project project = await ObjectsLocator.ProjectService.GetProject(projectId);
            return Request.CreateResponse(HttpStatusCode.OK, project);
        }

        [MicroBuildAuthorize]
        [HttpPost]
        [Route("mbeProjects/import")]
        public async Task<HttpResponseMessage> ImportMBEProject(MbeProject mbeProject)
        {
            string userId = RequestContext.GetUserId();
            var result = await ObjectsLocator.MbeProjectService.ImportFromMbe(mbeProject,userId);

            if (result == null)
            {
                return Request.CreateResponse(HttpStatusCode.Conflict);
            }

            return Request.CreateResponse(HttpStatusCode.OK, result);
        }

		[MicroBuildAuthorize]
		[HttpGet]
		[Route("projects/{projectId}/sync/preview")]
		public async Task<HttpResponseMessage> GetSyncPreviewFromMbeProject(string projectId)
		{
			string userId = RequestContext.GetUserId();
			var result = await ObjectsLocator.MbeDoorService.GetMbeSyncPreview(projectId, userId);

			if (result == null)
			{
				return Request.CreateResponse(HttpStatusCode.Conflict);
			}

			return Request.CreateResponse(HttpStatusCode.OK, result);
		}

		[MicroBuildAuthorize]
		[HttpPut]
		[Route("projects/{projectId}/sync/apply")]
		public async Task<HttpResponseMessage> SyncFromMbeProject(string projectId)
		{
			string userId = RequestContext.GetUserId();
			var result = await ObjectsLocator.MbeDoorService.SyncFromMbe(projectId, userId);

			if (result == -1)
			{
				return Request.CreateResponse(HttpStatusCode.Conflict);
			}

			return Request.CreateResponse(HttpStatusCode.OK, result);
		}

		[MicroBuildAuthorize]
		[HttpPost]
		[Route("projects/{projectId}/admins")]
		public async Task<HttpResponseMessage> AddProjectAdmins(string projectId, List<string> userIds)
		{
			string currentUserId = RequestContext.GetUserId();
			long countOfUsersAdded = await ObjectsLocator.ProjectService.AddProjectAdmins(projectId, userIds, currentUserId);

			return Request.CreateResponse(HttpStatusCode.OK, countOfUsersAdded);
		}

        [MicroBuildAuthorize]
        [HttpDelete]
        [Route("projects/{projectId}/admin/{userId}")]
        public async Task<HttpResponseMessage> DeleteProjectAdmin(string projectId, string userId)
        {
            string currentUserId = RequestContext.GetUserId();
            bool isDeleted = await ObjectsLocator.ProjectService.DeleteProjectAdmin(projectId, userId);

            return Request.CreateResponse(HttpStatusCode.OK, isDeleted);
        }

        [MicroBuildAuthorize]
		[HttpDelete]
		[Route("projects/{projectId}")]
		public async Task<HttpResponseMessage> DeleteProject(string projectId)
		{
			string userId = RequestContext.GetUserId();

			await ObjectsLocator.ProjectService.DeleteProject(projectId, userId);

			return Request.CreateResponse(HttpStatusCode.OK);
		}

		#endregion

		#region [ MOBILE ]

		[MicroBuildAuthorize]
        [HttpGet]
        [Route("projects/view/mobile")]
        public async Task<HttpResponseMessage> GetProjectsForUser()
        {
            var userId = RequestContext.GetUserId();
            var projects = (await ObjectsLocator.ProjectService.GetProjectsForMobile(userId)).ToList();

            projects.Sort(delegate (ProjectsViewModelForMobile x, ProjectsViewModelForMobile y)
            {
                return System.DateTime.Compare(
                    x.CreatedDate == null ? System.DateTime.Now : (System.DateTime)x.CreatedDate,
                    y.CreatedDate == null ? System.DateTime.Now : (System.DateTime)y.CreatedDate
                    );
            });
            return Request.CreateResponse(HttpStatusCode.OK, projects);
        }

        #endregion

        #region [ CLIENT ]

        [MicroBuildAuthorize]
        [HttpGet]
        [Route("projects/view/client")]
        public async Task<HttpResponseMessage> GetProjectsForUserClient()
        {
            var userId = RequestContext.GetUserId();
            var projects = (await ObjectsLocator.ProjectService.GetProjectsForMobile(userId)).ToList();

            projects.Sort(delegate (ProjectsViewModelForMobile x, ProjectsViewModelForMobile y)
            {
                return System.DateTime.Compare(
                    x.CreatedDate == null ? System.DateTime.Now : (System.DateTime)x.CreatedDate,
                    y.CreatedDate == null ? System.DateTime.Now : (System.DateTime)y.CreatedDate
                    );
            });
            return Request.CreateResponse(HttpStatusCode.OK, projects);
        }

		[MicroBuildAuthorize]
		[HttpPost]
		[Route("projects/notifications")]
		public async Task<HttpResponseMessage> GetProjectNotificationsOnClient(ProjectNotificationsRequestModel notificationsRequestModel)
		{
			var userId = RequestContext.GetUserId();
			var notificationsMap = await ObjectsLocator.ProjectService.GetProjectNotificationsMap(userId, notificationsRequestModel.ProjectIds);

			return Request.CreateResponse(HttpStatusCode.OK, notificationsMap);
		}

		#endregion


		[MicroBuildAuthorize]
        [HttpGet]
        [Route("projects/{projectId}/user/{userId}/roles")]
        public async Task<HttpResponseMessage> test(string projectId,string userId)
        {
            var _userId = RequestContext.GetUserId();
            var userRoles = await ObjectsLocator.ProjectService.GetUserRoles(projectId, userId);
            return Request.CreateResponse(HttpStatusCode.OK, userRoles);
        }
    }
}
