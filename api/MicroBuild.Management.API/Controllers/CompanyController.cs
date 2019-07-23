using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

using MicroBuild.Management.Common.DTO;
using MicroBuild.Management.Domain.Interfaces;
using MicroBuild.WebApiUtils.Authentication;
using MicroBuild.WebApiUtils.Extensions;

namespace MicroBuild.Management.API.Controllers
{
    public class CompanyController : ApiController
    {
        private ICompanyService _companyService;

        public CompanyController(ICompanyService companyService)
        {
            _companyService = companyService;
        }

        [MicroBuildAuthorize]
        [HttpGet]
        [Route("projects/{projectId}/companies")]
        public async Task<HttpResponseMessage> GetAllProjectCompanies(string projectId)
        {
            List<Company> companies = await _companyService.GetCompaniesByProjectId(projectId);
            return Request.CreateResponse(HttpStatusCode.OK, companies);
        }

        [HttpDelete]
        [Route("companies/{companyId}/users/{userId}")]
        public async Task<HttpResponseMessage> RemoveUserFromCompany(string companyId, string userId)
        {
            Company company = await _companyService.RemoveUserById(companyId, userId);
            return Request.CreateResponse(HttpStatusCode.OK, company);
        }

        [MicroBuildAuthorize]
        [HttpPut]
        [Route("projects/{projectId}/companies")]
        public async Task<HttpResponseMessage> UpdateCompany(Company company)
        {
            Company _company = await _companyService.UpdateCompany(company);
            return Request.CreateResponse(HttpStatusCode.OK, _company);
        }

        [MicroBuildAuthorize]
        [HttpPost]
        [Route("projects/{projectId}/companies")]
        public async Task<HttpResponseMessage> CreateProgressCompany(string projectId, Company company)
        {
            company = await _companyService.AddCompany(company, projectId);
            return Request.CreateResponse(HttpStatusCode.OK, company);
        }

        [MicroBuildAuthorize]
        [HttpDelete]
        [Route("projects/{projectId}/companies/{companyId}")]
        public async Task<HttpResponseMessage> DeleteCompany(string projectId, string companyId)
        {
            Company company = await _companyService.RemoveCompany(companyId, projectId);
            return Request.CreateResponse(HttpStatusCode.OK, company);
        }

        [MicroBuildAuthorize]
        [HttpGet]
        [Route("projects/{projectId}/users/{mbeUserId}/company")]
        public async Task<HttpResponseMessage> GetProjectCompanyForUser(string projectId)
        {
            var userId = RequestContext.GetUserId();

            var companies = await _companyService.GetProjectCompanyForUser(projectId, userId);

            return Request.CreateResponse(HttpStatusCode.OK, companies);
        }

		[MicroBuildAuthorize]
        [HttpGet]
        [Route("projects/{projectId}/companyUsers")]
        public async Task<HttpResponseMessage> GetUserByUserId(string projectId)
        {
            string userId = RequestContext.GetUserId();
            var user = await _companyService.GetUserByUserId(projectId, userId);
            return Request.CreateResponse(HttpStatusCode.OK, user);
        }
        
    }
}
