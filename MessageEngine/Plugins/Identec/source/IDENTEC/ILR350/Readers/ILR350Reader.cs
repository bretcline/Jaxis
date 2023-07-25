namespace IDENTEC.ILR350.Readers
{
    using IDENTEC;
    using IDENTEC.ILR350;
    using IDENTEC.ILR350.Tags;
    using IDENTEC.ILR350.Tags.Info;
    using IDENTEC.Readers;
    using IDENTEC.Readers.BeaconReaders;
    using IDENTEC.Tags;
    using NLog;
    using System;
    using System.Collections;
    using System.Runtime.InteropServices;
    using System.Text;
    using System.Threading;

    public abstract class ILR350Reader : iBusModule, IBusDevice
    {
        private int _ActiveAntenna;
        private int _antennaCount;
        private string _bootLoaderVersion;
        internal byte _byAddress;
        private int _firmwareVersionMajor;
        private int _firmwareVersionMinor;
        private Frequency _frequency;
        internal iBusAdapter _iBus;
        private string _infoString;
        private int _MaxReadPacket;
        private int _MaxWritePacket;
        private int _nLastLQI;
        internal int _nLastTickCountDuringBroadcastCommunication;
        private int _protocolVersionMajor;
        private int _protocolVersionMinor;
        private int _ReaderHW;
        private iBusDeviceStatus _readerStatus;
        private int _retries;
        private IDENTEC.ILR350.RFBaudRate _rfBaudrate;
        private string _serialNumber;
        private int _SupportedFrequencies;
        private int _TXPower;
        private TimeSpan _WakeUpLength;
        private ILR350TagCollection BeaconTagList;
        internal static readonly byte DISCONNECTED_SLAVE_ADDRESS = 0xfe;
        protected static NLog.Logger log = LogManager.GetLogger("ILR350Reader");

        protected ILR350Reader()
        {
            this._byAddress = DISCONNECTED_SLAVE_ADDRESS;
            this.BeaconTagList = new ILR350TagCollection();
            this._SupportedFrequencies = 2;
            this._antennaCount = 1;
            this._WakeUpLength = TimeSpan.Zero;
            this._nLastTickCountDuringBroadcastCommunication = Environment.TickCount - 0x2710;
            this._MaxReadPacket = 110;
            this._MaxWritePacket = 0x30;
            this._retries = 5;
            this._readerStatus = new iBusDeviceStatus(0);
        }

        protected ILR350Reader(ILR350Reader reader)
        {
            this._byAddress = DISCONNECTED_SLAVE_ADDRESS;
            this.BeaconTagList = new ILR350TagCollection();
            this._SupportedFrequencies = 2;
            this._antennaCount = 1;
            this._WakeUpLength = TimeSpan.Zero;
            this._nLastTickCountDuringBroadcastCommunication = Environment.TickCount - 0x2710;
            this._MaxReadPacket = 110;
            this._MaxWritePacket = 0x30;
            this._retries = 5;
            this._readerStatus = new iBusDeviceStatus(0);
            this._iBus = reader._iBus;
            this._byAddress = reader._byAddress;
            this._antennaCount = reader._antennaCount;
            this._ActiveAntenna = reader._ActiveAntenna;
            this._infoString = reader._infoString;
            this._firmwareVersionMajor = reader._firmwareVersionMajor;
            this._firmwareVersionMinor = reader._firmwareVersionMinor;
            this._bootLoaderVersion = reader._bootLoaderVersion;
            this._serialNumber = reader._serialNumber;
            this._ReaderHW = reader._ReaderHW;
            this._SupportedFrequencies = reader._SupportedFrequencies;
            this._protocolVersionMajor = reader._protocolVersionMajor;
            this._protocolVersionMinor = reader._protocolVersionMinor;
            this._antennaCount = reader._antennaCount;
            this._MaxReadPacket = reader._MaxReadPacket;
            this._MaxWritePacket = reader._MaxWritePacket;
            this._nLastTickCountDuringBroadcastCommunication = reader._nLastTickCountDuringBroadcastCommunication;
            this._readerStatus = reader._readerStatus;
            this._TXPower = reader._TXPower;
            this._retries = reader._retries;
            this._WakeUpLength = reader._WakeUpLength;
            this._frequency = reader._frequency;
            this._rfBaudrate = reader._rfBaudrate;
        }

        public virtual void BroadcastBeaconRequest(int pingCount, TimeSpan slotSize, BeaconInformation info)
        {
            this.BroadcastBeaconRequest(pingCount, slotSize, info, TimeSpan.Zero);
        }

        public virtual void BroadcastBeaconRequest(int pingCount, TimeSpan slotSize, BeaconInformation info, TimeSpan WakeUpDuration)
        {
            if (WakeUpDuration < TimeSpan.Zero)
            {
                throw new ArgumentOutOfRangeException("cannot have negative timespan");
            }
            if (WakeUpDuration > new TimeSpan(0, 0, 5))
            {
                throw new ArgumentOutOfRangeException("maximum wake up is 5 seconds");
            }
            if (slotSize < new TimeSpan(0, 0, 0, 0, 100))
            {
                throw new ArgumentOutOfRangeException("minimum slot size is 100ms");
            }
            if (slotSize > new TimeSpan(0, 0, 0x19))
            {
                throw new ArgumentOutOfRangeException("maximum slot size is 25 seconds");
            }
            if (pingCount < 0)
            {
                throw new ArgumentOutOfRangeException("ping count cannot be negative");
            }
            if (pingCount > 0xfe)
            {
                throw new ArgumentOutOfRangeException("ping count must be < 255");
            }
            byte[] data = new byte[] { (byte) (pingCount & 0xff), (byte) (slotSize.TotalMilliseconds / 100.0), (byte)info };
            this.BroadcastWriteDataToRegister(0xff, 0xff, 1, 13, data, 3, true, WakeUpDuration);
        }

        public virtual void BroadcastWriteDataToEEPROM(int address, byte[] data, int bytesToWrite, bool UseWakeUp, TimeSpan WakeUpDuration)
        {
            this.BroadcastWriteDataToEEPROM(0xff, 0xff, address, data, bytesToWrite, UseWakeUp, WakeUpDuration);
        }

        public virtual void BroadcastWriteDataToEEPROM(byte manufacturer, byte tagType, int address, byte[] data, int bytesToWrite, bool UseWakeUp, TimeSpan WakeUpDuration)
        {
            TimeSpan span;
            iQ350 tag = new iQ350 {
                ManufacturerID = manufacturer,
                TypeID = tagType,
                _SerialNumber = BitConverter.GetBytes(uint.MaxValue)
            };
            TimeSpan wakeUpDuration = this.WakeUpDuration;
            if (WakeUpDuration < TimeSpan.Zero)
            {
                throw new ArgumentOutOfRangeException("cannot have negative timespan");
            }
            if (WakeUpDuration > new TimeSpan(0, 0, 5))
            {
                throw new ArgumentOutOfRangeException("maximum wake up is 5 seconds");
            }
            if (bytesToWrite <= 0)
            {
                throw new ArgumentOutOfRangeException("bytesToWrite must be > 0");
            }
            if (UseWakeUp)
            {
                span = WakeUpDuration;
            }
            else
            {
                span = new TimeSpan(0, 0, 0);
            }
            int nByteToWrite = 0;
            int num2 = 0;
            int nBytesWritten = 0;
            WakeUpMode forceWakeUP = WakeUpMode.ForceWakeUP;
            while (num2 < bytesToWrite)
            {
                try
                {
                    try
                    {
                        nByteToWrite = bytesToWrite - num2;
                        if (nByteToWrite > this.MaxWritePacket)
                        {
                            nByteToWrite = this.MaxWritePacket;
                        }
                        this.WriteTagDataHelper(tag, forceWakeUP, span, address + num2, data, num2, nByteToWrite, ref nBytesWritten);
                        Thread.Sleep(30);
                        forceWakeUP = WakeUpMode.ForceNoWakeUp;
                        span = new TimeSpan(0, 0, 0);
                        num2 += nByteToWrite;
                        this._nLastTickCountDuringBroadcastCommunication = Environment.TickCount;
                        log.Trace("written :" + num2.ToString() + " :" + nBytesWritten.ToString());
                    }
                    catch (Exception exception)
                    {
                        log.Error("Exception during broadcast write nb byte written:" + num2.ToString(), exception);
                        throw exception;
                    }
                    continue;
                }
                finally
                {
                    this.WakeUpDuration = wakeUpDuration;
                }
            }
        }

        public virtual void BroadcastWriteDataToRegister(byte register, int address, byte[] data, int bytesToWrite, bool UseWakeUp, TimeSpan WakeUpDuration)
        {
            this.BroadcastWriteDataToEEPROM(0xff, 0xff, (register << 20) + address, data, bytesToWrite, UseWakeUp, WakeUpDuration);
        }

        public virtual void BroadcastWriteDataToRegister(byte manufacturer, byte tagType, byte register, int address, byte[] data, int bytesToWrite, bool UseWakeUp, TimeSpan WakeUpDuration)
        {
            this.BroadcastWriteDataToEEPROM(manufacturer, tagType, (register << 20) + address, data, bytesToWrite, UseWakeUp, WakeUpDuration);
        }

        public void ClearTagList()
        {
            this.SetParameter(0x15, 0);
        }

        public void ConnectSlavePort(bool enable)
        {
            this.SetParameterPrivate(0x10, enable ? 1 : 0);
        }

        public bool EnterReflashMode()
        {
            byte[] buffer = new byte[70];
            buffer[0] = (byte) this.Address;
            buffer[1] = 0x45;
            buffer[2] = 0;
            this.DataStream.SendMessage(buffer, 3);
            IC3ProtocolMessage message = this.DataStream.ReadMessage(0x3e8);
            message.CheckResponse((byte) this.Address, 0x45);
            if (message[3] != 0)
            {
                return false;
            }
            Thread.Sleep(100);
            return true;
        }

        public virtual ILR350TagCollection GetBeaconTags()
        {
            this.BeaconTagList = new ILR350TagCollection();
            IC3ProtocolMessage message = new IC3ProtocolMessage();
            byte[] buffer = new byte[0x10];
            buffer[0] = this._byAddress;
            buffer[1] = 0x40;
            buffer[2] = 0;
            ArrayList list = new ArrayList();
            Exception exception = null;
            DateTime now = DateTime.Now;
            lock (this.DataStream.LockObject)
            {
                try
                {
                    this.DataStream.SendMessage(buffer, 3);
                    now = DateTime.Now;
                }
                catch (Exception)
                {
                    throw;
                }
                Thread.Sleep(15);
            Label_006E:
                try
                {
                    message = this.DataStream.ReadMessage(0x7d0);
                }
                catch (CRCException exception2)
                {
                    log.TraceException(exception2.Message + "\n\n" + exception2.Buffer, exception2);
                    exception = exception2;
                    goto Label_010D;
                }
                catch (Exception exception3)
                {
                    log.Trace(exception3.Message, exception3);
                    if (exception == null)
                    {
                        exception = exception3;
                    }
                }
                if (exception == null)
                {
                    if (message[2] != (buffer[1] + 0x80))
                    {
                        exception = new InvalidOperationException("The response contained the incorrect command");
                    }
                    else if (message[3] != 0x10)
                    {
                        list.Add(message);
                        goto Label_006E;
                    }
                }
            }
        Label_010D:
            log.Trace("Received : " + list.Count + " messages");
            if (exception != null)
            {
                log.Trace("RX exception, " + exception.Message);
            }
            for (int i = 0; i < list.Count; i++)
            {
                DateTime time2;
                message = (IC3ProtocolMessage) list[i];
                int startIndex = 12;
                if (message[3] != 0)
                {
                    byte num6 = message[3];
                    throw new InvalidDeviceResponseException("Cmd Status byte =" + num6.ToString() + " but should be 0");
                }
                byte num1 = message[5];
                uint iD = BitConverter.ToUInt32(message.Buffer, startIndex);
                ILR350Tag item = ILR350Tag.TagFactory(message[10], (TagType) message[11], iD);
                item._beaconCounter = (ushort) ((message[8] << 8) + message[7]);
                item.BeaconMessageType = message[0x10];
                ushort num4 = message[0x11];
                item.Status = message[6];
                item.BeaconMessage = new byte[num4];
                Array.Copy(message.Buffer, 0x12, item.BeaconMessage, 0, item.BeaconMessage.Length);
                log.Trace(item.SerialLabel + " beacon length :" + item.BeaconMessage.Length.ToString());
                DateTime time3 = time2 = now;
                int num5 = BitConverter.ToInt32(message.Buffer, num4 + 0x16);
                time3 = now.AddSeconds((double) num5);
                item.TimeFirstSeen = time3;
                num5 = BitConverter.ToInt32(message.Buffer, num4 + 0x1a);
                time2 = now.AddSeconds((double) num5);
                item.TimeLastSeen = time2;
                item.MaxSignal = (sbyte) message[num4 + 0x1f];
                item.LastSignal = (sbyte) message[num4 + 30];
                item.SeenCount = message[num4 + 0x20];
                item._nBeaconsSinceLastReaderTransmission = message[num4 + 0x20];
                this.BeaconTagList.Add(item);
            }
            if (exception != null)
            {
                throw exception;
            }
            this.OnReaderStatusErrorReadStatusAndFireEvent(message);
            return this.BeaconTagList;
        }

        public byte GetBusAddress()
        {
            this._byAddress = (byte) this.GetParameter(0x11);
            return this._byAddress;
        }

        public int GetDataLen()
        {
            return this.GetParameter(0x17);
        }

        public Frequency GetFrequency()
        {
            int parameter = this.GetParameter(0x20);
            this._frequency = (Frequency) parameter;
            return this._frequency;
        }

        public int GetLastTagFrequencyOffset()
        {
            int parameter = this.GetParameter(6);
            int num2 = this.GetParameter(7);
            this._nLastLQI = num2 & 0xffff;
            return (parameter & 0xffff);
        }

        public int GetNumberOfTagsDuringRequest()
        {
            return this.GetParameter(0x16);
        }

        public int GetParameter(int subCommand)
        {
            byte bySubCmd = (byte) subCommand;
            uint dwParameter = 0;
            this.GetParameterPrivate(bySubCmd, ref dwParameter);
            return (int) dwParameter;
        }

        internal void GetParameterPrivate(byte bySubCmd, ref uint dwParameter)
        {
            dwParameter = 0;
            byte[] buffer = new byte[0x10];
            buffer[0] = this._byAddress;
            buffer[1] = 100;
            buffer[2] = bySubCmd;
            int length = 3;
            lock (this.DataStream.LockObject)
            {
                this.DataStream.SendMessage(buffer, length);
                Thread.Sleep(5);
                IC3ProtocolMessage response = this.DataStream.ReadMessage(0x7d0);
                response.CheckResponse(this._byAddress, 100);
                this.ThrowOnReturnCodeError(response);
                dwParameter = (uint) ((((response[8] << 0x18) + (response[7] << 0x10)) + (response[6] << 8)) + response[5]);
                if (bySubCmd != 4)
                {
                    this.OnReaderStatusErrorReadStatusAndFireEvent(response);
                }
            }
        }

        public IDENTEC.ILR350.RFBaudRate GetRFBeaconBaudrate()
        {
            return (IDENTEC.ILR350.RFBaudRate) ((byte) this.GetParameter(0x19));
        }

        public iBusDeviceStatus GetStatus()
        {
            uint parameter = (uint) this.GetParameter(4);
            this._readerStatus = new iBusDeviceStatus(parameter);
            return this._readerStatus;
        }

        public TagListBehavior GetTagListBehavior()
        {
            return (TagListBehavior) this.GetParameter(20);
        }

        public TimeSpan GetTagListInhibitTime()
        {
            return new TimeSpan(0, 0, this.GetParameter(0x12));
        }

        public TimeSpan GetTagReReportingInterval()
        {
            return new TimeSpan(0, 0, this.GetParameter(0x13));
        }

        public int GetTagSignalFilterLevel()
        {
            return (sbyte) this.GetParameter(0x2b);
        }

        public DateTime GetUpTime()
        {
            uint parameter = (uint) this.GetParameter(2);
            return DateTime.Now.Subtract(TimeSpan.FromSeconds((double) parameter));
        }

        internal byte GetWakeUpMode(iQ350 tag, IDENTEC.ILR350.RFBaudRate baudrate, WakeUpMode wakeUpMode)
        {
            byte num = (byte) baudrate;
            if (wakeUpMode == WakeUpMode.ForceWakeUP)
            {
                num = (byte) (num | 0x20);
            }
            TimeSpan zero = TimeSpan.Zero;
            if (tag != null)
            {
                TagDescription description;
                if (!ILR350Tag.Description.TryGetValue(tag.TypeID, out description))
                {
                    zero = iQ350.MAX_AWAKE_TIME;
                }
                else
                {
                    zero = description.AwakeTime;
                }
                if ((wakeUpMode == WakeUpMode.AutoWakeUp) && (zero < iQ350.MAX_AWAKE_TIME))
                {
                    int elapsedTime = Reader.GetElapsedTime(ref tag._nLastTickCountDuringWakeUp);
                    int num3 = Reader.GetElapsedTime(ref this._nLastTickCountDuringBroadcastCommunication);
                    if ((elapsedTime > (zero.TotalMilliseconds - 20.0)) && (num3 > (zero.TotalMilliseconds - 20.0)))
                    {
                        num = (byte) (num | 0x20);
                    }
                }
            }
            return num;
        }

        public virtual void Initialize()
        {
            throw new NotImplementedException("The method or operation is not implemented.");
        }

        public void LimitNumberOfTagsDuringRequest(int maxTags)
        {
            this.SetParameter(0x16, maxTags);
        }

        public void lockConfiguration()
        {
            byte[] data = new byte[] { 0x7a };
            this.WriteConfiguration(1, data, 1);
            data[0] = 0x69;
            this.WriteConfiguration(0, data, 1);
            Thread.Sleep(0x3e8);
            data[0] = 0;
            this.WriteConfiguration(1, data, 1);
        }

        public void NANOTRONOFF(int time)
        {
            byte[] buffer = new byte[70];
            buffer[0] = this._byAddress;
            buffer[1] = 0x3e;
            buffer[2] = 2;
            buffer[3] = (byte) time;
            buffer[4] = 0;
            this.DataStream.SendMessage(buffer, 5);
            this.DataStream.ReadMessage(0x3e8).CheckResponse(this._byAddress, 0x3e);
        }

        internal void OnReaderStatusErrorReadStatusAndFireEvent(IC3ProtocolMessage response)
        {
            if (response == null)
            {
                log.Warn("OnReaderStatusErrorReadStatusAndFireEvent response null");
            }
            else if (response.Buffer == null)
            {
                log.Warn("OnReaderStatusErrorReadStatusAndFireEvent buffer null");
            }
            else if (response[4] != 0)
            {
                log.Warn("The device reported a status error");
                this._readerStatus = new iBusDeviceStatus((uint) this.GetParameter(4));
                if (this._readerStatus.HasError)
                {
                    log.Warn("The device reported status: " + this._readerStatus.ToString());
                    base.FireEventModuleStatusError(this, this._readerStatus);
                }
            }
        }

        public virtual bool PingTag(iQ350 tag)
        {
            byte[] readData = new byte[2];
            int nBytesRead = 0;
            try
            {
                readData[0] = 1;
                readData[1] = 0;
                this.ReadTagDataHelper(tag, WakeUpMode.AutoWakeUp, tag.GetWakeUpLength(this, WakeUpMode.AutoWakeUp), 0, out readData, 1, ref nBytesRead);
                return (nBytesRead == 1);
            }
            catch (DeviceException exception)
            {
                if (exception.ResponseCode != 5)
                {
                    throw;
                }
                return false;
            }
        }

        [CLSCompliant(false)]
        public virtual iQ350 PingTag(uint tagID)
        {
            return this.PingTag(tagID, this.WakeUpDuration);
        }

        [CLSCompliant(false)]
        public virtual iQ350 PingTag(uint tagID, TagType type)
        {
            return this.PingTag(tagID, type, this.WakeUpDuration);
        }

        [CLSCompliant(false)]
        public virtual iQ350 PingTag(uint tagID, TimeSpan WakeUpDuration)
        {
            return this.PingTag(tagID, TagType.UNKNOWN, WakeUpDuration);
        }

        [CLSCompliant(false)]
        public virtual iQ350 PingTag(uint tagID, TagType type, TimeSpan WakeUpDuration)
        {
            TimeSpan wakeUpDuration = this.WakeUpDuration;
            log.Debug(string.Concat(new object[] { "Ping Tag ", tagID, " with wake up", WakeUpDuration.ToString() }));
            this.WakeUpDuration = WakeUpDuration;
            ILR350Tag tag = ILR350Tag.TagFactory(type, tagID);
            iQ350 iq = tag as iQ350;
            if (iq == null)
            {
                throw new NotSupportedException("the type of tag " + tag.TypeID + " does not support RW operation ");
            }
            TagReadDataResult result = null;
            try
            {
                result = iq.ReadDataFromRegister(this, Register.REGISTER_15, 4, 2, 0);
            }
            finally
            {
                this.WakeUpDuration = wakeUpDuration;
            }
            if (result.BytesRead != 2)
            {
                return null;
            }
            iQ350 iq2 = ILR350Tag.TagFactory(result.Data[0], (TagType) result.Data[1], tagID) as iQ350;
            if (iq == null)
            {
                throw new NotSupportedException("the type of tag " + tag.TypeID + " does not support RW operation ");
            }
            iq2._status = iq._status;
            iq2._nLastTickCountDuringWakeUp = iq._nLastTickCountDuringWakeUp;
            iq2.MaxSignal = iq.MaxSignal;
            iq2.LastSignal = iq.MaxSignal;
            return iq2;
        }

        public void ReadConfiguration(int address, out byte[] data, byte len)
        {
            data = new byte[0];
            int num = 0;
            byte[] buffer = new byte[0x10];
            buffer[0] = this._byAddress;
            buffer[1] = 0x34;
            buffer[2] = (byte) (address & 0xff);
            buffer[3] = (byte) ((address & 0xff00) >> 8);
            buffer[4] = len;
            int length = 5;
            this.DataStream.SendMessage(buffer, length);
            IC3ProtocolMessage message = this.DataStream.ReadMessage(100);
            message.CheckResponse(this._byAddress, 0x34);
            num = message.MessageLength - 7;
            data = new byte[num];
            Array.Copy(message.Buffer, 4, data, 0, data.Length);
        }

        [CLSCompliant(false)]
        public iPortM350.EEPROMCalibration ReadEEPROMCalibration()
        {
            return this.ReadEEPROMCalibration(AntennaNr.Ant1);
        }

        [CLSCompliant(false)]
        public iPortM350.EEPROMCalibration ReadEEPROMCalibration(AntennaNr antenna)
        {
            byte[] buffer;
            int address = 0x100;
            iPortM350.EEPROMCalibration calibration = new iPortM350.EEPROMCalibration();
            switch (antenna)
            {
                case AntennaNr.Ant1:
                    address = 0x100;
                    break;

                case AntennaNr.Ant2:
                    address = 0x108;
                    break;
            }
            this.ReadConfiguration(address, out buffer, 8);
            if (!IDENTEC.Readers.CRC.CRCok(buffer, 8))
            {
                throw new CRCException("Block CRC is not valid");
            }
            calibration.compatibility = buffer[0];
            calibration.reserved = buffer[1];
            calibration.FrequencyOffset = (short) ((buffer[2] << 8) + buffer[3]);
            calibration.TXOffset = (sbyte) buffer[4];
            calibration.RSSIOffset = (sbyte) buffer[5];
            return calibration;
        }

        [CLSCompliant(false)]
        public iPortM350.EEPROMInfo ReadEEPROMInfo()
        {
            byte[] buffer;
            iPortM350.EEPROMInfo info = new iPortM350.EEPROMInfo();
            this.ReadConfiguration(2, out buffer, 14);
            if (!IDENTEC.Readers.CRC.CRCok(buffer, 14))
            {
                throw new CRCException("Block CRC is not valid");
            }
            info.compatibility = buffer[0];
            info.SN = new byte[10];
            Array.Copy(buffer, 1, info.SN, 0, 10);
            info.FrequencyArea = buffer[11];
            return info;
        }

        [CLSCompliant(false)]
        public iPortM350.EEPROMRTLS ReadEEPROMRTLS()
        {
            byte[] buffer;
            iPortM350.EEPROMRTLS eepromrtls = new iPortM350.EEPROMRTLS();
            this.ReadConfiguration(0x120, out buffer, 0x20);
            if (!IDENTEC.Readers.CRC.CRCok(buffer, 0x20))
            {
                throw new CRCException("Block CRC is not valid");
            }
            eepromrtls.Compatibility = buffer[0];
            eepromrtls.ManufacturerID = new byte[2];
            Array.Copy(buffer, 1, eepromrtls.ManufacturerID, 0, 2);
            eepromrtls.ReaderID = new byte[4];
            Array.Copy(buffer, 3, eepromrtls.ReaderID, 0, 4);
            eepromrtls.Modulation = buffer[7];
            eepromrtls.Reserved = new byte[0x16];
            Array.Copy(buffer, 8, eepromrtls.Reserved, 0, 0x16);
            return eepromrtls;
        }

        public virtual void ReadInfo()
        {
            byte[] buffer = new byte[0x10];
            buffer[0] = this._byAddress;
            buffer[1] = 0x60;
            int length = 2;
            IC3ProtocolMessage response = null;
            lock (this.DataStream.LockObject)
            {
                this.DataStream.SendMessage(buffer, length);
                Thread.Sleep(5);
                response = this.DataStream.ReadMessage(0x7d0);
            }
            response.CheckResponse(this._byAddress, 0x60);
            this.OnReaderStatusErrorReadStatusAndFireEvent(response);
            this.ThrowOnReturnCodeError(response);
            this._ReaderHW = response[5];
            if (response[6] == 0)
            {
                this._SupportedFrequencies = 2;
            }
            else
            {
                this._SupportedFrequencies = (response[6] - 1) & 15;
            }
            int index = 7;
            this._serialNumber = Encoding.UTF8.GetString(response.Buffer, index, 10);
            this._serialNumber = this._serialNumber.Replace("\0", "");
            this._serialNumber = this._serialNumber.TrimEnd(null);
            index += 10;
            this._firmwareVersionMajor = response[index];
            this._firmwareVersionMinor = response[index + 1];
            index += 2;
            this._infoString = Encoding.UTF8.GetString(response.Buffer, index, 20);
            this._infoString = this._infoString.Replace("\0", "");
            this._infoString = this._infoString.TrimEnd(null);
            index += 20;
            this._bootLoaderVersion = string.Format("{0}.{1}", response[index], response[index + 1]);
            index += 2;
            this._protocolVersionMajor = response[index];
            this._protocolVersionMinor = response[index + 1];
            index += 2;
        }

        internal int ReadTagDataHelper(iQ350 tag, WakeUpMode wakeUpMode, TimeSpan wakeUpLength, int address, out byte[] readData, int nByteToRead, ref int nBytesRead)
        {
            int index = 0;
            byte[] buffer = new byte[0x20];
            readData = null;
            nBytesRead = 0;
            buffer[index++] = this._byAddress;
            buffer[index++] = 0x66;
            buffer[index++] = (byte) this._ActiveAntenna;
            buffer[index++] = (byte) this._TXPower;
            buffer[index++] = this.GetWakeUpMode(tag, this.RFBaudRate, wakeUpMode);
            int num2 = index;
            buffer[index++] = Convert.ToByte((double) (wakeUpLength.TotalMilliseconds / 100.0));
            buffer[index] = 15;
            if (address >= 0x10000)
            {
                buffer[index] = (byte) (buffer[index] + 30);
            }
            if (this.RFBaudRate < IDENTEC.ILR350.RFBaudRate.RF_57600)
            {
                buffer[index] = (byte) (buffer[index] + ((byte) (2 + (nByteToRead / 2))));
            }
            else
            {
                buffer[index] = (byte) (buffer[index] + 7);
            }
            index++;
            buffer[index++] = tag.ManufacturerID;
            buffer[index++] = tag.TypeID;
            buffer[index++] = tag._SerialNumber[0];
            buffer[index++] = tag._SerialNumber[1];
            buffer[index++] = tag._SerialNumber[2];
            buffer[index++] = tag._SerialNumber[3];
            buffer[index++] = (byte) this._retries;
            buffer[index++] = (byte) (address & 0xff);
            buffer[index++] = (byte) ((address & 0xff00) >> 8);
            buffer[index++] = (byte) ((address & 0xff0000) >> 0x10);
            buffer[index++] = (byte) nByteToRead;
            tag.MaxSignal = -128;
            tag.LastSignal = -128;
            log.Trace(Environment.TickCount);
            lock (this.DataStream.LockObject)
            {
                this.DataStream.SendMessage(buffer, index);
                int timeout = 0x7d0;
                if ((buffer[num2] != 0) && (wakeUpMode == WakeUpMode.ForceWakeUP))
                {
                    Thread.Sleep((int) (buffer[num2] * 100));
                }
                else
                {
                    if (buffer[num2] > 1)
                    {
                        Thread.Sleep((int) (buffer[num2] * 100));
                    }
                    timeout += buffer[num2] * 100;
                }
                IC3ProtocolMessage response = this.DataStream.ReadMessage(timeout);
                response.CheckResponse(this._byAddress, 0x66);
                nBytesRead = response.Buffer[6];
                if (nByteToRead < nBytesRead)
                {
                    log.Trace("Read more data than requested");
                    throw new ArgumentOutOfRangeException("Read more data than requested");
                }
                readData = new byte[nBytesRead];
                log.Trace("Read " + readData.Length.ToString());
                if ((nBytesRead != 0) || (response[3] == 10))
                {
                    tag._nLastTickCountDuringWakeUp = Environment.TickCount;
                    tag.Status = response.Buffer[5];
                    tag.MaxSignal = (sbyte) response.Buffer[7 + readData.Length];
                    tag.LastSignal = tag.MaxSignal;
                }
                Array.Copy(response.Buffer, 7, readData, 0, readData.Length);
                this.OnReaderStatusErrorReadStatusAndFireEvent(response);
                this.ThrowOnReturnCodeError(response, readData);
            }
            return nBytesRead;
        }

        public void ResetToFactoryDefault()
        {
            try
            {
                base.EventMask = -2049L;
                this.SetParameter(0, 1);
            }
            finally
            {
                base.EventMask = 0xffffffffL;
            }
        }

        public virtual iQ350TagCollection ScanForTags(int estimatedTagCount, bool UseWakeUp)
        {
            TimeSpan wakeUpDuration = this.WakeUpDuration;
            if (this.WakeUpDuration < new TimeSpan(0, 0, 0, 0, 100))
            {
                wakeUpDuration = new TimeSpan(0, 0, 2);
            }
            return this.ScanForTags(estimatedTagCount, UseWakeUp, wakeUpDuration);
        }

        public virtual iQ350TagCollection ScanForTags(int estimatedTagCount, bool UseWakeUp, TimeSpan WakeUpDuration)
        {
            iQ350TagCollection tags = new iQ350TagCollection();
            IC3ProtocolMessage message = null;
            if (WakeUpDuration < TimeSpan.Zero)
            {
                throw new ArgumentOutOfRangeException("cannot use a negative wake up duration");
            }
            if (WakeUpDuration > new TimeSpan(0, 0, 5))
            {
                throw new ArgumentOutOfRangeException("maximum wake up is 5 seconds");
            }
            int length = 0;
            byte[] buffer = new byte[0x20];
            buffer[length++] = this._byAddress;
            buffer[length++] = 0x65;
            buffer[length++] = (byte) this._ActiveAntenna;
            buffer[length++] = (byte) this._TXPower;
            buffer[length++] = this.GetWakeUpMode(null, this.RFBaudRate, UseWakeUp ? WakeUpMode.ForceWakeUP : WakeUpMode.ForceNoWakeUp);
            if (UseWakeUp)
            {
                buffer[length++] = Convert.ToByte((double) (WakeUpDuration.TotalMilliseconds / 100.0));
            }
            else
            {
                buffer[length++] = 0;
            }
            buffer[length++] = (byte) Reader.CalculateSlotSize(estimatedTagCount);
            buffer[length++] = 0;
            buffer[length++] = 0;
            buffer[length++] = 0;
            buffer[length++] = 0;
            buffer[length++] = 0;
            ArrayList list = new ArrayList();
            Exception exception = null;
            DateTime now = DateTime.Now;
            lock (this.DataStream.LockObject)
            {
                try
                {
                    this.DataStream.SendMessage(buffer, length);
                    DateTime time2 = DateTime.Now;
                }
                catch (Exception)
                {
                    throw;
                }
                if (UseWakeUp)
                {
                    Thread.Sleep(Convert.ToInt32(WakeUpDuration.TotalMilliseconds));
                }
                else
                {
                    Thread.Sleep(15);
                }
                int tickCount = Environment.TickCount;
                int num3 = 0;
                switch (this.RFBaudRate)
                {
                    case IDENTEC.ILR350.RFBaudRate.RF_38400:
                        num3 = 0x18c;
                        break;

                    case IDENTEC.ILR350.RFBaudRate.RF_57600:
                        num3 = 0x108;
                        break;

                    case IDENTEC.ILR350.RFBaudRate.RF_115200:
                        num3 = 0x84;
                        break;

                    default:
                        num3 = 0x318;
                        break;
                }
                int timeout = 100 + ((num3 * (((int) 1) << buffer[6])) / 100);
                while (!Reader.TimedOut(ref tickCount, timeout))
                {
                    try
                    {
                        message = this.DataStream.ReadMessage(0x7d0);
                    }
                    catch (ReaderTimeoutException exception2)
                    {
                        if (Reader.TimedOut(ref tickCount, timeout))
                        {
                            log.TraceException("exception ", exception2);
                            throw exception2;
                        }
                        continue;
                    }
                    catch (TimeoutException exception3)
                    {
                        if (Reader.TimedOut(ref tickCount, timeout))
                        {
                            log.TraceException("exception", exception3);
                            throw exception3;
                        }
                        continue;
                    }
                    catch (CRCException exception4)
                    {
                        log.TraceException(exception4.Message + "\n\n" + exception4.Buffer, exception4);
                        exception = exception4;
                        goto Label_02A6;
                    }
                    catch (Exception exception5)
                    {
                        log.TraceException(exception5.Message, exception5);
                        exception = exception5;
                    }
                    if (exception != null)
                    {
                        goto Label_02A6;
                    }
                    if (message[2] != (buffer[1] + 0x80))
                    {
                        exception = new InvalidOperationException("The response contained the incorrect command");
                        goto Label_02A6;
                    }
                    if (message[3] == 0x10)
                    {
                        goto Label_02A6;
                    }
                    list.Add(message);
                }
            }
        Label_02A6:
            log.Trace("Received : " + list.Count + " messages");
            if (exception != null)
            {
                log.Trace("RX exception, " + exception.Message);
            }
            this._nLastTickCountDuringBroadcastCommunication = Environment.TickCount;
            for (int i = 0; i < list.Count; i++)
            {
                message = (IC3ProtocolMessage) list[i];
                uint iD = BitConverter.ToUInt32(message.Buffer, 8);
                iQ350 item = ILR350Tag.TagFactory(message[6], (TagType) message[7], iD) as iQ350;
                if (item != null)
                {
                    item._nLastTickCountDuringWakeUp = this._nLastTickCountDuringBroadcastCommunication;
                    item.Status = message[5];
                    item.BeaconMessage = new byte[0];
                    item.MaxSignal = (sbyte) message[12];
                    item.LastSignal = item.MaxSignal;
                    tags.Add(item);
                }
            }
            if (exception != null)
            {
                throw exception;
            }
            this.OnReaderStatusErrorReadStatusAndFireEvent(message);
            tags.Sort();
            return tags;
        }

        public void Send24GHzCarrier(int power, byte channel, byte baudrate, int time, bool modulated)
        {
            byte[] buffer = new byte[70];
            buffer[0] = this._byAddress;
            buffer[1] = 0x3e;
            buffer[2] = 1;
            if (modulated)
            {
                buffer[3] = 1;
            }
            else
            {
                buffer[3] = 0;
            }
            buffer[4] = channel;
            buffer[5] = baudrate;
            buffer[6] = (byte) power;
            buffer[7] = (byte) time;
            buffer[8] = 0;
            this.DataStream.SendMessage(buffer, 9);
            this.DataStream.ReadMessage(0x3e8).CheckResponse(this._byAddress, 0x3e);
        }

        public void Send24GHzPacket(iQ350 tag, int power, byte channel, byte baudrate, int nbPacket, int periodicity)
        {
            byte[] destinationArray = new byte[70];
            destinationArray[0] = this._byAddress;
            destinationArray[1] = 0x3e;
            destinationArray[2] = 3;
            destinationArray[3] = (byte) power;
            destinationArray[4] = channel;
            destinationArray[5] = baudrate;
            destinationArray[6] = tag.ManufacturerID;
            destinationArray[7] = tag.TypeID;
            Array.Copy(tag._SerialNumber, 0, destinationArray, 8, 4);
            destinationArray[12] = (byte) nbPacket;
            destinationArray[13] = (byte) periodicity;
            this.DataStream.SendMessage(destinationArray, 14);
            this.DataStream.ReadMessage(0x3e8).CheckResponse(this._byAddress, 0x3e);
        }

        public void SendBeacon(int power, IDENTEC.ILR350.RFBaudRate baudrate, ref byte[] dataSent, int len)
        {
            byte[] destinationArray = new byte[70];
            destinationArray[0] = this._byAddress;
            destinationArray[1] = 0x52;
            destinationArray[2] = 0;
            destinationArray[3] = (byte) power;
            destinationArray[4] = (byte) baudrate;
            destinationArray[4] = 0;
            destinationArray[5] = 0;
            destinationArray[6] = (byte) len;
            Array.Copy(dataSent, 0, destinationArray, 7, len);
            this.DataStream.SendMessage(destinationArray, 7 + len);
            IC3ProtocolMessage response = this.DataStream.ReadMessage(0xbb8);
            response.CheckResponse(this._byAddress, 0x52);
            this.ThrowOnReturnCodeError(response);
        }

        public void SendCarrier(int frequency, byte power, byte modulated, byte baudRate)
        {
            byte[] buffer = new byte[70];
            buffer[0] = this._byAddress;
            buffer[1] = 0x3f;
            buffer[2] = modulated;
            buffer[3] = (byte) (frequency & 0xff);
            buffer[4] = (byte) ((frequency & 0xff00) >> 8);
            buffer[5] = (byte) ((frequency & 0xff0000) >> 0x10);
            buffer[6] = baudRate;
            buffer[7] = power;
            this.DataStream.SendMessage(buffer, 8);
            IC3ProtocolMessage response = this.DataStream.ReadMessage(0x3e8);
            response.CheckResponse(this._byAddress, 0x3f);
            this.ThrowOnReturnCodeError(response);
        }

        [CLSCompliant(false)]
        public void SendRF(int power, byte Wake_up_mode, byte Wake_up_length, byte timeout, ref byte[] dataSent, int len, out byte[] dataReceived, ref int rssi, ref sbyte freq_offset)
        {
            dataReceived = null;
            byte[] destinationArray = new byte[70];
            destinationArray[0] = this._byAddress;
            destinationArray[1] = 0x51;
            destinationArray[2] = (byte) this._frequency;
            destinationArray[3] = (byte) power;
            destinationArray[4] = Wake_up_mode;
            destinationArray[5] = Wake_up_length;
            destinationArray[6] = timeout;
            destinationArray[7] = (byte) len;
            Array.Copy(dataSent, 0, destinationArray, 8, len);
            this.DataStream.SendMessage(destinationArray, 8 + len);
            Thread.Sleep(Wake_up_length);
            IC3ProtocolMessage response = this.DataStream.ReadMessage(0x3e8);
            response.CheckResponse(this._byAddress, 0x51);
            this.ThrowOnReturnCodeError(response);
            dataReceived = new byte[response[5]];
            Array.Copy(response.Buffer, 6, dataReceived, 0, dataReceived.Length);
            byte num = 0;
            num = dataReceived[dataReceived.Length - 3];
            if (num >= 0x80)
            {
                rssi = ((num - 0x100) / 2) - 0x4a;
            }
            else
            {
                rssi = (num / 2) - 0x4a;
            }
            freq_offset = (sbyte) dataReceived[dataReceived.Length - 1];
        }

        public void SetAllTagsInListAsNotYetReported()
        {
            this.SetParameter(0x15, 1);
        }

        public void SetBusAddress(int address)
        {
            this.SetParameterPrivate(0x11, (uint) address);
            this._byAddress = (byte) address;
        }

        public bool SetDataLen(int len)
        {
            if ((len < 0) || (len > 50))
            {
                throw new ArgumentOutOfRangeException("Len must be between 0 and 50");
            }
            this.SetParameterPrivate(0x17, (uint) len);
            return true;
        }

        public void SetFrequency(Frequency freq)
        {
            this.SetParameterPrivate(0x20, (uint) freq);
            this.GetFrequency();
            if (this._frequency != freq)
            {
                throw new ArgumentException("Failed to set the frequency or frequency is not supported by device");
            }
        }

        public void SetParameter(int subCommand, int param)
        {
            byte bySubCmd = (byte) subCommand;
            uint dwParameter = (uint) param;
            this.SetParameterPrivate(bySubCmd, dwParameter);
        }

        internal IC3ProtocolMessage SetParameterPrivate(byte bySubCmd, int dwParameter)
        {
            return SetParameterPrivate(bySubCmd, (uint) dwParameter);
        }

        internal IC3ProtocolMessage SetParameterPrivate(byte bySubCmd, uint dwParameter)
        {
            IC3ProtocolMessage message;
            byte[] buffer = new byte[0x20];
            buffer[0] = this._byAddress;
            buffer[1] = 0x63;
            buffer[2] = bySubCmd;
            buffer[3] = (byte) (dwParameter & 0xff);
            buffer[4] = (byte) ((dwParameter >> 8) & 0xff);
            buffer[5] = (byte) ((dwParameter >> 0x10) & 0xff);
            buffer[6] = (byte) ((dwParameter >> 0x18) & 0xff);
            int length = 7;
            lock (this.DataStream.LockObject)
            {
                this.DataStream.SendMessage(buffer, length);
                Thread.Sleep(5);
                message = this.DataStream.ReadMessage(0x7d0);
                message.CheckResponse(this._byAddress, 0x63);
                this.OnReaderStatusErrorReadStatusAndFireEvent(message);
                this.ThrowOnReturnCodeError(message);
            }
            return message;
        }

        public void SetRFBeaconBaudrate(IDENTEC.ILR350.RFBaudRate baudrate)
        {
            this.SetParameterPrivate(0x19, (uint) baudrate);
        }

        public void SetTagListBehavior(TagListBehavior mode)
        {
            this.SetParameter(20, (int) mode);
        }

        public void SetTagListInhibitTime(TimeSpan lifetimeInList)
        {
            int totalSeconds = (int) lifetimeInList.TotalSeconds;
            this.SetParameter(0x12, totalSeconds);
        }

        public void SetTagReReportingInterval(TimeSpan interval)
        {
            int totalSeconds = (int) interval.TotalSeconds;
            this.SetParameter(0x13, totalSeconds);
        }

        public void SetTagSignalFilterLevel(int minSignal)
        {
            if (minSignal < -128)
            {
                throw new ArgumentOutOfRangeException();
            }
            this.SetParameter(0x2b, minSignal);
        }

        internal void ThrowOnReturnCodeError(IC3ProtocolMessage response)
        {
            byte actualCode = response[3];
            if (actualCode == 10)
            {
                this.ThrowOnTagErrorCode(response);
            }
            if (actualCode != 0)
            {
                Exception exception = new DeviceException("The device responded with error code: " + actualCode.ToString(), actualCode);
                log.Debug(exception.Message);
                throw exception;
            }
        }

        internal void ThrowOnReturnCodeError(IC3ProtocolMessage response, byte[] arrayData)
        {
            byte actualCode = response[3];
            if (actualCode == 10)
            {
                this.ThrowOnTagErrorCode(response);
            }
            if (actualCode != 0)
            {
                Exception exception = new DeviceException("The device responded with error code: " + actualCode.ToString(), actualCode, arrayData);
                log.Debug(exception.Message);
                throw exception;
            }
        }

        internal void ThrowOnReturnCodeError(IC3ProtocolMessage response, int nByteProcessed)
        {
            byte actualCode = response[3];
            if (actualCode == 10)
            {
                this.ThrowOnTagErrorCode(response);
            }
            if (actualCode != 0)
            {
                Exception exception = new DeviceException("The device responded with error code: " + actualCode.ToString(), actualCode, nByteProcessed);
                log.Debug(exception.Message);
                throw exception;
            }
        }

        internal void ThrowOnTagErrorCode(IC3ProtocolMessage response)
        {
            byte statusCode = response[5];
            statusCode = (byte) (statusCode & 15);
            if (statusCode != 0)
            {
                Exception exception = new InvalidTagStatusException("The tag responded with error code: " + statusCode.ToString(), statusCode);
                log.Debug(exception.Message);
                throw exception;
            }
        }

        public override string ToString()
        {
            return string.Format("{0}: Serial #: {1} Version {2}", this.Information, this.SerialNumber, this.FirmwareVersion);
        }

        public void UnlockConfiguration()
        {
            byte[] data = new byte[] { 0x7a };
            this.WriteConfiguration(1, data, 1);
            data[0] = 0;
            this.WriteConfiguration(0, data, 1);
        }

        public void WriteConfiguration(int address, byte[] data, byte len)
        {
            byte[] destinationArray = new byte[0xff];
            destinationArray[0] = this._byAddress;
            destinationArray[1] = 0x35;
            destinationArray[2] = (byte) (address & 0xff);
            destinationArray[3] = (byte) ((address & 0xff00) >> 8);
            destinationArray[4] = len;
            Array.Copy(data, 0, destinationArray, 5, len);
            int length = 5 + len;
            this.DataStream.SendMessage(destinationArray, length);
            this.DataStream.ReadMessage(100).CheckResponse(this._byAddress, 0x35);
        }

        [CLSCompliant(false)]
        public void WriteEEPROMCalibration(iPortM350.EEPROMCalibration config)
        {
            this.WriteEEPROMCalibration(config, AntennaNr.Ant1);
        }

        [CLSCompliant(false)]
        public void WriteEEPROMCalibration(iPortM350.EEPROMCalibration config, AntennaNr antenna)
        {
            int address = 0x100;
            byte[] msg = new byte[8];
            switch (antenna)
            {
                case AntennaNr.Ant1:
                    address = 0x100;
                    break;

                case AntennaNr.Ant2:
                    address = 0x108;
                    break;
            }
            this.UnlockConfiguration();
            msg[0] = config.compatibility;
            msg[1] = config.reserved;
            msg[2] = (byte) (config.FrequencyOffset >> 8);
            msg[3] = (byte) (config.FrequencyOffset & 0xff);
            msg[4] = (byte) config.TXOffset;
            msg[5] = (byte) config.RSSIOffset;
            config.CRC = IDENTEC.Readers.CRC.CRC16(msg, 6);
            msg[6] = (byte) (config.CRC & 0xff);
            msg[7] = (byte) (config.CRC >> 8);
            this.WriteConfiguration(address, msg, 8);
            this.lockConfiguration();
        }

        [CLSCompliant(false)]
        public void WriteEEPROMInfo(iPortM350.EEPROMInfo Info)
        {
            byte[] destinationArray = new byte[14];
            this.UnlockConfiguration();
            destinationArray[0] = Info.compatibility;
            Array.Copy(Info.SN, 0, destinationArray, 1, 10);
            destinationArray[11] = Info.FrequencyArea;
            Info.CRC = IDENTEC.Readers.CRC.CRC16(destinationArray, 12);
            destinationArray[12] = (byte) (Info.CRC & 0xff);
            destinationArray[13] = (byte) (Info.CRC >> 8);
            this.WriteConfiguration(2, destinationArray, 14);
            this.lockConfiguration();
        }

        [CLSCompliant(false)]
        public void WriteEEPROMRTLS(iPortM350.EEPROMRTLS rtlsData)
        {
            byte[] destinationArray = new byte[0x20];
            this.UnlockConfiguration();
            destinationArray[0] = rtlsData.Compatibility;
            Array.Copy(rtlsData.ManufacturerID, 0, destinationArray, 1, 2);
            Array.Copy(rtlsData.ReaderID, 0, destinationArray, 3, 4);
            destinationArray[7] = rtlsData.Modulation;
            Array.Copy(rtlsData.Reserved, 0, destinationArray, 8, 0x16);
            rtlsData.CRC = IDENTEC.Readers.CRC.CRC16(destinationArray, 30);
            destinationArray[30] = (byte) (rtlsData.CRC & 0xff);
            destinationArray[0x1f] = (byte) (rtlsData.CRC >> 8);
            this.WriteConfiguration(0x120, destinationArray, 0x20);
            this.lockConfiguration();
        }

        internal int WriteTagDataHelper(iQ350 tag, WakeUpMode wakeUpMode, TimeSpan wakeUpLength, int address, byte[] data, int data_offset, int nByteToWrite, ref int nBytesWritten)
        {
            int index = 0;
            byte[] destinationArray = new byte[300];
            nBytesWritten = 0;
            destinationArray[index++] = this._byAddress;
            destinationArray[index++] = 0x67;
            destinationArray[index++] = (byte) this._ActiveAntenna;
            destinationArray[index++] = (byte) this._TXPower;
            destinationArray[index++] = this.GetWakeUpMode(tag, this.RFBaudRate, wakeUpMode);
            int num2 = index;
            destinationArray[index++] = Convert.ToByte((double) (wakeUpLength.TotalMilliseconds / 100.0));
            destinationArray[index] = 0x19;
            if (address >= 0x10000)
            {
                destinationArray[index] = (byte) (destinationArray[index] + 30);
            }
            if (this.RFBaudRate < IDENTEC.ILR350.RFBaudRate.RF_57600)
            {
                destinationArray[index] = (byte) (destinationArray[index] + 2);
            }
            if ((address == 0xe07800) && (nByteToWrite == 8))
            {
                destinationArray[index] = 0;
            }
            index++;
            destinationArray[index++] = tag.ManufacturerID;
            destinationArray[index++] = tag.TypeID;
            destinationArray[index++] = tag._SerialNumber[0];
            destinationArray[index++] = tag._SerialNumber[1];
            destinationArray[index++] = tag._SerialNumber[2];
            destinationArray[index++] = tag._SerialNumber[3];
            destinationArray[index++] = (byte) this._retries;
            destinationArray[index++] = (byte) (address & 0xff);
            destinationArray[index++] = (byte) ((address & 0xff00) >> 8);
            destinationArray[index++] = (byte) ((address & 0xff0000) >> 0x10);
            destinationArray[index++] = (byte) nByteToWrite;
            Array.Copy(data, data_offset, destinationArray, index, nByteToWrite);
            tag.MaxSignal = -128;
            tag.LastSignal = -128;
            lock (this.DataStream.LockObject)
            {
                this.DataStream.SendMessage(destinationArray, index + nByteToWrite);
                int timeout = 0x7d0;
                if ((destinationArray[num2] != 0) && (wakeUpMode == WakeUpMode.ForceWakeUP))
                {
                    Thread.Sleep((int) (destinationArray[num2] * 100));
                }
                else
                {
                    if (destinationArray[num2] > 1)
                    {
                        Thread.Sleep((int) (destinationArray[num2] * 100));
                    }
                    timeout += destinationArray[num2] * 100;
                }
                IC3ProtocolMessage response = this.DataStream.ReadMessage(timeout);
                response.CheckResponse(this._byAddress, 0x67);
                nBytesWritten = response.Buffer[6];
                if ((nBytesWritten != 0) || (response[3] == 10))
                {
                    tag._nLastTickCountDuringWakeUp = Environment.TickCount;
                    tag.Status = response.Buffer[5];
                    tag.MaxSignal = (sbyte) response.Buffer[7];
                    tag.LastSignal = tag.MaxSignal;
                }
                this.OnReaderStatusErrorReadStatusAndFireEvent(response);
                this.ThrowOnReturnCodeError(response, nBytesWritten);
            }
            return nBytesWritten;
        }

        public int Address
        {
            get
            {
                return this._byAddress;
            }
            set
            {
                this._byAddress = (byte) value;
            }
        }

        public int Antenna
        {
            get
            {
                return (this._ActiveAntenna + 1);
            }
            set
            {
                if (value > this.AntennaCount)
                {
                    throw new ArgumentOutOfRangeException("value must be <" + this.AntennaCount.ToString());
                }
                if (value <= 0)
                {
                    throw new ArgumentOutOfRangeException("value must be > 0");
                }
                this._ActiveAntenna = value - 1;
            }
        }

        public int AntennaCount
        {
            get
            {
                return this._antennaCount;
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

        public string FirmwareVersion
        {
            get
            {
                return (this._firmwareVersionMajor.ToString() + "." + this._firmwareVersionMinor.ToString().PadLeft(2, '0'));
            }
        }

        public int HWType
        {
            get
            {
                return this._ReaderHW;
            }
        }

        public string Information
        {
            get
            {
                return this._infoString;
            }
            internal set
            {
                this._infoString = value;
            }
        }

        public bool IsInternalBuild
        {
            get
            {
                return true;
            }
        }

        public int LinkQualityIndicator
        {
            get
            {
                return this._nLastLQI;
            }
        }

        public int MajorVersion
        {
            get
            {
                return this._firmwareVersionMajor;
            }
        }

        internal int MaxReadPacket
        {
            get
            {
                return this._MaxReadPacket;
            }
        }

        internal int MaxWritePacket
        {
            get
            {
                return this._MaxWritePacket;
            }
        }

        public int MinorVersion
        {
            get
            {
                return this._firmwareVersionMinor;
            }
        }

        public string ProtocolVersion
        {
            get
            {
                return (this._protocolVersionMajor.ToString() + "." + this._protocolVersionMinor.ToString().PadLeft(2, '0'));
            }
        }

        public int Retries
        {
            get
            {
                return this._retries;
            }
            set
            {
                if (value < 0)
                {
                    throw new ArgumentOutOfRangeException("minimum value is 0");
                }
                if (value > 10)
                {
                    throw new ArgumentOutOfRangeException("maximum value is 10");
                }
                this._retries = value;
            }
        }

        public IDENTEC.ILR350.RFBaudRate RFBaudRate
        {
            get
            {
                return this._rfBaudrate;
            }
            set
            {
                if (value > IDENTEC.ILR350.RFBaudRate.RF_115200)
                {
                    throw new ArgumentOutOfRangeException("invalid baudrate");
                }
                if (value < IDENTEC.ILR350.RFBaudRate.RF_19200)
                {
                    throw new ArgumentOutOfRangeException("invalid baudrate");
                }
                this._rfBaudrate = value;
                Thread.Sleep(500);
            }
        }

        public string SerialNumber
        {
            get
            {
                return this._serialNumber;
            }
        }

        public iBusDeviceStatus Status
        {
            get
            {
                return this._readerStatus;
            }
        }

        public Frequency SupportedFrequencies
        {
            get
            {
                return (Frequency) this._SupportedFrequencies;
            }
            internal set
            {
                this._SupportedFrequencies = (int) value;
            }
        }

        public int TXPower
        {
            get
            {
                return this._TXPower;
            }
            set
            {
                if (value > 10)
                {
                    throw new ArgumentOutOfRangeException("maximum TX power is +10");
                }
                if (value < -30)
                {
                    throw new ArgumentOutOfRangeException("minimum TX power is -30");
                }
                this._TXPower = value;
            }
        }

        public TimeSpan WakeUpDuration
        {
            get
            {
                return this._WakeUpLength;
            }
            set
            {
                if (value < TimeSpan.Zero)
                {
                    throw new ArgumentOutOfRangeException("minimum wake up is 0");
                }
                if (value > new TimeSpan(0, 0, 5))
                {
                    throw new ArgumentOutOfRangeException("maximum wake up is 5 seconds");
                }
                this._WakeUpLength = value;
            }
        }

        public enum AntennaNr
        {
            Ant1,
            Ant2
        }
    }
}

