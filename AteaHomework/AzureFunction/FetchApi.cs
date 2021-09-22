using System;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using StorageLibrary.Configuration;
using StorageLibrary.Services;
namespace AzureFunction
{
    public class FetchApi
    {
        private readonly IApiScraper _apiScraper;
        private readonly ICustomConfigurationProvider _provider;

        public FetchApi(IApiScraper apiScraper, ICustomConfigurationProvider provider)
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