namespace IDENTEC.ILR350.Readers
{
    using IDENTEC;
    using IDENTEC.ILR350.Tags;
    using NLog;
    using System;
    using System.Collections;
    using System.Runtime.InteropServices;
    using System.Threading;

    public class iSAT : iBusModule, IBusDevice, RTLSDevice
    {
        protected static Logger log = LogManager.GetLogger("iSAT");
        private byte m_BaudRate24GHz;
        private byte m_Channel24GHz;
        private byte m_Retries24GHz;
        private byte m_Timeout;
        private byte m_TXPower24GHz;
        private ILR350Reader M350Part;

        public iSAT(iBusAdapter bus)
        {
            this.m_TXPower24GHz = 20;
            this.m_Retries24GHz = 5;
            this.m_Timeout = 20;
            this.M350Part = new IDENTEC.ILR350.Readers.iPortM350(bus);
        }

        internal iSAT(ILR350Reader M350)
        {
            this.m_TXPower24GHz = 20;
            this.m_Retries24GHz = 5;
            this.m_Timeout = 20;
            this.M350Part = M350;
        }

        public void ConnectSlavePort(bool enable)
        {
            this.M350Part.ConnectSlavePort(enable);
        }

        public bool EnterReflashMode()
        {
            return this.M350Part.EnterReflashMode();
        }

        public int GetApplicationID()
        {
            uint dwParameter = 0;
            this.M350Part.GetParameterPrivate(0x36, ref dwParameter);
            return (int) dwParameter;
        }

        public byte GetBusAddress()
        {
            return this.M350Part.GetBusAddress();
        }

        public int GetRangingTXPower()
        {
            uint dwParameter = 0;
            this.M350Part.GetParameterPrivate(0x3b, ref dwParameter);
            return (int) dwParameter;
        }

        public bool GetRTLSrssiMargin()
        {
            uint dwParameter = 0;
            this.M350Part.GetParameterPrivate(0x3d, ref dwParameter);
            return (dwParameter == 1);
        }

        public int GetRTLSSignalFilterLevel()
        {
            uint dwParameter = 0;
            this.M350Part.GetParameterPrivate(60, ref dwParameter);
            return (int) dwParameter;
        }

        public iBusDeviceStatus GetStatus()
        {
            return this.M350Part.GetStatus();
        }

        public void Initialize()
        {
            log.Trace("Initialize");
            this.M350Part.Initialize();
        }

        public void lockConfiguration()
        {
            this.M350Part.lockConfiguration();
        }

        public void NANOTRONOFF(int time)
        {
            this.M350Part.NANOTRONOFF(time);
        }

        public void ReadConfiguration(int address, out byte[] data, byte len)
        {
            this.M350Part.ReadConfiguration(address, out data, len);
        }

        public virtual int ReadData24GHz(byte ManufID, byte TypeID, long ID, int address, int bytesToRead, out byte[] data, out int RSSI)
        {
            int nByteToRead = 0;
            int num2 = 0;
            int num3 = 0;
            int num4 = 5;
            ArrayList list = new ArrayList();
            byte[] buffer = null;
            RSSI = -128;
            while (num2 < bytesToRead)
            {
                nByteToRead = bytesToRead - num2;
                if (nByteToRead > 0x7c)
                {
                    nByteToRead = 0x7c;
                }
                try
                {
                    int rSSI = 0;
                    num3 = this.ReadData24GHzHelper(ManufID, TypeID, (uint) ID, address + num2, nByteToRead, out buffer, out rSSI);
                    if (num3 == 0)
                    {
                        if (num4-- <= 0)
                        {
                            data = (byte[]) list.ToArray(typeof(byte));
                            return -1;
                        }
                    }
                    else
                    {
                        RSSI = rSSI;
                        num4 = 2;
                    }
                    for (int i = 0; i < num3; i++)
                    {
                        list.Add(buffer[i]);
                    }
                    num2 += num3;
                    log.Trace(string.Concat(new object[] { "Read chunk:", num3.ToString(), " total:", num2.ToString(), " requested:", bytesToRead }));
                    continue;
                }
                catch (DeviceException exception)
                {
                    log.DebugException("Failed to read " + nByteToRead.ToString(), exception);
                    if (exception.ResponseCode == 9)
                    {
                        throw;
                    }
                    if (exception.ResponseData != null)
                    {
                        for (int j = 0; j < exception.ResponseData.Length; j++)
                        {
                            list.Add(exception.ResponseData[j]);
                        }
                        num2 += exception.ResponseData.Length;
                    }
                    if (num4-- <= 0)
                    {
                        data = (byte[]) list.ToArray(typeof(byte));
                        return exception.ResponseCode;
                    }
                    continue;
                }
            }
            data = (byte[]) list.ToArray(typeof(byte));
            return 0;
        }

