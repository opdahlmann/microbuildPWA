using MicroBuild.Management.Common.DTO;
using MicroBuild.Management.Domain.Interfaces;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace MicroBuild.Management.Domain.Services
{
    public class MbeCompanyService : BaseService, IMbeCompanyService
    {
        public MbeCompanyService(IObjectsLocator objectsLocator) : base(objectsLocator) { }

        public Task<List<MBECompany>> GetExternalCompanies(HttpRequestMessage request)
        {
            var companyImportAdapter = ObjectLocator.CompanyImportAdapter;
            return companyImportAdapter.GetExternalCompanies();
        }
    }
}
