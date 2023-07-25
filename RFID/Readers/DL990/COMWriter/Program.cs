using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO.Ports;

namespace COMWriter
{

    public class RFIDCom
    {
        //Dictionary<DL990CommandType, DL990Command> m_Response = new Dictionary<DL990CommandType, DL990Command>();
        DL990Command m_Response = new DL990Command(0x00);
        bool m_Reading = false;
        System.IO.Ports.SerialPort m_Port = null;

        static void Main( string[] args )
        {

            DL900();

            //Puk();
        }
        static private void DL900()
        {
            RFIDCom Com = new RFIDCom();

            Console.WriteLine("Open");
            Com.Open("COM7");

            Console.WriteLine("Send");
            DL990Command Command = new DL990Command(DL990CommandType.ReadSerialNumber);
            DL990Command Resp = Com.SendMessage(Command);
            Com.ProcessResponse(Command.CommandType, Resp, Console.WriteLine);

            Command = new DL990Command(DL990CommandType.ISO_Inventory, new byte[] { 0x06, 0x00, 0x00, 0x12 });
            Resp = Com.SendISOMessage (Command );
            byte[] Inventory = Com.ProcessInventory(Resp, Console.WriteLine);

            int i = 0;
            while (i < Inventory.Count())
            {
                byte[] Data = new byte[9];
                Data[0] = 0x24;
                Array.Copy (Inventory, 0, Data, 1, 8);
                Command = new DL990Command(DL990CommandType.ISO_GetSystemInformation, Data);
                Resp = Com.SendISOMessage(Command);
                Com.ProcessResponse(Command.CommandType, Resp, Console.WriteLine);

                //Command = new DL990Command(DL990CommandType.ISO_Write, new byte[] { 0x00, 0x01, 0x01, 0xFF });
                //Resp = Com.SendISOMessage(Command);
                //Com.ProcessResponse(Command.CommandType, Resp, Console.WriteLine);

                Command = new DL990Command(DL990CommandType.ISO_Read, new byte[] { 0x02, 0x01, 0x05 });
                Resp = Com.SendISOMessage(Command);
                Com.ProcessResponse(Command.CommandType, Resp, Console.WriteLine);

                i += 8;
            }
            
            Com.Close( );
        }


//        static private void Puk()
//        {
//        RFIDCom Com = new RFIDCom( );

//            Console.WriteLine( "Open" );
//            Com.Open("COM15");

//            Console.WriteLine( "Send" );
//            PUKCommand Resp = Com.SendMessage( new PUKCommand( PUKCommandType.ReadVersion ) );

//            //Resp = Com.SendMessage( new RFIDCommand( CommandType.ResetPUK ) );
//            //Com.Close( );
//            //Com.Open( );

////            Resp = Com.SendMessage( new RFIDCommand( CommandType.ReadSerialNumber ) );

////            Console.WriteLine( BitConverter.ToString( Resp.Parameters.ToArray( ) ) );
//            byte ISO15693Options = 0x02;


//            ISO15693Options = 0x00; //  0x7E; // MLF 0x76;
//            Resp = Com.SendMessage( new PUKCommand( PUKCommandType.CarrierOnOff, ISO15693Options ) );

//            Resp = Com.SendMessage( new PUKCommand( PUKCommandType.ProgramConfigurationBytes, ISO15693Options, new byte[] { 0x0f, 0x81, 0x06 } ) );

//            while (true)
//            {
//                byte Error = 0xFF;
//                while (0xFF == Error)
//                {
//                    Resp = Com.SendMessage(new PUKCommand(PUKCommandType.TIRIS_ChargeRead));
//                    Error = Resp.Options;
//                }
//                Com.ProcessResponse(Resp.CommandType, Resp, Console.WriteLine);

//                TIRISTransponderType TagType = (TIRISTransponderType)Resp.Parameters[0];
//                Console.WriteLine( "Tag Type: " + TagType );
//                switch( TagType )                
//                {
//                    case TIRISTransponderType.ReadWrite:
//                    {
//                        Resp = Com.SendMessage(new PUKCommand(PUKCommandType.TIRIS_Write, 0x00, new byte[] { 0x01, 0x02, 0x03, 0x04, 0x05, 0x06, 0x07, 0x08 }));
//                        Com.ProcessResponse(Resp.CommandType, Resp, Console.WriteLine);
//                        break;
//                    }
//                }

//            }

//            //byte Error = 0xFF;
//            //while( 0xFF == Error )
//            //{
//            //    ISO15693Options = 0x3E; // MLF
//            //    //Resp = Com.SendMessage(new PUKCommand(PUKCommandType.ISO_Inventory, ISO15693Options));
//            //    Resp = Com.SendMessage( new PUKCommand( PUKCommandType.TIRIS_Read ) );
//            //    Error = Resp.Options;
//            //}
//            //Com.ProcessResponse( Resp.CommandType, Resp, Console.WriteLine );
//            //Resp.Parameters.ForEach( C => Console.Write( (char)C ) );
//            //Console.WriteLine( );
//            //Console.WriteLine( BitConverter.ToString( Resp.Parameters.ToArray( ) ) );

//            //List<byte> MList = new List<byte>( ) { (byte)'B', (byte)'r', (byte)'e', (byte)'t', 0x32, 0x32, 0x32, 0x32 };
//            ////char[] Message = "11111111".ToCharArray( );
//            ////Array.ForEach( Message, c => MList.Add( (byte)c ));
//            //Resp = Com.SendMessage( new PUKCommand( PUKCommandType.TIRIS_Write, MList.ToArray( ) ) );

//            //Error = 0xFF;
//            //while( 0xFF == Error )
//            //{
//            //    Resp = Com.SendMessage( new PUKCommand( PUKCommandType.TIRIS_Read ) );
//            //    Error = Resp.Options;
//            //}
//            //Com.ProcessResponse( Resp.CommandType, Resp, Console.WriteLine );

//            //Resp.Parameters.ForEach( C => Console.Write( (char)C ) );
//            //Console.WriteLine( );

//            //Console.WriteLine( BitConverter.ToString( Resp.Parameters.ToArray( ) ) );


//            Com.Close( );
//        }


