namespace IDENTEC.ILR350.Tags
{
    using IDENTEC;
    using IDENTEC.ILR350;
    using IDENTEC.ILR350.Readers;
    using IDENTEC.ILR350.Tags.Info;
    using IDENTEC.Readers;
    using IDENTEC.Tags;
    using System;
    using System.Collections;
    using System.ComponentModel;
    using System.Runtime.InteropServices;
    using System.Text;
    using System.Threading;

    public class iQ350 : ILR350Tag, IWritableInterface
    {
        internal int _nLastTickCountDuringWakeUp;
        private int _ResetCounter;
        private string _Version;
        internal static readonly TimeSpan MAX_AWAKE_TIME = new TimeSpan(0, 0, 0, 0, 500);
        internal static readonly TimeSpan MAX_FAST_SNIFF_TIME = new TimeSpan(0, 0, 5);
        internal static readonly int RETRIES = 2;

        public iQ350()
        {
            this._Version = string.Empty;
            this._nLastTickCountDuringWakeUp = Environment.TickCount - 0x2710;
        }

        public iQ350(iQ350 tag) : base(tag)
        {
            this._Version = string.Empty;
            this._nLastTickCountDuringWakeUp = Environment.TickCount - 0x2710;
            this._Version = tag._Version;
            this._nLastTickCountDuringWakeUp = tag._nLastTickCountDuringWakeUp;
            this._ResetCounter = tag._ResetCounter;
        }

        public void AlarmClear(ILR350Reader reader, AlarmFlag AlarmToClear)
        {
            this.WriteAlarmConfiguration(reader, AlarmFlag.None, AlarmFlag.None, AlarmToClear);
        }

        public virtual bool BlinkLED(ILR350Reader reader, LEDColor color, TimeSpan interval, byte amount)
        {
            return this.BlinkLED(reader, color, interval, amount, RETRIES);
        }

        public virtual bool BlinkLED(ILR350Reader reader, LEDColor color, TimeSpan interval, byte amount, int retries)
        {
            TagWriteDataResult result = new TagWriteDataResult();
            byte[] data = new byte[] { amount, (byte) (interval.TotalMilliseconds / 100.0), (byte) color };
            return this.WriteDataToRegister(reader, Register.REGISTER_1, 12, data, 3, retries).Success;
        }

        internal TimeSpan GetWakeUpLength(ILR350Reader reader, WakeUpMode wakeUpMode)
        {
            TimeSpan wakeUpTime = new TimeSpan(0, 0, 0, 0, 100);
            TagDescription description = null;
            if (wakeUpMode == WakeUpMode.ForceNoWakeUp)
            {
                ILR350Tag.log.Trace("Forced no wake up");
                return TimeSpan.Zero;
            }
            ILR350Tag.Description.TryGetValue(this.TypeID, out description);
            TimeSpan zero = TimeSpan.Zero;
            if (description != null)
            {
                wakeUpTime = description.WakeUpTime;
                ILR350Tag.log.Trace("wake up len from des" + wakeUpTime.TotalMilliseconds);
                zero = description.FastSniffTime;
            }
            else
            {
                wakeUpTime = new TimeSpan(0, 0, 0, 2);
                zero = MAX_FAST_SNIFF_TIME;
                ILR350Tag.log.Trace("Default wake up len " + wakeUpTime.TotalMilliseconds);
            }
            int elapsedTime = Reader.GetElapsedTime(ref this._nLastTickCountDuringWakeUp);
            int num2 = Reader.GetElapsedTime(ref reader._nLastTickCountDuringBroadcastCommunication);
            if ((elapsedTime < (zero.TotalMilliseconds - 120.0)) || (num2 < (zero.TotalMilliseconds - 120.0)))
            {
                wakeUpTime = new TimeSpan(0, 0, 0, 0, 100);
                ILR350Tag.log.Trace(string.Concat(new object[] { "tag still awake ", Environment.TickCount, " ", elapsedTime, " ", num2, " ", zero.TotalMilliseconds }));
                return wakeUpTime;
            }
            ILR350Tag.log.Trace(string.Concat(new object[] { "tag sleeping ", Environment.TickCount, " ", elapsedTime, " ", num2, " ", zero.TotalMilliseconds }));
            if ((elapsedTime < zero.TotalMilliseconds) || (num2 < zero.TotalMilliseconds))
            {
                Thread.Sleep(120);
            }
            if (reader.WakeUpDuration != TimeSpan.Zero)
            {
                wakeUpTime = reader.WakeUpDuration;
                ILR350Tag.log.Trace("wake up len from reader " + wakeUpTime.TotalMilliseconds);
            }
            if ((base.Status != -1) && ((base.Status & 0x30) != 0))
            {
                switch (((byte) ((base.Status & 0x30) >> 4)))
                {
                    case 1:
                        wakeUpTime = new TimeSpan(0, 0, 0, 0, 200);
                        break;

                    case 2:
                        wakeUpTime = new TimeSpan(0, 0, 0, 1);
                        break;

                    case 3:
                        wakeUpTime = new TimeSpan(0, 0, 0, 2);
                        break;
                }
                ILR350Tag.log.Trace("wake up len from tag " + wakeUpTime.TotalMilliseconds);
            }
            ILR350Tag.log.Trace("wake up used " + wakeUpTime.TotalMilliseconds);
            return wakeUpTime;
        }

