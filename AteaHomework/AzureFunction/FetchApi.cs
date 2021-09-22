using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Azure;
using Azure.Storage.Blobs;
using Microsoft.Azure.Storage;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using StorageLibrary;
using StorageLibrary.Configuration;
using StorageLibrary.Services;
namespace AzureFunction
{
    public class FetchAPI
    {
        private readonly IApiScraper _apiScraper;
        private readonly ICustomConfigurationProvider _provider;

        public FetchAPI(IApiScraper apiScraper, ICustomConfigurationProvider provider)
        {
            _apiScraper = apiScraper;
            _provider = provider;

        }

        [FunctionName("FetchAPI")]
        public async Task RunAsync([TimerTrigger("*/10 * * * * *")] TimerInfo myTimer, ILogger log)
        {
            log.LogInformation($"API fetch attempted at: {DateTime.Now}");
            _apiScraper.Scrape();

        }
    }
}