        public void Open(string _Port )
        {
            m_Port = new SerialPort ( _Port, 9600, Parity.None, 8, StopBits.One );
            m_Port.Open( );
            System.Threading.Thread.Sleep( 50 );
            m_Port.DataReceived += Ports_DataReceived;
        }

        public void Close( )
        {
            m_Port.Close( );
        }

        public DL990Command SendMessage(DL990Command _Comm)
        {
            m_Port.Write(_Comm.CommandBytes, 0, _Comm.CommandLength);
            m_Reading = true;
            while (m_Reading && m_Port.IsOpen)
            {
                System.Threading.Thread.Sleep(75);
            }

            System.Diagnostics.Debug.WriteLine("SendMessage Command:" + _Comm.CommandType + "  " + (int)_Comm.CommandType);
            //return m_Response[_Comm.CommandType];
            return m_Response;
        }

        public DL990Command SendISOMessage(DL990Command _Comm)
        {
            m_Port.Write(_Comm.ISOCommandBytes, 0, _Comm.ISOCommandLength);
            m_Reading = true;
            while (m_Reading && m_Port.IsOpen)
            {
                System.Threading.Thread.Sleep(75);
            }

            System.Diagnostics.Debug.WriteLine("SendMessage Command:" + _Comm.CommandType + "  " + (int)_Comm.CommandType);
            //return m_Response[_Comm.CommandType];
            return m_Response;
        }

        void Ports_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            try
            {
                int Offset = 0;
                System.Threading.Thread.Sleep(35);
                SerialPort Port = sender as SerialPort;
                int ReadCount = Port.BytesToRead;

                List<byte> MsgResp = new List<byte>();
                byte[] Resp = new byte[ReadCount];
                int read = Port.Read(Resp, Offset, ReadCount);
                MsgResp.AddRange(Resp);

                while (read < 9 && Offset < 9)
                {
                    Offset += read;
                    ReadCount = Port.BytesToRead;
                    Resp = new byte[ReadCount];

                    read = Port.Read(Resp, 0, ReadCount);
                    MsgResp.AddRange(Resp);
                }

                DL990Command Cmd = new DL990Command(MsgResp.ToArray());
                //m_Response[Cmd.CommandType] = Cmd;
                m_Response = Cmd;

                m_Reading = false;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }
        }

