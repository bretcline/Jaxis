namespace IDENTEC.Tags
{
    using System;

    public class PartialTagCommunicationsException : Exception
    {
        public PartialTagCommunicationsException()
        {
        }

        public PartialTagCommunicationsException(string message) : base(message)
        {
        }

        public PartialTagCommunicationsException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}

