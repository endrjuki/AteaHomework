using System;

namespace StorageLibrary.Exceptions
{
    public class DataStorageException : Exception
    {
        public DataStorageException()
        {
        }

        public DataStorageException(string message)
            : base(message)
        {
        }

        public DataStorageException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}