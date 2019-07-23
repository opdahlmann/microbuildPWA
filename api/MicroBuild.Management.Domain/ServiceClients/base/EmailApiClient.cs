using System.Net.Http;
using System.Configuration;

namespace MicroBuild.Management.Domain.ServiceClients
{
    public abstract class EmailApiClient
    {
        public HttpClient HttpClient;

        public EmailApiClient(string userId) : this()
        {
            HttpClient.DefaultRequestHeaders.Remove("UserId");
            HttpClient.DefaultRequestHeaders.Add("UserId", userId);
        }

        public EmailApiClient()
        {
            HttpClient = new HttpClient();
            HttpClient.BaseAddress = new System.Uri(ConfigurationManager.AppSettings.Get("microbuild.MailService.api"));
        }
    }
}