        public AlarmInfo ReadAlarmReadInfo(ILR350Reader reader)
        {
            TagReadDataResult result = this.ReadDataFromRegister(reader, Register.REGISTER_1, 0x13, 3, RETRIES);
            if (!result.Success)
            {
                throw new Exception("Unable to read the data");
            }
            AlarmFlag triggeredAlarm = (AlarmFlag) result.Data[0];
            return new AlarmInfo((AlarmFlag) result.Data[1], triggeredAlarm);
        }

        public bool ReadBeaconActivationState(ILR350Reader reader)
        {
            TagReadDataResult result = this.ReadDataFromRegister(reader, Register.REGISTER_2, 0x7f, 1, RETRIES);
            if (!result.Success)
            {
                throw new Exception("Unable to read the data");
            }
            if (result.Data[0] == 0)
            {
                return false;
            }
            return true;
        }

        public void ReadBeaconConfiguration(ILR350Reader reader, out BeaconInformation info, out TimeSpan interval, out byte[] data)
        {
            info = BeaconInformation.None;
            interval = TimeSpan.Zero;
            data = null;
            TagReadDataResult result = this.ReadDataFromRegister(reader, Register.REGISTER_2, 0, 0x36, RETRIES);
            if (!result.Success)
            {
                throw new Exception("Unable to read the data");
            }
            int index = 1;
            if (this.TypeID == 3)
            {
                interval = new TimeSpan(0, 0, result.Data[0]);
            }
            else
            {
                index = 2;
                ushort num2 = result.Data[0];
                num2 = (ushort) (num2 << 8);
                num2 = (ushort) (num2 + result.Data[1]);
                interval = TimeSpan.FromMilliseconds(num2 * 500.0);
            }
            info = (BeaconInformation) result.Data[index++];
            if (result.Data[index] > 50)
            {
                result.Data[index] = 0;
            }
            data = new byte[result.Data[index]];
            Array.Copy(result.Data, index + 1, data, 0, data.Length);
        }

        public virtual TimeSpan ReadBeaconInterval(ILR350Reader reader)
        {
            TagReadDataResult result = new TagReadDataResult();
            result = this.ReadDataFromRegister(reader, Register.REGISTER_2, 0, 2, RETRIES);
            if (!result.Success)
            {
                throw new Exception("Unable to read the data");
            }
            if (this.TypeID == 3)
            {
                return new TimeSpan(0, 0, result.Data[0]);
            }
            ushort num = result.Data[0];
            num = (ushort) (num << 8);
            num = (ushort) (num + result.Data[1]);
            return TimeSpan.FromMilliseconds(num * 500.0);
        }

        public virtual byte[] ReadBeaconUserData(ILR350Reader reader)
        {
            TagReadDataResult result = this.ReadDataFromRegister(reader, Register.REGISTER_2, 3, 0x33, RETRIES);
            if (!result.Success)
            {
                throw new Exception("Unable to read the data");
            }
            if (result.Data[0] > 50)
            {
                result.Data[0] = 0;
            }
            if (result.Data[0] == 0)
            {
                return new byte[0];
            }
            byte[] destinationArray = new byte[result.Data[0]];
            Array.Copy(result.Data, 1, destinationArray, 0, destinationArray.Length);
            return destinationArray;
        }

        public virtual TagReadDataResult ReadData(ILR350Reader reader, int address, int bytesToRead, int retries)
        {
            return this.ReadData(reader, address, bytesToRead, retries, WakeUpMode.AutoWakeUp);
        }

