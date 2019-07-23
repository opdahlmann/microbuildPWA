using MicroBuild.Management.Common.DTO;
using System.Threading.Tasks;

namespace MicroBuild.Management.Domain.Interfaces
{
	public interface IAuthService
	{
		Task<string> GetUserByCredentials(LoginRequestModel userRequestModel);
	}
}
