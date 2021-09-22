using System.Collections.Generic;
using System.Threading.Tasks;

namespace StorageLibrary.Services
{
    public interface IDataStorageService
    {
        public Task<string> GetDataAsync(string name);

        public Task<IEnumerable<string>> ListAllDataAsync();

        public Task UploadContentAsync(string content, string fileName);
    }
}