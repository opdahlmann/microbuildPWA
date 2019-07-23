using MicroBuild.Infrastructure.Models;
using MicroBuild.Management.Common.DTO;
using MicroBuild.Management.Common.ExceptionHandling;
using MicroBuild.Management.Domain.Interfaces;
using MicroBuild.WebApiUtils.Authentication;
using MicroBuild.WebApiUtils.Extensions;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace MicroBuild.Management.API.Controllers
{
	[ControllerExceptionFilter("microbuild.management.admin")]
	public class AuthController : ApiController
    {
		private IAuthService AuthService;

		public AuthController(IAuthService userService)
		{
			this.AuthService = userService;
		}

		[HttpPost]
		[Route("login")]
		public async Task<HttpResponseMessage> GetAuthentication(LoginRequestModel loginRequestModel)
		{
			var tokenStr = await AuthService.GetUserByCredentials(loginRequestModel);

			return Request.CreateResponse(HttpStatusCode.OK, tokenStr);
		}
	}
}
