using MicroBuild.Infrastructure.Models;
using MicroBuild.Management.Common.ViewObjects;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MicroBuild.Management.Domain.ImportAdapters
{
    public interface IUserImportAdapter
    {
        Task<List<User>> GetUsersAsync();

        Task<List<UserViewModel>> GetAllActiveUsersAnonymously();

        Task<User> GetLoggedInUserAsync();

        Task<List<User>> GetUsersByIds(string[] userIds);
    }
}
