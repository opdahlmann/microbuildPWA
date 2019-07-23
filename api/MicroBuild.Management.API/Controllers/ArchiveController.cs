using MicroBuild.Management.Domain.Interfaces;
using MicroBuild.WebApiUtils.Authentication;
using MicroBuild.WebApiUtils.Extensions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web.Http;

namespace MicroBuild.Management.API.Controllers
{
    public class ArchiveController : ApiController
    {
        private IArchiveService _archiveService;

        public ArchiveController(IArchiveService archiveService)
        {
            _archiveService = archiveService;
        }


        [MicroBuildAuthorize]
        [HttpGet]
        [Route("archive/projects/{projectId}")]
        public async Task<HttpResponseMessage> GetBackup(string projectId)
        {
            string userId = RequestContext.GetUserId();
            var archive = await _archiveService.CreateProjectBackup(projectId, userId, Request);

            var resp = Request.CreateResponse(HttpStatusCode.OK);
            resp.Content = new StreamContent(archive);
            resp.Content.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");
            return resp;
        }

        [MicroBuildAuthorize]
        [HttpPost]
        [Route("archive/restore")]
        public async Task<HttpResponseMessage> RestoreFromBackup()
        {
            string userId = RequestContext.GetUserId();

            using (Stream fileContent = await Request.Content.ReadAsStreamAsync())
            {
                var mediaTypeHeaderValue = Request.Content.Headers.ContentType;
                var contentType = mediaTypeHeaderValue != null ? mediaTypeHeaderValue.MediaType : "application/octet-stream";

                var isSuccess = await _archiveService.RestoreArchive(contentType, fileContent, userId);

                if (isSuccess)
                {
                    return Request.CreateResponse(HttpStatusCode.Created);
                }
            }

            return Request.CreateResponse(HttpStatusCode.Conflict);
        }
    }
}
