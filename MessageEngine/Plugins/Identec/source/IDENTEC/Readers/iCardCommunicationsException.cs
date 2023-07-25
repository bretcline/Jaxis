namespace IDENTEC.Readers
{
    using System;

    public class iCardCommunicationsException : Exception
    {
        public iCardCommunicationsException()
        {
        }

        public iCardCommunicationsException(string message) : base(message)
        {
        }

        public iCardCommunicationsException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}

