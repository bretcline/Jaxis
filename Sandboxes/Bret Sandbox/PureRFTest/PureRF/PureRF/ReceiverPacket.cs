namespace PureRF
{
    using System;

    public class ReceiverPacket
    {
        public const byte HEADER_LEN = 5;
        public CmdID m_CmdID;
        public byte[] m_Payload;
        public byte m_UnitID;
        public const byte MAX_PACKET_LEN = 80;
        public const byte MAX_PAYLOAD_LEN = 0x4a;
        public const byte SYNCBYTE_1 = 0x12;
        public const byte SYNCBYTE_2 = 0x12;

        public ReceiverPacket()
        {
        }

        public ReceiverPacket(byte UnitID, CmdID CmdID, byte[] Payload)
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

        public ReceiverRetVal Unpack(byte[] buf)
        {
            ReceiverRetVal val = VerifyBufPacket(buf);
            if (ReceiverRetVal.SUCCESS != val)
            {
                return val;
            }
            this.m_UnitID = buf[2];
            this.m_CmdID = (CmdID) buf[4];
            this.m_Payload = new byte[buf[3] - 2];
            Buffer.BlockCopy(buf, 5, this.m_Payload, 0, this.m_Payload.Length);
            return ReceiverRetVal.SUCCESS;
        }

        public static ReceiverRetVal VerifyBufPacket(byte[] buf)
        {
            if (5 > buf.Length)
            {
                return ReceiverRetVal.PACKET_TOO_SMALL;
            }
            if ((0x12 != buf[0]) || (0x12 != buf[1]))
            {
                return ReceiverRetVal.BAD_SYNC;
            }
            byte num = buf[3];
            if (buf[3 + num] != CalcCRC(buf, num + 3))
            {
                return ReceiverRetVal.BAD_CRC;
            }
            return ReceiverRetVal.SUCCESS;
        }

        public enum CmdID : byte
        {
            ACTIVATE_RELAY = 0x5d,
            DOWNLOAD = 11,
            FLUSH_TAG_BUFFER = 0x3f,
            GET_FIRMWARE_CHECKSUM = 13,
            GET_NOISE_LEVEL = 0x17,
            GET_SET_ANTENNA_GAIN = 0x3d,
            GET_SET_MODE = 9,
            GET_SET_MODULATION = 0x4b,
            GET_SET_NAME = 0x41,
            GET_SET_POWER_CONTROL = 7,
            GET_SET_RF_BAUDRATE = 0x47,
            GET_SET_RSSI = 0x43,
            GET_SET_SERIAL_NUM = 0x13,
            GET_SET_SITECODE = 0x45,
            GET_SET_TIME = 15,
            GET_TAGS = 0x31,
            INFO = 1,
            IS_GOT_BROADCAST = 11,
            SETRFCONFIG = 0x4d,
            STATUS = 3
        }
    }
}