        internal int ReadData24GHzHelper(byte ManufID, byte TypeID, uint ID, int address, int nByteToRead, out byte[] data, out int RSSI)
        {
            int num = 0;
            if ((nByteToRead < 1) || (nByteToRead > 0x7d))
            {
                throw new ArgumentOutOfRangeException("Cannot only read 1 to 125 bytes");
            }
            RSSI = -128;
            int length = 0;
            byte[] buffer = new byte[0x80];
            buffer[length++] = (byte) this.Address;
            buffer[length++] = 0x70;
            buffer[length++] = 0;
            buffer[length++] = this.m_TXPower24GHz;
            buffer[length++] = this.m_Channel24GHz;
            buffer[length++] = this.m_BaudRate24GHz;
            buffer[length++] = this.m_Timeout;
            buffer[length++] = 0;
            buffer[length++] = 0;
            buffer[length++] = ManufID;
            buffer[length++] = TypeID;
            buffer[length++] = (byte) (ID & 0xff);
            buffer[length++] = (byte) ((ID & 0xff00) >> 8);
            buffer[length++] = (byte) ((ID & 0xff0000) >> 0x10);
            buffer[length++] = (byte) ((ID & -16777216) >> 0x18);
            buffer[length++] = this.m_Retries24GHz;
            buffer[length++] = (byte) (address & 0xff);
            buffer[length++] = (byte) ((address & 0xff00) >> 8);
            buffer[length++] = (byte) ((address & 0xff0000) >> 0x10);
            buffer[length++] = (byte) nByteToRead;
            lock (this.DataStream.LockObject)
            {
                this.M350Part.DataStream.SendMessage(buffer, length);
                int timeout = ((this.m_Timeout * this.m_Retries24GHz) * 10) + 0x7d0;
                IC3ProtocolMessage response = this.M350Part.DataStream.ReadMessage(timeout);
                response.CheckResponse((byte) this.Address, 0x70);
                this.M350Part.OnReaderStatusErrorReadStatusAndFireEvent(response);
                this.M350Part.ThrowOnReturnCodeError(response);
                num = response.Buffer[6];
                if (nByteToRead < num)
                {
                    log.Trace("Read more data than requested");
                    throw new ArgumentOutOfRangeException("Read more data than requested");
                }
                data = new byte[num];
                log.Trace("Read " + data.Length.ToString());
                Array.Copy(response.Buffer, 7, data, 0, data.Length);
                RSSI = response.Buffer[(6 + data.Length) + 3];
            }
            return num;
        }

        public void ResetToFactoryDefault()
        {
            this.M350Part.ResetToFactoryDefault();
        }

        public void Send24GHzCarrier(int power, byte channel, byte baudrate, int time, bool modulated)
        {
            this.M350Part.Send24GHzCarrier(power, channel, baudrate, time, modulated);
        }

        public void Send24GHzPacket(iQ350 tag, int power, byte channel, byte baudrate, int nbPacket, int periodicity)
        {
            this.M350Part.Send24GHzPacket(tag, power, channel, baudrate, nbPacket, periodicity);
        }

        public bool SetApplicationID(int ID)
        {
            if ((ID < 0) || (ID > 0x3fff))
            {
                throw new ArgumentOutOfRangeException("Application ID must be between 0 and 16383");
            }
            this.M350Part.SetParameterPrivate(0x36, (uint) ID);
            return true;
        }

        public void SetBusAddress(int address)
        {
            this.M350Part.SetBusAddress(address);
        }

        public bool SetRangingTXPower(int Power)
        {
            if ((Power < 0) || (Power > 20))
            {
                throw new ArgumentOutOfRangeException("Power must be between 0 and 20");
            }
            this.M350Part.SetParameterPrivate(0x3b, (uint) Power);
            return true;
        }

        public bool SetRTLSrssiMargin(bool on)
        {
            this.M350Part.SetParameterPrivate(0x3d, on ? 1 : 0);
            return true;
        }

        public bool SetRTLSSignalFilterLevel(int level)
        {
            if ((level < -128) || ((level > -39) && (level != 0)))
            {
                throw new ArgumentOutOfRangeException("level must be between -38 and -128 or equal to 0");
            }
            this.M350Part.SetParameterPrivate(60, (uint) level);
            return true;
        }

        public override string ToString()
        {
            return this.M350Part.ToString();
        }

        public void UnlockConfiguration()
        {
            this.M350Part.UnlockConfiguration();
        }

        public void WriteConfiguration(int address, byte[] data, byte len)
        {
            this.M350Part.WriteConfiguration(address, data, len);
        }

