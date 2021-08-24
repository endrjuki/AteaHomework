using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Azure;
using Azure.Storage.Blobs;
using Microsoft.Azure.Storage;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using StorageTools;

namespace AzureFunction.FunctionApp
{
    public class FetchAPI
    {
        private const string _url = "https://api.publicapis.org/random?auth=null";
        private readonly ITableService _tableService;
        private readonly IBlobService _blobService;
        private readonly HttpClient _httpClient;

        public FetchAPI(IHttpClientFactory httpClientFactory, IBlobService blobService, ITableService tableService)
        {
            _httpClient = httpClientFactory.CreateClient();
            _blobService = blobService;
            _tableService = tableService;
        }


        [FunctionName("FetchAPI")]
        public async Task RunAsync([TimerTrigger("0 * * * * *")] TimerInfo myTimer, ILogger log)
        {
            try
            {
                log.LogInformation($"API fetch attempted at: {DateTime.Now}");
                var response = await _httpClient.GetAsync(_url);

                if (response.IsSuccessStatusCode)
                {
                    var responseBody = await response.Content.ReadAsStringAsync();
                    var guid = Guid.NewGuid().ToString();
                    await _tableService.AddTableEntry(guid, DateTime.Now, "apiLog", response.StatusCode.ToString());
                    await _blobService.UploadContentBlobAsync(responseBody, guid, "api-payloads");
                    log.LogInformation(responseBody);
                }
                else
                {
                    log.LogInformation($"Fetch failed, HTTP status code: ${response.StatusCode}");
                    await _tableService.AddTableEntry("null", DateTime.Now, "apiLog", response.StatusCode.ToString());
                }
            }
            catch (Exception e)
            {
                log.LogInformation($"Fetch attempt failed: ${e}");
            }
        }
    }
}