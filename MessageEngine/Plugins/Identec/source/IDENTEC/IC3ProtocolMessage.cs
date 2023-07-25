namespace IDENTEC
{
    using IDENTEC.Readers;
    using NLog;
    using System;
    using System.Reflection;

    public class IC3ProtocolMessage
    {
        public const byte DLE = 0x10;
        public const byte EOT = 4;
        protected static Logger log = LogManager.GetLogger("IC3ProtocolMessage");
        private bool m_bUnpacked;
        private ByteBufferArray m_Bytes = new ByteBufferArray();
        private int m_indexEOT = -1;
        private int m_indexSOH = -1;
        private int m_nMaxLength = 0x1000;
        public const byte SOH = 1;

        private void AddBytesRead(byte[] byData, int length)
        {
            if ((length + this.m_Bytes.Count) > this.MaxLength)
            {
                throw new ArgumentOutOfRangeException("The length of the data has exceeded the maximum allow sized");
            }
            for (int i = 0; i < length; i++)
            {
                this.m_Bytes.Add(byData[i]);
            }
        }

        internal byte CheckResponse(byte address, byte command)
        {
            if (!this.IsUnpacked)
            {
                this.UnpackAndCheckCRC();
            }
            if (this.MessageLength <= 0)
            {
                throw new InvalidOperationException("The message length is invalid.");
            }
            if (address != iBusAdapter.BroadcastAddress)
            {
                byte num = (byte) (command | 0x80);
                if (num != this[2])
                {
                    throw new InvalidDeviceResponseException("The device did not respond with the appropriate response code: " + num.ToString(), this[2]);
                }
                byte num2 = this[1];
                if ((address != iBusAdapter.DISCONNECTED_SLAVE_ADDRESS) && (address != num2))
                {
                    throw new InvalidDeviceResponseException("The device's return address is invalid");
                }
            }
            return this[3];
        }

        private byte[] GetMessageBody()
        {
            byte[] buffer = new byte[this.m_Bytes.Count - 2];
            int num = 0;
            int num2 = this.StartOfHeaderIndex + 1;
            for (int i = num2; i < (this.m_Bytes.Count - 1); i++)
            {
                buffer[num++] = this.m_Bytes[i];
            }
            return buffer;
        }

        public static void PackMessage(ref int nIndex, byte[] bySendBuffer, byte byChar)
        {
            switch (byChar)
            {
                case 1:
                    bySendBuffer[nIndex++] = 0x10;
                    byChar = (byte) (byChar ^ 0x80);
                    bySendBuffer[nIndex++] = byChar;
                    return;

                case 4:
                    bySendBuffer[nIndex++] = 0x10;
                    byChar = (byte) (byChar ^ 0x80);
                    bySendBuffer[nIndex++] = byChar;
                    return;

                case 0x10:
                    bySendBuffer[nIndex++] = 0x10;
                    byChar = (byte) (byChar ^ 0x80);
                    bySendBuffer[nIndex++] = byChar;
                    return;
            }
            bySendBuffer[nIndex++] = byChar;
        }

        public IC3ProtocolMessage ParseMessage()
        {
            this.SynchronizeMessage();
            for (int i = 0; i < this.m_Bytes.Count; i++)
            {
                switch (this.m_Bytes[i])
                {
                    case 1:
                        this.m_indexSOH = i;
                        break;

                    case 4:
                        this.m_indexEOT = i;
                        return this.SplitBuffer();
                }
            }
            return null;
        }

        public IC3ProtocolMessage ParseMessage(byte[] byData, int length)
        {
            this.m_Bytes.Add(byData, 0, length);
            return this.ParseMessage();
        }

        public IC3ProtocolMessage SplitBuffer()
        {
            if (!this.FullMessage)
            {
                return null;
            }
            if (this.m_indexEOT == (this.m_Bytes.Count - 1))
            {
                return null;
            }
            IC3ProtocolMessage message = new IC3ProtocolMessage();
            for (int i = this.m_indexEOT + 1; i < this.m_Bytes.Count; i++)
            {
                message.m_Bytes.Add(this.m_Bytes[i]);
            }
            this.m_Bytes.RemoveRange(this.m_indexEOT + 1, message.m_Bytes.Count);
            return message;
        }

        internal void SynchronizeMessage()
        {
            int index = 0;
            int sourceIndex = -1;
            for (index = 0; index < this.m_Bytes.Count; index++)
            {
                if (this.m_Bytes[index] == 1)
                {
                    sourceIndex = index;
                    break;
                }
            }
            if (sourceIndex == -1)
            {
                sourceIndex = this.m_Bytes.Count;
            }
            if (sourceIndex > 0)
            {
                byte[] destinationArray = new byte[this.m_Bytes.Count - sourceIndex];
                if (log.IsDebugEnabled)
                {
                    string message = "Cleared: ";
                    for (int i = 0; i < sourceIndex; i++)
                    {
                        message = message + this.m_Bytes[i].ToString("X") + ":";
                    }
                    log.Debug(message);
                }
                Array.Copy(this.m_Bytes.ToArray(this.m_Bytes.Count), sourceIndex, destinationArray, 0, this.m_Bytes.Count - sourceIndex);
                this.m_Bytes.Clear();
                for (index = 0; index < destinationArray.Length; index++)
                {
                    this.m_Bytes.Add(destinationArray[index]);
                }
            }
        }

        public override string ToString()
        {
            return string.Format("|Buffer Length: {0}| Unpacked: {1}| Complete Message: {2}|", this.m_Bytes.Count, this.m_bUnpacked, this.FullMessage);
        }

        public void UnpackAndCheckCRC()
        {
            if (this.m_Bytes.Count > 0)
            {
                byte[] byOut = new byte[this.m_Bytes.Count];
                int num = UnpackMessage(byOut, this.m_Bytes.ToArray(), byOut.Length);
                this.m_bUnpacked = true;
                if (num != this.m_Bytes.Count)
                {
                    this.m_indexEOT -= this.m_Bytes.Count - num;
                    this.m_Bytes.Clear();
                    for (int i = 0; i < num; i++)
                    {
                        this.m_Bytes.Add(byOut[i]);
                    }
                }
                byte[] messageBody = this.GetMessageBody();
                ushort num3 = IDENTEC.CRC.CRC16(messageBody, messageBody.Length - 2);
                ushort num4 = (ushort) ((messageBody[messageBody.Length - 1] << 8) | messageBody[messageBody.Length - 2]);
                if (num3 != num4)
                {
                    throw new CRCException("The message received from the reader has an invalid CRC should be:" + num3.ToString("X") + " is :" + num4.ToString("X"));
                }
            }
        }

        public static int UnpackMessage(byte[] byOut, byte[] byIn, int len)
        {
            int num = 0;
            byte num2 = 0;
            int index = 0;
            int num4 = 0;
            while (len-- > 0)
            {
                byte num5 = byIn[index];
                if (num5 == 0x10)
                {
                    num2 = 0x80;
                }
                else
                {
                    byOut[num4++] = (byte) (byIn[index] ^ num2);
                    num++;
                    num2 = 0;
                }
                index++;
            }
            if (num2 != 0)
            {
                return 0;
            }
            return num;
        }

        public byte[] Buffer
        {
            get
            {
                return this.m_Bytes.ToArray();
            }
        }

        public int BufferLength
        {
            get
            {
                return this.m_Bytes.Count;
            }
        }

        internal ByteBufferArray Bytes
        {
            get
            {
                return this.m_Bytes;
            }
            set
            {
                this.m_Bytes = value;
            }
        }

        public CommandRequest Command
        {
            get
            {
                if (!this.FullMessage)
                {
                    throw new InvalidOperationException("Full message required for operation.");
                }
                return (CommandRequest) this.m_Bytes[2];
            }
        }

        public int DaisyChainAddress
        {
            get
            {
                if (!this.FullMessage)
                {
                    throw new InvalidOperationException("Full message required for operation.");
                }
                return this.m_Bytes[1];
            }
        }

        public int EndOfTransmissionIndex
        {
            get
            {
                return this.m_indexEOT;
            }
            set
            {
                this.m_indexEOT = value;
            }
        }

        public bool FullMessage
        {
            get
            {
                return ((this.m_indexEOT != -1) && (this.m_indexSOH != -1));
            }
        }

        public GetParameterSubCmd GetParam
        {
            get
            {
                if (!this.FullMessage)
                {
                    throw new InvalidOperationException("Full message required for operation.");
                }
                return (GetParameterSubCmd) this.m_Bytes[3];
            }
        }

        public bool IsSOHAtStartOfBuffer
        {
            get
            {
                return (this.StartOfHeaderIndex == 0);
            }
        }

        public bool IsUnpacked
        {
            get
            {
                return this.m_bUnpacked;
            }
        }

        public byte this[int index]
        {
            get
            {
                return this.m_Bytes[index];
            }
            set
            {
                this.m_Bytes[index] = value;
            }
        }

        public int MaxLength
        {
            get
            {
                return this.m_nMaxLength;
            }
            set
            {
                this.m_nMaxLength = value;
            }
        }

        public int MessageBodyLength
        {
            get
            {
                if (this.FullMessage)
                {
                    return (this.MessageLength - 4);
                }
                return 0;
            }
        }

        public int MessageLength
        {
            get
            {
                if (this.FullMessage)
                {
                    return this.BufferLength;
                }
                return 0;
            }
        }

        public SetParamSubCmd SetParam
        {
            get
            {
                if (!this.FullMessage)
                {
                    throw new InvalidOperationException("Full message required for operation.");
                }
                return (SetParamSubCmd) this.m_Bytes[3];
            }
        }

        public int StartOfHeaderIndex
        {
            get
            {
                return this.m_indexSOH;
            }
            set
            {
                this.m_indexSOH = value;
            }
        }

        public enum CommandRequest : byte
        {
            GetParameter = 0x44,
            GetTags = 0x41,
            GetTagsExtended = 0x42,
            ReadVersion = 0x33,
            SetParameter = 0x43
        }

        public enum GetParameterSubCmd : byte
        {
            GetChecksumStatus = 3,
            GetDeviceAddress = 0x11,
            GetExternalSupplyVoltage = 5,
            GetHFStats = 8,
            GetInhibitTime = 0x12,
            GetLimitTagTx = 0x16,
            GetListBehaviour = 20,
            GetReReportingInterval = 0x13,
            GetRFReferenceLevel = 0x1a,
            GetRFSens = 0x19,
            GetRxLevelForTagEntry = 0x17,
            GetRxLevelForTagExit = 0x18,
            GetSerialNumber = 1,
            GetSlavePortStatus = 0x10,
            GetStatus = 4,
            GetTemperature = 6,
            GetUpTime = 2,
            Reserved = 0x15
        }

        public enum SetParamSubCmd : byte
        {
            ConnectSlavePort = 0x10,
            NumTagsToTransmit = 0x16,
            RfReferenceLevel = 0x1a,
            RxLevelForTagEntry = 0x17,
            RxLevelForTagExit = 0x18,
            RxSensitivity = 0x19,
            SetDeviceAddress = 0x11,
            SetInhibitTime = 0x12,
            SetReReportingInterval = 0x13,
            TagListBehavior = 20,
            TagListCommand = 0x15
        }
    }
}

