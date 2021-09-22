using System;

namespace StorageLibrary.Exceptions
{
    public class LogStorageException : Exception
    {
        public LogStorageException()
        {
        }

        public LogStorageException(string message)
            : base(message)
        {
        }

        public LogStorageException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}