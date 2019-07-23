using MicroBuild.Management.Common.DTO;
using MicroBuild.Management.Domain.ServiceClients;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace MicroBuild.Management.Domain.ImportAdapters
{
    public class MbeDoorImportAdapter : IDoorImportAdapter
    {
        private MbeClient client;

		public MbeDoorImportAdapter(HttpRequestMessage request)
		{
			client = new MbeClient(request);
		}


		public async Task<List<Door>> GetDoorsByEngProjectId(string mbeProjectId)
        {
            return await client.GetAsync<List<Door>>($"projects/{mbeProjectId}/doors");
        }

        public Task<DoorImportSuccessStatus> ImportDoorsAsync(string mbeProjectId, string projectId, string userId)
        {
            throw new NotImplementedException();
        }
    }
}
