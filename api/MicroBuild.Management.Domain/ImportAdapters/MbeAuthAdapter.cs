using MicroBuild.Management.Common.DTO;
using MicroBuild.Management.Domain.ServiceClients;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace MicroBuild.Management.Domain.ImportAdapters
{
	public class MbeAuthAdapter
	{
		private MbeClient client;

		public MbeAuthAdapter()
		{
			client = new MbeClient();
		}


		public async Task<string> GetUserByCredentials(LoginRequestModel userRequestModel)
		{
			var loginString = $"grant_type=password&username={userRequestModel.UserName}&password={userRequestModel.Password}";
			var responseJson = await client.PostStringAsync("token", loginString);

			var loginJson = JsonConvert.DeserializeObject<dynamic>(responseJson);
			return loginJson?.access_token;
		}
	}
}
