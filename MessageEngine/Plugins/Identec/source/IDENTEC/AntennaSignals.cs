namespace IDENTEC
{
    using System;
    using System.Reflection;
    using System.Runtime.InteropServices;
    using System.Text;

    [StructLayout(LayoutKind.Sequential)]
    public struct AntennaSignals
    {
        public const int InvalidSignal = -128;
        internal const uint SignalsInvalid = 0x80808080;
        internal uint _dwAntennas;
        public AntennaSignals(AntennaSignals other)
        {
            this._dwAntennas = other._dwAntennas;
        }

        public AntennaSignals(int ant1Signal) : this(ant1Signal, -128, -128, -128)
        {
        }

        public AntennaSignals(int ant1Signal, int ant2Signal) : this(ant1Signal, ant2Signal, -128, -128)
        {
        }

        public AntennaSignals(int ant1Signal, int ant2Signal, int ant3Signal) : this(ant1Signal, ant2Signal, ant3Signal, -128)
        {
        }

        internal AntennaSignals(uint dw)
        {
            this._dwAntennas = dw;
        }

        public void Invalidate()
        {
            this._dwAntennas = 0x80808080;
        }

        public AntennaSignals(int ant1Signal, int ant2Signal, int ant3Signal, int ant4Signal)
        {
            byte num = (byte) ((sbyte) ant1Signal);
            byte num2 = (byte) ((sbyte) ant2Signal);
            byte num3 = (byte) ((sbyte) ant3Signal);
            byte num4 = (byte) ((sbyte) ant4Signal);
            this._dwAntennas = (uint) ((((num4 << 0x18) + (num3 << 0x10)) + (num2 << 8)) + num);
        }

        public int GetSignal(int antenna)
        {
            return this[antenna];
        }

        public int BestReceiveAntenna
        {
            get
            {
                if (this._dwAntennas == 0)
                {
                    throw new InvalidOperationException("The antenna signals have not been initialized.");
                }
                int num = -128;
                int num2 = 1;
                for (int i = 1; i <= 4; i++)
                {
                    int num4 = this[i];
                    if (num4 > num)
                    {
                        num = num4;
                        num2 = i;
                    }
                }
                return num2;
            }
        }
        public int BestReceiveSignal
        {
            get
            {
                if (this._dwAntennas == 0)
                {
                    throw new InvalidOperationException("The antenna signals have not been initialized.");
                }
                if (0x80808080 == this._dwAntennas)
                {
                    return 0;
                }
                return this[this.BestReceiveAntenna];
            }
        }
        public int AntennasWithValidSignalCount
        {
            get
            {
                int num = 0;
                for (int i = 1; i <= 4; i++)
                {
                    if (this[i] != -128)
                    {
                        num++;
                    }
                }
                return num;
            }
        }
        public int this[int i]
        {
            get
            {
                if ((i < 1) || (i > 4))
                {
                    throw new IndexOutOfRangeException();
                }
                switch (i)
                {
                    case 1:
                        return (sbyte) ((byte) (this._dwAntennas & 0xff));

                    case 2:
                        return (sbyte) ((byte) ((this._dwAntennas >> 8) & 0xff));

                    case 3:
                        return (sbyte) ((byte) ((this._dwAntennas >> 0x10) & 0xff));

                    case 4:
                        return (sbyte) ((byte) ((this._dwAntennas >> 0x18) & 0xff));
                }
                return -128;
            }
            set
            {
                if ((value > 0) || (value < -128))
                {
                    throw new ArgumentOutOfRangeException();
                }
                if ((i < 1) || (i > 4))
                {
                    throw new IndexOutOfRangeException();
                }
                if (value == 0)
                {
                    value = -128;
                }
                switch (i)
                {
                    case 1:
                        this._dwAntennas = (this._dwAntennas & 0xffffff00) | ((byte) ((sbyte) value));
                        return;

                    case 2:
                        this._dwAntennas = (this._dwAntennas & 0xffff00ff) | ((uint) (((byte) ((sbyte) value)) << 8));
                        return;

                    case 3:
                        this._dwAntennas = (this._dwAntennas & 0xff00ffff) | ((uint) (((byte) ((sbyte) value)) << 0x10));
                        return;

                    case 4:
                        this._dwAntennas = (this._dwAntennas & 0xffffff) | ((uint) (((byte) ((sbyte) value)) << 0x18));
                        return;
                }
            }
        }
        public override string ToString()
        {
            if ((this._dwAntennas == 0) || (this._dwAntennas == 0x80808080))
            {
                return "|------|------|------|------|";
            }
            StringBuilder builder = new StringBuilder();
            builder.Append("|");
            for (int i = 1; i <= 4; i++)
            {
                int num2 = this[i];
                if (-128 == num2)
                {
                    builder.Append("------|");
                }
                else
                {
                    builder.Append(string.Format("{0:d2}dBm|", num2));
                }
            }
            return builder.ToString();
        }

        internal string FormatToString(int bankNumber)
        {
            if ((this._dwAntennas == 0) || (this._dwAntennas == 0x80808080))
            {
                return "|------|------|------|------|";
            }
            bankNumber--;
            int num = bankNumber * 4;
            StringBuilder builder = new StringBuilder();
            StringBuilder builder2 = new StringBuilder();
            if (this.AntennasWithValidSignalCount == 1)
            {
                builder2.Append(" (Antenna");
            }
            else
            {
                builder2.Append(" (Antennas");
            }
            builder.Append("|");
            for (int i = 1; i <= 4; i++)
            {
                int num3 = this[i];
                if (-128 == num3)
                {
                    builder.Append("------|");
                }
                else
                {
                    builder.Append(string.Format("{0:d2}dBm|", num3));
                    builder2.Append(string.Format(" {0},", i + num));
                }
            }
            builder.Append(builder2.ToString().TrimEnd(new char[] { ',' }));
            builder.Append(")");
            return builder.ToString();
        }

        public override int GetHashCode()
        {
            return this._dwAntennas.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            AntennaSignals signals = (AntennaSignals) obj;
            return this._dwAntennas.Equals(signals._dwAntennas);
        }

        public static bool operator ==(AntennaSignals x, AntennaSignals y)
        {
            return (x._dwAntennas == y._dwAntennas);
        }

        public static bool operator !=(AntennaSignals x, AntennaSignals y)
        {
            return (x._dwAntennas != y._dwAntennas);
        }
    }
}

