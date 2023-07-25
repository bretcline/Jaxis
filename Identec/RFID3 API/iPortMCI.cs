/* COPYRIGHT AND COMPANY INFORMATION ****************************************************************************
 *  
 * Copyright (c) 2006 by IDENTEC SOLUTIONS.
 *
 * This Copyright notice may not be removed or modified without prior written consent of IDENTEC SOLUTIONS.
 * IDENTEC SOLUTIONS, reserves the right to modify this software without notice.
 * 
 *
 * IDENTEC Solutions, Inc.
 *
 * www.identecsolutions.com                  
 *
 ****************************************************************************************************************/
using System;
using System.Collections.Generic;
using System.Text;
using IDENTEC.Util;
using IDENTEC.Protocol;

namespace IDENTEC
{
    namespace Readers
    {
        /// <summary>
        /// Summary description for i-Port MCI.
        /// </summary>
        public class iPortMCI
        {
            internal enum DeviceType
            {
                iPortT2 = 1,
                iportMQ1 = 2,
                iportMQ2 = 3,
                PositionMarker = 4,
                PositionMarker2 = 5  // to keep while bug in marker not fixed
            }

		    /// <summary>
		    /// The supported frequency types.
		    /// </summary>
		    /// <remarks>Not to be confused with the Tag/Reader frequency enumeration (values are different).</remarks>
		    enum Frequency: int
		    {
			    Indeterminate = -1,
			    NotApplicable,
			    European,
			    NorthAmerican,
			    EuropeanAndNorthAmerican
		    }
            private System.Collections.Generic.Dictionary<int, string> m_Error = new Dictionary<int, string>();
            private System.Collections.Generic.Dictionary<int, string> m_TagError = new Dictionary<int, string>();

            public enum Wake_Up : byte
            {
                SelfManaged = 0,
                Force = 1,
                NoWakeUp = 2
            }
			internal enum Command: byte
			{
				//Note: response += 0x80 (use this for checking the response)
                GetVersion          = 0x33,
                GetInformation      = 0x60,
				GetDeviceParameter  = 0x64, 
				SetDeviceParameter  = 0x63,
                Collection          = 0x61,
                Sleep               = 0x62,
                SleepAllBut         = 0x65,
                ReadRoutingCode     = 0x66,
                WriteRoutingCode    = 0x67,
                ReadEEPROM          = 0x68,
                WriteEEPROM         = 0x69,
                ReadUDB             = 0x6A, //ok
                BeeperControl       = 0x6B,
                DataBase = 0x6C,
                DeleteWriteableMemory = 0x6D,
            }
            internal enum DatabaseSubCmd : byte
            {
                CreateTable = 0x01,
                AddRecords = 0x02 ,//: Add records
                UpdateRecords = 0x03,// : Update records
                UpdateFields = 0x04,// : Update fields
                DeleteRecord = 0x05,// : Delete record
                GetData = 0x06,// : Get Data
                GetProperty = 0x07,// : Get properties
                ReadFragment = 0x08,// : Read fragment
                WriteFragment = 0x09,// : Write fragment
                Query = 0x10,// : Query

            }
			/// <summary>
			/// 
			/// <seealso cref="Command.GetDeviceParameter"/>
			/// <seealso cref="Command.SetDeviceParameter"/>
			/// </summary>			
			internal enum DeviceParameterID: byte
			{
				ResetToDefault = 0,
				UpTime = 0x02,
				CheckSumAndBootLoader = 0x03,
				Status = 0x04,
				SlavePortConnected = 0x10,
				BusAddress = 0x11,
			}

			//TODO: reuse all the "i-CARD 3" protocol goodies the SDK already implements
			//TODO: implement a bus class for a daisy chain implementation rather than only one reader (see i-PORT R2/T2 in this SDK)
			
			readonly static byte BroadcastAddress = 0xFF;
			readonly static int WAKE_UP_TIME = 3000;  // 3 seconds

			#region >>>>> Private Members <<<<<
			private string m_strFirmware;
			private string m_strBootLoaderVersion;
			private string m_strProtocolVersion;
			private string m_strSerialNumber;
			private string m_info;
			private Frequency m_freq = Frequency.Indeterminate;
			private byte m_byAddress = 0;
            // members used for all RF communication
			private int m_nTxPower= 30;
			private int m_nCurrentAntenna = 1; //1-4
			private int m_nRetries = 5; //the lower level retries
            private Wake_Up m_byWakeUp = Wake_Up.SelfManaged;
            private char m_MaxPacketLength = (char)16;
            private DateTime m_Last_wake_up = new DateTime(200, 1,1);
            /// <summary>
            /// This helper class does all the real communications with the card.
            /// </summary>
            internal IDENTEC.Readers.ISolProtocolFramer m_comm = null;
            #endregion

			#region >>>>> Properties <<<<<
            public int Retries
            {
                get
                {
                    return m_nRetries;
                }
                set
                {
                    if (value < 0)
                        throw new ArgumentOutOfRangeException("value");
                    if (value > 20)
                        throw new ArgumentOutOfRangeException("value");
                    m_nRetries = value;

                }
            }

            public char MaxPacketLength
            {
                get
                {
                    return m_MaxPacketLength;
                }
                set
                {
                    if (value < 16)
                        throw new ArgumentOutOfRangeException("value");
                    if (value > 255)
                        throw new ArgumentOutOfRangeException("value");
                    m_MaxPacketLength = value;

                }
            }
            public int TXPower
            {
                get
                {
                    return m_nTxPower;
                }
                set
                {
                    if (value < -30)
                        throw new ArgumentOutOfRangeException("value");
                    if (value > 30)
                        throw new ArgumentOutOfRangeException("value");
                    m_nTxPower = value;

                }
            }

            public Wake_Up WakeUp
            {
                get
                {
                    return m_byWakeUp;
                }
                set
                {
                    m_byWakeUp = value;
                }
            }

            public int Antenna
            {
                get
                {
                    return m_nCurrentAntenna;
                }
                set
                {
                    if (value < 1)
                        throw new ArgumentOutOfRangeException("value");
                    if (value > 4)
                        throw new ArgumentOutOfRangeException("value");
                    m_nCurrentAntenna = value;

                }
            }

            [System.ComponentModel.Description("The reader firmware version.")]
            [System.ComponentModel.Browsable(true)]
            [System.ComponentModel.DisplayName(" Firmware Version")]
            public string FirmwareVersion
			{
				get 
				{
					return m_strFirmware;
				}
			}

            /// <summary>
            /// Reader Boot loader version
            /// </summary>			
            [System.ComponentModel.Description("The bootloader version.")]
            [System.ComponentModel.Browsable(false)]
            [System.ComponentModel.DisplayName("Bootloader Version")]
            public string BootLoaderVersion
			{
				get 
				{
					return m_strBootLoaderVersion;
				}
			}

