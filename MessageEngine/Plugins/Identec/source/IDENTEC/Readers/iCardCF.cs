namespace IDENTEC.Readers
{
    using IDENTEC;
    using IDENTEC.Tags;
    using IDENTEC.Tags.DigitalInputLogging;
    using IDENTEC.Tags.Logging;
    using System;

    public class iCardCF : iCard, ITagReaderIQ, IDisposable, IBusDevice
    {
        private iCard3 m_iCard3;

        public iCardCF()
        {
            this.m_iCard3 = new iCard3();
        }

        public iCardCF(iBusAdapter iBus)
        {
            this.m_iCard3 = new iCard3();
            this.m_iCard3.m_comm.DataStream2007 = iBus.DataStream;
        }

        public bool BlinkTag(iQTag tag, int blinkCount)
        {
            return this.m_iCard3.BlinkTag(tag, blinkCount);
        }

        public void Connect()
        {
            int port = CFReaderSearch.FindReaderComPort();
            if (port < 1)
            {
                throw new InvalidOperationException("The card is not inserted in the host");
            }
            this.Connect(port);
        }

        public override bool Connect(int port)
        {
            if (!this.m_iCard3.ConnectRS232(port))
            {
                return false;
            }
            if (-1 != this.m_iCard3.Information.IndexOf("R2"))
            {
                this.m_iCard3.Disconnect();
                throw new InvalidOperationException("The device is a beacon reader");
            }
            return true;
        }

        public void ConnectSlavePort(bool enable)
        {
            this.EnableSlavePort(enable);
        }

        public override bool Disconnect()
        {
            return this.m_iCard3.Disconnect();
        }

        public void Dispose()
        {
            if (this.m_iCard3 != null)
            {
                this.m_iCard3.Dispose();
                this.m_iCard3 = null;
            }
        }

        internal bool EnableSlavePort(bool enable)
        {
            return this.SetParameter(0x10, enable ? 1 : 0);
        }

        public byte GetBusAddress()
        {
            this.GetStaticAddress();
            return this.m_iCard3.m_comm.m_byBusAddress;
        }

        [CLSCompliant(false)]
        public void GetParameter(byte bySubCmd, ref uint dwParameter)
        {
            dwParameter = 0;
            byte[] msg = new byte[0x10];
            msg[0] = this.m_iCard3.m_comm.m_byBusAddress;
            msg[1] = 0x44;
            msg[2] = bySubCmd;
            byte[] buffer2 = new byte[0x40];
            this.m_iCard3.m_comm.SendMessage(msg, 3);
            this.m_iCard3.m_comm.RecvMsg(buffer2, 500, 0);
            if (buffer2[1] != 0xc4)
            {
                throw new InvalidOperationException("The response contained the incorrect command");
            }
            dwParameter = (uint) ((((buffer2[3] << 0x18) + (buffer2[4] << 0x10)) + (buffer2[5] << 8)) + buffer2[6]);
        }

        internal void GetStaticAddress()
        {
            uint dwParameter = 0;
            this.GetParameter(0x11, ref dwParameter);
            this.m_iCard3.m_comm.m_byBusAddress = (byte) dwParameter;
        }

        public virtual void Initialize()
        {
            this.m_iCard3.ReadVersion();
            this.m_iCard3.ReadIntialParameters();
        }

        public bool PingTag(iQTag tag)
        {
            return this.m_iCard3.PingTag(tag);
        }

        public LoopData ReadiQLTagMarkerInfo(iQTag tag)
        {
            return this.m_iCard3.ReadiQLTagMarkerInfo(tag);
        }

        public TimeSpan ReadIQTagAbsoluteEngineHourCounter(iQTag tag)
        {
            return this.m_iCard3.ReadIQTagAbsoluteEngineHourCounter(tag);
        }

        public TimeSpan ReadIQTagUserEngineHourCounter(iQTag tag)
        {
            return this.m_iCard3.ReadIQTagUserEngineHourCounter(tag);
        }

        public iQTagVersionInfo ReadiQTagVersion(iQTag tag)
        {
            return this.m_iCard3.ReadiQTagVersion(tag);
        }

        public TemperatureLogSample ReadLastSampledTemperature(iQTag tag)
        {
            return this.m_iCard3.ReadTagLastRecordedTemperatureLogSample(tag);
        }

        public TemperatureLogSample ReadTagCurrentTemperature(iQTag tag)
        {
            return this.m_iCard3.ReadTagCurrentTemperature(tag);
        }

        public TagReadDataResult ReadTagData(iQTag tag, int address, int bytesToRead)
        {
            return this.m_iCard3.ReadTagData(tag, address, bytesToRead);
        }

        public TagReadStringResult ReadTagDataString(iQTag tag, int address)
        {
            return this.m_iCard3.ReadTagDataString(tag, address);
        }

        public TagReadDataResult ReadTagDataWithCRCAndLength(iQTag tag, int address)
        {
            return this.m_iCard3.ReadTagDataWithCRCAndLength(tag, address);
        }

        public DigitalInputLog ReadTagDigitalInputEventLog(iQTag tag)
        {
            return this.m_iCard3.ReadTagDigitalInputEventLog(tag);
        }

        public TemperatureLogData ReadTagLastnTemperatureLog(iQTag tag, int nbSamples)
        {
            return this.m_iCard3.ReadTagLastnTemperatureLog(tag, nbSamples);
        }

        public TimeSpan ReadTagLogSamplingInterval(iQTag tag)
        {
            return this.m_iCard3.ReadTagLogSamplingInterval(tag);
        }

        public RawLogData ReadTagRawLog(iQTag tag)
        {
            return this.m_iCard3.ReadTagRawLog(tag);
        }

        public TemperatureExtremes ReadTagTemperatureExtremes(iQTag tag)
        {
            return this.m_iCard3.ReadTagTemperatureExtremes(tag);
        }

        public TemperatureLogData ReadTagTemperatureLog(iQTag tag)
        {
            return this.m_iCard3.ReadTagTemperatureLog(tag);
        }

        public void ResetToFactoryDefault()
        {
            this.SetParameter(0, 1);
        }

        public TagCollection ScanForIQTags(int maxTagsThatCanRespond)
        {
            return this.m_iCard3.ScanForIQTags(maxTagsThatCanRespond);
        }

        public TagCollection ScanForIQTags(int maxTagsThatCanRespond, bool blink)
        {
            return this.m_iCard3.ScanForIQTags(maxTagsThatCanRespond, blink);
        }

        public void SetBusAddress(int address)
        {
            this.SetStaticAddress(address);
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
            msg[0] = this.m_iCard3.m_comm.m_byBusAddress;
            msg[1] = 0x43;
            msg[2] = bySubCmd;
            msg[3] = (byte) ((dwParameter >> 0x18) & 0xff);
            msg[4] = (byte) ((dwParameter >> 0x10) & 0xff);
            msg[5] = (byte) ((dwParameter >> 8) & 0xff);
            msg[6] = (byte) (dwParameter & 0xff);
            this.m_iCard3.m_comm.SendMessage(msg, 7);
            if (this.m_iCard3.m_comm.m_byBusAddress == 0xff)
            {
                return true;
            }
            if ((this.m_iCard3.m_comm.RecvMsg(buffer2, 500, 0) > 0) && (buffer2[1] == 0xc3))
            {
                this.m_iCard3.m_comm.m_byBusAddress = buffer2[0];
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
                this.m_iCard3.m_comm.m_byBusAddress = (byte) address;
                return true;
            }
            return false;
        }

        public bool SetTagRangeState(iQTag tag, bool enableExtendedRange)
        {
            return this.m_iCard3.SetTagRangeState(tag, enableExtendedRange);
        }

        public bool SleepTag(iQTag tag, int seconds)
        {
            return this.m_iCard3.SleepTag(tag, seconds);
        }

        public void StartTagDigitalInputEventLog(iQTag tag)
        {
            this.StartTagDigitalInputEventLog(tag, false);
        }

        public void StartTagDigitalInputEventLog(iQTag tag, bool synchronize)
        {
            this.m_iCard3.StartTagDigitalInputEventLog(tag, synchronize);
        }

        public void StartTagLogging(iQTag tag, TimeSpan samplingRate)
        {
            this.m_iCard3.StartTagLogging(tag, samplingRate);
        }

        public void StopTagLogging(iQTag tag)
        {
            this.m_iCard3.StopTagLogging(tag);
        }

        public override void TestCommunications()
        {
            this.m_iCard3.TestCommunications();
        }

        public override string ToString()
        {
            return "i-CARD CF 'Q'";
        }

        public void WriteIQTagUserEngineHourCounter(iQTag tag, TimeSpan ts)
        {
            this.m_iCard3.WriteIQTagUserEngineHourCounter(tag, ts);
        }

        public TagWriteDataResult WriteTagData(iQTag tag, int address, byte[] byData, int bytesToWrite)
        {
            return this.m_iCard3.WriteTagData(tag, address, byData, bytesToWrite);
        }

        public TagWriteDataResult WriteTagDataString(iQTag tag, int address, string text)
        {
            return this.m_iCard3.WriteTagDataString(tag, address, text);
        }

        public TagWriteDataResult WriteTagDataWithCRCAndLength(iQTag tag, int address, byte[] byData, int bytesToWrite)
        {
            return this.m_iCard3.WriteTagDataWithCRCAndLength(tag, address, byData, bytesToWrite);
        }

        public int Address
        {
            get
            {
                return this.m_iCard3.m_comm.m_byBusAddress;
            }
        }

        public override bool Connected
        {
            get
            {
                return this.m_iCard3.Connected;
            }
        }

        public IDENTEC.DataStream DataStream
        {
            get
            {
                return this.m_iCard3.m_comm.DataStream2007;
            }
            set
            {
                this.m_iCard3.m_comm.DataStream2007 = value;
            }
        }

        public override iCard.DeviceCode DeviceStatus
        {
            get
            {
                return this.m_iCard3.DeviceStatus;
            }
        }

        public bool EnableChangeICSessionAddressDuringScan
        {
            get
            {
                return this.m_iCard3.EnableChangeICSessionAddressDuringScan;
            }
            set
            {
                this.m_iCard3.EnableChangeICSessionAddressDuringScan = value;
            }
        }

        public bool EnableReceiveBoost
        {
            get
            {
                return this.m_iCard3.EnableReceiveBoost;
            }
            set
            {
                this.m_iCard3.EnableReceiveBoost = value;
            }
        }

        public bool EnableTagLogDataProtection
        {
            get
            {
                return this.m_iCard3.EnableTagLogDataProtection;
            }
            set
            {
                this.m_iCard3.EnableTagLogDataProtection = value;
            }
        }

        public bool EnableWakeupTagsDuringScan
        {
            get
            {
                return this.m_iCard3.EnableWakeupTagsDuringScan;
            }
            set
            {
                this.m_iCard3.EnableWakeupTagsDuringScan = value;
            }
        }

        public virtual string FirmwareVersion
        {
            get
            {
                return (this.MajorVersion.ToString() + "." + this.MinorVersion.ToString().PadLeft(2, '0'));
            }
        }

        public override string Information
        {
            get
            {
                return this.m_iCard3.Information;
            }
        }

        public int MajorVersion
        {
            get
            {
                return this.m_iCard3.m_nMajorVersion;
            }
        }

        public int MaxOutputdBm
        {
            get
            {
                return this.m_iCard3.MaxOutputdBmIQ;
            }
        }

        public int MinorVersion
        {
            get
            {
                return this.m_iCard3.m_nMinorVersion;
            }
        }

        public int MinOutputdBm
        {
            get
            {
                return -20;
            }
        }

        public static int MinReceivedBm
        {
            get
            {
                return -128;
            }
        }

        public override Frequency Region
        {
            get
            {
                return this.m_iCard3.Region;
            }
            set
            {
                this.m_iCard3.Region = value;
            }
        }

        public int Retries
        {
            get
            {
                return this.m_iCard3.Retries;
            }
            set
            {
                this.m_iCard3.Retries = value;
            }
        }

        public override string SerialNumber
        {
            get
            {
                return this.m_iCard3.SerialNumber;
            }
        }

        public int TxPowerIQ
        {
            get
            {
                return this.m_iCard3.TxPowerIQ;
            }
            set
            {
                this.m_iCard3.TxPowerIQ = value;
            }
        }
    }
}

