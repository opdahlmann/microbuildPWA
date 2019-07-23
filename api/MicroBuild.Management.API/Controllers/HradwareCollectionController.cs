using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

using MicroBuild.Management.Common.ExceptionHandling;
using MicroBuild.Management.Domain.Interfaces;
using MicroBuild.WebApiUtils.Authentication;
using MicroBuild.WebApiUtils.Extensions;

namespace MicroBuild.Management.API.Controllers
{
	[ControllerExceptionFilter("Management.admin")]
	public class HardwareCollectionController : ApiController
	{
		private IObjectsLocator objectsLocator;

		public HardwareCollectionController(IObjectsLocator objectsLocator)
		{
			this.objectsLocator = objectsLocator;
		}

		[MicroBuildAuthorize]
		[HttpGet]
		[Route("projects/{projectId}/templates/{templateId}/hardwarecollections")]
		public async Task<HttpResponseMessage> GetAllWorkorders(string projectId, string templateId)
		{
			string userId = RequestContext.GetUserId();

			var templateHardwareCollections = await objectsLocator.WorkorderTemplateHardwareCollectionService.GetHardwareCollections(templateId);

			return Request.CreateResponse(HttpStatusCode.OK, templateHardwareCollections);
		}
	}
}