            [System.ComponentModel.Description("The protocol version.")]
            [System.ComponentModel.Browsable(false)]
            [System.ComponentModel.DisplayName("Protocol Version")]
            public string ProtocolVersion
			{
				get 
				{
					return m_strProtocolVersion;
				}
			}

			public string SerialNumber
			{
				get
				{
					return m_strSerialNumber;
				}
			}
			#endregion

            public delegate void OnCommandError(string msg);
            public event OnCommandError ErrorCommandEvent;

			/// <summary>
			/// Initializes a new reader instance.
			/// </summary>
            public iPortMCI()
			{
                m_comm = new IDENTEC.Readers.ISolProtocolFramer();

                m_Error.Add(1, "Invalid parameter");
                m_Error.Add(2, "Parameter out of range");
                m_Error.Add(3, "Tag CRC error");
                m_Error.Add(4, "No Tag response");
                m_Error.Add(5, "Bad tag response");

                // add all tag specific error
                m_TagError.Add(0x01, "invalid command code");
                m_TagError.Add(0x02, "invalid command parameter");
                m_TagError.Add(0x04, "Not Found");
                m_TagError.Add(0x06, "can't create object");
                m_TagError.Add(0x08, "Authorization failure");
                m_TagError.Add(0x09, "Object is read only");
                m_TagError.Add(0x40, " Sequence ID mismatch");
                m_TagError.Add(0x41, "boundary Exceeded");
            }

            public override String ToString()
            {
                return "i-Port MCI";
            }

            /// <summary>
            /// 
            /// </summary>
            public void Dispose()
            {
                if (null != m_comm)
                    m_comm.Dispose();
            }

			/// <summary>
			/// Connects to the position marker using a serial port
			/// </summary>
			/// <param name="port"></param>
			public Boolean ConnectSerial(int port)
			{
				//Reset the info collected from the last connection (possibly a different device)
                if (!m_comm.ConnectSerialPort(port))
                    return false;
                string info = "", SN = "";
                int major = 0, minor = 0;
                try
                {
                    if (!GetInformation(ref info, ref SN, ref major, ref minor))
                    {
                        m_comm.Disconnect();
                        ErrorCommandEvent("Failed to read information");
                        return false;
                    }
                }
                catch (Exception ex)
                {
                    m_comm.Disconnect();
                    ErrorCommandEvent("Exception while reading reader information");
                    return false;
                }

				//TODO: read version info and serial number and set private members.
                return true;
			}

            public Boolean Connect(string commport)
            {
                string info = "", SN = "";
                int major = 0, minor = 0;
				m_strFirmware = "";
				m_strSerialNumber = "";
				m_strBootLoaderVersion = "";
                try
                {
                    if (!m_comm.Connect(commport))
                    {
                        ErrorCommandEvent("Failed to open port " + commport);
                        return false;
                    }
                    if (!GetInformation(ref info, ref SN, ref major, ref minor))
                    {
                        m_comm.Disconnect();
                        ErrorCommandEvent("Failed to read information");
                        return false;
                    }
                    if (!info.ToUpper().Contains("MCI"))
                    {
                        m_comm.Disconnect();
                        ErrorCommandEvent("Reader " + info + " not supported");
                        return false;
                    }
                }
                catch (Exception ex)
                {
                    m_comm.Disconnect();
                    ErrorCommandEvent("Exception while reading reader information: " + ex.Message);
                    return false;
                }
                return true;
            }

            public Boolean IsConnected()
            {
                return m_comm.IsOpen;
            }
            private bool IsCommandStatusOK(ref byte[] response)
            {
                if (response[2] != 0)
                {
                    string error;
                    if ((response[2] & 0x80) != 0)         // check if we have a tag error
                    {
                        if (!m_TagError.TryGetValue((response[2] & 0x7F), out error))
                            error = " Unknown";
                        // todo add sub error code
                        ErrorCommandEvent("Tag ERROR: " + error);
                    }
                    else
                    {
                        if (!m_Error.TryGetValue(response[2], out error))
                            error = " Unknown";
                        ErrorCommandEvent("ERROR: " + error);
                    }
                    return false;//ILR_LLBADMSG;   // wrong response code!??	
                }
                return true;
            }


            /// <summary>
			/// Closes the connection to the device and performs cleanup.
			/// </summary>
			public void Disconnect()
			{
                m_comm.Disconnect();
            }

            void retriggerWake_up()
            {
                m_Last_wake_up = DateTime.Now;
            }

            byte getWakeUp(IDENTEC.Tags.ISO18000Tag tag)
            {
                if (tag == null)
                {
                    Console.WriteLine("wake_up");
                    return ((byte)Wake_Up.Force);
                }
                if (this.WakeUp == Wake_Up.SelfManaged)
                {
                    if (tag == null)
                    {
                        TimeSpan time = DateTime.Now - m_Last_wake_up;
                        if (time.Ticks > (TimeSpan.TicksPerSecond * 29))
                        {
                            Console.WriteLine("wake_up");
                            return ((byte)Wake_Up.Force);
                        }
                        else
                        {
                            Console.WriteLine("No wake_up");
                            return ((byte)Wake_Up.NoWakeUp);
                        }
                    }
                    else
                    {
                        TimeSpan time = DateTime.Now - tag._lastTimeSeenAwake;
                        if (time.Ticks > (TimeSpan.TicksPerSecond * 29))
                            return ((byte)Wake_Up.Force);
                        else
                            return ((byte)Wake_Up.NoWakeUp);
                    }

                }
                return (byte)this.WakeUp;
            }

			internal Boolean SetParameter(DeviceParameterID paramID, uint dwParameter)
			{
				byte[] msg = new byte[32];

				msg[0] = m_byAddress;
				msg[1] = (byte) Command.SetDeviceParameter;
				msg[2] = (byte) paramID;

				//Note: this section here is from the i-PORT R2 code:
				//Each parameter is coded on 4 bytes with LSB transmitted first.
				msg[3] = (byte)((dwParameter & 0x000000ff));
				msg[4] = (byte)((dwParameter >> 8) & 0x000000ff);
				msg[5] = (byte)((dwParameter >> 16) & 0x000000ff);
				msg[6] = (byte)((dwParameter >> 24) & 0x000000ff);

				//TODO: write to communications stream and read response
				//SendMessage()
				//TODO: an exception may be thrown if the communications stream barfs
                if (!m_comm.IsOpen)
                    return false;
                m_comm.SendMessage(msg, 7);
                //            SendMessage(msg, 7);
                byte[] byResponse = new byte[64];
                //TODO: Consider a smaller timeout or make it configurable!!!
                int nReceived = m_comm.iCard3_RecvMsg(byResponse, 2000, 0);
                //            int nReceived = RecvMsg(byResponse, /*ReaderBus.CommunicationsTimeout,*/ 5);
                if (nReceived > 0)
                {
                    //TODO: check error code
                    if (byResponse[1] != 0xE3)
                    {
                        throw new InvalidOperationException("The response contained the incorrect command");
                    }
                    return true;
                }
                return false;

			}