        public virtual TagReadDataResult ReadData(ILR350Reader reader, int address, int bytesToRead, int retries, WakeUpMode wakeUpMode)
        {
            if (reader == null)
            {
                throw new NullReferenceException("No reader asigned to this tag");
            }
            if (bytesToRead == 0)
            {
                throw new ArgumentOutOfRangeException("Cannot read 0 byte");
            }
            int nByteToRead = 0;
            int bytesRead = 0;
            int nBytesRead = 0;
            byte[] data = new byte[0];
            ArrayList list = new ArrayList();
            byte[] readData = null;
            while (bytesRead < bytesToRead)
            {
                nByteToRead = bytesToRead - bytesRead;
                if (nByteToRead > reader.MaxReadPacket)
                {
                    nByteToRead = reader.MaxReadPacket;
                }
                try
                {
                    reader.ReadTagDataHelper(this, wakeUpMode, this.GetWakeUpLength(reader, wakeUpMode), address + bytesRead, out readData, nByteToRead, ref nBytesRead);
                    if (nBytesRead != 0)
                    {
                        wakeUpMode = WakeUpMode.AutoWakeUp;
                    }
                    else if (retries-- <= 0)
                    {
                        data = (byte[]) list.ToArray(typeof(byte));
                        return new TagReadDataResult(data, false, bytesRead, address);
                    }
                    for (int i = 0; i < nBytesRead; i++)
                    {
                        list.Add(readData[i]);
                    }
                    bytesRead += nBytesRead;
                    ILR350Tag.log.Trace(string.Concat(new object[] { "Read chunk:", nBytesRead.ToString(), " total:", bytesRead.ToString(), " requested:", bytesToRead }));
                    continue;
                }
                catch (DeviceException exception)
                {
                    ILR350Tag.log.DebugException("Failed to read " + nByteToRead.ToString(), exception);
                    if (exception.ResponseCode == 10)
                    {
                        throw;
                    }
                    if (exception.ResponseCode == 9)
                    {
                        throw;
                    }
                    for (int j = 0; j < exception.ResponseData.Length; j++)
                    {
                        list.Add(exception.ResponseData[j]);
                    }
                    bytesRead += exception.ResponseData.Length;
                    if (exception.ResponseData.Length != 0)
                    {
                        wakeUpMode = WakeUpMode.AutoWakeUp;
                    }
                    if (retries-- <= 0)
                    {
                        return new TagReadDataResult((byte[]) list.ToArray(typeof(byte)), false, bytesRead, address, exception);
                    }
                    continue;
                }
            }
            return new TagReadDataResult((byte[]) list.ToArray(typeof(byte)), true, bytesRead, address);
        }

        public virtual TagReadDataResult ReadDataFromRegister(ILR350Reader reader, Register register, int address, int bytesToRead, int retries)
        {
            return this.ReadData(reader, (((byte) register) << 20) + address, bytesToRead, retries, WakeUpMode.AutoWakeUp);
        }

        public virtual TagReadDataResult ReadDataFromRegister(ILR350Reader reader, Register register, int address, int bytesToRead, int retries, WakeUpMode wakeUpMode)
        {
            return this.ReadData(reader, (((byte) register) << 20) + address, bytesToRead, retries, wakeUpMode);
        }

        public bool ReadEventConfiguration(ILR350Reader reader, EventType Type, out BeaconInformation config, out TimeSpan SlotSize, out int burstCount)
        {
            config = BeaconInformation.Marker;
            SlotSize = TimeSpan.Zero;
            burstCount = 0;
            int address = 0;
            switch (Type)
            {
                case EventType.Marker:
                    address = 0x40;
                    break;

                case EventType.DigitalIO:
                    address = 0x48;
                    break;

                case EventType.MotionSensor:
                    address = 0x60;
                    break;

                default:
                    throw new ArgumentOutOfRangeException("Unknown type of event");
            }
            TagReadDataResult result = this.ReadDataFromRegister(reader, Register.REGISTER_2, address, 3, RETRIES);
            if (!result.Success)
            {
                return false;
            }
            config = (BeaconInformation) result.Data[2];
            burstCount = result.Data[0];
            SlotSize = new TimeSpan(0, 0, 0, 0, 100 * result.Data[1]);
            if ((Type == EventType.Marker) && (result.Data[1] == 0))
            {
                SlotSize = new TimeSpan(0, 0, 0, 0, 40);
            }
            return true;
        }

        public string ReadVersion(ILR350Reader reader)
        {
            ASCIIEncoding encoding = new ASCIIEncoding();
            TagReadDataResult result = this.ReadDataFromRegister(reader, Register.REGISTER_15, 0x20, 20, RETRIES);
            if (result.BytesRead == 20)
            {
                this._Version = encoding.GetString(result.Data, 0, result.Data.Length);
            }
            return this._Version;
        }

