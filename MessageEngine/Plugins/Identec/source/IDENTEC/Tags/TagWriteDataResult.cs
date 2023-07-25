namespace IDENTEC.Tags
{
    using System;

    public class TagWriteDataResult
    {
        private bool bSuccess;
        private int nBytesRead;

        public TagWriteDataResult()
        {
        }

        public TagWriteDataResult(bool success, int bytesRead)
        {
            this.bSuccess = success;
            this.nBytesRead = bytesRead;
        }

        public int BytesWritten
        {
            get
            {
                return this.nBytesRead;
            }
            set
            {
                this.nBytesRead = value;
            }
        }

        public bool Success
        {
            get
            {
                return this.bSuccess;
            }
            set
            {
                this.bSuccess = value;
            }
        }
    }
}