        public void ProcessError(DL990Command _ErrorPacket, Action<string> _Writer)
        {
            Console.WriteLine("Error: " + _ErrorPacket.CommandType);
            System.Diagnostics.Debug.WriteLine("Error");
            foreach (byte Err in _ErrorPacket.Data)
            {
                _Writer(String.Format("{0} : {1}", (PUKErrorCodes)Err, (int)Err));
                System.Diagnostics.Debug.WriteLine(String.Format("{0} : {1}", (PUKErrorCodes)Err, (int)Err));
            }
        }

        public void ProcessResponse(DL990CommandType _Type, DL990Command _Packet, Action<string> _Writer)
        {
            switch (_Type)
            {
                case DL990CommandType.ReadSerialNumber:
                    {
                        byte[] SN = new byte[_Packet.DataLength-1];
                        Array.Copy(_Packet.Data.ToArray(), 0, SN, 0, _Packet.DataLength-1);
                        _Writer(String.Format("Reader SN: {0:X}", BitConverter.ToString(SN).Replace("-", "")));
                        break;
                    }
                case DL990CommandType.ISO_Inventory:
                    {
                        byte[] Flag_DSFID = new byte[2];
                        Array.Copy(_Packet.Data.ToArray(), 0, Flag_DSFID, 0, 2);
                        _Writer(String.Format("Flag and DSFID: {0:X}", BitConverter.ToString(Flag_DSFID).Replace("-", " ")));
                        byte[] Card_SN = new byte[8];
                        Array.Copy(_Packet.Data.ToArray(), 2, Card_SN, 0, 8);
                        _Writer(String.Format("Card SN: {0:X}", BitConverter.ToString(Card_SN).Replace("-", "")));
                        break;
                    }
                case DL990CommandType.ISO_GetSystemInformation:
                    {
                        if (0x0b == _Packet.DataLength)
                        {
                            byte[] UID = new byte[8];
                            Array.Copy(_Packet.Data.ToArray(), 0, UID, 0, 8);
                            _Writer(String.Format("Card SN: {0:X}", BitConverter.ToString(UID).Replace("-", "")));
                            byte[] MemSize = new byte[2];
                            Array.Copy(_Packet.Data.ToArray(), 8, MemSize, 0, 2);
                            _Writer(String.Format("Mem Size: {0:X}", BitConverter.ToString(MemSize).Replace("-", "")));
                        }
                        else
                        {
                            byte[] Flags = new byte[1];
                            Array.Copy(_Packet.Data.ToArray(), 0, Flags, 0, 1);
                            _Writer(String.Format("Flags: {0:X}", BitConverter.ToString(Flags)));
                            byte[] UID = new byte[8];
                            Array.Copy(_Packet.Data.ToArray(), 1, UID, 0, 8);
                            _Writer(String.Format("UID: {0:X}", BitConverter.ToString(UID).Replace("-", "")));
                            byte[] DSFID_AFI = new byte[2];
                            Array.Copy(_Packet.Data.ToArray(), 9, DSFID_AFI, 0, 2);
                            _Writer(String.Format("DSFID AFI: {0:X}", BitConverter.ToString(DSFID_AFI).Replace("-", "")));
                            byte[] MemSize = new byte[2];
                            Array.Copy(_Packet.Data.ToArray(), 11, MemSize, 0, 2);
                            _Writer(String.Format("Mem Size: {0:X}", BitConverter.ToString(MemSize).Replace("-", "")));
                            byte[] IC = new byte[1];
                            Array.Copy(_Packet.Data.ToArray(), 12, IC, 0, 1);
                            _Writer(String.Format("IC: {0:X}", BitConverter.ToString(IC).Replace("-", "")));
                        }
                        break;
                    }
                case DL990CommandType.ISO_Write:
                    {
                        break;
                    }
                case DL990CommandType.ISO_Read:
                    {
                        byte[] Flags = new byte[1];
                        Array.Copy(_Packet.Data.ToArray(), 0, Flags, 0, 1);
                        _Writer(String.Format("Flags: {0:X}", BitConverter.ToString(Flags).Replace("-", " ")));
                        byte[] Data = new byte[1];
                        Array.Copy(_Packet.Data.ToArray(), 1, Data, 0, 1);
                        _Writer(String.Format("Data: {0:X}", BitConverter.ToString(Data).Replace("-", "")));
                        break;
                    }
                default:
                    {
                        byte[] TagID = new byte[_Packet.DataLength - 1];
                        Array.Copy(_Packet.Data.ToArray(), 1, TagID, 0, TagID.Length);
                        _Writer(String.Format("Tag ID: {0:X}", BitConverter.ToString(TagID).Replace("-", "")));
                        break;
                    }

            }
        }

