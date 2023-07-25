namespace IDENTEC.Tags.Logging
{
    using System;
    using System.Collections;
    using System.Reflection;

    public class RawLogSampleCollection : ArrayList
    {
        public RawLogSampleCollection()
        {
        }

        public RawLogSampleCollection(RawLogSampleCollection samples) : base(samples)
        {
        }

        public RawLogSampleCollection(int capacity) : base(capacity)
        {
        }

        public RawLogSample Add()
        {
            return this.Add(new RawLogSample());
        }

        public RawLogSample Add(RawLogSample sample)
        {
            base.Add(sample);
            return sample;
        }

        public bool Contains(RawLogSample sample)
        {
            return base.Contains(sample);
        }

        public void CopyTo(TemperatureLogSample[] array, int index)
        {
            this.CopyTo(array, index);
        }

        public int IndexOf(RawLogSample sample)
        {
            return base.IndexOf(sample);
        }

        public void Insert(int index, RawLogSample sample)
        {
            base.Insert(index, sample);
        }

        public void Remove(RawLogSample sample)
        {
            base.Remove(sample);
        }

        public RawLogSample this[int index]
        {
            get
            {
                return (RawLogSample) base[index];
            }
            set
            {
                base[index] = value;
            }
        }
    }
}

