namespace PureRF
{
    using System;

    public class Tag_Settings
    {
        private int _ActavtorOFF;
        private int _ActavtorON;
        private int _ActivatorDuration;
        private int _ActivatorSlotMask;
        private int _ActivatorTxInterval;
        private TagActivatorFlags _ActvatorFlags;
        private int _Alarm_Period;
        private int _Alarm_Timer_Multiplier;
        private int _Battery_Average_value;
        private int _Firmware_Version;
        private Motion_Sensor_Activator_States _Motion_Sensor_Activator_State;
        private int _Motion_Threshold;
        private int _Periodic_Timer_Multiplier;
        private int _Tag_ID;
        private int _Tag_Site_Code;
        private byte[] _Tag_SN;
        private Tag_States _Tag_State;
        private Tamper_Panic_Activator_States _Tamper_Panic_Activator_State;
        private Tamper_Type_States _Tamper_Type_State;
        private Transmission_Types _Transmission_Type;
        private TransmissionBaudrates _TransmissionBaudrate;
        public const int Dont_modify = 0x7fffffff;
        private const double UnitVolt = 0.001075;

        public Tag_Settings()
        {
            this._Tag_State = Tag_States.Do_not_modify;
            this._Periodic_Timer_Multiplier = 0x7fffffff;
            this._Alarm_Timer_Multiplier = 0x7fffffff;
            this._Alarm_Period = 0x7fffffff;
            this._Tamper_Panic_Activator_State = Tamper_Panic_Activator_States.Do_not_modify;
            this._Motion_Sensor_Activator_State = Motion_Sensor_Activator_States.Do_not_modify;
            this._Tamper_Type_State = Tamper_Type_States.Do_not_modify;
            this._Motion_Threshold = 0x7fffffff;
            this._Transmission_Type = Transmission_Types.Do_not_modify;
            this._Tag_SN = new byte[6];
            this._ActivatorSlotMask = 0x7fffffff;
            this._ActivatorTxInterval = 0x7fffffff;
            this._ActivatorDuration = 0x7fffffff;
            this._ActavtorON = 0x7fffffff;
            this._ActavtorOFF = 0x7fffffff;
            this._TransmissionBaudrate = TransmissionBaudrates.Do_not_modify;
        }

        public Tag_Settings(byte[] TagBinaryData)
        {
            this._Tag_State = Tag_States.Do_not_modify;
            this._Periodic_Timer_Multiplier = 0x7fffffff;
            this._Alarm_Timer_Multiplier = 0x7fffffff;
            this._Alarm_Period = 0x7fffffff;
            this._Tamper_Panic_Activator_State = Tamper_Panic_Activator_States.Do_not_modify;
            this._Motion_Sensor_Activator_State = Motion_Sensor_Activator_States.Do_not_modify;
            this._Tamper_Type_State = Tamper_Type_States.Do_not_modify;
            this._Motion_Threshold = 0x7fffffff;
            this._Transmission_Type = Transmission_Types.Do_not_modify;
            this._Tag_SN = new byte[6];
            this._ActivatorSlotMask = 0x7fffffff;
            this._ActivatorTxInterval = 0x7fffffff;
            this._ActivatorDuration = 0x7fffffff;
            this._ActavtorON = 0x7fffffff;
            this._ActavtorOFF = 0x7fffffff;
            this._TransmissionBaudrate = TransmissionBaudrates.Do_not_modify;
            try
            {
                this._Battery_Average_value = TagBinaryData[0] + (TagBinaryData[1] << 8);
            }
            catch
            {
                this._Battery_Average_value = 0;
            }
            try
            {
                this._Tag_State = (Tag_States) TagBinaryData[4];
            }
            catch
            {
                this._Tag_State = Tag_States.Do_not_modify;
            }
            try
            {
                this._Periodic_Timer_Multiplier = TagBinaryData[0x20];
                this._Periodic_Timer_Multiplier += TagBinaryData[0x21] << 8;
            }
            catch
            {
                this._Periodic_Timer_Multiplier = 1;
            }
            try
            {
                this._Alarm_Timer_Multiplier = TagBinaryData[6];
            }
            catch
            {
                this._Alarm_Timer_Multiplier = 0x7fffffff;
            }
            try
            {
                this._Alarm_Period = TagBinaryData[7];
            }
            catch
            {
                this._Alarm_Period = 0x7fffffff;
            }
            try
            {
                this._Tamper_Panic_Activator_State = (Tamper_Panic_Activator_States) TagBinaryData[8];
            }
            catch
            {
                this._Tag_State = Tag_States.Do_not_modify;
            }
            try
            {
                this._Motion_Sensor_Activator_State = (Motion_Sensor_Activator_States) TagBinaryData[9];
            }
            catch
            {
                this._Motion_Sensor_Activator_State = Motion_Sensor_Activator_States.Do_not_modify;
            }
            try
            {
                this._Tamper_Type_State = (Tamper_Type_States) TagBinaryData[10];
            }
            catch
            {
                this._Tamper_Type_State = Tamper_Type_States.Do_not_modify;
            }
            try
            {
                this._Motion_Threshold = TagBinaryData[11];
            }
            catch
            {
                this._Motion_Threshold = 0x7fffffff;
            }
            try
            {
                this._Transmission_Type = (Transmission_Types) TagBinaryData[12];
            }
            catch
            {
                this._Transmission_Type = Transmission_Types.Do_not_modify;
            }
            try
            {
                this._Firmware_Version = TagBinaryData[14];
            }
            catch
            {
                this._Firmware_Version = 0;
            }
            try
            {
                this._Tag_ID = ((TagBinaryData[0x11] << 0x10) | (TagBinaryData[0x12] << 8)) | TagBinaryData[0x13];
                this._Tag_Site_Code = TagBinaryData[0x10];
            }
            catch
            {
                this._Tag_ID = 0;
                this._Tag_Site_Code = 0;
            }
            try
            {
                for (int i = 0; i < 6; i++)
                {
                    this._Tag_SN[i] = TagBinaryData[i + 0x18];
                }
            }
            catch
            {
                this._Tag_SN = new byte[6];
            }
            try
            {
                if (TagBinaryData[0x22] == 0xff)
                {
                    this._ActvatorFlags = TagActivatorFlags.Undifined;
                }
                else
                {
                    this._ActvatorFlags = (TagActivatorFlags) TagBinaryData[0x22];
                }
            }
            catch
            {
                this._ActvatorFlags = TagActivatorFlags.Undifined;
            }
            try
            {
                this._ActivatorSlotMask = TagBinaryData[0x23];
            }
            catch
            {
                this._ActivatorSlotMask = 0x7fffffff;
            }
            try
            {
                this._ActivatorTxInterval = TagBinaryData[0x24];
            }
            catch
            {
                this._ActivatorTxInterval = 0x7fffffff;
            }
            try
            {
                this._ActivatorDuration = TagBinaryData[0x25];
            }
            catch
            {
                this._ActivatorDuration = 0x7fffffff;
            }
            try
            {
                this._ActavtorON = TagBinaryData[0x26];
            }
            catch
            {
                this._ActavtorON = 0x7fffffff;
            }
            try
            {
                this._ActavtorOFF = TagBinaryData[0x27];
            }
            catch
            {
                this._ActavtorOFF = 0x7fffffff;
            }
            try
            {
                this._TransmissionBaudrate = (TransmissionBaudrates) TagBinaryData[40];
            }
            catch
            {
                this._TransmissionBaudrate = TransmissionBaudrates.Do_not_modify;
            }
        }

