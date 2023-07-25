namespace IDENTEC.Tags.Logging
{
    using System;
    using System.Collections;
    using System.Reflection;

    public class TemperatureLogSampleCollection : ArrayList
    {
        public TemperatureLogSampleCollection()
        {
        }

        public TemperatureLogSampleCollection(TemperatureLogSampleCollection samples) : base(samples)
        {
        }

        public TemperatureLogSampleCollection(int capacity) : base(capacity)
        {
        }

        public TemperatureLogSample Add()
        {
            return this.Add(new TemperatureLogSample());
        }

        public TemperatureLogSample Add(TemperatureLogSample sample)
        {
            base.Add(sample);
            return sample;
        }

        public bool Contains(TemperatureLogSample sample)
        {
            return base.Contains(sample);
        }

        public void CopyTo(TemperatureLogSample[] array, int index)
        {
            this.CopyTo(array, index);
        }

        public int IndexOf(TemperatureLogSample sample)
        {
            return base.IndexOf(sample);
        }

        public void Insert(int index, TemperatureLogSample sample)
        {
            base.Insert(index, sample);
        }

        public void Remove(TemperatureLogSample sample)
        {
            base.Remove(sample);
        }

        public TemperatureLogSample this[int index]
        {
            get
            {
                return (TemperatureLogSample) base[index];
            }
            set
            {
                base[index] = value;
            }
        }
    }
}

