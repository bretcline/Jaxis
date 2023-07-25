namespace PureRF
{
    using System;

    public class InitializerPacket
    {
        public const byte HEADER_LEN = 5;
        public CmdID m_CmdID;
        public byte[] m_Payload;
        public byte m_UnitID;
        public const byte MAX_PACKET_LEN = 80;
        public const byte MAX_PAYLOAD_LEN = 0x4a;
        public const byte SYNCBYTE_1 = 0x12;
        public const byte SYNCBYTE_2 = 0x12;

        public InitializerPacket()
        {
        }

        public InitializerPacket(byte UnitID, CmdID CmdID, byte[] Payload)
        {
            this.m_UnitID = UnitID;
            this.m_CmdID = CmdID;
            this.m_Payload = Payload;
        }

        private static byte CalcCRC(byte[] buf)
        {
            return CalcCRC(buf, buf.Length);
        }

        private static byte CalcCRC(byte[] buf, int len)
        {
            byte num = 0;
            for (int i = 0; i < len; i++)
            {
                num = (byte) (num + buf[i]);
            }
            return num;
        }

        public static byte[] LFCRC(byte[] Data)
        {
            byte num = 0;
            int length = Data.Length;
            byte[] buffer = new byte[length + 1];
            for (int i = 0; i < length; i++)
            {
                buffer[i] = Data[i];
                num = (byte) (num + buffer[i]);
            }
            buffer[length] = num;
            return buffer;
        }

        public byte[] Pack()
        {
            byte[] array = new byte[(5 + this.m_Payload.Length) + 1];
            array[0] = 0x12;
            array[1] = 0x12;
            array[2] = this.m_UnitID;
            array[3] = (byte) (this.m_Payload.Length + 2);
            array[4] = (byte) this.m_CmdID;
            this.m_Payload.CopyTo(array, 5);
            array[array.Length - 1] = CalcCRC(array);
            return array;
        }

        public InitializerRetVal Unpack(byte[] buf)
        {
            InitializerRetVal val = VerifyBufPacket(buf);
            if (InitializerRetVal.SUCCESS != val)
            {
                return val;
            }
            this.m_UnitID = buf[2];
            this.m_CmdID = (CmdID) buf[4];
            this.m_Payload = new byte[buf[3] - 2];
            Buffer.BlockCopy(buf, 5, this.m_Payload, 0, this.m_Payload.Length);
            return InitializerRetVal.SUCCESS;
        }

        public static InitializerRetVal VerifyBufPacket(byte[] buf)
        {
            if (5 > buf.Length)
            {
                return InitializerRetVal.PACKET_TOO_SMALL;
            }
            if ((0x12 != buf[0]) || (0x12 != buf[1]))
            {
                return InitializerRetVal.BAD_SYNC;
            }
            byte num = buf[3];
            if (buf[3 + num] != CalcCRC(buf, num + 3))
            {
                return InitializerRetVal.BAD_CRC;
            }
            return InitializerRetVal.SUCCESS;
        }

        public enum CmdID : byte
        {
            COIL_CMD = 7,
            DOWNLOAD = 11,
            GET_FIRMWARE_CHECKSUM = 13,
            GET_NOISE_LEVEL = 0x17,
            GET_SET_ANTENNA_GAIN = 0x3d,
            GET_SET_MODULATION = 0x4b,
            GET_TAGS = 0x31,
            IDENTIFY_TAG = 0x61,
            INFO = 1,
            LF_COMMAND = 0x63,
            RFBaudRate = 0x47,
            SET_BOOT_MODE = 9,
            SET_TAG_ID = 0x65,
            SETRFCONFIG = 0x4d
        }
    }
}

