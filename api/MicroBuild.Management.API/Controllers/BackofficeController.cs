using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

using MicroBuild.Management.Common.DTO;
using MicroBuild.Management.Common.ExceptionHandling;
using MicroBuild.Management.Domain.Interfaces;
using MicroBuild.WebApiUtils.Authentication;
using MicroBuild.WebApiUtils.Extensions;

namespace MicroBuild.Management.API.Controllers
{
	[ControllerExceptionFilter("MicroBuild.Progress.Backoffice")]
	public class BackofficeController : ApiController
    {
		private readonly IObjectsLocator ObjectsLocator;

		public BackofficeController(IObjectsLocator objectsLocator)
		{
			ObjectsLocator = objectsLocator;
		}

		[MicroBuildAuthorize]
		[HttpGet]
		[Route("projects/view/backoffice")]
		public async Task<HttpResponseMessage> GetAllProjects()
		{
			var userId = RequestContext.GetUserId();

			var projects = await ObjectsLocator.ProjectService.GetAllProjects();

			projects.Sort((Project x, Project y) =>
				System.DateTime.Compare(
					x.CreatedDate == null ? System.DateTime.Now : (System.DateTime)x.CreatedDate,
					y.CreatedDate == null ? System.DateTime.Now : (System.DateTime)y.CreatedDate
				)
			);

			return Request.CreateResponse(HttpStatusCode.OK, projects);
		}
	}
}
