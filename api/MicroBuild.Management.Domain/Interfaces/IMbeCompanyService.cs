using System.Net.Http;
using System.Threading.Tasks;
using System.Collections.Generic;
using MicroBuild.Management.Common.DTO;

namespace MicroBuild.Management.Domain.Interfaces
{
	public interface IMbeCompanyService
    {
        Task<List<MBECompany>> GetExternalCompanies(HttpRequestMessage request);
    }
}
