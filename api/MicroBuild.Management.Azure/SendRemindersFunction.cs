using System;
using System.Net.Http;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Newtonsoft.Json;

namespace MicroBuild.Management.Azure
{
    public static class SendRemindersFunction
    {
        // every min
        private const string TimerSchedule = "0 */5 * * * *";
        private static HttpClient _client = new HttpClient();

        [FunctionName("SendReminders")]
        public static async void Run([TimerTrigger(TimerSchedule)]TimerInfo timer, TraceWriter log)
        {
            try
            {
                log.Info($"Tiggering Blog rebuild at: {DateTime.Now}");

                var response = await _client.PostAsJsonAsync(Environment.GetEnvironmentVariable("fornotstartedworkorders"),"{}");
                var responseContent = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<string>(responseContent);

                log.Info(result);
                log.Info($"Blog rebuild triggered successfully at: {DateTime.Now}");
            }
            catch (Exception ex)
            {
                log.Info(ex.Message);
            }
        }
    }
}