			internal void GetParameter(DeviceParameterID paramID, ref uint dwParameter)
			{
				byte[] msg = new byte[32];

				msg[0] = m_byAddress;
				msg[1] = (byte) Command.GetDeviceParameter;
				msg[2] = (byte) paramID;
				
				//TODO: write to communications stream and read response
                if (!m_comm.IsOpen)
                    return;
                m_comm.SendMessage(msg, 3);
                byte[] byResponse = new byte[64];

                //            int nReceived = RecvMsg(byResponse, /*m_ReaderBus.CommunicationsTimeout,*/ 5);
                int nReceived = m_comm.iCard3_RecvMsg(byResponse, 2000, 0);
                if (byResponse[1] != 0xE4)
                {
                    throw new InvalidOperationException("The response contained the incorrect command");
                }



                //Note that the receive buffer is off by one when compared to the C++ code (SOH byte not part of byResponse buff)
//                dwParameter = (uint)((byResponse[4] << 24) + (byResponse[5] << 16) + (byResponse[6] << 8) + (byResponse[7]));
                dwParameter = (uint)((byResponse[7] << 24) + (byResponse[6] << 16) + (byResponse[5] << 8) + (byResponse[4]));
                //SendMessage()
				//TODO: an exception may be thrown if the communications stream barfs
				

			}

			virtual protected void SendMessage( byte [] msg, int len )
			{
				//TODO: take advantage of the ISolProtocolFramer class (does all the fun byte stuffing etc)
			}

			virtual protected int RecvMsg(byte [] msg/*, long timeout,*/, int nPauseBeforeRead)
			{
				//TODO: take advantage of the ISolProtocolFramer class (does all the fun byte stuffing etc)
				return 0;
			}

            public Boolean GetVersion(ref String info, ref int major, ref int minor)
            {
                byte[] msg = new byte[16];
                byte[] buf = new byte[100];
                int rc;
                if (!m_comm.IsOpen)
                {
                    ErrorCommandEvent("Comm Port is closed");
                    return false;
                }
                msg[0] = m_byAddress;
                msg[1] = (byte)Command.GetVersion;
                m_comm.SendMessage(msg, 2);
                rc = m_comm.iCard3_RecvMsg(buf, 1000, 0);
                if (buf[1] != 0xb3)
                {
                    ErrorCommandEvent("Response error");
                    return false;//ILR_LLBADMSG;   // wrong response code!??		
                }
                if (rc < 4)
                {
                    ErrorCommandEvent("Response error " + rc.ToString());
                    return false;//ILR_LLBADMSG;   // wrong response code!??		
                }

                major = buf[2];
                minor = buf[3];

                info = System.Text.Encoding.UTF8.GetString(buf, 4, 20);
                info = info.Replace("\0", "");
                info = info.TrimEnd(null);
                return true;
            }

            public Boolean GetInformation(ref String info, ref string SN,
                                          ref int major, ref int minor)
            {
                byte[] msg = new byte[16];
                byte[] buf = new byte[100];
                int temp1, temp2;
                int rc;
                if (!m_comm.IsOpen)
                {
                    return false;
                }
                msg[0] = m_byAddress;
                msg[1] = (byte)Command.GetInformation;

                m_comm.SendMessage(msg, 2);
                rc = m_comm.iCard3_RecvMsg(buf, 1000, 0);
                if (buf[1] != 0xe0)
                {
                    ErrorCommandEvent("Response error");
                    return false;//ILR_LLBADMSG;   // wrong response code!??			
                }
                if (rc < 4)
                {
                    ErrorCommandEvent("Response error code :" + rc.ToString());
                    return false;//ILR_LLBADMSG;   // wrong response code!??			
                }		

                m_freq = (Frequency)buf[5];
                major = buf[0x10];
                minor = buf[0x11];

                SN = System.Text.Encoding.UTF8.GetString(buf, 7, 10);
                SN = SN.Replace("\0", "");
                SN = SN.TrimEnd(null);
                info = System.Text.Encoding.UTF8.GetString(buf, 0x12, 20);
                info = info.Replace("\0", "");
                info = info.TrimEnd(null);
                m_strFirmware = major.ToString() + "." + minor.ToString();
                temp1 = buf[0x26];
                temp2 = buf[0x27];
			    m_strBootLoaderVersion = temp1.ToString() + "." + temp2.ToString();
                temp1 = buf[0x28];
                temp2 = buf[0x29];
                m_strProtocolVersion = temp1.ToString() + "." + temp2.ToString(); ;
			    m_strSerialNumber = SN;
                m_info = info;
                return true;
            }

