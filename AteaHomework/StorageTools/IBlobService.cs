using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Azure.Storage.Blobs.Models;

namespace StorageTools
{
    public interface IBlobService
    {
        public Task<string> GetBlobAsync(string name, string containerName);

        public Task<IEnumerable<string>> ListBlobsAsync(string containerName);

        public Task UploadContentBlobAsync(string content, string fileName, string containerName);
    }
}