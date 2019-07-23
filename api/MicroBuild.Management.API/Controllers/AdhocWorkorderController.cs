using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

using MicroBuild.Management.Domain.Interfaces;
using MicroBuild.WebApiUtils.Authentication;
using MicroBuild.WebApiUtils.Extensions;

namespace MicroBuild.Management.API.Controllers
{
    public class AdhocWorkorderController : ApiController
    {
		private readonly IObjectsLocator ObjectsLocator;

		public AdhocWorkorderController(IObjectsLocator objectsLocator)
		{
			ObjectsLocator = objectsLocator;
		}

		[MicroBuildAuthorize]
		[HttpGet]
		[Route("projects/{projectId}/company/{companyId}/workorder/ad-hoc/door/{doorNr}")]
		public async Task<HttpResponseMessage> CreateWorkorder(string projectId, string companyId, string doorNr)
		{
			string userId = RequestContext.GetUserId();

			var workorderDoor = await ObjectsLocator.AdhocWorkorderService.CreateWordorderForDoor(
				projectId,
				companyId,
				doorNr,
				userId
			);

			return Request.CreateResponse(HttpStatusCode.OK, workorderDoor);
		}

		[MicroBuildAuthorize]
		[HttpGet]
		[Route("projects/{projectId}/templates/{templateId}/ad-hoc/workorder")]
		public async Task<HttpResponseMessage> GetWorkorder(string projectId, string templateId)
		{
			string userId = RequestContext.GetUserId();

			var workorderInstance = await ObjectsLocator.WorkorderService.GetFirstWorkorderByTemplateId(templateId);

			return Request.CreateResponse(HttpStatusCode.OK, workorderInstance);
		}
	}
}
