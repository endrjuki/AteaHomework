using System;
using System.Diagnostics.CodeAnalysis;

namespace StorageLibrary.Providers
{
    public interface ITimeProvider
    {
        public DateTime Now();
    }
}