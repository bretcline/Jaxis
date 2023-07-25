using System;
using System.Collections.Generic;
using System.Text;

namespace LFI.RFID.Format
{
    public interface IDataTypeConverter
    {
        int GetSize(string valueAsText);
        byte[] ToByteArray(string valueAsText);
        string FromByteArray(byte[] byteArray, ref int startIndex);
    }

    public class Int16Converter : IDataTypeConverter
    {
        public int GetSize(string valueAsText) { return 2; }
        public byte[] ToByteArray(string valueAsText)
        {
            Int16 value = Int16.Parse(valueAsText);
            return BitConverter.GetBytes(value);
        }

        public string FromByteArray(byte[] byteArray, ref int startIndex)
        {
            Int16 value = BitConverter.ToInt16(byteArray, startIndex);
            startIndex += 2;
            return value.ToString();
        }
    }

    public class Int32Converter : IDataTypeConverter
    {
        public int GetSize(string valueAsText) { return 4; }
        public byte[] ToByteArray(string valueAsText)
        {
            Int32 value = Int32.Parse(valueAsText);
            return BitConverter.GetBytes(value);
        }

        public string FromByteArray(byte[] byteArray, ref int startIndex)
        {
            Int32 value = BitConverter.ToInt32(byteArray, startIndex);
            startIndex += 4;
            return value.ToString();
        }
    }

    public class DoubleConverter : IDataTypeConverter
    {
        public int GetSize(string valueAsText) { return 8; }
        public byte[] ToByteArray(string valueAsText)
        {
            double value = double.Parse(valueAsText);
            return BitConverter.GetBytes(value);
        }

        public string FromByteArray(byte[] byteArray, ref int startIndex)
        {
            double value = BitConverter.ToDouble(byteArray, startIndex);
            startIndex += 8;
            return value.ToString();
        }
    }

    public class FloatConverter : IDataTypeConverter
    {
        public int GetSize(string valueAsText) { return 4; }
        public byte[] ToByteArray(string valueAsText)
        {
            float value = float.Parse(valueAsText);
            return BitConverter.GetBytes(value);
        }

        public string FromByteArray(byte[] byteArray, ref int startIndex)
        {
            float value = BitConverter.ToSingle(byteArray, startIndex);
            startIndex += 4;
            return value.ToString();
        }
    }

    public class GuidConverter : IDataTypeConverter
    {
        public int GetSize(string valueAsText) { return 16; }
        public byte[] ToByteArray(string valueAsText)
        {
            Guid value = new Guid(valueAsText);
            return value.ToByteArray();
        }

        public string FromByteArray(byte[] byteArray, ref int startIndex)
        {
            byte[] bits = new byte[16];
            for (int offset = 0; offset < 16; offset++)
                bits[offset] = byteArray[offset + startIndex];
            
            Guid value = new Guid(bits);            
            startIndex += 16;
            return value.ToString();
        }
    }

    public class BoolConverter : IDataTypeConverter
    {
        public int GetSize(string valueAsText) { return 1; }
        public byte[] ToByteArray(string valueAsText)
        {
            bool value = bool.Parse(valueAsText);
            return BitConverter.GetBytes(value);
        }

        public string FromByteArray(byte[] byteArray, ref int startIndex)
        {
            bool value = BitConverter.ToBoolean(byteArray, startIndex);
            startIndex += 1;
            return value.ToString();
        }
    }

    public class DateTimeConverter : IDataTypeConverter
    {
        public DateTimeConverter(bool includeDate, bool includeTime)
        {
            this.includeDate = includeDate;
            this.includeTime = includeTime;
        }
        private bool includeDate, includeTime;

        public int GetSize(string valueAsText) 
        {
            int size = 0;
            if (includeDate) size += 4;
            if (includeTime) size += 4;
            return size; 
        }

        public byte[] ToByteArray(string valueAsText)
        {
            DateTime value = DateTime.MinValue;
            if (!string.IsNullOrEmpty(valueAsText))
                value = DateTime.Parse(valueAsText);
            int numBytes = GetSize(string.Empty);
            byte[] result = new byte[numBytes];

            int timeOffset = 0;
            if (includeDate)
            {
                byte[] dateBytes = DateToByteArray(value);
                result[0] = dateBytes[0];
                result[1] = dateBytes[1];
                result[2] = dateBytes[2];
                result[3] = dateBytes[3];
                timeOffset = 4;
            }
            if (includeTime)
            {
                byte[] timeBytes = TimeToByteArray(value);
                result[timeOffset] = timeBytes[0];
                result[timeOffset+1] = timeBytes[1];
                result[timeOffset+2] = timeBytes[2];
                result[timeOffset+3] = timeBytes[3];
            }

            return result;
        }

        private byte[] DateToByteArray(DateTime value)
        {
            TimeSpan span = value - baseDate;
            Int32 daysFromBase = (Int32)span.Days;
            return BitConverter.GetBytes(daysFromBase);
        }

        private byte[] TimeToByteArray(DateTime value)
        {
            DateTime beginOfDay = new DateTime(value.Year, value.Month, value.Day, 0, 0, 0);
            TimeSpan span = value - beginOfDay;
            Int32 secondsIntoDay = (Int32)span.TotalSeconds;
            return BitConverter.GetBytes(secondsIntoDay);
        }

        public string FromByteArray(byte[] byteArray, ref int startIndex)
        {
            int daysFromBase = 0;
            int secondsIntoDay = 0;
            if (includeDate)
            {
                daysFromBase = BitConverter.ToInt32(byteArray, startIndex);
                startIndex += 4;
            }
            if (includeTime)
            {
                secondsIntoDay = BitConverter.ToInt32(byteArray, startIndex);
                startIndex += 4;
            }

            DateTime value = baseDate.AddDays(daysFromBase).AddSeconds(secondsIntoDay);
            if (value == DateTime.MinValue)
                return string.Empty;
            else
                return value.ToString();
        }

        private static DateTime baseDate = new DateTime(1900, 1, 1);
    }

    public class TextConverter : IDataTypeConverter
    {
        public TextConverter(bool unicode)
        {
            this.unicode = unicode;
        }
        private bool unicode; 

        public int GetSize(string valueAsText) 
        {
            byte[] result = ToByteArray(valueAsText);
            return result.Length;
        }

        public byte[] ToByteArray(string valueAsText)
        {
            Encoding encoding = GetEncoding();

            byte[] textBytes = encoding.GetBytes(valueAsText);
            Int16 textLength = (Int16)textBytes.Length;
            byte[] lengthBytes = BitConverter.GetBytes(textLength);

            int numBytes = lengthBytes.Length + textBytes.Length;
            byte[] result = new byte[numBytes];
            int offset = 0;
            foreach (byte value in lengthBytes)
                result[offset++] = value;
            foreach (byte value in textBytes)
                result[offset++] = value;

            return result;
        }

        public string FromByteArray(byte[] byteArray, ref int startIndex)
        {
            // Get the number of bytes in the text array
            Int16 numBytes = BitConverter.ToInt16(byteArray, startIndex);
            startIndex += 2;

            Encoding encoding = GetEncoding();
            string value = encoding.GetString(byteArray, startIndex, numBytes);

            startIndex += numBytes;
            return value;
        }

        private Encoding GetEncoding()
        {
            if (unicode)
                return new UnicodeEncoding();
            else
                return new ASCIIEncoding();
        }
    }
}
