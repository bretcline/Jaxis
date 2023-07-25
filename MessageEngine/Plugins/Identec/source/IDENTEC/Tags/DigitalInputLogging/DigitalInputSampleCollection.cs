namespace IDENTEC.Tags.DigitalInputLogging
{
    using System;
    using System.Collections;
    using System.Reflection;

    public class DigitalInputSampleCollection : ArrayList
    {
        public DigitalInputSampleCollection()
        {
        }

        public DigitalInputSampleCollection(DigitalInputSampleCollection samples) : base(samples)
        {
        }

        public DigitalInputSampleCollection(int capacity) : base(capacity)
        {
        }

        public DigitalInputLogSample Add()
        {
            return this.Add(new DigitalInputLogSample());
        }

        public DigitalInputLogSample Add(DigitalInputLogSample sample)
        {
            base.Add(sample);
            return sample;
        }

        public bool Contains(DigitalInputLogSample sample)
        {
            return base.Contains(sample);
        }

        public void CopyTo(DigitalInputLogSample[] array, int index)
        {
            this.CopyTo(array, index);
        }

        public int IndexOf(DigitalInputLogSample sample)
        {
            return base.IndexOf(sample);
        }

        public void Insert(int index, DigitalInputLogSample sample)
        {
            base.Insert(index, sample);
        }

        public void Remove(DigitalInputLogSample sample)
        {
            base.Remove(sample);
        }

        public DigitalInputLogSample this[int index]
        {
            get
            {
                return (DigitalInputLogSample) base[index];
            }
            set
            {
                base[index] = value;
            }
        }
    }
}