            public Boolean CollectWithUDB(ref System.Collections.ArrayList arrayTags, short WindowSize)
            {
                byte collectionLoop = 0;
                byte[] msg = new byte[32];
                byte[] byResponse = new byte[256];
                int rc;
                int i;

                msg[0] = m_byAddress;
                msg[1] = (byte)Command.Collection;
                msg[2] = (byte)this.Antenna;
                msg[3] = (byte)this.TXPower;
                msg[4] = getWakeUp(null);
                msg[5] = collectionLoop;                     // collection loop
                msg[6] = (byte)(WindowSize & 0xFF);          // window size
                msg[7] = (byte)((WindowSize >> 8) & 0xFF);
                msg[8] = (byte)this.m_MaxPacketLength;       // packet length
                msg[9] = 0;                                 // reserved should be 0

                //TODO: write to communications stream and read response
                if (!m_comm.IsOpen)
                {
                    ErrorCommandEvent("Comm Port is closed");
                    return false;
                }
                if (msg[4] == 1)
                    retriggerWake_up();
                m_comm.SendMessage(msg, 10);


                long ltimeout = (57300 * WindowSize);
                if (msg[4] != (byte)Wake_Up.NoWakeUp)
                    System.Threading.Thread.Sleep(WAKE_UP_TIME);

                uint dwTagID = 0;
                UInt16 ManufacturerID;

                while (true)
                {
                    //TODO: make this timeout settable
                    rc = m_comm.iCard3_RecvMsg(byResponse, 5000, 0);
                    if (byResponse[1] != (byte)(Command.Collection + 0x80))
                    {
                        ErrorCommandEvent("Response error");
                        return false;//ILR_LLBADMSG;   // wrong response code!??	
                    }
                    /*
                    if (byArrayMessage[2] != 0)
                    {
                        if (byArrayMessage[2] != 0 )
                            m_deviceCode = (DeviceCode)(((int)DeviceCode.NoAcknowledgementFromTag) - byArrayMessage[2]);
                        //continue;   // error code from i-CARD
                        throw new iCardCommunicationsException("Reader returned an error : " + m_deviceCode.ToString());
                    }
                     */
                    ManufacturerID = BitConverter.ToUInt16(byResponse, 7);
                    dwTagID = BitConverter.ToUInt32(byResponse, 9);
                    //when no more tags are reported, the card responds with an ID of 0
                    if (0 == dwTagID)
                        break;

                    sbyte cSignal = (sbyte)byResponse[6];
                    IDENTEC.Tags.ISO18000Tag tag = null;

                    tag = new IDENTEC.Tags.ISO18000Tag();
                    tag.ID = dwTagID;
                    tag.ManufacturerID = ManufacturerID;
                    tag.setRSSI(1, cSignal);
                    tag._lastTimeSeenAwake = DateTime.Now;

                    int UDBLength = byResponse[13];
                    // check if we have at least 1 routing code byte returned
                    if (UDBLength > 2)
                    {
                        int index = 14;
                        tag.m_RoutingCodeType = byResponse[index++];
                        tag.m_RoutingCode = new byte[byResponse[index++]];
                        if (tag.m_RoutingCode.Length > UDBLength - 2)
                        {
                            tag.m_RoutingCode = new byte[UDBLength - 2];
                        }
                        for (i = 0; i < tag.m_RoutingCode.Length; i++)
                            tag.m_RoutingCode[i] = byResponse[index++];

                        // check if we have at least 1 userID byte returned
                        if (UDBLength > index - 14 + 2)
                        {
                            tag.m_UserIDType = (byte)byResponse[index++];
                            tag.m_UserID = new byte[byResponse[index++]];
                            if (tag.m_UserID.Length > (UDBLength - tag.m_RoutingCode.Length - 4))
                            {
                                tag.m_UserID = new byte[(UDBLength - tag.m_RoutingCode.Length - 4)];
                            }
                            for (i = 0; i < tag.m_UserID.Length; i++)
                                tag.m_UserID[i] = byResponse[index++];
                        }
                    }
                    // check if returned size match the length processed.
                    arrayTags.Add(tag);
                }
                arrayTags.Sort();

//                return arrayTags;
                return true;
            }

            public Boolean Sleep(IDENTEC.Tags.ISO18000Tag tag)
            {
                byte[] msg = new byte[32];
                byte[] byResponse = new byte[64];
                int rc;

                msg[0] = m_byAddress;
                msg[1] = (byte)Command.Sleep;
                msg[2] = (byte)this.Antenna;
                msg[3] = (byte)this.TXPower;
                msg[4] = (byte)Wake_Up.NoWakeUp;
                msg[5] = (byte)(tag.ManufacturerID & 0xFF);
                msg[6] = (byte)((tag.ManufacturerID >> 8) & 0xFF);
                msg[7] = (byte)(tag.ID & 0xFF);
                msg[8] = (byte)((tag.ID >> 8) & 0xFF);
                msg[9] = (byte)((tag.ID >> 16) & 0xFF);
                msg[10] = (byte)((tag.ID >> 24) & 0xFF);


                //TODO: write to communications stream and read response
                if (!m_comm.IsOpen)
                {
                    ErrorCommandEvent("Comm Port is closed");
                    return false;
                }
                m_comm.SendMessage(msg, 11);
                rc = m_comm.iCard3_RecvMsg(byResponse, 1000, 0);
                if (byResponse[1] != (byte)(Command.Sleep + 0x80))
                {
                    ErrorCommandEvent("Response error");
                    return false;//ILR_LLBADMSG;   // wrong response code!??	
                }
                if (!IsCommandStatusOK(ref byResponse))
                {
                    return false;	
                }
                if (rc < 4)
                {
                    ErrorCommandEvent("Command error :" + rc.ToString());
                    return false;//ILR_LLBADMSG;   // wrong response code!??	
                }
                tag._lastTimeSeenAwake = DateTime.Now;
                tag._lastTimeSeenAwake.Subtract(new TimeSpan(1, 0, 0));
                return true;
            }

            public Boolean SleepAllBut(IDENTEC.Tags.ISO18000Tag tag)
            {
                byte[] msg = new byte[32];
                byte[] byResponse = new byte[64];
                int rc;

                msg[0] = m_byAddress;
                msg[1] = (byte)Command.SleepAllBut;
                msg[2] = (byte)this.Antenna;
                msg[3] = (byte)this.TXPower;
                msg[4] = (byte)Wake_Up.NoWakeUp;
                msg[5] = (byte)(tag.ManufacturerID & 0xFF);
                msg[6] = (byte)((tag.ManufacturerID >> 8) & 0xFF);
                msg[7] = (byte)(tag.ID & 0xFF);
                msg[8] = (byte)((tag.ID >> 8) & 0xFF);
                msg[9] = (byte)((tag.ID >> 16) & 0xFF);
                msg[10] = (byte)((tag.ID >> 24) & 0xFF);


                //TODO: write to communications stream and read response
                if (!m_comm.IsOpen)
                {
                    ErrorCommandEvent("Comm Port is closed");
                    return false;
                }
                m_comm.SendMessage(msg, 11);
                rc = m_comm.iCard3_RecvMsg(byResponse, 1000, 0);
                if (byResponse[1] != (byte)(Command.SleepAllBut + 0x80))
                {
                    ErrorCommandEvent("Response error");
                    return false;//ILR_LLBADMSG;   // wrong response code!??	
                }
                if (!IsCommandStatusOK(ref byResponse))
                {
                    return false;
                }
                if (rc < 4)
                {
                    ErrorCommandEvent("Command error :" + rc.ToString());
                    return false;//ILR_LLBADMSG;   // wrong response code!??	
                }
                return true;
            }

