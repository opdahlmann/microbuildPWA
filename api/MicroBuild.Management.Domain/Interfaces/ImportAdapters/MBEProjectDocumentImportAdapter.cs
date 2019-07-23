using MicroBuild.Management.Common.DTO;
using MicroBuild.Management.Domain.Interfaces;
using MicroBuild.Management.Domain.ServiceClients;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace MicroBuild.Management.Domain.ImportAdapters
{
    public class MbeProjectDocumentImportAdapter : IProjectDocumentImportAdapter
    {
        private MbeClient client;

		public MbeProjectDocumentImportAdapter(HttpRequestMessage request)
		{
			client = new MbeClient(request);
		}


		public async Task<List<ProjectDocument>> GetProjectDocuments(string projectId)
        {
			return await client.GetAsync<List<ProjectDocument>>($"projects/{projectId}/projectDocuments");
        }
    }
}
