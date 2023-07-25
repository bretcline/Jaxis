namespace IDENTEC
{
    using IDENTEC.Readers;
    using NLog;
    using System;
    using System.Text;

    public abstract class DataStream
    {
        private IC3ProtocolMessage _nextMessage;
        private bool _PollingMode;
        internal object LockObject = new object();
        protected static Logger log = LogManager.GetLogger("DataStream");

        protected DataStream()
        {
        }

        public virtual void Close()
        {
            throw new NotImplementedException("The method or operation is not implemented.");
        }

        public virtual void Open()
        {
            throw new NotImplementedException("The method or operation is not implemented.");
        }

        public virtual int PollingRead(byte[] buffer, int offset, int nBytesToRead)
        {
            throw new NotImplementedException("The method or operation is not implemented.");
        }

        internal IC3ProtocolMessage PollingReadMessage(int timeout)
        {
            IC3ProtocolMessage message = null;
            try
            {
                this._PollingMode = true;
                message = this.ReadMessage(timeout);
            }
            finally
            {
                this._PollingMode = false;
            }
            return message;
        }

        public virtual void PurgeRX()
        {
        }

        public virtual void PurgeTX()
        {
        }

        public virtual int Read(byte[] buffer, int offset, int nBytesToRead)
        {
            throw new NotImplementedException("The method or operation is not implemented.");
        }

        internal virtual IC3ProtocolMessage ReadMessage(int timeout)
        {
            IC3ProtocolMessage message = null;
            if (this._nextMessage != null)
            {
                message = this._nextMessage;
            }
            else
            {
                message = new IC3ProtocolMessage();
            }
            if (message.BufferLength > 0)
            {
                this._nextMessage = message.ParseMessage();
                if (message.FullMessage)
                {
                    if (log.IsDebugEnabled)
                    {
                        StringBuilder builder = new StringBuilder("RX:");
                        for (int i = 0; i < message.MessageLength; i++)
                        {
                            builder.Append(message[i].ToString("X2") + ":");
                        }
                        log.Debug(builder.ToString());
                    }
                    message.UnpackAndCheckCRC();
                    return message;
                }
            }
            int tickCount = Environment.TickCount;
            byte[] buffer = new byte[0x400];
            do
            {
                if (Reader.TimedOut(ref tickCount, timeout))
                {
                    throw new ReaderTimeoutException(string.Format("The reader did not respond within the specified timeout period of {0}ms", timeout));
                }
                if (message.Bytes.Count >= 0x10000)
                {
                    throw new InvalidOperationException("The size of the message is too large to be valid");
                }
                if (!message.FullMessage)
                {
                    int length = 0;
                    if (this._PollingMode)
                    {
                        length = this.PollingRead(buffer, 0, buffer.Length);
                    }
                    else
                    {
                        length = this.Read(buffer, 0, buffer.Length);
                    }
                    this._nextMessage = message.ParseMessage(buffer, length);
                }
            }
            while (!message.FullMessage);
            if (log.IsDebugEnabled)
            {
                StringBuilder builder2 = new StringBuilder("RX:");
                for (int j = 0; j < message.MessageLength; j++)
                {
                    builder2.Append(message[j].ToString("X2") + ":");
                }
                log.Debug(builder2.ToString());
            }
            message.UnpackAndCheckCRC();
            return message;
        }

        internal virtual int SendMessage(byte[] buffer, int length)
        {
            this._nextMessage = null;
            this.PurgeTX();
            this.PurgeRX();
            byte[] buffer2 = ISolProtocolFramer.PackageMessage(buffer, ref length);
            if (log.IsDebugEnabled)
            {
                string message = "TX:";
                for (int i = 0; i < buffer2.Length; i++)
                {
                    message = message + buffer2[i].ToString("X2") + ":";
                }
                log.Debug(message);
            }
            return this.Write(buffer2, 0, buffer2.Length);
        }

        public virtual int Write(byte[] buffer, int offset, int nBytesToWrite)
        {
            throw new NotImplementedException("The method or operation is not implemented.");
        }

        public virtual bool IsOpen
        {
            get
            {
                return false;
            }
        }

        public virtual TimeSpan ReadTimeout
        {
            get
            {
                return new TimeSpan();
            }
            set
            {
            }
        }

        public virtual TimeSpan WriteTimeout
        {
            get
            {
                return new TimeSpan();
            }
            set
            {
            }
        }
    }
}

