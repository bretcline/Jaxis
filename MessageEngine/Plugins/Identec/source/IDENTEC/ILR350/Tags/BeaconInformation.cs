namespace IDENTEC.ILR350.Tags
{
    using System;

    [Flags]
    public enum BeaconInformation : byte
    {
        DigitalIO = 8,
        Marker = 2,
        MotionSensor = 0x10,
        None = 0,
        Temperature = 1,
        UserData = 4
    }
}

