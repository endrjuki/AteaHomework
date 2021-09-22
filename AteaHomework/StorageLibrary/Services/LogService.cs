using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Azure;
using Azure.Data.Tables;
using Refit;
using StorageLibrary.Configuration;
using StorageLibrary.Models;


namespace StorageLibrary.Services
{
    public class LogService : ILogService
    {
        private readonly TableServiceClient _tableServiceClient;
        private readonly ICustomConfigurationProvider _configurationProvider;

        public LogService(TableServiceClient tableServiceClient, ICustomConfigurationProvider provider)
        {
            _tableServiceClient = tableServiceClient;
            _configurationProvider = provider;
        }
        public async Task AddLogEntryAsync(string id, string statusCode, DateTime time)
        {
            var tableClient = _tableServiceClient.GetTableClient(_configurationProvider.AzureTableName);
            await tableClient.CreateIfNotExistsAsync();

            string yearMonthDay = time.ToString("yyyy-MM-dd");
            string hourMinute = time.ToString("hh-mm");
            var newEntity = new ApiEntryEntity()
            {
                Id = id,
                PartitionKey = yearMonthDay,
                RowKey = hourMinute,
                Status = statusCode
            };

            await tableClient.AddEntityAsync(newEntity);
        }

        public async Task<IEnumerable<string>> ListEntriesByDateAsync(DateTime startDate, DateTime endDate)
        {
            var tableClient = _tableServiceClient.GetTableClient(_configurationProvider.AzureTableName);

            var entityList = new List<string>();
            for (DateTime date = startDate; date <= endDate; date = date.AddDays(1))
            {
                var query = tableClient
                    .QueryAsync<ApiEntryEntity>(e =>
                        e.PartitionKey == $"{date:yyyy-MM-dd}", null, new[] { "Id, Status, Timestamp" });

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