namespace IDENTEC.Tags
{
    using System;

    public class TagReadDataResult
    {
        private bool m_bSuccess;
        private byte[] m_byArrayData;
        private Exception m_ErrorException;
        private int m_nBytesRead;
        private int m_nStartAddress;

        public TagReadDataResult()
        {
        }

        public TagReadDataResult(byte[] data, bool success, int bytesRead, int startAddress)
        {
            this.m_byArrayData = data;
            this.m_bSuccess = success;
            this.m_nBytesRead = bytesRead;
            this.m_nStartAddress = startAddress;
        }

        public TagReadDataResult(byte[] data, bool success, int bytesRead, int startAddress, Exception ex)
        {
            this.m_byArrayData = data;
            this.m_bSuccess = success;
            this.m_nBytesRead = bytesRead;
            this.m_nStartAddress = startAddress;
            this.m_ErrorException = ex;
        }

        [Obsolete("This property will be removed. Please use the 'Data' property instead.", true)]
        public byte[] GetData()
        {
            return this.m_byArrayData;
        }

        public override string ToString()
        {
            if (this.m_byArrayData != null)
            {
                return string.Format("Start: {0}, Success: {1}, {2} bytes: {3}", new object[] { this.m_nStartAddress, this.m_bSuccess, this.m_byArrayData.Length, BitConverter.ToString(this.m_byArrayData, 0, this.m_byArrayData.Length) });
            }
            return "";
        }

        public int BytesRead
        {
            get
            {
                return this.m_nBytesRead;
            }
            set
            {
                this.m_nBytesRead = value;
            }
        }

        public byte[] Data
        {
            get
            {
                return this.m_byArrayData;
            }
            set
            {
                this.m_byArrayData = value;
            }
        }

        public int StartAddress
        {
            get
            {
                return this.m_nStartAddress;
            }
            set
            {
                this.m_nStartAddress = value;
            }
        }

        public bool Success
        {
            get
            {
                return this.m_bSuccess;
            }
            set
            {
                this.m_bSuccess = value;
            }
        }
    }
}

