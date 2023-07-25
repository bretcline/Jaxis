namespace IDENTEC.ILR350.Readers
{
    using IDENTEC;
    using IDENTEC.Readers;
    using System;

    public class iCardCF350 : ILR350Reader
    {
        public iCardCF350()
        {
            base._iBus = new iBusAdapter(new SerialPortStream(CFReaderSearch.FindReaderComPort()));
            base._iBus.DataStream.Open();
            base._byAddress = 0;
        }

        public iCardCF350(DataStream stream)
        {
            base._iBus = new iBusAdapter(stream);
            base._byAddress = 0;
        }

        public iCardCF350(iBusAdapter bus)
        {
            base._iBus = bus;
            base._byAddress = 0;
        }

        public void Disconnect()
        {
            base._iBus.DataStream.Close();
        }

        public override void Initialize()
        {
            base._iBus.BroadcastDisconnectMessage();
            base.Information = base._iBus.QueryDeviceInformation(base.Address);
            this.ReadInfo();
            base.LimitNumberOfTagsDuringRequest(0);
        }

        public override void ReadInfo()
        {
            base.ReadInfo();
            base.SupportedFrequencies = Frequency.Every;
        }

        public bool Connected
        {
            get
            {
                if (base._iBus == null)
                {
                    return false;
                }
                return base._iBus.DataStream.IsOpen;
            }
        }
    }
}

