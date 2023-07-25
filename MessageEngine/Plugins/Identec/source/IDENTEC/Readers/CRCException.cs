namespace IDENTEC.Readers
{
    using System;

    public class CRCException : Exception
    {
        private string _CalculatedCRC;
        private string _GivenCRC;
        internal string m_strBuffer;

        public CRCException()
        {
            this._CalculatedCRC = "";
            this._GivenCRC = "";
        }

        public CRCException(string message) : base(message)
        {
            this._CalculatedCRC = "";
            this._GivenCRC = "";
        }

        public CRCException(string message, Exception innerException) : base(message, innerException)
        {
            this._CalculatedCRC = "";
            this._GivenCRC = "";
        }

        public CRCException(string message, string buffer) : base(message)
        {
            this._CalculatedCRC = "";
            this._GivenCRC = "";
            this.m_strBuffer = buffer;
        }

        public string Buffer
        {
            get
            {
                return this.m_strBuffer;
            }
        }

        public string CalculatedCRC
        {
            get
            {
                return this._CalculatedCRC;
            }
            set
            {
                this._CalculatedCRC = value;
            }
        }

        public string GivenCRC
        {
            get
            {
                return this._GivenCRC;
            }
            set
            {
                this._GivenCRC = value;
            }
        }
    }
}

