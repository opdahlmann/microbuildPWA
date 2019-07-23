using System.Net.Http;
using System.Threading.Tasks;
using MicroBuild.Management.Common.RequestModels;
using MicroBuild.Management.Domain.ServiceClients;

namespace MicroBuild.Management.Domain.ImportAdapters
{
    public class JsonToExcelAdapter : IJsonToExcelAdapter
    {
        private JsonToExcelClient client;

        public JsonToExcelAdapter()
        {
            client = new JsonToExcelClient();
            API_URL = client.API_URL;
        }

        public async Task<string> CreateExcelViewForAdmin(string mbeProjectId, object content)
        {
            var response = await client.PostAsync($"excel/formatcontent/configs/geturl", content);
            var result = await response.Content.ReadAsAsync<ExcelExportResult>();
            return result?.url;
        }

        public string API_URL { get; private set; }
    }
}
