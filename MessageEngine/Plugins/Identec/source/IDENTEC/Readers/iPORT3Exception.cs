namespace IDENTEC.Readers
{
    using System;

    public class iPORT3Exception : Exception
    {
        private IDENTEC.Readers.iPort3.ErrorCode m_errorCode;

        public iPORT3Exception()
        {
        }

        public iPORT3Exception(string message) : base(message)
        {
        }

        public iPORT3Exception(string message, IDENTEC.Readers.iPort3.ErrorCode error) : base(message)
        {
            this.m_errorCode = error;
        }

        public iPORT3Exception(string message, Exception innerException) : base(message, innerException)
        {
        }

        public IDENTEC.Readers.iPort3.ErrorCode ErrorCode
        {
            get
            {
                return this.m_errorCode;
            }
            set
            {
                this.m_errorCode = value;
            }
        }
    }
}

