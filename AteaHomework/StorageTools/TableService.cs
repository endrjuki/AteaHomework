using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Azure;
using Azure.Data.Tables;


namespace StorageTools
{
    public class TableService : ITableService
    {
        private readonly TableServiceClient _tableServiceClient;

        public TableService(TableServiceClient tableServiceClient)
        {
            _tableServiceClient = tableServiceClient;
        }

        public async Task AddTableEntry(string id, DateTime time, string tableName, string status = "success")
        {
            var tableClient = _tableServiceClient.GetTableClient(tableName);
            await tableClient.CreateIfNotExistsAsync();

            string yearMonthDay = time.ToString("yyyy-MM-dd");
            string hourMinute = time.ToString("hh-mm");
            var newEntity = new ApiEntryEntity()
            {
                Id = id,
                PartitionKey = yearMonthDay,
                RowKey = hourMinute,
                Status = status
            };

            await tableClient.AddEntityAsync(newEntity);
        }
        public async Task<IEnumerable<string>> ListEntriesAsync(string tableName, DateTime startDate, DateTime endDate)
        {
            var tableClient = _tableServiceClient.GetTableClient(tableName);

            var entityList = new List<string>();
            for (DateTime date = startDate; date <= endDate; date = date.AddDays(1))
            {
                var query = tableClient.QueryAsync<ApiEntryEntity>(e => e.PartitionKey == $"{date:yyyy-MM-dd}", null, new[] { "Id, Status, Timestamp" });

                await foreach (Page<ApiEntryEntity> page in query.AsPages())
                {
                    foreach (ApiEntryEntity entry in page.Values)
                    {
                        DateTime? timestamp;
                        timestamp = entry.Timestamp?.ToLocalTime().DateTime ?? DateTime.UnixEpoch;

                        entityList.Add(
                            $"{entry.Id} - {entry.Status} - {timestamp}");
                    }
                }
            }

            return entityList;
        }
    }
}