using MicroBuild.Infrastructure.Models;
using MicroBuild.Management.Common.ViewObjects;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace MicroBuild.Management.Domain.Interfaces
{
	public interface IMbeUsersService
	{
		Task<List<User>> GetAllUsers(string userId, HttpRequestMessage request);

		Task<User> GetLoggedInUserAsync(HttpRequestMessage request);

        Task<List<User>> GetAllUsers();

        Task<List<UserViewModel>> GetAllActiveUsersAnonymously();

        Task<List<User>> GetUsersByIds(string[] userIds);

    }
}
