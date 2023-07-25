namespace IDENTEC.Tags
{
    using System;

    public class TagReadStringResult
    {
        private bool bSuccess;
        private int nBytesRead;
        private string strData;

        public TagReadStringResult()
        {
        }

        public TagReadStringResult(string data, bool success, int bytesRead)
        {
            this.strData = data;
            this.bSuccess = success;
            this.nBytesRead = bytesRead;
        }

        public int BytesRead
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

        public string Text
        {
            get
            {
                return this.strData;
            }
            set
            {
                this.strData = value;
            }
        }
    }
}

