using MicroBuild.Management.Common.DTO;
using MicroBuild.Management.Domain.Interfaces;
using MicroBuild.Management.Domain.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace MicroBuild.Management.API.Controllers
{
    public class MbeCompanyController : ApiController
    {
        private IMbeCompanyService MbeProjectService;

        public MbeCompanyController(MbeCompanyService mbeCompanyService)
        {
            this.MbeProjectService = mbeCompanyService;
        }

        [HttpGet]
        [Route("mbecompanies")]
        public async Task<HttpResponseMessage> GetMBECompanies()
        {
            List<MBECompany> companies = await MbeProjectService.GetExternalCompanies(Request);
            return Request.CreateResponse(HttpStatusCode.OK, companies);
        }
    }
}
