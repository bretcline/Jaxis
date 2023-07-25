namespace IDENTEC.Readers
{
    using System;

    public class iCardTimeoutException : Exception
    {
        public iCardTimeoutException()
        {
        }

        public iCardTimeoutException(string message) : base(message)
        {
        }

        public iCardTimeoutException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}

