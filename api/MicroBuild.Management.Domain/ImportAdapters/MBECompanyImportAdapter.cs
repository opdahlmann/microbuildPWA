using MicroBuild.Management.Common.DTO;
using MicroBuild.Management.Domain.ServiceClients;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace MicroBuild.Management.Domain.ImportAdapters
{
    public class MbeCompanyImportAdapter : ICompanyImportAdapter
    {
        private MbeClient client;

		public MbeCompanyImportAdapter(HttpRequestMessage request)
		{
			client = new MbeClient(request);
		}


		public async Task<List<MBECompany>> GetExternalCompanies()
		{
			return await client.GetAsync<List<MBECompany>>("companies");
		}
	}
}
