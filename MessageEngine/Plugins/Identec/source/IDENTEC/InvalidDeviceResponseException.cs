namespace IDENTEC
{
    using System;
    using System.Runtime.Serialization;

    [Serializable]
    public class InvalidDeviceResponseException : Exception
    {
        private int _responseCode;

        public InvalidDeviceResponseException()
        {
        }

        public InvalidDeviceResponseException(string message) : base(message)
        {
        }

        protected InvalidDeviceResponseException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        public InvalidDeviceResponseException(string message, Exception innerException) : base(message, innerException)
        {
        }

        public InvalidDeviceResponseException(string message, int actualCode) : base(message)
        {
            this._responseCode = actualCode;
        }

        public override string Message
        {
            get
            {
                return base.Message;
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
    }
}

