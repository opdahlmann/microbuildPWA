using MicroBuild.Management.API;
using System.Web.Http;
using Microsoft.Owin;
using Owin;
using MicroBuild.Management.API.App_Start;

[assembly: OwinStartup(typeof(MicroBuild.Management.API.Startup))]
namespace MicroBuild.Management.API
{
	public partial class Startup
	{
		public void Configuration(IAppBuilder app)
		{
			var config = new HttpConfiguration();
			config.Routes.MapHttpRoute("DefaultHttpRoute", "api/{controller}");

			var container = IocConfig.Configure(config);

			app.UseAutofacMiddleware(container);
			app.UseAutofacWebApi(config);

			WebApiConfig.Register(config);
			app.UseWebApi(config);
		}
	}
}