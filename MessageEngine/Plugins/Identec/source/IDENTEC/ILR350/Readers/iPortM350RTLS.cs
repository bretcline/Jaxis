namespace IDENTEC.ILR350.Readers
{
    using IDENTEC;
    using System;
    using System.Runtime.InteropServices;

    public class iPortM350RTLS : iPortM350, RTLSDevice
    {
        private iSAT iSat;

        public iPortM350RTLS(iBusAdapter bus) : base(bus)
        {
            this.iSat = new iSAT(this);
        }

        public int GetApplicationID()
        {
            return this.iSat.GetApplicationID();
        }

        public int GetRangingTXPower()
        {
            return this.iSat.GetRangingTXPower();
        }

        public bool GetRTLSrssiMargin()
        {
            return this.iSat.GetRTLSrssiMargin();
        }

        public int GetRTLSSignalFilterLevel()
        {
            return this.iSat.GetRTLSSignalFilterLevel();
        }

        public virtual int ReadData24GHz(byte ManufID, byte TypeID, long ID, int address, int bytesToRead, out byte[] data, out int RSSI)
        {
            return this.iSat.ReadData24GHz(ManufID, TypeID, ID, address, bytesToRead, out data, out RSSI);
        }

        public bool SetApplicationID(int ID)
        {
            return this.iSat.SetApplicationID(ID);
        }

        public bool SetRangingTXPower(int Power)
        {
            return this.iSat.SetRangingTXPower(Power);
        }

        public bool SetRTLSrssiMargin(bool on)
        {
            return this.iSat.SetRTLSrssiMargin(on);
        }

        public bool SetRTLSSignalFilterLevel(int level)
        {
            return this.iSat.SetRTLSSignalFilterLevel(level);
        }

        public virtual int WriteData24GHz(byte ManufID, byte TypeID, long ID, int address, byte[] data, int bytesToWrite, out int nBytesWritten, out int RSSI)
        {
            return this.iSat.WriteData24GHz(ManufID, TypeID, ID, address, data, bytesToWrite, out nBytesWritten, out RSSI);
        }

        public int SatelliteID
        {
            get
            {
                return this.iSat.SatelliteID;
            }
        }
    }
}

