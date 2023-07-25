namespace PureRF
{
    using System;
    using System.IO.Ports;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using System.Text;
    using System.Threading;

    public class Initializer
    {
        private byte _TransactionID;
        public const int BOOTLOADER_BAUDRATE = 0x1c200;
        private SerialPort Conn;
        public const int FIRMWARE_BAUDRATE = 0x1c200;
        public const byte FIRMWARE_PACKET_LEN = 0x10;
        public static TagID INVALID_TAG_ID = new TagID(uint.MaxValue);
        public InitializerPacket LastReplyPacket;
        public int TransmisionDelay = 80;

        public Initializer(SerialPort SerialConn)
        {
            this.Conn = SerialConn;
        }

        public void CloseConnection()
        {
            try
            {
                this.Conn.Close();
            }
            catch
            {
            }
        }

        private InitializerRetVal CoilCmd(byte UnitID, byte[] payload)
        {
            InitializerPacket packet;
            InitializerRetVal val = this.PacketTransaction(UnitID, InitializerPacket.CmdID.COIL_CMD, payload, out packet);
            if (InitializerRetVal.SUCCESS != val)
            {
                return val;
            }
            switch (packet.m_Payload[0])
            {
                case 0:
                    return InitializerRetVal.ERROR;

                case 1:
                    return InitializerRetVal.SUCCESS;

                case 5:
                    return InitializerRetVal.INVALID_OR_NO_TAG;
            }
            return InitializerRetVal.PROTOCOL_ERROR;
        }

        public InitializerRetVal CoilCmd_SetTimerMultiplier(byte unitID, TagID tagID, TimerType timerMode, int multiplier)
        {
            if (multiplier > 0)
            {
                multiplier--;
            }
            if (timerMode != TimerType.Periodic_Timer)
            {
                return this.LF_Command(unitID, tagID, (byte) timerMode, (byte) multiplier);
            }
            InitializerRetVal val = this.LF_Command(unitID, tagID, 0x20, (byte) (multiplier & 0xff));
            if (val != InitializerRetVal.SUCCESS)
            {
                return val;
            }
            Thread.Sleep(this.TransmisionDelay);
            return this.LF_Command(unitID, tagID, 0x21, (byte) ((multiplier >> 8) & 0xff));
        }

        public InitializerRetVal CoilCmd_SetWireStatus(byte unitID, TagID tagID, bool hasWire)
        {
            byte[] payload = new byte[] { 0x1b, (byte) ((tagID.GetPureRFTagID() >> 8) & 0xff), (byte) (tagID.GetPureRFTagID() & 0xff), 8, hasWire ? ((byte) 2) : ((byte) 1) };
            return this.CoilCmd(unitID, payload);
        }

        public InitializerRetVal CoilCmd_TagOnOff(byte unitID, TagID tagID, bool turnON)
        {
            byte parameter = turnON ? ((byte) 1) : ((byte) 0);
            return this.SetTagParameter(unitID, tagID, TagParameterComand.ON_OFF, parameter);
        }

        public InitializerRetVal CoilCmd_TransmissionType(byte unitID, TagID tagID, bool turnSNOn)
        {
            byte parameter = turnSNOn ? ((byte) 1) : ((byte) 0);
            return this.SetTagParameter(unitID, tagID, TagParameterComand.TRANSMISSION_TYPE, parameter);
        }

        public InitializerRetVal Download(byte unitID, ushort packet_num, byte[] firmwarePacket)
        {
            InitializerPacket packet;
            byte[] array = new byte[0x12];
            if (0x10 != firmwarePacket.Length)
            {
                return InitializerRetVal.BAD_PARAMS;
            }
            array[0] = (byte) (packet_num >> 8);
            array[1] = (byte) (packet_num & 0xff);
            firmwarePacket.CopyTo(array, 2);
            InitializerRetVal val = this.PacketTransaction(unitID, InitializerPacket.CmdID.DOWNLOAD, array, out packet);
            if (InitializerRetVal.SUCCESS != val)
            {
                return val;
            }
            if (((packet.m_Payload.Length != 3) || (packet.m_Payload[0] != array[0])) || (packet.m_Payload[1] != array[1]))
            {
                return InitializerRetVal.PROTOCOL_ERROR;
            }
            if (packet.m_Payload[2] != 1)
            {
                return InitializerRetVal.ERROR;
            }
            return InitializerRetVal.SUCCESS;
        }

        public InitializerRetVal FirmwareState(byte unitID, out ushort flash_cksum)
        {
            InitializerPacket packet;
            InitializerRetVal val = this.PacketTransaction(unitID, InitializerPacket.CmdID.GET_FIRMWARE_CHECKSUM, out packet);
            flash_cksum = 0;
            if (InitializerRetVal.SUCCESS != val)
            {
                return val;
            }
            if (11 == packet.m_Payload.Length)
            {
                flash_cksum = (ushort) ((packet.m_Payload[0] << 8) | packet.m_Payload[1]);
                switch (packet.m_Payload[2])
                {
                    case 0:
                        return InitializerRetVal.FLASH_NO_FIRMWARE;

                    case 1:
                        return InitializerRetVal.FLASH_DAMAGED;

                    case 2:
                        return InitializerRetVal.FLASH_OK;
                }
            }
            return InitializerRetVal.PROTOCOL_ERROR;
        }

        public InitializerRetVal Get_8_Bytes_from_Tag_EEPROM(byte UnitID, TagID tagID, int FromAddress, out byte[] Response)
        {
            InitializerPacket packet;
            byte[] data = new byte[4];
            uint pureRFTagID = tagID.GetPureRFTagID();
            Response = new byte[8];
            data[0] = 0x1b;
            data[1] = (byte) (pureRFTagID >> 8);
            data[2] = (byte) pureRFTagID;
            data[3] = (byte) FromAddress;
            data = InitializerPacket.LFCRC(data);
            InitializerRetVal val = this.PacketTransaction(UnitID, InitializerPacket.CmdID.LF_COMMAND, data, out packet);
            if (InitializerRetVal.SUCCESS != val)
            {
                return val;
            }
            if (1 == packet.m_Payload.Length)
            {
                switch (packet.m_Payload[0])
                {
                    case 2:
                        return InitializerRetVal.TAG_NAC;

                    case 5:
                        return InitializerRetVal.TAG_TIMEOUT;
                }
                return InitializerRetVal.PROTOCOL_ERROR;
            }
            if (10 != packet.m_Payload.Length)
            {
                return InitializerRetVal.PROTOCOL_ERROR;
            }
            for (int i = 0; i < 8; i++)
            {
                Response[i] = packet.m_Payload[i + 1];
            }
            return InitializerRetVal.SUCCESS;
        }

        public InitializerRetVal Get_Tag_Serial_Number(byte UnitID, TagID tagID, out byte[] TagSN)
        {
            byte[] buffer;
            TagSN = new byte[6];
            InitializerRetVal val = this.Get_8_Bytes_from_Tag_EEPROM(UnitID, tagID, 0x18, out buffer);
            if (InitializerRetVal.SUCCESS == val)
            {
                for (int i = 0; i < 6; i++)
                {
                    TagSN[i] = buffer[i];
                }
            }
            return val;
        }

        public InitializerRetVal Get_Tag_Settings(byte UnitID, TagID tagID, out Tag_Settings TagSettings)
        {
            byte[] tagBinaryData = new byte[0x30];
            TagSettings = null;
            InitializerRetVal val = InitializerRetVal.PROTOCOL_ERROR;
            for (int i = 0; i < (tagBinaryData.Length / 8); i++)
            {
                byte[] buffer2;
                Thread.Sleep(580);
                val = this.Get_8_Bytes_from_Tag_EEPROM(UnitID, tagID, i * 8, out buffer2);
                if (InitializerRetVal.SUCCESS != val)
                {
                    val = this.Get_8_Bytes_from_Tag_EEPROM(UnitID, tagID, i * 8, out buffer2);
                }
                if (InitializerRetVal.SUCCESS != val)
                {
                    return val;
                }
                for (int j = 0; j < buffer2.Length; j++)
                {
                    tagBinaryData[(i * 8) + j] = buffer2[j];
                }
            }
            TagSettings = new Tag_Settings(tagBinaryData);
            return val;
        }

        public InitializerRetVal GetAntennaGain(byte unitID, out AntennaGain antennaGain)
        {
            InitializerPacket packet;
            byte[] payload = new byte[1];
            InitializerRetVal val = this.PacketTransaction(unitID, InitializerPacket.CmdID.GET_SET_ANTENNA_GAIN, payload, out packet);
            antennaGain = AntennaGain.INVALID;
            if (InitializerRetVal.SUCCESS != val)
            {
                return val;
            }
            if (1 == packet.m_Payload.Length)
            {
                switch (packet.m_Payload[0])
                {
                    case 1:
                        antennaGain = AntennaGain.HIGH;
                        goto Label_0055;

                    case 2:
                        antennaGain = AntennaGain.LOW;
                        goto Label_0055;
                }
            }
            return InitializerRetVal.PROTOCOL_ERROR;
        Label_0055:
            return InitializerRetVal.SUCCESS;
        }

        public InitializerRetVal GetInitializerRFBaudRate(byte unitID, out Tag_Settings.TransmissionBaudrates RFBaud)
        {
            InitializerPacket packet;
            byte[] payload = new byte[1];
            RFBaud = Tag_Settings.TransmissionBaudrates.Do_not_modify;
            InitializerRetVal val = this.PacketTransaction(unitID, InitializerPacket.CmdID.RFBaudRate, payload, out packet);
            if (InitializerRetVal.SUCCESS == val)
            {
                if (1 != packet.m_Payload.Length)
                {
                    return InitializerRetVal.PROTOCOL_ERROR;
                }
                if (packet.m_Payload[0] > 5)
                {
                    packet.m_Payload[0] = 4;
                }
                try
                {
                    RFBaud = (Tag_Settings.TransmissionBaudrates) packet.m_Payload[0];
                }
                catch
                {
                }
            }
            return val;
        }

        public InitializerRetVal GetMode(byte unitID)
        {
            InitializerPacket packet;
            byte[] payload = new byte[1];
            InitializerRetVal val = this.PacketTransaction(unitID, InitializerPacket.CmdID.SET_BOOT_MODE, payload, out packet);
            if (InitializerRetVal.SUCCESS != val)
            {
                return val;
            }
            if (1 == packet.m_Payload.Length)
            {
                switch (packet.m_Payload[0])
                {
                    case 1:
                        return InitializerRetVal.MODE_BOOTLOADER;

                    case 2:
                        return InitializerRetVal.MODE_FIRMWARE;
                }
            }
            return InitializerRetVal.PROTOCOL_ERROR;
        }

        public InitializerRetVal GetModulation(byte unitID, out ModulationParameter Modulation)
        {
            InitializerPacket packet;
            byte[] payload = new byte[1];
            Modulation = ModulationParameter.INVALID;
            InitializerRetVal val = this.PacketTransaction(unitID, InitializerPacket.CmdID.GET_SET_MODULATION, payload, out packet);
            if (InitializerRetVal.SUCCESS != val)
            {
                return val;
            }
            if (1 != packet.m_Payload.Length)
            {
                return InitializerRetVal.PROTOCOL_ERROR;
            }
            if (packet.m_Payload[0] > 2)
            {
                packet.m_Payload[0] = 1;
            }
            try
            {
                Modulation = (ModulationParameter) packet.m_Payload[0];
            }
            catch (Exception)
            {
                return InitializerRetVal.PROTOCOL_ERROR;
            }
            return InitializerRetVal.SUCCESS;
        }

        public InitializerRetVal GetNoiseLevel(byte unitID, out ushort noiseLevel)
        {
            InitializerPacket packet;
            InitializerRetVal val = this.PacketTransaction(unitID, InitializerPacket.CmdID.GET_NOISE_LEVEL, out packet);
            noiseLevel = 0xffff;
            if (InitializerRetVal.SUCCESS != val)
            {
                return val;
            }
            if (2 != packet.m_Payload.Length)
            {
                return InitializerRetVal.PROTOCOL_ERROR;
            }
            noiseLevel = (ushort) ((packet.m_Payload[0] << 8) | packet.m_Payload[1]);
            return InitializerRetVal.SUCCESS;
        }

        public InitializerRetVal GetTagChangebleID(byte UnitID, TagID ReportedTagID, out TagID TagChangebleID)
        {
            byte[] buffer;
            TagChangebleID = new TagID();
            InitializerRetVal val = this.Get_8_Bytes_from_Tag_EEPROM(UnitID, ReportedTagID, 0x10, out buffer);
            if (InitializerRetVal.SUCCESS == val)
            {
                TagChangebleID.SetPureRFTagID((uint) ((((buffer[0] << 0x18) | (buffer[1] << 0x10)) | (buffer[2] << 8)) | buffer[3]));
            }
            return val;
        }

        public InitializerRetVal GetTagIDVersion(byte unitID, out TagIDVersion tagIDVersion)
        {
            tagIDVersion = TagIDVersion.VER_2;
            return InitializerRetVal.SUCCESS;
        }

        public InitializerRetVal GetTags(byte unitID, byte transactionID, byte maxTagsCount, out Tag[] tags)
        {
            return this.GetTags(unitID, transactionID, maxTagsCount, out tags, false);
        }

        public InitializerRetVal GetTags(byte unitID, byte transactionID, byte maxTagsCount, out Tag[] tags, bool withTS)
        {
            byte num = 0;
            if (transactionID == 0)
            {
                transactionID = this.GetTransactionID();
            }
            byte[] payload = new byte[] { maxTagsCount, transactionID };
            int count = 10;
            InitializerRetVal val = this.PacketTransaction(unitID, InitializerPacket.CmdID.GET_TAGS, payload, out this.LastReplyPacket);
            tags = new Tag[0];
            if (InitializerRetVal.SUCCESS != val)
            {
                return val;
            }
            if ((2 > this.LastReplyPacket.m_Payload.Length) || (transactionID != this.LastReplyPacket.m_Payload[1]))
            {
                return InitializerRetVal.PROTOCOL_ERROR;
            }
            num = this.LastReplyPacket.m_Payload[2];
            tags = new Tag[num];
            for (int i = 0; i < num; i++)
            {
                byte[] dst = new byte[count];
                Buffer.BlockCopy(this.LastReplyPacket.m_Payload, 3 + (i * count), dst, 0, count);
                tags[i] = new Tag();
                if (!tags[i].Unpack(dst))
                {
                    return InitializerRetVal.PROTOCOL_ERROR;
                }
            }
            return InitializerRetVal.SUCCESS;
        }

        private byte GetTransactionID()
        {
            this._TransactionID = (byte) (this._TransactionID + 1);
            return this._TransactionID;
        }

        public InitializerRetVal GetUnitInfo(byte unitID, out UnitInfo unitInfo)
        {
            InitializerPacket packet;
            InitializerRetVal val = this.PacketTransaction(unitID, InitializerPacket.CmdID.INFO, out packet);
            unitInfo = new UnitInfo();
            if (InitializerRetVal.SUCCESS != val)
            {
                return val;
            }
            if (0x1c != packet.m_Payload.Length)
            {
                return InitializerRetVal.PROTOCOL_ERROR;
            }
            unitInfo.protocol_version = packet.m_Payload[0];
            unitInfo.Device_Class = packet.m_Payload[1];
            unitInfo.Device_SubClass = packet.m_Payload[2];
            unitInfo.Firmware_Version = packet.m_Payload[3];
            unitInfo.Unit_Name = Encoding.ASCII.GetString(packet.m_Payload, 4, 20);
            unitInfo.Unit_Serial_Number = (uint) ((((packet.m_Payload[0x18] << 0x18) | (packet.m_Payload[0x19] << 0x10)) | (packet.m_Payload[0x1a] << 8)) | packet.m_Payload[0x1b]);
            return InitializerRetVal.SUCCESS;
        }

        public InitializerRetVal IdentifyTagCmd(byte UnitID, out TagID tagID)
        {
            InitializerPacket packet;
            InitializerRetVal val = this.PacketTransaction(UnitID, InitializerPacket.CmdID.IDENTIFY_TAG, out packet);
            tagID = new TagID();
            if (InitializerRetVal.SUCCESS != val)
            {
                return val;
            }
            if (7 == packet.m_Payload.Length)
            {
                if (1 != packet.m_Payload[0])
                {
                    return InitializerRetVal.TAG_TIMEOUT;
                }
                tagID.SetPureRFTagID((uint) ((((packet.m_Payload[1] << 0x18) | (packet.m_Payload[2] << 0x10)) | (packet.m_Payload[3] << 8)) | packet.m_Payload[4]));
                return InitializerRetVal.SUCCESS;
            }
            if (packet.m_Payload.Length == 3)
            {
                tagID.SetPureRFTagID((uint) (((packet.m_Payload[0] << 0x10) | (packet.m_Payload[1] << 8)) | packet.m_Payload[2]));
                return InitializerRetVal.SUCCESS;
            }
            return InitializerRetVal.PROTOCOL_ERROR;
        }

        public InitializerRetVal LF_Command(byte UnitID, TagID tagID, byte CMD, byte Value)
        {
            InitializerPacket packet;
            byte[] data = new byte[5];
            uint pureRFTagID = tagID.GetPureRFTagID();
            data[0] = 0x19;
            data[1] = (byte) (pureRFTagID >> 8);
            data[2] = (byte) pureRFTagID;
            data[3] = CMD;
            data[4] = Value;
            data = InitializerPacket.LFCRC(data);
            InitializerRetVal val = this.PacketTransaction(UnitID, InitializerPacket.CmdID.LF_COMMAND, data, out packet);
            if (InitializerRetVal.SUCCESS != val)
            {
                return val;
            }
            if (1 == packet.m_Payload.Length)
            {
                switch (packet.m_Payload[0])
                {
                    case 1:
                        return InitializerRetVal.SUCCESS;

                    case 2:
                        return InitializerRetVal.TAG_NAC;

                    case 5:
                        return InitializerRetVal.TAG_TIMEOUT;
                }
            }
            return InitializerRetVal.PROTOCOL_ERROR;
        }

        private InitializerRetVal PacketTransaction(byte UnitID, InitializerPacket.CmdID CmdID, out InitializerPacket replyPacket)
        {
            return this.PacketTransaction(UnitID, CmdID, new byte[0], out replyPacket);
        }

        private InitializerRetVal PacketTransaction(byte UnitID, InitializerPacket.CmdID CmdID, byte[] payload, out InitializerPacket replyPacket)
        {
            byte[] buffer = new InitializerPacket(UnitID, CmdID, payload).Pack();
            byte[] buf = new byte[5];
            int num = 0;
            replyPacket = new InitializerPacket();
            try
            {
                this.SerialFlashBuffers();
                this.Conn.Write(buffer, 0, buffer.Length);
            }
            catch
            {
                return InitializerRetVal.LOOP_COMM_ERROR;
            }
            InitializerRetVal val = SerialReadAll(this.Conn, buf, buf.Length);
            if (InitializerRetVal.SUCCESS != val)
            {
                return val;
            }
            if ((0x12 != buf[0]) || (0x12 != buf[1]))
            {
                return InitializerRetVal.BAD_SYNC;
            }
            num = buf[3] - 1;
            byte[] buffer3 = new byte[num];
            val = SerialReadAll(this.Conn, buffer3, buffer3.Length);
            if (InitializerRetVal.SUCCESS != val)
            {
                return val;
            }
            buffer = new byte[buf.Length + buffer3.Length];
            buf.CopyTo(buffer, 0);
            buffer3.CopyTo(buffer, buf.Length);
            return replyPacket.Unpack(buffer);
        }

        public InitializerRetVal Send_RAW_Data(byte UnitID, int PacketType, byte Command, byte[] RAW_DATA, out byte[] Replay)
        {
            Replay = null;
            this.SerialFlashBuffers();
            switch (PacketType)
            {
                case 0:
                    try
                    {
                        this.Conn.Write(RAW_DATA, 0, RAW_DATA.Length);
                    }
                    catch
                    {
                        return InitializerRetVal.LOOP_COMM_ERROR;
                    }
                    Thread.Sleep(700);
                    Thread.Sleep(0);
                    if (this.Conn.BytesToRead > 0)
                    {
                        Replay = new byte[this.Conn.BytesToRead];
                        this.Conn.Read(Replay, 0, this.Conn.BytesToRead);
                        break;
                    }
                    return InitializerRetVal.LOOP_COMM_ERROR;

                case 1:
                {
                    InitializerPacket packet;
                    RAW_DATA = InitializerPacket.LFCRC(RAW_DATA);
                    InitializerRetVal val = this.PacketTransaction(UnitID, (InitializerPacket.CmdID) Command, RAW_DATA, out packet);
                    if (InitializerRetVal.SUCCESS == val)
                    {
                        Replay = packet.m_Payload;
                        break;
                    }
                    return val;
                }
                default:
                    return InitializerRetVal.BAD_PARAMS;
            }
            return InitializerRetVal.SUCCESS;
        }

        private void SerialFlashBuffers()
        {
            if (this.Conn.BytesToRead > 0)
            {
                byte[] buffer = new byte[this.Conn.BytesToRead];
                this.Conn.Read(buffer, 0, this.Conn.BytesToRead);
            }
        }

        private static InitializerRetVal SerialReadAll(SerialPort Conn, byte[] buf, int len)
        {
            int offset = 0;
            int num2 = 0;
            while (offset < len)
            {
                try
                {
                    num2 = Conn.Read(buf, offset, len - offset);
                }
                catch (TimeoutException)
                {
                    return InitializerRetVal.LOOP_TIMEOUT;
                }
                catch
                {
                    return InitializerRetVal.LOOP_COMM_ERROR;
                }
                offset += num2;
            }
            return InitializerRetVal.SUCCESS;
        }

        public InitializerRetVal Set_Tag_ID(byte UnitID, TagID tagID, TagID NewTagID)
        {
            InitializerPacket packet;
            byte[] data = new byte[7];
            uint pureRFTagID = tagID.GetPureRFTagID();
            uint num2 = NewTagID.GetPureRFTagID();
            data[0] = 0x17;
            data[1] = (byte) (pureRFTagID >> 8);
            data[2] = (byte) pureRFTagID;
            data[3] = (byte) (num2 >> 0x18);
            data[4] = (byte) (num2 >> 0x10);
            data[5] = (byte) (num2 >> 8);
            data[6] = (byte) num2;
            data = InitializerPacket.LFCRC(data);
            InitializerRetVal val = this.PacketTransaction(UnitID, InitializerPacket.CmdID.SET_TAG_ID, data, out packet);
            if (InitializerRetVal.SUCCESS != val)
            {
                return val;
            }
            if (1 == packet.m_Payload.Length)
            {
                switch (packet.m_Payload[0])
                {
                    case 1:
                        return InitializerRetVal.SUCCESS;

                    case 2:
                        return InitializerRetVal.TAG_NAC;

                    case 5:
                        return InitializerRetVal.TAG_TIMEOUT;
                }
            }
            return InitializerRetVal.PROTOCOL_ERROR;
        }

        public InitializerRetVal Set_Tag_Serial_Number(byte UnitID, TagID tagID, byte[] TagSN)
        {
            InitializerPacket packet;
            if (TagSN.Length != 6)
            {
                return InitializerRetVal.BAD_PARAMS;
            }
            byte[] data = new byte[9];
            uint pureRFTagID = tagID.GetPureRFTagID();
            data[0] = 0x1a;
            data[1] = (byte) (pureRFTagID >> 8);
            data[2] = (byte) pureRFTagID;
            data[3] = TagSN[0];
            data[4] = TagSN[1];
            data[5] = TagSN[2];
            data[6] = TagSN[3];
            data[7] = TagSN[4];
            data[8] = TagSN[5];
            data = InitializerPacket.LFCRC(data);
            InitializerRetVal val = this.PacketTransaction(UnitID, InitializerPacket.CmdID.LF_COMMAND, data, out packet);
            if (InitializerRetVal.SUCCESS != val)
            {
                return val;
            }
            if (1 == packet.m_Payload.Length)
            {
                switch (packet.m_Payload[0])
                {
                    case 1:
                        return InitializerRetVal.SUCCESS;

                    case 2:
                        return InitializerRetVal.TAG_NAC;

                    case 5:
                        return InitializerRetVal.TAG_TIMEOUT;
                }
            }
            return InitializerRetVal.PROTOCOL_ERROR;
        }

        public InitializerRetVal SetAntennaGain(byte unitID, AntennaGain antennaGain)
        {
            InitializerPacket packet;
            byte[] payload = new byte[] { (byte)antennaGain };
            InitializerRetVal val = this.PacketTransaction(unitID, InitializerPacket.CmdID.GET_SET_ANTENNA_GAIN, payload, out packet);
            if (InitializerRetVal.SUCCESS != val)
            {
                return val;
            }
            if (1 != packet.m_Payload.Length)
            {
                return InitializerRetVal.PROTOCOL_ERROR;
            }
            if (payload[0] != packet.m_Payload[0])
            {
                return InitializerRetVal.BAD_PARAMS;
            }
            if( (byte)antennaGain != packet.m_Payload[0] )
            {
                return InitializerRetVal.ERROR;
            }
            return InitializerRetVal.SUCCESS;
        }

        public InitializerRetVal SetInitializerRFBaudRate(byte unitID, Tag_Settings.TransmissionBaudrates RFBaud)
        {
            InitializerPacket packet;
            byte[] payload = new byte[] { (byte) RFBaud };
            InitializerRetVal val = this.PacketTransaction(unitID, InitializerPacket.CmdID.RFBaudRate, payload, out packet);
            if (InitializerRetVal.SUCCESS == val)
            {
                if (1 != packet.m_Payload.Length)
                {
                    return InitializerRetVal.PROTOCOL_ERROR;
                }
                if (payload[0] != packet.m_Payload[0])
                {
                    return InitializerRetVal.BAD_PARAMS;
                }
            }
            return val;
        }

        public InitializerRetVal SetModeBootloader(byte unitID)
        {
            InitializerPacket packet;
            byte[] payload = new byte[] { 2 };
            InitializerRetVal val = this.PacketTransaction(unitID, InitializerPacket.CmdID.SET_BOOT_MODE, payload, out packet);
            if (InitializerRetVal.SUCCESS != val)
            {
                return val;
            }
            if (1 == packet.m_Payload.Length)
            {
                switch (packet.m_Payload[0])
                {
                    case 1:
                        return InitializerRetVal.ERROR;

                    case 2:
                        return InitializerRetVal.SUCCESS;

                    case 3:
                        return InitializerRetVal.ERROR;
                }
            }
            return InitializerRetVal.PROTOCOL_ERROR;
        }

        public InitializerRetVal SetModeFirmware(byte unitID)
        {
            InitializerPacket packet;
            byte[] payload = new byte[] { 1 };
            InitializerRetVal val = this.PacketTransaction(unitID, InitializerPacket.CmdID.SET_BOOT_MODE, payload, out packet);
            if (InitializerRetVal.SUCCESS != val)
            {
                return val;
            }
            if (1 == packet.m_Payload.Length)
            {
                switch (packet.m_Payload[0])
                {
                    case 1:
                        return InitializerRetVal.SUCCESS;

                    case 2:
                        return InitializerRetVal.ERROR;

                    case 3:
                        return InitializerRetVal.ERROR;
                }
            }
            return InitializerRetVal.PROTOCOL_ERROR;
        }

        public InitializerRetVal SetModulation(byte unitID, ref ModulationParameter Modulation)
        {
            InitializerPacket packet;
            byte[] payload = new byte[] { (byte)Modulation };
            InitializerRetVal val = this.PacketTransaction(unitID, InitializerPacket.CmdID.GET_SET_MODULATION, payload, out packet);
            if (InitializerRetVal.SUCCESS != val)
            {
                return val;
            }
            if (1 != packet.m_Payload.Length)
            {
                return InitializerRetVal.PROTOCOL_ERROR;
            }
            if (payload[0] != packet.m_Payload[0])
            {
                return InitializerRetVal.BAD_PARAMS;
            }
            return InitializerRetVal.SUCCESS;
        }

        public InitializerRetVal SetRFChipSettings(byte unitID, byte[] Data)
        {
            InitializerPacket packet;
            int length = Data.Length;
            InitializerRetVal val = this.PacketTransaction(unitID, InitializerPacket.CmdID.SETRFCONFIG, Data, out packet);
            if (InitializerRetVal.SUCCESS != val)
            {
                return val;
            }
            if (1 != packet.m_Payload.Length)
            {
                return InitializerRetVal.PROTOCOL_ERROR;
            }
            if (packet.m_Payload[0] != 0)
            {
                return InitializerRetVal.ERROR;
            }
            return InitializerRetVal.SUCCESS;
        }

        public InitializerRetVal SetTagParameter(byte UnitID, TagID tagID, TagParameterComand CMD, byte Parameter)
        {
            return this.LF_Command(UnitID, tagID, (byte) CMD, Parameter);
        }

        public InitializerRetVal SetTagParameter(byte UnitID, TagID tagID, TagParameterComand CMD, byte Parameter, int Retrys)
        {
            InitializerRetVal val = InitializerRetVal.TAG_TIMEOUT;
            do
            {
                val = this.SetTagParameter(UnitID, tagID, CMD, Parameter);
            }
            while ((val != InitializerRetVal.SUCCESS) && (Retrys-- > 0));
            return val;
        }

        public InitializerRetVal SetTagRFBaudRate(byte UnitID, TagID tagID, Tag_Settings.TransmissionBaudrates Baud)
        {
            return this.LF_Command(UnitID, tagID, 40, (byte) Baud);
        }

        public InitializerRetVal SetTagSettings(byte UnitID, TagID tagID, Tag_Settings TagSettings, int Retrys)
        {
            int num;
            InitializerRetVal sUCCESS = InitializerRetVal.SUCCESS;
            if (TagSettings.Alarm_Period != 0x7fffffff)
            {
                num = TagSettings.Alarm_Period;
                if (num > 0)
                {
                    num--;
                }
                sUCCESS = this.SetTagParameter(UnitID, tagID, TagParameterComand.Alarm_Period, (byte) num, Retrys);
                if (sUCCESS != InitializerRetVal.SUCCESS)
                {
                    return sUCCESS;
                }
                Thread.Sleep(this.TransmisionDelay);
            }
            if (TagSettings.Alarm_Timer_Multiplier != 0x7fffffff)
            {
                num = TagSettings.Alarm_Timer_Multiplier;
                if (num > 0)
                {
                    num--;
                }
                sUCCESS = this.SetTagParameter(UnitID, tagID, TagParameterComand.Alarm_Timer_Multiplier, (byte) num, Retrys);
                if (sUCCESS != InitializerRetVal.SUCCESS)
                {
                    return sUCCESS;
                }
                Thread.Sleep(this.TransmisionDelay);
            }
            if (TagSettings.Motion_Threshold != 0x7fffffff)
            {
                sUCCESS = this.SetTagParameter(UnitID, tagID, TagParameterComand.Motion_Threshold, (byte) TagSettings.Motion_Threshold, Retrys);
                if (sUCCESS != InitializerRetVal.SUCCESS)
                {
                    return sUCCESS;
                }
                Thread.Sleep(this.TransmisionDelay);
            }
            if (TagSettings.Motion_Sensor_Activator_State != Tag_Settings.Motion_Sensor_Activator_States.Do_not_modify)
            {
                sUCCESS = this.SetTagParameter(UnitID, tagID, TagParameterComand.Motion_Sensor_Activator, (byte) TagSettings.Motion_Sensor_Activator_State, Retrys);
                if (sUCCESS != InitializerRetVal.SUCCESS)
                {
                    return sUCCESS;
                }
                Thread.Sleep(this.TransmisionDelay);
            }
            if (TagSettings.Periodic_Timer_Multiplier != 0x7fffffff)
            {
                int num2 = Retrys;
                do
                {
                    sUCCESS = this.CoilCmd_SetTimerMultiplier(UnitID, tagID, TimerType.Periodic_Timer, (byte) TagSettings.Periodic_Timer_Multiplier);
                }
                while ((sUCCESS != InitializerRetVal.SUCCESS) && (num2-- > 0));
                if (sUCCESS != InitializerRetVal.SUCCESS)
                {
                    return sUCCESS;
                }
                Thread.Sleep(this.TransmisionDelay);
            }
            if (TagSettings.Tag_State != Tag_Settings.Tag_States.Do_not_modify)
            {
                sUCCESS = this.SetTagParameter(UnitID, tagID, TagParameterComand.ON_OFF, (byte) TagSettings.Tag_State, Retrys);
                if (sUCCESS != InitializerRetVal.SUCCESS)
                {
                    return sUCCESS;
                }
                Thread.Sleep(this.TransmisionDelay);
            }
            if (TagSettings.Tamper_Panic_Activator_State != Tag_Settings.Tamper_Panic_Activator_States.Do_not_modify)
            {
                sUCCESS = this.SetTagParameter(UnitID, tagID, TagParameterComand.Tamper_Panic_Activator, (byte) TagSettings.Tamper_Panic_Activator_State, Retrys);
                if (sUCCESS != InitializerRetVal.SUCCESS)
                {
                    return sUCCESS;
                }
                Thread.Sleep(this.TransmisionDelay);
            }
            if (TagSettings.Tamper_Type_State != Tag_Settings.Tamper_Type_States.Do_not_modify)
            {
                sUCCESS = this.SetTagParameter(UnitID, tagID, TagParameterComand.Tamper_Type, (byte) TagSettings.Tamper_Type_State, Retrys);
                if (sUCCESS != InitializerRetVal.SUCCESS)
                {
                    return sUCCESS;
                }
                Thread.Sleep(this.TransmisionDelay);
            }
            if (TagSettings.Transmission_Type != Tag_Settings.Transmission_Types.Do_not_modify)
            {
                sUCCESS = this.SetTagParameter(UnitID, tagID, TagParameterComand.TRANSMISSION_TYPE, (byte) TagSettings.Transmission_Type, Retrys);
                if (sUCCESS != InitializerRetVal.SUCCESS)
                {
                    return sUCCESS;
                }
                Thread.Sleep(this.TransmisionDelay);
            }
            if (TagSettings.Tag_State != Tag_Settings.Tag_States.Do_not_modify)
            {
                sUCCESS = this.SetTagParameter(UnitID, tagID, TagParameterComand.ON_OFF, (byte) TagSettings.Tag_State, Retrys);
                if (sUCCESS != InitializerRetVal.SUCCESS)
                {
                    return sUCCESS;
                }
                Thread.Sleep(this.TransmisionDelay);
            }
            if (TagSettings.TransmissionBaudrate != Tag_Settings.TransmissionBaudrates.Do_not_modify)
            {
                sUCCESS = this.SetTagParameter(UnitID, tagID, TagParameterComand.RF_BAUDRATE_ADDR, (byte) TagSettings.TransmissionBaudrate, Retrys);
                if (sUCCESS != InitializerRetVal.SUCCESS)
                {
                    return sUCCESS;
                }
                Thread.Sleep(this.TransmisionDelay);
            }
            return sUCCESS;
        }

        public enum AntennaGain : byte
        {
            HIGH = 1,
            INVALID = 0,
            LOW = 2
        }

        public delegate void EventCallback(byte[] Send_Data, byte[] Resived_Data);

        public enum ModulationParameter : byte
        {
            INVALID = 0,
            ModASK = 1,
            ModFSK = 2
        }

        public class Tag
        {
            public byte activatorNum;
            public ushort NoiseLevel;
            public ushort RSSI;
            public const byte TAG_WITH_TS_PAYLOAD_LEN = 14;
            public const byte TAG_WITHOUT_TS_PAYLOAD_LEN = 10;
            public TagID tagID;
            public Initializer.TagMsg tagMsg;
            public byte transmissionIndex;
            public Initializer.Time ts;

            public bool Unpack(byte[] buf)
            {
                if ((10 != buf.Length) && (14 != buf.Length))
                {
                    return false;
                }
                this.tagID = new TagID((uint) ((((buf[0] << 0x18) | (buf[1] << 0x10)) | (buf[2] << 8)) | buf[3]));
                this.transmissionIndex = (byte) (buf[4] & 0x1f);
                this.tagMsg = (Initializer.TagMsg) ((byte) (buf[4] >> 5));
                this.activatorNum = buf[5];
                this.RSSI = (ushort) ((buf[6] << 8) | buf[7]);
                this.NoiseLevel = (ushort) ((buf[8] << 8) | buf[9]);
                this.ts = new Initializer.Time();
                if (14 == buf.Length)
                {
                    ushort num = (ushort) ((buf[10] << 8) | buf[11]);
                    ushort num2 = (ushort) ((buf[12] << 8) | buf[13]);
                    this.ts.hour = (byte) (num / 60);
                    this.ts.min = (byte) (num - (this.ts.hour * 60));
                    this.ts.sec = (byte) (num2 / 0x3e8);
                    this.ts.msec = (byte) (num2 - (this.ts.sec * 0x3e8));
                }
                return true;
            }
        }

        public enum TagIDVersion
        {
            INVALID,
            VER_1,
            VER_2
        }

        public enum TagMsg : byte
        {
            MAINTENANCE = 1,
            MOVEMENT = 2,
            NEAR_ACTIVATOR = 5,
            RESERVED_1 = 4,
            RESERVED_3 = 6,
            RESERVED_4 = 7,
            TORN_WIRE = 3
        }

        public enum TagParameterComand
        {
            ACTIVATOR = 0x22,
            ACTIVATOR_MULT_ADDR = 0x24,
            ACTIVATOR_OFF_ADDR = 0x27,
            ACTIVATOR_ON_ADDR = 0x26,
            ACTIVATOR_PERIOD_ADDR = 0x25,
            ACTIVATOR_SLOT_MASK = 0x23,
            Alarm_Period = 7,
            Alarm_Timer_Multiplier = 6,
            Motion_Sensor_Activator = 9,
            Motion_Threshold = 11,
            ON_OFF = 4,
            Periodic_Timer_Multiplier = 5,
            RF_BAUDRATE_ADDR = 40,
            RFTEST = 3,
            TAG_ID_ADDR = 0x10,
            TAG_SN_ADDR = 0x18,
            Tamper_Panic_Activator = 8,
            Tamper_Type = 10,
            TRANSMISSION_TYPE = 12
        }

        public class Time
        {
            public byte hour;
            public byte min;
            public short msec;
            public byte sec;

            public static bool operator ==(Initializer.Time t1, Initializer.Time t2)
            {
                return ((((t1.hour == t2.hour) && (t1.min == t2.min)) && (t1.sec == t2.sec)) && (t1.msec == t2.msec));
            }

            public static bool operator !=(Initializer.Time t1, Initializer.Time t2)
            {
                return !(t1 == t2);
            }

            public byte[] Pack()
            {
                byte[] buffer = new byte[4];
                ushort num = (ushort) ((this.hour * 60) + this.min);
                ushort num2 = (ushort) ((this.sec * 0x3e8) + this.msec);
                buffer[0] = (byte) (num >> 8);
                buffer[1] = (byte) (num & 0xff);
                buffer[2] = (byte) (num2 >> 8);
                buffer[3] = (byte) (num2 & 0xff);
                return buffer;
            }

            public override string ToString()
            {
                return string.Format("{0:00}:{1:00}:{2:00}:{3:0000}", new object[] { this.hour, this.min, this.sec, this.msec });
            }

            public bool Unpack(byte[] buf)
            {
                ushort num = 0;
                ushort num2 = 0;
                if (4 != buf.Length)
                {
                    return false;
                }
                num = (ushort) ((buf[0] << 8) | buf[1]);
                num2 = (ushort) ((buf[2] << 8) | buf[3]);
                this.hour = (byte) (num / 60);
                this.min = (byte) (num - (this.hour * 60));
                this.sec = (byte) (num2 / 0x3e8);
                this.msec = (byte) (num2 - (this.sec * 0x3e8));
                return true;
            }
        }

        public enum TimerType
        {
            Alarm_Period = 7,
            Alarm_Timer = 6,
            INVALID_TIMER_MODE = 0,
            Periodic_Timer = 5
        }

        public class UnitInfo
        {
            public byte Device_Class;
            public byte Device_SubClass;
            public byte Firmware_Version;
            public byte protocol_version;
            public string Unit_Name;
            public uint Unit_Serial_Number;
        }
    }
}