            public Boolean ReadRoutingCode(IDENTEC.Tags.ISO18000Tag tag, out byte[] routingCode)
            {
                byte[] msg = new byte[32];
                byte[] byResponse = new byte[64];
                int rc;

                routingCode = null;
                msg[0] = m_byAddress;
                msg[1] = (byte)Command.ReadRoutingCode;
                msg[2] = (byte)this.Antenna;
                msg[3] = (byte)this.TXPower;
                msg[4] = getWakeUp(tag);
                msg[5] = (byte)(tag.ManufacturerID & 0xFF);
                msg[6] = (byte)((tag.ManufacturerID >> 8) & 0xFF);
                msg[7] = (byte)(tag.ID & 0xFF);
                msg[8] = (byte)((tag.ID >> 8) & 0xFF);
                msg[9] = (byte)((tag.ID >> 16) & 0xFF);
                msg[10] = (byte)((tag.ID >> 24) & 0xFF);
                msg[11] = (byte)(this.m_nRetries);


                //TODO: write to communications stream and read response
                if (!m_comm.IsOpen)
                {
                    ErrorCommandEvent("Comm Port is closed");
                    return false;
                }
                if (msg[4] == 1)
                    retriggerWake_up();
                m_comm.SendMessage(msg, 12);
                if (msg[4] != (byte)Wake_Up.NoWakeUp)
                    System.Threading.Thread.Sleep(WAKE_UP_TIME);

                rc = m_comm.iCard3_RecvMsg(byResponse, 1000, 0);
                if (byResponse[1] != (byte)(Command.ReadRoutingCode + 0x80))
                {
                    ErrorCommandEvent("Response error");
                    return false;//ILR_LLBADMSG;   // wrong response code!??	
                }
                if (!IsCommandStatusOK(ref byResponse))
                {
                    return false;
                }
                if (rc < 4)
                {
                    ErrorCommandEvent("Command error :" + rc.ToString());
                    return false;//ILR_LLBADMSG;   // wrong response code!??	
                }
                routingCode = new byte[byResponse[7]];
                for (int i= 0; i < routingCode.Length; i++)
                    routingCode[i] = byResponse[8 + i];
                tag.m_RoutingCode = routingCode;
                sbyte cSignal = (sbyte)byResponse[6];
                tag.setRSSI(Antenna, cSignal);
                tag._lastTimeSeenAwake = DateTime.Now;
                return true;
            }

            public Boolean WriteRoutingCode(IDENTEC.Tags.ISO18000Tag tag, byte code_length, ref byte[] routingCode)
            {
                byte[] msg = new byte[32 + code_length];
                byte[] byResponse = new byte[32];
                int rc;
                msg[0] = m_byAddress;
                msg[1] = (byte)Command.WriteRoutingCode;
                msg[2] = (byte)this.Antenna;
                msg[3] = (byte)this.TXPower;
                msg[4] = getWakeUp(tag);
                msg[5] = (byte)(tag.ManufacturerID & 0xFF);
                msg[6] = (byte)((tag.ManufacturerID >> 8) & 0xFF);
                msg[7] = (byte)(tag.ID & 0xFF);
                msg[8] = (byte)((tag.ID >> 8) & 0xFF);
                msg[9] = (byte)((tag.ID >> 16) & 0xFF);
                msg[10] = (byte)((tag.ID >> 24) & 0xFF);
                msg[11] = (byte)(this.m_nRetries);
                msg[12] = code_length;                             // nb Bytes
                for (int i = 0; i < code_length; i++)
                    msg[13 + i] = routingCode[i];


                //TODO: write to communications stream and read response
                if (!m_comm.IsOpen)
                {
                    ErrorCommandEvent("Comm Port is closed");
                    return false;
                }
                if (msg[4] == 1)
                    retriggerWake_up();
                m_comm.SendMessage(msg, 13 + code_length);
                if (msg[4] != (byte)Wake_Up.NoWakeUp)
                    System.Threading.Thread.Sleep(WAKE_UP_TIME);
                rc = m_comm.iCard3_RecvMsg(byResponse, 1000, 0);
                if (byResponse[1] != (byte)(Command.WriteRoutingCode + 0x80))
                {
                    ErrorCommandEvent("Response error");
                    return false;//ILR_LLBADMSG;   // wrong response code!??	
                }
                if (!IsCommandStatusOK(ref byResponse))
                {
                    return false;
                }
                if (rc < 4)
                {
                    ErrorCommandEvent("Command error :" + rc.ToString());
                    return false;//ILR_LLBADMSG;   // wrong response code!??	
                }
                sbyte cSignal = (sbyte)byResponse[6];
                tag.setRSSI(Antenna, cSignal);
                tag.m_RoutingCode = routingCode;
                tag._lastTimeSeenAwake = DateTime.Now;
                return true;
            }

            public Boolean ReadUDB(IDENTEC.Tags.ISO18000Tag tag, out byte[] UDB)
            {
                byte[] msg = new byte[32];
                byte[] byResponse = new byte[64];
                int rc;
                int i;

                UDB = null;
                msg[0] = m_byAddress;
                msg[1] = (byte)Command.ReadUDB;
                msg[2] = (byte)this.Antenna;
                msg[3] = (byte)this.TXPower;
                msg[4] = getWakeUp(tag);
                msg[5] = (byte)(tag.ManufacturerID & 0xFF);
                msg[6] = (byte)((tag.ManufacturerID >> 8) & 0xFF);
                msg[7] = (byte)(tag.ID & 0xFF);
                msg[8] = (byte)((tag.ID >> 8) & 0xFF);
                msg[9] = (byte)((tag.ID >> 16) & 0xFF);
                msg[10] = (byte)((tag.ID >> 24) & 0xFF);
                msg[11] = (byte)(this.m_nRetries);
                msg[12] = (byte)this.m_MaxPacketLength;      // max length


                //TODO: write to communications stream and read response
                if (!m_comm.IsOpen)
                {
                    ErrorCommandEvent("Comm Port is closed");
                    return false;
                }
                if (msg[4] == 1)
                    retriggerWake_up();
                m_comm.SendMessage(msg, 13);
                if (msg[4] != (byte)Wake_Up.NoWakeUp)
                    System.Threading.Thread.Sleep(WAKE_UP_TIME);
                rc = m_comm.iCard3_RecvMsg(byResponse, 1000, 0);
                if (byResponse[1] != (byte)(Command.ReadUDB + 0x80))
                {
                    ErrorCommandEvent("Response error");
                    return false;//ILR_LLBADMSG;   // wrong response code!??	
                }
                if (!IsCommandStatusOK(ref byResponse))
                {
                    return false;
                }
                if (rc < 4)
                {
                    ErrorCommandEvent("Command error :" + rc.ToString());
                    return false;//ILR_LLBADMSG;   // wrong response code!??	
                }
                sbyte cSignal = (sbyte)byResponse[6];
                tag.setRSSI(Antenna, cSignal);
                int index = 8;
                tag.m_RoutingCodeType = byResponse[index++];
                tag.m_RoutingCode = new byte[byResponse[index++]];
                if (tag.m_RoutingCode.Length > byResponse[7] - 2)
                {
                    ErrorCommandEvent("Returned RC data length " + tag.m_RoutingCode.Length.ToString() + " is not correct");
                    tag.m_RoutingCode = null;
                    return false;//ILR_LLBADMSG;   // wrong response code!??	
                }
                for (i = 0; i < tag.m_RoutingCode.Length; i++)
                    tag.m_RoutingCode[i] = byResponse[index++];

                tag.m_UserIDType = (byte)byResponse[index++];
                tag.m_UserID = new byte[byResponse[index++]];
                tag._lastTimeSeenAwake = DateTime.Now;
                if (tag.m_UserID.Length != (byResponse[7] - tag.m_RoutingCode.Length - 4))
                {
                    ErrorCommandEvent("Returned User ID data length " + tag.m_UserID.Length .ToString() + "is not correct");
                    tag.m_UserID = null;
                    return false;//ILR_LLBADMSG;   // wrong response code!??	
                }
                for (i = 0; i < tag.m_UserID.Length; i++)
                    tag.m_UserID[i] = byResponse[index++];
                // check if returned size match the length processed.

                return true;
            }


