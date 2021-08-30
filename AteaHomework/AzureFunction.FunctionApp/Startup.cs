using System.Configuration;
using Azure.Data.Tables;
using Azure.Storage.Blobs;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StorageTools;

[assembly: FunctionsStartup(typeof(AzureFunction.FunctionApp.Startup))]
namespace AzureFunction.FunctionApp
{
    public class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            builder.Services.AddHttpClient();

            builder.Services.AddSingleton(x =>
                new BlobServiceClient("UseDevelopmentStorage=true")); //Configuration.GetValue<string>("AzureWebJobsStorage")"
            builder.Services.AddSingleton(x =>
                new TableServiceClient("UseDevelopmentStorage=true"));

            builder.Services.AddSingleton<IBlobService, BlobService>();
            builder.Services.AddSingleton<ITableService, TableService>();
        }
    }
}