namespace IDENTEC.ILR350.Tags
{
    using System;

    [Flags]
    public enum AlarmFlag : byte
    {
        MotionSensor = 2,
        None = 0,
        PushButton = 4,
        Temperature = 1
    }
}

