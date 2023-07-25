namespace IDENTEC.PositionMarker
{
    using IDENTEC;
    using System;
    using System.Text;
    using System.Threading;

    public class PositionMarker : iBusModule, IBusDevice
    {
        private string _bootLoaderVersion;
        private byte _byAddress = IDENTEC.iBusAdapter.DISCONNECTED_SLAVE_ADDRESS;
        private DateTime _dtBoot;
        private int _firmwareVersionMajor;
        private int _firmwareVersionMinor;
        private IDENTEC.iBusAdapter _iBus;
        private string _information;
        private string _protocolVersion;
        internal iBusDeviceStatus _readerStatus = new iBusDeviceStatus(0);
        private string _serialNumber;

        internal PositionMarker(IDENTEC.iBusAdapter iBus, string information)
        {
            this._iBus = iBus;
            this._information = information;
        }

        public void ConnectSlavePort(bool enable)
        {
            this.SetParameter(DeviceParameterID.SlavePortConnected, enable ? 1 : 0);
        }

        public void CustomLFSignal(int pow, int len, byte[] data)
        {
            if (pow > 0xffff)
            {
                throw new ArgumentOutOfRangeException();
            }
            if (pow < 0)
            {
                throw new ArgumentOutOfRangeException();
            }
            if (len > 0x80)
            {
                throw new ArgumentOutOfRangeException();
            }
            if (len < 0)
            {
                throw new ArgumentOutOfRangeException();
            }
            if (data.Length < len)
            {
                throw new ArgumentOutOfRangeException();
            }
            int destinationIndex = 0;
            byte[] destinationArray = new byte[0x66];
            destinationArray[destinationIndex++] = this._byAddress;
            destinationArray[destinationIndex++] = 110;
            Array.Copy(BitConverter.GetBytes((ushort) pow), 0, destinationArray, destinationIndex, 2);
            destinationIndex += 2;
            destinationArray[destinationIndex++] = (byte) len;
            Array.Copy(data, 0, destinationArray, 5, len);
            destinationIndex += len;
            IC3ProtocolMessage message = null;
            lock (this.DataStream.LockObject)
            {
                this._iBus.DataStream.SendMessage(destinationArray, destinationIndex);
                Thread.Sleep(5);
                message = this.DataStream.ReadMessage(0x7d0);
                message.CheckResponse(this._byAddress, 110);
            }
            if (message[3] != 0)
            {
                byte num2 = message[3];
                throw new DeviceException("The device did respond with error code: " + num2.ToString(), message[3]);
            }
        }

        public int GetAnalogNegativeCurrent()
        {
            return (int) this.GetParameter(DeviceParameterID.AnalogNegativeCurrent);
        }

        public int GetAnalogPositiveCurrent()
        {
            return (int) this.GetParameter(DeviceParameterID.AnalogPositiveCurrent);
        }

        public int GetAnalogVoltage()
        {
            return (int) this.GetParameter(DeviceParameterID.AnalogPositiveVoltage);
        }

        public DateTime GetBootTime()
        {
            uint parameter = 0;
            parameter = this.GetParameter(DeviceParameterID.UpTime);
            TimeSpan span = new TimeSpan(0, 0, 0, (int) parameter, 0);
            this._dtBoot = DateTime.Now - span;
            return this._dtBoot;
        }

        public byte GetBusAddress()
        {
            this._byAddress = (byte) this.GetParameter(DeviceParameterID.BusAddress);
            return this._byAddress;
        }

        public int GetCycleDelay()
        {
            return (int) this.GetParameter(DeviceParameterID.CycleDelay);
        }

        private void GetDeviceInformation()
        {
            byte[] buffer = new byte[0x10];
            buffer[0] = this._byAddress;
            buffer[1] = 0x60;
            IC3ProtocolMessage response = null;
            lock (this.DataStream.LockObject)
            {
                this._iBus.DataStream.SendMessage(buffer, 2);
                Thread.Sleep(5);
                response = this._iBus.DataStream.ReadMessage(0x7d0);
            }
            response.CheckResponse(this._byAddress, 0x60);
            if (response[3] != 0)
            {
                byte num2 = response[3];
                throw new DeviceException("The device did respond with error code: " + num2.ToString(), response[3]);
            }
            int index = 7;
            this._serialNumber = Encoding.UTF8.GetString(response.Buffer, index, 10);
            this._serialNumber = this._serialNumber.Replace("\0", "");
            this._serialNumber = this._serialNumber.TrimEnd(null);
            index += 10;
            this._firmwareVersionMajor = response[index];
            this._firmwareVersionMinor = response[index + 1];
            index += 2;
            this._information = Encoding.UTF8.GetString(response.Buffer, index, 20);
            this._information = this._information.Replace("\0", "");
            this._information = this._information.TrimEnd(null);
            index += 20;
            this._bootLoaderVersion = string.Format("{0}.{1}", response[index], response[index + 1]);
            index += 2;
            this._protocolVersion = string.Format("{0}.{1}", response[index], response[index + 1]);
            index += 2;
            this.OnStatusErrorReadStatusAndFireEvent(response);
        }

        public int GetDigitalVoltage()
        {
            return (int) this.GetParameter(DeviceParameterID.DigitalVoltage);
        }

        public decimal GetDisablePeri()
        {
            return this.GetParameter(DeviceParameterID.PeriDisable);
        }

        public bool GetEnableHostOnlyRunningMode()
        {
            uint parameter = this.GetParameter(DeviceParameterID.RunningMode);
            return (1 == parameter);
        }

        public bool GetEnableJitter()
        {
            uint parameter = this.GetParameter(DeviceParameterID.JitterOn);
            return (1 == parameter);
        }

        public bool GetEnableMasterMode()
        {
            uint parameter = this.GetParameter(DeviceParameterID.MasterMode);
            return (1 == parameter);
        }

        public byte GetGen3dac()
        {
            return (byte) this.GetParameter(DeviceParameterID.Gen3dac);
        }

        public iDeviceHWInfo GetHWInfo()
        {
            return new iDeviceHWInfo(this.GetParameter(DeviceParameterID.CheckSumAndBootLoader));
        }

        public int GetLFFrequency()
        {
            return (int) this.GetParameter(DeviceParameterID.LFFrequency);
        }

        public int GetLoopCurrent()
        {
            return (int) this.GetParameter(DeviceParameterID.LoopCurrent);
        }

        public int GetLoopID()
        {
            return (int) this.GetParameter(DeviceParameterID.LoopID);
        }

        public int GetLoopPeakCurrent()
        {
            return (int) this.GetParameter(DeviceParameterID.LoopPeakCurrent);
        }

        [CLSCompliant(false)]
        public uint GetParameter(DeviceParameterID subCommand)
        {
            return this.GetParameter((byte) subCommand);
        }

        [CLSCompliant(false)]
        public uint GetParameter(byte bySubCmd)
        {
            byte[] buffer = new byte[0x10];
            buffer[0] = this._byAddress;
            buffer[1] = 100;
            buffer[2] = bySubCmd;
            int length = 3;
            IC3ProtocolMessage response = null;
            lock (this.DataStream.LockObject)
            {
                this._iBus.DataStream.SendMessage(buffer, length);
                Thread.Sleep(5);
                response = this.DataStream.ReadMessage(0x7d0);
                response.CheckResponse(this._byAddress, 100);
            }
            if (response[3] != 0)
            {
                byte num3 = response[3];
                throw new DeviceException("The device did respond with error code: " + num3.ToString(), response[3]);
            }
            if (4 != bySubCmd)
            {
                this.OnStatusErrorReadStatusAndFireEvent(response);
            }
            return (uint) ((((response[8] << 0x18) + (response[7] << 0x10)) + (response[6] << 8)) + response[5]);
        }

        public RelayControl GetRelayControl()
        {
            return (RelayControl) this.GetParameter(DeviceParameterID.RelayControl);
        }

        public bool GetSlavePortConnected()
        {
            return (this.GetParameter(DeviceParameterID.SlavePortConnected) == 1);
        }

        public iBusDeviceStatus GetStatus()
        {
            uint parameter = this.GetParameter(DeviceParameterID.Status);
            this.Status = new iBusDeviceStatus(parameter);
            return this.Status;
        }

        public SynchSlots GetSynchronizationSlots()
        {
            return new SynchSlots((int) this.GetParameter(DeviceParameterID.SynchronizationSlots));
        }

        public LFTelegramType GetTelegramType()
        {
            return (LFTelegramType) this.GetParameter(DeviceParameterID.LFTelegramType);
        }

        public int GetVehicleDetectionLevelPercentage()
        {
            return (int) this.GetParameter(DeviceParameterID.VehicleDetectionLevel);
        }

        public int GetVehicleDetectionThreshold()
        {
            return (int) this.GetParameter(DeviceParameterID.VehicleDetectionThreshold);
        }

        private void GetVersion()
        {
            byte[] buffer = new byte[0x20];
            buffer[0] = this._byAddress;
            buffer[1] = 0x33;
            IC3ProtocolMessage message = null;
            lock (this.DataStream.LockObject)
            {
                this._iBus.DataStream.SendMessage(buffer, 2);
                Thread.Sleep(5);
                message = this._iBus.DataStream.ReadMessage(0x7d0);
            }
            message.CheckResponse(this._byAddress, 0x33);
            byte num1 = message[3];
            byte num2 = message[4];
            this._information = Encoding.UTF8.GetString(message.Buffer, 5, 20);
            this._information = this._information.Replace("\0", "");
            this._information = this._information.TrimEnd(null);
        }

        public void Initialize()
        {
            if (string.IsNullOrEmpty(this._information))
            {
                this._information = this._iBus.QueryDeviceInformation(this._byAddress);
            }
            this.GetBootTime();
            this.GetDeviceInformation();
        }

        private void LockEEPROM()
        {
            byte[] buffer = new byte[] { this._byAddress, 0x35, 0, 0, 1, 0x69 };
            IC3ProtocolMessage message = null;
            lock (this.DataStream.LockObject)
            {
                this._iBus.DataStream.SendMessage(buffer, 6);
                Thread.Sleep(5);
                message = this.DataStream.ReadMessage(0x7d0);
            }
            message.CheckResponse(this._byAddress, 0x35);
            if (message[3] != 0)
            {
                byte num = message[3];
                throw new DeviceException("The device did respond with error code: " + num.ToString(), message[3]);
            }
            Thread.Sleep(200);
            buffer[2] = 1;
            buffer[4] = 1;
            buffer[5] = 0;
            message = null;
            lock (this.DataStream.LockObject)
            {
                this._iBus.DataStream.SendMessage(buffer, 6);
                Thread.Sleep(5);
                message = this.DataStream.ReadMessage(0x7d0);
            }
            message.CheckResponse(this._byAddress, 0x35);
            if (message[3] != 0)
            {
                byte num2 = message[3];
                throw new DeviceException("The device did respond with error code: " + num2.ToString(), message[3]);
            }
        }

        internal void OnStatusErrorReadStatusAndFireEvent(IC3ProtocolMessage response)
        {
            if (response[4] != 0)
            {
                this._readerStatus = new iBusDeviceStatus(this.GetParameter((byte) 4));
                if (this._readerStatus.HasError)
                {
                    base.FireEventModuleStatusError(this, this._readerStatus);
                }
            }
        }

        public byte[] ReadConfig(int address, int length)
        {
            byte[] buffer3;
            try
            {
                if ((address > 0xffff) || (address < 0))
                {
                    throw new ArgumentOutOfRangeException();
                }
                if ((length > 0x7f) || (length < 1))
                {
                    throw new ArgumentOutOfRangeException();
                }
                byte[] buffer = new byte[length + 5];
                buffer[0] = this._byAddress;
                buffer[1] = 0x34;
                buffer[2] = (byte) (address & 0xff);
                buffer[3] = (byte) ((address >> 8) & 0xff);
                buffer[4] = (byte) length;
                IC3ProtocolMessage message = null;
                lock (this.DataStream.LockObject)
                {
                    this._iBus.DataStream.SendMessage(buffer, length + 5);
                    Thread.Sleep(5);
                    message = this.DataStream.ReadMessage(0x7d0);
                }
                message.CheckResponse(this._byAddress, 0x34);
                if (message[3] != 0)
                {
                    byte num = message[3];
                    throw new DeviceException("The device did respond with error code: " + num.ToString(), message[3]);
                }
                byte[] destinationArray = new byte[length];
                Array.Copy(message.Buffer, 4, destinationArray, 0, length);
                buffer3 = destinationArray;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return buffer3;
        }

        public bool Reboot()
        {
            try
            {
                this.SetParameter(DeviceParameterID.AnalogPositiveVoltage, 0xdeadbeef);
            }
            catch (Exception exception)
            {
                if (!exception.Message.Contains("did not respond within"))
                {
                    throw exception;
                }
            }
            return true;
        }

        public void ResetToFactoryDefault()
        {
            this.SetParameter(DeviceParameterID.ResetToDefault, 0);
        }

        public void SendLFSignal()
        {
            this.SendLFSignal(this.GetGen3dac());
        }

        public void SendLFSignal(int mA)
        {
            if (mA > 0xffff)
            {
                throw new ArgumentOutOfRangeException();
            }
            if (mA < 0)
            {
                throw new ArgumentOutOfRangeException();
            }
            ushort num = (ushort) mA;
            byte[] destinationArray = new byte[0x10];
            destinationArray[0] = this._byAddress;
            destinationArray[1] = 0x6d;
            Array.Copy(BitConverter.GetBytes(num), 0, destinationArray, 2, 2);
            int length = 4;
            IC3ProtocolMessage message = null;
            lock (this.DataStream.LockObject)
            {
                this._iBus.DataStream.SendMessage(destinationArray, length);
                Thread.Sleep(5);
                message = this.DataStream.ReadMessage(0x7d0);
            }
            message.CheckResponse(this._byAddress, 0x6d);
            if (message[3] != 0)
            {
                byte num3 = message[3];
                throw new DeviceException("The device did respond with error code: " + num3.ToString(), message[3]);
            }
        }

        public void SetBusAddress(int address)
        {
            this.SetParameter(DeviceParameterID.BusAddress, (byte) address);
            this._byAddress = (byte) address;
        }

        public void SetCycleDelay(int delay)
        {
            uint dwParameter = (uint) delay;
            this.SetParameter(DeviceParameterID.CycleDelay, dwParameter);
        }

        public void SetDisablePeri(short bm)
        {
            uint dwParameter = (uint) bm;
            this.SetParameter(DeviceParameterID.PeriDisable, dwParameter);
        }

        public void SetEnableHostOnlyRunningMode(bool enable)
        {
            uint dwParameter = 0;
            if (enable)
            {
                dwParameter = 1;
            }
            this.SetParameter(DeviceParameterID.RunningMode, dwParameter);
        }

        public void SetEnableJitter(bool enable)
        {
            uint dwParameter = 0;
            if (enable)
            {
                dwParameter = 1;
            }
            this.SetParameter(DeviceParameterID.JitterOn, dwParameter);
        }

        public void SetEnableMasterMode(bool enable)
        {
            uint dwParameter = 0;
            if (enable)
            {
                dwParameter = 1;
            }
            this.SetParameter(DeviceParameterID.MasterMode, dwParameter);
        }

        public void SetGen3dac(int dac)
        {
            if ((dac > 0xff) || (dac < 0))
            {
                throw new ArgumentOutOfRangeException();
            }
            this.SetParameter(DeviceParameterID.Gen3dac, (uint) dac);
        }

        public void SetLFFrequency(int frequency)
        {
            uint dwParameter = (uint) frequency;
            this.SetParameter(DeviceParameterID.LFFrequency, dwParameter);
        }

        public void SetLoopCurrent(int current)
        {
            this.SetParameter(DeviceParameterID.LoopCurrent, (uint) current);
        }

        public void SetLoopID(int loopID)
        {
            uint dwParameter = (uint) loopID;
            this.SetParameter(DeviceParameterID.LoopID, dwParameter);
        }

        [CLSCompliant(false)]
        public IC3ProtocolMessage SetParameter(DeviceParameterID subCmd, uint dwParameter)
        {
            return this.SetParameter((byte)subCmd, dwParameter);
        }

        [CLSCompliant(false)]
        public IC3ProtocolMessage SetParameter(DeviceParameterID subCmd, int dwParameter)
        {
            return this.SetParameter((byte)subCmd, (uint)dwParameter);
        }

        [CLSCompliant(false)]
        public IC3ProtocolMessage SetParameter(byte bySubCmd, uint dwParameter)
        {
            byte[] buffer = new byte[0x20];
            buffer[0] = this._byAddress;
            buffer[1] = 0x43;
            buffer[2] = bySubCmd;
            buffer[3] = (byte) ((dwParameter >> 0x18) & 0xff);
            buffer[4] = (byte) ((dwParameter >> 0x10) & 0xff);
            buffer[5] = (byte) ((dwParameter >> 8) & 0xff);
            buffer[6] = (byte) (dwParameter & 0xff);
            int length = 7;
            IC3ProtocolMessage message = null;
            lock (this.DataStream.LockObject)
            {
                this._iBus.DataStream.SendMessage(buffer, length);
                Thread.Sleep(5);
                message = this.DataStream.ReadMessage(0x7d0);
            }
            message.CheckResponse(this._byAddress, 0x43);
            if (message[3] != 0)
            {
                byte num2 = message[3];
                throw new DeviceException("The device did respond with error code: " + num2.ToString(), message[3]);
            }
            return message;
        }

        public void SetRelayControl(RelayControl control)
        {
            uint dwParameter = (uint) control;
            this.SetParameter(DeviceParameterID.RelayControl, dwParameter);
        }

        public void SetSynchronizationSlots(SynchSlots slots)
        {
            this.SetParameter(DeviceParameterID.SynchronizationSlots, (uint) slots.ui);
        }

        public void SetTelegramType(LFTelegramType type)
        {
            uint dwParameter = (uint) type;
            this.SetParameter(DeviceParameterID.LFTelegramType, dwParameter);
        }

        public void SetVehicleDetectionThreshold(int threshold)
        {
            if (threshold < 0)
            {
                throw new ArgumentOutOfRangeException();
            }
            if (threshold > 100)
            {
                throw new ArgumentOutOfRangeException();
            }
            this.SetParameter(DeviceParameterID.VehicleDetectionThreshold, (uint) threshold);
        }

        public void Synchronize()
        {
            this._iBus.SendSyncMessage(IDENTEC.iBusAdapter.BroadcastAddress);
        }

        public override string ToString()
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(string.Format("Position Marker:\t{0}\r\n", this._information));
            builder.Append(string.Format("Serial Number:\t{0}\r\n", this._serialNumber));
            builder.Append(string.Format("Booted:\t\t{0}\r\n", this._dtBoot.ToString()));
            builder.Append(string.Format("Firmware:\t{0}\r\n", this.FirmwareVersion));
            builder.Append(string.Format("Boot Loader:\t{0}\r\n", this._bootLoaderVersion));
            builder.Append(string.Format("Protocol:\t{0}\r\n", this._protocolVersion));
            return builder.ToString();
        }

        private void UnlockEEPROM()
        {
            byte[] buffer = new byte[] { this._byAddress, 0x35, 1, 0, 1, 0x7a };
            IC3ProtocolMessage message = null;
            lock (this.DataStream.LockObject)
            {
                this._iBus.DataStream.SendMessage(buffer, 6);
                Thread.Sleep(5);
                message = this.DataStream.ReadMessage(0x7d0);
            }
            message.CheckResponse(this._byAddress, 0x35);
            if (message[3] != 0)
            {
                byte num = message[3];
                throw new DeviceException("The device did respond with error code: " + num.ToString(), message[3]);
            }
            buffer[2] = 0;
            buffer[4] = 1;
            buffer[5] = 0;
            message = null;
            lock (this.DataStream.LockObject)
            {
                this._iBus.DataStream.SendMessage(buffer, 6);
                Thread.Sleep(5);
                message = this.DataStream.ReadMessage(0x7d0);
            }
            message.CheckResponse(this._byAddress, 0x35);
            if (message[3] != 0)
            {
                byte num2 = message[3];
                throw new DeviceException("The device did respond with error code: " + num2.ToString(), message[3]);
            }
        }

        public void WriteConfig(int address, int length, byte[] data)
        {
            try
            {
                this.UnlockEEPROM();
                if ((address > 0xffff) || (address < 0))
                {
                    throw new ArgumentOutOfRangeException();
                }
                if ((length > 0x7f) || (length < 1))
                {
                    throw new ArgumentOutOfRangeException();
                }
                if (data.GetLength(0) != length)
                {
                    throw new ArgumentOutOfRangeException();
                }
                byte[] destinationArray = new byte[length + 5];
                destinationArray[0] = this._byAddress;
                destinationArray[1] = 0x35;
                destinationArray[2] = (byte) (address & 0xff);
                destinationArray[3] = (byte) ((address >> 8) & 0xff);
                destinationArray[4] = (byte) length;
                Array.Copy(data, 0, destinationArray, 5, length);
                IC3ProtocolMessage message = null;
                lock (this.DataStream.LockObject)
                {
                    this._iBus.DataStream.SendMessage(destinationArray, length + 5);
                    Thread.Sleep(5);
                    message = this.DataStream.ReadMessage(0x7d0);
                }
                message.CheckResponse(this._byAddress, 0x35);
                if (message[3] != 0)
                {
                    byte num = message[3];
                    throw new DeviceException("The device did respond with error code: " + num.ToString(), message[3]);
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
            finally
            {
                this.LockEEPROM();
            }
        }

        public int Address
        {
            get
            {
                return this._byAddress;
            }
        }

        public string BootLoaderVersion
        {
            get
            {
                return this._bootLoaderVersion;
            }
        }

        public IDENTEC.DataStream DataStream
        {
            get
            {
                return this._iBus.DataStream;
            }
        }

        public DateTime DateTimeBoot
        {
            get
            {
                return this._dtBoot;
            }
        }

        public string FirmwareVersion
        {
            get
            {
                return (this._firmwareVersionMajor.ToString() + "." + this._firmwareVersionMinor.ToString().PadLeft(2, '0'));
            }
        }

        public IDENTEC.iBusAdapter iBusAdapter
        {
            get
            {
                return this._iBus;
            }
        }

        public string Information
        {
            get
            {
                return this._information;
            }
        }

        public int MajorVersion
        {
            get
            {
                return this._firmwareVersionMajor;
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
                return this._protocolVersion;
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
            set
            {
                this._readerStatus = value;
            }
        }

        internal enum Command : byte
        {
            CustomLFSignal = 110,
            GetDeviceInformation = 0x60,
            GetDeviceParameter = 0x44,
            GetDeviceParameterN = 100,
            ReadConfig = 0x34,
            SendLFSignal = 0x6d,
            SetDeviceParameter = 0x43,
            SetDeviceParameterN = 0x63,
            Synchronization = 0x6c,
            WriteConfig = 0x35
        }

        public enum DeviceParameterID : byte
        {
            AnalogLoopVoltage = 11,
            AnalogNegativeCurrent = 8,
            AnalogPositiveCurrent = 7,
            AnalogPositiveVoltage = 6,
            BusAddress = 0x11,
            CheckSumAndBootLoader = 3,
            CPUTemperature = 12,
            CycleDelay = 0x1b,
            DigitalVoltage = 5,
            Gen3dac = 0x1d,
            JitterOn = 0x1c,
            LFFrequency = 0x1a,
            LFTelegramType = 0x19,
            LoopCurrent = 0x13,
            LoopID = 0x12,
            LoopPeakCurrent = 9,
            MasterMode = 20,
            PeriDisable = 30,
            RelayControl = 0x18,
            ResetToDefault = 0,
            RunningMode = 0x15,
            SlavePortConnected = 0x10,
            Status = 4,
            SynchronizationSlots = 0x16,
            UpTime = 2,
            VehicleDetectionLevel = 10,
            VehicleDetectionThreshold = 0x17
        }

        public enum LFTelegramType
        {
            StandardMarker,
            Gen3String
        }

        public enum RelayControl
        {
            OpenRelay,
            CloseRelay,
            CloseRelayWhenVehicleDetectionLevelAboveThreshold,
            OpenRelayWhenVehicleDetectionLevelAboveThreshold
        }
    }
}

