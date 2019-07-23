using MicroBuild.Infrastructure.Models;
using MicroBuild.Management.Domain.Interfaces;
using MicroBuild.WebApiUtils.Authentication;
using MicroBuild.WebApiUtils.Extensions;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace MicroBuild.Management.API.Controllers
{
    public class MbeUsersController : ApiController
    {
		private IMbeUsersService MbeUsersService;

		public MbeUsersController(IMbeUsersService mbeUsersService)
		{
			this.MbeUsersService = mbeUsersService;
		}

		[MicroBuildAuthorize]
		[HttpGet]
		[Route("loggedInUser")]
		public async Task<HttpResponseMessage> GetLoggedInUser()
		{
			var user = await MbeUsersService.GetLoggedInUserAsync(Request);
			user.Password = null;

			return Request.CreateResponse(HttpStatusCode.OK, user);
		}

		[MicroBuildAuthorize]
		[HttpGet]
		[Route("mbeUsers")]
		public async Task<HttpResponseMessage> GetMBEUsers()
		{
			string userId = RequestContext.GetUserId();

			var users = await MbeUsersService.GetAllUsers(userId, Request);
			users.ForEach(u => u.Password = null);
			users.Sort(delegate (User x, User y) {
				return string.Compare(x.Name, y.Name);
			});

			return Request.CreateResponse(HttpStatusCode.OK, users);
		}

		[HttpGet]
		[Route("ping")]
		public async Task<HttpResponseMessage> Ping()
		{
			return Request.CreateResponse(HttpStatusCode.OK, "pong");
		}
	}
}