        public virtual int WriteData24GHz(byte ManufID, byte TypeID, long ID, int address, byte[] data, int bytesToWrite, out int nBytesWritten, out int RSSI)
        {
            int num = 5;
            int length = 0;
            nBytesWritten = 0;
            int num3 = 0;
            RSSI = -128;
            while (nBytesWritten < bytesToWrite)
            {
                length = bytesToWrite - nBytesWritten;
                if (length > 120)
                {
                    length = 120;
                }
                try
                {
                    int rSSI = -128;
                    num3 = this.WriteData24GHzHelper(ManufID, TypeID, (uint) ID, address + nBytesWritten, data, nBytesWritten, length, out rSSI);
                    if (num3 == 0)
                    {
                        Thread.Sleep(100);
                        if (num-- <= 0)
                        {
                            return -1;
                        }
                    }
                    else
                    {
                        RSSI = rSSI;
                        num = 2;
                    }
                    nBytesWritten += num3;
                    log.Trace("written :" + ((int) nBytesWritten).ToString() + " :" + num3.ToString());
                    continue;
                }
                catch (DeviceException exception)
                {
                    log.Debug("Failed to write " + length.ToString() + " bytes, wrote " + ((int) nBytesWritten).ToString() + " " + exception.Message, exception);
                    if (exception.ResponseCode == 9)
                    {
                        throw;
                    }
                    nBytesWritten += exception.TotalByteProcessed;
                    if (num-- <= 0)
                    {
                        return exception.ResponseCode;
                    }
                    Thread.Sleep(100);
                    continue;
                }
            }
            return 0;
        }

        internal int WriteData24GHzHelper(byte ManufID, byte TypeID, uint ID, int address, byte[] data, int offset, int length, out int RSSI)
        {
            IC3ProtocolMessage message;
            if ((length < 1) || (length > 0x7d))
            {
                throw new ArgumentOutOfRangeException("Cannot only write 1 to 125 bytes");
            }
            int destinationIndex = 0;
            RSSI = -128;
            byte[] destinationArray = new byte[0xff];
            destinationArray[destinationIndex++] = (byte) this.Address;
            destinationArray[destinationIndex++] = 0x71;
            destinationArray[destinationIndex++] = 0;
            destinationArray[destinationIndex++] = this.m_TXPower24GHz;
            destinationArray[destinationIndex++] = this.m_Channel24GHz;
            destinationArray[destinationIndex++] = this.m_BaudRate24GHz;
            destinationArray[destinationIndex++] = this.m_Timeout;
            destinationArray[destinationIndex++] = 0;
            destinationArray[destinationIndex++] = 0;
            destinationArray[destinationIndex++] = ManufID;
            destinationArray[destinationIndex++] = TypeID;
            destinationArray[destinationIndex++] = (byte) (ID & 0xff);
            destinationArray[destinationIndex++] = (byte) ((ID & 0xff00) >> 8);
            destinationArray[destinationIndex++] = (byte) ((ID & 0xff0000) >> 0x10);
            destinationArray[destinationIndex++] = (byte) ((ID & -16777216) >> 0x18);
            destinationArray[destinationIndex++] = this.m_Retries24GHz;
            destinationArray[destinationIndex++] = (byte) (address & 0xff);
            destinationArray[destinationIndex++] = (byte) ((address & 0xff00) >> 8);
            destinationArray[destinationIndex++] = (byte) ((address & 0xff0000) >> 0x10);
            destinationArray[destinationIndex++] = (byte) length;
            Array.Copy(data, offset, destinationArray, destinationIndex, length);
            lock (this.DataStream.LockObject)
            {
                this.M350Part.DataStream.SendMessage(destinationArray, destinationIndex + length);
                int timeout = ((this.m_Timeout * this.m_Retries24GHz) * 10) + 0x7d0;
                message = this.M350Part.DataStream.ReadMessage(timeout);
                message.CheckResponse((byte) this.Address, 0x71);
                this.M350Part.OnReaderStatusErrorReadStatusAndFireEvent(message);
                this.M350Part.ThrowOnReturnCodeError(message);
            }
            RSSI = message.Buffer[9];
            if (length < message.Buffer[6])
            {
                log.Trace("Written more data than requested");
                throw new ArgumentOutOfRangeException("Written more data than requested");
            }
            return message.Buffer[6];
        }

        public int Address
        {
            get
            {
                return this.M350Part.Address;
            }
            set
            {
                this.M350Part.Address = (byte) value;
            }
        }

        public IDENTEC.DataStream DataStream
        {
            get
            {
                return this.M350Part.DataStream;
            }
            set
            {
                this.M350Part.DataStream = value;
            }
        }

        public string FirmwareVersion
        {
            get
            {
                return this.M350Part.FirmwareVersion;
            }
        }

        public string Information
        {
            get
            {
                return this.M350Part.Information;
            }
        }

        public ILR350Reader iPortM350
        {
            get
            {
                return this.M350Part;
            }
        }

        public int MajorVersion
        {
            get
            {
                return this.M350Part.MajorVersion;
            }
        }

        public int MinorVersion
        {
            get
            {
                return this.M350Part.MinorVersion;
            }
        }

        public int SatelliteID
        {
            get
            {
                string s = "0";
                try
                {
                    s = this.M350Part.SerialNumber.Remove(this.M350Part.SerialNumber.IndexOf('M') - 1, 2);
                }
                catch (Exception exception)
                {
                    log.Error("Couldn't retrieve SatelliteID: " + exception.Message);
                }
                return int.Parse(s);
            }
        }

        public string SerialNumber
        {
            get
            {
                return this.M350Part.SerialNumber;
            }
        }
    }
}

