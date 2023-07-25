namespace IDENTEC.Readers
{
    using System;

    internal sealed class DateTimeConvertor
    {
        private DateTimeConvertor()
        {
        }

        internal static DateTime Convert_time_t(uint t)
        {
            TimeSpan span = (TimeSpan) (DateTime.Now - DateTime.UtcNow);
            DateTime time2 = new DateTime(0x7b2, 1, 1, 0, 0, 0, DateTimeKind.Local);
            return time2.AddSeconds((double) t).Add(span);
        }

        internal static uint ConvertDateTime(DateTime dt)
        {
            DateTime time = new DateTime(0x7b2, 1, 1);
            TimeSpan span = (TimeSpan) (dt - time);
            return (uint) span.TotalSeconds;
        }

        internal static uint GetUtcNow()
        {
            DateTime time = new DateTime(0x7b2, 1, 1);
            DateTime utcNow = DateTime.UtcNow;
            TimeSpan span = (TimeSpan) (DateTime.UtcNow - time);
            return Convert.ToUInt32(span.TotalSeconds);
        }
    }
}

