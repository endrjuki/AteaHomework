using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.OData.Edm;
using Refit;

namespace StorageLibrary.Services
{
    public interface ILogService
    {
        public Task AddLogEntryAsync(string id, string statusCode, DateTime time);

        public Task<IEnumerable<string>> ListEntriesByDateAsync(DateTime startDate, DateTime endDate);
    }
}