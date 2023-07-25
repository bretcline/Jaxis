namespace PureRF
{
    using System;
    using System.Collections;
    using System.Runtime.InteropServices;
    using System.Text;

    public class Receiver
    {
        public const int BOOTLOADER_BAUDRATE = 0xe100;
        public int DebugTagCount;
        public int DebugTagCount1;
        public int DebugTagCount2;
        public int DebugTagCount3;
        public const int FIRMWARE_BAUDRATE = 0xe100;
        public const byte FIRMWARE_PACKET_LEN = 0x10;
        public const byte MAX_TAGS_TO_FETCH = 8;
        private ReceiverBusConnection mConn;
        private byte TransactionNumber;

        public Receiver(ReceiverBusConnection Conn)
        {
            this.mConn = Conn;
        }

        public ReceiverRetVal ActivateRelay(byte unitID, ActivateRelayParameter RelayParameter)
        {
            ReceiverPacket packet;
            byte[] payload = new byte[] { (byte) RelayParameter.Relay, (byte) RelayParameter.Interval };
            ReceiverRetVal val = this.PacketTransaction(unitID, ReceiverPacket.CmdID.ACTIVATE_RELAY, payload, out packet);
            if (ReceiverRetVal.SUCCESS != val)
            {
                return val;
            }
            if (1 != packet.m_Payload.Length)
            {
                return ReceiverRetVal.PROTOCOL_ERROR;
            }
            return ReceiverRetVal.SUCCESS;
        }

        public static string AntennaGainToString(AntennaGain antennaGain)
        {
            switch (antennaGain)
            {
                case AntennaGain.MAX:
                    return "Maximal";

                case AntennaGain.MINUS_6DB:
                    return "-6dB";

                case AntennaGain.MINUS_14DB:
                    return "-14dB";

                case AntennaGain.MINUS_20DB:
                    return "-20dB";
            }
            return "???";
        }

        public ReceiverRetVal Download(byte unitID, ushort packet_num, byte[] firmwarePacket)
        {
            byte[] array = new byte[0x12];
            if (0x10 != firmwarePacket.Length)
            {
                return ReceiverRetVal.BAD_PARAMS;
            }
            array[0] = (byte) (packet_num >> 8);
            array[1] = (byte) (packet_num & 0xff);
            firmwarePacket.CopyTo(array, 2);
            byte[] buffer = new ReceiverPacket(unitID, ReceiverPacket.CmdID.DOWNLOAD, array).Pack();
            try
            {
                this.mConn.Write(buffer, buffer.Length);
            }
            catch
            {
                return ReceiverRetVal.LOOP_COMM_ERROR;
            }
            return ReceiverRetVal.SUCCESS;
        }

        public ReceiverRetVal FirmwareChecksum(byte unitID, out ushort flash_cksum)
        {
            ReceiverPacket packet;
            ReceiverRetVal val = this.PacketTransaction(unitID, ReceiverPacket.CmdID.GET_FIRMWARE_CHECKSUM, out packet);
            flash_cksum = 0;
            if (ReceiverRetVal.SUCCESS != val)
            {
                return val;
            }
            if (11 == packet.m_Payload.Length)
            {
                flash_cksum = (ushort) ((packet.m_Payload[0] << 8) | packet.m_Payload[1]);
                switch (packet.m_Payload[2])
                {
                    case 0:
                        return ReceiverRetVal.FLASH_EMPTY;

                    case 1:
                        return ReceiverRetVal.FLASH_DAMAGED;

                    case 2:
                        return ReceiverRetVal.FLASH_OK;
                }
            }
            return ReceiverRetVal.PROTOCOL_ERROR;
        }

        public ReceiverRetVal FlushTagBuffer(byte unitID)
        {
            ReceiverPacket packet;
            ReceiverRetVal val = this.PacketTransaction(unitID, ReceiverPacket.CmdID.FLUSH_TAG_BUFFER, out packet);
            if (ReceiverRetVal.SUCCESS != val)
            {
                return val;
            }
            if (1 == packet.m_Payload.Length)
            {
                switch (packet.m_Payload[0])
                {
                    case 0:
                        return ReceiverRetVal.ERROR;

                    case 1:
                        return ReceiverRetVal.SUCCESS;
                }
            }
            return ReceiverRetVal.PROTOCOL_ERROR;
        }

        public ReceiverRetVal Get_RSSI_Threshold(byte unitID, out RSSIFilterParameter RSSI_threshold)
        {
            ReceiverPacket packet;
            byte[] payload = new byte[3];
            RSSI_threshold = new RSSIFilterParameter();
            payload[0] = 1;
            payload[1] = 0;
            payload[2] = 0;
            ReceiverRetVal val = this.PacketTransaction(unitID, ReceiverPacket.CmdID.GET_SET_RSSI, payload, out packet);
            if (ReceiverRetVal.SUCCESS != val)
            {
                return val;
            }
            if (2 != packet.m_Payload.Length)
            {
                return ReceiverRetVal.PROTOCOL_ERROR;
            }
            RSSI_threshold.RSSIFilter = (packet.m_Payload[0] << 8) + packet.m_Payload[1];
            return ReceiverRetVal.SUCCESS;
        }

        public ReceiverRetVal GetAllReceiverInfo(byte unitID, out AllReceiverInfo AllInfo)
        {
            AllInfo = new AllReceiverInfo();
            ReceiverRetVal unitInfo = this.GetUnitInfo(unitID, out AllInfo.UnitInfo);
            if (unitInfo != ReceiverRetVal.SUCCESS)
            {
                return unitInfo;
            }
            unitInfo = this.GetUnitStatus(unitID, out AllInfo.UnitStatus);
            if (unitInfo != ReceiverRetVal.SUCCESS)
            {
                return unitInfo;
            }
            unitInfo = this.GetNoiseLevel(unitID, out AllInfo.NoiseLevel);
            if (unitInfo != ReceiverRetVal.SUCCESS)
            {
                return unitInfo;
            }
            unitInfo = this.GetPowerControl(unitID, out AllInfo.PowerControl);
            if (unitInfo != ReceiverRetVal.SUCCESS)
            {
                return unitInfo;
            }
            return ReceiverRetVal.SUCCESS;
        }

        public ReceiverRetVal GetAllTags(byte unitID, out Tag[] tags)
        {
            return this.GetAllTags(unitID, out tags, false);
        }

        public ReceiverRetVal GetAllTags(byte unitID, out Tag[] allTags, bool withTS)
        {
            Tag[] tagArray;
            int num2;
            new Random();
            ArrayList list = new ArrayList();
            int num = 0;
            int tagsInBuffer = 0;
            allTags = new Tag[0];
            ReceiverRetVal val = this.GetTags(unitID, this.TransactionNumber, 8, out tagArray, out tagsInBuffer, withTS);
            if (val != ReceiverRetVal.SUCCESS)
            {
                return val;
            }
            this.TransactionNumber = (byte) (this.TransactionNumber + 1);
            list.Add(tagArray);
            if (tagsInBuffer != 0)
            {
                do
                {
                    val = this.GetTags(unitID, this.TransactionNumber, 8, out tagArray, out tagsInBuffer, withTS);
                    if (val != ReceiverRetVal.SUCCESS)
                    {
                        return val;
                    }
                    this.TransactionNumber = (byte) (this.TransactionNumber + 1);
                    if (tagArray.Length > 0)
                    {
                        list.Add(tagArray);
                    }
                }
                while ((tagArray.Length != 0) && (tagsInBuffer != 0));
            }
            num = 0;
            for (num2 = 0; num2 < list.Count; num2++)
            {
                num += ((Tag[]) list[num2]).Length;
            }
            allTags = new Tag[num];
            int index = 0;
            for (num2 = 0; num2 < list.Count; num2++)
            {
                foreach (Tag tag in (Tag[]) list[num2])
                {
                    allTags[index] = tag;
                    index++;
                }
            }
            this.DebugTagCount += num;
            return ReceiverRetVal.SUCCESS;
        }

        public ReceiverRetVal GetAllTagsWithTS(byte unitID, out Tag[] tags)
        {
            return this.GetAllTags(unitID, out tags, true);
        }

        public ReceiverRetVal GetAntennaGain(byte unitID, out AntennaGain antennaGain)
        {
            ReceiverPacket packet;
            byte[] payload = new byte[1];
            ReceiverRetVal val = this.PacketTransaction(unitID, ReceiverPacket.CmdID.GET_SET_ANTENNA_GAIN, payload, out packet);
            antennaGain = AntennaGain.INVALID;
            if (ReceiverRetVal.SUCCESS != val)
            {
                return val;
            }
            if (1 == packet.m_Payload.Length)
            {
                switch (packet.m_Payload[0])
                {
                    case 1:
                        antennaGain = AntennaGain.MAX;
                        goto Label_0067;

                    case 2:
                        antennaGain = AntennaGain.MINUS_6DB;
                        goto Label_0067;

                    case 3:
                        antennaGain = AntennaGain.MINUS_14DB;
                        goto Label_0067;

                    case 4:
                        antennaGain = AntennaGain.MINUS_20DB;
                        goto Label_0067;
                }
            }
            return ReceiverRetVal.PROTOCOL_ERROR;
        Label_0067:
            return ReceiverRetVal.SUCCESS;
        }

        public ReceiverRetVal GetMode(byte unitID)
        {
            ReceiverPacket packet;
            byte[] payload = new byte[] { 0xff };
            ReceiverRetVal val = this.PacketTransaction(unitID, ReceiverPacket.CmdID.GET_SET_MODE, payload, out packet);
            if (ReceiverRetVal.SUCCESS != val)
            {
                return val;
            }
            if (1 == packet.m_Payload.Length)
            {
                switch (packet.m_Payload[0])
                {
                    case 1:
                        return ReceiverRetVal.MODE_BOOTLOADER;

                    case 2:
                        return ReceiverRetVal.MODE_FIRMWARE;
                }
            }
            return ReceiverRetVal.PROTOCOL_ERROR;
        }

        public ReceiverRetVal GetModulation(byte unitID, out ModulationParameter Modulation)
        {
            ReceiverPacket packet;
            byte[] payload = new byte[2];
            Modulation = ModulationParameter.INVALID;
            payload[0] = 1;
            payload[1] = 0;
            ReceiverRetVal val = this.PacketTransaction(unitID, ReceiverPacket.CmdID.GET_SET_MODULATION, payload, out packet);
            if (ReceiverRetVal.SUCCESS != val)
            {
                return val;
            }
            if (1 != packet.m_Payload.Length)
            {
                return ReceiverRetVal.PROTOCOL_ERROR;
            }
            try
            {
                Modulation = (ModulationParameter) packet.m_Payload[0];
            }
            catch (Exception)
            {
                return ReceiverRetVal.PROTOCOL_ERROR;
            }
            return ReceiverRetVal.SUCCESS;
        }

        public ReceiverRetVal GetNoiseLevel(byte unitID, out ushort noiseLevel)
        {
            ReceiverPacket packet;
            ReceiverRetVal val = this.PacketTransaction(unitID, ReceiverPacket.CmdID.GET_NOISE_LEVEL, out packet);
            noiseLevel = 0xffff;
            if (ReceiverRetVal.SUCCESS != val)
            {
                return val;
            }
            if (2 != packet.m_Payload.Length)
            {
                return ReceiverRetVal.PROTOCOL_ERROR;
            }
            noiseLevel = (ushort) ((packet.m_Payload[0] << 8) | packet.m_Payload[1]);
            return ReceiverRetVal.SUCCESS;
        }

        public ReceiverRetVal GetPowerControl(byte unitID, out Power_Control PowerControl)
        {
            ReceiverPacket packet;
            PowerControl = new Power_Control();
            byte[] payload = new byte[] { 4 };
            ReceiverRetVal val = this.PacketTransaction(unitID, ReceiverPacket.CmdID.GET_SET_POWER_CONTROL, payload, out packet);
            if (ReceiverRetVal.SUCCESS != val)
            {
                return val;
            }
            if (3 != packet.m_Payload.Length)
            {
                return ReceiverRetVal.PROTOCOL_ERROR;
            }
            PowerControl.Power_Mode = packet.m_Payload[0];
            PowerControl.Input_Voltage = (ushort) ((packet.m_Payload[1] << 8) | packet.m_Payload[2]);
            return ReceiverRetVal.SUCCESS;
        }

        public ReceiverRetVal GetRFBaudRate(byte unitID, out RFBaudRates RFBaudRate)
        {
            ReceiverPacket packet;
            byte[] payload = new byte[2];
            RFBaudRate = RFBaudRates.INVALID;
            payload[0] = 1;
            payload[1] = 0;
            ReceiverRetVal val = this.PacketTransaction(unitID, ReceiverPacket.CmdID.GET_SET_RF_BAUDRATE, payload, out packet);
            if (ReceiverRetVal.SUCCESS != val)
            {
                return val;
            }
            if (1 != packet.m_Payload.Length)
            {
                return ReceiverRetVal.PROTOCOL_ERROR;
            }
            try
            {
                RFBaudRate = (RFBaudRates) packet.m_Payload[0];
            }
            catch (Exception)
            {
                return ReceiverRetVal.PROTOCOL_ERROR;
            }
            return ReceiverRetVal.SUCCESS;
        }

        public ReceiverRetVal GetSerialNum(byte unitID, out SerialNumParameter SerialNum)
        {
            ReceiverPacket packet;
            byte[] payload = new byte[5];
            SerialNum = new SerialNumParameter();
            payload[0] = 1;
            payload[1] = 0;
            payload[2] = 0;
            payload[3] = 0;
            payload[4] = 0;
            ReceiverRetVal val = this.PacketTransaction(unitID, ReceiverPacket.CmdID.GET_SET_SERIAL_NUM, payload, out packet);
            if (ReceiverRetVal.SUCCESS != val)
            {
                return val;
            }
            if (4 != packet.m_Payload.Length)
            {
                return ReceiverRetVal.PROTOCOL_ERROR;
            }
            SerialNum.Serial = (uint) ((((packet.m_Payload[0] << 0x18) + (packet.m_Payload[1] << 0x10)) + (packet.m_Payload[2] << 8)) + packet.m_Payload[3]);
            return ReceiverRetVal.SUCCESS;
        }

        public ReceiverRetVal GetSiteCode(byte unitID, out SiteCodeParameter SiteCode)
        {
            ReceiverPacket packet;
            byte[] payload = new byte[2];
            SiteCode = new SiteCodeParameter();
            payload[0] = 1;
            payload[1] = 0;
            ReceiverRetVal val = this.PacketTransaction(unitID, ReceiverPacket.CmdID.GET_SET_SITECODE, payload, out packet);
            if (ReceiverRetVal.SUCCESS != val)
            {
                return val;
            }
            if (1 != packet.m_Payload.Length)
            {
                return ReceiverRetVal.PROTOCOL_ERROR;
            }
            SiteCode.SiteCode = packet.m_Payload[0];
            return ReceiverRetVal.SUCCESS;
        }

        public ReceiverRetVal GetTags(byte unitID, byte transactionID, byte maxTagsCount, out Tag[] tags, out int TagsInBuffer)
        {
            return this.GetTags(unitID, transactionID, maxTagsCount, out tags, out TagsInBuffer, false);
        }

        public ReceiverRetVal GetTags(byte unitID, byte transactionID, byte maxTagsCount, out Tag[] tags, out int TagsInBuffer, bool withTS)
        {
            ReceiverPacket packet;
            TagsInBuffer = 0;
            byte num = 0;
            byte[] payload = new byte[] { maxTagsCount, transactionID };
            int count = 10;
            ReceiverRetVal val = this.PacketTransaction(unitID, ReceiverPacket.CmdID.GET_TAGS, payload, out packet);
            tags = new Tag[0];
            if (ReceiverRetVal.SUCCESS != val)
            {
                return val;
            }
            if ((3 > packet.m_Payload.Length) || (transactionID != packet.m_Payload[1]))
            {
                return ReceiverRetVal.PROTOCOL_ERROR;
            }
            num = packet.m_Payload[2];
            TagsInBuffer = packet.m_Payload[3];
            this.DebugTagCount1 += num;
            tags = new Tag[num];
            for (int i = 0; i < num; i++)
            {
                try
                {
                    byte[] dst = new byte[count];
                    Buffer.BlockCopy(packet.m_Payload, 4 + (i * count), dst, 0, count);
                    tags[i] = new Tag();
                    if (!tags[i].Unpack(dst))
                    {
                        return ReceiverRetVal.PROTOCOL_ERROR;
                    }
                }
                catch (Exception)
                {
                    return ReceiverRetVal.ERROR;
                }
            }
            this.DebugTagCount2 += tags.Length;
            return ReceiverRetVal.SUCCESS;
        }

        public ReceiverRetVal GetTagsWithTS(byte unitID, byte transactionID, byte maxTagsCount, out Tag[] tags, out int TagsInBuffer)
        {
            return this.GetTags(unitID, transactionID, maxTagsCount, out tags, out TagsInBuffer, true);
        }

        public ReceiverRetVal GetTime(byte unitID, out Time time)
        {
            ReceiverPacket packet;
            byte[] payload = new byte[] { 0xff, 0xff, 0xff, 0xff };
            ReceiverRetVal val = this.PacketTransaction(unitID, ReceiverPacket.CmdID.GET_SET_TIME, payload, out packet);
            time = new Time();
            if (ReceiverRetVal.SUCCESS != val)
            {
                return val;
            }
            if ((4 == packet.m_Payload.Length) && time.Unpack(packet.m_Payload))
            {
                return ReceiverRetVal.SUCCESS;
            }
            return ReceiverRetVal.PROTOCOL_ERROR;
        }

        public ReceiverRetVal GetUnitInfo(byte unitID, out UnitInfo unitInfo)
        {
            ReceiverPacket packet;
            ReceiverRetVal val = this.PacketTransaction(unitID, ReceiverPacket.CmdID.INFO, out packet);
            unitInfo = new UnitInfo();
            if (ReceiverRetVal.SUCCESS != val)
            {
                return val;
            }
            if (packet.m_Payload.Length < 0x1c)
            {
                return ReceiverRetVal.PROTOCOL_ERROR;
            }
            unitInfo.Protocol_Version = packet.m_Payload[0];
            unitInfo.Device_Class = packet.m_Payload[1];
            unitInfo.Device_SubClass = packet.m_Payload[2];
            unitInfo.Firmware_Version = packet.m_Payload[3];
            unitInfo.Unit_Name = Encoding.ASCII.GetString(packet.m_Payload, 4, 20);
            unitInfo.Unit_Serial_Number = (uint) ((((packet.m_Payload[0x18] << 0x18) | (packet.m_Payload[0x19] << 0x10)) | (packet.m_Payload[0x1a] << 8)) | packet.m_Payload[0x1b]);
            if (unitInfo.Unit_Serial_Number == uint.MaxValue)
            {
                unitInfo.Unit_Serial_Number = 0;
            }
            return ReceiverRetVal.SUCCESS;
        }

        public ReceiverRetVal GetUnitName(byte unitID, out UnitNameParameter aName)
        {
            ReceiverPacket packet;
            aName = new UnitNameParameter();
            byte[] payload = new byte[0x15];
            payload[0] = 1;
            ReceiverRetVal val = this.PacketTransaction(unitID, ReceiverPacket.CmdID.GET_SET_NAME, payload, out packet);
            if (ReceiverRetVal.SUCCESS != val)
            {
                return val;
            }
            if (20 != packet.m_Payload.Length)
            {
                return ReceiverRetVal.PROTOCOL_ERROR;
            }
            aName.UnitName = Encoding.ASCII.GetString(packet.m_Payload, 0, packet.m_Payload.Length);
            return ReceiverRetVal.SUCCESS;
        }

        public ReceiverRetVal GetUnitStatus(byte unitID, out UnitStatus unitStatus)
        {
            ReceiverPacket packet;
            ReceiverRetVal val = this.PacketTransaction(unitID, ReceiverPacket.CmdID.STATUS, out packet);
            unitStatus = new UnitStatus();
            if (ReceiverRetVal.SUCCESS != val)
            {
                return val;
            }
            if (packet.m_Payload.Length < 8)
            {
                return ReceiverRetVal.PROTOCOL_ERROR;
            }
            unitStatus.Sub_mode = packet.m_Payload[0];
            unitStatus.Uptime = (((packet.m_Payload[1] << 0x18) | (packet.m_Payload[2] << 0x10)) | (packet.m_Payload[3] << 8)) | packet.m_Payload[4];
            unitStatus.Average_Processor_Workload = packet.m_Payload[5];
            unitStatus.Number_of_Resets = packet.m_Payload[6];
            unitStatus.Tag_messages_in_buffer = packet.m_Payload[7];
            return ReceiverRetVal.SUCCESS;
        }

        private ReceiverRetVal PacketTransaction(byte UnitID, ReceiverPacket.CmdID CmdID, out ReceiverPacket replyPacket)
        {
            return this.PacketTransaction(UnitID, CmdID, new byte[0], out replyPacket);
        }

        private ReceiverRetVal PacketTransaction(byte UnitID, ReceiverPacket.CmdID CmdID, byte[] payload, out ReceiverPacket replyPacket)
        {
            byte[] buffer = new ReceiverPacket(UnitID, CmdID, payload).Pack();
            byte[] buffer2 = new byte[5];
            int num = 0;
            replyPacket = new ReceiverPacket();
            try
            {
                this.mConn.Write(buffer, buffer.Length);
            }
            catch
            {
                return ReceiverRetVal.LOOP_COMM_ERROR;
            }
            ReceiverRetVal val = this.mConn.Read(buffer2, buffer2.Length);
            if (ReceiverRetVal.SUCCESS != val)
            {
                return val;
            }
            if ((0x12 != buffer2[0]) || (0x12 != buffer2[1]))
            {
                return ReceiverRetVal.BAD_SYNC;
            }
            num = buffer2[3] - 1;
            byte[] buffer3 = new byte[num];
            val = this.mConn.Read(buffer3, buffer3.Length);
            if (ReceiverRetVal.SUCCESS != val)
            {
                return val;
            }
            buffer = new byte[buffer2.Length + buffer3.Length];
            buffer2.CopyTo(buffer, 0);
            buffer3.CopyTo(buffer, buffer2.Length);
            return replyPacket.Unpack(buffer);
        }

        public ReceiverRetVal Set_RSSI_Threshold(byte unitID, ref RSSIFilterParameter RSSI_threshold)
        {
            ReceiverPacket packet;
            byte[] payload = new byte[] { 2, (byte) (RSSI_threshold.RSSIFilter >> 8), (byte) (RSSI_threshold.RSSIFilter & 0xff) };
            ReceiverRetVal val = this.PacketTransaction(unitID, ReceiverPacket.CmdID.GET_SET_RSSI, payload, out packet);
            if (ReceiverRetVal.SUCCESS != val)
            {
                return val;
            }
            if (2 != packet.m_Payload.Length)
            {
                return ReceiverRetVal.PROTOCOL_ERROR;
            }
            return ReceiverRetVal.SUCCESS;
        }

        public ReceiverRetVal SetAntennaGain(byte unitID, AntennaGain antennaGain)
        {
            ReceiverPacket packet;
            byte[] payload = new byte[] { (byte)antennaGain };
            ReceiverRetVal val = this.PacketTransaction(unitID, ReceiverPacket.CmdID.GET_SET_ANTENNA_GAIN, payload, out packet);
            if (ReceiverRetVal.SUCCESS != val)
            {
                return val;
            }
            if (1 != packet.m_Payload.Length)
            {
                return ReceiverRetVal.PROTOCOL_ERROR;
            }
            if( (byte)antennaGain != packet.m_Payload[0] )
            {
                return ReceiverRetVal.ERROR;
            }
            return ReceiverRetVal.SUCCESS;
        }

        public ReceiverRetVal SetBootloaderMode(byte unitID)
        {
            ReceiverPacket packet;
            byte[] payload = new byte[] { 2 };
            ReceiverRetVal val = this.PacketTransaction(unitID, ReceiverPacket.CmdID.GET_SET_MODE, payload, out packet);
            if (ReceiverRetVal.SUCCESS != val)
            {
                return val;
            }
            if (1 == packet.m_Payload.Length)
            {
                switch (packet.m_Payload[0])
                {
                    case 1:
                        return ReceiverRetVal.ERROR;

                    case 2:
                        return ReceiverRetVal.SUCCESS;
                }
            }
            return ReceiverRetVal.PROTOCOL_ERROR;
        }

        public ReceiverRetVal SetMainMode(byte unitID)
        {
            ReceiverPacket packet;
            byte[] payload = new byte[] { 1 };
            ReceiverRetVal val = this.PacketTransaction(unitID, ReceiverPacket.CmdID.GET_SET_MODE, payload, out packet);
            if (ReceiverRetVal.SUCCESS != val)
            {
                return val;
            }
            if (1 == packet.m_Payload.Length)
            {
                switch (packet.m_Payload[0])
                {
                    case 1:
                        return ReceiverRetVal.SUCCESS;

                    case 2:
                        return ReceiverRetVal.ERROR;
                }
            }
            return ReceiverRetVal.PROTOCOL_ERROR;
        }

        public ReceiverRetVal SetModulation(byte unitID, ref ModulationParameter Modulation)
        {
            ReceiverPacket packet;
            byte[] payload = new byte[] { 2, (byte)Modulation };
            ReceiverRetVal val = this.PacketTransaction(unitID, ReceiverPacket.CmdID.GET_SET_MODULATION, payload, out packet);
            if (ReceiverRetVal.SUCCESS != val)
            {
                return val;
            }
            if (1 != packet.m_Payload.Length)
            {
                return ReceiverRetVal.PROTOCOL_ERROR;
            }
            return ReceiverRetVal.SUCCESS;
        }

        public ReceiverRetVal SetRFBaudRate(byte unitID, ref RFBaudRates RFBaudRate)
        {
            ReceiverPacket packet;
            byte[] payload = new byte[] { 2, (byte)RFBaudRate };
            ReceiverRetVal val = this.PacketTransaction(unitID, ReceiverPacket.CmdID.GET_SET_RF_BAUDRATE, payload, out packet);
            if (ReceiverRetVal.SUCCESS != val)
            {
                return val;
            }
            if (1 != packet.m_Payload.Length)
            {
                return ReceiverRetVal.PROTOCOL_ERROR;
            }
            return ReceiverRetVal.SUCCESS;
        }

        public ReceiverRetVal SetRFChipSettings(byte unitID, byte[] Data)
        {
            ReceiverPacket packet;
            int length = Data.Length;
            ReceiverRetVal val = this.PacketTransaction(unitID, ReceiverPacket.CmdID.SETRFCONFIG, Data, out packet);
            if (ReceiverRetVal.SUCCESS != val)
            {
                return val;
            }
            if (1 != packet.m_Payload.Length)
            {
                return ReceiverRetVal.PROTOCOL_ERROR;
            }
            if (packet.m_Payload[0] != 0)
            {
                return ReceiverRetVal.ERROR;
            }
            return ReceiverRetVal.SUCCESS;
        }

        public ReceiverRetVal SetSerialNum(byte unitID, ref SerialNumParameter SerialNum)
        {
            ReceiverPacket packet;
            byte[] payload = new byte[] { 2, (byte) (SerialNum.Serial >> 0x18), (byte) (SerialNum.Serial >> 0x10), (byte) (SerialNum.Serial >> 8), (byte) (SerialNum.Serial & 0xff) };
            ReceiverRetVal val = this.PacketTransaction(unitID, ReceiverPacket.CmdID.GET_SET_SERIAL_NUM, payload, out packet);
            if (ReceiverRetVal.SUCCESS != val)
            {
                return val;
            }
            if (4 != packet.m_Payload.Length)
            {
                return ReceiverRetVal.PROTOCOL_ERROR;
            }
            return ReceiverRetVal.SUCCESS;
        }

        public ReceiverRetVal SetSiteCode(byte unitID, ref SiteCodeParameter SiteCode)
        {
            ReceiverPacket packet;
            byte[] payload = new byte[] { 2, (byte) SiteCode.SiteCode };
            ReceiverRetVal val = this.PacketTransaction(unitID, ReceiverPacket.CmdID.GET_SET_SITECODE, payload, out packet);
            if (ReceiverRetVal.SUCCESS != val)
            {
                return val;
            }
            if (1 != packet.m_Payload.Length)
            {
                return ReceiverRetVal.PROTOCOL_ERROR;
            }
            return ReceiverRetVal.SUCCESS;
        }

        public ReceiverRetVal SetTime(byte unitID, Time time)
        {
            ReceiverPacket packet;
            Time time2 = new Time();
            byte[] payload = time.Pack();
            ReceiverRetVal val = this.PacketTransaction(unitID, ReceiverPacket.CmdID.GET_SET_TIME, payload, out packet);
            if (ReceiverRetVal.SUCCESS != val)
            {
                return val;
            }
            if ((4 != packet.m_Payload.Length) || !time2.Unpack(packet.m_Payload))
            {
                return ReceiverRetVal.PROTOCOL_ERROR;
            }
            time.msec = 0;
            time2.msec = 0;
            if (!(time == time2))
            {
                return ReceiverRetVal.ERROR;
            }
            return ReceiverRetVal.SUCCESS;
        }

        public ReceiverRetVal SetUnitName(byte unitID, ref UnitNameParameter aName)
        {
            ReceiverPacket packet;
            byte[] bytes = new byte[0x15];
            bytes[0] = 2;
            int length = aName.UnitName.Length;
            if (length > 20)
            {
                length = 20;
            }
            Encoding.ASCII.GetBytes(aName.UnitName, 0, length, bytes, 1);
            ReceiverRetVal val = this.PacketTransaction(unitID, ReceiverPacket.CmdID.GET_SET_NAME, bytes, out packet);
            if (ReceiverRetVal.SUCCESS != val)
            {
                return val;
            }
            if (20 != packet.m_Payload.Length)
            {
                return ReceiverRetVal.PROTOCOL_ERROR;
            }
            aName.UnitName = Encoding.ASCII.GetString(packet.m_Payload, 0, packet.m_Payload.Length);
            return ReceiverRetVal.SUCCESS;
        }

        public class ActivateRelayParameter
        {
            public int Interval;
            public int Relay;
        }

        public class AllReceiverInfo
        {
            public ushort NoiseLevel;
            public Receiver.Power_Control PowerControl;
            public PureRF.Receiver.UnitInfo UnitInfo;
            public PureRF.Receiver.UnitStatus UnitStatus;
        }

        public enum AntennaGain : byte
        {
            INVALID = 0,
            MAX = 1,
            MINUS_14DB = 3,
            MINUS_20DB = 4,
            MINUS_6DB = 2
        }

        public class DebugMassages
        {
            public byte[] Debug_Messages;
            public byte Number_of_Debug_Messages;
        }

        public enum ModulationParameter : byte
        {
            INVALID = 0,
            ModASK = 1,
            ModFSK = 2
        }

        public class Power_Control
        {
            public int Input_Voltage;
            public byte Power_Mode;
        }

        public enum RFBaudRates : byte
        {
            b115200 = 6,
            b19200 = 3,
            b28800 = 1,
            b38400 = 4,
            b57600 = 5,
            b9600 = 2,
            INVALID = 0
        }

        public class RSSIFilterParameter
        {
            public int RSSIFilter;
        }

        public class SerialNumParameter
        {
            public uint Serial;
        }

        public class SiteCodeParameter
        {
            public int SiteCode;
        }

        public class Tag
        {
            public byte activatorNum;
            public ushort NoiseLevel;
            public ushort RSSI;
            public const byte TAG_WITH_TS_PAYLOAD_LEN = 14;
            public const byte TAG_WITHOUT_TS_PAYLOAD_LEN = 10;
            public TagID tagID;
            public Receiver.TagMsg tagMsg;
            public byte transmissionIndex;
            public Receiver.Time ts;

            public bool Unpack(byte[] buf)
            {
                if ((10 != buf.Length) && (14 != buf.Length))
                {
                    return false;
                }
                this.tagID = new TagID((uint) ((((buf[0] << 0x18) | (buf[1] << 0x10)) | (buf[2] << 8)) | buf[3]));
                this.transmissionIndex = (byte) (buf[4] & 0x1f);
                this.tagMsg = (Receiver.TagMsg) ((byte) (buf[4] >> 5));
                this.activatorNum = buf[5];
                if (this.activatorNum != 0)
                {
                    this.tagMsg = (Receiver.TagMsg) ((byte) (this.tagMsg | Receiver.TagMsg.ACTIVATOR));
                }
                this.RSSI = (ushort) ((buf[6] << 8) | buf[7]);
                this.NoiseLevel = (ushort) ((buf[8] << 8) | buf[9]);
                this.ts = new Receiver.Time();
                if (14 == buf.Length)
                {
                    ushort num = (ushort) ((buf[10] << 8) | buf[11]);
                    ushort num2 = (ushort) ((buf[12] << 8) | buf[13]);
                    this.ts.hour = (byte) (num / 60);
                    this.ts.min = (byte) (num - (this.ts.hour * 60));
                    this.ts.sec = (byte) (num2 / 0x3e8);
                    this.ts.msec = (byte) (num2 - (this.ts.sec * 0x3e8));
                }
                else
                {
                    this.ts.hour = (byte) DateTime.Now.Hour;
                    this.ts.min = (byte) DateTime.Now.Minute;
                    this.ts.sec = (byte) DateTime.Now.Second;
                    this.ts.msec = (byte) DateTime.Now.Millisecond;
                }
                return true;
            }
        }

        [Flags]
        public enum TagMsg : byte
        {
            ACTIVATOR = 8,
            LowBattery = 1,
            MotionSensor = 2,
            Tamper = 4
        }

        public class Time
        {
            public byte hour;
            public byte min;
            public short msec;
            public byte sec;

            public static bool operator ==(Receiver.Time t1, Receiver.Time t2)
            {
                return ((((t1.hour == t2.hour) && (t1.min == t2.min)) && (t1.sec == t2.sec)) && (t1.msec == t2.msec));
            }

            public static bool operator !=(Receiver.Time t1, Receiver.Time t2)
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

        public class UnitInfo
        {
            public byte Device_Class;
            public byte Device_SubClass;
            public byte Firmware_Version;
            public byte Protocol_Version;
            public string Unit_Name;
            public uint Unit_Serial_Number;

            public override string ToString()
            {
                string str = "";
                return ((((((str + "Protocol Version = " + this.Protocol_Version.ToString()) + "Device Class = " + this.Device_Class.ToString()) + "Device SubClass = " + this.Device_SubClass.ToString()) + "Firmware Version = " + this.Firmware_Version.ToString()) + "Unit Name = " + this.Unit_Name.ToString()) + "Unit Serial Number = " + this.Unit_Serial_Number.ToString());
            }
        }

        public class UnitNameParameter
        {
            public string UnitName = "";
        }

        public class UnitStatus
        {
            public byte Average_Processor_Workload;
            public byte Number_of_Resets;
            public byte Sub_mode;
            public byte Tag_messages_in_buffer;
            public int Uptime;
        }
    }
}

