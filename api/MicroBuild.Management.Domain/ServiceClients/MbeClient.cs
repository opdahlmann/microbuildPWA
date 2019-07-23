using System.Net.Http;

namespace MicroBuild.Management.Domain.ServiceClients
{
    public class MbeClient : BaseClient
    {
		public MbeClient() : base("microbuild.engineering.api") { }

		public MbeClient(HttpRequestMessage request) : base("microbuild.engineering.api", request) { }
	}
}
