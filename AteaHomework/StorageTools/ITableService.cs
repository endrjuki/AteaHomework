using System;
using System.Threading.Tasks;

namespace StorageTools
{
    public interface ITableService
    {
        public void AddTableEntry(string id, DateTime time, string tableName);
    }
}