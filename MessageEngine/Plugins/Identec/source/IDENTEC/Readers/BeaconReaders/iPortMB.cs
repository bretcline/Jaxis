namespace IDENTEC.Readers.BeaconReaders
{
    using IDENTEC;
    using IDENTEC.Readers;
    using IDENTEC.Tags;
    using IDENTEC.Tags.BeaconTags;
    using System;
    using System.Collections;

    public class iPortMB : iPortR2
    {
        private Frequency _Region;
        private bool m_bEnableRxBoostAnt2;

        public iPortMB(iBusAdapter iBus) : base(iBus)
        {
            this._Region = Frequency.Indeterminate;
        }

        [Obsolete("This constructor is now obsolete. Please use the 'iPortMB(iBusAdapter iBus)' constructor instead.", false)]
        public iPortMB(ModularReaderBus comm) : base(comm)
        {
            this._Region = Frequency.Indeterminate;
        }

        [Obsolete("This constructor is now obsolete. Please use the 'iPortMB(iBusAdapter iBus)' constructor instead.", false)]
        public iPortMB(ModularReaderBus comm, iPortR2 r2) : base(comm)
        {
            this._Region = Frequency.Indeterminate;
            base.m_strInformation = r2.m_strInformation;
            base.m_nMajorVersion = r2.m_nMajorVersion;
            base.m_nMinorVersion = r2.m_nMinorVersion;
        }

        private void AntennaNumberValidation(int antenna)
        {
            if ((antenna > 2) || (antenna < 1))
            {
                throw new ArgumentOutOfRangeException("This device only supports 2 antennas");
            }
            if (this.AntennaCount < antenna)
            {
                throw new ArgumentOutOfRangeException(string.Format("The device only supports {0} antennas", this.AntennaCount));
            }
        }

        public bool EnableHighRfSensitivity(int antenna, bool enable)
        {
            this.AntennaNumberValidation(antenna);
            switch (antenna)
            {
                case 1:
                    return base.EnableHighRfSensitivity(enable);

                case 2:
                {
                    bool flag = base.SetParameter(0x22, enable ? 1 : 0);
                    if (flag)
                    {
                        this.m_bEnableRxBoostAnt2 = enable;
                    }
                    return flag;
                }
            }
            return false;
        }

        public int GetDataLen(int len)
        {
            uint dwParameter = 0;
            base.GetParameter(0x26, ref dwParameter);
            return (int) dwParameter;
        }

        private void GetEnableHighRfSensitivityAntenna2()
        {
            uint dwParameter = 0;
            base.GetParameter(0x22, ref dwParameter);
            this.m_bEnableRxBoostAnt2 = Convert.ToBoolean(dwParameter);
        }

        public Frequency GetRegion()
        {
            if (this.IsiPortR2)
            {
                throw new NotSupportedException("The reader does not support this operation");
            }
            uint dwParameter = 0;
            base.GetParameter(0x25, ref dwParameter);
            if (this.Information.ToUpper().IndexOf("CB") <= 0)
            {
                switch (dwParameter)
                {
                    case 1:
                        this._Region = Frequency.European;
                        goto Label_00AF;

                    case 2:
                        this._Region = Frequency.NorthAmerican;
                        goto Label_00AF;
                }
                this._Region = Frequency.Indeterminate;
            }
            else
            {
                switch (dwParameter)
                {
                    case 0:
                        this._Region = Frequency.European;
                        goto Label_00AF;

                    case 1:
                        this._Region = Frequency.NorthAmerican;
                        goto Label_00AF;

                    case 3:
                        this._Region = Frequency.Taiwanese;
                        goto Label_00AF;

                    case 4:
                        this._Region = Frequency.Indian;
                        goto Label_00AF;
                }
                this._Region = Frequency.Indeterminate;
            }
        Label_00AF:
            return this._Region;
        }

        public iBusDeviceStatus GetStatus()
        {
            if (this.IsiPortR2)
            {
                throw new NotSupportedException("The reader does not support this operation");
            }
            uint dwParameter = 0;
            base.GetParameter(4, ref dwParameter);
            return new iBusDeviceStatus(dwParameter);
        }

        public int GetTagSignalFilterLevel(int antenna)
        {
            this.AntennaNumberValidation(antenna);
            byte bySubCmd = 0x1b;
            if (antenna == 2)
            {
                bySubCmd = 0x20;
            }
            switch (antenna)
            {
                case 1:
                case 2:
                {
                    uint dwParameter = 0;
                    base.GetParameter(bySubCmd, ref dwParameter);
                    return (sbyte) dwParameter;
                }
            }
            throw new InvalidOperationException();
        }

        public TagCollection GetTagsOnSeparateAntennas()
        {
            base.m_tagsList = new TagCollection();
            uint dwParameter = 0;
            base.GetParameter(0x16, ref dwParameter);
            byte[] msg = new byte[0x10];
            msg[0] = base.m_byAddress;
            msg[1] = 0x40;
            msg[2] = 0;
            Exception rXException = null;
            DateTime now = DateTime.Now;
            ArrayList list = new ArrayList();
            lock (base.DataStream.LockObject)
            {
                try
                {
                    this.SendMessage(msg, 3);
                    now = DateTime.Now;
                }
                catch (iCardTimeoutException)
                {
                    throw;
                }
                list = base.RXReaderMessages(dwParameter, msg[1], ref rXException);
            }
            iPortR2.log.Trace("Received : " + list.Count + " messages");
            if (rXException != null)
            {
                iPortR2.log.Debug("RX exception, " + rXException.Message);
            }
            for (int i = 0; i < list.Count; i++)
            {
                byte[] buffer2 = (byte[]) list[i];
                uint id = BitConverter.ToUInt32(buffer2, 8);
                if (id == 0)
                {
                    break;
                }
                if (buffer2[1] != (msg[1] + 0x80))
                {
                    throw new InvalidOperationException("The response contained the incorrect command");
                }
                iB2Tag item = new iB2Tag(id);
                ArrayList list2 = new ArrayList(2);
                iB2AntennaDetection detection = base.ParseDetection(now, buffer2, 0x1c);
                if (detection.DetectionCount > 0)
                {
                    detection.m_AntennaNumber = 1;
                    list2.Add(detection);
                }
                if ((buffer2[3] == 0) || (buffer2[3] > 0x23))
                {
                    detection = base.ParseDetection(now, buffer2, 0x27);
                    if (detection.DetectionCount > 0)
                    {
                        detection.m_AntennaNumber = 2;
                        list2.Add(detection);
                    }
                }
                item.m_AnntennaInfo = new iB2AntennaDetection[list2.Count];
                item.AntennaSignals = new MultiAntennaSignals(2);
                for (int j = 0; j < list2.Count; j++)
                {
                    iB2AntennaDetection detection2 = list2[j] as iB2AntennaDetection;
                    item.m_AnntennaInfo[j] = detection2;
                    item.AntennaSignals[detection2.AntennaNumber] = detection2.m_Signal;
                }
                item.m_byData = new byte[9];
                Array.Copy(buffer2, 12, item.m_byData, 0, 9);
                item.m_flags = buffer2[0x15];
                item.m_nLowByteAgeCount = buffer2[7];
                item.m_nHighByteAgeCount = buffer2[6];
                item.ConfigureForSingleAntennaProperties();
                item.CreateLoopData();
                base.m_tagsList.Add(item);
            }
            if (rXException != null)
            {
                throw rXException;
            }
            return base.m_tagsList;
        }

        public override void Initialize()
        {
            base.Initialize();
            this.GetEnableHighRfSensitivityAntenna2();
            this.GetRegion();
        }

        public bool SetDataLen(int len)
        {
            return base.SetParameter(0x26, (uint) len);
        }

        public void SetTagSignalFilterLevelAntenna(int antenna, int minSignal)
        {
            this.AntennaNumberValidation(antenna);
            if (minSignal < -128)
            {
                throw new ArgumentOutOfRangeException();
            }
            byte bySubCmd = 0x1b;
            if (antenna == 2)
            {
                bySubCmd = 0x20;
            }
            switch (antenna)
            {
                case 1:
                case 2:
                {
                    sbyte num2 = (sbyte) minSignal;
                    uint dwParameter = (uint) num2;
                    base.SetParameter(bySubCmd, dwParameter);
                    return;
                }
            }
        }

        public override string ToString()
        {
            return string.Format("{0}: Serial #: {1} Version {2}", this.Information, this.SerialNumber, this.FirmwareVersion);
        }

        public int AntennaCount
        {
            get
            {
                if (string.IsNullOrEmpty(base.m_strInformation))
                {
                    return 0;
                }
                if (-1 < base.m_strInformation.IndexOf("R2"))
                {
                    return 1;
                }
                if (-1 < base.m_strInformation.IndexOf("MCB-1"))
                {
                    return 1;
                }
                if (-1 < base.m_strInformation.IndexOf("MB-1"))
                {
                    return 1;
                }
                return 2;
            }
        }

        internal bool IsiPortR2
        {
            get
            {
                if (0 > this.Information.IndexOf("i-PORT R2"))
                {
                    return false;
                }
                return true;
            }
        }

        public Frequency Region
        {
            get
            {
                return this._Region;
            }
            set
            {
                this._Region = value;
            }
        }

        public bool RxBoostEnabledAntenna2
        {
            get
            {
                return this.m_bEnableRxBoostAnt2;
            }
        }
    }
}

