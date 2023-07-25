namespace IDENTEC.Tags
{
    using System;

    public class TagDataException : Exception
    {
        internal byte[] m_byDataRead;

        public TagDataException()
        {
        }

        public TagDataException(string message) : base(message)
        {
        }

        public TagDataException(string message, byte[] tagData) : base(message)
        {
            this.m_byDataRead = tagData;
        }

        public TagDataException(string message, Exception innerException) : base(message, innerException)
        {
        }

        public byte[] DataRead
        {
            get
            {
                if (this.m_byDataRead == null)
                {
                    return new byte[0];
                }
                return this.m_byDataRead;
            }
        }
    }
}

