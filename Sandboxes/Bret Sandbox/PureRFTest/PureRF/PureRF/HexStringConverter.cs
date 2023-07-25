namespace PureRF
{
    using System;

    public static class HexStringConverter
    {
        public static byte[] ToByteArray(string HexString)
        {
            try
            {
                int length = HexString.Length;
                byte[] buffer = new byte[length / 2];
                for (int i = 0; i < length; i += 2)
                {
                    try
                    {
                        buffer[i / 2] = Convert.ToByte(HexString.Substring(i, 2), 0x10);
                    }
                    catch
                    {
                    }
                }
                return buffer;
            }
            catch
            {
                return new byte[0];
            }
        }
    }
}

