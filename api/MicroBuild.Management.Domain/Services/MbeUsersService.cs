using MicroBuild.Infrastructure.Models;
using MicroBuild.Management.Common.ViewObjects;
using MicroBuild.Management.Domain.Interfaces;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace MicroBuild.Management.Domain.Services
{
	public class MbeUsersService : BaseService, IMbeUsersService
	{
		public MbeUsersService(IObjectsLocator objectsLocator) : base(objectsLocator) { }

		public async Task<List<User>> GetAllUsers(string userId, HttpRequestMessage request)
		{
			var userImportAdapter = ObjectLocator.UserImportAdapter;
            return await userImportAdapter.GetUsersAsync();
		}

        public async Task<List<User>> GetAllUsers()
        {
            var userImportAdapter = ObjectLocator.UserImportAdapter;
            var users = await userImportAdapter.GetUsersAsync();
            users.ForEach(u => u.Password = null);
            return users;
        }

        public async Task<List<UserViewModel>> GetAllActiveUsersAnonymously()
        {
            var userImportAdapter = ObjectLocator.UserImportAdapter;
            var users = await userImportAdapter.GetAllActiveUsersAnonymously();
            return users;
        }

        public async Task<User> GetLoggedInUserAsync(HttpRequestMessage request)
		{
			var userImportAdapter = ObjectLocator.UserImportAdapter;
			return await userImportAdapter.GetLoggedInUserAsync();
		}

        public async Task<List<User>> GetUsersByIds(string[] userIds)
        {
            var userImportAdapter = ObjectLocator.UserImportAdapter;
            var users = await userImportAdapter.GetUsersByIds(userIds);
            users.ForEach(u => u.Password = null);
            return users;
        }
    }
}
