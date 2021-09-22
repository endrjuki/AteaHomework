using System.Configuration;
using Azure.Data.Tables;
using Azure.Storage.Blobs;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StorageLibrary;
using StorageLibrary.Configuration;
using StorageLibrary.Providers;
using StorageLibrary.Services;

[assembly: FunctionsStartup(typeof(AzureFunction.Startup))]
namespace AzureFunction
{
    public class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            builder.Services.AddSingleton(x =>
                new BlobServiceClient("UseDevelopmentStorage=true"));
            builder.Services.AddSingleton(x =>
                new TableServiceClient("UseDevelopmentStorage=true"));

            builder.Services.AddSingleton<ICustomConfigurationProvider, CustomConfigurationProvider>();
            builder.Services.AddSingleton<IStorageService, StorageService>();
            builder.Services.AddSingleton<IDataStorageService, DataStorageService>();
            builder.Services.AddSingleton<ILogService, LogService>();
            builder.Services.AddSingleton<IApiFetchService, ApiFetchService>();
            builder.Services.AddSingleton<IApiScraper, ApiScraper>();
            builder.Services.AddSingleton<ITimeProvider, TimeProvider>();
        }
    }
}