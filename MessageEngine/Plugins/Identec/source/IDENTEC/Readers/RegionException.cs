namespace IDENTEC.Readers
{
    using System;

    public class RegionException : Exception
    {
        public RegionException()
        {
        }

        public RegionException(string message) : base(message)
        {
        }

        public RegionException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}

