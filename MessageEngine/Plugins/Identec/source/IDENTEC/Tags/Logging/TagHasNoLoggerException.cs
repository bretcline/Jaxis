namespace IDENTEC.Tags.Logging
{
    using System;

    public class TagHasNoLoggerException : Exception
    {
        public TagHasNoLoggerException()
        {
        }

        public TagHasNoLoggerException(string message) : base(message)
        {
        }

        public TagHasNoLoggerException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}