        public byte[] ProcessInventory(DL990Command _Packet, Action<string> _Writer)
        {
            byte[] rc = new byte[_Packet.Command * 8];
            //if (0x00 == _Packet.Command) // Status == no error
            //{
                byte[] FLAG_DSFID = new byte[2];
                Array.Copy(_Packet.Data.ToArray(), 0, FLAG_DSFID, 0, 2);
                _Writer(String.Format("FLAG DSFID: {0:X}", BitConverter.ToString(FLAG_DSFID).Replace("-", " ")));
                _Writer(String.Format("Card Count: {0:X}", _Packet.Command));
                for (int i = 0; i < _Packet.Command; i++)
                {
                    byte[] Card_SN = new byte[8];
                    Array.Copy(_Packet.Data.ToArray(), 2 + i*10, Card_SN, 0, 8);
                    _Writer(String.Format("Card {1} SN: {0:X}", BitConverter.ToString(Card_SN).Replace("-", ""), i));
                    Array.Copy (Card_SN, 0, rc, i*8, 8);
                }
            //}
            //else
            //{
            //    _Writer(String.Format("Inv Error: {0:X}", _Packet.Command));
            //}


            return rc;
        }

        //public PUKCommand SendMessage( PUKCommand _Comm )
        //{
        //    m_Port.Write( _Comm.CommandBytes, 0, _Comm.CommandLength );
        //    m_Reading = true;
        //    while( m_Reading && m_Port.IsOpen )
        //    {
        //        System.Threading.Thread.Sleep( 75 );
        //    }

        //    System.Diagnostics.Debug.WriteLine( "SendMessage Command:" + _Comm.CommandType + "  " + (int)_Comm.CommandType );
        //    return m_Response[_Comm.CommandType];
        //}

        //void Ports_DataReceived( object sender, SerialDataReceivedEventArgs e )
        //{
        //    try
        //    {
        //        int Offset = 0;
        //        System.Threading.Thread.Sleep( 35 );
        //        SerialPort Port = sender as SerialPort;
        //        int ReadCount = Port.BytesToRead;

        //        List<byte> MsgResp = new List<byte>( );
        //        byte[] Resp = new byte[ReadCount];
        //        int read = Port.Read( Resp, Offset, ReadCount );
        //        MsgResp.AddRange( Resp );

        //        while( read < 9 && Offset < 9 )
        //        {
        //            Offset += read;
        //            ReadCount = Port.BytesToRead;
        //            Resp = new byte[ReadCount];

        //            read = Port.Read( Resp, 0, ReadCount );
        //            MsgResp.AddRange( Resp );
        //        }
                
        //        PUKCommand Cmd = new PUKCommand( MsgResp.ToArray( ) );
        //        m_Response[Cmd.CommandType] = Cmd;

        //        m_Reading = false;
        //    }
        //    catch( Exception ex )
        //    {
        //        System.Diagnostics.Debug.WriteLine( ex.Message );
        //    }
        //}

