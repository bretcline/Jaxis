namespace IDENTEC
{
    using System;
    using System.IO.Ports;
    using System.Threading;

    public class SerialPortStream : DataStream, IDisposable
    {
        private bool _bPurgeComOnSend;
        private SerialPort _port;
        public readonly TimeSpan DefaultReadTimeout;
        public readonly TimeSpan DefaultWriteTimeout;

        public SerialPortStream(int port) : this(port, 0x1c200)
        {
        }

        public SerialPortStream(string portName)
        {
            this._bPurgeComOnSend = true;
            this.DefaultWriteTimeout = new TimeSpan(0, 0, 0, 1, 0);
            this.DefaultReadTimeout = new TimeSpan(0, 0, 0, 1, 0);
            this.PrivateCtor(portName, 0x1c200);
        }

        public SerialPortStream(int port, bool setDTR)
        {
            this._bPurgeComOnSend = true;
            this.DefaultWriteTimeout = new TimeSpan(0, 0, 0, 1, 0);
            this.DefaultReadTimeout = new TimeSpan(0, 0, 0, 1, 0);
            this.PrivateCtor("COM" + port.ToString(), 0x1c200);
            this._port.DtrEnable = setDTR;
        }

        public SerialPortStream(int port, int baudRate)
        {
            this._bPurgeComOnSend = true;
            this.DefaultWriteTimeout = new TimeSpan(0, 0, 0, 1, 0);
            this.DefaultReadTimeout = new TimeSpan(0, 0, 0, 1, 0);
            this.PrivateCtor("COM" + port.ToString(), baudRate);
        }

        public SerialPortStream(string portName, bool setDTR)
        {
            this._bPurgeComOnSend = true;
            this.DefaultWriteTimeout = new TimeSpan(0, 0, 0, 1, 0);
            this.DefaultReadTimeout = new TimeSpan(0, 0, 0, 1, 0);
            this.PrivateCtor(portName, 0x1c200);
            this._port.DtrEnable = setDTR;
        }

        public SerialPortStream(string portName, int baudRate)
        {
            this._bPurgeComOnSend = true;
            this.DefaultWriteTimeout = new TimeSpan(0, 0, 0, 1, 0);
            this.DefaultReadTimeout = new TimeSpan(0, 0, 0, 1, 0);
            this.PrivateCtor(portName, baudRate);
        }

        public override void Close()
        {
            this._port.Close();
        }

        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing && (this._port != null))
            {
                if (this._port.IsOpen)
                {
                    this._port.Close();
                }
                IDisposable disposable = this._port;
                if (disposable != null)
                {
                    disposable.Dispose();
                }
                this._port = null;
            }
        }

        private void OnError(string description)
        {
        }

        public override void Open()
        {
            this._port.Open();
        }

        public override int PollingRead(byte[] buffer, int offset, int nBytesToRead)
        {
            return this.Read(buffer, offset, nBytesToRead);
        }

        private void PrivateCtor(string portName, int baudRate)
        {
            this._port = new SerialPort(portName, baudRate, System.IO.Ports.Parity.None, 8, System.IO.Ports.StopBits.One);
            this._port.DtrEnable = false;
            this._port.Handshake = System.IO.Ports.Handshake.None;
            this._port.RtsEnable = false;
            this._port.DataBits = 8;
            this._port.Parity = System.IO.Ports.Parity.None;
            this._port.StopBits = System.IO.Ports.StopBits.One;
            this._port.RtsEnable = false;
            this._port.ReadBufferSize = 0x800;
            this._port.WriteBufferSize = 0x800;
            this._port.ReadTimeout = (int) this.DefaultReadTimeout.TotalMilliseconds;
            this._port.WriteTimeout = (int) this.DefaultWriteTimeout.TotalMilliseconds;
            this._port.ReceivedBytesThreshold = 10;
        }

        public override void PurgeRX()
        {
            try
            {
                if (this._port.BytesToRead != 0)
                {
                    this._port.DiscardInBuffer();
                }
            }
            catch (Exception)
            {
            }
        }

        public override void PurgeTX()
        {
            try
            {
                if (this._port.BytesToWrite != 0)
                {
                    this._port.DiscardOutBuffer();
                }
            }
            catch (Exception)
            {
            }
        }

        public override int Read(byte[] buffer, int offset, int nBytesToRead)
        {
            int num = offset;
            int num2 = 0;
            int bytesToRead = 0;
            int count = 0;
            try
            {
                while (num < nBytesToRead)
                {
                    bytesToRead = this._port.BytesToRead;
                    count = bytesToRead;
                    if (bytesToRead == 0)
                    {
                        if ((num != 0) && (buffer[(offset + num) - 1] == 4))
                        {
                            return num;
                        }
                        count = nBytesToRead - num;
                    }
                    else
                    {
                        if (count > (nBytesToRead - num))
                        {
                            count = nBytesToRead - num;
                        }
                        if (num > 100)
                        {
                            count = nBytesToRead - num;
                        }
                    }
                    num2 = 0;
                    num2 = this._port.Read(buffer, offset + num, count);
                    num += num2;
                }
            }
            catch (TimeoutException exception)
            {
                if ((bytesToRead != 0) && DataStream.log.IsDebugEnabled)
                {
                    DataStream.log.Debug("Read timeout byte available: " + bytesToRead.ToString() + " requested: " + count.ToString(), exception);
                }
                return num;
            }
            catch (Exception exception2)
            {
                if (DataStream.log.IsDebugEnabled)
                {
                    DataStream.log.Debug("Read exception: ", exception2);
                }
                throw;
            }
            return num;
        }

        public override int Write(byte[] buffer, int offset, int nBytesToWrite)
        {
            if (this._bPurgeComOnSend)
            {
                int num = 100;
                do
                {
                    this._port.DiscardInBuffer();
                    Thread.Sleep(1);
                    num--;
                }
                while ((num != 0) && (this._port.BytesToRead != 0));
                this._port.DiscardOutBuffer();
            }
            byte[] destinationArray = buffer;
            if (nBytesToWrite != buffer.Length)
            {
                destinationArray = new byte[nBytesToWrite];
                Array.Copy(buffer, offset, destinationArray, 0, nBytesToWrite);
            }
            this._port.Write(buffer, offset, nBytesToWrite);
            return nBytesToWrite;
        }

        public bool ClearReceiveBufferOnWrite
        {
            get
            {
                return this._bPurgeComOnSend;
            }
            set
            {
                this._bPurgeComOnSend = value;
            }
        }

        public override bool IsOpen
        {
            get
            {
                return this._port.IsOpen;
            }
        }

        public override TimeSpan ReadTimeout
        {
            get
            {
                return new TimeSpan(0, 0, 0, 0, this._port.ReadTimeout);
            }
            set
            {
                this._port.ReadTimeout = (int) value.TotalMilliseconds;
            }
        }

        public override TimeSpan WriteTimeout
        {
            get
            {
                return new TimeSpan(0, 0, 0, 0, this._port.WriteTimeout);
            }
            set
            {
                this._port.WriteTimeout = (int) value.TotalMilliseconds;
            }
        }
    }
}

