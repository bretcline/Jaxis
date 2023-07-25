namespace PureRF
{
    using System;
    using System.IO;
    using System.Net.Sockets;
    using System.Runtime.Serialization.Formatters.Binary;

    public class ReceiverBusConnection_IP : ReceiverBusConnection
    {
        private string mHost;
        private int mPort;
        private Socket mSocket;
        private int mTimeout;

        public ReceiverBusConnection_IP(string SerializedSettings) : base(ReceiverBusConnection.BusType.LOOP_IP)
        {
            MemoryStream serializationStream = new MemoryStream(Convert.FromBase64String(SerializedSettings));
            BinaryFormatter formatter = new BinaryFormatter();
            Settings settings = (Settings) formatter.Deserialize(serializationStream);
            this.mHost = settings.Host;
            this.mPort = settings.Port;
            this.SetTimeout(settings.Timeout);
        }

        public ReceiverBusConnection_IP(string Host, int Port, int Timeout) : base(ReceiverBusConnection.BusType.LOOP_IP)
        {
            this.mHost = Host;
            this.mPort = Port;
            this.SetTimeout(Timeout);
        }

        public override bool Close()
        {
            try
            {
                this.mSocket.Close();
            }
            catch
            {
                return false;
            }
            return true;
        }

        public override bool Open()
        {
            this.Close();
            this.mSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            try
            {
                this.mSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                this.mSocket.ReceiveTimeout = this.mTimeout;
                this.mSocket.Connect(this.mHost, this.mPort);
            }
            catch
            {
                return false;
            }
            return true;
        }

        public override ReceiverRetVal Read(byte[] buffer, int count)
        {
            int offset = 0;
            int num2 = 0;
            while (offset < count)
            {
                try
                {
                    this.mSocket.Blocking = true;
                    num2 = this.mSocket.Receive(buffer, offset, count - offset, SocketFlags.None);
                    if (num2 == 0)
                    {
                        return ReceiverRetVal.LOOP_COMM_ERROR;
                    }
                }
                catch (TimeoutException)
                {
                    return ReceiverRetVal.LOOP_TIMEOUT;
                }
                catch
                {
                    return ReceiverRetVal.LOOP_COMM_ERROR;
                }
                offset += num2;
            }
            return ReceiverRetVal.SUCCESS;
        }

        public override string Serialize()
        {
            Settings graph = new Settings();
            graph.Host = this.mHost;
            graph.Port = this.mPort;
            graph.Timeout = this.mTimeout;
            try
            {
                MemoryStream serializationStream = new MemoryStream();
                new BinaryFormatter().Serialize(serializationStream, graph);
                return Convert.ToBase64String(serializationStream.ToArray());
            }
            catch
            {
                return "";
            }
        }

        public override void SetTimeout(int Timeout)
        {
            this.mTimeout = Timeout;
            if (this.mSocket != null)
            {
                this.mSocket.ReceiveTimeout = this.mTimeout;
            }
        }

        public override bool Write(byte[] buffer, int count)
        {
            if (this.WriteAll(buffer, count))
            {
                return true;
            }
            if (!this.Open())
            {
                return false;
            }
            return this.WriteAll(buffer, count);
        }

        public bool WriteAll(byte[] buffer, int count)
        {
            int offset = 0;
            int num2 = 0;
            while (offset < count)
            {
                try
                {
                    num2 = this.mSocket.Send(buffer, offset, count - offset, SocketFlags.None);
                }
                catch
                {
                    return false;
                }
                offset += num2;
            }
            return true;
        }

        public override string Name
        {
            get
            {
                return string.Format("IP_{0}:{1}", this.mHost, this.mPort);
            }
        }

        [Serializable]
        public class Settings
        {
            public string Host;
            public int Port;
            public int Timeout;
        }
    }
}

