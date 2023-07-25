namespace IDENTEC
{
    using System;

    public class CommPortException : Exception
    {
        private int m_nErrorCode;

        public CommPortException(string desc) : base(desc)
        {
        }

        public CommPortException(string desc, int Win32ErrorCode) : base(desc)
        {
            this.m_nErrorCode = Win32ErrorCode;
        }

        public int Win32ErrorCode
        {
            get
            {
                return this.m_nErrorCode;
            }
        }
    }
}

