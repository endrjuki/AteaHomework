using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using StorageLibrary.Services;

namespace AzureFunction
{
    public class GetApiPayload
    {
        private readonly IStorageService _storageService;
        public GetApiPayload(IStorageService storageService)
        {
            _storageService = storageService;
        }

        [FunctionName("GetApiPayload")]
        public async Task<IActionResult> RunAsync(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = null)]
            HttpRequest req, ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            var id = req.Query["id"];
            var blob = await _storageService.RetrieveData(id);
            return new AcceptedResult("", blob);
        }
    }
}