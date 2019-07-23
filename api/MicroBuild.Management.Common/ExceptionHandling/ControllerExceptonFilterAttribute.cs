using System.Net;
using System.Net.Http;
using System.Web.Http.Filters;

namespace MicroBuild.Management.Common.ExceptionHandling
{
	public class ControllerExceptionFilterAttribute : ExceptionFilterAttribute
	{
		private readonly string ApiName;

		public ControllerExceptionFilterAttribute(string apiName)
		{
			ApiName = apiName;
		}

        public override void OnException(HttpActionExecutedContext actionExecutedContext)
        {
            if (actionExecutedContext.Exception is ExternalApiNotFoundException)
            {
                actionExecutedContext.Response = ResponseMessage(actionExecutedContext, 750); //new HttpResponseMessage((HttpStatusCode) 750);
            }
            else if (actionExecutedContext.Exception is FailureAtExternalApiException)
            {
                actionExecutedContext.Response = ResponseMessage(actionExecutedContext, 751); //new HttpResponseMessage((HttpStatusCode) 751);
            }
            else
            {
                actionExecutedContext.Response = ResponseMessage(actionExecutedContext, 901); //new HttpResponseMessage((HttpStatusCode) 901);
            }
        }

		private HttpResponseMessage ResponseMessage(HttpActionExecutedContext actionExecutedContext, int code)
		{
			return new HttpResponseMessage
			{
				Content = new StringContent($"{ApiName}\n{actionExecutedContext.Exception.ToString()}"),
				StatusCode = (HttpStatusCode)code
			};
		}
	}
}
