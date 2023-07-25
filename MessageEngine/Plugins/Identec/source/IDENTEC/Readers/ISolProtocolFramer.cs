namespace IDENTEC.Readers
{
    using IDENTEC;
    using NLog;
    using System;
    using System.Net;
    using System.Net.Sockets;
    using System.Runtime.CompilerServices;
    using System.Threading;

    internal class ISolProtocolFramer : IDisposable
    {
        private IDENTEC.DataStream _dataStream;
        private byte[] _lastSentMessageBuffer = new byte[0x100];
        internal bool bPollTCP4AvailableDataFirst;
        private const byte DLE = 0x10;
        private const byte EOT = 4;
        protected static Logger log = LogManager.GetLogger("ISolProtocolFramer");
        private bool m_bTCPConnected;
        internal bool m_bTraceOutSendMessage;
        private byte[] m_byArrayReadHelper = new byte[0x200];
        internal byte m_byBusAddress;
        internal byte[] m_bySendBuffer = new byte[0x400];
        private Mutex m_mutexConnection;
        private int m_nBytesAlreadyRead;
        protected uint m_nReceivePacketCount;
        protected uint m_nSendPacketCount;
        private Port m_Port;
        private System.Net.Sockets.Socket m_socket;
        private ICompatibleIOStream m_stream;
        internal Transport m_transport;
        private const byte SOH = 1;
        internal TimeSpan tsTimeOutPoll4AvailableTCPData;

        public event PacketReceivedEventHandler PacketReceived;

        public event PacketSentEventHandler PacketSent;

        public void ClearReadBuffer()
        {
            Array.Clear(this.m_byArrayReadHelper, 0, this.m_byArrayReadHelper.Length);
            this.m_nBytesAlreadyRead = 0;
        }

        public void ClearReceiveBuffer()
        {
            this.m_Port.PurgeComm();
            this.m_nBytesAlreadyRead = 0;
        }

        private void CloseConnectionMutex()
        {
            if (this.m_mutexConnection != null)
            {
                this.m_mutexConnection.Close();
                this.m_mutexConnection = null;
            }
        }

        private bool Connect(string strConnection)
        {
            return this.Connect(strConnection, 0x1c200);
        }

        private bool Connect(string strConnection, int baudRate)
        {
            if (this.m_Port == null)
            {
                HandshakeNone initialSettings = new HandshakeNone {
                    BasicSettings = { BaudRate = (IDENTEC.BaudRates)baudRate },
                    DTRControl = DTRControlFlows.disable,
                    RTSControl = RTSControlFlows.disable
                };
                this.m_Port = new Port(strConnection, initialSettings);
                this.m_Port.OverlappedEnabled = false;
                this.m_Port.OnError += new Port.CommErrorEvent(this.m_Port_OnError);
                return this.m_Port.Open();
            }
            if (this.m_Port.IsOpen)
            {
                return false;
            }
            this.m_Port = null;
            return this.Connect(strConnection, baudRate);
        }

        public bool ConnectPCMCIA(int nPort)
        {
            if (NativeMethods.FullFramework)
            {
                nPort--;
                this.CreateConnectionMutex(nPort, true);
                return this.Connect(@"\\.\Icard" + nPort.ToString());
            }
            return this.Connect("ILR" + nPort.ToString() + ":");
        }

        public bool ConnectSerialPort(int port)
        {
            return this.ConnectSerialPort(port, 0x1c200);
        }

        public bool ConnectSerialPort(int port, int baudRate)
        {
            this.CreateConnectionMutex(port, false);
            if (port < 10)
            {
                return this.Connect("COM" + port.ToString() + ":", baudRate);
            }
            return this.Connect(@"\\.\COM" + port.ToString(), baudRate);
        }

        public void ConnectTCP(IPAddress address, int port)
        {
            this.InitializeTCPClient();
            this.m_bTCPConnected = false;
            this.m_socket.Connect(new IPEndPoint(address, port));
            this.m_bTCPConnected = true;
        }

        public void ConnectTCP(string hostname, int port)
        {
            try
            {
                IPAddress address = IPAddress.Parse(hostname);
                if (address != null)
                {
                    this.ConnectTCP(address, port);
                }
            }
            catch (FormatException)
            {
                this.InitializeTCPClient();
                this.m_bTCPConnected = false;
                this.m_socket.Connect(new IPEndPoint(IPAddress.Parse(hostname), port));
                this.m_bTCPConnected = true;
            }
        }

        private void CreateConnectionMutex(int nPort, bool bPCMCIA)
        {
            if ((NativeMethods.FullFramework && (this.m_mutexConnection == null)) && !true)
            {
                this.CloseConnectionMutex();
                throw new iCardCommunicationsException("Another application or process is already connected to the i-CARD 3 specified");
            }
        }

        public virtual bool Disconnect()
        {
            this.CloseConnectionMutex();
            this.m_nBytesAlreadyRead = 0;
            switch (this.m_transport)
            {
                case Transport.Serial:
                    if (this.m_Port == null)
                    {
                        break;
                    }
                    return this.m_Port.Close();

                case Transport.TCP:
                    if (this.m_socket == null)
                    {
                        break;
                    }
                    this.m_socket.Close();
                    this.m_socket = null;
                    this.m_bTCPConnected = false;
                    return true;

                case Transport.Stream:
                    return true;
            }
            return false;
        }

        public void Dispose()
        {
            if (this.m_Port != null)
            {
                this.m_Port.Dispose();
            }
        }

        ~ISolProtocolFramer()
        {
            if (this.m_Port != null)
            {
                this.m_Port.Dispose();
            }
        }

        private void InitializeTCPClient()
        {
            this.m_transport = Transport.TCP;
            this.m_socket = new System.Net.Sockets.Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        }

        private void m_Port_OnError(string Description)
        {
        }

        public static byte[] PackageMessage(byte[] msg, ref int len)
        {
            byte[] bySendBuffer = new byte[2 + ((msg.Length + 2) * 2)];
            ushort num = IDENTEC.Readers.CRC.CRC16(msg, len);
            bySendBuffer[0] = 1;
            int nIndex = 1;
            int num3 = 0;
            while (len-- > 0)
            {
                StuffiCard3Message(ref nIndex, bySendBuffer, msg[num3++]);
            }
            StuffiCard3Message(ref nIndex, bySendBuffer, (byte) (num & 0xff));
            StuffiCard3Message(ref nIndex, bySendBuffer, (byte) (num >> 8));
            bySendBuffer[nIndex++] = 4;
            byte[] destinationArray = new byte[nIndex];
            Array.Copy(bySendBuffer, 0, destinationArray, 0, nIndex);
            return destinationArray;
        }

        protected bool ReadPortCached(byte[] byArray, int nBytesToRead, ref int nRead)
        {
            bool flag = true;
            if (this.m_nBytesAlreadyRead == 0)
            {
                switch (this.m_transport)
                {
                    case Transport.Serial:
                        flag = this.m_Port.ReadPort(this.m_byArrayReadHelper, 0x100, ref this.m_nBytesAlreadyRead);
                        break;

                    case Transport.TCP:
                        if (this.bPollTCP4AvailableDataFirst)
                        {
                            DateTime time = DateTime.Now.Add(this.tsTimeOutPoll4AvailableTCPData);
                            while (this.m_socket.Available <= 0)
                            {
                                if (DateTime.Now > time)
                                {
                                    return false;
                                }
                                Thread.Sleep(1);
                            }
                        }
                        try
                        {
                            this.m_nBytesAlreadyRead = this.m_socket.Receive(this.m_byArrayReadHelper, 0, this.m_byArrayReadHelper.Length, SocketFlags.None);
                        }
                        catch (Exception)
                        {
                            this.m_bTCPConnected = false;
                            throw;
                        }
                        if (this.m_nBytesAlreadyRead <= 0)
                        {
                            flag = false;
                        }
                        else
                        {
                            flag = true;
                        }
                        break;

                    case Transport.Stream:
                        if (this.m_stream == null)
                        {
                            throw new InvalidOperationException("The data stream is null");
                        }
                        if (!this.m_stream.IsOpen())
                        {
                            throw new InvalidOperationException("The data stream is not open");
                        }
                        this.m_nBytesAlreadyRead = this.m_stream.ReadData(this.m_byArrayReadHelper, 0, this.m_byArrayReadHelper.Length);
                        if (this.m_nBytesAlreadyRead <= 0)
                        {
                            flag = false;
                        }
                        else
                        {
                            flag = true;
                        }
                        break;

                    case Transport.DataStream2007:
                        flag = true;
                        this.m_nBytesAlreadyRead = this._dataStream.Read(this.m_byArrayReadHelper, 0, 0x100);
                        break;
                }
            }
            nRead = this.m_nBytesAlreadyRead;
            if (this.m_nBytesAlreadyRead > 0)
            {
                if (nBytesToRead < nRead)
                {
                    Array.Copy(this.m_byArrayReadHelper, 0, byArray, 0, nBytesToRead);
                    this.m_nBytesAlreadyRead -= nBytesToRead;
                    nRead = nBytesToRead;
                }
                else
                {
                    Array.Copy(this.m_byArrayReadHelper, 0, byArray, 0, nRead);
                    this.m_nBytesAlreadyRead -= nRead;
                }
                if (this.m_nBytesAlreadyRead > 0)
                {
                    Array.Copy(this.m_byArrayReadHelper, nRead, this.m_byArrayReadHelper, 0, this.m_nBytesAlreadyRead);
                }
                return flag;
            }
            nRead = 0;
            return flag;
        }

        internal virtual int RecvMsg(byte[] msg, int timeout, int nPauseBeforeRead)
        {
            int index = 0;
            int nRead = 0;
            byte[] byArray = new byte[1];
            Thread.Sleep(nPauseBeforeRead);
            int tickCount = Environment.TickCount;
        Label_0017:
            try
            {
                if (!this.ReadPortCached(byArray, 1, ref nRead))
                {
                    return -1;
                }
            }
            catch (iCardTimeoutException exception)
            {
                throw new ReaderTimeoutException(exception.Message);
            }
            if (nRead > 0)
            {
                msg[index] = byArray[0];
                index += nRead;
                if (msg.Length <= index)
                {
                    throw new iCardCommunicationsException("Error parsing i-CARD 3 message: " + BitConverter.ToString(msg, 0, msg.Length));
                }
            }
            if (Reader.TimedOut(ref tickCount, timeout))
            {
                throw new iCardTimeoutException("Low level communications timeout. Timeout setting was " + timeout + "ms");
            }
            if (nRead == 0)
            {
                goto Label_0017;
            }
            if ((index == 0) && (byArray[0] != 1))
            {
                Thread.Sleep(0);
                goto Label_0017;
            }
            if (byArray[0] == 1)
            {
                index = 1;
                msg[0] = 1;
            }
            if (byArray[0] != 4)
            {
                goto Label_0017;
            }
            if (index <= 2)
            {
                throw new iCardCommunicationsException("Low level message format error");
            }
            if (log.IsDebugEnabled)
            {
                string message = "RX:";
                for (int i = 0; i < index; i++)
                {
                    message = message + msg[i].ToString("X") + ":";
                }
                log.Debug(message);
            }
            byte[] destinationArray = new byte[index];
            Array.Copy(msg, 1, destinationArray, 0, index);
            index = UnstuffiCard3Message(msg, destinationArray, index - 2);
            if (index <= 2)
            {
                throw new iCardCommunicationsException("Low level message format error");
            }
            if (!IDENTEC.Readers.CRC.CRCok(msg, index))
            {
                throw new CRCException("The message received from the reader has an invalid CRC", BitConverter.ToString(msg, 0, index));
            }
            this.m_nReceivePacketCount++;
            if (this.PacketReceived != null)
            {
                this.PacketReceived(this);
            }
            return (index - 2);
        }

        internal void SendMessage(byte[] msg, int len)
        {
            byte[] buffer = PackageMessage(msg, ref len);
            this._lastSentMessageBuffer = buffer;
            if (log.IsDebugEnabled)
            {
                string message = "TX:";
                for (int i = 0; i < buffer.Length; i++)
                {
                    message = message + buffer[i].ToString("X") + ":";
                }
                log.Debug(message);
            }
            switch (this.m_transport)
            {
                case Transport.Serial:
                    if (this.m_Port == null)
                    {
                        throw new iCardCommunicationsException("The device is not connected");
                    }
                    if (!this.m_Port.IsOpen)
                    {
                        throw new iCardCommunicationsException("The device is not connected");
                    }
                    this.ClearReceiveBuffer();
                    this.m_Port.Output = buffer;
                    goto Label_0125;

                case Transport.TCP:
                    try
                    {
                        this.m_socket.Send(buffer, 0, buffer.Length, SocketFlags.None);
                        goto Label_0125;
                    }
                    catch (Exception)
                    {
                        this.m_bTCPConnected = false;
                        throw;
                    }
                    break;

                case Transport.Stream:
                    if (this.m_stream == null)
                    {
                        throw new InvalidOperationException("Data Stream has not been set");
                    }
                    if (!this.m_stream.IsOpen())
                    {
                        throw new InvalidOperationException("Data Stream is not open");
                    }
                    this.m_stream.WriteData(buffer, buffer.Length);
                    goto Label_0125;

                case Transport.DataStream2007:
                    break;

                default:
                    goto Label_0125;
            }
            this._dataStream.Write(buffer, 0, buffer.Length);
        Label_0125:
            this.m_nSendPacketCount++;
            if (this.PacketSent != null)
            {
                this.PacketSent(this);
            }
        }

        protected void SetPacketReceivedEvent()
        {
            if (this.PacketReceived != null)
            {
                this.PacketReceived(this);
            }
        }

        internal static void StuffiCard3Message(ref int nIndex, byte[] bySendBuffer, byte byChar)
        {
            switch (byChar)
            {
                case 1:
                    bySendBuffer[nIndex++] = 0x10;
                    byChar = (byte) (byChar ^ 0x80);
                    bySendBuffer[nIndex++] = byChar;
                    return;

                case 4:
                    bySendBuffer[nIndex++] = 0x10;
                    byChar = (byte) (byChar ^ 0x80);
                    bySendBuffer[nIndex++] = byChar;
                    return;

                case 0x10:
                    bySendBuffer[nIndex++] = 0x10;
                    byChar = (byte) (byChar ^ 0x80);
                    bySendBuffer[nIndex++] = byChar;
                    return;
            }
            bySendBuffer[nIndex++] = byChar;
        }

        internal static int UnstuffiCard3Message(byte[] byOut, byte[] byIn, int len)
        {
            int num = 0;
            byte num2 = 0;
            int index = 0;
            int num4 = 0;
            while (len-- > 0)
            {
                byte num5 = byIn[index];
                if (num5 == 0x10)
                {
                    num2 = 0x80;
                }
                else
                {
                    byOut[num4++] = (byte) (byIn[index] ^ num2);
                    num++;
                    num2 = 0;
                }
                index++;
            }
            if (num2 != 0)
            {
                return 0;
            }
            return num;
        }

        public ICompatibleIOStream DataStream
        {
            get
            {
                return this.m_stream;
            }
            set
            {
                this.m_transport = Transport.Stream;
                this.m_stream = value;
            }
        }

        public IDENTEC.DataStream DataStream2007
        {
            get
            {
                return this._dataStream;
            }
            set
            {
                this._dataStream = value;
                this.m_transport = Transport.DataStream2007;
            }
        }

        internal bool IsOpen
        {
            get
            {
                switch (this.m_transport)
                {
                    case Transport.Serial:
                        return ((this.m_Port != null) && this.m_Port.IsOpen);

                    case Transport.TCP:
                        return ((this.m_socket != null) && this.m_bTCPConnected);

                    case Transport.Stream:
                        return this.m_stream.IsOpen();

                    case Transport.DataStream2007:
                        return this._dataStream.IsOpen;
                }
                return false;
            }
        }

        public string LastSentMessageAsHexString
        {
            get
            {
                if (this._lastSentMessageBuffer != null)
                {
                    return BitConverter.ToString(this._lastSentMessageBuffer, 0);
                }
                return "";
            }
        }

        public byte[] LastSentMessageBuffer
        {
            get
            {
                return this._lastSentMessageBuffer;
            }
        }

        public int ReadBytesCached
        {
            get
            {
                return this.m_nBytesAlreadyRead;
            }
        }

        public uint ReceivedPackets
        {
            get
            {
                return this.m_nReceivePacketCount;
            }
        }

        public uint SentPackets
        {
            get
            {
                return this.m_nSendPacketCount;
            }
        }

        public System.Net.Sockets.Socket Socket
        {
            get
            {
                return this.m_socket;
            }
        }

        public delegate void PacketReceivedEventHandler(object sender);

        public delegate void PacketSentEventHandler(object sender);

        internal enum Transport
        {
            Serial,
            TCP,
            Stream,
            DataStream2007
        }
    }
}

