using MicroBuild.Management.Domain.Interfaces;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace MicroBuild.Management.API.Controllers
{
    public class LogController : ApiController
    {
		private IObjectsLocator ObjectsLocator;

		public LogController(IObjectsLocator objectsLocator)
		{
			ObjectsLocator = objectsLocator;
		}

		[HttpGet]
		[Route("projects/{projectId}/synclog")]
		public async Task<HttpResponseMessage> GetSyncLogMetadataList(string projectId)
		{
			var syncLogMetadataList = await ObjectsLocator.SyncLogService.GetSyncLogItemsMetadataProjection(projectId);
			return Request.CreateResponse(HttpStatusCode.OK, syncLogMetadataList);
		}

		[HttpGet]
		[Route("projects/{projectId}/synclog/{syncLogId}")]
		public async Task<HttpResponseMessage> GetSyncLog(string projectId, string syncLogId)
		{
			var syncLogMetadataList = await ObjectsLocator.SyncLogService.GetSyncLogItem(projectId, syncLogId);
			return Request.CreateResponse(HttpStatusCode.OK, syncLogMetadataList);
		}

        [HttpGet]
        [Route("projects/{projectId}/workordertemplate/{workordertemplateId}/workorder/{workorderId}/maintainedlogs")]
        public async Task<HttpResponseMessage> GetAllMaintainedLogs(string projectId,string workordertemplateId,string workorderId)
        {
            var syncLogMetadataList = await ObjectsLocator.LogService.GetMaintainedLogEntries(projectId, workordertemplateId, workorderId);
            return Request.CreateResponse(HttpStatusCode.OK, syncLogMetadataList);
        }

        [HttpGet]
        [Route("projects/{projectId}/workordertemplate/{workordertemplateId}/workorder/{workorderId}/maintaineddoorlogs")]
        public async Task<HttpResponseMessage> GetAllMaintainedDoorLogs(string projectId, string workordertemplateId, string workorderId)
        {
            var syncLogMetadataList = await ObjectsLocator.LogService.GetMaintainedDoorLogEntries(projectId, workordertemplateId, workorderId);
            return Request.CreateResponse(HttpStatusCode.OK, syncLogMetadataList);
        }
    }
}
