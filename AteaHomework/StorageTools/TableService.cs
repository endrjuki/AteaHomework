using System;
using System.Threading.Tasks;
using Azure.Data.Tables;
using Microsoft.WindowsAzure.Storage.Table;

namespace StorageTools
{
    public class TableService : ITableService
    {
        private readonly TableServiceClient _tableServiceClient;

        public TableService(TableServiceClient tableServiceClient)
        {
            _tableServiceClient = tableServiceClient;
        }

        public void AddTableEntry(string id, DateTime time, string tableName)
        {
            var tableClient = _tableServiceClient.GetTableClient(tableName);

            string dayHour = $"{time.Day-time.Hour}";
            string minute = $"{time.Minute}";
            //tableClient.AddEntityAsync(new ApiEntry(dayHour, minute, id));
        }
    }
}