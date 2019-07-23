using MicroBuild.Infrastructure.Exceptions.ExternalApplicationConnection;
using System;
using System.Configuration;
using System.Net.Http;
using System.Threading.Tasks;

namespace MicroBuild.Management.Domain.ServiceClients
{
	public class JsonToExcelClient
    {
        private HttpClient client;

        public JsonToExcelClient()
        {
            client = new HttpClient();
            API_URL = ConfigurationManager.AppSettings.Get("jsontoexcel.api");
            client.BaseAddress = new Uri(API_URL);
        }

        public async Task<HttpResponseMessage> PostAsync(string url, object content)
        {
            var message = await client.PostAsJsonAsync(url, content);

            if (message.IsSuccessStatusCode)
            {
                return message;
            }

            throw new UnsuccessfulResponseException($"{client.BaseAddress}{url} returned unsuccessful response. Status code: {message.StatusCode}");
        }

        public string API_URL { get; private set; }
    }
}
