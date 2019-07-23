using MicroBuild.Management.API.App_Start;
using MicroBuild.Management.Data.API;
using MicroBuild.Management.Data.Mongo;
using System;
using System.Configuration;
using System.Web;
using System.Web.Http;

namespace MicroBuild.Management.API
{
    public class WebApiApplication : HttpApplication
    {
        protected void Application_Start()
        {
			IocConfig.Configure(new HttpConfiguration());
			GlobalConfiguration.Configure(WebApiConfig.Register);
			IConfiguration dataConfiguration = new MongoConfiguration();
			dataConfiguration.Configure();
		}

		protected void Application_BeginRequest(object sender, EventArgs e)
		{
			HttpContext.Current.Response.AddHeader("Access-Control-Allow-Origin", "*");
			//HttpContext.Current.Response.AddHeader("Access-Control-Allow-Credentials", "true");

			if (HttpContext.Current.Request.HttpMethod == "OPTIONS")
			{
				HttpContext.Current.Response.AddHeader("Access-Control-Allow-Methods", ConfigurationManager.AppSettings.Get("AccessControlAllowMethods"));

				HttpContext.Current.Response.AddHeader("Access-Control-Allow-Headers", ConfigurationManager.AppSettings.Get("AccessControlAllowHeaders"));
				HttpContext.Current.Response.AddHeader("Access-Control-Max-Age", ConfigurationManager.AppSettings.Get("AccessControlMaxAge"));

				HttpContext.Current.Response.End();
			}
		}
	}
}
