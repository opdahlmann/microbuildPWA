using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using MicroBuild.Management.Common.MBEModels;
using MicroBuild.Management.Domain.ServiceClients;

namespace MicroBuild.Management.Domain.ImportAdapters
{
    public class MbeProjectImportAdapter : IProjectImportAdapter
    {
        private readonly MbeClient client;

		public MbeProjectImportAdapter(HttpRequestMessage httpRequest)
		{
			client = new MbeClient(httpRequest);
		}


		public async Task<List<MbeProject>> GetUserProjectsAsync()
		{
			return await client.GetAsync<List<MbeProject>>("projects");
		}

		public async Task<MbeProject> GetProjectByIdAsync(string projectId)
        {
            return await client.GetAsync<MbeProject>("projects/" + projectId);
        }

        public async Task<MasterDoorTemplate> GetMasterTemplate()
        {
            return await client.GetAsync<MasterDoorTemplate>("doorTemplates/master");
        }

        public async Task<bool> SetProgressProjectId(string engProjectId, string progProjectId)
        {
            return await client.PatchAsJsonAsync<string, bool>($"projects/{engProjectId}/ProgressId", progProjectId);
        }

		public async Task<bool> SetManagementProjectId(string engProjectId, string progProjectId)
		{
			return await client.PatchAsJsonAsync<string, bool>($"projects/{engProjectId}/ManagementId", progProjectId);
		}
	}
}