        public virtual bool ResetBattery(ILR350Reader reader)
        {
            TagWriteDataResult result = new TagWriteDataResult();
            byte[] data = new byte[] { 60, 0x86 };
            return this.WriteDataToRegister(reader, Register.REGISTER_1, 5, data, 2, RETRIES).Success;
        }

        public void WriteAlarmConfiguration(ILR350Reader reader, AlarmFlag AlarmToActivate, AlarmFlag AlarmToDeActivate, AlarmFlag AlarmToClear)
        {
            TagWriteDataResult result = new TagWriteDataResult();
            byte[] data = new byte[3];
            if (((byte) (AlarmToActivate & AlarmToDeActivate)) != 0)
            {
                throw new ArgumentException("Cannot activate and deactivate the same alarm");
            }
            data[0] = (byte) AlarmToClear;
            data[1] = (byte) AlarmToActivate;
            data[2] = (byte) AlarmToDeActivate;
            if (!this.WriteDataToRegister(reader, Register.REGISTER_1, 0x13, data, 3, RETRIES).Success)
            {
                throw new Exception("Unable to write the data");
            }
        }

        public void WriteBeaconActivationState(ILR350Reader reader, bool Activate)
        {
            TagWriteDataResult result = new TagWriteDataResult();
            byte[] data = new byte[] { 0 };
            if (Activate)
            {
                data[0] = 0xff;
            }
            if (!this.WriteDataToRegister(reader, Register.REGISTER_2, 0x7f, data, 1, RETRIES).Success)
            {
                throw new Exception("Unable to write the data");
            }
        }

        public bool WriteBeaconConfiguration(ILR350Reader reader, BeaconInformation info)
        {
            TagWriteDataResult result = new TagWriteDataResult();
            byte[] data = new byte[] { (byte)info };
            return this.WriteDataToRegister(reader, Register.REGISTER_2, 2, data, 1, RETRIES).Success;
        }

        public virtual bool WriteBeaconInterval(ILR350Reader reader, TimeSpan interval)
        {
            if (interval > new TimeSpan(0, 5, 0))
            {
                throw new ArgumentOutOfRangeException("interval must be <= 5 minutes");
            }
            if (interval < TimeSpan.Zero)
            {
                throw new ArgumentOutOfRangeException("interval must be > 0");
            }
            TagWriteDataResult result = new TagWriteDataResult();
            byte[] data = new byte[2];
            if (this.TypeID == 3)
            {
                data[0] = (byte) interval.TotalSeconds;
                result = this.WriteDataToRegister(reader, Register.REGISTER_2, 0, data, 1, RETRIES);
            }
            else
            {
                ushort num = (ushort) (interval.TotalMilliseconds / 500.0);
                data[0] = (byte) (num >> 8);
                data[1] = (byte) num;
                result = this.WriteDataToRegister(reader, Register.REGISTER_2, 0, data, 2, RETRIES);
            }
            return result.Success;
        }

        public virtual bool WriteBeaconUserData(ILR350Reader reader, byte[] data, int len)
        {
            if (len > 50)
            {
                throw new ArgumentOutOfRangeException("user data len canoot exceed 50 bytes");
            }
            if (data.Length < len)
            {
                throw new ArgumentOutOfRangeException("not enough data provided");
            }
            TagWriteDataResult result = new TagWriteDataResult();
            byte[] destinationArray = new byte[len + 1];
            destinationArray[0] = (byte) len;
            Array.Copy(data, 0, destinationArray, 1, len);
            return this.WriteDataToRegister(reader, Register.REGISTER_2, 3, destinationArray, destinationArray.Length, RETRIES).Success;
        }

        public virtual TagWriteDataResult WriteData(ILR350Reader reader, int address, byte[] data, int bytesToWrite, int retries)
        {
            return this.WriteData(reader, address, data, bytesToWrite, retries, WakeUpMode.AutoWakeUp);
        }

