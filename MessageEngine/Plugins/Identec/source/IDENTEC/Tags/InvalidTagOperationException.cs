namespace IDENTEC.Tags
{
    using System;

    public class InvalidTagOperationException : Exception
    {
        public InvalidTagOperationException()
        {
        }

        public InvalidTagOperationException(string message) : base(message)
        {
        }

        public InvalidTagOperationException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}

