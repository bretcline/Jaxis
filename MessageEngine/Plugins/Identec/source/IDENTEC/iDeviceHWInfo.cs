namespace IDENTEC
{
    using System;

    public class iDeviceHWInfo
    {
        private bool _bGotInfo;
        private System.DateTime _dtInfo;
        private uint _dwInfo;

        internal iDeviceHWInfo()
        {
        }

        internal iDeviceHWInfo(uint dwInfo)
        {
            this._dwInfo = dwInfo;
            this._bGotInfo = true;
            this._dtInfo = System.DateTime.Now;
        }

        public byte GetBLversion()
        {
            this.ThrowOnInvalidStatus();
            return (byte) ((this._dwInfo >> 0x18) & 0xff);
        }

        public short GetHWvalue()
        {
            this.ThrowOnInvalidStatus();
            return (short) (this._dwInfo & 0xffff);
        }

        public byte GetHWversion()
        {
            this.ThrowOnInvalidStatus();
            return (byte) ((this._dwInfo >> 0x10) & 0xff);
        }

        private void ThrowOnInvalidStatus()
        {
            if (!this._bGotInfo)
            {
                throw new InvalidOperationException("The Information is invalid");
            }
        }

        public System.DateTime DateTime
        {
            get
            {
                return this._dtInfo;
            }
        }
    }
}

