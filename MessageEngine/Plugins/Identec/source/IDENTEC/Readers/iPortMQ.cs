namespace IDENTEC.Readers
{
    using IDENTEC;
    using IDENTEC.Tags;
    using IDENTEC.Tags.DigitalInputLogging;
    using IDENTEC.Tags.Logging;
    using System;

    public class iPortMQ : ModularReader, ITagReaderIQ, IBusDevice
    {
        private iCard3 m_iCard3;

        public iPortMQ(iBusAdapter iBus) : base(iBus)
        {
            this.m_iCard3 = new iCard3();
            this.m_iCard3.m_comm.DataStream2007 = iBus.DataStream;
            base._iBus = iBus;
        }

        [Obsolete("This constructor is now obsolete. Please use the 'iPortMQ(iBusAdapter iBus)' constructor instead.", false)]
        public iPortMQ(ModularReaderBus comm) : base(comm)
        {
            this.m_iCard3 = new iCard3();
            this.m_iCard3.m_comm = comm.m_isolProtocolFramer;
        }

        public bool BlinkTag(iQTag tag, int blinkCount)
        {
            this.SetComBusAddress();
            return this.m_iCard3.BlinkTag(tag, blinkCount);
        }

        public void ConnectSlavePort(bool enable)
        {
            base.EnableSlavePort(enable);
        }

        public byte GetBusAddress()
        {
            base.GetStaticAddress();
            return base.m_byAddress;
        }

        public iBusDeviceStatus GetStatus()
        {
            uint dwParameter = 0;
            base.GetParameter(4, ref dwParameter);
            return new iBusDeviceStatus(dwParameter);
        }

        public override void Initialize()
        {
            this.SetComBusAddress();
            this.m_iCard3.ReadIntialParameters();
            if (this.m_iCard3.m_FormFactor != iCard3.FormFactor.T2)
            {
                throw new ApplicationException("The device is not an i-Q reader");
            }
            base.Initialize();
        }

        public bool PingTag(iQTag tag)
        {
            this.SetComBusAddress();
            return this.m_iCard3.PingTag(tag);
        }

        internal override void ReadGetReaderInformation()
        {
            byte[] msg = new byte[0x100];
            msg[0] = base.m_byAddress;
            msg[1] = 0x60;
            this.SendMessage(msg, 2);
            msg[0] = 0;
            msg[1] = 0;
            int num = this.RecvMsg(msg, 10);
            if (((num > 0) && (msg[0] == base.m_byAddress)) && ((msg[1] == 0xe0) && (num >= 4)))
            {
                switch (msg[5])
                {
                    case 1:
                        this.m_iCard3.m_Freq = Frequency.European;
                        this.m_iCard3.m_WorkingRegion = Reader.CompatibleRegion.EuropeanOnly;
                        return;

                    case 2:
                        this.m_iCard3.m_Freq = Frequency.NorthAmerican;
                        this.m_iCard3.m_WorkingRegion = Reader.CompatibleRegion.NorthAmericanOnly;
                        return;

                    case 3:
                        this.m_iCard3.m_WorkingRegion = Reader.CompatibleRegion.All;
                        return;
                }
                this.m_iCard3.m_Freq = Frequency.Indeterminate;
            }
        }

        public LoopData ReadiQLTagMarkerInfo(iQTag tag)
        {
            this.SetComBusAddress();
            return this.m_iCard3.ReadiQLTagMarkerInfo(tag);
        }

        public TimeSpan ReadIQTagAbsoluteEngineHourCounter(iQTag tag)
        {
            this.SetComBusAddress();
            return this.m_iCard3.ReadIQTagAbsoluteEngineHourCounter(tag);
        }

        public TimeSpan ReadIQTagUserEngineHourCounter(iQTag tag)
        {
            this.SetComBusAddress();
            return this.m_iCard3.ReadIQTagUserEngineHourCounter(tag);
        }

        public iQTagVersionInfo ReadiQTagVersion(iQTag tag)
        {
            this.SetComBusAddress();
            return this.m_iCard3.ReadiQTagVersion(tag);
        }

        public TemperatureLogSample ReadLastSampledTemperature(iQTag tag)
        {
            this.SetComBusAddress();
            return this.m_iCard3.ReadTagLastRecordedTemperatureLogSample(tag);
        }

        public TemperatureLogSample ReadTagCurrentTemperature(iQTag tag)
        {
            this.SetComBusAddress();
            return this.m_iCard3.ReadTagCurrentTemperature(tag);
        }

        public TagReadDataResult ReadTagData(iQTag tag, int address, int bytesToRead)
        {
            this.SetComBusAddress();
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
            this.SetComBusAddress();
            return this.m_iCard3.ReadTagDigitalInputEventLog(tag);
        }

        public TemperatureLogData ReadTagLastnTemperatureLog(iQTag tag, int nbSample)
        {
            this.SetComBusAddress();
            return this.m_iCard3.ReadTagLastnTemperatureLog(tag, nbSample);
        }

        public TimeSpan ReadTagLogSamplingInterval(iQTag tag)
        {
            this.SetComBusAddress();
            return this.m_iCard3.ReadTagLogSamplingInterval(tag);
        }

        public RawLogData ReadTagRawLog(iQTag tag)
        {
            this.SetComBusAddress();
            return this.m_iCard3.ReadTagRawLog(tag);
        }

        public TemperatureExtremes ReadTagTemperatureExtremes(iQTag tag)
        {
            this.SetComBusAddress();
            return this.m_iCard3.ReadTagTemperatureExtremes(tag);
        }

        public TemperatureLogData ReadTagTemperatureLog(iQTag tag)
        {
            this.SetComBusAddress();
            return this.m_iCard3.ReadTagTemperatureLog(tag);
        }

        internal override void ReadVersion()
        {
        }

        public void ResetToFactoryDefault()
        {
            base.SetParameter(0, 1);
        }

        public TagCollection ScanForIQTags(int maxTagsThatCanRespond)
        {
            return this.m_iCard3.ScanForIQTags(maxTagsThatCanRespond);
        }

        public TagCollection ScanForIQTags(int maxTagsThatCanRespond, bool blink)
        {
            this.SetComBusAddress();
            return this.m_iCard3.ScanForIQTags(maxTagsThatCanRespond, blink);
        }

        public void SetBusAddress(int address)
        {
            base.SetStaticAddress(address);
        }

        private void SetComBusAddress()
        {
            this.m_iCard3.m_comm.m_byBusAddress = (byte) base.Address;
        }

        public bool SetTagRangeState(iQTag tag, bool enableExtendedRange)
        {
            this.SetComBusAddress();
            return this.m_iCard3.SetTagRangeState(tag, enableExtendedRange);
        }

        public bool SleepTag(iQTag tag, int seconds)
        {
            this.SetComBusAddress();
            return this.m_iCard3.SleepTag(tag, seconds);
        }

        public void StartTagDigitalInputEventLog(iQTag tag)
        {
            this.SetComBusAddress();
            this.m_iCard3.StartTagDigitalInputEventLog(tag, false);
        }

        public void StartTagDigitalInputEventLog(iQTag tag, bool synchronize)
        {
            this.SetComBusAddress();
            this.m_iCard3.StartTagDigitalInputEventLog(tag, synchronize);
        }

        public void StartTagLogging(iQTag tag, TimeSpan samplingRate)
        {
            this.SetComBusAddress();
            this.m_iCard3.StartTagLogging(tag, samplingRate);
        }

        public void StopTagLogging(iQTag tag)
        {
            this.SetComBusAddress();
            this.m_iCard3.StopTagLogging(tag);
        }

        public override string ToString()
        {
            return string.Format("{0}: Serial #: {1} Version {2}", this.Information, this.SerialNumber, this.FirmwareVersion);
        }

        public void WriteIQTagUserEngineHourCounter(iQTag tag, TimeSpan ts)
        {
            this.SetComBusAddress();
            this.m_iCard3.WriteIQTagUserEngineHourCounter(tag, ts);
        }

        public TagWriteDataResult WriteTagData(iQTag tag, int address, byte[] byData, int bytesToWrite)
        {
            this.SetComBusAddress();
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

        public int Antenna
        {
            get
            {
                return this.m_iCard3.Antenna;
            }
            set
            {
                this.m_iCard3.Antenna = value;
            }
        }

        public iCard.DeviceCode DeviceStatus
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

        public override string Information
        {
            get
            {
                return this.m_iCard3.Information;
            }
        }

        public int MaxOutputdBm
        {
            get
            {
                return this.m_iCard3.MaxOutputdBmIQ;
            }
        }

        public int MinOutputdBm
        {
            get
            {
                return -40;
            }
        }

        public static int MinReceivedBm
        {
            get
            {
                return -128;
            }
        }

        public Frequency Region
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
                if (value > this.m_iCard3.MaxOutputdBmIQ)
                {
                    throw new ArgumentOutOfRangeException("value");
                }
                if (value < this.MinOutputdBm)
                {
                    throw new ArgumentOutOfRangeException("value");
                }
                this.m_iCard3.m_nTxPowerIQTags = value;
            }
        }

        private enum ReaderType
        {
            MQ = 2,
            T2 = 1
        }
    }
}

