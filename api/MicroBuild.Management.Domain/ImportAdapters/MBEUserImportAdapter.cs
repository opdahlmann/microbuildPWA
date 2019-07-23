using MicroBuild.Infrastructure.Models;
using MicroBuild.Management.Common.ViewObjects;
using MicroBuild.Management.Domain.ServiceClients;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace MicroBuild.Management.Domain.ImportAdapters
{
    public class MbeUserImportAdapter : IUserImportAdapter
    {
        private MbeClient client;

		public MbeUserImportAdapter(HttpRequestMessage httpRequest)
		{
			client = new MbeClient(httpRequest);
		}


		public async Task<List<User>> GetUsersAsync()
        {
            return await client.GetAsync<List<User>>("users");
        }

        public async Task<List<UserViewModel>> GetAllActiveUsersAnonymously()
        {
            return await client.GetAsync<List<UserViewModel>>("allActiveUsersAnonymously");
        }      

        public async Task<User> GetLoggedInUserAsync()
		{
			return await client.GetAsync<User>("loggedInUser");
		}

        public async Task<List<User>> GetUsersByIds(string[] userIds)
        {
            var users = await client.GetAsync<List<User>>("users");
            return users.FindAll(user=> {
                foreach (var userId in userIds)
                {
                    if (userId == user.Id)
                        return true;
                    else
                        return false;
                }
                return false;
            });
        }
    }
}
