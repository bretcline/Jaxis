namespace IDENTEC.Tags
{
    using System;

    public class iD2Tag : ResponseTag
    {
        internal BatteryStatus m_BattStatus;
        internal static readonly int VIRTUAL_EEPROM_ADDRESS_BLINK = 8;

        public iD2Tag()
        {
            this.m_BattStatus = BatteryStatus.Indeterminate;
        }

        public iD2Tag(iD2Tag t) : base(t)
        {
            this.m_BattStatus = BatteryStatus.Indeterminate;
            this.m_BattStatus = t.m_BattStatus;
        }

        [CLSCompliant(false)]
        public iD2Tag(uint id) : base(id)
        {
            this.m_BattStatus = BatteryStatus.Indeterminate;
        }

        [CLSCompliant(false)]
        public iD2Tag(uint id, DateTime dt, int signal) : base(id, dt, signal)
        {
            this.m_BattStatus = BatteryStatus.Indeterminate;
        }

        internal static void CheckMultiBlinkParameters(ref TimeSpan tsLedOn, ref TimeSpan tsLedOff, int blinkCount)
        {
            if (blinkCount > 0xff)
            {
                throw new ArgumentOutOfRangeException("blinkCount cannot exceed 255.");
            }
            if (tsLedOn.TotalSeconds > 2.55)
            {
                throw new ArgumentOutOfRangeException("The maximum LED on time is 2.55 seconds.");
            }
            if (tsLedOff.TotalSeconds > 2.55)
            {
                throw new ArgumentOutOfRangeException("The maximum LED off time is 2.55 seconds.");
            }
        }

        internal static byte[] CreateMultiBlinkBuffer(ref TimeSpan tsLedOn, ref TimeSpan tsLedOff, int blinkCount)
        {
            CheckMultiBlinkParameters(ref tsLedOn, ref tsLedOff, blinkCount);
            return new byte[] { ((byte) blinkCount), ((byte) (tsLedOn.TotalMilliseconds / 10.0)), ((byte) (tsLedOff.TotalMilliseconds / 10.0)) };
        }

        internal override int WaitForResponse(bool bWakeUp)
        {
            return 5;
        }

        public BatteryStatus Battery
        {
            get
            {
                return this.m_BattStatus;
            }
        }

        public override int DataCapacity
        {
            get
            {
                return 0x40;
            }
        }

        public override int MinDataWriteAddress
        {
            get
            {
                return 8;
            }
        }

        public enum BatteryStatus
        {
            Good = 1,
            Indeterminate = 0xff,
            Poor = 0
        }
    }
}

