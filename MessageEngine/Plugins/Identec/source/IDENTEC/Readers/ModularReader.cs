namespace IDENTEC.Readers
{
    using IDENTEC;
    using System;
    using System.Text;
    using System.Threading;

    [Obsolete("This constructor is now obsolete. Please use the 'iPortMB(iBusAdapter iBus)' constructor instead.", false)]
    public abstract class ModularReader : Reader, IComparable
    {
        internal iBusAdapter _iBus;
        public static readonly byte FIRSTREADERWITHDISCONNECTEDSLAVE = 0xfe;
        internal byte m_byAddress;
        private DateTime m_dtBoot;
        private uint m_dwSerialNumber;
        internal int m_nMajorVersion;
        internal int m_nMinorVersion;
        private uint m_nPacketsReceived;
        private uint m_nPacketsSent;
        internal IDENTEC.Readers.ModularReaderBus m_ReaderBus;
        internal string m_strInformation;

        public ModularReader(iBusAdapter iBus)
        {
            this.m_byAddress = FIRSTREADERWITHDISCONNECTEDSLAVE;
            this.m_strInformation = "";
            this._iBus = iBus;
        }

        [Obsolete("This constructor is now obsolete. Please use the 'ModularReader(iBusAdapter iBus)' constructor instead.", false)]
        public ModularReader(IDENTEC.Readers.ModularReaderBus comm)
        {
            this.m_byAddress = FIRSTREADERWITHDISCONNECTEDSLAVE;
            this.m_strInformation = "";
            this.m_ReaderBus = comm;
            this.m_ReaderBus.m_isolProtocolFramer.PacketReceived += new ISolProtocolFramer.PacketReceivedEventHandler(this.m_isolProtocolFramer_PacketReceived);
            this.m_ReaderBus.m_isolProtocolFramer.PacketSent += new ISolProtocolFramer.PacketSentEventHandler(this.m_isolProtocolFramer_PacketSent);
        }

        public int CompareTo(object obj)
        {
            ModularReader reader = obj as ModularReader;
            return this.SerialNumber.CompareTo(reader.SerialNumber);
        }

        public override bool Disconnect()
        {
            throw new NotImplementedException("The method or operation is not implemented.");
        }

        internal bool EnableSlavePort(bool enable)
        {
            return this.SetParameter(0x10, enable ? 1 : 0);
        }

        [CLSCompliant(false)]
        public void GetParameter(byte bySubCmd, ref uint dwParameter)
        {
            dwParameter = 0;
            byte[] msg = new byte[0x10];
            msg[0] = this.m_byAddress;
            msg[1] = 0x44;
            msg[2] = bySubCmd;
            byte[] buffer2 = new byte[0x40];
            this.SendMessage(msg, 3);
            this.RecvMsg(buffer2, 5);
            if (buffer2[1] != 0xc4)
            {
                throw new InvalidOperationException("The response contained the incorrect command");
            }
            dwParameter = (uint) ((((buffer2[3] << 0x18) + (buffer2[4] << 0x10)) + (buffer2[5] << 8)) + buffer2[6]);
        }

        public int GetPowerSupplyVoltage()
        {
            uint dwParameter = 0;
            this.GetParameter(5, ref dwParameter);
            return (int) dwParameter;
        }

        internal void GetStaticAddress()
        {
            uint dwParameter = 0;
            this.GetParameter(0x11, ref dwParameter);
            this.m_byAddress = (byte) dwParameter;
        }

        public TimeSpan GetUptime()
        {
            uint dwParameter = 0;
            this.GetParameter(2, ref dwParameter);
            TimeSpan span = new TimeSpan(0, 0, 0, (int) dwParameter, 0);
            this.m_dtBoot = DateTime.Now - span;
            return span;
        }

        public virtual void Initialize()
        {
            this.ReadVersion();
            this.ReadGetReaderInformation();
        }

        private void m_isolProtocolFramer_PacketReceived(object sender)
        {
            this.m_nPacketsReceived++;
        }

        private void m_isolProtocolFramer_PacketSent(object sender)
        {
            this.m_nPacketsSent++;
        }

        internal virtual void ReadGetReaderInformation()
        {
        }

        internal void ReadSerialNumber()
        {
            this.GetParameter(1, ref this.m_dwSerialNumber);
        }

        internal virtual void ReadVersion()
        {
            byte[] msg = new byte[0x180];
            msg[0] = this.m_byAddress;
            msg[1] = 0x33;
            this.SendMessage(msg, 2);
            msg[0] = 0;
            msg[1] = 0;
            int num = this.RecvMsg(msg, 10);
            if (((num > 0) && ((msg[0] == this.m_byAddress) || (msg[0] == 0xff))) && ((msg[1] == 0xb3) && (num >= 4)))
            {
                this.m_nMajorVersion = msg[2];
                this.m_nMinorVersion = msg[3];
                this.m_strInformation = Encoding.UTF8.GetString(msg, 4, 20);
                this.m_strInformation = this.m_strInformation.Replace("\0", "");
                this.m_strInformation = this.m_strInformation.TrimEnd(null);
            }
        }

        protected virtual int RecvMsg(byte[] msg, int nPauseBeforeRead)
        {
            if (this._iBus == null)
            {
                return this.m_ReaderBus.m_isolProtocolFramer.RecvMsg(msg, this.m_ReaderBus.CommunicationsTimeout, nPauseBeforeRead);
            }
            if (nPauseBeforeRead != 0)
            {
                Thread.Sleep(nPauseBeforeRead);
            }
            IC3ProtocolMessage message = this._iBus._stream.ReadMessage(0x1388);
            Array.Copy(message.Buffer, 1, msg, 0, message.BufferLength - 1);
            return (message.BufferLength - 1);
        }

        protected virtual void SendMessage(byte[] msg, int len)
        {
            if (this._iBus == null)
            {
                this.m_ReaderBus.m_isolProtocolFramer.SendMessage(msg, len);
            }
            else
            {
                this._iBus.DataStream.SendMessage(msg, len);
            }
        }

        public bool SetParameter(byte bySubCmd, int dwParameter)
        {
            return SetParameter(bySubCmd, (uint) dwParameter);
        }

        [CLSCompliant(false)]
        public bool SetParameter(byte bySubCmd, uint dwParameter)
        {
            byte[] msg = new byte[0x20];
            byte[] buffer2 = new byte[0x40];
            msg[0] = this.m_byAddress;
            msg[1] = 0x43;
            msg[2] = bySubCmd;
            msg[3] = (byte) ((dwParameter >> 0x18) & 0xff);
            msg[4] = (byte) ((dwParameter >> 0x10) & 0xff);
            msg[5] = (byte) ((dwParameter >> 8) & 0xff);
            msg[6] = (byte) (dwParameter & 0xff);
            this.SendMessage(msg, 7);
            if (this.m_byAddress == 0xff)
            {
                return true;
            }
            if ((this.RecvMsg(buffer2, 5) > 0) && (buffer2[1] == 0xc3))
            {
                this.m_byAddress = buffer2[0];
                if (buffer2[2] == 0)
                {
                    return true;
                }
            }
            return false;
        }

        internal bool SetStaticAddress(int address)
        {
            if (this.SetParameter(0x11, (uint) address))
            {
                this.m_byAddress = (byte) address;
                return true;
            }
            return false;
        }

        public int Address
        {
            get
            {
                return this.m_byAddress;
            }
        }

        public DateTime BootDateTime
        {
            get
            {
                return this.m_dtBoot;
            }
        }

        public override bool Connected
        {
            get
            {
                if (this._iBus == null)
                {
                    return this.ModularReaderBus.Connected;
                }
                return this._iBus.DataStream.IsOpen;
            }
        }

        public IDENTEC.DataStream DataStream
        {
            get
            {
                return this._iBus.DataStream;
            }
            set
            {
                this._iBus.DataStream = value;
            }
        }

        public virtual string FirmwareVersion
        {
            get
            {
                return (this.m_nMajorVersion.ToString() + "." + this.m_nMinorVersion.ToString().PadLeft(2, '0'));
            }
        }

        public virtual string Information
        {
            get
            {
                return this.m_strInformation;
            }
        }

        public int MajorVersion
        {
            get
            {
                return this.m_nMajorVersion;
            }
        }

        public int MinorVersion
        {
            get
            {
                return this.m_nMinorVersion;
            }
        }

        public IDENTEC.Readers.ModularReaderBus ModularReaderBus
        {
            get
            {
                return this.m_ReaderBus;
            }
        }

        [CLSCompliant(false)]
        public uint PacketsReceived
        {
            get
            {
                return this.m_nPacketsReceived;
            }
        }

        [CLSCompliant(false)]
        public uint PacketsSent
        {
            get
            {
                return this.m_nPacketsSent;
            }
        }

        public virtual string SerialNumber
        {
            get
            {
                return this.m_dwSerialNumber.ToString();
            }
        }
    }
}

