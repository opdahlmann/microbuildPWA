using MicroBuild.PWA.Mongo;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Routing;

namespace MicroBuild.PWA.API
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            GlobalConfiguration.Configure(WebApiConfig.Register);
            MongoConfiguration dataConfiguration = new MongoConfiguration();
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
