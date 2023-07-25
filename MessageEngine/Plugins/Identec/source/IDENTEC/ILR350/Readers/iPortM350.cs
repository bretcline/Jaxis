namespace IDENTEC.ILR350.Readers
{
    using IDENTEC;
    using System;
    using System.Runtime.InteropServices;

    public class iPortM350 : ILR350Reader
    {
        public iPortM350(DataStream stream)
        {
            base._iBus = new iBusAdapter(stream);
        }

        public iPortM350(iBusAdapter bus)
        {
            base._iBus = bus;
        }

        public iPortM350(iPortM350 reader) : base(reader)
        {
            base._iBus = reader._iBus;
        }

        public void Disconnect()
        {
            base._iBus.DataStream.Close();
        }

        public override void Initialize()
        {
            base.Information = base._iBus.QueryDeviceInformation(base.Address);
            this.ReadInfo();
            base.LimitNumberOfTagsDuringRequest(0);
        }

        protected int ReadEEPROM(int address, byte[] buffer, int bytesToRead)
        {
            throw new NotImplementedException("The method or operation is not implemented.");
        }

        protected int WriteEEPROM(int address, byte[] buffer, int bytesToWrite)
        {
            throw new NotImplementedException("The method or operation is not implemented.");
        }

        [StructLayout(LayoutKind.Sequential), CLSCompliant(false)]
        public struct EEPROMCalibration
        {
            public byte compatibility;
            public byte reserved;
            public short FrequencyOffset;
            public sbyte TXOffset;
            public sbyte RSSIOffset;
            internal ushort CRC;
        }

        [StructLayout(LayoutKind.Sequential), CLSCompliant(false)]
        public struct EEPROMInfo
        {
            public byte compatibility;
            public byte[] SN;
            public byte FrequencyArea;
            public byte ReaderManufacturer;
            public byte ReaderType;
            internal ushort CRC;
        }

        [StructLayout(LayoutKind.Sequential), CLSCompliant(false)]
        public struct EEPROMRTLS
        {
            public byte Compatibility;
            public byte[] ManufacturerID;
            public byte[] ReaderID;
            public byte Modulation;
            public byte[] Reserved;
            internal ushort CRC;
        }
    }
}

