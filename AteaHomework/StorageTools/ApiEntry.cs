using System;
using System.Collections.Generic;
using Microsoft.Azure.Cosmos.Table;

namespace StorageTools
{
    public class ApiEntry : TableEntity
    {
        public string Id { get; set; }

        public ApiEntry(){ }
        public ApiEntry(string dayHour, string hourMinute, string id) : base (dayHour, hourMinute)
        {
            this.Id = id; // GUID
        }
    }
}