            /// <summary>
            ///  New read udb method including max package length and udb-type.
            /// </summary>
            /// <param name="tag"></param>
            /// <param name="udbType"></param>
            /// <param name="maxPackageLength"></param>
            /// <param name="UDB"></param>
            /// <returns></returns>
            


            public Boolean ReadUDB(IDENTEC.Tags.ISO18000Tag tag, int udbType, int maxPacketLength, int offset, out byte[] UDB, out int udbLength)
            {
                byte[] msg = new byte[32];
                byte[] byResponse = new byte[64];
                int rc;
                int i;

                UDB = null;
                udbLength = 0;
                msg[0] = m_byAddress;
                msg[1] = (byte)Command.ReadUDB;
                msg[2] = (byte)this.Antenna;
                msg[3] = (byte)this.TXPower;
                msg[4] = getWakeUp(tag);
                msg[5] = (byte)(tag.ManufacturerID & 0xFF);
                msg[6] = (byte)((tag.ManufacturerID >> 8) & 0xFF);
                msg[7] = (byte)(tag.ID & 0xFF);
                msg[8] = (byte)((tag.ID >> 8) & 0xFF);
                msg[9] = (byte)((tag.ID >> 16) & 0xFF);
                msg[10] = (byte)((tag.ID >> 24) & 0xFF);
                msg[11] = (byte)(this.m_nRetries);
                msg[12] = (byte)udbType;
                msg[13] = (byte)(offset & 0xFF);
                msg[14] = (byte)(offset >> 8 & 0xFF);
                msg[15] = (byte)maxPacketLength;      // max length


                //TODO: write to communications stream and read response
                if (!m_comm.IsOpen)
                {
                    ErrorCommandEvent("Comm Port is closed");
                    return false;
                }
                if (msg[4] == 1)
                    retriggerWake_up();
                m_comm.SendMessage(msg, 16);
                if (msg[4] != (byte)Wake_Up.NoWakeUp)
                    System.Threading.Thread.Sleep(WAKE_UP_TIME);
                rc = m_comm.iCard3_RecvMsg(byResponse, 1000, 0);
                if (byResponse[1] != (byte)(Command.ReadUDB + 0x80))
                {
                    ErrorCommandEvent("Response error");
                    return false;//ILR_LLBADMSG;   // wrong response code!??	
                }
                if (!IsCommandStatusOK(ref byResponse))
                {
                    return false;
                }
                if (rc < 4)
                {
                    ErrorCommandEvent("Command error :" + rc.ToString());
                    return false;//ILR_LLBADMSG;   // wrong response code!??	
                }
                sbyte cSignal = (sbyte)byResponse[7];
                tag.setRSSI(Antenna, cSignal);
                int index = 8;

                
                UDB = new byte[rc-12];                
                Array.Copy(byResponse,12,UDB,0,UDB.Length);
                
                udbLength = byResponse[8];
                udbLength += byResponse[9] << 8;
                
                



                //TODO Parse UDB!

/**                
                tag.m_RoutingCodeType = byResponse[index++];
                tag.m_RoutingCode = new byte[byResponse[index++]];
                if (tag.m_RoutingCode.Length > byResponse[7] - 2)
                {
                    ErrorCommandEvent("Returned RC data length " + tag.m_RoutingCode.Length.ToString() + " is not correct");
                    tag.m_RoutingCode = null;
                    return false;//ILR_LLBADMSG;   // wrong response code!??	
                }
                for (i = 0; i < tag.m_RoutingCode.Length; i++)
                    tag.m_RoutingCode[i] = byResponse[index++];

                tag.m_UserIDType = (byte)byResponse[index++];
                tag.m_UserID = new byte[byResponse[index++]];
                tag._lastTimeSeenAwake = DateTime.Now;
                if (tag.m_UserID.Length != (byResponse[7] - tag.m_RoutingCode.Length - 4))
                {
                    ErrorCommandEvent("Returned User ID data length " + tag.m_UserID.Length.ToString() + "is not correct");
                    tag.m_UserID = null;
                    return false;//ILR_LLBADMSG;   // wrong response code!??	
                }
                for (i = 0; i < tag.m_UserID.Length; i++)
                    tag.m_UserID[i] = byResponse[index++];
                // check if returned size match the length processed.
**/
                return true;
            }


 


            public Boolean ReadMemory(IDENTEC.Tags.ISO18000Tag tag, int address, byte nb_bytes,  out byte[] dataRead)
            {
                byte[] msg = new byte[32];
                byte[] byResponse = new byte[64 + nb_bytes];
                int rc;
                int i;

                dataRead = null;

                msg[0] = m_byAddress;
                msg[1] = (byte)Command.ReadEEPROM;
                msg[2] = (byte)this.Antenna;
                msg[3] = (byte)this.TXPower;
                msg[4] = getWakeUp(tag);
                msg[5] = (byte)(tag.ManufacturerID & 0xFF);
                msg[6] = (byte)((tag.ManufacturerID >> 8) & 0xFF);
                msg[7] = (byte)(tag.ID & 0xFF);
                msg[8] = (byte)((tag.ID >> 8) & 0xFF);
                msg[9] = (byte)((tag.ID >> 16) & 0xFF);
                msg[10] = (byte)((tag.ID >> 24) & 0xFF);
                msg[11] = (byte)(this.m_nRetries);
                msg[12] = (byte)(address & 0xFF); ;             // address
                msg[13] = (byte)((address >> 8) & 0xFF); ;      // address
                msg[14] = (byte)((address >> 16) & 0xFF); ;      // address
                msg[15] = nb_bytes;                             // max length


                //TODO: write to communications stream and read response
                if (!m_comm.IsOpen)
                {
                    ErrorCommandEvent("Comm Port is closed");
                    return false;
                }
                if (msg[4] == 1)
                    retriggerWake_up();
                m_comm.SendMessage(msg, 16);
                if (msg[4] != (byte)Wake_Up.NoWakeUp)
                    System.Threading.Thread.Sleep(WAKE_UP_TIME);
                rc = m_comm.iCard3_RecvMsg(byResponse, 1000, 0);
                if (byResponse[1] != (byte)(Command.ReadEEPROM + 0x80))
                {
                    ErrorCommandEvent("Response error");
                    return false;//ILR_LLBADMSG;   // wrong response code!??	
                }
                if (!IsCommandStatusOK(ref byResponse))
                {
                    return false;
                }
                if (rc < 4)
                {
                    ErrorCommandEvent("Command error :" + rc.ToString());
                    return false;//ILR_LLBADMSG;   // wrong response code!??	
                }
                dataRead = new byte[byResponse[7]];
                for (i = 0; i < dataRead.Length; i++)
                    dataRead[i] = byResponse[8+i];
                sbyte cSignal = (sbyte)byResponse[6];
                tag.setRSSI(Antenna, cSignal);
                tag._lastTimeSeenAwake = DateTime.Now;
                return true;
            }

