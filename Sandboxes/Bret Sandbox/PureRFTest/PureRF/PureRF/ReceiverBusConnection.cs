namespace PureRF
{
    using System;

    public abstract class ReceiverBusConnection
    {
        private BusType mBusType;

        public ReceiverBusConnection(BusType busType)
        {
            this.mBusType = busType;
        }

        public abstract bool Close();
        public abstract bool Open();
        public abstract ReceiverRetVal Read(byte[] buffer, int count);
        public abstract string Serialize();
        public virtual void SetBaudRate(int BaudRate)
        {
        }

        public abstract void SetTimeout(int Timeout);
        public abstract bool Write(byte[] buffer, int count);

        public BusType ConnectionBusType
        {
            get
            {
                return this.mBusType;
            }
        }

        public abstract string Name { get; }

        public enum BusType
        {
            LOOP_SERIAL,
            LOOP_IP
        }
    }
}

