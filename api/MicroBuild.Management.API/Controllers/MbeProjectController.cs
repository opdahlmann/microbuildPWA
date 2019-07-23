using MicroBuild.Management.Common.DTO;
using MicroBuild.Management.Common.MBEModels;
using MicroBuild.Management.Domain.Interfaces;
using MicroBuild.Management.Domain.Services;
using MicroBuild.WebApiUtils.Authentication;
using MicroBuild.WebApiUtils.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace MicroBuild.Management.API.Controllers
{
    public class MbeProjectController : ApiController
    {
        private IMbeProjectService MbeProjectService;

        public MbeProjectController(MbeProjectService mbeProjectService)
        {
            this.MbeProjectService = mbeProjectService;
        }

        [MicroBuildAuthorize]
        [HttpGet]
        [Route("mbeProjects")]
        public async Task<HttpResponseMessage> GetMBEProjects()
        {
            string userId = RequestContext.GetUserId();
            List<MbeProject> projects = await MbeProjectService.GetMbeProjectsAsync(userId, Request);
            projects.Sort(delegate (MbeProject x, MbeProject y)
            {
                return string.Compare(
                     x.Name,
                     y.Name
                    );
            });
            return Request.CreateResponse(HttpStatusCode.OK, projects);
        }

        [MicroBuildAuthorize]
        [HttpGet]
        [Route("mbeHeaders/{mbeProjectId}")]
        public async Task<HttpResponseMessage> GetMBEProjectHeaders(string mbeProjectId)
        {
            string userId = RequestContext.GetUserId();
            List<FieldHeader> headersList = await MbeProjectService.GetMbeProjectFieldHeaders( mbeProjectId);
            return Request.CreateResponse(HttpStatusCode.OK, headersList);
        }
    }
}