        public virtual TagWriteDataResult WriteData(ILR350Reader reader, int address, byte[] data, int bytesToWrite, int retries, WakeUpMode wakeUpMode)
        {
            if (reader == null)
            {
                throw new NullReferenceException("No reader asigned to this tag");
            }
            if (bytesToWrite == 0)
            {
                throw new ArgumentOutOfRangeException("Cannot write 0 byte");
            }
            TagWriteDataResult result = new TagWriteDataResult();
            int nByteToWrite = 0;
            int num2 = 0;
            int nBytesWritten = 0;
            while (num2 < bytesToWrite)
            {
                nByteToWrite = bytesToWrite - num2;
                if (nByteToWrite > reader.MaxWritePacket)
                {
                    nByteToWrite = reader.MaxWritePacket;
                }
                try
                {
                    reader.WriteTagDataHelper(this, wakeUpMode, this.GetWakeUpLength(reader, wakeUpMode), address + num2, data, num2, nByteToWrite, ref nBytesWritten);
                    if (nBytesWritten != 0)
                    {
                        wakeUpMode = WakeUpMode.AutoWakeUp;
                    }
                    else if (retries-- <= 0)
                    {
                        result.BytesWritten = num2;
                        result.Success = false;
                        return result;
                    }
                    num2 += nBytesWritten;
                    ILR350Tag.log.Trace("written :" + num2.ToString() + " :" + nBytesWritten.ToString());
                    continue;
                }
                catch (DeviceException exception)
                {
                    ILR350Tag.log.Debug("Failed to write " + nByteToWrite.ToString() + " bytes, wrote " + num2.ToString() + " " + exception.Message, exception);
                    if (exception.ResponseCode == 10)
                    {
                        throw;
                    }
                    if (exception.ResponseCode == 9)
                    {
                        throw;
                    }
                    num2 += exception.TotalByteProcessed;
                    if (exception.TotalByteProcessed != 0)
                    {
                        wakeUpMode = WakeUpMode.AutoWakeUp;
                    }
                    if (retries-- <= 0)
                    {
                        result.BytesWritten = num2;
                        result.Success = false;
                        return result;
                    }
                    continue;
                }
            }
            result.BytesWritten = num2;
            result.Success = true;
            return result;
        }

        public virtual TagWriteDataResult WriteDataToRegister(ILR350Reader reader, Register register, int address, byte[] data, int bytesToWrite, int retries)
        {
            return this.WriteData(reader, (((byte) register) << 20) + address, data, bytesToWrite, retries, WakeUpMode.AutoWakeUp);
        }

        public virtual TagWriteDataResult WriteDataToRegister(ILR350Reader reader, Register register, int address, byte[] data, int bytesToWrite, int retries, WakeUpMode wakeUpMode)
        {
            return this.WriteData(reader, (((byte) register) << 20) + address, data, bytesToWrite, retries, wakeUpMode);
        }

        public bool WriteEventConfiguration(ILR350Reader reader, EventType Type, BeaconInformation config, TimeSpan SlotSize, int burstCount)
        {
            if (burstCount > 15)
            {
                throw new ArgumentOutOfRangeException(" Number of burst is limited to 15");
            }
            if (burstCount < 0)
            {
                throw new ArgumentOutOfRangeException(" Number of burst must be positive");
            }
            if ((SlotSize < TimeSpan.Zero) || (SlotSize > new TimeSpan(0, 1, 30)))
            {
                throw new ArgumentOutOfRangeException("interval must be between 0 and 1.5 minutes");
            }
            int address = 0;
            byte[] data = new byte[] { (byte) burstCount, (byte) (SlotSize.TotalMilliseconds / 100.0), (byte)config };
            switch (Type)
            {
                case EventType.Marker:
                    if (burstCount == 0)
                    {
                        throw new ArgumentOutOfRangeException(" Number of burst cannot be 0");
                    }
                    config = (BeaconInformation) ((byte) (config | BeaconInformation.Marker));
                    if (SlotSize == new TimeSpan(0, 0, 0, 0, 40))
                    {
                        data[1] = 0;
                    }
                    address = 0x40;
                    break;

                case EventType.DigitalIO:
                    config = (BeaconInformation) ((byte) (config | BeaconInformation.DigitalIO));
                    address = 0x48;
                    break;

                case EventType.MotionSensor:
                    config = (BeaconInformation) ((byte) (config | BeaconInformation.MotionSensor));
                    address = 0x60;
                    break;

                default:
                    throw new ArgumentOutOfRangeException("Unknown type of event");
            }
            TagWriteDataResult result = new TagWriteDataResult();
            return this.WriteDataToRegister(reader, Register.REGISTER_2, address, data, 3, RETRIES).Success;
        }

        public int ResetCounter
        {
            get
            {
                return this._ResetCounter;
            }
            set
            {
                this._ResetCounter = value;
            }
        }

        [Browsable(false)]
        public virtual string Version
        {
            get
            {
                return this._Version;
            }
        }
    }
}

