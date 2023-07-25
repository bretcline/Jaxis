namespace IDENTEC.Tags
{
    using IDENTEC.Tags.Logging;
    using System;

    public class iQTag : ResponseTag
    {
        internal LoggingState m_LoggingState;
        internal Model m_Model;
        internal int m_nPercentBatteryConsumed;
        internal RangeState m_range;
        internal const int nABS_HOUR_COUNTER = 60;
        internal const int nHOUR_COUNTER = 0x90;
        internal const int nLOG_STARTTIMEPOSITION = 0x17af;
        internal const int nLogIntervalAddress = 0x80;
        internal const int nLogSampleSizeAddr = 0x17b8;
        internal const int nLogStartAddress = 0x17ad;
        internal const int nLogTimeSize = 4;
        internal const int nLogTimeSynchAddr = 0x94;
        internal const int nMarkerInfoSize = 10;
        internal const int nMarkerInfoVirtualEEPROM = 0x90;
        internal const int nTAGRAM_LOGPOINTERPOS = 0x7d;
        internal const int nTagRamLogInterval = 0x7b;
        internal const int nTagRamTemperatureAddress = 0x77;
        internal const int nTEMPERATURE_CALIBRATION_DATA_ADDRESS = 0x17bf;
        internal const int nTEMPLOG_DATALOCATION = 0x1800;
        internal const int nTLOG_ENDTIMEPOSITION = 0x17b3;
        internal const int nTLOG_EXTREMEPOSITION = 0x17a0;
        internal const int nTLOG_STARTFLAG = 0x1000;
        internal const int nTLOG_WRAPFLAGPOSITION = 0x17b7;
        internal const byte TAG_DISABLE_BROADCASTING = 0x20;
        internal const byte TAG_DISABLE_HISENS = 0x40;
        internal const byte TAG_DISABLE_LOGGING = 0x10;
        internal const byte TAG_ENABLE_BROADCASTING = 2;
        internal const byte TAG_ENABLE_HISENS = 4;
        internal const byte TAG_ENABLE_LOGGING = 1;

        public iQTag() : this((uint) 0)
        {
        }

        public iQTag(iQTag t) : base(t)
        {
            this.m_range = RangeState.Indeterminate;
            this.m_LoggingState = LoggingState.Indeterminate;
            this.m_Model = Model.Indeterminate;
            this.m_LoggingState = t.m_LoggingState;
            this.m_Model = t.m_Model;
            this.m_nPercentBatteryConsumed = t.m_nPercentBatteryConsumed;
            this.m_range = t.m_range;
        }

        [CLSCompliant(false)]
        public iQTag(uint id) : base(id)
        {
            this.m_range = RangeState.Indeterminate;
            this.m_LoggingState = LoggingState.Indeterminate;
            this.m_Model = Model.Indeterminate;
            this.m_LoggingState = LoggingState.Indeterminate;
            this.m_Model = Model.Indeterminate;
            this.m_range = RangeState.Indeterminate;
        }

        internal iQTag(uint id, DateTime dt, int signal) : base(id, dt, signal)
        {
            this.m_range = RangeState.Indeterminate;
            this.m_LoggingState = LoggingState.Indeterminate;
            this.m_Model = Model.Indeterminate;
            this.m_LoggingState = LoggingState.Indeterminate;
            this.m_Model = Model.Indeterminate;
            this.m_range = RangeState.Indeterminate;
        }

        internal static void IsTemperatureTag(iQTag tag)
        {
            if (tag.LoggerInstalled == LoggerInstalledState.Unavailable)
            {
                throw new TagHasNoLoggerException(tag.Label + " is not capable of logging.");
            }
        }

        internal void SetModelTypeFromTagProtocol(byte byRaw)
        {
            int num = byRaw >> 4;
            if (this.ReportsBatteryVoltage)
            {
                switch (num)
                {
                    case 8:
                        this.m_Model = Model.IQ8T;
                        goto Label_00C4;

                    case 9:
                        this.m_Model = Model.IQ8T;
                        goto Label_00C4;

                    case 10:
                        this.m_Model = Model.IQ32T;
                        goto Label_00C4;

                    case 11:
                        this.m_Model = Model.IQ32T;
                        goto Label_00C4;
                }
                this.m_Model = Model.IQ8;
            }
            else
            {
                switch (num)
                {
                    case 1:
                        this.m_Model = Model.IQ32Elpro;
                        goto Label_00C4;

                    case 3:
                        this.m_Model = Model.IQ8C;
                        goto Label_00C4;

                    case 6:
                        this.m_Model = Model.IQ8N;
                        goto Label_00C4;

                    case 8:
                        this.m_Model = Model.IQ8S;
                        goto Label_00C4;

                    case 10:
                        this.m_Model = Model.IQ32S;
                        goto Label_00C4;
                }
                this.m_Model = Model.IQ8N;
            }
        Label_00C4:
            if (this.LoggerInstalled == LoggerInstalledState.Unavailable)
            {
                this.m_LoggingState = LoggingState.Indeterminate;
            }
            else if ((byRaw & 1) != 0)
            {
                this.m_LoggingState = LoggingState.Off;
            }
            else
            {
                this.m_LoggingState = LoggingState.On;
            }
            if ((byRaw & 4) != 0)
            {
                this.m_range = RangeState.NormalRange;
            }
            else
            {
                this.m_range = RangeState.ExtendedRange;
            }
        }

        internal override int WaitForResponse(bool bWakeUp)
        {
            if (bWakeUp)
            {
                return 50;
            }
            return 30;
        }

        public int BatteryPercentConsumed
        {
            get
            {
                if (!this.ReportsBatteryPercentConsumed)
                {
                    throw new NotSupportedException("This tag does not report battery usage consumed");
                }
                return this.m_nPercentBatteryConsumed;
            }
        }

        public override int DataCapacity
        {
            get
            {
                if (((Model.IQ32 != this.ModelType) && (Model.IQ32Elpro != this.ModelType)) && ((Model.IQ32S != this.ModelType) && (Model.IQ32T != this.ModelType)))
                {
                    return 0x2000;
                }
                return 0x8000;
            }
        }

        public LoggerInstalledState LoggerInstalled
        {
            get
            {
                switch (this.m_Model)
                {
                    case Model.IQ8T:
                        return LoggerInstalledState.Available;

                    case Model.IQ32T:
                        return LoggerInstalledState.Available;

                    case Model.IQ8S:
                        return LoggerInstalledState.Available;

                    case Model.IQ32S:
                        return LoggerInstalledState.Available;

                    case Model.IQ32Elpro:
                        return LoggerInstalledState.Available;

                    case Model.Indeterminate:
                        return LoggerInstalledState.Indeterminate;
                }
                return LoggerInstalledState.Unavailable;
            }
        }

        public LoggingState Logging
        {
            get
            {
                return this.m_LoggingState;
            }
        }

        public override int MinDataWriteAddress
        {
            get
            {
                return 0x84;
            }
        }

        public Model ModelType
        {
            get
            {
                return this.m_Model;
            }
        }

        public RangeState Range
        {
            get
            {
                return this.m_range;
            }
        }

        public bool ReportsBatteryPercentConsumed
        {
            get
            {
                return !this.ReportsBatteryVoltage;
            }
        }

        public bool ReportsBatteryVoltage
        {
            get
            {
                if (base.Number >= 0xbebc200)
                {
                    return false;
                }
                return true;
            }
        }

        public enum LoggerInstalledState
        {
            Available = 0,
            Indeterminate = -1,
            Unavailable = 1
        }

        public enum LoggingState
        {
            Indeterminate = -1,
            Off = 1,
            On = 0
        }

        [Flags]
        public enum Model
        {
            Indeterminate = 15,
            IQ32 = 3,
            IQ32Elpro = 9,
            IQ32S = 7,
            IQ32T = 4,
            IQ8 = 1,
            IQ8C = 8,
            IQ8N = 5,
            IQ8S = 6,
            IQ8T = 2
        }

        public enum RangeState
        {
            ExtendedRange = 0,
            Indeterminate = -1,
            NormalRange = 1
        }
    }
}

