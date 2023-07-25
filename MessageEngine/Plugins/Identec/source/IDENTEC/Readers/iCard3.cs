namespace IDENTEC.Readers
{
    using IDENTEC;
    using IDENTEC.Tags;
    using IDENTEC.Tags.DigitalInputLogging;
    using IDENTEC.Tags.Logging;
    using NLog;
    using System;
    using System.Text;
    using System.Threading;

    public class iCard3 : iCard, IDisposable, ITagReaderIQ, ITagReaderID2
    {
        protected static Logger log = LogManager.GetLogger("iCard3");
        private bool m_bBlinkiD2Tags;
        private bool m_bChangeICSessionAddressDuringScan;
        internal bool m_bRAM;
        private bool m_bRxBoostOn;
        private bool m_bTemperatureTagDataProtectionEnabled;
        private bool m_bWakeUpTagsDuringScan;
        internal ISolProtocolFramer m_comm;
        private CommandCode m_Command;
        private DateTime m_dtLastScan;
        internal FormFactor m_FormFactor;
        private ResponseTag.TAG_TYPE m_LastScanType;
        private int m_nCurrentAntenna;
        private int m_nLastTickCountDuringWakeUp;
        private int m_nMaxTransmissionID;
        private int m_nMaxTransmissionIQ;
        private int m_nRetries;
        private int m_nTxPowerIDTags;
        internal int m_nTxPowerIQTags;

        public iCard3()
        {
            this.m_nMaxTransmissionID = 30;
            this.m_nMaxTransmissionIQ = 6;
            this.m_nLastTickCountDuringWakeUp = Environment.TickCount - 500;
            this.m_nRetries = 5;
            this.m_bBlinkiD2Tags = true;
            this.m_comm = new ISolProtocolFramer2();
            base.m_Freq = Frequency.NorthAmerican;
            this.m_bRxBoostOn = true;
            this.m_nCurrentAntenna = 1;
            this.m_bWakeUpTagsDuringScan = true;
            this.m_bTemperatureTagDataProtectionEnabled = true;
            this.Retries = 3;
            this.m_bChangeICSessionAddressDuringScan = true;
            base.m_deviceCode = iCard.DeviceCode.OK;
        }

        internal iCard3(iBusAdapter iBus)
        {
            this.m_nMaxTransmissionID = 30;
            this.m_nMaxTransmissionIQ = 6;
            this.m_nLastTickCountDuringWakeUp = Environment.TickCount - 500;
            this.m_nRetries = 5;
            this.m_bBlinkiD2Tags = true;
            this.m_comm = new ISolProtocolFramer2();
            base.m_Freq = Frequency.NorthAmerican;
            this.m_bRxBoostOn = true;
            this.m_nCurrentAntenna = 1;
            this.m_bWakeUpTagsDuringScan = true;
            this.m_bTemperatureTagDataProtectionEnabled = true;
            this.Retries = 3;
            this.m_bChangeICSessionAddressDuringScan = true;
            base.m_deviceCode = iCard.DeviceCode.OK;
            this.m_comm.DataStream2007 = iBus.DataStream;
        }

        public bool BlinkTag(iQTag tag, int blinkCount)
        {
            if (blinkCount > 0xff)
            {
                throw new ArgumentOutOfRangeException("blinkCount");
            }
            int nBytesWritten = 0;
            byte[] byData = new byte[] { (byte) blinkCount };
            return this.WriteTagRAM(tag, 0x13, byData, 1, ref nBytesWritten);
        }

        public bool BlinkTag(iD2Tag tag, TimeSpan LEDOn, TimeSpan LEDOff, int blinkCount)
        {
            byte[] byData = iD2Tag.CreateMultiBlinkBuffer(ref LEDOn, ref LEDOff, blinkCount);
            return this.WriteTagDataPrivate(tag, iD2Tag.VIRTUAL_EEPROM_ADDRESS_BLINK, byData, byData.Length, 1).Success;
        }

        public bool Connect()
        {
            return this.Connect(1);
        }

        public override bool Connect(int port)
        {
            if (this.m_comm.ConnectPCMCIA(port))
            {
                this.ReadIntialParameters();
                return true;
            }
            return false;
        }

        public bool ConnectRS232(int port)
        {
            if (!this.m_comm.ConnectSerialPort(port))
            {
                return false;
            }
            this.ReadIntialParameters();
            return true;
        }

        public override bool Disconnect()
        {
            return this.m_comm.Disconnect();
        }

        public void Dispose()
        {
            if (this.m_comm != null)
            {
                this.m_comm.Dispose();
                this.m_comm = null;
            }
        }

        public TagCollection EchoLastScan()
        {
            TagCollection tags = null;
            byte[] msg = new byte[0x10];
            msg[0] = this.m_comm.m_byBusAddress;
            msg[1] = 0x36;
            this.m_comm.SendMessage(msg, 2);
            if (this.m_Command == CommandCode.Scan)
            {
                tags = this.ScanForTagsHelper(0x40, this.m_LastScanType, false);
                foreach (Tag tag in tags)
                {
                    tag.m_dt = this.m_dtLastScan;
                }
            }
            return tags;
        }

        public bool PingTag(iD2Tag tag)
        {
            return this.PingTagPrivate(tag);
        }

        public bool PingTag(iQTag tag)
        {
            return this.PingTagPrivate(tag);
        }

        internal bool PingTag(ResponseTag tag, byte byMode)
        {
            base.m_deviceCode = iCard.DeviceCode.OK;
            tag.AntennaSignals.Invalidate();
            byte[] destinationArray = new byte[290];
            destinationArray[0] = this.m_comm.m_byBusAddress;
            destinationArray[1] = 0x31;
            destinationArray[2] = (byte) ((this.m_nCurrentAntenna - 1) << 6);
            Frequency region = tag.Region;
            if (region != Frequency.Indeterminate)
            {
                base.RegionCompatibilityCheck(tag.Region);
            }
            else
            {
                region = this.Region;
            }
            destinationArray[2] = (byte) (destinationArray[2] | ((byte) (((int) region) << 3)));
            ResponseTag.TAG_TYPE iQ = ResponseTag.TAG_TYPE.IQ;
            if (tag is iD2Tag)
            {
                switch (region)
                {
                    case Frequency.European:
                        iQ = ResponseTag.TAG_TYPE.ID_INTERNATIONAL;
                        break;

                    case Frequency.NorthAmerican:
                        iQ = ResponseTag.TAG_TYPE.ID_NORTH_AMERICA;
                        break;
                }
            }
            destinationArray[2] = (byte) (destinationArray[2] | ((byte) (((byte) iQ) << 4)));
            if (this.m_bRxBoostOn)
            {
                destinationArray[2] = (byte) (destinationArray[2] | 4);
            }
            bool bWakeupIQTag = this.bWakeupIQTag;
            this.m_nLastTickCountDuringWakeUp = 0;
            if (bWakeupIQTag)
            {
                destinationArray[2] = (byte) (destinationArray[2] | 2);
            }
            if (iQ == ResponseTag.TAG_TYPE.IQ)
            {
                destinationArray[3] = (byte) this.m_nTxPowerIQTags;
            }
            else
            {
                destinationArray[3] = (byte) this.m_nTxPowerIDTags;
                if (this.m_bBlinkiD2Tags)
                {
                    byMode = (byte) (byMode | 1);
                }
                else
                {
                    byMode = (byte) (byMode & 0xfe);
                }
            }
            destinationArray[4] = byMode;
            destinationArray[5] = (byte) this.Retries;
            byte[] bytes = BitConverter.GetBytes(tag.Number);
            Array.Copy(bytes, 0, destinationArray, 6, bytes.Length);
            destinationArray[10] = 0;
            destinationArray[11] = 0;
            destinationArray[12] = 0;
            int num = 100;
            if (bWakeupIQTag && (iQ == ResponseTag.TAG_TYPE.IQ))
            {
                num += 150;
            }
            num *= this.Retries + 1;
            this.m_comm.SendMessage(destinationArray, 13);
            destinationArray.Initialize();
            this.m_comm.RecvMsg(destinationArray, 0x1388, tag.WaitForResponse(bWakeupIQTag));
            ResponseCode code = (ResponseCode) destinationArray[1];
            if (ResponseCode.ReadData != code)
            {
                throw new iCardCommunicationsException("The i-CARD responded with an incorrect response code");
            }
            if ((destinationArray[2] != 0) && (destinationArray[2] != 1))
            {
                base.m_deviceCode = (iCard.DeviceCode) (-320 - destinationArray[2]);
            }
            if (base.m_deviceCode != iCard.DeviceCode.OK)
            {
                throw new iCardCommunicationsException(string.Format("i-CARD returned an error: '{0}' and the message sent to the card was {1}", base.m_deviceCode.ToString(), this.m_comm.LastSentMessageAsHexString));
            }
            int num2 = (sbyte) destinationArray[3];
            this.m_nLastTickCountDuringWakeUp = Environment.TickCount;
            tag.Signal = num2;
            iQTag tag2 = tag as iQTag;
            if (tag2 != null)
            {
                if ((destinationArray[4] & 4) != 0)
                {
                    tag2.m_range = iQTag.RangeState.NormalRange;
                }
                else
                {
                    tag2.m_range = iQTag.RangeState.ExtendedRange;
                }
                tag2.SetModelTypeFromTagProtocol(destinationArray[4]);
                tag2.m_nPercentBatteryConsumed = destinationArray[6];
            }
            else
            {
                iD2Tag tag3 = tag as iD2Tag;
                if (tag3 != null)
                {
                    tag3.m_BattStatus = ((iD2Tag.BatteryStatus) destinationArray[5]) & iD2Tag.BatteryStatus.Good;
                }
            }
            tag.m_nVersion = destinationArray[4] >> 4;
            tag.m_byMode = destinationArray[4];
            tag.m_dt = DateTime.Now;
            this.m_nLastTickCountDuringWakeUp = Environment.TickCount;
            tag.Region = region;
            return true;
        }

        private bool PingTagPrivate(ResponseTag tag)
        {
            return this.PingTag(tag, 1);
        }

        private void PingTags(TagCollection aTags)
        {
            iCard.DeviceCode deviceCode = base.m_deviceCode;
            foreach (ResponseTag tag in aTags)
            {
                int signal = tag.Signal;
                try
                {
                    this.PingTagPrivate(tag);
                }
                catch (iCardCommunicationsException)
                {
                }
                if (tag.Signal == -128)
                {
                    tag.Signal = signal;
                }
            }
            base.m_deviceCode = deviceCode;
        }

        private bool ReadConfig(ushort wStartAddress, byte[] byBuffer, ushort wLength)
        {
            base.m_deviceCode = iCard.DeviceCode.OK;
            byte[] msg = new byte[320];
            msg[0] = this.m_comm.m_byBusAddress;
            msg[1] = 0x34;
            msg[2] = (byte) (wStartAddress % 0x100);
            msg[3] = (byte) (wStartAddress / 0x100);
            msg[4] = (byte) wLength;
            this.m_comm.SendMessage(msg, 5);
            int length = this.m_comm.RecvMsg(msg, 0x1388, 5);
            if (length <= 0)
            {
                return false;
            }
            if (length < 3)
            {
                return false;
            }
            if (msg[2] != 0)
            {
                base.m_deviceCode = (iCard.DeviceCode) (-320 - msg[2]);
                return false;
            }
            length -= 3;
            Array.Copy(msg, 3, byBuffer, 0, length);
            return true;
        }

        internal void ReadIntialParameters()
        {
            Thread.Sleep(50);
            this.ReadVersion();
            try
            {
                this.ReadMaxRFOutputAndRegion();
                this.ReadProductionInformation();
            }
            catch (iCardCommunicationsException)
            {
                this.m_comm.Disconnect();
                throw new iCardCommunicationsException("The device connected to the communications port is not an i-CARD.");
            }
        }

        public LoopData ReadiQLTagMarkerInfo(iQTag tag)
        {
            byte[] byData = new byte[10];
            int nBytesRead = 0;
            if (!this.ReadTagDataHelper(tag, 0x90, byData, 10, ref nBytesRead))
            {
                throw new PartialTagCommunicationsException("Could not read the tag. (" + base.m_deviceCode.ToString() + ")");
            }
            return LoopData.CreateLoopData(tag.Number, byData, tag.ContactTime);
        }

        public TimeSpan ReadIQTagAbsoluteEngineHourCounter(iQTag tag)
        {
            return EngineHourTagHelper.ReadIQTagAbsoluteEngineHourCounter(this, tag);
        }

        public TimeSpan ReadIQTagUserEngineHourCounter(iQTag tag)
        {
            return EngineHourTagHelper.ReadIQTagUserEngineHourCounter(this, tag);
        }

        public iQTagVersionInfo ReadiQTagVersion(iQTag tag)
        {
            iQTagVersionInfo info = new iQTagVersionInfo();
            byte[] byData = new byte[0x20];
            int nBytesRead = 0;
            if (this.ReadTagDataHelper(tag, 8, byData, byData.Length, ref nBytesRead))
            {
                info.PCBVersion = (byData[0] & 240) >> 4;
                info.AssemblingVersion = byData[0] & 15;
                info.SoftwareMajorVersion = (byData[1] & 240) >> 4;
                info.SoftwareMinorVersion = byData[1] & 15;
            }
            return info;
        }

        [Obsolete("This property will be removed in the future. Please use the 'ReadTagLastRecordedTemperatureLogSample' method instead.", false)]
        public TemperatureLogSample ReadLastSampledTemperature(iQTag tag)
        {
            return this.ReadTagLastRecordedTemperatureLogSample(tag);
        }

        private void ReadMaxRFOutputAndRegion()
        {
            byte[] byBuffer = new byte[0x20];
            switch (this.m_FormFactor)
            {
                case FormFactor.CompactFlash:
                    this.ReadConfig(13, byBuffer, 3);
                    this.m_nMaxTransmissionIQ = 0;
                    break;

                case FormFactor.T2:
                    this.m_nMaxTransmissionIQ = 6;
                    break;

                default:
                    this.ReadConfig(0x30, byBuffer, 3);
                    if (byBuffer[1] != 0xff)
                    {
                        this.m_nMaxTransmissionIQ = byBuffer[1];
                    }
                    if (byBuffer[2] != 0xff)
                    {
                        this.m_nMaxTransmissionID = byBuffer[2];
                    }
                    break;
            }
            switch (byBuffer[0])
            {
                case 0:
                    base.m_Freq = Frequency.European;
                    base.m_WorkingRegion = Reader.CompatibleRegion.EuropeanOnly;
                    break;

                case 1:
                    base.m_Freq = Frequency.NorthAmerican;
                    base.m_WorkingRegion = Reader.CompatibleRegion.NorthAmericanOnly;
                    break;

                default:
                    base.m_Freq = Frequency.NorthAmerican;
                    base.m_WorkingRegion = Reader.CompatibleRegion.All;
                    break;
            }
            this.m_nTxPowerIQTags = this.m_nMaxTransmissionIQ;
            this.m_nTxPowerIDTags = this.m_nMaxTransmissionID;
        }

        internal void ReadProductionInformation()
        {
            byte[] byBuffer = new byte[320];
            ushort wStartAddress = 2;
            switch (this.m_FormFactor)
            {
                case FormFactor.PCMCIA:
                    wStartAddress = 2;
                    break;

                case FormFactor.CompactFlash:
                    wStartAddress = 3;
                    break;

                case FormFactor.T2:
                    wStartAddress = 3;
                    break;
            }
            if (this.ReadConfig(wStartAddress, byBuffer, (ushort) byBuffer.Length) && (((byBuffer[0] != 0xff) || (byBuffer[1] != 0xff)) || ((byBuffer[2] != 0xff) || (byBuffer[3] != 0xff))))
            {
                string str = new string(Encoding.ASCII.GetString(byBuffer, 0, 10).ToCharArray());
                iCardProductionInformation information = new iCardProductionInformation();
                try
                {
                    information.nYear = 0x7d0 + int.Parse(str.Substring(0, 2));
                    information.nWeek = int.Parse(str.Substring(2, 2));
                    information.nProductionNumber = int.Parse(str.Substring(6, 4));
                    base.m_prod = information;
                    base.m_strSerialNumber = str;
                }
                catch (FormatException)
                {
                }
            }
        }

        public TemperatureLogSample ReadTagCurrentTemperature(iQTag tag)
        {
            TemperatureLogSample sample = new TemperatureLogSample();
            ushort num = 1;
            byte[] bytes = BitConverter.GetBytes(num);
            int nBytesWritten = 0;
            if (!this.WriteTagDataHelper(tag, 0xb0, bytes, 2, ref nBytesWritten))
            {
                throw new PartialTagCommunicationsException("Failed to write data to tag. (" + base.m_deviceCode.ToString() + ")");
            }
            iQTag.IsTemperatureTag(tag);
            Thread.Sleep(50);
            if (!this.ReadTagDataHelper(tag, 0xb0, bytes, 2, ref nBytesWritten))
            {
                throw new PartialTagCommunicationsException("Failed to read data from tag. (" + base.m_deviceCode.ToString() + ")");
            }
            short num3 = BitConverter.ToInt16(bytes, 0);
            sample.m_temperatureDegreesC = num3 * 0.01f;
            sample._time = DateTime.Now;
            return sample;
        }

        public TagReadDataResult ReadTagData(iD2Tag tag, int address, int bytesToRead)
        {
            return this.ReadTagDataPrivate(tag, address, bytesToRead, 5);
        }

        public TagReadDataResult ReadTagData(iQTag tag, int address, int bytesToRead)
        {
            int num;
            if (bytesToRead > 0x4e20)
            {
                num = 20;
            }
            else if (bytesToRead > 0x2710)
            {
                num = 15;
            }
            else if (bytesToRead > 0x1388)
            {
                num = 10;
            }
            else
            {
                num = 5;
            }
            return this.ReadTagDataPrivate(tag, address, bytesToRead, num);
        }

        internal bool ReadTagDataHelper(ResponseTag tag, int address, byte[] byData, int bytesToRead, ref int nBytesRead)
        {
            base.m_deviceCode = iCard.DeviceCode.OK;
            this.m_Command = CommandCode.ReadData;
            nBytesRead = 0;
            tag.AntennaSignals.Invalidate();
            iQTag tag2 = tag as iQTag;
            if (((tag2 != null) && (iQTag.Model.Indeterminate == tag2.m_Model)) && !this.PingTagPrivate(tag))
            {
                return false;
            }
            byte[] destinationArray = new byte[290];
            byte num = 1;
            destinationArray[0] = this.m_comm.m_byBusAddress;
            destinationArray[1] = 0x31;
            destinationArray[2] = (byte) ((this.m_nCurrentAntenna - 1) << 6);
            Frequency region = tag.Region;
            if (region != Frequency.Indeterminate)
            {
                base.RegionCompatibilityCheck(tag.Region);
            }
            else
            {
                region = this.Region;
            }
            destinationArray[2] = (byte) (destinationArray[2] | ((byte) (((int) region) << 3)));
            ResponseTag.TAG_TYPE iQ = ResponseTag.TAG_TYPE.IQ;
            if (tag is iD2Tag)
            {
                switch (region)
                {
                    case Frequency.European:
                        iQ = ResponseTag.TAG_TYPE.ID_INTERNATIONAL;
                        break;

                    case Frequency.NorthAmerican:
                        iQ = ResponseTag.TAG_TYPE.ID_NORTH_AMERICA;
                        break;
                }
            }
            destinationArray[2] = (byte) (destinationArray[2] | ((byte) (((byte) iQ) << 4)));
            if (this.m_bRxBoostOn)
            {
                destinationArray[2] = (byte) (destinationArray[2] | 4);
            }
            bool bWakeupIQTag = this.bWakeupIQTag;
            this.m_nLastTickCountDuringWakeUp = 0;
            if (bWakeupIQTag)
            {
                destinationArray[2] = (byte) (destinationArray[2] | 2);
            }
            if (this.m_bRAM)
            {
                destinationArray[2] = (byte) (destinationArray[2] | 1);
            }
            if (iQ == ResponseTag.TAG_TYPE.IQ)
            {
                destinationArray[3] = (byte) this.m_nTxPowerIQTags;
            }
            else
            {
                destinationArray[3] = (byte) this.m_nTxPowerIDTags;
                if (this.m_bBlinkiD2Tags)
                {
                    num = (byte) (num | 1);
                }
                else
                {
                    num = (byte) (num & 0xfe);
                }
            }
            destinationArray[4] = num;
            destinationArray[5] = (byte) this.Retries;
            byte[] bytes = BitConverter.GetBytes(tag.Number);
            Array.Copy(bytes, 0, destinationArray, 6, bytes.Length);
            destinationArray[10] = (byte) (address % 0x100);
            destinationArray[11] = (byte) (address / 0x100);
            destinationArray[12] = (byte) bytesToRead;
            int num2 = 100;
            if (iQ == ResponseTag.TAG_TYPE.IQ)
            {
                num2 += bytesToRead * 3;
                if (bWakeupIQTag)
                {
                    num2 += 100;
                }
            }
            num2 *= this.Retries + 1;
            this.m_comm.SendMessage(destinationArray, 13);
            int num3 = 0;
            num3 = this.m_comm.RecvMsg(destinationArray, 0x1388, tag.WaitForResponse(bWakeupIQTag));
            ResponseCode code = (ResponseCode) destinationArray[1];
            if (ResponseCode.ReadData != code)
            {
                throw new iCardCommunicationsException("The i-CARD responded with an incorrect response code: " + code.ToString() + " instead of 'Read Data'");
            }
            if ((destinationArray[2] != 0) && (destinationArray[2] != 1))
            {
                base.m_deviceCode = (iCard.DeviceCode) (-320 - destinationArray[2]);
            }
            if (base.m_deviceCode != iCard.DeviceCode.OK)
            {
                throw new iCardCommunicationsException(string.Format("i-CARD returned an error: '{0}' and the message sent to the card was {1}", base.m_deviceCode.ToString(), this.m_comm.LastSentMessageAsHexString));
            }
            int num4 = (sbyte) destinationArray[3];
            num3 -= 8;
            nBytesRead = num3;
            int length = bytesToRead;
            if (num3 < bytesToRead)
            {
                base.m_deviceCode = iCard.DeviceCode.TagPartialRead;
                length = num3;
            }
            if (num3 > bytesToRead)
            {
                length = bytesToRead;
                nBytesRead = length;
            }
            Array.Copy(destinationArray, 8, byData, 0, length);
            this.m_nLastTickCountDuringWakeUp = Environment.TickCount;
            tag.Signal = num4;
            tag.m_dt = DateTime.Now;
            tag.Region = region;
            tag.m_byMode = destinationArray[4];
            return (nBytesRead == bytesToRead);
        }

        private TagReadDataResult ReadTagDataPrivate(ResponseTag tag, int address, int bytesToRead, int Retries)
        {
            int destinationIndex = 0;
            TagReadDataResult result = new TagReadDataResult {
                Data = new byte[bytesToRead]
            };
            this.m_bRAM = false;
            destinationIndex = 0;
            iQTag tag2 = tag as iQTag;
            if (((tag2 == null) || this.m_bRAM) || ((iQTag.Model.Indeterminate != tag2.m_Model) || this.PingTagPrivate(tag)))
            {
                if ((address + bytesToRead) > tag.DataCapacity)
                {
                    throw new ArgumentOutOfRangeException("address");
                }
                int nBytesRead = 0;
                int num3 = 0;
                byte[] byData = new byte[0x80];
                while (bytesToRead > 0)
                {
                    if (bytesToRead > 0x80)
                    {
                        num3 = 0x80;
                    }
                    else
                    {
                        num3 = bytesToRead;
                    }
                    try
                    {
                        bool flag = this.ReadTagDataHelper(tag, address + destinationIndex, byData, num3, ref nBytesRead);
                        Array.Copy(byData, 0, result.Data, destinationIndex, nBytesRead);
                        destinationIndex += nBytesRead;
                        bytesToRead -= nBytesRead;
                        result.Success = flag;
                        result.BytesRead = destinationIndex;
                        result.StartAddress = address;
                        if (!flag)
                        {
                            Retries--;
                            if (Retries < 0)
                            {
                                return result;
                            }
                        }
                        continue;
                    }
                    catch (iCardCommunicationsException)
                    {
                        if ((base.m_deviceCode != iCard.DeviceCode.TagReadNoAcknowledge) && (base.m_deviceCode != iCard.DeviceCode.TagPartialRead))
                        {
                            throw;
                        }
                        Retries--;
                        if (Retries < 0)
                        {
                            throw;
                        }
                        continue;
                    }
                }
            }
            return result;
        }

        public TagReadStringResult ReadTagDataString(iD2Tag tag, int address)
        {
            return TagDataFormatter.ReadTagDataString(this, tag, address);
        }

        public TagReadStringResult ReadTagDataString(iQTag tag, int address)
        {
            return TagDataFormatter.ReadTagDataString(this, tag, address);
        }

        public TagReadDataResult ReadTagDataWithCRCAndLength(iD2Tag tag, int address)
        {
            return TagDataFormatter.ReadTagDataWithCRCAndLength(this, tag, address);
        }

        public TagReadDataResult ReadTagDataWithCRCAndLength(iQTag tag, int address)
        {
            return TagDataFormatter.ReadTagDataWithCRCAndLength(this, tag, address);
        }

        public DigitalInputLog ReadTagDigitalInputEventLog(iQTag tag)
        {
            DigitalInputLog log = new DigitalInputLog();
            byte[] byData = new byte[0x80];
            int nBytesRead = 0;
            if (!this.ReadTagRAM(tag, 0x7d, byData, 2, ref nBytesRead))
            {
                throw new PartialTagCommunicationsException("Failed to read tag RAM. (" + base.m_deviceCode.ToString() + ")");
            }
            ushort num2 = BitConverter.ToUInt16(byData, 0);
            TagReadDataResult result = this.ReadTagDataPrivate(tag, 0x17ad, 12, 5);
            if (!result.Success)
            {
                throw new PartialTagCommunicationsException("Failed to read tag log time information. (" + base.m_deviceCode.ToString() + ")");
            }
            ushort num3 = BitConverter.ToUInt16(result.Data, 0);
            if (result.Data[10] == 0)
            {
                log.m_bWrapped = true;
            }
            else
            {
                log.m_bWrapped = false;
            }
            log.m_dtStart = DateTimeConvertor.Convert_time_t(BitConverter.ToUInt32(result.Data, 2));
            log.m_dtEnd = DateTimeConvertor.Convert_time_t(BitConverter.ToUInt32(result.Data, 6));
            if (tag.Logging == iQTag.LoggingState.On)
            {
                log.m_dtEnd = DateTime.Now;
            }
            int bytesToRead = num2 - num3;
            if (num2 < 0x1800)
            {
                bytesToRead = 0;
            }
            else
            {
                bytesToRead += 4;
            }
            if (bytesToRead < 0)
            {
                bytesToRead += tag.DataCapacity - 0x1800;
            }
            if (log.m_bWrapped)
            {
                bytesToRead = tag.DataCapacity - 0x1800;
            }
            if (bytesToRead > 0)
            {
                log.m_byBuffer = new byte[bytesToRead];
                if (bytesToRead < (tag.DataCapacity - 0x1800))
                {
                    if ((num2 >= num3) || ((num2 - 0x1800) >= bytesToRead))
                    {
                        result = this.ReadTagDataPrivate(tag, (num2 + 4) - bytesToRead, bytesToRead, 10);
                        if (!result.Success)
                        {
                            throw new PartialTagCommunicationsException("Could not completely read the raw data. (" + base.m_deviceCode.ToString() + ")");
                        }
                        Array.Copy(result.Data, 0, log.m_byBuffer, 0, result.BytesRead);
                    }
                    else
                    {
                        int num5 = ((bytesToRead + 0x1800) - num2) - 4;
                        int destinationIndex = 0;
                        if (num5 != 0)
                        {
                            result = this.ReadTagDataPrivate(tag, tag.DataCapacity - num5, num5, 10);
                            if (!result.Success)
                            {
                                throw new PartialTagCommunicationsException("Could not completely read the raw data. (" + base.m_deviceCode.ToString() + ")");
                            }
                            Array.Copy(result.Data, 0, log.m_byBuffer, 0, result.BytesRead);
                            destinationIndex = result.BytesRead;
                        }
                        result = this.ReadTagDataPrivate(tag, 0x1800, bytesToRead - destinationIndex, 10);
                        if (!result.Success)
                        {
                            throw new PartialTagCommunicationsException("Could not completely read the raw data. (" + base.m_deviceCode.ToString() + ")");
                        }
                        Array.Copy(result.Data, 0, log.m_byBuffer, destinationIndex, result.BytesRead);
                    }
                }
                else
                {
                    int sourceIndex = (num2 + 4) - 0x1800;
                    result = this.ReadTagDataPrivate(tag, 0x1800, bytesToRead, 10);
                    if (!result.Success)
                    {
                        throw new PartialTagCommunicationsException("Could not completely read the raw data. (" + base.m_deviceCode.ToString() + ")");
                    }
                    if ((result.BytesRead - sourceIndex) < 0)
                    {
                        throw new PartialTagCommunicationsException("Log's integrity broken. (" + base.m_deviceCode.ToString() + ")");
                    }
                    Array.Copy(result.Data, sourceIndex, log.m_byBuffer, 0, result.BytesRead - sourceIndex);
                    Array.Copy(result.Data, 0, log.m_byBuffer, result.BytesRead - sourceIndex, sourceIndex);
                }
            }
            log.CreateInputSamples();
            return log;
        }

        public TemperatureLogData ReadTagLastnTemperatureLog(iQTag tag, int nbSample)
        {
            TemperatureLogData data = this.ReadTagRawLog(tag, true, true, nbSample) as TemperatureLogData;
            data.ConvertLogToCelsius(tag.ModelType);
            return data;
        }

        public RawLogSample ReadTagLastRecordedRawLogSample(iQTag tag)
        {
            TimeSpan span = this.ReadTagLogSamplingInterval(tag);
            if (tag.Logging != iQTag.LoggingState.On)
            {
                throw new InvalidTagOperationException("This operation is only valid for tags that are logging.");
            }
            byte[] byData = new byte[2];
            int nBytesRead = 0;
            if (!this.ReadTagRAM(tag, 0x77, byData, 2, ref nBytesRead))
            {
                throw new PartialTagCommunicationsException("Failed to read the last recorded log entry from tag RAM. (" + base.m_deviceCode.ToString() + ")");
            }
            RawLogSample sample = new RawLogSample {
                _wSample = BitConverter.ToUInt16(byData, 0)
            };
            if (!this.ReadTagRAM(tag, 0x7b, byData, 2, ref nBytesRead))
            {
                throw new PartialTagCommunicationsException("Failed to read the log counter from RAM. (" + base.m_deviceCode.ToString() + ")");
            }
            ushort num2 = BitConverter.ToUInt16(byData, 0);
            int seconds = ((int) span.TotalSeconds) - num2;
            TimeSpan span2 = new TimeSpan(0, 0, 0, seconds, 0);
            sample._time = DateTime.Now - span2;
            return sample;
        }

        public TemperatureLogSample ReadTagLastRecordedTemperatureLogSample(iQTag tag)
        {
            RawLogSample sample = this.ReadTagLastRecordedRawLogSample(tag);
            return new TemperatureLogSample { m_temperatureDegreesC = TemperatureLogData.ConvertRawToCelsius(sample.Sample, tag.ModelType) };
        }

        public TimeSpan ReadTagLogSamplingInterval(iQTag tag)
        {
            TimeSpan span = new TimeSpan();
            if (!this.PingTagPrivate(tag))
            {
                throw new PartialTagCommunicationsException("Failed to ping tag. (" + base.m_deviceCode.ToString() + ")");
            }
            iQTag.IsTemperatureTag(tag);
            TagReadDataResult result = this.ReadTagDataPrivate(tag, 0x80, 2, 5);
            if (result.Success)
            {
                ushort seconds = BitConverter.ToUInt16(result.Data, 0);
                if (0xffff == seconds)
                {
                    throw new ArgumentOutOfRangeException("The tag's sampling interval is corrupt");
                }
                if (tag.ReportsBatteryPercentConsumed && (1 == (seconds & 1)))
                {
                    seconds = (ushort) (seconds + 1);
                }
                span = new TimeSpan(0, 0, 0, seconds, 0);
            }
            return span;
        }

        internal bool ReadTagRAM(ResponseTag tag, int address, byte[] byData, int bytesToRead, ref int nBytesRead)
        {
            bool flag2;
            this.m_bRAM = true;
            bool flag = false;
            int num = 5;
        Label_000B:;
            try
            {
                try
                {
                    flag = this.ReadTagDataHelper(tag, address, byData, bytesToRead, ref nBytesRead);
                    this.m_bRAM = false;
                    return flag;
                }
                catch (iCardCommunicationsException)
                {
                    if ((base.m_deviceCode != iCard.DeviceCode.TagReadNoAcknowledge) && (base.m_deviceCode != iCard.DeviceCode.TagPartialRead))
                    {
                        throw;
                    }
                    num--;
                    if (num < 0)
                    {
                        this.m_bRAM = false;
                        throw;
                    }
                }
                goto Label_000B;
            }
            finally
            {
                this.m_bRAM = false;
            }
            return flag2;
        }

        public RawLogData ReadTagRawLog(iQTag tag)
        {
            return this.ReadTagRawLog(tag, true, false, 0);
        }

        internal RawLogData ReadTagRawLog(iQTag tag, bool bReadLogData, bool bTempLog, int nbSamples)
        {
            RawLogData data;
            if (!bTempLog)
            {
                data = new RawLogData();
            }
            else
            {
                data = new TemperatureLogData();
            }
            data.m_LogInfo.m_tsLogInterval = this.ReadTagLogSamplingInterval(tag);
            byte[] byData = new byte[0x80];
            int nBytesRead = 0;
            if (!this.ReadTagRAM(tag, 0x7d, byData, 2, ref nBytesRead))
            {
                throw new PartialTagCommunicationsException("Failed to read tag RAM. (" + base.m_deviceCode.ToString() + ")");
            }
            ushort num2 = BitConverter.ToUInt16(byData, 0);
            TagReadDataResult result = this.ReadTagDataPrivate(tag, 0x17ad, 12, 5);
            if (!result.Success)
            {
                throw new PartialTagCommunicationsException("Failed to read tag log time information. (" + base.m_deviceCode.ToString() + ")");
            }
            ushort address = BitConverter.ToUInt16(result.Data, 0);
            ushort num4 = 0;
            data.m_dtStart = DateTimeConvertor.Convert_time_t(BitConverter.ToUInt32(result.Data, 2));
            data.m_dtEnd = DateTimeConvertor.Convert_time_t(BitConverter.ToUInt32(result.Data, 6));
            if (address == 0)
            {
                data.m_wBuffer = new ushort[0];
                data.m_byBuffer = new byte[0];
                return data;
            }
            if (address < 0x17fe)
            {
                data.m_byBuffer = new byte[0];
                return data;
            }
            if (num2 < 0x17fe)
            {
                data.m_byBuffer = new byte[0];
                data.m_wBuffer = new ushort[0];
                return data;
            }
            if (address > tag.DataCapacity)
            {
                data.m_byBuffer = new byte[0];
                data.m_wBuffer = new ushort[0];
                return data;
            }
            if (num2 > tag.DataCapacity)
            {
                data.m_byBuffer = new byte[0];
                data.m_wBuffer = new ushort[0];
                return data;
            }
            if (tag.ModelType == iQTag.Model.IQ32Elpro)
            {
                if (result.Data[10] == 0)
                {
                    data.m_LogInfo.m_bWrapped = true;
                }
                else
                {
                    data.m_LogInfo.m_bWrapped = false;
                }
            }
            else
            {
                result = this.ReadTagDataPrivate(tag, address, 2, 2);
                if (!result.Success)
                {
                    throw new PartialTagCommunicationsException("Failed to read the first sample in the log. (" + base.m_deviceCode.ToString() + ")");
                }
                num4 = BitConverter.ToUInt16(result.Data, 0);
                if (0x1000 == num4)
                {
                    data.m_LogInfo.m_bWrapped = false;
                }
                else
                {
                    data.m_LogInfo.m_bWrapped = true;
                }
            }
            if (tag.Logging == iQTag.LoggingState.On)
            {
                if (!this.ReadTagRAM(tag, 0x7b, byData, 2, ref nBytesRead))
                {
                    throw new PartialTagCommunicationsException("Failed to read tag RAM. (" + base.m_deviceCode.ToString() + ")");
                }
                ushort num5 = BitConverter.ToUInt16(byData, 0);
                int num6 = (int) (data.m_LogInfo.m_tsLogInterval.TotalSeconds - num5);
                data.m_dtEnd = DateTime.Now.AddSeconds((double) (-1 * num6));
            }
            int num7 = 0;
            if (data.m_LogInfo.m_bWrapped)
            {
                num7 = (tag.DataCapacity - 0x1800) / 2;
                data.m_dtStart = data.m_dtEnd.AddSeconds(-1.0 * (data.m_LogInfo.m_tsLogInterval.TotalSeconds * (num7 - 1)));
            }
            else
            {
                if (num4 != 0x1000)
                {
                    data.m_dtStart.AddSeconds(data.m_LogInfo.m_tsLogInterval.TotalSeconds - 1.0);
                }
                if (num2 >= address)
                {
                    num7 = (num2 - address) / 2;
                }
                else
                {
                    num7 = ((tag.DataCapacity - address) + (num2 - 0x1800)) / 2;
                }
            }
            if (num7 == 0)
            {
                data.m_byBuffer = new byte[0];
                data.m_wBuffer = new ushort[0];
                return data;
            }
            if ((nbSamples > 0) && (nbSamples < num7))
            {
                num7 = nbSamples;
                data.m_dtStart = data.m_dtEnd.AddSeconds(-1.0 * (data.m_LogInfo.m_tsLogInterval.TotalSeconds * (num7 - 1)));
            }
            if (bReadLogData)
            {
                int bytesToRead = 0;
                data.m_byBuffer = new byte[num7 * 2];
                if (!data.m_LogInfo.m_bWrapped)
                {
                    if (num2 >= address)
                    {
                        bytesToRead = num7 * 2;
                        result = this.ReadTagDataPrivate(tag, (num2 - bytesToRead) + 2, bytesToRead, 10);
                        if (!result.Success)
                        {
                            throw new PartialTagCommunicationsException("Could not completely read the raw data. (" + base.m_deviceCode.ToString() + ")");
                        }
                        Array.Copy(result.Data, 0, data.m_byBuffer, 0, result.BytesRead);
                    }
                    else if ((num2 - 0x1800) >= (num7 * 2))
                    {
                        bytesToRead = num7 * 2;
                        result = this.ReadTagDataPrivate(tag, (num2 - bytesToRead) + 2, bytesToRead, 10);
                        if (!result.Success)
                        {
                            throw new PartialTagCommunicationsException("Could not completely read the raw data. (" + base.m_deviceCode.ToString() + ")");
                        }
                        Array.Copy(result.Data, 0, data.m_byBuffer, 0, result.BytesRead);
                    }
                    else
                    {
                        bytesToRead = (num7 * 2) - ((num2 - 0x1800) + 2);
                        int destinationIndex = 0;
                        if (bytesToRead != 0)
                        {
                            result = this.ReadTagDataPrivate(tag, tag.DataCapacity - bytesToRead, bytesToRead, 10);
                            if (!result.Success)
                            {
                                throw new PartialTagCommunicationsException("Could not completely read the raw data. (" + base.m_deviceCode.ToString() + ")");
                            }
                            Array.Copy(result.Data, 0, data.m_byBuffer, 0, result.BytesRead);
                            destinationIndex = result.BytesRead;
                        }
                        result = this.ReadTagDataPrivate(tag, 0x1800, (num7 * 2) - destinationIndex, 10);
                        if (!result.Success)
                        {
                            throw new PartialTagCommunicationsException("Could not completely read the raw data. (" + base.m_deviceCode.ToString() + ")");
                        }
                        Array.Copy(result.Data, 0, data.m_byBuffer, destinationIndex, result.BytesRead);
                    }
                }
                else if ((num2 - 0x1800) >= (num7 * 2))
                {
                    bytesToRead = num7 * 2;
                    result = this.ReadTagDataPrivate(tag, (num2 - bytesToRead) + 2, bytesToRead, 10);
                    if (!result.Success)
                    {
                        throw new PartialTagCommunicationsException("Could not completely read the raw data. (" + base.m_deviceCode.ToString() + ")");
                    }
                    Array.Copy(result.Data, 0, data.m_byBuffer, 0, result.BytesRead);
                }
                else
                {
                    bytesToRead = (num7 * 2) - ((num2 - 0x1800) + 2);
                    result = this.ReadTagDataPrivate(tag, tag.DataCapacity - bytesToRead, bytesToRead, 10);
                    if (!result.Success)
                    {
                        throw new PartialTagCommunicationsException("Could not completely read the raw data. (" + base.m_deviceCode.ToString() + ")");
                    }
                    Array.Copy(result.Data, 0, data.m_byBuffer, 0, result.BytesRead);
                    int bytesRead = result.BytesRead;
                    result = this.ReadTagDataPrivate(tag, 0x1800, (num7 * 2) - bytesRead, 10);
                    if (!result.Success)
                    {
                        throw new PartialTagCommunicationsException("Could not completely read the raw data. (" + base.m_deviceCode.ToString() + ")");
                    }
                    Array.Copy(result.Data, 0, data.m_byBuffer, bytesRead, result.BytesRead);
                }
            }
            data.ConvertRawBufferToWords();
            return data;
        }

        public TemperatureExtremes ReadTagTemperatureExtremes(iQTag tag)
        {
            TemperatureExtremes extremes = new TemperatureExtremes();
            RawLogData data = this.ReadTagRawLog(tag, false, false, 0);
            extremes.m_dtLogEnd = data.End;
            extremes.m_dtLogStart = data.Start;
            if (!tag.ReportsBatteryPercentConsumed)
            {
                throw new InvalidTagOperationException("The specified i-Q tag " + tag.Number.ToString() + " does not support temperature extremes");
            }
            TagReadDataResult result = this.ReadTagDataPrivate(tag, 0x17a0, 4, 5);
            if (!result.Success)
            {
                throw new PartialTagCommunicationsException("Could not read the tag (" + base.m_deviceCode.ToString() + ")");
            }
            extremes.m_bSuccess = true;
            extremes.m_MinimumDegreesCelsius = TemperatureLogData.ConvertRawToCelsius(BitConverter.ToUInt16(result.Data, 0), tag.ModelType);
            extremes.m_MaximumDegreesCelsius = TemperatureLogData.ConvertRawToCelsius(BitConverter.ToUInt16(result.Data, 2), tag.ModelType);
            return extremes;
        }

        public TemperatureLogData ReadTagTemperatureLog(iQTag tag)
        {
            TemperatureLogData data = this.ReadTagRawLog(tag, true, true, 0) as TemperatureLogData;
            data.ConvertLogToCelsius(tag.ModelType);
            return data;
        }

        internal void ReadVersion()
        {
            base.m_deviceCode = iCard.DeviceCode.OK;
            byte[] msg = new byte[0x180];
            msg[0] = this.m_comm.m_byBusAddress;
            msg[1] = 0x33;
            this.m_comm.SendMessage(msg, 2);
            msg[0] = 0;
            msg[1] = 0;
            int num = this.m_comm.RecvMsg(msg, 0x1388, 1);
            if ((num > 0) && ((msg[1] == 0xb3) && (num >= 4)))
            {
                base.m_nMajorVersion = msg[2];
                base.m_nMinorVersion = msg[3];
                base.m_strInformation = msg[2].ToString() + "." + msg[3].ToString() + " " + Encoding.UTF8.GetString(msg, 4, 20);
                base.m_strInformation = base.m_strInformation.Replace("\0", "");
                base.m_strInformation = base.m_strInformation.TrimEnd(null);
                if (-1 != base.m_strInformation.IndexOf("IB2"))
                {
                    this.Disconnect();
                    throw new iCardCommunicationsException("The device is actually an i-CARD R2 (beacon tag) reader");
                }
                if (-1 != base.m_strInformation.IndexOf("CF"))
                {
                    this.m_FormFactor = FormFactor.CompactFlash;
                }
                else if (-1 != base.m_strInformation.IndexOf("T2"))
                {
                    this.m_FormFactor = FormFactor.T2;
                }
                else if (-1 != base.m_strInformation.IndexOf("MQ"))
                {
                    this.m_FormFactor = FormFactor.T2;
                }
                else
                {
                    this.m_FormFactor = FormFactor.PCMCIA;
                }
            }
        }

        public TagCollection ScanForID2NATags(int maxTagsThatCanRespond)
        {
            return this.ScanForTagsHelper(maxTagsThatCanRespond, ResponseTag.TAG_TYPE.ID_NORTH_AMERICA, true);
        }

        public TagCollection ScanForID2NATags(int maxTagsThatCanRespond, bool blink)
        {
            if (this.Region != Frequency.NorthAmerican)
            {
                throw new RegionException("This command works only with North American (916.5MHz) frequency regions");
            }
            TagCollection aTags = this.ScanForID2Tags(maxTagsThatCanRespond);
            if (blink)
            {
                this.PingTags(aTags);
            }
            return aTags;
        }

        public TagCollection ScanForID2Tags(int maxTagsThatCanRespond)
        {
            return this.ScanForTagsHelper(maxTagsThatCanRespond, ResponseTag.TAG_TYPE.ID_INTERNATIONAL, true);
        }

        public TagCollection ScanForID2Tags(int maxTagsThatCanRespond, bool blink)
        {
            TagCollection aTags = this.ScanForID2Tags(maxTagsThatCanRespond);
            if (blink)
            {
                this.PingTags(aTags);
            }
            return aTags;
        }

        public TagCollection ScanForIQTags(int maxTagsThatCanRespond)
        {
            return this.ScanForTagsHelper(maxTagsThatCanRespond, ResponseTag.TAG_TYPE.IQ, true);
        }

        public TagCollection ScanForIQTags(int maxTagsThatCanRespond, bool blink)
        {
            TagCollection aTags = this.ScanForIQTags(maxTagsThatCanRespond);
            if (blink)
            {
                this.PingTags(aTags);
            }
            return aTags;
        }

        private TagCollection ScanForTagsHelper(int maxTagsThatCanRespond, ResponseTag.TAG_TYPE tagType, bool bSendMessage)
        {
            int num2;
            base.m_deviceCode = iCard.DeviceCode.OK;
            this.m_LastScanType = tagType;
            this.m_Command = CommandCode.Scan;
            byte[] msg = new byte[0x100];
            msg[0] = this.m_comm.m_byBusAddress;
            msg[1] = 0x30;
            msg[2] = (byte) ((this.m_nCurrentAntenna - 1) << 6);
            msg[2] = (byte) (msg[2] | ((byte) (((byte) tagType) << 4)));
            msg[2] = (byte) (msg[2] | ((byte) (((int) base.m_Freq) << 3)));
            if (this.m_bRxBoostOn)
            {
                msg[2] = (byte) (msg[2] | 4);
            }
            if (this.m_bWakeUpTagsDuringScan)
            {
                msg[2] = (byte) (msg[2] | 2);
            }
            if (this.m_bChangeICSessionAddressDuringScan)
            {
                msg[2] = (byte) (msg[2] | 1);
            }
            if (tagType == ResponseTag.TAG_TYPE.IQ)
            {
                msg[3] = (byte) this.m_nTxPowerIQTags;
            }
            else
            {
                msg[3] = (byte) this.m_nTxPowerIDTags;
            }
            int num = Reader.CalculateSlotSize(maxTagsThatCanRespond);
            msg[4] = (byte) num;
            switch (num)
            {
                case 4:
                    num2 = 0x3e8;
                    break;

                case 5:
                    num2 = 0x3e8;
                    break;

                case 6:
                    num2 = 0x3e8;
                    break;

                case 7:
                    num2 = 0x5dc;
                    break;

                case 8:
                    num2 = 0x5dc;
                    break;

                case 9:
                    num2 = 0x5dc;
                    break;

                case 10:
                    num2 = 0x7d0;
                    break;

                default:
                    num2 = 0x1770;
                    break;
            }
            int nPauseBeforeRead = 20;
            if (bSendMessage)
            {
                if (this.m_bWakeUpTagsDuringScan && (tagType == ResponseTag.TAG_TYPE.IQ))
                {
                    num2 += 100;
                    nPauseBeforeRead = 0x55;
                }
                this.m_comm.SendMessage(msg, 6);
                this.m_dtLastScan = DateTime.Now;
            }
            uint id = 0;
            TagCollection tags = new TagCollection();
            while (true)
            {
                this.m_comm.RecvMsg(msg, num2, nPauseBeforeRead);
                nPauseBeforeRead = 0;
                ResponseCode code = (ResponseCode) msg[1];
                if (ResponseCode.Scan != code)
                {
                    throw new iCardCommunicationsException("The card responded with a " + code.ToString() + " response instead of a Scan response");
                }
                if (msg[2] != 0)
                {
                    if (msg[2] != 0)
                    {
                        base.m_deviceCode = (iCard.DeviceCode) (-320 - msg[2]);
                    }
                    throw new iCardCommunicationsException(string.Format("i-CARD returned an error: '{0}' and the message sent to the card was {1}", base.m_deviceCode.ToString(), this.m_comm.LastSentMessageAsHexString));
                }
                id = BitConverter.ToUInt32(msg, 3);
                if (id == 0)
                {
                    break;
                }
                sbyte signal = (sbyte) msg[7];
                Tag item = null;
                if (tagType == ResponseTag.TAG_TYPE.IQ)
                {
                    item = new iQTag(id, DateTime.Now, signal);
                }
                else
                {
                    item = new iD2Tag(id, DateTime.Now, signal);
                }
                item.Region = this.Region;
                tags.Add(item);
            }
            tags.Sort();
            this.m_nLastTickCountDuringWakeUp = Environment.TickCount;
            return tags;
        }

        public bool SessionSetupTag(iD2Tag tag, int seconds, bool sleep, bool quiet)
        {
            byte byMode = 0;
            if (seconds < 2)
            {
                byMode = 0;
            }
            else if (seconds < 3)
            {
                byMode = 0x20;
            }
            else if (seconds < 6)
            {
                byMode = 0x40;
            }
            else
            {
                byMode = 0x60;
            }
            if (sleep)
            {
                byMode = (byte) (byMode | 2);
            }
            if (quiet)
            {
                byMode = (byte) (byMode | 4);
            }
            return this.PingTag(tag, byMode);
        }

        public bool SetTagRangeState(iQTag tag, bool enableExtendedRange)
        {
            if (enableExtendedRange)
            {
                return this.ToggleTagModes(tag, 4);
            }
            return this.ToggleTagModes(tag, 0x40);
        }

        public bool SleepTag(iQTag tag, int seconds)
        {
            byte[] byData = new byte[] { (byte) seconds };
            int nBytesWritten = 0;
            return this.WriteTagRAM(tag, 8, byData, 1, ref nBytesWritten);
        }

        public void StartTagDigitalInputEventLog(iQTag tag)
        {
            this.StartTagDigitalInputEventLog(tag, false);
        }

        public void StartTagDigitalInputEventLog(iQTag tag, bool synchronize)
        {
            TagReadDataResult result = this.ReadTagDataPrivate(tag, 0x17b8, 1, 5);
            if (!result.Success)
            {
                throw new PartialTagCommunicationsException("Could not read tag spec. (" + base.m_deviceCode.ToString() + ")");
            }
            if (result.Data[0] != 4)
            {
                throw new TagHasWrongLoggerException("The tag is not compatible with the digital input event log!)");
            }
            if (!this.WriteTagDataPrivate(tag, 0x17b7, BitConverter.GetBytes(0xff), 1, 5).Success)
            {
                throw new PartialTagCommunicationsException("Could not clear wrap flag. (" + base.m_deviceCode.ToString() + ")");
            }
            if (synchronize && !this.WriteTagDataPrivate(tag, 0x94, BitConverter.GetBytes(DateTimeConvertor.ConvertDateTime(DateTime.UtcNow)), 4, 5).Success)
            {
                throw new PartialTagCommunicationsException("Could not synchronize tag time. (" + base.m_deviceCode.ToString() + ")");
            }
            if (tag.LoggerInstalled == iQTag.LoggerInstalledState.Unavailable)
            {
                throw new TagHasNoLoggerException("The i-Q tag is incapable of logging.");
            }
            this.WriteLogStartTimeOnTag(tag);
            int nBytesWritten = 0;
            if (!this.WriteTagRAM(tag, 0x7d, BitConverter.GetBytes(0x17fc), 2, ref nBytesWritten))
            {
                throw new PartialTagCommunicationsException("Failed to save the actual logger start address on the RAM of tag. (" + base.m_deviceCode.ToString() + ")");
            }
            if (!this.WriteTagDataPrivate(tag, 0x17ad, BitConverter.GetBytes(0x1800), 2, 5).Success)
            {
                throw new PartialTagCommunicationsException("Failed to save the actual logger start address on the EEPROM of tag. (" + base.m_deviceCode.ToString() + ")");
            }
            if (!this.ToggleTagModes(tag, 1))
            {
                throw new PartialTagCommunicationsException("Failed to switch the tag into logging mode");
            }
        }

        public void StartTagLogging(iQTag tag, TimeSpan samplingRate)
        {
            int totalSeconds = (int) samplingRate.TotalSeconds;
            if (totalSeconds > 0xffff)
            {
                throw new ArgumentOutOfRangeException("samplingRate");
            }
            if (totalSeconds == 0)
            {
                throw new ArgumentOutOfRangeException("samplingRate");
            }
            if ((tag.LoggerInstalled == iQTag.LoggerInstalledState.Indeterminate) && !this.PingTagPrivate(tag))
            {
                throw new PartialTagCommunicationsException("Could not ping the tag. (" + base.m_deviceCode.ToString() + ")");
            }
            if (tag.LoggerInstalled == iQTag.LoggerInstalledState.Unavailable)
            {
                throw new TagHasNoLoggerException("The i-Q tag is incapable of logging.");
            }
            if (tag.Logging == iQTag.LoggingState.On)
            {
                this.StopTagLogging(tag);
            }
            byte[] byData = new byte[2];
            if (!this.WriteTagDataPrivate(tag, 0x17ad, byData, 2, 5).Success)
            {
                throw new PartialTagCommunicationsException("Could not write to the tag. (" + base.m_deviceCode.ToString() + ")");
            }
            int nBytesWritten = 0;
            ushort num3 = (ushort) totalSeconds;
            if (!this.WriteTagDataHelper(tag, 0x80, BitConverter.GetBytes(num3), 2, ref nBytesWritten))
            {
                throw new PartialTagCommunicationsException("Could not write logging interval to the tag. (" + base.m_deviceCode.ToString() + ")");
            }
            if (!this.WriteTagRAM(tag, 0x7b, BitConverter.GetBytes(num3), 2, ref nBytesWritten))
            {
                throw new PartialTagCommunicationsException("Failed to write the interval to the tag's RAM. (" + base.m_deviceCode.ToString() + ")");
            }
            ushort num4 = 0;
            if (tag.ModelType == iQTag.Model.IQ32Elpro)
            {
                num4 = 0x1800;
                num4 = (ushort) (num4 - 2);
                if (!this.WriteTagRAM(tag, 0x7d, BitConverter.GetBytes(num4), 2, ref nBytesWritten))
                {
                    throw new PartialTagCommunicationsException("Failed to write the log pointer position to the tag's RAM. (" + base.m_deviceCode.ToString() + ")");
                }
            }
            if (!this.ReadTagRAM(tag, 0x7d, byData, 2, ref nBytesWritten))
            {
                throw new PartialTagCommunicationsException("Failed to read the start of log pointer from tag RAM. (" + base.m_deviceCode.ToString() + ")");
            }
            num4 = BitConverter.ToUInt16(byData, 0);
            if (tag.ModelType == iQTag.Model.IQ32Elpro)
            {
                num4 = (ushort) (tag.DataCapacity - 2);
            }
            if (num4 < 0x1800)
            {
                num4 = (ushort) (tag.DataCapacity - 2);
            }
            ushort num5 = 0x1000;
            if (!this.WriteTagDataPrivate(tag, num4, BitConverter.GetBytes(num5), 2, 5).Success)
            {
                throw new PartialTagCommunicationsException("Failed to write the start flag on the tag. (" + base.m_deviceCode.ToString() + ")");
            }
            if (tag.ReportsBatteryPercentConsumed)
            {
                uint num6 = 0x20101ff;
                if (!this.WriteTagDataPrivate(tag, 0x17a0, BitConverter.GetBytes(num6), 4, 5).Success)
                {
                    throw new PartialTagCommunicationsException("Failed to clear min/max temperature extremes. (" + base.m_deviceCode.ToString() + ")");
                }
            }
            this.WriteLogStartTimeOnTag(tag);
            if (tag.ModelType == iQTag.Model.IQ32Elpro)
            {
                byte num7 = 0xff;
                if (!this.WriteTagDataPrivate(tag, 0x17b7, BitConverter.GetBytes((short) num7), 1, 5).Success)
                {
                    throw new PartialTagCommunicationsException("Failed to write the 'log wrapped' indicator flag. (" + base.m_deviceCode.ToString() + ")");
                }
            }
            if (!this.WriteTagDataPrivate(tag, 0x17ad, BitConverter.GetBytes(num4), 2, 5).Success)
            {
                throw new PartialTagCommunicationsException("Failed to save the actual logger start address on the tag. (" + base.m_deviceCode.ToString() + ")");
            }
            if ((this.ToggleTagModes(tag, 1) && tag.ReportsBatteryPercentConsumed) && !this.WriteTagRAM(tag, 0x7b, BitConverter.GetBytes(num3), 2, ref nBytesWritten))
            {
                throw new PartialTagCommunicationsException("Failed to write to tag RAM to start the log immediately. (" + base.m_deviceCode.ToString() + ")");
            }
        }

        public void StopTagLogging(iQTag tag)
        {
            TagReadDataResult result = this.ReadTagDataPrivate(tag, 0x17b8, 1, 5);
            if (!result.Success)
            {
                throw new PartialTagCommunicationsException("Could not read tag spec. (" + base.m_deviceCode.ToString() + ")");
            }
            if (tag.Logging != iQTag.LoggingState.Off)
            {
                uint num = 0;
                if (result.Data[0] != 4)
                {
                    TimeSpan span = this.ReadTagLogSamplingInterval(tag);
                    if (tag.Logging != iQTag.LoggingState.On)
                    {
                        throw new InvalidTagOperationException("An i-Q tag can only stop logging when it is in the logging state.");
                    }
                    int nBytesRead = 0;
                    byte[] byData = new byte[2];
                    if (!this.ReadTagRAM(tag, 0x7b, byData, 2, ref nBytesRead))
                    {
                        throw new PartialTagCommunicationsException("Failed to read Tag RAM. (" + base.m_deviceCode.ToString() + ")");
                    }
                    ushort num3 = BitConverter.ToUInt16(byData, 0);
                    int num4 = (int) (span.TotalSeconds - num3);
                    num = DateTimeConvertor.ConvertDateTime(DateTime.Now.AddSeconds((double) (-1 * num4)).ToUniversalTime());
                }
                else
                {
                    num = DateTimeConvertor.ConvertDateTime(DateTime.UtcNow);
                }
                if (!this.WriteTagDataPrivate(tag, 0x17b3, BitConverter.GetBytes(num), 4, 5).Success)
                {
                    throw new PartialTagCommunicationsException("Failed to write the stop time on the tag. (" + base.m_deviceCode.ToString() + ")");
                }
                if (!this.ToggleTagModes(tag, 0x10))
                {
                    throw new PartialTagCommunicationsException("Failed to disable the logging mode. (" + base.m_deviceCode.ToString() + ")");
                }
            }
        }

        public override void TestCommunications()
        {
            this.ReadVersion();
        }

        private bool ToggleTagModes(iQTag tag, byte byModeFlag)
        {
            byte num = 0;
            byte num2 = 0;
            byte num3 = 0;
            byte num4 = 0;
            TagReadDataResult result = this.ReadTagDataPrivate(tag, 0, 1, 2);
            if (!result.Success)
            {
                return false;
            }
            if ((tag.m_byMode & 240) != (result.Data[0] & 240))
            {
                string message = "Incorrect TagVersion: " + tag.m_byMode.ToString("X") + " / " + result.Data[0].ToString("X");
                log.Error(message);
                throw new Exception(message);
            }
            num = num2 = result.Data[0];
            num3 = num4 = byModeFlag;
            num3 = (byte) (num3 & 15);
            num2 = (byte) (num2 & ~num3);
            num4 = (byte) (num4 & 240);
            num4 = (byte) (num4 >> 4);
            num2 = (byte) (num2 | num4);
            if ((byModeFlag != 0) && (num2 != num))
            {
                int nBytesWritten = 0;
                if (!this.WriteTagDataHelper(tag, 0, BitConverter.GetBytes((short) num2), 1, ref nBytesWritten))
                {
                    return false;
                }
                if ((num2 & 1) != 0)
                {
                    tag.m_LoggingState = iQTag.LoggingState.Off;
                }
                else
                {
                    tag.m_LoggingState = iQTag.LoggingState.On;
                }
                if ((num2 & 4) != 0)
                {
                    tag.m_range = iQTag.RangeState.NormalRange;
                }
                else
                {
                    tag.m_range = iQTag.RangeState.ExtendedRange;
                }
            }
            return true;
        }

        public override string ToString()
        {
            return "i-CARD 3";
        }

        public void WriteIQTagUserEngineHourCounter(iQTag tag, TimeSpan ts)
        {
            EngineHourTagHelper.WriteIQTagUserEngineHourCounter(this, tag, ts);
        }

        private void WriteLogStartTimeOnTag(iQTag tag)
        {
            DateTime now = DateTime.Now;
            uint utcNow = DateTimeConvertor.GetUtcNow();
            if (!this.WriteTagDataPrivate(tag, 0x17af, BitConverter.GetBytes(utcNow), 4, 5).Success)
            {
                throw new PartialTagCommunicationsException("Failed to write the start time to the tag. (" + base.m_deviceCode.ToString() + ")");
            }
        }

        public TagWriteDataResult WriteTagData(iD2Tag tag, int address, byte[] byData, int bytesToWrite)
        {
            return this.WriteTagDataPrivate(tag, address, byData, bytesToWrite, 5);
        }

        public TagWriteDataResult WriteTagData(iQTag tag, int address, byte[] byData, int bytesToWrite)
        {
            int num;
            if (bytesToWrite > 0x4e20)
            {
                num = 20;
            }
            else if (bytesToWrite > 0x2710)
            {
                num = 15;
            }
            else if (bytesToWrite > 0x1388)
            {
                num = 10;
            }
            else
            {
                num = 5;
            }
            return this.WriteTagDataPrivate(tag, address, byData, bytesToWrite, num);
        }

        internal bool WriteTagDataHelper(ResponseTag tag, int address, byte[] byData, int bytesToWrite, ref int nBytesWritten)
        {
            base.m_deviceCode = iCard.DeviceCode.OK;
            this.m_Command = CommandCode.WriteData;
            nBytesWritten = 0;
            tag.AntennaSignals.Invalidate();
            iQTag tag2 = tag as iQTag;
            if (((tag2 != null) && !this.m_bRAM) && ((iQTag.Model.Indeterminate == tag2.m_Model) && !this.PingTagPrivate(tag)))
            {
                return false;
            }
            byte[] destinationArray = new byte[290];
            byte num2 = 1;
            destinationArray[0] = this.m_comm.m_byBusAddress;
            destinationArray[1] = 50;
            destinationArray[2] = (byte) ((this.m_nCurrentAntenna - 1) << 6);
            Frequency region = tag.Region;
            if (region != Frequency.Indeterminate)
            {
                base.RegionCompatibilityCheck(tag.Region);
            }
            else
            {
                region = this.Region;
            }
            destinationArray[2] = (byte) (destinationArray[2] | ((byte) (((int) region) << 3)));
            ResponseTag.TAG_TYPE iQ = ResponseTag.TAG_TYPE.IQ;
            if (tag is iD2Tag)
            {
                switch (region)
                {
                    case Frequency.European:
                        iQ = ResponseTag.TAG_TYPE.ID_INTERNATIONAL;
                        break;

                    case Frequency.NorthAmerican:
                        iQ = ResponseTag.TAG_TYPE.ID_NORTH_AMERICA;
                        break;
                }
            }
            destinationArray[2] = (byte) (destinationArray[2] | ((byte) (((byte) iQ) << 4)));
            if (this.m_bRxBoostOn)
            {
                destinationArray[2] = (byte) (destinationArray[2] | 4);
            }
            bool bWakeupIQTag = this.bWakeupIQTag;
            this.m_nLastTickCountDuringWakeUp = 0;
            if (bWakeupIQTag)
            {
                destinationArray[2] = (byte) (destinationArray[2] | 2);
            }
            if (this.m_bRAM)
            {
                destinationArray[2] = (byte) (destinationArray[2] | 1);
            }
            if (iQ == ResponseTag.TAG_TYPE.IQ)
            {
                destinationArray[3] = (byte) this.m_nTxPowerIQTags;
            }
            else
            {
                destinationArray[3] = (byte) this.m_nTxPowerIDTags;
                if (this.m_bBlinkiD2Tags)
                {
                    num2 = (byte) (num2 | 1);
                }
                else
                {
                    num2 = (byte) (num2 & 0xfe);
                }
            }
            destinationArray[4] = 1;
            destinationArray[5] = (byte) this.Retries;
            byte[] bytes = BitConverter.GetBytes(tag.Number);
            Array.Copy(bytes, 0, destinationArray, 6, bytes.Length);
            destinationArray[10] = (byte) (address % 0x100);
            destinationArray[11] = (byte) (address / 0x100);
            destinationArray[12] = (byte) bytesToWrite;
            Array.Copy(byData, 0, destinationArray, 13, bytesToWrite);
            int num = 100;
            if (iQ == ResponseTag.TAG_TYPE.IQ)
            {
                num += bytesToWrite * 3;
                if (bWakeupIQTag)
                {
                    num += 100;
                }
            }
            num *= this.Retries + 1;
            this.m_comm.SendMessage(destinationArray, 13 + bytesToWrite);
            this.m_comm.RecvMsg(destinationArray, 0x1388, tag.WaitForResponse(bWakeupIQTag));
            ResponseCode code = (ResponseCode) destinationArray[1];
            if (ResponseCode.WriteData != code)
            {
                throw new iCardCommunicationsException("The i-CARD responded with an incorrect response code");
            }
            if ((destinationArray[2] != 0) && (destinationArray[2] != 6))
            {
                base.m_deviceCode = (iCard.DeviceCode) (-320 - destinationArray[2]);
            }
            if (base.m_deviceCode != iCard.DeviceCode.OK)
            {
                throw new iCardCommunicationsException(string.Format("i-CARD returned an error: '{0}' and the message sent to the card was {1}", base.m_deviceCode.ToString(), this.m_comm.LastSentMessageAsHexString));
            }
            int num3 = (sbyte) destinationArray[3];
            tag.Signal = num3;
            if (tag2 != null)
            {
                iQTag.Model modelType = tag2.ModelType;
                tag2.SetModelTypeFromTagProtocol(destinationArray[4]);
                if (iQTag.Model.Indeterminate == tag2.ModelType)
                {
                    tag2.m_Model = modelType;
                }
                else
                {
                    if ((destinationArray[4] & 4) != 0)
                    {
                        tag2.m_range = iQTag.RangeState.NormalRange;
                    }
                    else
                    {
                        tag2.m_range = iQTag.RangeState.ExtendedRange;
                    }
                    tag2.m_nPercentBatteryConsumed = destinationArray[6];
                }
            }
            else
            {
                iD2Tag tag3 = tag as iD2Tag;
                if (tag3 != null)
                {
                    tag3.m_BattStatus = ((iD2Tag.BatteryStatus) destinationArray[5]) & iD2Tag.BatteryStatus.Good;
                }
            }
            tag.m_nVersion = destinationArray[4] >> 4;
            this.m_nLastTickCountDuringWakeUp = Environment.TickCount;
            nBytesWritten = bytesToWrite;
            tag.m_dt = DateTime.Now;
            tag.Region = region;
            return true;
        }

        private TagWriteDataResult WriteTagDataPrivate(ResponseTag tag, int address, byte[] byData, int bytesToWrite, int Retries)
        {
            TagWriteDataResult result = new TagWriteDataResult();
            this.m_bRAM = false;
            if (address < tag.MinDataWriteAddress)
            {
                throw new ArgumentOutOfRangeException("address");
            }
            iQTag tag2 = tag as iQTag;
            if (tag2 != null)
            {
                if ((iQTag.Model.Indeterminate == tag2.m_Model) && !this.PingTagPrivate(tag))
                {
                    return result;
                }
                if ((this.m_bTemperatureTagDataProtectionEnabled && (tag2.LoggerInstalled == iQTag.LoggerInstalledState.Available)) && ((address <= 0x17ff) && ((address + bytesToWrite) >= 0x17bf)))
                {
                    throw new ArgumentOutOfRangeException("address");
                }
            }
            if ((address + bytesToWrite) > tag.DataCapacity)
            {
                throw new ArgumentOutOfRangeException("address");
            }
            int nBytesWritten = 0;
            int num2 = 0;
            byte[] destinationArray = new byte[0x80];
            while (bytesToWrite > 0)
            {
                if (bytesToWrite > 0x80)
                {
                    Array.Copy(byData, result.BytesWritten, destinationArray, 0, 0x80);
                    num2 = 0x80;
                }
                else
                {
                    Array.Copy(byData, result.BytesWritten, destinationArray, 0, bytesToWrite);
                    num2 = bytesToWrite;
                }
                try
                {
                    bool flag = this.WriteTagDataHelper(tag, address + result.BytesWritten, destinationArray, num2, ref nBytesWritten);
                    result.BytesWritten += nBytesWritten;
                    bytesToWrite -= nBytesWritten;
                    result.Success = true;
                    if (!flag)
                    {
                        Retries--;
                        if (Retries < 0)
                        {
                            return result;
                        }
                    }
                    continue;
                }
                catch (iCardCommunicationsException)
                {
                    if ((base.m_deviceCode != iCard.DeviceCode.TagWriteNoAcknowledge) && (base.m_deviceCode != iCard.DeviceCode.TagPartialWrite))
                    {
                        throw;
                    }
                    Retries--;
                    if (Retries < 0)
                    {
                        throw;
                    }
                    continue;
                }
            }
            return result;
        }

        public TagWriteDataResult WriteTagDataString(iD2Tag tag, int address, string text)
        {
            return TagDataFormatter.WriteTagDataString(this, tag, address, text);
        }

        public TagWriteDataResult WriteTagDataString(iQTag tag, int address, string text)
        {
            return TagDataFormatter.WriteTagDataString(this, tag, address, text);
        }

        public TagWriteDataResult WriteTagDataWithCRCAndLength(iD2Tag tag, int address, byte[] byData, int bytesToWrite)
        {
            return TagDataFormatter.WriteTagDataWithCRCAndLength(this, tag, address, byData, bytesToWrite);
        }

        public TagWriteDataResult WriteTagDataWithCRCAndLength(iQTag tag, int address, byte[] byData, int bytesToWrite)
        {
            return TagDataFormatter.WriteTagDataWithCRCAndLength(this, tag, address, byData, bytesToWrite);
        }

        internal bool WriteTagRAM(ResponseTag tag, int address, byte[] byData, int bytesToWrite, ref int nBytesWritten)
        {
            bool flag2;
            this.m_bRAM = true;
            int num = 5;
        Label_000B:;
            try
            {
                try
                {
                    return this.WriteTagDataHelper(tag, address, byData, bytesToWrite, ref nBytesWritten);
                }
                catch (iCardCommunicationsException)
                {
                    if ((base.m_deviceCode != iCard.DeviceCode.TagWriteNoAcknowledge) && (base.m_deviceCode != iCard.DeviceCode.TagPartialWrite))
                    {
                        throw;
                    }
                    num--;
                    if (num < 0)
                    {
                        this.m_bRAM = false;
                        throw;
                    }
                }
                goto Label_000B;
            }
            finally
            {
                this.m_bRAM = false;
            }
            return flag2;
        }

        public int Antenna
        {
            get
            {
                return this.m_nCurrentAntenna;
            }
            set
            {
                if (value > 4)
                {
                    throw new ArgumentOutOfRangeException("value");
                }
                if (value < 1)
                {
                    throw new ArgumentOutOfRangeException("value");
                }
                this.m_nCurrentAntenna = value;
            }
        }

        internal bool bWakeupIQTag
        {
            get
            {
                return (Reader.GetElapsedTime(ref this.m_nLastTickCountDuringWakeUp) > 250);
            }
        }

        public override bool Connected
        {
            get
            {
                if (this.m_comm == null)
                {
                    return false;
                }
                return this.m_comm.IsOpen;
            }
        }

        public bool EnableChangeICSessionAddressDuringScan
        {
            get
            {
                return this.m_bChangeICSessionAddressDuringScan;
            }
            set
            {
                this.m_bChangeICSessionAddressDuringScan = value;
            }
        }

        public bool EnableiD2TagBlink
        {
            get
            {
                return this.m_bBlinkiD2Tags;
            }
            set
            {
                this.m_bBlinkiD2Tags = value;
            }
        }

        public bool EnableReceiveBoost
        {
            get
            {
                return this.m_bRxBoostOn;
            }
            set
            {
                this.m_bRxBoostOn = value;
            }
        }

        public bool EnableTagLogDataProtection
        {
            get
            {
                return this.m_bTemperatureTagDataProtectionEnabled;
            }
            set
            {
                this.m_bTemperatureTagDataProtectionEnabled = value;
            }
        }

        public bool EnableWakeupTagsDuringScan
        {
            get
            {
                return this.m_bWakeUpTagsDuringScan;
            }
            set
            {
                this.m_bWakeUpTagsDuringScan = value;
            }
        }

        internal FormFactor Format
        {
            get
            {
                return this.m_FormFactor;
            }
        }

        public override string Information
        {
            get
            {
                return base.m_strInformation;
            }
        }

        public int MaxOutputdBmID2
        {
            get
            {
                return this.m_nMaxTransmissionID;
            }
        }

        public int MaxOutputdBmIQ
        {
            get
            {
                return this.m_nMaxTransmissionIQ;
            }
        }

        public int MinOutputdBm
        {
            get
            {
                return -30;
            }
        }

        public static int MinReceivedBm
        {
            get
            {
                return -128;
            }
        }

        public int Retries
        {
            get
            {
                return this.m_nRetries;
            }
            set
            {
                if (value < 1)
                {
                    throw new ArgumentOutOfRangeException("value");
                }
                if (value > 0xff)
                {
                    throw new ArgumentOutOfRangeException("value");
                }
                this.m_nRetries = value;
            }
        }

        public int TxPowerID
        {
            get
            {
                return this.m_nTxPowerIDTags;
            }
            set
            {
                if (value > this.MaxOutputdBmID2)
                {
                    throw new ArgumentOutOfRangeException("value");
                }
                if (value < MinReceivedBm)
                {
                    throw new ArgumentOutOfRangeException("value");
                }
                this.m_nTxPowerIDTags = value;
            }
        }

        public int TxPowerIQ
        {
            get
            {
                return this.m_nTxPowerIQTags;
            }
            set
            {
                if (value > this.MaxOutputdBmIQ)
                {
                    throw new ArgumentOutOfRangeException("value");
                }
                if (value < this.MinOutputdBm)
                {
                    throw new ArgumentOutOfRangeException("value");
                }
                this.m_nTxPowerIQTags = value;
            }
        }

        private enum CommandCode : byte
        {
            AddRemoveCardKey = 0x52,
            CommunicationEncryption = 0x51,
            GetVersion = 0x33,
            Indeterminate = 0,
            ReadConfig = 0x34,
            ReadData = 0x31,
            RepeatLastResponse = 0x36,
            Scan = 0x30,
            SwitchTagSecureMode = 0x53,
            Test = 0x3f,
            UpdateFlash = 0x37,
            WriteConfig = 0x35,
            WriteData = 50
        }

        internal enum FormFactor
        {
            Indeterminate,
            PCMCIA,
            CompactFlash,
            T2
        }

        private enum ResponseCode : byte
        {
            AddRemoveCardKey = 210,
            CommunicationEncryption = 0xd1,
            GetVersion = 0xb3,
            ReadConfig = 180,
            ReadData = 0xb1,
            Scan = 0xb0,
            SwitchTagSecureMode = 0xd3,
            Test = 0xbf,
            UpdateFlash = 0xb7,
            WriteConfig = 0xb5,
            WriteData = 0xb2
        }
    }
}

