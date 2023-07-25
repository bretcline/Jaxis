namespace IDENTEC.Tags.Logging
{
    using IDENTEC.Tags;
    using System;

    public class TemperatureLogData : RawLogData
    {
        private TemperatureLogSample _highestTemperatureRecord = new TemperatureLogSample();
        private TemperatureLogSample _lowestTemperatureRecord = new TemperatureLogSample();
        private TemperatureLogSampleCollection _samples;
        internal float[] m_fTemperatures;

        internal void ConvertLogToCelsius(iQTag.Model model)
        {
            base.ConvertRawBufferToWords();
            if (base.m_wBuffer == null)
            {
                this.m_fTemperatures = new float[0];
            }
            else
            {
                this.m_fTemperatures = new float[base.m_wBuffer.Length];
                for (int i = 0; i < base.m_wBuffer.Length; i++)
                {
                    this.m_fTemperatures[i] = ConvertRawToCelsius(base.m_wBuffer[i], model);
                }
            }
        }

        internal static float ConvertRawToCelsius(ushort sample, iQTag.Model model)
        {
            if (model == iQTag.Model.IQ32Elpro)
            {
                short num = (short) sample;
                if ((num & 0x2000) != 0)
                {
                    num = (short) (num | -16384);
                }
                else
                {
                    num = (short) (num & 0x3fff);
                }
                return (((float) num) / 10f);
            }
            short num3 = (short) sample;
            if ((num3 & 0x200) != 0)
            {
                num3 = (short) (num3 | -1024);
            }
            return (((float) num3) / 4f);
        }

        internal void CreateSamples()
        {
            this._samples = new TemperatureLogSampleCollection(this.SampleCount);
            if (this.SampleCount != 0)
            {
                DateTime dtStart;
                int totalSeconds = 0;
                totalSeconds = (int) base.m_LogInfo.m_tsLogInterval.TotalSeconds;
                if (!this.Wrapped)
                {
                    dtStart = base.m_dtStart;
                }
                else
                {
                    dtStart = this.m_dtEnd.AddSeconds((double) (-1 * (totalSeconds * (this.SampleCount - 1))));
                }
                for (int i = 0; i < this.SampleCount; i++)
                {
                    TemperatureLogSample sample = this._samples.Add();
                    sample._time = dtStart.AddSeconds((double) (totalSeconds * i));
                    sample.m_temperatureDegreesC = this.m_fTemperatures[i];
                }
            }
            this.GetExtremes();
        }

        private void GetExtremes()
        {
            if (this._lowestTemperatureRecord.m_temperatureDegreesC == float.MinValue)
            {
                TemperatureLogSampleCollection samples = new TemperatureLogSampleCollection(this.Samples);
                if (samples.Count > 0)
                {
                    samples.Sort(new CompareTemperatureLogSamplesByLowestTemperature());
                    this._lowestTemperatureRecord = new TemperatureLogSample(samples[0]);
                    samples.Sort(new CompareTemperatureLogSamplesByHighestTemperature());
                    this._highestTemperatureRecord = new TemperatureLogSample(samples[0]);
                }
            }
        }

        public override string ToString()
        {
            return string.Format("Begin: {0} -- End: {1} -- Interval: {2} -- Sample Count {3} -- Max: {4} -- Min: {5}", new object[] { this.Start, this.End, this.LoggingInterval, this.SampleCount, this.HighestTemperatureRecord, this.LowestTemperatureRecord });
        }

        public TemperatureLogSample HighestTemperatureRecord
        {
            get
            {
                return this._highestTemperatureRecord;
            }
        }

        public TemperatureLogSample LowestTemperatureRecord
        {
            get
            {
                return this._lowestTemperatureRecord;
            }
        }

        public override int SampleCount
        {
            get
            {
                if (this.m_fTemperatures == null)
                {
                    return base.SampleCount;
                }
                return this.m_fTemperatures.Length;
            }
        }

        public TemperatureLogSampleCollection Samples
        {
            get
            {
                if (this._samples == null)
                {
                    this.CreateSamples();
                }
                return this._samples;
            }
        }
    }
}

