namespace IDENTEC
{
    using System;

    public class InvalidTagStatusException : Exception
    {
        private int _statusCode;

        public InvalidTagStatusException(string message, int statusCode) : base(message)
        {
            this._statusCode = statusCode;
        }

        public override string ToString()
        {
            switch (this._statusCode)
            {
                case 0:
                    return " 0: No error";

                case 1:
                    return " 1: Unknown/Unsupported Command";

                case 2:
                    return " 2: Invalid Memory range";

                case 3:
                    return " 3: Incorrect Command Parameter";

                case 4:
                    return " 4: Data have been overwritten";

                case 5:
                    return " 5: Memory block cannot be partially read or written";

                case 6:
                    return " 6: No respone from device, potential HW failure";
            }
            return (" " + this._statusCode + ": Unknown");
        }

        public override string Message
        {
            get
            {
                return (base.Message + this.ToString());
            }
        }

        public int StatusCode
        {
            get
            {
                return this._statusCode;
            }
            set
            {
                this._statusCode = value;
            }
        }
    }
}