        public override string ToString()
        {
            string str = "\n";
            return (((((((((((str + "Tag State - " + this._Tag_State.ToString() + "\n") + "Periodic Timer Multiplier - " + this._Periodic_Timer_Multiplier.ToString() + "\n") + "Alarm Timer Multiplier - " + this._Alarm_Timer_Multiplier.ToString() + "\n") + "Alarm Period - " + this._Alarm_Period.ToString() + "\n") + "Tamper Panic Activator - " + this._Tamper_Panic_Activator_State.ToString() + "\n") + "Motion Sensor Activator - " + this._Motion_Sensor_Activator_State.ToString() + "\n") + "Tamper Type - " + this._Tamper_Type_State.ToString() + "\n") + "Tamper Type - " + this._Tamper_Type_State.ToString() + "\n") + "Motion Threshold - " + this._Motion_Threshold.ToString() + "\n") + "Transmission Type - " + this._Transmission_Type.ToString() + "\n") + "Firmware Version - " + this._Firmware_Version.ToString() + "\n");
        }

        public int ActavtorOFF
        {
            get
            {
                return this._ActavtorOFF;
            }
            set
            {
                this._ActavtorOFF = value;
            }
        }

        public int ActavtorON
        {
            get
            {
                return this._ActavtorON;
            }
            set
            {
                this._ActavtorON = value;
            }
        }

        public int ActivatorDuration
        {
            get
            {
                return this._ActivatorDuration;
            }
            set
            {
                this._ActivatorDuration = value;
            }
        }

        public int ActivatorSlotMask
        {
            get
            {
                return this._ActivatorSlotMask;
            }
            set
            {
                this._ActivatorSlotMask = value;
            }
        }

        public int ActivatorTxInterval
        {
            get
            {
                return this._ActivatorTxInterval;
            }
            set
            {
                this._ActivatorTxInterval = value;
            }
        }

        public TagActivatorFlags ActvatorFlags
        {
            get
            {
                return this._ActvatorFlags;
            }
            set
            {
                this._ActvatorFlags = value;
            }
        }

        public int Alarm_Period
        {
            get
            {
                return this._Alarm_Period;
            }
            set
            {
                this._Alarm_Period = value;
            }
        }

        public int Alarm_Timer_Multiplier
        {
            get
            {
                return this._Alarm_Timer_Multiplier;
            }
            set
            {
                this._Alarm_Timer_Multiplier = value;
            }
        }

        public int Battery_Average_value
        {
            get
            {
                return this._Battery_Average_value;
            }
        }