            public Boolean WriteMemory(IDENTEC.Tags.ISO18000Tag tag, int address, byte nb_bytes, ref byte[] data)
            {
                byte[] msg;
                byte[] byResponse = new byte[32];
                int rc;

                msg = new byte[32 + nb_bytes];
                msg[0] = m_byAddress;
                msg[1] = (byte)Command.WriteEEPROM;
                msg[2] = (byte)this.Antenna;
                msg[3] = (byte)this.TXPower;
                msg[4] = getWakeUp(tag);
                msg[5] = (byte)(tag.ManufacturerID & 0xFF);
                msg[6] = (byte)((tag.ManufacturerID >> 8) & 0xFF);
                msg[7] = (byte)(tag.ID & 0xFF);
                msg[8] = (byte)((tag.ID >> 8) & 0xFF);
                msg[9] = (byte)((tag.ID >> 16) & 0xFF);
                msg[10] = (byte)((tag.ID >> 24) & 0xFF);
                msg[11] = (byte)(this.m_nRetries);
                msg[12] = (byte)(address & 0xFF); ;             // address
                msg[13] = (byte)((address >> 8) & 0xFF); ;      // address
                msg[14] = (byte)((address >> 16) & 0xFF); ;      // address
                msg[15] = nb_bytes;                             // nb Bytes
                for (int i=0; i< nb_bytes; i++)
                    msg[16+i] = data[i];


                //TODO: write to communications stream and read response
                if (!m_comm.IsOpen)
                {
                    ErrorCommandEvent("Comm Port is closed");
                    return false;
                }
                if (msg[4] == 1)
                    retriggerWake_up();
                m_comm.SendMessage(msg, 16 + nb_bytes);
                if (msg[4] != (byte)Wake_Up.NoWakeUp)
                    System.Threading.Thread.Sleep(WAKE_UP_TIME);
                rc = m_comm.iCard3_RecvMsg(byResponse, 2000, 0);
                if (byResponse[1] != (byte)(Command.WriteEEPROM + 0x80))
                {
                    ErrorCommandEvent("Response error");
                    return false;//ILR_LLBADMSG;   // wrong response code!??	
                }
                if (!IsCommandStatusOK(ref byResponse))
                {
                    return false;
                }
                if (rc < 4)
                {
                    ErrorCommandEvent("Command error :" + rc.ToString());
                    return false;//ILR_LLBADMSG;   // wrong response code!??	
                }
                sbyte cSignal = (sbyte)byResponse[6];
                tag.setRSSI(Antenna, cSignal);
                tag._lastTimeSeenAwake = DateTime.Now;
                return true;
            }

            public Boolean BeeperControl(IDENTEC.Tags.ISO18000Tag tag, Boolean BeeperON)
            {
                byte[] msg = new byte[32];
                byte[] byResponse = new byte[32];
                int rc;

                msg[0] = m_byAddress;
                msg[1] = (byte)Command.BeeperControl;
                msg[2] = (byte)this.Antenna;
                msg[3] = (byte)this.TXPower;
                msg[4] = getWakeUp(tag);
                msg[5] = (byte)(tag.ManufacturerID & 0xFF);
                msg[6] = (byte)((tag.ManufacturerID >> 8) & 0xFF);
                msg[7] = (byte)(tag.ID & 0xFF);
                msg[8] = (byte)((tag.ID >> 8) & 0xFF);
                msg[9] = (byte)((tag.ID >> 16) & 0xFF);
                msg[10] = (byte)((tag.ID >> 24) & 0xFF);
                msg[11] = (byte)(this.m_nRetries);
                if (BeeperON)
                    msg[12] = 1;
                else
                    msg[12] = 0;

                //TODO: write to communications stream and read response
                if (!m_comm.IsOpen)
                {
                    ErrorCommandEvent("Comm Port is closed");
                    return false;
                }
                if (msg[4] == 1)
                    retriggerWake_up();
                m_comm.SendMessage(msg, 13);
                if (msg[4] != (byte)Wake_Up.NoWakeUp)
                    System.Threading.Thread.Sleep(WAKE_UP_TIME);
                rc = m_comm.iCard3_RecvMsg(byResponse, 3000, 0);
                if (byResponse[1] != (byte)(Command.BeeperControl + 0x80))
                {
                    ErrorCommandEvent("Response error");
                    return false;//ILR_LLBADMSG;   // wrong response code!??	
                }
                if (rc < 4)
                {
                    ErrorCommandEvent("Command error :" + rc.ToString());
                    return false;//ILR_LLBADMSG;   // wrong response code!??	
                }
                if (!IsCommandStatusOK(ref byResponse))
                {
                    return false;
                }
                sbyte cSignal = (sbyte)byResponse[6];
                tag.setRSSI(Antenna, cSignal);
                tag._lastTimeSeenAwake = DateTime.Now;
                return true;
            }

            internal Boolean DataBase(IDENTEC.Tags.ISO18000Tag tag, IDENTEC.Readers.iPortMCI.DatabaseSubCmd SubCommand, byte[] Cmddata)
            {
                byte[] resp = null;
                return DataBase(tag, SubCommand, Cmddata, out resp);
            }

