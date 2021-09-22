namespace StorageLibrary.Configuration
{
    public interface ICustomConfigurationProvider
    {
        string AzureConnectionString { get; }
        string ApiEndpoint { get; }
        string AzureTableName { get; }
        string AzureBlobContainerName { get; }

    }
}