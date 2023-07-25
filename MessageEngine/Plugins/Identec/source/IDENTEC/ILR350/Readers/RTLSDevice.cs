namespace IDENTEC.ILR350.Readers
{
    using System;
    using System.Runtime.InteropServices;

    public interface RTLSDevice
    {
        int GetApplicationID();
        int GetRangingTXPower();
        bool GetRTLSrssiMargin();
        int GetRTLSSignalFilterLevel();
        int ReadData24GHz(byte ManufID, byte TypeID, long ID, int address, int bytesToRead, out byte[] data, out int RSSI);
        bool SetApplicationID(int ID);
        bool SetRangingTXPower(int Power);
        bool SetRTLSrssiMargin(bool on);
        bool SetRTLSSignalFilterLevel(int level);
        int WriteData24GHz(byte ManufID, byte TypeID, long ID, int address, byte[] data, int bytesToWrite, out int nBytesWritten, out int RSSI);

        int SatelliteID { get; }
    }
}

