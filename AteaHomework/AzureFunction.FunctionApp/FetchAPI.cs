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
        private readonly IBlobService _blobService;
        private readonly HttpClient _httpClient;

        public FetchAPI(IHttpClientFactory httpClientFactory, IBlobService blobService)
        {
            _httpClient = httpClientFactory.CreateClient();
            _blobService = blobService;
        }


        [FunctionName("FetchAPI")]
        public async Task RunAsync([TimerTrigger("0/5 * * * * *")] TimerInfo myTimer, ILogger log)
        {
            try
            {
                log.LogInformation($"API fetch attempted at: {DateTime.UtcNow}");
                var response = await _httpClient.GetAsync(_url);

                if (response.IsSuccessStatusCode)
                {
                    var responseBody = await response.Content.ReadAsStringAsync();
                    var guid = Guid.NewGuid().ToString();
                    await _blobService.UploadContentBlobAsync(responseBody, guid, "api-payloads");
                    log.LogInformation(responseBody);
                }
                else
                {
                    log.LogInformation($"Fetch failed, HTTP status code: ${response.StatusCode}");
                }

            }
            catch (Exception e)
            {
                log.LogInformation($"Fetch attempt failed: ${e}");
            }
        }
    }
}