        public double Battery_Average_Voltage
        {
            get
            {
                return (((this._Battery_Average_value * 0.001075) * 30.0) / 10.0);
            }
        }

        public int Firmware_Version
        {
            get
            {
                return this._Firmware_Version;
            }
        }

        public Motion_Sensor_Activator_States Motion_Sensor_Activator_State
        {
            get
            {
                return this._Motion_Sensor_Activator_State;
            }
            set
            {
                this._Motion_Sensor_Activator_State = value;
            }
        }

        public int Motion_Threshold
        {
            get
            {
                return this._Motion_Threshold;
            }
            set
            {
                this._Motion_Threshold = value;
            }
        }

        public int Periodic_Timer_Multiplier
        {
            get
            {
                return this._Periodic_Timer_Multiplier;
            }
            set
            {
                this._Periodic_Timer_Multiplier = value;
            }
        }

        public int Tag_ID
        {
            get
            {
                return this._Tag_ID;
            }
            set
            {
                this._Tag_ID = value;
            }
        }

        public int Tag_Site_Code
        {
            get
            {
                return this._Tag_Site_Code;
            }
            set
            {
                this._Tag_Site_Code = value;
            }
        }

        public byte[] Tag_SN
        {
            get
            {
                return this._Tag_SN;
            }
            set
            {
                this._Tag_SN = value;
            }
        }

        public Tag_States Tag_State
        {
            get
            {
                return this._Tag_State;
            }
            set
            {
                this._Tag_State = value;
            }
        }

        public Tamper_Panic_Activator_States Tamper_Panic_Activator_State
        {
            get
            {
                return this._Tamper_Panic_Activator_State;
            }
            set
            {
                this._Tamper_Panic_Activator_State = value;
            }
        }

        public Tamper_Type_States Tamper_Type_State
        {
            get
            {
                return this._Tamper_Type_State;
            }
            set
            {
                this._Tamper_Type_State = value;
            }
        }

        public Transmission_Types Transmission_Type
        {
            get
            {
                return this._Transmission_Type;
            }
            set
            {
                this._Transmission_Type = value;
            }
        }

        public TransmissionBaudrates TransmissionBaudrate
        {
            get
            {
                return this._TransmissionBaudrate;
            }
            set
            {
                this._TransmissionBaudrate = value;
            }
        }

        public enum EEPROM_ADDR
        {
            ACTIVATOR = 0x22,
            ACTIVATOR_MULT_ADDR = 0x24,
            ACTIVATOR_OFF_ADDR = 0x27,
            ACTIVATOR_ON_ADDR = 0x26,
            ACTIVATOR_PERIOD_ADDR = 0x25,
            ACTIVATOR_SLOT_MASK = 0x23,
            ALARM_MULT_ADDR = 6,
            ALARM_PERIOD_ADDR = 7,
            BATTARY_AVERAGE = 0,
            FIRMWARE_VERSION_ADDR = 14,
            MOTION_AVAIL_ADDR = 9,
            MOVE_THRESHOLD_ADDR = 11,
            OSCCAL_DATA_ADDR = 15,
            PERIOD_MULT_ADDR_LS = 0x20,
            PERIOD_MULT_ADDR_MS = 0x21,
            Reserved = 13,
            RF_BAUDRATE_ADDR = 40,
            STATE_ADDR = 4,
            TAG_ID_ADDR = 0x10,
            TAG_SN_ADDR = 0x18,
            TAMPER_AVAIL_ADDR = 8,
            TAMPER_TYPE_ADDR = 10,
            TRANSMISSION_TYPE = 12
        }

        public enum Motion_Sensor_Activator_States
        {
            Do_not_modify = 0x7fffffff,
            Motion_Disabled = 0,
            Motion_Enabled_as_Alarm = 1,
            Motion_Enabled_as_Indication = 2
        }

        public enum Tag_States
        {
            Do_not_modify = 0x7fffffff,
            TAG_OFF = 0,
            TAG_ON = 1
        }

        [Flags]
        public enum TagActivatorFlags
        {
            Undifined = 0,
            UseAlarm = 2,
            UseMask = 1,
            UseONOFF = 4
        }

        public enum Tamper_Panic_Activator_States
        {
            Do_not_modify = 0x7fffffff,
            Tamper_Panic_Disabled = 0,
            Tamper_Panic_Enabled_as_Alarm = 1,
            Tamper_Panic_Enabled_as_Indication = 2
        }

        public enum Tamper_Type_States
        {
            Do_not_modify = 0x7fffffff,
            Panic = 1,
            Photo_Sensor = 2,
            ReedSwitch = 4,
            Wire_Tamper = 3
        }

        public enum Transmission_Types
        {
            Do_not_modify = 0x7fffffff,
            Transmit_ID = 0,
            Transmit_SN = 1
        }

        public enum TransmissionBaudrates
        {
            Do_not_modify = 0x7fffffff,
            RFBaudrate19200 = 3,
            RFBaudrate28800 = 1,
            RFBaudrate38400 = 4,
            RFBaudrate57600 = 5,
            RFBaudrate9600 = 2
        }
    }
}

