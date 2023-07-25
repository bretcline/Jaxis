namespace IDENTEC.Readers
{
    using IDENTEC.Tags;
    using System;

    internal sealed class EngineHourTagHelper
    {
        private EngineHourTagHelper()
        {
        }

        private static TimeSpan ReadIQCounter(ITagReaderIQ reader, iQTag tag, int address)
        {
            TagReadDataResult result = reader.ReadTagData(tag, address, 4);
            if (!result.Success)
            {
                throw new PartialTagCommunicationsException("Failed to contact tag");
            }
            uint num = BitConverter.ToUInt32(result.Data, 0) * 6;
            return new TimeSpan(0, (int) num, 0);
        }

        public static TimeSpan ReadIQTagAbsoluteEngineHourCounter(ITagReaderIQ reader, iQTag tag)
        {
            return ReadIQCounter(reader, tag, 0x90);
        }

        public static TimeSpan ReadIQTagUserEngineHourCounter(ITagReaderIQ reader, iQTag tag)
        {
            return ReadIQCounter(reader, tag, 60);
        }

        public static void WriteIQTagUserEngineHourCounter(ITagReaderIQ reader, iQTag tag, TimeSpan ts)
        {
            uint totalMinutes = (uint) ts.TotalMinutes;
            totalMinutes /= 6;
            if (!reader.WriteTagData(tag, 0x90, BitConverter.GetBytes(totalMinutes), 4).Success)
            {
                throw new PartialTagCommunicationsException("Failed to contact tag");
            }
        }
    }
}

