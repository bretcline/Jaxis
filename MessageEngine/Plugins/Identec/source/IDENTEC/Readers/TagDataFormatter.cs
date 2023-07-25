namespace IDENTEC.Readers
{
    using IDENTEC.Tags;
    using System;
    using System.Text;

    internal sealed class TagDataFormatter
    {
        private TagDataFormatter()
        {
        }

        internal static byte[] CreateFormattedBuffer(byte[] byData, int bytesToWrite)
        {
            byte[] destinationArray = new byte[bytesToWrite + 5];
            destinationArray[0] = 0x7e;
            ushort num = (ushort) bytesToWrite;
            byte[] bytes = BitConverter.GetBytes(num);
            Array.Copy(bytes, 0, destinationArray, 1, bytes.Length);
            bytes = BitConverter.GetBytes(CRC.Crc16TagData(byData, bytesToWrite));
            Array.Copy(bytes, 0, destinationArray, 3, bytes.Length);
            Array.Copy(byData, 0, destinationArray, 5, bytesToWrite);
            return destinationArray;
        }

        internal static TagReadStringResult ReadTagDataString(ITagReaderID2 reader, iD2Tag tag, int address)
        {
            TagReadStringResult result = new TagReadStringResult();
            TagReadDataResult result2 = reader.ReadTagDataWithCRCAndLength(tag, address);
            if (result2.Success && (0 < result2.BytesRead))
            {
                result.BytesRead = result2.BytesRead;
                result.Text = new string(Encoding.ASCII.GetString(result2.Data, 0, result2.BytesRead).ToCharArray());
                result.Success = true;
            }
            return result;
        }

        internal static TagReadStringResult ReadTagDataString(ITagReaderIQ reader, iQTag tag, int address)
        {
            TagReadStringResult result = new TagReadStringResult();
            TagReadDataResult result2 = reader.ReadTagDataWithCRCAndLength(tag, address);
            if (result2.Success && (0 < result2.BytesRead))
            {
                result.BytesRead = result2.BytesRead;
                result.Text = new string(Encoding.ASCII.GetString(result2.Data, 0, result2.BytesRead).ToCharArray());
                result.Success = true;
            }
            return result;
        }

        internal static TagReadDataResult ReadTagDataWithCRCAndLength(ITagReaderID2 reader, iD2Tag tag, int address)
        {
            TagReadDataResult result = new TagReadDataResult();
            TagReadDataResult result2 = reader.ReadTagData(tag, address, 5);
            if (result2.Success)
            {
                if (0x7e != result2.Data[0])
                {
                    throw new TagDataException("The tag does not contain formatted data at the specified address.");
                }
                ushort bytesToRead = BitConverter.ToUInt16(result2.Data, 1);
                if (bytesToRead > tag.DataCapacity)
                {
                    throw new TagDataException("The tag's formatted data does not contain valid length information.");
                }
                result = reader.ReadTagData(tag, address + 5, bytesToRead);
                if (!result.Success)
                {
                    return result;
                }
                ushort num2 = BitConverter.ToUInt16(result2.Data, 3);
                if (CRC.Crc16TagData(result.Data, result.BytesRead) != num2)
                {
                    throw new TagDataException("The formatted data on the tag failed the CRC check. The data must have been overwritten.", result.Data);
                }
            }
            return result;
        }

        internal static TagReadDataResult ReadTagDataWithCRCAndLength(ITagReaderIQ reader, iQTag tag, int address)
        {
            TagReadDataResult result = new TagReadDataResult();
            TagReadDataResult result2 = reader.ReadTagData(tag, address, 5);
            if (result2.Success)
            {
                if (0x7e != result2.Data[0])
                {
                    throw new TagDataException("The tag does not contain formatted data at the specified address.");
                }
                ushort bytesToRead = BitConverter.ToUInt16(result2.Data, 1);
                if (bytesToRead > tag.DataCapacity)
                {
                    throw new TagDataException("The tag's formatted data does not contain valid length information.");
                }
                result = reader.ReadTagData(tag, address + 5, bytesToRead);
                if (!result.Success)
                {
                    return result;
                }
                ushort num2 = BitConverter.ToUInt16(result2.Data, 3);
                if (CRC.Crc16TagData(result.Data, result.BytesRead) != num2)
                {
                    throw new TagDataException("The formatted data on the tag failed the CRC check. The data must have been overwritten.", result.Data);
                }
            }
            return result;
        }

        internal static TagWriteDataResult WriteTagDataString(ITagReaderID2 reader, iD2Tag tag, int address, string text)
        {
            return WriteTagDataWithCRCAndLength(reader, tag, address, Encoding.ASCII.GetBytes(text), text.Length);
        }

        internal static TagWriteDataResult WriteTagDataString(ITagReaderIQ reader, iQTag tag, int address, string text)
        {
            return WriteTagDataWithCRCAndLength(reader, tag, address, Encoding.ASCII.GetBytes(text), text.Length);
        }

        internal static TagWriteDataResult WriteTagDataWithCRCAndLength(ITagReaderID2 reader, iD2Tag tag, int address, byte[] byData, int bytesToWrite)
        {
            byte[] buffer = CreateFormattedBuffer(byData, bytesToWrite);
            TagWriteDataResult result = reader.WriteTagData(tag, address, buffer, buffer.Length);
            if (result.BytesWritten > 0)
            {
                result.BytesWritten -= 5;
            }
            return result;
        }

        internal static TagWriteDataResult WriteTagDataWithCRCAndLength(ITagReaderIQ reader, iQTag tag, int address, byte[] byData, int bytesToWrite)
        {
            byte[] buffer = CreateFormattedBuffer(byData, bytesToWrite);
            TagWriteDataResult result = reader.WriteTagData(tag, address, buffer, buffer.Length);
            if (result.BytesWritten > 0)
            {
                result.BytesWritten -= 5;
            }
            return result;
        }
    }
}

