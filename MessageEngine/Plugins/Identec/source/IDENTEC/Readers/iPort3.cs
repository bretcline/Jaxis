namespace IDENTEC.Readers
{
    using IDENTEC;
    using IDENTEC.Tags;
    using IDENTEC.Tags.DigitalInputLogging;
    using IDENTEC.Tags.Logging;
    using System;
    using System.Globalization;
    using System.IO;
    using System.Net;
    using System.Net.Sockets;
    using System.Runtime.InteropServices;
    using System.Text;
    using System.Threading;

    public class iPort3 : Reader, IDisposable, ITagReaderIQ, ITagReaderID2
    {
        internal iPort3Antenna[] m_ArrayAntennas;
        private bool m_bAntennaSettingsCached;
        private bool m_bConnected;
        internal bool m_bEnableCallResponseMode = true;
        internal byte[] m_byLastSendBuffer;
        private CachedKeys m_CachedKeys;
        private DateTime m_dtLastContact;
        private ErrorCode m_ErrorCode;
        private byte[] m_myReadBuffer = new byte[0x4000];
        private int m_nActiveAntenna;
        private int m_nSwMain;
        private int m_nSwSub;
        private int m_nTotalBytesReceived;
        private Random m_rand = new Random();
        private SerialPortStream m_serialPort;
        private DataStream m_stream;
        private string m_strLastConfigKey;
        private string m_strLocation;
        private string m_strVersion;
        private TCPSocketStream m_tcpClient;

        public iPort3()
        {
            this.InitializeTCPClient();
            this.m_CachedKeys = new CachedKeys();
            this.m_ArrayAntennas = new iPort3Antenna[5];
            for (int i = 0; i < 5; i++)
            {
                this.m_ArrayAntennas[i].bEnabled = true;
                this.m_ArrayAntennas[i].bEnableiDRxSens = true;
                this.m_ArrayAntennas[i].bEnableiQRxSens = true;
                this.m_ArrayAntennas[i].nCableLoss = 0;
                this.m_ArrayAntennas[i].niDTxPwr = 30;
                this.m_ArrayAntennas[i].niQTxPwr = 6;
                this.m_ArrayAntennas[i].nRxThreshold = -90;
            }
        }

        private static void AntennaArgumentOutOfRangeCheck(int antenna)
        {
            if ((antenna > 5) || (antenna < 1))
            {
                throw new ArgumentOutOfRangeException("antenna");
            }
        }

        public bool BlinkTag(iQTag tag, int blinks)
        {
            return this.MultiBlink(tag, blinks);
        }

        public bool BlinkTag(iD2Tag tag, TimeSpan LEDOn, TimeSpan LEDOff, int blinkCount)
        {
            byte[] byData = iD2Tag.CreateMultiBlinkBuffer(ref LEDOn, ref LEDOff, blinkCount);
            this.EnableBlinkOnID2EEPROMAccess(false);
            return this.WriteTagDataPrivate(tag, iD2Tag.VIRTUAL_EEPROM_ADDRESS_BLINK, byData, byData.Length).Success;
        }

        internal static string CalculateCRC(byte[] byBuffer, int nLength, int nStartIndex)
        {
            int num = 0;
            for (int i = 0; i < nLength; i++)
            {
                num += byBuffer[i + nStartIndex];
            }
            return string.Format(CultureInfo.InvariantCulture, "{0:X2}", new object[] { num % 0x100 });
        }

        private void ClearAllEvents()
        {
            byte[] byCommandToSendBuffer = new byte[0x10];
            byCommandToSendBuffer[0] = 0x23;
            byCommandToSendBuffer[1] = 0x38;
            byCommandToSendBuffer[2] = 50;
            this.SendCommand(byCommandToSendBuffer, 3);
            this.ReceiveCommand(FunctionCode.ClearAllEvents, this.m_myReadBuffer, ref this.m_nTotalBytesReceived);
        }

        private void CloseTcpClient()
        {
            if (this.m_tcpClient != null)
            {
                try
                {
                    this.m_tcpClient.Close();
                }
                finally
                {
                    this.m_tcpClient = null;
                }
            }
        }

        public void CommitConfiguration()
        {
            byte[] byCommandToSendBuffer = new byte[0x10];
            byCommandToSendBuffer[0] = 0x23;
            byCommandToSendBuffer[1] = 0x36;
            byCommandToSendBuffer[2] = 0x34;
            this.SendCommand(byCommandToSendBuffer, 3);
            this.ReceiveCommand(FunctionCode.StoreConfigToEEPROM, this.m_myReadBuffer, ref this.m_nTotalBytesReceived);
        }

        public void Connect(IPAddress address, int port)
        {
            this.Connect(address, port, true);
        }

        public void Connect(string hostname, int port)
        {
            this.Connect(hostname, port, true);
        }

        internal void Connect(IPAddress address, int port, bool clearPendingEvents)
        {
            this.m_bConnected = false;
            try
            {
                this.m_tcpClient = new TCPSocketStream(address.ToString(), port);
                this.m_tcpClient.Open();
                if (clearPendingEvents)
                {
                    this.ClearAllEvents();
                }
                this.GetInformation();
                this.InitializeConfigurationKeys();
                this.m_bConnected = true;
            }
            catch (SocketException)
            {
                this.m_bConnected = false;
                throw;
            }
        }

        internal void Connect(string hostname, int port, bool clearPendingEvents)
        {
            this.Connect(IPAddress.Parse(hostname), port, clearPendingEvents);
        }

        public void ConnectRS232(int port)
        {
            this.m_bConnected = false;
            if (this.m_tcpClient != null)
            {
                this.CloseTcpClient();
            }
            if ((this.m_serialPort != null) && this.m_serialPort.IsOpen)
            {
                this.m_serialPort.Close();
            }
            this.m_serialPort = new SerialPortStream(port, 0x1c200);
            this.m_serialPort.Open();
            this.m_stream = this.m_serialPort;
            this.ClearAllEvents();
            this.GetInformation();
            this.InitializeConfigurationKeys();
            this.m_bConnected = true;
        }

        public override bool Disconnect()
        {
            try
            {
                this.CloseTcpClient();
                if (this.m_serialPort != null)
                {
                    this.m_serialPort.Close();
                }
            }
            catch
            {
                throw;
            }
            finally
            {
                this.m_bConnected = false;
            }
            return true;
        }

        public void Dispose()
        {
            this.CloseTcpClient();
        }

        public void EnableAllAntennas(bool enable)
        {
            for (int i = 1; i <= 5; i++)
            {
                this.EnableAntenna(i, enable);
            }
        }

        public void EnableAntenna(int antenna, bool enable)
        {
            AntennaArgumentOutOfRangeCheck(antenna);
            if (!this.m_bAntennaSettingsCached || (enable != this.m_ArrayAntennas[antenna - 1].bEnabled))
            {
                this.m_ArrayAntennas[antenna - 1].bEnabled = enable;
                this.WriteConfigKeyAntEnable(antenna, enable);
            }
        }

        private void EnableBlinkOnID2EEPROMAccess(bool enableBlink)
        {
            if (enableBlink != this.m_CachedKeys.EnableBlinkOnTagData)
            {
                this.m_CachedKeys.EnableBlinkOnTagData = enableBlink;
                this.WriteConfigurationKey("BlinkEnable", this.m_CachedKeys.EnableBlinkOnTagData ? "1" : "0");
            }
        }

        public void EnableRecieveSensitivityForID2Tags(int antenna, bool enable)
        {
            AntennaArgumentOutOfRangeCheck(antenna);
            if ((5 != antenna) && (!this.m_bAntennaSettingsCached || (enable != this.m_ArrayAntennas[antenna - 1].bEnableiDRxSens)))
            {
                this.m_ArrayAntennas[antenna - 1].bEnableiDRxSens = enable;
                this.WriteConfigKeyAntiDEnableRxSens(antenna, enable);
            }
        }

        public void EnableRecieveSensitivityForIQTags(int antenna, bool enable)
        {
            AntennaArgumentOutOfRangeCheck(antenna);
            if ((5 != antenna) && (!this.m_bAntennaSettingsCached || (enable != this.m_ArrayAntennas[antenna - 1].bEnableiQRxSens)))
            {
                this.m_ArrayAntennas[antenna - 1].bEnableiQRxSens = enable;
                this.WriteConfigKeyAntiQEnableRxSens(antenna, enable);
            }
        }

        public void EnableWakeupAntenna(bool enable)
        {
            this.EnableAntenna(5, enable);
        }

        ~iPort3()
        {
            this.Dispose();
        }

        internal static int FindIndexOfCarriageReturn(byte[] byBuffer, int nBytesReceived, int nOffset)
        {
            for (int i = nOffset; i < nBytesReceived; i++)
            {
                if (byBuffer[i] == 13)
                {
                    return i;
                }
            }
            return -1;
        }

        public bool GetEnableRecieveSensitivityForID2Tags(int antenna)
        {
            AntennaArgumentOutOfRangeCheck(antenna);
            return this.m_ArrayAntennas[antenna - 1].bEnableiDRxSens;
        }

        public bool GetEnableRecieveSensitivityForIQTags(int antenna)
        {
            AntennaArgumentOutOfRangeCheck(antenna);
            return this.m_ArrayAntennas[antenna - 1].bEnableiQRxSens;
        }

        public static string GetErrorCodeAsString(ErrorCode errorCode)
        {
            return errorCode.ToString();
        }

        private void GetInformation()
        {
            byte[] byCommandToSendBuffer = new byte[0x10];
            byCommandToSendBuffer[0] = 0x23;
            byCommandToSendBuffer[1] = 0x30;
            byCommandToSendBuffer[2] = 0x31;
            this.SendCommand(byCommandToSendBuffer, 3);
            this.ReceiveCommand(FunctionCode.GetInfo, this.m_myReadBuffer, ref this.m_nTotalBytesReceived);
            this.m_nSwMain = int.Parse(Encoding.ASCII.GetString(this.m_myReadBuffer, 4, 2));
            this.m_nSwSub = int.Parse(Encoding.ASCII.GetString(this.m_myReadBuffer, 6, 2));
            this.m_strVersion = Encoding.ASCII.GetString(this.m_myReadBuffer, 4, 2) + "." + Encoding.ASCII.GetString(this.m_myReadBuffer, 6, 2);
            int count = int.Parse(Encoding.ASCII.GetString(this.m_myReadBuffer, 8, 2), NumberStyles.AllowHexSpecifier, CultureInfo.InvariantCulture);
            if (count > 0)
            {
                this.m_strLocation = Encoding.ASCII.GetString(this.m_myReadBuffer, 10, count);
            }
        }

        public bool GetInputs(ref bool[] inputs, ref bool[] outputs)
        {
            int index = 0;
            byte[] byCommandToSendBuffer = new byte[0x40];
            if ((inputs == null) || (inputs.Length < 4))
            {
                throw new Exception("array for inputs invalid");
            }
            if ((outputs == null) || (outputs.Length < 4))
            {
                throw new Exception("array for outputs invalid");
            }
            byCommandToSendBuffer[0] = 0x23;
            byCommandToSendBuffer[1] = 0x34;
            byCommandToSendBuffer[2] = 0x31;
            this.SendCommand(byCommandToSendBuffer, 3);
            this.ReceiveCommand(FunctionCode.GetStatusOfInputs, this.m_myReadBuffer, ref this.m_nTotalBytesReceived);
            if ((this.m_nTotalBytesReceived < 8) || (this.m_myReadBuffer[3] != 0x30))
            {
                throw new Exception("request returned a code: 0x" + this.m_myReadBuffer[3]);
            }
            for (index = 0; index < 4; index++)
            {
                if (((this.m_myReadBuffer[4] >> index) & 1) != 0)
                {
                    inputs[index] = true;
                }
                else
                {
                    inputs[index] = false;
                }
                if (((this.m_myReadBuffer[5] >> index) & 1) != 0)
                {
                    outputs[index] = true;
                }
                else
                {
                    outputs[index] = false;
                }
            }
            return true;
        }

        public int GetMainID()
        {
            return int.Parse(this.ReadConfigurationKey("IPortMainID"));
        }

        public int GetSubID()
        {
            return int.Parse(this.ReadConfigurationKey("IPortSubID"));
        }

        private void GetTime()
        {
            byte[] byCommandToSendBuffer = new byte[0x10];
            byCommandToSendBuffer[0] = 0x23;
            byCommandToSendBuffer[1] = 0x30;
            byCommandToSendBuffer[2] = 50;
            this.SendCommand(byCommandToSendBuffer, 3);
            this.ReceiveCommand(FunctionCode.GetTimeDate, this.m_myReadBuffer, ref this.m_nTotalBytesReceived);
        }

        public int GetTxPowerForID2Tags(int antenna)
        {
            AntennaArgumentOutOfRangeCheck(antenna);
            return this.m_ArrayAntennas[antenna - 1].niDTxPwr;
        }

        public int GetTxPowerForIQTags(int antenna)
        {
            AntennaArgumentOutOfRangeCheck(antenna);
            return this.m_ArrayAntennas[antenna - 1].niQTxPwr;
        }

        private void GetVersion()
        {
            byte[] byCommandToSendBuffer = new byte[0x10];
            byCommandToSendBuffer[0] = 0x23;
            byCommandToSendBuffer[1] = 0x30;
            byCommandToSendBuffer[2] = 0x35;
            this.SendCommand(byCommandToSendBuffer, 3);
            this.ReceiveCommand(FunctionCode.GetVersion, this.m_myReadBuffer, ref this.m_nTotalBytesReceived);
            this.m_nSwMain = int.Parse(Encoding.ASCII.GetString(this.m_myReadBuffer, 4, 2));
            this.m_nSwSub = int.Parse(Encoding.ASCII.GetString(this.m_myReadBuffer, 6, 2));
            int.Parse(Encoding.ASCII.GetString(this.m_myReadBuffer, 8, 2));
            int.Parse(Encoding.ASCII.GetString(this.m_myReadBuffer, 10, 2));
            Encoding.ASCII.GetString(this.m_myReadBuffer, 12, 8);
            int.Parse(Encoding.ASCII.GetString(this.m_myReadBuffer, 12, 8), NumberStyles.AllowHexSpecifier, CultureInfo.InvariantCulture);
            int.Parse(Encoding.ASCII.GetString(this.m_myReadBuffer, 20, 4));
        }

        private void InitializeConfigurationKeys()
        {
            this.WriteConfigKeyEventMsgEnable(false);
            this.WriteConfigurationKey("ReadEnable", "0");
            this.WriteConfigurationKey("PowSaveEnable", "0");
            this.WriteConfigurationKey("DetectMode", "0");
            this.WriteConfigKeyScanPause(0);
            this.WriteConfigKeyScanMode(ScanMode.HostOnly);
            this.m_CachedKeys.detectTagType = DetectTagType.IQ;
            this.WriteConfigurationKey("DetectTagType", ((int) this.m_CachedKeys.detectTagType).ToString(CultureInfo.InvariantCulture));
            this.WriteConfigurationKey("DetectInhibit", "0");
            this.m_CachedKeys.EnableBlinkOnTagData = true;
            this.WriteConfigurationKey("BlinkEnable", this.m_CachedKeys.EnableBlinkOnTagData ? "1" : "0");
            this.WriteConfigKeySlotSize(Reader.CalculateSlotSize(0x80));
            for (int i = 0; i < 5; i++)
            {
                int nAntenna = i + 1;
                this.m_ArrayAntennas[i].bEnabled = this.ReadConfigKeyAntEnable(nAntenna);
                this.m_ArrayAntennas[i].niDTxPwr = this.ReadConfigKeyAntiD2TxPwr(nAntenna);
                this.m_ArrayAntennas[i].niQTxPwr = this.ReadConfigKeyAntiQTxPwr(nAntenna);
                this.m_ArrayAntennas[i].bEnableiDRxSens = this.ReadConfigKeyAntiD2EnableRxSens(nAntenna);
                this.m_ArrayAntennas[i].bEnableiQRxSens = this.ReadConfigKeyAntiQEnableRxSens(nAntenna);
                this.SetReceiveThreshold(nAntenna, this.m_ArrayAntennas[i].nRxThreshold);
                this.SetCableLoss(nAntenna, this.m_ArrayAntennas[i].nCableLoss);
            }
            this.m_bAntennaSettingsCached = true;
        }

        private void InitializeTCPClient()
        {
        }

        private void IsActiveAntennaValid()
        {
            if (this.m_nActiveAntenna == 0)
            {
                throw new ArgumentOutOfRangeException("The active antenna cannot be set to 0 for single tag communications.");
            }
        }

        public bool IsAntennaEnabled(int antenna)
        {
            AntennaArgumentOutOfRangeCheck(antenna);
            return this.m_ArrayAntennas[antenna - 1].bEnabled;
        }

        internal static bool IsCRCValid(byte[] byBuffer, int nLength)
        {
            string str = CalculateCRC(byBuffer, nLength - 4, 1);
            string str2 = Encoding.ASCII.GetString(byBuffer, nLength - 3, 2);
            return (str == str2);
        }

        internal static bool IsFullMessageReceived(int nCurrentLength, byte[] byBuffer, ref int nMessageLength)
        {
            for (int i = 0; i < nCurrentLength; i++)
            {
                if (byBuffer[i] == 13)
                {
                    nMessageLength = i + 1;
                    return true;
                }
            }
            nMessageLength = 0;
            return false;
        }

        public bool IsWakeupAntennaEnabled()
        {
            return this.IsAntennaEnabled(5);
        }

        internal bool MultiBlink(ResponseTag tag, int nBlinks)
        {
            this.IsActiveAntennaValid();
            tag.ResetSignals();
            byte[] destinationArray = new byte[0x20];
            destinationArray[0] = 0x23;
            destinationArray[1] = 50;
            destinationArray[2] = 0x35;
            destinationArray[3] = (byte) (this.m_nActiveAntenna + 0x30);
            byte[] bytes = Encoding.ASCII.GetBytes(tag.HexID);
            Array.Copy(bytes, 0, destinationArray, 4, bytes.Length);
            if (tag is iQTag)
            {
                destinationArray[12] = 0x30;
            }
            else
            {
                destinationArray[12] = 0x31;
            }
            byte[] sourceArray = Encoding.ASCII.GetBytes(string.Format(CultureInfo.InvariantCulture, "{0:X2}", new object[] { nBlinks }));
            Array.Copy(sourceArray, 0, destinationArray, 13, sourceArray.Length);
            this.SendCommand(destinationArray, 15);
            this.ReceiveCommand(FunctionCode.BlinkTag, this.m_myReadBuffer, ref this.m_nTotalBytesReceived);
            if ((this.m_ErrorCode != ErrorCode.NoError) && (this.m_ErrorCode != ErrorCode.OK))
            {
                return false;
            }
            this.ParseBLINKResponse(tag);
            return true;
        }

        private void ParseBLINKResponse(Tag tag)
        {
            tag.m_dt = DateTime.Now;
            iD2Tag tag2 = tag as iD2Tag;
            if (tag2 != null)
            {
                if (0x30 == this.m_myReadBuffer[4])
                {
                    tag2.m_BattStatus = iD2Tag.BatteryStatus.Good;
                }
                else
                {
                    tag2.m_BattStatus = iD2Tag.BatteryStatus.Poor;
                }
            }
            this.ParseVersionFromBuffer(tag as iQTag, 5);
            this.ParseFieldStrengthFromBuffer(tag, 11);
        }

        internal void ParseEventResponse()
        {
        }

        private void ParseFieldStrengthFromBuffer(Tag tag, int nOffset)
        {
            tag.ResetSignals();
            int num = 0;
            for (int i = 1; i <= 4; i++)
            {
                tag.SetSignalStrength(i, sbyte.Parse(Encoding.ASCII.GetString(this.m_myReadBuffer, nOffset + num, 2), NumberStyles.AllowHexSpecifier, CultureInfo.InvariantCulture));
                num += 2;
            }
        }

        internal void ParseReadEEPROMResponse(byte[] byData, ref int nBytesRead)
        {
            nBytesRead = int.Parse(Encoding.ASCII.GetString(this.m_myReadBuffer, 0x13, 4), NumberStyles.AllowHexSpecifier, CultureInfo.InvariantCulture);
            int index = 0x17;
            if (nBytesRead != 0)
            {
                for (int i = 0; i < nBytesRead; i++)
                {
                    byData[i] = byte.Parse(Encoding.ASCII.GetString(this.m_myReadBuffer, index, 2), NumberStyles.AllowHexSpecifier, CultureInfo.InvariantCulture);
                    index += 2;
                }
            }
        }

        private void ParseScanResponse(TagCollection tags)
        {
            int num = int.Parse(Encoding.ASCII.GetString(this.m_myReadBuffer, 4, 4), NumberStyles.AllowHexSpecifier, CultureInfo.InvariantCulture);
            int index = 8;
            Tag tag = null;
            DateTime now = DateTime.Now;
            for (int i = 0; i < num; i++)
            {
                switch (this.m_myReadBuffer[index + 0x10])
                {
                    case 0x30:
                        tag = new iQTag();
                        break;

                    case 0x31:
                        tag = new iD2Tag();
                        break;
                }
                tag.m_dt = now;
                tag.Number = uint.Parse(Encoding.ASCII.GetString(this.m_myReadBuffer, index, 8), NumberStyles.AllowHexSpecifier, CultureInfo.InvariantCulture);
                index += 8;
                this.ParseFieldStrengthFromBuffer(tag, index);
                index += 8;
                index++;
                tags.Add(tag);
            }
        }

        private void ParseVersionFromBuffer(iQTag tag, int nOffset)
        {
            if (tag != null)
            {
                byte byRaw = byte.Parse(Encoding.ASCII.GetString(this.m_myReadBuffer, nOffset, 2), NumberStyles.AllowHexSpecifier, CultureInfo.InvariantCulture);
                tag.SetModelTypeFromTagProtocol(byRaw);
            }
        }

        public bool PingTag(iD2Tag tag)
        {
            return this.PingTagPrivate(tag);
        }

        public bool PingTag(iQTag tag)
        {
            return this.PingTagPrivate(tag);
        }

        public bool PingTag(iD2Tag tag, bool enableBlink)
        {
            if (enableBlink)
            {
                return this.PingTag(tag);
            }
            return this.ReadTagDataPrivate(tag, this.m_rand.Next(0x30), 1, enableBlink).Success;
        }

        private bool PingTagPrivate(ResponseTag tag)
        {
            return this.MultiBlink(tag, 1);
        }

        private bool ReadConfigKeyAntEnable(int nAntenna)
        {
            string strKey = string.Format(CultureInfo.InvariantCulture, "Ant{0}Enable", new object[] { nAntenna });
            if (5 == nAntenna)
            {
                strKey = "AntWEnable";
            }
            string strValue = "0";
            this.ReadConfigurationKey(strKey, out strValue);
            return Convert.ToBoolean(int.Parse(strValue));
        }

        private bool ReadConfigKeyAntiD2EnableRxSens(int nAntenna)
        {
            string strKey = string.Format(CultureInfo.InvariantCulture, "Ant{0}HighSensD", new object[] { nAntenna });
            if (5 == nAntenna)
            {
                return false;
            }
            string strValue = "0";
            this.ReadConfigurationKey(strKey, out strValue);
            return Convert.ToBoolean(int.Parse(strValue));
        }

        private int ReadConfigKeyAntiD2TxPwr(int nAntenna)
        {
            string strKey = string.Format(CultureInfo.InvariantCulture, "Ant{0}TxPowerD", new object[] { nAntenna });
            if (5 == nAntenna)
            {
                strKey = "AntWTxPowerD";
            }
            string strValue = "0";
            this.ReadConfigurationKey(strKey, out strValue);
            return int.Parse(strValue);
        }

        private bool ReadConfigKeyAntiQEnableRxSens(int nAntenna)
        {
            if (nAntenna == 5)
            {
                return false;
            }
            string strKey = string.Format(CultureInfo.InvariantCulture, "Ant{0}HighSens", new object[] { nAntenna });
            string strValue = "0";
            this.ReadConfigurationKey(strKey, out strValue);
            return Convert.ToBoolean(int.Parse(strValue));
        }

        private int ReadConfigKeyAntiQTxPwr(int nAntenna)
        {
            string strKey = string.Format(CultureInfo.InvariantCulture, "Ant{0}TxPowerQ", new object[] { nAntenna });
            if (5 == nAntenna)
            {
                strKey = "AntWTxPowerQ";
            }
            string strValue = "0";
            this.ReadConfigurationKey(strKey, out strValue);
            return int.Parse(strValue);
        }

        protected string ReadConfigurationKey(string key)
        {
            string strValue = "";
            this.ReadConfigurationKey(key, out strValue);
            return strValue;
        }

        internal void ReadConfigurationKey(string strKey, out string strValue)
        {
            this.m_strLastConfigKey = strKey;
            byte[] destinationArray = new byte[0x40];
            destinationArray[0] = 0x23;
            destinationArray[1] = 0x36;
            destinationArray[2] = 0x30;
            for (int i = 0; i < 0x10; i++)
            {
                destinationArray[i + 3] = 0x20;
            }
            byte[] bytes = Encoding.ASCII.GetBytes(strKey);
            Array.Copy(bytes, 0, destinationArray, 0x13 - bytes.Length, bytes.Length);
            this.SendCommand(destinationArray, 0x13);
            this.ReceiveCommand(FunctionCode.ReadConfigKey, this.m_myReadBuffer, ref this.m_nTotalBytesReceived);
            strValue = Encoding.ASCII.GetString(this.m_myReadBuffer, 4, 0x10);
        }

        public LoopData ReadiQLTagMarkerInfo(iQTag tag)
        {
            TagReadDataResult result = this.ReadTagData(tag, 0x90, 10);
            if (!result.Success)
            {
                throw new PartialTagCommunicationsException("Could not read the tag. (" + this.DeviceStatus.ToString() + ")");
            }
            return LoopData.CreateLoopData(tag.Number, result.Data, tag.ContactTime);
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
            throw new NotImplementedException("Not yet implemented");
        }

        public TemperatureLogSample ReadLastSampledTemperature(iQTag tag)
        {
            TemperatureLogSample sample = new TemperatureLogSample();
            ushort num = 1;
            byte[] bytes = BitConverter.GetBytes(num);
            if (!this.WriteTagData(tag, 0xb0, bytes, 2).Success)
            {
                throw new PartialTagCommunicationsException("Failed to write data to tag. (" + this.DeviceStatus.ToString() + ")");
            }
            iQTag.IsTemperatureTag(tag);
            Thread.Sleep(50);
            TagReadDataResult result2 = this.ReadTagData(tag, 0xb0, 2);
            if (!result2.Success)
            {
                throw new PartialTagCommunicationsException("Failed to read data from tag. (" + this.DeviceStatus.ToString() + ")");
            }
            short num2 = BitConverter.ToInt16(result2.Data, 0);
            sample.m_temperatureDegreesC = num2 * 0.01f;
            sample._time = DateTime.Now;
            return sample;
        }

        public TemperatureLogSample ReadTagCurrentTemperature(iQTag tag)
        {
            TemperatureLogSample sample = new TemperatureLogSample();
            ushort num = 1;
            byte[] bytes = BitConverter.GetBytes(num);
            if (!this.WriteTagData(tag, 0xb0, bytes, 2).Success)
            {
                throw new PartialTagCommunicationsException("Failed to write data to tag. (" + this.DeviceStatus.ToString() + ")");
            }
            iQTag.IsTemperatureTag(tag);
            Thread.Sleep(50);
            TagReadDataResult result2 = this.ReadTagData(tag, 0xb0, 2);
            if (!result2.Success)
            {
                throw new PartialTagCommunicationsException("Failed to read data from tag. (" + this.DeviceStatus.ToString() + ")");
            }
            short num2 = BitConverter.ToInt16(result2.Data, 0);
            sample.m_temperatureDegreesC = num2 * 0.01f;
            sample._time = DateTime.Now;
            return sample;
        }

        public TagReadDataResult ReadTagData(iD2Tag tag, int address, int bytesToRead)
        {
            return this.ReadTagDataPrivate(tag, address, bytesToRead, true);
        }

        public TagReadDataResult ReadTagData(iQTag tag, int address, int bytesToRead)
        {
            return this.ReadTagDataPrivate(tag, address, bytesToRead, true);
        }

        internal TagReadDataResult ReadTagDataPrivate(ResponseTag tag, int address, int bytesToRead, bool enableBlink)
        {
            this.IsActiveAntennaValid();
            this.EnableBlinkOnID2EEPROMAccess(enableBlink);
            tag.ResetSignals();
            TagReadDataResult result = new TagReadDataResult {
                Data = new byte[bytesToRead]
            };
            byte[] destinationArray = new byte[0x18];
            destinationArray[0] = 0x23;
            destinationArray[1] = 50;
            destinationArray[2] = 0x31;
            destinationArray[3] = (byte) (this.m_nActiveAntenna + 0x30);
            byte[] bytes = Encoding.ASCII.GetBytes(tag.HexID);
            Array.Copy(bytes, 0, destinationArray, 4, bytes.Length);
            if (tag is iQTag)
            {
                destinationArray[12] = 0x30;
            }
            else
            {
                destinationArray[12] = 0x31;
            }
            ushort num = (ushort) address;
            byte[] sourceArray = Encoding.ASCII.GetBytes(string.Format(CultureInfo.InvariantCulture, "{0:X4}", new object[] { num }));
            Array.Copy(sourceArray, 0, destinationArray, 13, sourceArray.Length);
            ushort num2 = (ushort) bytesToRead;
            byte[] buffer4 = Encoding.ASCII.GetBytes(string.Format(CultureInfo.InvariantCulture, "{0:X4}", new object[] { num2 }));
            Array.Copy(buffer4, 0, destinationArray, 0x11, buffer4.Length);
            this.SendCommand(destinationArray, 0x15);
            this.ReceiveCommand(FunctionCode.ReadTagData, this.m_myReadBuffer, ref this.m_nTotalBytesReceived);
            if ((this.m_ErrorCode == ErrorCode.NoError) || (this.m_ErrorCode == ErrorCode.OK))
            {
                int nBytesRead = 0;
                this.ParseReadEEPROMResponse(result.Data, ref nBytesRead);
                if (nBytesRead != 0)
                {
                    result.BytesRead = nBytesRead;
                    result.Success = true;
                    this.ParseBLINKResponse(tag);
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
            throw new NotSupportedException();
        }

        public TimeSpan ReadTagLogSamplingInterval(iQTag tag)
        {
            throw new NotImplementedException("Not yet implemented");
        }

        public RawLogData ReadTagRawLog(iQTag tag)
        {
            throw new NotImplementedException("Not yet implemented");
        }

        public TemperatureExtremes ReadTagTemperatureExtremes(iQTag tag)
        {
            throw new NotImplementedException("Not yet implemented");
        }

        public TemperatureLogData ReadTagTemperatureLog(iQTag tag)
        {
            throw new NotImplementedException("Not yet implemented");
        }

        internal FunctionCode ReceiveCommand(FunctionCode fcExpected, byte[] byReceiveBuffer, ref int nTotalBytesReceived)
        {
            FunctionCode code;
            nTotalBytesReceived = 0;
            int num = 0;
            int nMessageLength = 0;
            bool flag = true;
            while (true)
            {
                if (flag)
                {
                    this.m_bConnected = false;
                    num = this.m_stream.Read(byReceiveBuffer, nTotalBytesReceived, byReceiveBuffer.Length - nTotalBytesReceived);
                    this.m_bConnected = true;
                    if (num == 0)
                    {
                        throw new IOException("The server closed the connection.");
                    }
                    nTotalBytesReceived += num;
                    this.m_dtLastContact = DateTime.Now;
                }
                if (IsFullMessageReceived(nTotalBytesReceived, byReceiveBuffer, ref nMessageLength))
                {
                    code = (FunctionCode) ((byte) ushort.Parse(Encoding.ASCII.GetString(byReceiveBuffer, 1, 2), NumberStyles.AllowHexSpecifier, CultureInfo.InvariantCulture));
                    if ((fcExpected == FunctionCode.None) || (code == fcExpected))
                    {
                        break;
                    }
                    RemoveFirstMessageFromBuffer(byReceiveBuffer, ref nTotalBytesReceived);
                    flag = 0 == nTotalBytesReceived;
                }
                else
                {
                    flag = true;
                }
            }
            if (!IsCRCValid(byReceiveBuffer, nMessageLength))
            {
                throw new CRCException("The message received from the reader has an invalid CRC", Encoding.ASCII.GetString(byReceiveBuffer, 0, nMessageLength));
            }
            if (FunctionCode.NewTagEvent != code)
            {
                if (0x30 != byReceiveBuffer[3])
                {
                    this.m_ErrorCode = (ErrorCode) (-512 - byReceiveBuffer[3]);
                    ErrorCode errorCode = this.m_ErrorCode;
                    if (errorCode != ErrorCode.NoConfigurationKeyWithThisName)
                    {
                        switch (errorCode)
                        {
                            case ErrorCode.OperationPartialSuccess:
                            case ErrorCode.TagNotInField:
                                return code;

                            case ErrorCode.TagCommunicationPermissionDenied:
                                return code;
                        }
                        throw new iPORT3Exception(GetErrorCodeAsString(this.m_ErrorCode));
                    }
                    throw new iPORT3Exception(GetErrorCodeAsString(this.m_ErrorCode) + ": " + this.m_strLastConfigKey);
                }
                this.m_ErrorCode = ErrorCode.OK;
            }
            return code;
        }

        internal static void RemoveFirstMessageFromBuffer(byte[] byBuffer, ref int nTotalBytesRead)
        {
            int num = FindIndexOfCarriageReturn(byBuffer, nTotalBytesRead, 0);
            if (-1 != num)
            {
                int num2 = FindIndexOfCarriageReturn(byBuffer, nTotalBytesRead - num, num + 1);
                nTotalBytesRead -= num + 1;
                if (-1 != num2)
                {
                    Array.Copy(byBuffer, num + 1, byBuffer, 0, nTotalBytesRead);
                }
            }
        }

        public void Reset()
        {
            byte[] byCommandToSendBuffer = new byte[0x10];
            byCommandToSendBuffer[0] = 0x23;
            byCommandToSendBuffer[1] = 0x30;
            byCommandToSendBuffer[2] = 0x34;
            this.SendCommand(byCommandToSendBuffer, 3);
            this.Disconnect();
        }

        public TagCollection ScanForID2Tags(int maxTagsThatCanRespond)
        {
            return this.ScanForID2Tags(maxTagsThatCanRespond, false);
        }

        public TagCollection ScanForID2Tags(int maxTagsThatCanRespond, bool blink)
        {
            return this.ScanForTags(maxTagsThatCanRespond, blink, DetectTagType.ID);
        }

        public TagCollection ScanForIQTags(int maxTagsThatCanRespond)
        {
            return this.ScanForIQTags(maxTagsThatCanRespond, false);
        }

        public TagCollection ScanForIQTags(int maxTagsThatCanRespond, bool blink)
        {
            return this.ScanForTags(maxTagsThatCanRespond, blink, DetectTagType.IQ);
        }

        private TagCollection ScanForTags(int maxTagsThatCanRespond, bool blink, DetectTagType tagType)
        {
            if (this.m_CachedKeys.detectTagType != tagType)
            {
                this.m_CachedKeys.detectTagType = tagType;
                this.WriteConfigurationKey("DetectTagType", ((int) this.m_CachedKeys.detectTagType).ToString(CultureInfo.InvariantCulture));
            }
            this.WriteConfigKeySlotSize(Reader.CalculateSlotSize(maxTagsThatCanRespond));
            this.SendScanRequest();
            TagCollection tags = new TagCollection();
            this.ParseScanResponse(tags);
            if (blink)
            {
                foreach (ResponseTag tag in tags)
                {
                    this.PingTagPrivate(tag);
                }
            }
            return tags;
        }

        internal void SendCommand(byte[] byCommandToSendBuffer, int nBytesToSend)
        {
            string s = CalculateCRC(byCommandToSendBuffer, nBytesToSend - 1, 1);
            byte[] bytes = Encoding.ASCII.GetBytes(s);
            Array.Copy(bytes, 0, byCommandToSendBuffer, nBytesToSend, bytes.Length);
            byCommandToSendBuffer[nBytesToSend + 2] = 13;
            this.m_bConnected = false;
            this.m_stream.Write(byCommandToSendBuffer, 0, nBytesToSend + 3);
            this.m_bConnected = true;
            this.m_byLastSendBuffer = byCommandToSendBuffer;
        }

        private void SendScanRequest()
        {
            byte[] byCommandToSendBuffer = new byte[0x20];
            byCommandToSendBuffer[0] = 0x23;
            byCommandToSendBuffer[1] = 50;
            byCommandToSendBuffer[2] = 0x30;
            byCommandToSendBuffer[3] = (byte) (this.m_nActiveAntenna + 0x30);
            byCommandToSendBuffer[4] = 0x30;
            byCommandToSendBuffer[5] = 0x30;
            this.SendCommand(byCommandToSendBuffer, 6);
            this.ReceiveCommand(FunctionCode.ScanTags, this.m_myReadBuffer, ref this.m_nTotalBytesReceived);
        }

        public bool SessionSetupTag(iD2Tag tag, int seconds, bool sleep, bool quiet)
        {
            return this.SleepTagPrivate(tag, seconds);
        }

        public void SetCableLoss(int antenna, int dBm)
        {
            AntennaArgumentOutOfRangeCheck(antenna);
            if ((5 != antenna) && (!this.m_bAntennaSettingsCached || (this.m_ArrayAntennas[antenna - 1].nCableLoss != dBm)))
            {
                this.m_ArrayAntennas[antenna - 1].nCableLoss = dBm;
                this.WriteConfigKeyAntCableLoss(antenna, dBm);
            }
        }

        public void SetMainID(int id)
        {
            this.WriteConfigurationKey("IPortMainID", id.ToString());
        }

        public bool SetOutputs(OutputOption[] outputOptions)
        {
            byte[] destinationArray = new byte[0x40];
            destinationArray[0] = 0x23;
            destinationArray[1] = 0x34;
            destinationArray[2] = 0x30;
            int destinationIndex = 3;
            for (int i = 0; i < 4; i++)
            {
                int setting;
                if (outputOptions[i].setting == OutputSetting.Timed)
                {
                    if (outputOptions[i].elapsedTime.TotalSeconds > 25.0)
                    {
                        throw new ArgumentOutOfRangeException("outputOptions");
                    }
                    setting = (int) (outputOptions[i].elapsedTime.TotalMilliseconds / 100.0);
                }
                else
                {
                    setting = (int) outputOptions[i].setting;
                }
                byte[] bytes = Encoding.ASCII.GetBytes(string.Format(CultureInfo.InvariantCulture, "{0:X2}", new object[] { setting }));
                Array.Copy(bytes, 0, destinationArray, destinationIndex, bytes.Length);
                destinationIndex += 2;
            }
            this.SendCommand(destinationArray, 11);
            this.ReceiveCommand(FunctionCode.SetOutputs, this.m_myReadBuffer, ref this.m_nTotalBytesReceived);
            return true;
        }

        public void SetReceiveThreshold(int antenna, int dBm)
        {
            AntennaArgumentOutOfRangeCheck(antenna);
            if ((5 != antenna) && (!this.m_bAntennaSettingsCached || (this.m_ArrayAntennas[antenna - 1].nRxThreshold != dBm)))
            {
                this.m_ArrayAntennas[antenna - 1].nRxThreshold = dBm;
                this.WriteConfigKeyAntRxThreshold(antenna, dBm);
            }
        }

        public void SetSubID(int id)
        {
            this.WriteConfigurationKey("IPortSubID", id.ToString());
        }

        public bool SetTagRangeState(iQTag tag, bool enableExtendedRange)
        {
            if (enableExtendedRange)
            {
                return this.ToggleTagModes(tag, 4);
            }
            return this.ToggleTagModes(tag, 0x40);
        }

        public void SetTxPowerForID2Tags(int antenna, int dBm)
        {
            AntennaArgumentOutOfRangeCheck(antenna);
            if (!this.m_bAntennaSettingsCached || (this.m_ArrayAntennas[antenna - 1].niDTxPwr != dBm))
            {
                this.m_ArrayAntennas[antenna - 1].niDTxPwr = dBm;
                this.WriteConfigKeyAntiDTxPwr(antenna, dBm);
            }
        }

        public void SetTxPowerForID2TagsOnWakeupAntenna(int dbm)
        {
            this.SetTxPowerForID2Tags(5, dbm);
        }

        public void SetTxPowerForIQTags(int antenna, int dBm)
        {
            AntennaArgumentOutOfRangeCheck(antenna);
            if (!this.m_bAntennaSettingsCached || (this.m_ArrayAntennas[antenna - 1].niQTxPwr != dBm))
            {
                this.m_ArrayAntennas[antenna - 1].niQTxPwr = dBm;
                this.WriteConfigKeyAntiQTxPwr(antenna, dBm);
            }
        }

        public void SetTxPowerForIQTagsOnWakeupAntenna(int dbm)
        {
            this.SetTxPowerForIQTags(5, dbm);
        }

        public bool SleepTag(iQTag tag, int seconds)
        {
            return this.SleepTagPrivate(tag, seconds);
        }

        public bool SleepTagPrivate(ResponseTag tag, int seconds)
        {
            this.IsActiveAntennaValid();
            tag.ResetSignals();
            byte[] destinationArray = new byte[0x20];
            destinationArray[0] = 0x23;
            destinationArray[1] = 50;
            destinationArray[2] = 0x36;
            destinationArray[3] = (byte) (this.m_nActiveAntenna + 0x30);
            byte[] bytes = Encoding.ASCII.GetBytes(tag.HexID);
            Array.Copy(bytes, 0, destinationArray, 4, bytes.Length);
            if (tag is iQTag)
            {
                destinationArray[12] = 0x30;
            }
            else
            {
                destinationArray[12] = 0x31;
            }
            destinationArray[13] = 0x30;
            byte[] sourceArray = Encoding.ASCII.GetBytes(string.Format(CultureInfo.InvariantCulture, "{0:X2}", new object[] { seconds }));
            Array.Copy(sourceArray, 0, destinationArray, 14, sourceArray.Length);
            this.SendCommand(destinationArray, 0x10);
            this.ReceiveCommand(FunctionCode.SleepTag, this.m_myReadBuffer, ref this.m_nTotalBytesReceived);
            if ((this.m_ErrorCode != ErrorCode.NoError) && (this.m_ErrorCode != ErrorCode.OK))
            {
                return false;
            }
            this.ParseBLINKResponse(tag);
            return true;
        }

        public void StartTagDigitalInputEventLog(iQTag tag)
        {
            this.StartTagDigitalInputEventLog(tag, false);
        }

        public void StartTagDigitalInputEventLog(iQTag tag, bool synchronize)
        {
            throw new NotSupportedException();
        }

        public void StartTagLogging(iQTag tag, TimeSpan samplingRate)
        {
            throw new NotImplementedException("Not yet implemented");
        }

        public void StopTagLogging(iQTag tag)
        {
            throw new NotImplementedException("Not yet implemented");
        }

        public bool TestConnection()
        {
            try
            {
                this.GetTime();
            }
            catch (IOException)
            {
                this.m_bConnected = false;
                throw;
            }
            return true;
        }

        private bool ToggleTagModes(iQTag tag, byte byModeFlag)
        {
            byte num = 0;
            byte num2 = 0;
            byte num3 = 0;
            byte num4 = 0;
            TagReadDataResult result = this.ReadTagDataPrivate(tag, 0, 1, false);
            if (!result.Success)
            {
                return false;
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
                if (!this.WriteTagDataPrivate(tag, 0, BitConverter.GetBytes((short) num2), 1).Success)
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
            return "i-PORT 3";
        }

        private void WriteConfigKeyAntCableLoss(int nAntenna, int nLoss)
        {
            string strKey = string.Format(CultureInfo.InvariantCulture, "Ant{0}CableLoss", new object[] { nAntenna });
            if (5 == nAntenna)
            {
                strKey = "AntWCableLoss";
            }
            this.WriteConfigurationKey(strKey, nLoss.ToString(CultureInfo.InvariantCulture));
        }

        private void WriteConfigKeyAntEnable(int nAntenna, bool enable)
        {
            string strKey = string.Format(CultureInfo.InvariantCulture, "Ant{0}Enable", new object[] { nAntenna });
            string str2 = string.Format(CultureInfo.InvariantCulture, "Ant{0}ScanEnable", new object[] { nAntenna });
            if (5 == nAntenna)
            {
                strKey = "AntWEnable";
                str2 = "AntWScanEnable";
            }
            if (enable)
            {
                this.WriteConfigurationKey(strKey, "1");
                this.WriteConfigurationKey(str2, "1");
            }
            else
            {
                this.WriteConfigurationKey(strKey, "0");
                this.WriteConfigurationKey(str2, "0");
            }
        }

        private void WriteConfigKeyAntiDEnableRxSens(int nAntenna, bool enable)
        {
            string strKey = string.Format(CultureInfo.InvariantCulture, "Ant{0}HighSensD", new object[] { nAntenna });
            if (5 != nAntenna)
            {
                if (enable)
                {
                    this.WriteConfigurationKey(strKey, "1");
                }
                else
                {
                    this.WriteConfigurationKey(strKey, "0");
                }
            }
        }

        private void WriteConfigKeyAntiDTxPwr(int nAntenna, int nPower)
        {
            string strKey = string.Format(CultureInfo.InvariantCulture, "Ant{0}TxPowerD", new object[] { nAntenna });
            if (5 == nAntenna)
            {
                strKey = "AntWTxPowerD";
            }
            this.WriteConfigurationKey(strKey, nPower.ToString(CultureInfo.InvariantCulture));
        }

        private void WriteConfigKeyAntiQEnableRxSens(int nAntenna, bool enable)
        {
            string strKey = string.Format(CultureInfo.InvariantCulture, "Ant{0}HighSens", new object[] { nAntenna });
            if (5 != nAntenna)
            {
                if (enable)
                {
                    this.WriteConfigurationKey(strKey, "1");
                }
                else
                {
                    this.WriteConfigurationKey(strKey, "0");
                }
            }
        }

        private void WriteConfigKeyAntiQTxPwr(int nAntenna, int nPower)
        {
            string strKey = string.Format(CultureInfo.InvariantCulture, "Ant{0}TxPowerQ", new object[] { nAntenna });
            if (5 == nAntenna)
            {
                strKey = "AntWTxPowerQ";
            }
            this.WriteConfigurationKey(strKey, nPower.ToString(CultureInfo.InvariantCulture));
        }

        private void WriteConfigKeyAntRxThreshold(int nAntenna, int nThreshold)
        {
            string strKey = string.Format(CultureInfo.InvariantCulture, "Ant{0}RxThreshold", new object[] { nAntenna });
            if (5 == nAntenna)
            {
                strKey = "AntWRxThreshold";
            }
            this.WriteConfigurationKey(strKey, nThreshold.ToString(CultureInfo.InvariantCulture));
        }

        internal void WriteConfigKeyEventMsgEnable(bool enableEventMode)
        {
            this.WriteConfigurationKey("EventMsgEnable", enableEventMode ? "1" : "0");
        }

        internal void WriteConfigKeyScanMode(ScanMode scanMode)
        {
            this.WriteConfigurationKey("ScanMode", ((int) scanMode).ToString(CultureInfo.InvariantCulture));
        }

        internal void WriteConfigKeyScanPause(int pauseSeconds)
        {
            this.WriteConfigurationKey("ScanPause", string.Format(CultureInfo.InvariantCulture, "{0:X2}", new object[] { pauseSeconds * 10 }));
        }

        private void WriteConfigKeySlotSize(int nSlots)
        {
            if (nSlots != this.m_CachedKeys.nSlots)
            {
                this.WriteConfigurationKey("ScanSlotSelect", nSlots.ToString(CultureInfo.InvariantCulture));
                this.m_CachedKeys.nSlots = nSlots;
            }
        }

        internal void WriteConfigurationKey(string strKey, string strValue)
        {
            byte[] destinationArray = new byte[0x40];
            destinationArray[0] = 0x23;
            destinationArray[1] = 0x36;
            destinationArray[2] = 0x31;
            for (int i = 0; i < 0x20; i++)
            {
                destinationArray[i + 3] = 0x20;
            }
            byte[] bytes = Encoding.ASCII.GetBytes(strKey);
            Array.Copy(bytes, 0, destinationArray, 0x13 - bytes.Length, bytes.Length);
            byte[] sourceArray = Encoding.ASCII.GetBytes(strValue);
            Array.Copy(sourceArray, 0, destinationArray, 0x23 - sourceArray.Length, sourceArray.Length);
            this.SendCommand(destinationArray, 0x23);
            this.m_strLastConfigKey = strKey;
            this.ReceiveCommand(FunctionCode.WriteConfigKey, this.m_myReadBuffer, ref this.m_nTotalBytesReceived);
        }

        public void WriteIQTagUserEngineHourCounter(iQTag tag, TimeSpan ts)
        {
            EngineHourTagHelper.WriteIQTagUserEngineHourCounter(this, tag, ts);
        }

        public TagWriteDataResult WriteTagData(iD2Tag tag, int address, byte[] byData, int bytesToWrite)
        {
            return this.WriteTagDataPrivate(tag, address, byData, bytesToWrite);
        }

        public TagWriteDataResult WriteTagData(iQTag tag, int address, byte[] byData, int bytesToWrite)
        {
            return this.WriteTagDataPrivate(tag, address, byData, bytesToWrite);
        }

        internal TagWriteDataResult WriteTagDataPrivate(ResponseTag tag, int address, byte[] byData, int bytesToWrite)
        {
            this.IsActiveAntennaValid();
            if (!this.m_CachedKeys.EnableBlinkOnTagData)
            {
                this.m_CachedKeys.EnableBlinkOnTagData = true;
                this.WriteConfigurationKey("BlinkEnable", this.m_CachedKeys.EnableBlinkOnTagData ? "1" : "0");
            }
            tag.ResetSignals();
            TagWriteDataResult result = new TagWriteDataResult();
            byte[] destinationArray = new byte[0x18 + (bytesToWrite * 2)];
            destinationArray[0] = 0x23;
            destinationArray[1] = 50;
            destinationArray[2] = 50;
            destinationArray[3] = (byte) (this.m_nActiveAntenna + 0x30);
            byte[] bytes = Encoding.ASCII.GetBytes(tag.HexID);
            Array.Copy(bytes, 0, destinationArray, 4, bytes.Length);
            if (tag is iQTag)
            {
                destinationArray[12] = 0x30;
            }
            else
            {
                destinationArray[12] = 0x31;
            }
            ushort num = (ushort) address;
            byte[] sourceArray = Encoding.ASCII.GetBytes(string.Format(CultureInfo.InvariantCulture, "{0:X4}", new object[] { num }));
            Array.Copy(sourceArray, 0, destinationArray, 13, sourceArray.Length);
            ushort num2 = (ushort) bytesToWrite;
            byte[] buffer4 = Encoding.ASCII.GetBytes(string.Format(CultureInfo.InvariantCulture, "{0:X4}", new object[] { num2 }));
            Array.Copy(buffer4, 0, destinationArray, 0x11, buffer4.Length);
            for (int i = 0; i < bytesToWrite; i++)
            {
                Array.Copy(Encoding.ASCII.GetBytes(string.Format(CultureInfo.InvariantCulture, "{0:X2}", new object[] { byData[i] })), 0, destinationArray, 0x15 + (i * 2), 2);
            }
            this.SendCommand(destinationArray, 0x15 + (num2 * 2));
            this.ReceiveCommand(FunctionCode.WriteTagData, this.m_myReadBuffer, ref this.m_nTotalBytesReceived);
            if ((this.m_ErrorCode == ErrorCode.NoError) || (this.m_ErrorCode == ErrorCode.OK))
            {
                this.ParseBLINKResponse(tag);
                result.BytesWritten = bytesToWrite;
                result.Success = true;
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

        public int ActiveAntenna
        {
            get
            {
                return this.m_nActiveAntenna;
            }
            set
            {
                if ((value > 4) || (value < 0))
                {
                    throw new ArgumentOutOfRangeException("value");
                }
                this.m_nActiveAntenna = value;
            }
        }

        public override bool Connected
        {
            get
            {
                return this.m_bConnected;
            }
        }

        public ErrorCode DeviceStatus
        {
            get
            {
                return this.m_ErrorCode;
            }
        }

        public DateTime LastContact
        {
            get
            {
                return this.m_dtLastContact;
            }
        }

        public string Location
        {
            get
            {
                return this.m_strLocation;
            }
        }

        public TCPSocketStream TcpClient
        {
            get
            {
                return this.m_tcpClient;
            }
            set
            {
                this.m_tcpClient = value;
            }
        }

        public string Version
        {
            get
            {
                return this.m_strVersion;
            }
        }

        [StructLayout(LayoutKind.Sequential)]
        internal struct CachedKeys
        {
            private bool m_bEnableBlinkiD2TagData;
            private iPort3.DetectTagType _detectTagType;
            private int _nSlots;
            public bool EnableBlinkOnTagData
            {
                get
                {
                    return this.m_bEnableBlinkiD2TagData;
                }
                set
                {
                    this.m_bEnableBlinkiD2TagData = value;
                }
            }
            public iPort3.DetectTagType detectTagType
            {
                get
                {
                    return this._detectTagType;
                }
                set
                {
                    this._detectTagType = value;
                }
            }
            public int nSlots
            {
                get
                {
                    return this._nSlots;
                }
                set
                {
                    this._nSlots = value;
                }
            }
        }

        internal enum DetectTagType
        {
            IQ,
            ID
        }

        public enum ErrorCode
        {
            AntennaNotEnabled = -577,
            CommandNotAllowed = -563,
            CommandNotSupported = -565,
            Common = -564,
            ConfigurationKeyOutOfBounds = -610,
            IPError = -512,
            LoggerInactive = -584,
            NAK = -560,
            NoAccessToAddressRange = -580,
            NoConfigurationKeyWithThisName = -609,
            NoError = -560,
            NotConnected = -2147483648,
            OK = 0,
            OperationPartialSuccess = -579,
            RangeOrFormat = -562,
            SelectedTagNotInList = -582,
            StartAddressOutOfRange = -581,
            TagCommunicationPermissionDenied = -586,
            TagNotInField = -578,
            TagTypeDoesNotSupportOperation = -583,
            TemperatureOutOf256Range = -585,
            UnknownCommand = -561
        }

        internal enum FunctionCode : byte
        {
            BlinkTag = 0x25,
            ClearAllEvents = 130,
            GetInfo = 1,
            GetStatusOfInputs = 0x41,
            GetTimeDate = 2,
            GetVersion = 5,
            InputEvent = 0x81,
            LoadConfigFromEEPROM = 0x63,
            LoadDefaultConfig = 0x62,
            NewTagEvent = 0x80,
            None = 0,
            ReadConfigKey = 0x60,
            ReadTagData = 0x21,
            ReadTagLoggingData = 0x24,
            Reset = 4,
            ScanTags = 0x20,
            ScanWithData = 0x27,
            SetOutputs = 0x40,
            SetTimeDate = 3,
            SleepTag = 0x26,
            StoreConfigToEEPROM = 100,
            TagLoggerControl = 0x23,
            WriteConfigKey = 0x61,
            WriteTagData = 0x22
        }

        [StructLayout(LayoutKind.Sequential)]
        internal struct iPort3Antenna
        {
            public bool bEnabled;
            public int niQTxPwr;
            public int niDTxPwr;
            public bool bEnableiQRxSens;
            public bool bEnableiDRxSens;
            public int nRxThreshold;
            public int nCableLoss;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct OutputOption
        {
            public iPort3.OutputSetting setting;
            public TimeSpan elapsedTime;
        }

        public enum OutputSetting
        {
            NoChange = 0xff,
            Off = 0,
            On = 0xfe,
            Timed = -1
        }

        internal enum ScanMode : byte
        {
            Continuous = 1,
            HostOnly = 0,
            InputOne = 2
        }
    }
}

