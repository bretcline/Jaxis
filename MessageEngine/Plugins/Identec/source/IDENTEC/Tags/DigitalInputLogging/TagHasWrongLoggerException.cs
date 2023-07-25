namespace IDENTEC.Tags.DigitalInputLogging
{
    using System;

    public class TagHasWrongLoggerException : Exception
    {
        public TagHasWrongLoggerException()
        {
        }

        public TagHasWrongLoggerException(string message) : base(message)
        {
        }

        public TagHasWrongLoggerException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}

