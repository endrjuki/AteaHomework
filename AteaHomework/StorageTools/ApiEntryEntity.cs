using System;
using Azure;
using Azure.Data.Tables;

namespace StorageTools
{
    public class ApiEntryEntity : ITableEntity
    {
        public string Id { get; set; }
        public string Status { get; set; }
        public string PartitionKey { get; set; }
        public string RowKey { get; set; }
        public DateTimeOffset? Timestamp { get; set; }
        public ETag ETag { get; set; }
    }
}