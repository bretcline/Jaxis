namespace IDENTEC.Readers
{
    using IDENTEC.Readers.BeaconReaders;
    using System;
    using System.Collections;
    using System.IO;
    using System.Net.Sockets;
    using System.Text;
    using System.Threading;

    [Obsolete("This class is now obsolete and will removed in future versions of this library. Please use the 'iBusAdapter' class instead.", false)]
    public class ModularReaderBus
    {
        internal ISolProtocolFramer m_isolProtocolFramer = new ISolProtocolFramer2();
        private int m_nTimeout = 0x1388;
        protected ArrayList m_Readers = new ArrayList();
        private static readonly int MaxReadersAllowedOnDaisyChain = 0x10;

        public ModularReaderBus()
        {
            this.m_isolProtocolFramer.m_bTraceOutSendMessage = false;
        }

        public virtual bool ConnectSerial(int port)
        {
            return this.m_isolProtocolFramer.ConnectSerialPort(port, 0x1c200);
        }

        public virtual bool ConnectSerial(int port, int baudRate)
        {
            return this.m_isolProtocolFramer.ConnectSerialPort(port, baudRate);
        }

        public virtual void ConnectTCP(string hostName, int port)
        {
            this.m_isolProtocolFramer.ConnectTCP(hostName, port);
        }

        public virtual bool Disconnect()
        {
            this.m_Readers.Clear();
            return this.m_isolProtocolFramer.Disconnect();
        }

        public ModularReader[] EnumerateReaders()
        {
            return this.EnumerateReaders(new TimeSpan(0, 0, 0, 2, 0));
        }

        public ModularReader[] EnumerateReaders(TimeSpan timeout)
        {
            return this.EnumerateReaders(timeout, -1);
        }

        public ModularReader[] EnumerateReaders(TimeSpan timeout, int maxDevices)
        {
            ModularReader[] readerArray;
            this.m_isolProtocolFramer.tsTimeOutPoll4AvailableTCPData = timeout;
            this.m_Readers.Clear();
            this.SendDisconnectDaisyChainMessage();
            int num = 0;
            int num2 = 1;
            do
            {
                ModularReader reader = null;
                try
                {
                    if (num != 0)
                    {
                        this.m_isolProtocolFramer.tsTimeOutPoll4AvailableTCPData = new TimeSpan(0, 0, 0, 0, (num * 2) + 100);
                    }
                    this.m_isolProtocolFramer.bPollTCP4AvailableDataFirst = true;
                    int tickCount = Environment.TickCount;
                    ModularReaderType type = this.QueryReadVersion(ModularReader.FIRSTREADERWITHDISCONNECTEDSLAVE);
                    this.m_isolProtocolFramer.bPollTCP4AvailableDataFirst = false;
                    int num4 = Environment.TickCount;
                    if ((num4 - tickCount) > 0)
                    {
                        num = num4 - tickCount;
                    }
                    switch (type)
                    {
                        case ModularReaderType.R2:
                            reader = new iPortMB(this);
                            break;

                        case ModularReaderType.T2:
                            reader = new iPortMQ(this);
                            break;

                        case ModularReaderType.MQ:
                            reader = new iPortMQ(this);
                            break;

                        case ModularReaderType.Indeterminate:
                            goto Label_0128;
                    }
                    reader.GetUptime();
                }
                catch (iCardTimeoutException)
                {
                    break;
                }
                catch (IOException)
                {
                    throw;
                }
                catch (InvalidOperationException)
                {
                    break;
                }
                catch (SocketException)
                {
                    break;
                }
                catch (ReaderTimeoutException)
                {
                    break;
                }
                finally
                {
                    this.m_isolProtocolFramer.bPollTCP4AvailableDataFirst = false;
                }
                reader.SetStaticAddress(num2++);
                reader.Initialize();
                reader.EnableSlavePort(true);
                this.m_Readers.Add(reader);
            }
            while ((num2 <= MaxReadersAllowedOnDaisyChain) && ((maxDevices == -1) || (this.m_Readers.Count < maxDevices)));
        Label_0128:
            readerArray = new ModularReader[this.m_Readers.Count];
            for (int i = 0; i < this.m_Readers.Count; i++)
            {
                readerArray[i] = this.m_Readers[i] as ModularReader;
            }
            return readerArray;
        }

        internal virtual ModularReaderType QueryReadVersion(byte byAddress)
        {
            byte[] msg = new byte[0x180];
            msg[0] = byAddress;
            msg[1] = 0x33;
            this.m_isolProtocolFramer.SendMessage(msg, 2);
            msg[0] = 0;
            msg[1] = 0;
            int num = this.m_isolProtocolFramer.RecvMsg(msg, 0x1388, 10);
            if (num <= 0)
            {
                return ModularReaderType.Indeterminate;
            }
            if ((msg[1] != 0xb3) || (num < 4))
            {
                return ModularReaderType.Indeterminate;
            }
            string str = Encoding.UTF8.GetString(msg, 4, 20).Replace("\0", "").TrimEnd(null);
            if (-1 != str.IndexOf("T2"))
            {
                return ModularReaderType.T2;
            }
            if (-1 != str.IndexOf("MQ"))
            {
                return ModularReaderType.MQ;
            }
            return ModularReaderType.R2;
        }

        internal void SendDisconnectDaisyChainMessage()
        {
            byte[] msg = new byte[] { 0xff, 0x43, 0x10, 0, 0, 0, 0 };
            this.m_isolProtocolFramer.SendMessage(msg, msg.Length);
            Thread.Sleep(200);
        }

        public int CommunicationsTimeout
        {
            get
            {
                return this.m_nTimeout;
            }
            set
            {
                if (value <= 0)
                {
                    throw new ArgumentOutOfRangeException("CommunicationsTimeout must be greater than 0");
                }
                this.m_nTimeout = value;
            }
        }

        public bool Connected
        {
            get
            {
                return this.m_isolProtocolFramer.IsOpen;
            }
        }

        public ICompatibleIOStream DataStream
        {
            get
            {
                return this.m_isolProtocolFramer.DataStream;
            }
            set
            {
                this.m_isolProtocolFramer.DataStream = value;
            }
        }

        public System.Net.Sockets.Socket Socket
        {
            get
            {
                return this.m_isolProtocolFramer.Socket;
            }
        }

        internal enum ModularReaderType
        {
            Indeterminate = -1,
            MQ = 2,
            R2 = 0,
            T2 = 1
        }
    }
}

