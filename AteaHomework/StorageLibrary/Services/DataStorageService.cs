using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using StorageLibrary.Configuration;

namespace StorageLibrary.Services
{
    public class DataStorageService : IDataStorageService
    {
        private readonly BlobServiceClient _blobServiceClient;
        private readonly ICustomConfigurationProvider _configurationProvider;

        public DataStorageService(BlobServiceClient blobServiceClient, ICustomConfigurationProvider configurationProvider)
        {
            _blobServiceClient = blobServiceClient;
            _configurationProvider = configurationProvider;
        }

        public async Task<string> GetDataAsync(string name)
        {
            var containerClient = _blobServiceClient
                .GetBlobContainerClient(_configurationProvider.AzureBlobContainerName);
            var blobClient = containerClient.GetBlobClient(name);
            var blobDownloadInfo = await blobClient.DownloadAsync();
            return await new StreamReader(blobDownloadInfo.Value.Content).ReadToEndAsync();
        }

        public async Task<IEnumerable<string>> ListAllDataAsync()
        {
            var containerClient = _blobServiceClient
                .GetBlobContainerClient(_configurationProvider.AzureBlobContainerName);
            var items = new List<string>();

            await foreach (var blobItem in containerClient.GetBlobsAsync())
            {
                items.Add(blobItem.Name);
            }

            return items;
        }

        public async Task UploadContentAsync(string content, string fileName)
        {
            var containerClient = _blobServiceClient
                .GetBlobContainerClient(_configurationProvider.AzureBlobContainerName);
            await containerClient.CreateIfNotExistsAsync();
            var blobClient = containerClient.GetBlobClient(fileName);

            var bytes = Encoding.UTF8.GetBytes(content);
            await using var memoryStream = new MemoryStream(bytes);
            await blobClient.UploadAsync(memoryStream, new BlobHttpHeaders { ContentType = "application/json" });
        }
    }
}