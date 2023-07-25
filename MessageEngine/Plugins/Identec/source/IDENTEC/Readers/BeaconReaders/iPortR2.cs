namespace IDENTEC.Readers.BeaconReaders
{
    using IDENTEC;
    using IDENTEC.Readers;
    using IDENTEC.Tags;
    using IDENTEC.Tags.BeaconTags;
    using NLog;
    using System;
    using System.Collections;

    public class iPortR2 : ModularReader, IBeaconReader, IBusDevice
    {
        protected static Logger log = LogManager.GetLogger("iPortR2");
        protected bool m_bEnableRxBoost;
        internal TagCollection m_tagsList;

        public iPortR2(iBusAdapter iBus) : base(iBus)
        {
            this.m_tagsList = new TagCollection();
        }

        [Obsolete("This constructor is now obsolete. Please use the 'iPortR2(iBusAdapter iBus)' constructor instead.", false)]
        public iPortR2(ModularReaderBus comm) : base(comm)
        {
            this.m_tagsList = new TagCollection();
        }

        public bool ClearTagList()
        {
            return base.SetParameter(0x15, 0);
        }

        public void ConnectSlavePort(bool enable)
        {
            base.EnableSlavePort(enable);
        }

        public bool EnableHighRfSensitivity(bool enable)
        {
            bool flag = base.SetParameter(0x19, enable ? 1 : 0);
            if (flag)
            {
                this.m_bEnableRxBoost = enable;
            }
            return flag;
        }

        private void ForceReaderToRepeatLastResponse()
        {
            byte[] msg = new byte[0x10];
            msg[0] = base.m_byAddress;
            msg[1] = 0x36;
            lock (base.DataStream.LockObject)
            {
                this.SendMessage(msg, 2);
            }
        }

        public byte GetBusAddress()
        {
            uint dwParameter = 0;
            base.GetParameter(0x11, ref dwParameter);
            base.m_byAddress = (byte) dwParameter;
            return base.m_byAddress;
        }

        private void GetEnableHighRfSensitivity()
        {
            uint dwParameter = 0;
            base.GetParameter(0x19, ref dwParameter);
            this.m_bEnableRxBoost = Convert.ToBoolean(dwParameter);
        }

        public int GetMaxTagReported()
        {
            uint dwParameter = 0;
            base.GetParameter(0x16, ref dwParameter);
            return (int) dwParameter;
        }

        public TagListBehavior GetTagListBehavior()
        {
            uint dwParameter = 0;
            base.GetParameter(20, ref dwParameter);
            return (TagListBehavior) dwParameter;
        }

        public TimeSpan GetTagListInhibitTime()
        {
            uint dwParameter = 0;
            base.GetParameter(0x12, ref dwParameter);
            return new TimeSpan(0, 0, (int) dwParameter);
        }

        public TimeSpan GetTagReReportingInterval()
        {
            uint dwParameter = 0;
            base.GetParameter(0x13, ref dwParameter);
            return new TimeSpan(0, 0, (int) dwParameter);
        }

        public TagCollection GetTags(bool enableExtendedInfo)
        {
            uint dwParameter = 0;
            base.GetParameter(0x16, ref dwParameter);
            this.m_tagsList = new TagCollection();
            byte[] msg = new byte[0x10];
            msg[0] = base.m_byAddress;
            if (enableExtendedInfo)
            {
                msg[1] = 0x42;
            }
            else
            {
                msg[1] = 0x41;
            }
            msg[2] = 0;
            Exception rXException = null;
            ArrayList list = new ArrayList();
            lock (base.DataStream.LockObject)
            {
                try
                {
                    this.SendMessage(msg, 3);
                }
                catch (iCardTimeoutException)
                {
                    throw;
                }
                list = this.RXReaderMessages(dwParameter, msg[1], ref rXException);
            }
            log.Trace("Received : " + list.Count + " messages");
            if (rXException != null)
            {
                log.Debug("RX exception, " + rXException.Message, rXException);
            }
            for (int i = 0; i < list.Count; i++)
            {
                int startIndex = 5;
                int index = 0x13;
                int num6 = 4;
                int num7 = 3;
                if (enableExtendedInfo)
                {
                    startIndex = 8;
                    index = 0x24;
                    num6 = 7;
                    num7 = 6;
                }
                byte[] buffer2 = (byte[]) list[i];
                uint id = BitConverter.ToUInt32(buffer2, startIndex);
                if (id == 0)
                {
                    break;
                }
                if (buffer2[1] != (msg[1] + 0x80))
                {
                    throw new InvalidOperationException("The response contained the incorrect command");
                }
                sbyte num8 = (sbyte) buffer2[index];
                int signal = num8;
                iB2Tag item = new iB2Tag(id, DateTime.Now, signal);
                if (enableExtendedInfo)
                {
                    Array.Reverse(buffer2, 0x20, 4);
                    int num10 = BitConverter.ToInt32(buffer2, 0x20);
                    item.m_AnntennaInfo[0].m_LastSeen = DateTime.Now.AddSeconds((double) num10);
                    Array.Reverse(buffer2, 0x1c, 4);
                    num10 = BitConverter.ToInt32(buffer2, 0x1c);
                    item.m_AnntennaInfo[0].m_FirstSeen = DateTime.Now.AddSeconds((double) num10);
                    item.m_AnntennaInfo[0].m_DetectionCount = buffer2[0x26];
                    item.m_HFProtID = buffer2[4];
                    item.m_AnntennaInfo[0].m_SignalMax = (sbyte) buffer2[index + 1];
                }
                item.m_byData = new byte[9];
                Array.Copy(buffer2, startIndex + 4, item.m_byData, 0, 9);
                item.m_flags = buffer2[startIndex + 13];
                item.m_nLowByteAgeCount = buffer2[num6];
                item.m_nHighByteAgeCount = buffer2[num7];
                item.CreateLoopData();
                this.m_tagsList.Add(item);
            }
            if (rXException != null)
            {
                throw rXException;
            }
            return this.m_tagsList;
        }

        public int GetTagSignalFilterLevel()
        {
            uint dwParameter = 0;
            base.GetParameter(0x17, ref dwParameter);
            return (sbyte) dwParameter;
        }

        public TagCollection GetTagsVariableDataLen()
        {
            uint dwParameter = 0;
            base.GetParameter(0x16, ref dwParameter);
            this.m_tagsList = new TagCollection();
            byte[] msg = new byte[0x10];
            msg[0] = base.m_byAddress;
            msg[1] = 0x3f;
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
                list = this.RXReaderMessages(dwParameter, msg[1], ref rXException);
            }
            log.Trace("Received : " + list.Count + " messages");
            if (rXException != null)
            {
                log.Debug("RX exception, " + rXException.Message, rXException);
            }
            for (int i = 0; i < list.Count; i++)
            {
                byte[] buffer2 = (byte[]) list[i];
                if (buffer2[2] != 0)
                {
                    throw new InvalidOperationException("The reader responded with Invalid status:" + buffer2[2].ToString());
                }
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
                iB2AntennaDetection detection = this.ParseDetection(now, buffer2, 14);
                if (detection.DetectionCount > 0)
                {
                    detection.m_AntennaNumber = 1;
                    list2.Add(detection);
                }
                if ((buffer2[3] == 0) || (buffer2[3] > 0x19))
                {
                    detection = this.ParseDetection(now, buffer2, 0x19);
                    if (detection.DetectionCount > 0)
                    {
                        detection.m_AntennaNumber = 2;
                        list2.Add(detection);
                    }
                }
                item.m_AnntennaInfo = new iB2AntennaDetection[list2.Count];
                for (int j = 0; j < list2.Count; j++)
                {
                    item.m_AnntennaInfo[j] = list2[j] as iB2AntennaDetection;
                }
                int index = 0x19;
                if ((buffer2[3] == 0) || (buffer2[3] > 0x19))
                {
                    index += 11;
                }
                item.m_byData = new byte[buffer2[index]];
                Array.Copy(buffer2, index + 1, item.m_byData, 0, item.m_byData.Length);
                item.m_HFProtID = buffer2[4];
                item.m_nHighByteAgeCount = buffer2[6];
                item.m_nLowByteAgeCount = buffer2[7];
                item.m_flags = buffer2[12];
                item.ConfigureForSingleAntennaProperties();
                item.CreateLoopData();
                this.m_tagsList.Add(item);
            }
            if (rXException != null)
            {
                throw rXException;
            }
            return this.m_tagsList;
        }

        public override void Initialize()
        {
            base.Initialize();
            base.ReadSerialNumber();
            this.LimitNumberOfTagsDuringRequest(0);
            this.GetEnableHighRfSensitivity();
        }

        internal bool LimitNumberOfTagsDuringRequest(int maxTags)
        {
            return base.SetParameter(0x16, (uint) maxTags);
        }

        protected iB2AntennaDetection ParseDetection(DateTime time, byte[] buffer, int offset)
        {
            iB2AntennaDetection detection = new iB2AntennaDetection();
            Array.Reverse(buffer, offset, 4);
            detection.m_FirstSeen = time.AddSeconds((double) BitConverter.ToInt32(buffer, offset));
            offset += 4;
            Array.Reverse(buffer, offset, 4);
            detection.m_LastSeen = time.AddSeconds((double) BitConverter.ToInt32(buffer, offset));
            offset += 4;
            detection.m_Signal = (sbyte) buffer[offset++];
            detection.m_SignalMax = (sbyte) buffer[offset++];
            detection.m_DetectionCount = buffer[offset++];
            return detection;
        }

        public void ResetToFactoryDefault()
        {
            base.SetParameter(0, 1);
        }

        internal ArrayList RXReaderMessages(uint MaxMessages, byte Cmd, ref Exception RXException)
        {
            int num;
            byte[] msg = new byte[0x400];
            ArrayList list = new ArrayList();
            RXException = null;
        Label_0014:
            num = 0;
            try
            {
                num = this.RecvMsg(msg, 0);
            }
            catch (CRCException exception)
            {
                log.Debug(exception.Message + "\n\n" + exception.Buffer, exception);
                if (msg[2] != 0)
                {
                    goto Label_0014;
                }
                if (base.m_ReaderBus != null)
                {
                    base.m_ReaderBus.m_isolProtocolFramer.ClearReadBuffer();
                }
                return list;
            }
            catch (iCardTimeoutException exception2)
            {
                if (RXException == null)
                {
                    RXException = new ReaderTimeoutException(exception2.Message);
                }
            }
            catch (Exception exception3)
            {
                if (RXException == null)
                {
                    RXException = exception3;
                }
            }
            if (RXException == null)
            {
                if (msg[1] != (Cmd + 0x80))
                {
                    RXException = new InvalidOperationException("The response contained the incorrect command");
                    return list;
                }
                if (msg[2] != 0x10)
                {
                    byte[] destinationArray = new byte[num];
                    Array.Copy(msg, 0, destinationArray, 0, destinationArray.Length);
                    list.Add(destinationArray);
                    if (list.Count != MaxMessages)
                    {
                        goto Label_0014;
                    }
                }
            }
            return list;
        }

        public bool SetAllTagsInListAsNotYetReported()
        {
            return base.SetParameter(0x15, 1);
        }

        public void SetBusAddress(int address)
        {
            base.SetStaticAddress(address);
        }

        public bool SetFrequency(Frequency freq)
        {
            uint dwParameter = (uint) freq;
            return base.SetParameter(0x25, dwParameter);
        }

        public bool SetMaxTagReported(int MaxTag)
        {
            return base.SetParameter(0x16, (uint) MaxTag);
        }

        public bool SetTagListBehavior(TagListBehavior mode)
        {
            return base.SetParameter(20, (uint) mode);
        }

        public bool SetTagListInhibitTime(TimeSpan lifetimeInList)
        {
            uint totalSeconds = (uint) lifetimeInList.TotalSeconds;
            return base.SetParameter(0x12, totalSeconds);
        }

        public bool SetTagReReportingInterval(TimeSpan interval)
        {
            uint totalSeconds = (uint) interval.TotalSeconds;
            return base.SetParameter(0x13, totalSeconds);
        }

        public void SetTagSignalFilterLevel(int minSignal)
        {
            if (minSignal < -128)
            {
                throw new ArgumentOutOfRangeException();
            }
            sbyte num = (sbyte) minSignal;
            uint dwParameter = (uint) num;
            base.SetParameter(0x17, dwParameter);
        }

        public override string ToString()
        {
            return string.Format("{0}: Serial #: {1} Version {2}", this.Information, this.SerialNumber, this.FirmwareVersion);
        }

        public bool RxBoostEnabled
        {
            get
            {
                return this.m_bEnableRxBoost;
            }
        }
    }
}

