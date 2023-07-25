namespace IDENTEC
{
    using System;
    using System.Text;

    public class iBusDeviceStatus
    {
        private bool _bGotStatus;
        private System.DateTime _dtStatus;
        private uint _dwStatus;

        internal iBusDeviceStatus()
        {
        }

        internal iBusDeviceStatus(uint dwStatus)
        {
            this._dwStatus = dwStatus;
            this._bGotStatus = true;
            this._dtStatus = System.DateTime.Now;
        }

        private bool GetStatus(int nBitPosition)
        {
            this.ThrowOnInvalidStatus();
            uint num = 1 & (this._dwStatus >> nBitPosition);
            return (num == 1);
        }

        private void ThrowOnInvalidStatus()
        {
            if (!this._bGotStatus)
            {
                throw new InvalidOperationException("The status is invalid");
            }
        }

        public override string ToString()
        {
            StringBuilder builder = new StringBuilder();
            if (this.CRCError)
            {
                builder.Append("* CRC of a telegram received was not valid.\r\n");
            }
            if (this.UnknownHostCommand)
            {
                builder.Append("* Unknown host command.\r\n");
            }
            if (this.InternalVersion)
            {
                builder.Append("* Internal version of firmware.\r\n");
            }
            if (this.HostTimeout)
            {
                builder.Append("* Timeout receiving command from host.\r\n");
            }
            if (this.EscapeDLENotUsedProperly)
            {
                builder.Append("* Invalid escape sequence detected.\r\n");
            }
            if (this.Rebooted)
            {
                builder.Append("* The device rebooted.\r\n");
            }
            if (this.EEPROMParamCRC)
            {
                builder.Append("* Checksum over EEPROM parameters is invalid and reset to default values.\r\n");
            }
            if (this.WatchDogReboot)
            {
                builder.Append("* Hardware or software failure. Watchdog reboot.\r\n");
            }
            if (this.EEPROMCalibrationCRC)
            {
                builder.Append("* Checksum over EEPROM calibration/configuration is invalid and reset to default values. Please contact IDENTEC Solutions.\r\n");
            }
            if (this.ParameterRESETToDefault)
            {
                builder.Append("* Parameters have been resetted to default values. If you did not request it please contact IDENTEC Solutions.\r\n");
            }
            if (this.FallBackSoftware)
            {
                builder.Append("* The device has 2 firmware images and the checksum over the last image fails so the device run the previous firmware.\r\n");
            }
            if (this.VoltageTooLow)
            {
                builder.Append("* Voltage has been detected too low at least once since the last status query.\r\n");
            }
            if (this.ActualVoltageTooLow)
            {
                builder.Append("* Actual voltage is too low.\r\n");
            }
            if (this.TemperatureTooLow)
            {
                builder.Append("* Temperature has been detected too cold (< -20\x00b0C) at least once since the last status query.\r\n");
            }
            if (this.TemperatureTooHigh)
            {
                builder.Append("* Temperature has been detected too hot (> +80\x00b0C) at least once since the last status query.\r\n");
            }
            if (this.LoopError)
            {
                builder.Append("* LF antenna loop has been detected defective at least once since last status query.\r\n");
            }
            if (this.CurrentError)
            {
                builder.Append("* LF antenna current has been detected > 20% differnt at least once since last status query.\r\n");
            }
            if (this.AmplifierError)
            {
                builder.Append("* LF antenna amplifier has been switching off due to risk of overheating at least once since last status query.\r\n");
            }
            if (this.VersionError)
            {
                builder.Append("* HW-version of device hadn't been retrieved at boot time.\r\n");
            }
            if (this.EEPROMError)
            {
                builder.Append("* EEPROM Error.\r\n");
            }
            if (this.QuartzClockError)
            {
                builder.Append("* Quartz Error.\r\n");
            }
            if (this.I2CError)
            {
                builder.Append("* I2C Error.\r\n");
            }
            if (builder.Length == 0)
            {
                return "No errors reported.";
            }
            return builder.ToString();
        }

        public bool ActualVoltageTooLow
        {
            get
            {
                return this.GetStatus(0x11);
            }
        }

        public bool AmplifierError
        {
            get
            {
                return this.GetStatus(0x16);
            }
        }

        public bool CRCError
        {
            get
            {
                return this.GetStatus(0);
            }
        }

        public bool CurrentError
        {
            get
            {
                return this.GetStatus(0x15);
            }
        }

        public System.DateTime DateTime
        {
            get
            {
                return this._dtStatus;
            }
        }

        public long DWord
        {
            get
            {
                return (long) this._dwStatus;
            }
        }

        public bool EEPROMCalibrationCRC
        {
            get
            {
                return this.GetStatus(10);
            }
        }

        public bool EEPROMError
        {
            get
            {
                return this.GetStatus(0x1d);
            }
        }

        public bool EEPROMParamCRC
        {
            get
            {
                return this.GetStatus(8);
            }
        }

        public bool EscapeDLENotUsedProperly
        {
            get
            {
                return this.GetStatus(4);
            }
        }

        public bool FallBackSoftware
        {
            get
            {
                return this.GetStatus(15);
            }
        }

        public bool HasError
        {
            get
            {
                return (this._dwStatus != 0);
            }
        }

        public bool HostTimeout
        {
            get
            {
                return this.GetStatus(3);
            }
        }

        public bool I2CError
        {
            get
            {
                return this.GetStatus(0x1f);
            }
        }

        public bool InternalVersion
        {
            get
            {
                return this.GetStatus(2);
            }
        }

        public bool LoopError
        {
            get
            {
                return this.GetStatus(20);
            }
        }

        public bool ParameterRESETToDefault
        {
            get
            {
                return this.GetStatus(11);
            }
        }

        public bool QuartzClockError
        {
            get
            {
                return this.GetStatus(30);
            }
        }

        public bool Rebooted
        {
            get
            {
                return this.GetStatus(7);
            }
        }

        public bool SlotError
        {
            get
            {
                return this.GetStatus(0x18);
            }
        }

        public bool TemperatureTooHigh
        {
            get
            {
                return this.GetStatus(0x13);
            }
        }

        public bool TemperatureTooLow
        {
            get
            {
                return this.GetStatus(0x12);
            }
        }

        public bool UnknownHostCommand
        {
            get
            {
                return this.GetStatus(1);
            }
        }

        public bool VersionError
        {
            get
            {
                return this.GetStatus(0x17);
            }
        }

        public bool VoltageTooLow
        {
            get
            {
                return this.GetStatus(0x10);
            }
        }

        public bool WatchDogReboot
        {
            get
            {
                return this.GetStatus(9);
            }
        }
    }
}

