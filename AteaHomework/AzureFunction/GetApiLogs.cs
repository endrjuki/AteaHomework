using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using StorageLibrary.Services;

namespace AzureFunction
{
    public class GetApiLogs
    {
        private readonly IStorageService _storageService;

        public GetApiLogs(IStorageService storageService)
        {
            _storageService = storageService;
        }

        [FunctionName("GetApiLogs")]
        public async Task<IActionResult> RunAsync(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = null)]
            HttpRequest req, ILogger log)
        {

            log.LogInformation("C# HTTP trigger function processed a request.");

            var from = DateTime.Parse(req.Query["from"]);
            var to = DateTime.Parse(req.Query["to"]);

            var logs = await _storageService.RetrieveLogEntries(from, to);

            return new OkObjectResult(logs);
        }
    }
}