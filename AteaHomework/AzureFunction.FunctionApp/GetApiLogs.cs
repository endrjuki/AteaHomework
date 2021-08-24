using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;
using Microsoft.OData.Edm;
using Newtonsoft.Json;
using StorageTools;

namespace AzureFunction.FunctionApp
{
    public class GetApiLogs
    {
        private readonly ITableService _tableService;

        public GetApiLogs(ITableService tableService)
        {
            _tableService = tableService;
        }

        [FunctionName("GetApiLogs")]
        public async Task<IActionResult> RunAsync(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = null)]
            HttpRequest req, ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            string fromDate = req.Query["startDate"];
            string toDate = req.Query["endDate"];

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            dynamic data = JsonConvert.DeserializeObject(requestBody);
            fromDate = fromDate ?? data?.fromDate;
            toDate = toDate ?? data?.toDate;

            if (fromDate == null || toDate == null)
                return new BadRequestObjectResult(
                    "Please pass a startDate/endDate on the query string or in the request body in dd-MM-yyyy format");
            try
            {
                var startDate = DateTime.Parse(fromDate);
                var endDate = DateTime.Parse(toDate);
                var list = await _tableService.ListEntriesAsync("apiLog", startDate, endDate);
                return new OkObjectResult("API FETCH LOG [ID - STATUS - TIMESTAMP]:\n\n" + String.Join("\n", list));
            }
            catch (Exception e)
            {
                log.LogInformation($"{e} occured, message: {e.Message}");
                return new BadRequestObjectResult("error has occured.");
            }
        }
    }
}