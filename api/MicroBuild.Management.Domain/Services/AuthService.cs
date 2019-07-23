using MicroBuild.Management.Common.DTO;
using MicroBuild.Management.Domain.ImportAdapters;
using MicroBuild.Management.Domain.Interfaces;
using System.Threading.Tasks;

namespace MicroBuild.Management.Domain.Services
{
	public class AuthService : BaseService, IAuthService
	{
		public async Task<string> GetUserByCredentials(LoginRequestModel loginRequestModel)
		{
			var authAdapter = new MbeAuthAdapter();

			return await authAdapter.GetUserByCredentials(loginRequestModel);
		}
	}
}
