using System;
using Microsoft.OData.Edm;

namespace StorageLibrary.Providers
{
    public class TimeProvider : ITimeProvider
    {
        public DateTime Now()
        {
            return DateTime.Now;
        }
    }
}