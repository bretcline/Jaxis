namespace IDENTEC
{
    using System;

    public class DeviceException : Exception
    {
        private byte[] _data;
        private int _nByteProcessed;
        private int _responseCode;

        public DeviceException()
        {
        }

        public DeviceException(string message) : base(message)
        {
        }

        public DeviceException(string message, Exception innerException) : base(message, innerException)
        {
        }

        public DeviceException(string message, int actualCode) : base(message)
        {
            this._responseCode = actualCode;
        }

        public DeviceException(string message, int actualCode, int byteProcessed) : base(message)
        {
            this._responseCode = actualCode;
            this._nByteProcessed = byteProcessed;
        }

        public DeviceException(string message, int actualCode, byte[] data) : base(message)
        {
            this._responseCode = actualCode;
            this._nByteProcessed = data.Length;
            this.ResponseData = data;
        }

        public override string ToString()
        {
            switch (this._responseCode)
            {
                case 0:
                    return "Unknown";

                case 1:
                    return "Packet to sent too large";

                case 2:
                    return "Command in tag response is invalid";

                case 3:
                    return "Packet received too large";

                case 4:
                    return "Tag response CRC error";

                case 5:
                    return "No response from tag";

                case 6:
                    return "Tag response session ID not correct";

                case 7:
                    return "Tag response interrogator ID not corret";

                case 8:
                    return "Number of byte read does not match requested";

                case 9:
                    return "No access to this memory range";

                case 10:
                    return "Tag returned an error";
            }
            return "Unknown";
        }

        public override string Message
        {
            get
            {
                return (base.Message + ": " + this.ToString());
            }
        }

        public int ResponseCode
        {
            get
            {
                return this._responseCode;
            }
            set
            {
                this._responseCode = value;
            }
        }

        public byte[] ResponseData
        {
            get
            {
                return this._data;
            }
            set
            {
                this._data = value;
            }
        }

        public int TotalByteProcessed
        {
            get
            {
                return this._nByteProcessed;
            }
            set
            {
                this._nByteProcessed = value;
            }
        }
    }
}

