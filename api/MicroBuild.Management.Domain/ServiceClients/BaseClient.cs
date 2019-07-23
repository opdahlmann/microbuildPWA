using MicroBuild.Infrastructure.Exceptions.ExternalApplicationConnection;
using System;
using System.Configuration;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace MicroBuild.Management.Domain.ServiceClients
{
	public class BaseClient
	{
		private HttpClient client;

		public BaseClient(string urlKey)
		{
			CreateClient(urlKey);
		}

		public BaseClient(string urlKey, HttpRequestMessage request)
		{
			CreateClient(urlKey);

			client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", GetAccessToken(request));
		}

		private void CreateClient(string urlKey)
		{
			client = new HttpClient
			{
				BaseAddress = new Uri(ConfigurationManager.AppSettings.Get(urlKey)),
			};
		}


		public async Task<U> GetAsync<U>(string url)
		{
			var response = await client.GetAsync(url);

			if (response.IsSuccessStatusCode)
			{
				return await response.Content.ReadAsAsync<U>();
			}

			throw new UnsuccessfulResponseException($"{client.BaseAddress}{url} returned unsuccessful response. Status code: {response.StatusCode}");
		}

		public async Task<U> PostAsync<U>(string url, object content)
		{
			var message = await client.PostAsJsonAsync(url, content);

			if (message.IsSuccessStatusCode)
			{
				return await message.Content.ReadAsAsync<U>();
			}

			throw new UnsuccessfulResponseException($"{client.BaseAddress}{url} returned unsuccessful response. Status code: {message.StatusCode}");
		}

		public async Task<string> PostStringAsync(string url, string data)
		{
			var response = await client.PostAsync(url, new StringContent(data));

			if (response.IsSuccessStatusCode)
			{
				return await response.Content.ReadAsStringAsync();
			}

			throw new UnsuccessfulResponseException(client.BaseAddress + url + " returned unsuccessful response. Status code: " + response.StatusCode);
		}

		public async Task<U> PatchAsJsonAsync<T, U>(string requestUri, T content)
		{
			var jsonContent = new ObjectContent<T>(content, new JsonMediaTypeFormatter());
			var request = new HttpRequestMessage(new HttpMethod("PATCH"), requestUri) { Content = jsonContent };

			var resp = await client.SendAsync(request);

			if (resp.IsSuccessStatusCode)
			{
				return await resp.Content.ReadAsAsync<U>();
			}

			throw new UnsuccessfulResponseException(client.BaseAddress + requestUri + " returned unsuccessful response. Status code: " + resp.StatusCode);
		}

		private string GetAccessToken(HttpRequestMessage request)
		{
			return request?.Headers?.Authorization?.ToString().Split(' ')[1];
		}
	}
}
