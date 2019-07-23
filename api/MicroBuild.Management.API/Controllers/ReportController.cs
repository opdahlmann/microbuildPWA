using MicroBuild.Management.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace MicroBuild.Management.API.Controllers
{
    public class ReportController : ApiController
    {
        private IObjectsLocator ObjectsLocator;

        public ReportController(IObjectsLocator objectsLocator)
        {
            ObjectsLocator = objectsLocator;
        }

        [HttpGet]
        [Route("reports/project/{projectId}/templates/{templateId}/workorders/{workorderId}/overviewreport")]
        public async Task<HttpResponseMessage> GetSimpleOverviewReport(string projectId,string templateId,string workorderId)
        {
            var syncLogMetadataList = await ObjectsLocator.WorkorderDoorService.GetSimpleOverviewReport(projectId, templateId, workorderId);
            return Request.CreateResponse(HttpStatusCode.OK, syncLogMetadataList);
        }

        [HttpGet]
        [Route("reports/project/{projectId}/templates/{templateId}/workorders/overviewreport")]
        public async Task<HttpResponseMessage> GetSimpleOverviewReportForIntances(string projectId, string templateId)
        {
            var syncLogMetadataList = await ObjectsLocator.WorkorderDoorService.GetSimpleOverviewReportForIntances(projectId, templateId);
            return Request.CreateResponse(HttpStatusCode.OK, syncLogMetadataList);
        }
    }
}