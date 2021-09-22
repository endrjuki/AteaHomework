using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Refit;
using StorageLibrary.Models;

namespace StorageLibrary.Services
{
    public interface IStorageService
    {
        Task StoreData(PublicApiResponse response);
        Task<IEnumerable<string>> RetrieveLogEntries(DateTime startDate, DateTime endDate);
        public Task<string> RetrieveData(string id);
    }
}