        //public void ProcessError( PUKCommand _ErrorPacket, Action<string> _Writer )
        //{
        //    Console.WriteLine( "Error: " + _ErrorPacket.CommandType );
        //    System.Diagnostics.Debug.WriteLine( "Error" );
        //    foreach( byte Err in _ErrorPacket.Parameters )
        //    {
        //        _Writer( String.Format( "{0} : {1}", (PUKErrorCodes)Err, (int)Err ) );
        //        System.Diagnostics.Debug.WriteLine( String.Format( "{0} : {1}", (PUKErrorCodes)Err, (int)Err ) );
        //    }
        //}

        //public void ProcessResponse( PUKCommandType _Type, PUKCommand _Packet, Action<string> _Writer )
        //{
        //    switch( _Type )
        //    {
        //        case PUKCommandType.TIRIS_ChargeRead:
        //        {
        //            TIRISTransponderType TagType = (TIRISTransponderType)_Packet.Parameters[0];
        //            Console.WriteLine( "Tag Type: " + TagType );
        //            switch( TagType )
        //            {
        //                case TIRISTransponderType.MultiPage:
        //                {
        //                    int PageNumber = (int)_Packet.Parameters[1];
        //                    int Status = (int)_Packet.Parameters[2];
        //                    byte[] PageData = new byte[_Packet.Parameters.Count - 2];
        //                    Array.Copy( _Packet.Parameters.ToArray( ), 2, PageData, 0, PageData.Length );
        //                    _Writer( String.Format( "Page Data: {0}", Convert.ToBase64String( PageData ) ) );
        //                    break;
        //                }
        //                default:
        //                {

        //                    byte[] TagID = new byte[_Packet.Parameters.Count - 1];
        //                    Array.Copy( _Packet.Parameters.ToArray( ), 1, TagID, 0, TagID.Length );
        //                    _Writer( String.Format( "Tag ID: {0}", Convert.ToBase64String( TagID ) ) );
        //                    break;
        //                }
        //            }
        //            break;
        //        }
        //        case PUKCommandType.TIRIS_Write:
        //        {
        //            byte[] TagID = new byte[_Packet.Parameters.Count - 1];
        //            Array.Copy(_Packet.Parameters.ToArray(), 1, TagID, 0, TagID.Length);
        //            _Writer(String.Format("Tag Data: {0}", Convert.ToBase64String(TagID)));
        //            break;
        //        }
        //        default:
        //        {
        //            byte[] TagID = new byte[_Packet.Parameters.Count - 1];
        //            Array.Copy( _Packet.Parameters.ToArray( ), 1, TagID, 0, TagID.Length );
        //            _Writer( String.Format( "Tag ID: {0}", Convert.ToBase64String( TagID ) ) );
        //            break;
        //        }

        //    }
        //}
    }
}

