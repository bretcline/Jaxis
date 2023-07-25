namespace IDENTEC
{
    using IDENTEC.ILR350.Readers;
    using IDENTEC.PositionMarker;
    using IDENTEC.Readers;
    using IDENTEC.Readers.BeaconReaders;
    using NLog;
    using System;
    using System.Collections;
    using System.Runtime.CompilerServices;
    using System.Text;
    using System.Threading;

    public class iBusAdapter
    {
        private IBusDevice[] _Devices;
        internal IDENTEC.DataStream _stream;
        public static readonly byte BroadcastAddress = 0xff;
        public static readonly byte DISCONNECTED_SLAVE_ADDRESS = 0xfe;
        protected static Logger log = LogManager.GetLogger("iBusAdapter");

        public event UnknownBusDeviceHandler UnknownDeviceDiscovered;

        public iBusAdapter(IDENTEC.DataStream stream)
        {
            this._stream = stream;
        }

        public void BroadcastConnectMessage()
        {
            byte[] buffer = new byte[] { 0xff, 0x43, 0x10, 0, 0, 0, 1 };
            this._stream.SendMessage(buffer, buffer.Length);
            Thread.Sleep(100);
        }

        public void BroadcastDisconnectMessage()
        {
            byte[] buffer = new byte[] { 0xff, 0x43, 0x10, 0, 0, 0, 0 };
            this._stream.SendMessage(buffer, buffer.Length);
            Thread.Sleep(100);
        }

        public void BroadcastSyncMessage()
        {
            this.SendSyncMessage(BroadcastAddress);
        }

        public IBusDevice[] EnumerateBusModules()
        {
            return this.EnumerateBusModules(-1);
        }

        public IBusDevice[] EnumerateBusModules(int deviceCount)
        {
            return this.EnumerateBusModules(deviceCount, true);
        }

        public IBusDevice[] EnumerateBusModules(int deviceCount, bool readdress)
        {
            this.BroadcastDisconnectMessage();
            ArrayList list = new ArrayList();
            int num = 0x11;
            int num2 = 0;
        Label_0011:
            if (num2 == 0)
            {
                num2 = 0x3e8;
            }
            int tickCount = Environment.TickCount;
            IBusDevice nextBusDevice = this.GetNextBusDevice((num2 * 2) + 100);
            if (nextBusDevice != null)
            {
                num2 = Environment.TickCount - tickCount;
                if (readdress)
                {
                    nextBusDevice.SetBusAddress(num++);
                }
                else
                {
                    num = nextBusDevice.GetBusAddress() + 1;
                }
                nextBusDevice.Initialize();
                list.Add(nextBusDevice);
                if ((deviceCount == -1) || (((deviceCount != list.Count) && !(nextBusDevice is iCardCF)) && (!(nextBusDevice is iCardCFB) && !(nextBusDevice is iCardCF350))))
                {
                    nextBusDevice.ConnectSlavePort(true);
                    goto Label_0011;
                }
            }
            this._Devices = new IBusDevice[list.Count];
            IBusDevice[] deviceArray = new IBusDevice[list.Count];
            for (int i = 0; i < list.Count; i++)
            {
                deviceArray[i] = this._Devices[i] = list[i] as IBusDevice;
            }
            return deviceArray;
        }

        protected virtual IBusDevice GetNextBusDevice(int pollTimeout)
        {
            string information = string.Empty;
            try
            {
                information = this.QueryDeviceInformation(DISCONNECTED_SLAVE_ADDRESS, pollTimeout);
                if (-1 != information.IndexOf("i-B2-Programmer"))
                {
                    return new IDENTEC.PositionMarker.PositionMarker(this, information);
                }
                if (-1 != information.IndexOf("i-MARK"))
                {
                    return new IDENTEC.PositionMarker.PositionMarker(this, information);
                }
                if (-1 != information.IndexOf("MCB"))
                {
                    return new iPortMB(this);
                }
                if (-1 != information.IndexOf("MB"))
                {
                    return new iPortMB(this);
                }
                if ((-1 != information.ToUpper().IndexOf("CARD")) && (-1 != information.ToUpper().IndexOf("R2")))
                {
                    return new iCardCFB(this);
                }
                if (-1 != information.IndexOf("R2"))
                {
                    return new iPortR2(this);
                }
                if (-1 != information.IndexOf("MQ"))
                {
                    return new iPortMQ(this);
                }
                if (-1 != information.IndexOf("T2"))
                {
                    return new iPortMQ(this);
                }
                if (((-1 != information.ToUpper().IndexOf("PORT")) && (-1 != information.ToUpper().IndexOf("RTLS"))) && (-1 != information.ToUpper().IndexOf("350")))
                {
                    return new iPortM350RTLS(this);
                }
                if ((-1 != information.ToUpper().IndexOf("PORT")) && (-1 != information.ToUpper().IndexOf("350")))
                {
                    return new iPortM350(this);
                }
                if ((-1 != information.ToUpper().IndexOf("CARD")) && (-1 != information.IndexOf("350")))
                {
                    return new iCardCF350(this);
                }
                if (-1 != information.ToUpper().IndexOf("I-SAT"))
                {
                    return new iSAT(this);
                }
                if ((-1 != information.ToUpper().IndexOf("CARD")) && (-1 != information.ToUpper().IndexOf("CF")))
                {
                    return new iCardCF(this);
                }
            }
            catch (ReaderTimeoutException)
            {
            }
            catch (TimeoutException)
            {
            }
            if (this.UnknownDeviceDiscovered != null)
            {
                if (string.IsNullOrEmpty(information))
                {
                    log.Debug("Unknown device with empty info");
                    this.UnknownDeviceDiscovered(this, string.Empty);
                }
                else
                {
                    log.Debug("Unknown device " + information);
                    this.UnknownDeviceDiscovered(this, information);
                }
            }
            return null;
        }

        public IBusDevice[] GetProgrammer(int TOms)
        {
            int num = 0;
            this.BroadcastConnectMessage();
            this.BroadcastDisconnectMessage();
            ArrayList list = new ArrayList();
            int tickCount = Environment.TickCount;
            int num3 = 0x11;
            int num4 = 0x3e8;
            while (list.Count < 2)
            {
                int num5 = Environment.TickCount;
                IBusDevice nextBusDevice = this.GetNextBusDevice((num4 * 2) + 100);
                if (nextBusDevice == null)
                {
                    if (!Reader.TimedOut(ref tickCount, TOms))
                    {
                        num++;
                        Thread.Sleep((int) (TOms / 2));
                        continue;
                    }
                    log.Debug("iPort_MB device timed out after " + num.ToString() + " retries");
                    break;
                }
                num4 = Environment.TickCount - num5;
                nextBusDevice.SetBusAddress(num3++);
                nextBusDevice.ConnectSlavePort(true);
                list.Add(nextBusDevice);
            }
            this._Devices = new IBusDevice[list.Count];
            IBusDevice[] deviceArray = new IBusDevice[list.Count];
            for (int i = 0; i < list.Count; i++)
            {
                ((IBusDevice) list[i]).Initialize();
                deviceArray[i] = this._Devices[i] = list[i] as IBusDevice;
            }
            return deviceArray;
        }

        public string QueryDeviceInformation(int address)
        {
            return this.QueryDeviceInformation(address, 0x7d0);
        }

        private string QueryDeviceInformation(int address, int pollTimeout)
        {
            byte[] buffer = new byte[0x10];
            buffer[0] = (byte) address;
            buffer[1] = 0x33;
            IC3ProtocolMessage message = null;
            lock (this._stream.LockObject)
            {
                this._stream.SendMessage(buffer, 2);
                message = this._stream.PollingReadMessage(pollTimeout);
            }
            if (message == null)
            {
                return string.Empty;
            }
            if (message.Buffer.Length < 0x19)
            {
                return string.Empty;
            }
            return Encoding.UTF8.GetString(message.Buffer, 5, 20).Replace("\0", "").TrimEnd(null);
        }

        public void SendSyncMessage(int address)
        {
            byte[] destinationArray = new byte[0x20];
            destinationArray[0] = (byte) address;
            destinationArray[1] = 0x6c;
            uint tickCount = (uint) Environment.TickCount;
            uint num2 = tickCount / 0x3e8;
            tickCount -= num2 * 0x3e8;
            Array.Copy(BitConverter.GetBytes(num2), 0, destinationArray, 2, 4);
            Array.Copy(BitConverter.GetBytes(tickCount), 0, destinationArray, 6, 2);
            this._stream.SendMessage(destinationArray, 8);
            if (BroadcastAddress != address)
            {
                this._stream.ReadMessage(0x1388).CheckResponse((byte) address, 0x6c);
            }
        }

        public IDENTEC.DataStream DataStream
        {
            get
            {
                return this._stream;
            }
            set
            {
                this._stream = value;
            }
        }

        public delegate void UnknownBusDeviceHandler(object sender, string e);
    }
}

