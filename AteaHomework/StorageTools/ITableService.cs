using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.OData.Edm;

namespace StorageTools
{
    public interface ITableService
    {
        public Task AddTableEntry(string id, DateTime time, string tableName, string status);

        public Task<IEnumerable<string>> ListEntriesAsync(string tableName, DateTime startDate, DateTime endDate);
    }
}