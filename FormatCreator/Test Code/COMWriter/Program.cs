using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO.Ports;

namespace COMWriter
{

    public class RFIDCom
    {
        Dictionary<PUKCommandType, PUKCommand> m_Response = new Dictionary<PUKCommandType, PUKCommand>( );
        bool m_Reading = false;
        System.IO.Ports.SerialPort m_Port = null;

        static void Main( string[] args )
        {
            RFIDCom Com = new RFIDCom( );

            Console.WriteLine( "Open" );
            Com.Open( );

            Console.WriteLine( "Send" );
            PUKCommand Resp = Com.SendMessage( new PUKCommand( PUKCommandType.ReadVersion ) );

            //Resp = Com.SendMessage( new RFIDCommand( CommandType.ResetPUK ) );
            //Com.Close( );
            //Com.Open( );

//            Resp = Com.SendMessage( new RFIDCommand( CommandType.ReadSerialNumber ) );

//            Console.WriteLine( BitConverter.ToString( Resp.Parameters.ToArray( ) ) );
            byte ISO15693Options = 0x01;


            ISO15693Options = 0x76;
            Resp = Com.SendMessage( new PUKCommand( PUKCommandType.CarrierOnOff, ISO15693Options ) );

            Resp = Com.SendMessage( new PUKCommand( PUKCommandType.ProgramConfigurationBytes, ISO15693Options, new byte[] { 0x0f, 0x82 } ) );


            byte Error = 0xFF;
            while( 0xFF == Error )
            {
                Resp = Com.SendMessage( new PUKCommand( PUKCommandType.ISO_Inventory, ISO15693Options ) );
//                Resp = Com.SendMessage( new PUKCommand( PUKCommandType.TIRIS_Read ) );
                Error = Resp.Options;
            }
            Com.ProcessResponse( Resp.CommandType, Resp, Console.WriteLine );
            Resp.Parameters.ForEach( C => Console.Write( (char)C ) );
            Console.WriteLine( );
            Console.WriteLine( BitConverter.ToString( Resp.Parameters.ToArray( ) ) );

            List<byte> MList = new List<byte>( ) { (byte)'B', (byte)'r', (byte)'e', (byte)'t', 0x32, 0x32, 0x32, 0x32 };
            //char[] Message = "11111111".ToCharArray( );
            //Array.ForEach( Message, c => MList.Add( (byte)c ));
            Resp = Com.SendMessage( new PUKCommand( PUKCommandType.TIRIS_Write, MList.ToArray( ) ) );

            Error = 0xFF;
            while( 0xFF == Error )
            {
                Resp = Com.SendMessage( new PUKCommand( PUKCommandType.TIRIS_Read ) );
                Error = Resp.Options;
            }
            Com.ProcessResponse( Resp.CommandType, Resp, Console.WriteLine );

            Resp.Parameters.ForEach( C => Console.Write( (char)C ) );
            Console.WriteLine( );

            Console.WriteLine( BitConverter.ToString( Resp.Parameters.ToArray( ) ) );


            Com.Close( );
        }


        public void Open( )
        {
            m_Port = new SerialPort( "COM6", 57600, Parity.None, 8, StopBits.One );
            m_Port.Open( );
            System.Threading.Thread.Sleep( 50 );
            m_Port.DataReceived += Ports_DataReceived;
        }

        public void Close( )
        {
            m_Port.Close( );
        }

        public PUKCommand SendMessage( PUKCommand _Comm )
        {
            m_Port.Write( _Comm.CommandBytes, 0, _Comm.CommandLength );
            m_Reading = true;
            while( m_Reading && m_Port.IsOpen )
            {
                System.Threading.Thread.Sleep( 75 );
            }

            System.Diagnostics.Debug.WriteLine( "SendMessage Command:" + _Comm.CommandType + "  " + (int)_Comm.CommandType );
            return m_Response[_Comm.CommandType];
        }

        void Ports_DataReceived( object sender, SerialDataReceivedEventArgs e )
        {
            try
            {
                int Offset = 0;
                System.Threading.Thread.Sleep( 35 );
                SerialPort Port = sender as SerialPort;
                int ReadCount = Port.BytesToRead;

                List<byte> MsgResp = new List<byte>( );
                byte[] Resp = new byte[ReadCount];
                int read = Port.Read( Resp, Offset, ReadCount );
                MsgResp.AddRange( Resp );

                while( read < 9 && Offset < 9 )
                {
                    Offset += read;
                    ReadCount = Port.BytesToRead;
                    Resp = new byte[ReadCount];

                    read = Port.Read( Resp, 0, ReadCount );
                    MsgResp.AddRange( Resp );
                }
                
                PUKCommand Cmd = new PUKCommand( MsgResp.ToArray( ) );
                m_Response[Cmd.CommandType] = Cmd;

                m_Reading = false;
            }
            catch( Exception ex )
            {
                System.Diagnostics.Debug.WriteLine( ex.Message );
            }
        }

        public void ProcessError( PUKCommand _ErrorPacket, Action<string> _Writer )
        {
            Console.WriteLine( "Error: " + _ErrorPacket.CommandType );
            System.Diagnostics.Debug.WriteLine( "Error" );
            foreach( byte Err in _ErrorPacket.Parameters )
            {
                _Writer( String.Format( "{0} : {1}", (PUKErrorCodes)Err, (int)Err ) );
                System.Diagnostics.Debug.WriteLine( String.Format( "{0} : {1}", (PUKErrorCodes)Err, (int)Err ) );
            }
        }

        public void ProcessResponse( PUKCommandType _Type, PUKCommand _Packet, Action<string> _Writer )
        {
            switch( _Type )
            {
                case PUKCommandType.TIRIS_Read:
                {
                    TIRISTransponderType TagType = (TIRISTransponderType)_Packet.Parameters[0];
                    Console.WriteLine( "Tag Type: " + TagType );
                    switch( TagType )
                    {
                        case TIRISTransponderType.MultiPage:
                        {
                            int PageNumber = (int)_Packet.Parameters[1];
                            int Status = (int)_Packet.Parameters[2];
                            byte[] PageData = new byte[_Packet.Parameters.Count - 2];
                            Array.Copy( _Packet.Parameters.ToArray( ), 2, PageData, 0, PageData.Length );
                            _Writer( String.Format( "Page Data: {0}", Convert.ToBase64String( PageData ) ) );
                            break;
                        }
                        default:
                        {

                            byte[] TagID = new byte[_Packet.Parameters.Count - 1];
                            Array.Copy( _Packet.Parameters.ToArray( ), 1, TagID, 0, TagID.Length );
                            _Writer( String.Format( "Tag ID: {0}", Convert.ToBase64String( TagID ) ) );
                            break;
                        }
                    }
                    break;
                }
                default:
                {
                    byte[] TagID = new byte[_Packet.Parameters.Count - 1];
                    Array.Copy( _Packet.Parameters.ToArray( ), 1, TagID, 0, TagID.Length );
                    _Writer( String.Format( "Tag ID: {0}", Convert.ToBase64String( TagID ) ) );
                    break;
                }

            }
        }
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
