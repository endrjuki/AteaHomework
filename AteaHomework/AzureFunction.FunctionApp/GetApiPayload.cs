using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using StorageTools;

namespace AzureFunction.FunctionApp
{
    public class GetApiPayload
    {
        private readonly IBlobService _blobService;

        public GetApiPayload(IBlobService blobService)
        {
            _blobService = blobService;
        }

        [FunctionName("GetApiPayload")]
        public async Task<IActionResult> RunAsync(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = null)]
            HttpRequest req, ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            string id = req.Query["id"];

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            dynamic data = JsonConvert.DeserializeObject(requestBody);
            id = id ?? data?.id;

            if (id != null)
            {
                try
                {
                    var payload = await _blobService.GetBlobAsync(id, "api-payloads");
                    return new OkObjectResult(payload);
                }
                catch
                {
                    return new BadRequestObjectResult("Payload with that ID was not found");
                }
            }
            return new BadRequestObjectResult("Please pass a ID of the blob on the query string or in the request body");
        }
    }
}