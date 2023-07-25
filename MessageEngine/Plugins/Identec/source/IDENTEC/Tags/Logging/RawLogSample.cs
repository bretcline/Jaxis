namespace IDENTEC.Tags.Logging
{
    using System;

    public class RawLogSample : LogSample, IComparable
    {
        internal ushort _wSample;
        public static readonly short SampleOverflow = 0x7fff;
        public static readonly short SampleUnderflow = -32768;

        public RawLogSample()
        {
        }

        public RawLogSample(RawLogSample sample) : base(sample)
        {
            this._wSample = sample._wSample;
        }

        public int CompareTo(object obj)
        {
            RawLogSample sample = obj as RawLogSample;
            if (sample == null)
            {
                throw new ArgumentException("The tag can only be compared to a RawLogSample object");
            }
            return base.SampleTime.CompareTo(sample.SampleTime);
        }

        public bool Overflow
        {
            get
            {
                return (this.Sample == SampleOverflow);
            }
        }

        [CLSCompliant(false)]
        public ushort Sample
        {
            get
            {
                return this._wSample;
            }
        }

        public bool Underflow
        {
            get
            {
                return (this.Sample == SampleUnderflow);
            }
        }
    }
}

