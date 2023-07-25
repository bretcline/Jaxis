namespace IDENTEC.Readers.BeaconReaders
{
    using IDENTEC;
    using IDENTEC.Readers;
    using IDENTEC.Tags;
    using System;

    public class iCardCFB : Reader, IBeaconReader, IDisposable, IBusDevice
    {
        private iPortR2 m_r2;

        public iCardCFB()
        {
            this.m_r2 = new iPortR2(new iBusAdapter(new SerialPortStream(1)));
        }

        public iCardCFB(iBusAdapter iBus)
        {
            this.m_r2 = new iPortR2(iBus);
        }

        public bool ClearTagList()
        {
            return this.m_r2.ClearTagList();
        }

        public void Connect()
        {
            if ((this.m_r2.DataStream != null) && this.m_r2.DataStream.IsOpen)
            {
                this.InitializeConnection();
            }
            else
            {
                int commPort = CFReaderSearch.FindReaderComPort();
                if (commPort < 1)
                {
                    throw new InvalidOperationException("The card is not inserted in the host");
                }
                this.Connect(commPort);
            }
        }

        public void Connect(int commPort)
        {
            this.m_r2.DataStream = new SerialPortStream(commPort);
            this.m_r2.DataStream.Open();
            this.InitializeConnection();
        }

        public void ConnectSlavePort(bool enable)
        {
            this.m_r2.ConnectSlavePort(enable);
        }

        public override bool Disconnect()
        {
            this.m_r2.DataStream.Close();
            return true;
        }

        public void Dispose()
        {
            if (this.m_r2.DataStream != null)
            {
                this.m_r2.DataStream.Close();
                this.m_r2.DataStream = null;
            }
        }

        public bool EnableHighRfSensitivity(bool enable)
        {
            return this.m_r2.EnableHighRfSensitivity(enable);
        }

        public byte GetBusAddress()
        {
            return this.m_r2.GetBusAddress();
        }

        public int GetMaxTagReported()
        {
            return this.m_r2.GetMaxTagReported();
        }

        public int GetPowerSupplyVoltage()
        {
            return this.m_r2.GetPowerSupplyVoltage();
        }

        public TagListBehavior GetTagListBehavior()
        {
            return this.m_r2.GetTagListBehavior();
        }

        public TimeSpan GetTagListInhibitTime()
        {
            return this.m_r2.GetTagListInhibitTime();
        }

        public TimeSpan GetTagReReportingInterval()
        {
            return this.m_r2.GetTagReReportingInterval();
        }

        public TagCollection GetTags(bool enableExtendedInfo)
        {
            return this.m_r2.GetTags(enableExtendedInfo);
        }

        public TagCollection GetTagsVariableDataLen()
        {
            return this.m_r2.GetTagsVariableDataLen();
        }

        public void Initialize()
        {
            this.m_r2.Initialize();
        }

        private void InitializeConnection()
        {
            this.m_r2.m_byAddress = 0;
            this.m_r2.GetUptime();
            this.m_r2.Initialize();
            if (string.IsNullOrEmpty(this.m_r2.Information))
            {
                this.Disconnect();
                throw new InvalidOperationException("The device is not a beacon reader");
            }
            if ((-1 == this.m_r2.Information.IndexOf("MCB")) && (-1 == this.m_r2.Information.IndexOf("R2")))
            {
                this.Disconnect();
                throw new InvalidOperationException("The device is not a beacon reader");
            }
        }

        public bool ResetParams()
        {
            if (this.m_r2.SetParameter(0, 1))
            {
                this.m_r2.SetBusAddress(0x31);
                return true;
            }
            return false;
        }

        public void ResetToFactoryDefault()
        {
            this.m_r2.SetParameter(0, 1);
        }

        public bool SetAllTagsInListAsNotYetReported()
        {
            return this.m_r2.SetAllTagsInListAsNotYetReported();
        }

        public void SetBusAddress(int address)
        {
            this.m_r2.SetBusAddress(address);
        }

        public bool SetDataLen(int len)
        {
            return this.m_r2.SetParameter(0x26, (uint) len);
        }

        public bool SetMaxTagReported(int MaxTag)
        {
            return this.m_r2.SetMaxTagReported(MaxTag);
        }

        public bool SetTagListBehavior(TagListBehavior mode)
        {
            return this.m_r2.SetTagListBehavior(mode);
        }

        public bool SetTagListInhibitTime(TimeSpan lifetimeInList)
        {
            return this.m_r2.SetTagListInhibitTime(lifetimeInList);
        }

        public bool SetTagReReportingInterval(TimeSpan interval)
        {
            return this.m_r2.SetTagReReportingInterval(interval);
        }

        public void SetTagSignalFilterLevel(int minsignal)
        {
            this.m_r2.SetTagSignalFilterLevel(minsignal);
        }

        public override string ToString()
        {
            return string.Format("{0}: Serial #: {1} Version {2}", this.Information, this.SerialNumber, this.FirmwareVersion);
        }

        public int Address
        {
            get
            {
                return this.m_r2.Address;
            }
        }

        public DateTime BootDateTime
        {
            get
            {
                return this.m_r2.BootDateTime;
            }
        }

        public override bool Connected
        {
            get
            {
                return this.m_r2.Connected;
            }
        }

        public IDENTEC.DataStream DataStream
        {
            get
            {
                return this.m_r2.DataStream;
            }
            set
            {
                this.m_r2.DataStream = value;
            }
        }

        public string FirmwareVersion
        {
            get
            {
                return (this.m_r2.MajorVersion.ToString() + "." + this.m_r2.m_nMinorVersion.ToString());
            }
        }

        public string Information
        {
            get
            {
                return this.m_r2.Information;
            }
        }

        public int MajorVersion
        {
            get
            {
                return this.m_r2.MajorVersion;
            }
        }

        public int MinorVersion
        {
            get
            {
                return this.m_r2.MinorVersion;
            }
        }

        public iPortR2 R2
        {
            get
            {
                return this.m_r2;
            }
        }

        public bool RxBoostEnabled
        {
            get
            {
                return this.m_r2.RxBoostEnabled;
            }
        }

        public string SerialNumber
        {
            get
            {
                return this.m_r2.SerialNumber;
            }
        }
    }
}

