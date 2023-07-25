namespace PureRF
{
    using System;
    using System.IO;
    using System.IO.Ports;
    using System.Runtime.Serialization.Formatters.Binary;

    public class ReceiverBusConnection_Serial : ReceiverBusConnection
    {
        private int mBaudRate;
        private SerialPort mPort;
        private string mPortName;
        private int mTimeout;

        public ReceiverBusConnection_Serial(string SerializedSettings) : base(ReceiverBusConnection.BusType.LOOP_SERIAL)
        {
            MemoryStream serializationStream = new MemoryStream(Convert.FromBase64String(SerializedSettings));
            BinaryFormatter formatter = new BinaryFormatter();
            Settings settings = (Settings) formatter.Deserialize(serializationStream);
            this.mPortName = settings.PortName;
            this.mBaudRate = settings.BaudRate;
            this.mTimeout = settings.Timeout;
            this.mPort = null;
        }

        public ReceiverBusConnection_Serial(string PortName, int BaudRate, int Timeout) : base(ReceiverBusConnection.BusType.LOOP_SERIAL)
        {
            this.mPortName = PortName;
            this.mBaudRate = BaudRate;
            this.mTimeout = Timeout;
            this.mPort = null;
        }

        public override bool Close()
        {
            try
            {
                this.mPort.Close();
            }
            catch
            {
                return false;
            }
            return true;
        }

        public override bool Open()
        {
            try
            {
                this.mPort.Close();
            }
            catch
            {
            }
            this.mPort = new SerialPort(this.mPortName, this.mBaudRate);
            this.SetTimeout(this.mTimeout);
            try
            {
                this.mPort.Open();
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
                    num2 = this.mPort.Read(buffer, offset, count - offset);
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
            graph.PortName = this.mPortName;
            graph.BaudRate = this.mBaudRate;
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

        public override void SetBaudRate(int BaudRate)
        {
            this.mBaudRate = BaudRate;
            try
            {
                this.mPort.BaudRate = BaudRate;
            }
            catch
            {
                this.Open();
            }
        }

        public override void SetTimeout(int Timeout)
        {
            this.mTimeout = Timeout;
            try
            {
                this.mPort.ReadTimeout = Timeout;
            }
            catch (UnauthorizedAccessException)
            {
                this.Open();
            }
        }

        public override bool Write(byte[] buffer, int count)
        {
            try
            {
                if (this.mPort.BytesToRead > 0)
                {
                    byte[] buffer2 = new byte[this.mPort.BytesToRead];
                    this.mPort.Read(buffer2, 0, this.mPort.BytesToRead);
                }
                this.mPort.Write(buffer, 0, count);
            }
            catch
            {
                this.Open();
                try
                {
                    this.mPort.Write(buffer, 0, count);
                }
                catch
                {
                    return false;
                }
            }
            return true;
        }

        public override string Name
        {
            get
            {
                return this.mPort.PortName;
            }
        }

        [Serializable]
        public class Settings
        {
            public int BaudRate;
            public string PortName;
            public int Timeout;
        }
    }
}