//> 020003010000000600
//    02020003010009000
//    15555555555555555B802020003010009000
//    15555555555555555B802020003010009000
//    15555555555555555B80202000301FF0100070D01020003010009000
//    15555555555555555B80202000301FF0100070D01020003010009000
//    15555555555555555B802020003010009000
//    15555555555555555B80202000301FF0100070D01020003010009000
//    15555555555555555B80202000301FF0100070D0102000301FF0100070D0102000301FF0100070D0102000301FF0100070D01020003010009000
//    15555555555555555B802020003010009000
//    15555555555555555B802020003010009000
//    15555555555555555B802020003010009000
//    15555555555555555B802020003010009000
//    15555555555555555B802020003010009000
//    15555555555555555B80202000301FF01000
//    70D0102000301FF01000
//    70D0102000301FF01000
//    70D0102000301FF01000
//    70D0102000301FF01000
//    70D0102000301FF01000
//    70D0102000301FF01000
//    70D0102000301FF01000
//    70D0102000301FF01000
//    70D0102000301FF01000
//    70D0102000301FF01000
//    70D0102000301FF01000
//    70D0102000301FF01000
//    70D0102000301FF01000
//    70D0102000301FF01000
//    70D0102000301FF01000
//    70D0102000301FF01000
//    70D01020003010009000
//    15555555555555555B80202000301FF0100070D0102000301FF0100070D01020003010009000
//    15555555555555555B802020003010009000
//    15555555555555555B802020003010009000
//    15555555555555555B802020003010009000
//    15555555555555555B80202000301000900015555555555555555B80202000301FF0100070D0102000301FF0100070D0102000301FF0100070D0102000301FF0100070D0102000301FF0100070D0102000301FF0100070D0102000301FF0100070D0102000301FF0100070D0102000301FF0100070D0102000301FF0100070D0102000301FF0100070D0102000301FF0100070D0102000301000900015555555555555555B80202000301FF0100070D0102000301FF0100070D0102000301FF0100070D0102000301FF0100070D0102000301000900015555555555555555B80202000301000900015555555555555555B80202000301000900015555555555555555B80202000301FF0100070D0102000301FF0100070D0102000301FF0100070D0102000301FF0100070D0102000301FF0100070D0102000301FF0100070D0102000301FF0100070D0102000301000900015555555555555555B80202000301000900015555555555555555B80202000301FF0100070D0102000301FF0100070D0102000301FF0100070D0102000301FF0100070D0102000301FF0100070D0102000301FF0100070D0102000301FF0100070D0102000301FF0100070D0102000301FF0100070D0102000301FF0100070D0102000301FF0100070D0102000301FF0100070D0102000301000900015555555555555555B80202000301000900015555555555555555B80202000301FF0100070D0102000301FF0100070D0102000301000900015555555555555555B80202000301000900015555555555555555B80202000301FF0100070D0102000301FF0100070D0102000301FF0100070D0102000301FF0100070D0102000301FF0100070D0102000301FF0100070D0102000301FF0100070D0102000301FF0100070D0102000301FF0100070D0102000301FF0100070D0102000301FF0100070D0102000301000900015555555555555555B80202000301FF0100070D0102000301000900015555555555555555B80202000301FF0100070D0102000301000900015555555555555555B80202000301FF0100070D0102000301FF0100070D0102000301000900015555555555555555B80202000301FF0100070D0102000301FF0100070D0102000301FF0100070D0102000301000900015555555555555555B80202000301000900015555555555555555B80202000301000900015555555555555555B80202000301FF0100070D0102000301FF0100070D0102000301FF0100070D0102000301FF0100070D0102000301FF0100070D0102000301FF0100070D0102000301FF0100070D0102000301FF0100070D0102000301FF0100070D0102000301FF0100070D0102000301FF0100070D0102000301FF0100070D0102000301FF0100070D0102000301FF0100070D0102000301FF0100070D0102000301FF0100070D0102000301FF0100070D0102000301FF0100070D0102000301FF0100070D0102000301FF0100070D0102000301FF0100070D0102000301FF0100070D0102000301FF0100070D0102000301FF0100070D0102000301FF0100070D0102000301FF0100070D0102000301FF0100070D0102000301FF0100070D0102000301FF0100070D0102000301FF0100070D0102000301FF0100070D0102000301FF0100070D0102000301FF0100070D0102000301FF0100070D0102000301FF0100070D0102000301FF0100070D0102000301FF0100070D0102000301FF0100070D0102000301FF0100070D0102000301FF0100070D0102000301FF0100070D0102000301FF0100070D0102000301FF0100070D0102000301000900015555555555555555B80202000301FF0100070D0102000301FF0100070D0102000301FF0100070D0102000301FF0100070D0102000301FF0100070D0102000301FF0100070D0102000301FF0100070D0102000301FF0100070D0102000301FF0100070D0102000301FF0100070D0102000301FF0100070D0102000301FF0100070D0102000301FF0100070D0102000301FF0100070D0102000301FF0100070D0102000301FF0100070D0102000301FF0100070D0102000301FF0100070D0102000301FF0100070D0102000301FF0100070D0102000301FF0100070D0102000301FF0100070D0102000301FF0100070D0102000301FF0100070D0102000301FF0100070D0102000301FF0100070D0102000301FF0100070D0102000301FF0100070D0102000301FF0100070D0102000301FF0100070D0102000301FF0100070D0102000301FF0100070D0102000301FF0100070D0102000301FF0100070D0102000301FF0100070D0102000301FF0100070D0102000301FF0100070D0102000301FF0100070D0102000301FF0100070D0102000301FF0100070D01020003010009
