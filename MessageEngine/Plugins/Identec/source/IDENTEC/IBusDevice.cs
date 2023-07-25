namespace IDENTEC
{
    using System;

    public interface IBusDevice
    {
        void ConnectSlavePort(bool enable);
        byte GetBusAddress();
        void Initialize();
        void ResetToFactoryDefault();
        void SetBusAddress(int address);

        int Address { get; }

        IDENTEC.DataStream DataStream { get; }

        string FirmwareVersion { get; }

        string Information { get; }

        int MajorVersion { get; }

        int MinorVersion { get; }

        string SerialNumber { get; }
    }
}

