namespace IDENTEC
{
    using System;
    using System.Reflection;
    using System.Text;

    public class MultiAntennaSignals
    {
        private AntennaSignals[] _Ants;
        private int _sendingAntenna;
        public const int MinAntennas = 4;

        public MultiAntennaSignals() : this(4)
        {
        }

        public MultiAntennaSignals(MultiAntennaSignals sigs)
        {
            this.Copy(sigs);
        }

        public MultiAntennaSignals(int totalAntennas)
        {
            int num = 0;
            if ((totalAntennas % 4) != 0)
            {
                num = 1;
            }
            this._Ants = new AntennaSignals[(totalAntennas / 4) + num];
            for (int i = 0; i < this._Ants.Length; i++)
            {
                this._Ants[i].Invalidate();
            }
        }

        public void Copy(MultiAntennaSignals antennas)
        {
            this._Ants = new AntennaSignals[antennas._Ants.Length];
            for (int i = 0; i < antennas._Ants.Length; i++)
            {
                this._Ants[i]._dwAntennas = antennas._Ants[i]._dwAntennas;
            }
            this._sendingAntenna = antennas._sendingAntenna;
        }

        public int GetSignal(int antenna)
        {
            int index = (antenna - 1) / 4;
            int num2 = 1 + ((antenna - 1) % 4);
            return this._Ants[index][num2];
        }

        public void Invalidate()
        {
            for (int i = 0; i < this._Ants.Length; i++)
            {
                this._Ants[i].Invalidate();
            }
        }

        public void Resize(int totalAntennas)
        {
            int num = 0;
            if ((totalAntennas % 4) != 0)
            {
                num = 1;
            }
            AntennaSignals[] signalsArray = new AntennaSignals[(totalAntennas / 4) + num];
            for (int i = 0; i < signalsArray.Length; i++)
            {
                if (this._Ants.Length > i)
                {
                    signalsArray[i]._dwAntennas = this._Ants[i]._dwAntennas;
                }
                else
                {
                    signalsArray.Initialize();
                }
            }
        }

        public void SetSignal(int antenna, int signal)
        {
            int index = (antenna - 1) / 4;
            int num2 = 1 + ((antenna - 1) % 4);
            this._Ants[index][num2] = signal;
        }

        public override string ToString()
        {
            StringBuilder builder = new StringBuilder();
            for (int i = 0; i < this._Ants.Length; i++)
            {
                builder.Append(this._Ants[i].FormatToString(i + 1));
                builder.Append("\r\n");
            }
            return builder.ToString().TrimEnd(new char[] { '\r', '\n' }).TrimEnd(new char[] { '|', '-', '\r', '\n' });
        }

        public int AntennasWithValidSignalCount
        {
            get
            {
                int num = 0;
                if (this._Ants != null)
                {
                    foreach (AntennaSignals signals in this._Ants)
                    {
                        num += signals.AntennasWithValidSignalCount;
                    }
                }
                return num;
            }
        }

        public int BestReceiveAntenna
        {
            get
            {
                int num = -128;
                int num2 = 0;
                int num3 = this._Ants.Length * 4;
                for (int i = 1; i <= num3; i++)
                {
                    int num5 = this[i];
                    if (num5 > num)
                    {
                        num = num5;
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
                int bestReceiveAntenna = this.BestReceiveAntenna;
                if (bestReceiveAntenna > 0)
                {
                    return this[bestReceiveAntenna];
                }
                return -128;
            }
        }

        public int Count
        {
            get
            {
                if (this._Ants == null)
                {
                    return 0;
                }
                return (this._Ants.Length * 4);
            }
        }

        public int this[int antenna]
        {
            get
            {
                return this.GetSignal(antenna);
            }
            set
            {
                this.SetSignal(antenna, value);
            }
        }

        public int SendingAntenna
        {
            get
            {
                return this._sendingAntenna;
            }
            set
            {
                this._sendingAntenna = value;
            }
        }
    }
}

