using System;
using StorageLibrary.Configuration;

namespace AzureFunction
{
    public class CustomConfigurationProvider : ICustomConfigurationProvider
    {
        public string AzureConnectionString => Environment.GetEnvironmentVariable("connectionString");
        public string ApiEndpoint => Environment.GetEnvironmentVariable("apiEndpoint");
        public string AzureTableName => Environment.GetEnvironmentVariable("azureTableName");
        public string AzureBlobContainerName => Environment.GetEnvironmentVariable("azureBlobContainerName");
    }
}