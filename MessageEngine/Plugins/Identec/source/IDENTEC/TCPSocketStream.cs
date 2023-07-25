using System.IO;

namespace IDENTEC
{
    using System;
    using System.Net;
    using System.Net.Sockets;
    using System.Threading;

    public class TCPSocketStream : DataStream, IDisposable
    {
        private string _host;
        private int _port;
        private Socket _socket;
        public readonly TimeSpan DefaultReadTimeout = new TimeSpan(0, 0, 0, 10, 0);
        public readonly TimeSpan DefaultWriteTimeout = new TimeSpan(0, 0, 0, 10, 0);

        public TCPSocketStream(string host, int port)
        {
            this._host = host;
            this._port = port;
        }

        public TCPSocketStream( Socket _socket )
        {
            this._socket = _socket;
        }

        public override void Close()
        {
            this._socket.Shutdown(SocketShutdown.Both);
            this._socket.Close();
        }

        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                IDisposable disposable = this._socket;
                if (disposable != null)
                {
                    disposable.Dispose();
                }
                this._socket = null;
            }
        }

        public override void Open()
        {
            this._socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            this._socket.Connect(new IPEndPoint(IPAddress.Parse(this._host), this._port));
            this.ReadTimeout = this.DefaultReadTimeout;
            this.WriteTimeout = this.DefaultWriteTimeout;
        }

        public override int PollingRead(byte[] buffer, int offset, int nBytesToRead)
        {
            for (int i = 0; i < 10; i++)
            {
                if (this._socket.Available > 0)
                {
                    return this._socket.Receive(buffer, offset, nBytesToRead, SocketFlags.None);
                }
                Thread.Sleep(0);
            }
            return 0;
        }

        public override void PurgeRX()
        {
            int available = this._socket.Available;
            if (available > 0)
            {
                byte[] buffer = new byte[available];
                this._socket.Receive(buffer, 0, available, SocketFlags.None);
            }
        }

        public override void PurgeTX()
        {
        }

        public override int Read(byte[] buffer, int offset, int nBytesToRead)
        {
            int num = 0;
            do
            {
                int available = this._socket.Available;
                if (available == 0)
                {
                    Thread.Sleep(5);
                    return num;
                }
                if (available > (nBytesToRead - num))
                {
                    available = nBytesToRead - num;
                }
                int num3 = this._socket.Receive(buffer, offset + num, available, SocketFlags.None);
                num += num3;
            }
            while (num < nBytesToRead);
            return num;
        }

        public override int Write(byte[] buffer, int offset, int nBytesToWrite)
        {
            return this._socket.Send(buffer, offset, nBytesToWrite, SocketFlags.None);
        }

        public override bool IsOpen
        {
            get
            {
                return ((this._socket != null) && this._socket.Connected);
            }
        }

        public override TimeSpan ReadTimeout
        {
            get
            {
                return new TimeSpan(0, 0, 0, 0, (int) this._socket.GetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReceiveTimeout));
            }
            set
            {
            }
        }

        public override TimeSpan WriteTimeout
        {
            get
            {
                return new TimeSpan(0, 0, 0, 0, (int) this._socket.GetSocketOption(SocketOptionLevel.Socket, SocketOptionName.SendTimeout));
            }
            set
            {
            }
        }
    }
}

