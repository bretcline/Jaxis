namespace IDENTEC.Readers
{
    using System;

    public class ReaderTimeoutException : Exception
    {
        public ReaderTimeoutException()
        {
        }

        public ReaderTimeoutException(string message) : base(message)
        {
        }

        public ReaderTimeoutException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}