            internal Boolean DataBase(IDENTEC.Tags.ISO18000Tag tag, IDENTEC.Readers.iPortMCI.DatabaseSubCmd SubCommand, byte[] Cmddata, out byte[] response)
            {
                byte[] msg = new byte[255];
                byte[] byResponse = new byte[255];
                response = null;
                int rc;

                msg[0] = m_byAddress;
                msg[1] = (byte)Command.DataBase;
                msg[2] = (byte)this.Antenna;
                msg[3] = (byte)this.TXPower;
                msg[4] = getWakeUp(tag);
                msg[5] = (byte)(tag.ManufacturerID & 0xFF);
                msg[6] = (byte)((tag.ManufacturerID >> 8) & 0xFF);
                msg[7] = (byte)(tag.ID & 0xFF);
                msg[8] = (byte)((tag.ID >> 8) & 0xFF);
                msg[9] = (byte)((tag.ID >> 16) & 0xFF);
                msg[10] = (byte)((tag.ID >> 24) & 0xFF);
                msg[11] = (byte)(this.m_nRetries);
                msg[12] = (byte)(SubCommand);
                Array.Copy(Cmddata, 0, msg, 13, Cmddata.Length);

                //TODO: write to communications stream and read response
                if (!m_comm.IsOpen)
                {
                    ErrorCommandEvent("Comm Port is closed");
                    return false;
                }
                if (msg[4] == 1)
                    retriggerWake_up();

                m_comm.SendMessage(msg, 13 + Cmddata.Length);
                if (msg[4] != (byte)Wake_Up.NoWakeUp)
                    System.Threading.Thread.Sleep(WAKE_UP_TIME);
                rc = m_comm.iCard3_RecvMsg(byResponse, 3000, 0);
                if (byResponse[1] != (byte)(Command.DataBase + 0x80))
                {
                    ErrorCommandEvent("Response error");
                    return false;//ILR_LLBADMSG;   // wrong response code!??	
                }
                if (rc < 4)
                {
                    ErrorCommandEvent("Command error :" + rc.ToString());
                    return false;//ILR_LLBADMSG;   // wrong response code!??	
                }
                if (!IsCommandStatusOK(ref byResponse))
                {
                    return false;
                }
                sbyte cSignal = (sbyte)byResponse[6];
                tag.setRSSI(Antenna, cSignal);
                tag._lastTimeSeenAwake = DateTime.Now;
                response = new byte[byResponse.Length - 5];
                Array.Copy(byResponse, 5, response, 0, response.Length);
                return true;
            }

            public Boolean DeleteWriteableMemory(IDENTEC.Tags.ISO18000Tag tag)
            {
                byte[] msg = new byte[32];
                byte[] byResponse = new byte[64];
                int rc;

               
                msg[0] = m_byAddress;
                msg[1] = (byte)Command.DeleteWriteableMemory;
                msg[2] = (byte)this.Antenna;
                msg[3] = (byte)this.TXPower;
                msg[4] = getWakeUp(tag);
                msg[5] = (byte)(tag.ManufacturerID & 0xFF);
                msg[6] = (byte)((tag.ManufacturerID >> 8) & 0xFF);
                msg[7] = (byte)(tag.ID & 0xFF);
                msg[8] = (byte)((tag.ID >> 8) & 0xFF);
                msg[9] = (byte)((tag.ID >> 16) & 0xFF);
                msg[10] = (byte)((tag.ID >> 24) & 0xFF);
                msg[11] = (byte)(this.m_nRetries);


                //TODO: write to communications stream and read response
                if (!m_comm.IsOpen)
                {
                    ErrorCommandEvent("Comm Port is closed");
                    return false;
                }
                if (msg[4] == 1)
                    retriggerWake_up();
                m_comm.SendMessage(msg, 12);
                if (msg[4] != (byte)Wake_Up.NoWakeUp)
                    System.Threading.Thread.Sleep(WAKE_UP_TIME);

                rc = m_comm.iCard3_RecvMsg(byResponse, 1000, 0);
                if (byResponse[1] != (byte)(Command.DeleteWriteableMemory + 0x80))
                {
                    ErrorCommandEvent("Response error");
                    return false;//ILR_LLBADMSG;   // wrong response code!??	
                }
                if (!IsCommandStatusOK(ref byResponse))
                {
                    return false;
                }
                if (rc < 4)
                {
                    ErrorCommandEvent("Command error :" + rc.ToString());
                    return false;//ILR_LLBADMSG;   // wrong response code!??	
                }
                
                sbyte cSignal = (sbyte)byResponse[6];
                tag.setRSSI(Antenna, cSignal);
                tag._lastTimeSeenAwake = DateTime.Now;
                return true;
            }



			#region >>>>> Set/Get Commands for Application Specific Calls <<<<<
			public void RestoreDefaults()
			{
				SetParameter(DeviceParameterID.ResetToDefault, 0);
			}

			public DateTime GetBootTime()
			{
				uint dw = 0;
				//GetParameter(DeviceParameterID.UpTime,
				TimeSpan ts = new TimeSpan(0, 0, 0, (int)dw, 0);
				return DateTime.Now - ts;
			}

			//TODO: not sure what to do with the CRC status and boot loader version
			//TODO: Reader Status info -there's a lot there ;)


			public bool GetSlavePortConnected()
			{
				uint dw = 0;
				GetParameter(DeviceParameterID.SlavePortConnected, ref dw);
				return dw == 1;
			}

			public void SetSlavePortConnected(bool enable)
			{
				uint dw = 0;
				if (enable)
					dw = 1;
				SetParameter(DeviceParameterID.SlavePortConnected, dw);
			}

			public void SetAddresss(byte address)
			{
                //TODO: check values for valid addresses (throw ArgumentOutOfRange exception if necessary)
				//DO NOT SET THE MEMBER ADDRESS UNTIL THIS SUCCEEDS!
				uint dw = 0;
				dw = address;
				SetParameter(DeviceParameterID.BusAddress, dw);
				m_byAddress = (byte)dw;
			}
			#endregion		
		}
	}
    /// <summary>
    /// Class to provide conversions for time_t and DateTime
    /// </summary>
    internal class DateTimeConvertor
    {
        /// <summary>
        /// Converts a time_t value to the relevant DateTime value
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        internal static DateTime Convert_time_t(System.UInt32 t)
        {
#if DEBUG
            if (0 == t)
                throw new ArgumentOutOfRangeException("t cannot be 0");
#endif
            TimeZone tz = TimeZone.CurrentTimeZone;
            TimeSpan tsOffSet = tz.GetUtcOffset(DateTime.Now);
            //local time = UTC + offset
            //Bug Fix: Ensure that the time_t value is offset to the local time, because that's what the 
            //constructor for DateTime is looking for (time_t is UTC in seconds)
            DateTime dt = new System.DateTime(1970, 1, 1).AddSeconds(t + tsOffSet.TotalSeconds);
            return dt;
        }

        /// <summary>
        /// Converts the current DateTime to a time_t value
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        internal static uint ConvertDateTime(DateTime dt)
        {
            uint time_t = 0;
            //TODO: sanity check for datetime before birth of UNIX!!!

            //Todo: make this value static and read only
            //http://tinyurl.com/e2vmb (but I added the time zone stuff...)
            TimeZone tz = TimeZone.CurrentTimeZone;
            TimeSpan tsOffSet = tz.GetUtcOffset(DateTime.Now);
            DateTime dtUTC = new System.DateTime(1970, 1, 1);
            time_t = (uint)((dt - dtUTC).TotalSeconds - tsOffSet.TotalSeconds);

            return time_t;
        }
    }
}
