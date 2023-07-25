using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO.Ports;

namespace COMWriter
{
// 020001040000000700
// 0200010400400053544C462D30373036302D3230303820202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020C109
// 02000201000100000600
// 0200010400400053544C462D30373036302D3230303820202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020C109


    public enum TIRISTransponderType
    {
        ReadOnly = 0x00,
        ReadWrite = 0x01,
        MultiPage = 0x02,
    }

    [Flags]
    public enum PUKErrorCodes 
    {
        TransponderGeneratedError = 0x01,
        DestinationNotRecognised = 0x02,
        CommandNotRecognised = 0x03,
        InvalidOptions = 0x04,
        InvalidLength = 0x05,
        InvalidChecksum = 0x06,
        NoTransponderPresent = 0x07,
        InvalidParameters = 0x08,
        WriteNotVerified = 0x09,
        WriteSerialNumberFailed = 0x20,
        BootloaderError = 0xE0,
        UndefinedError = 0xFF,
    }

    [Flags]
    public enum PUKCommandType
    {
        ReadVersion = 0x0101,
        ReadSerialNumber = 0x0104,
        CarrierOnOff = 0x0110,
        PowerSaveOn = 0x01B2,
        PowerSaveOff = 0x01B3,
        WriteSerialNumber = 0x01F0,
        ResetPUK = 0x01f1,

        ProgramConfigurationBytes = 0x01b0,

        TagIT_SIDPoll = 0x0206,

        ISO_Inventory = 0x0401,
        ISO_GetSystemInformation = 0x042B,

        TIRIS_Read = 0x0301,
        TIRIS_Write = 0x0302,

    }


    public class PUKCommand
    {
        // SOF      RFU     DST     CMD     OPT     Len(LSB)   Len(MSB) P-- CS(LSB) CS(MSB)
        // 0x02     0x00    Destination     Options                     Parameters
        //                          Command            --   Length   -      Checksum
        public int CommandLength
        {
            get
            {
                return 9 + Parameters.Count;
            }
        }
        public PUKCommandType CommandType
        {
            get
            {
                byte[] Type = new byte[2];

                Type[0] = Command;
                Type[1] = Destination;

                return (PUKCommandType)BitConverter.ToInt16( Type, 0 );
            }
        }
        public byte[] CommandBytes
        {
            get
            {
                byte[] rc = new byte[Parameters.Count + 9];
                rc[0] = StartOfPacket;
                rc[1] = RFU;
                rc[2] = Destination;
                rc[3] = Command;
                rc[4] = Options;
                Array.Copy( Length, 0, rc, 5, 2 );
                Array.Copy( Parameters.ToArray( ), 0, rc, 7, Parameters.Count );
                Array.Copy( Checksum, 0, rc, Parameters.Count + 7, 2 );

                return rc;
            }

            set
            {
                if( 9 < value.Length )
                {
                    byte[] Params = new byte[value.Length - 9];
                    Array.Copy( value, 7, Params, 0, Params.Length );
                    Parameters.AddRange( Params );
                }
                StartOfPacket = value[0];
                RFU = value[1];
                Destination = value[2];
                Command = value[3];
                Options = value[4];
            }
        }


        public byte StartOfPacket { get; set; }
        public byte RFU { get; set; }
        public byte Destination { get; set; }
        public byte Command { get; set; }
        public byte[] Length
        {
            get
            {
                return BitConverter.GetBytes( Parameters.Count );
            }
        }
        public byte Options { get; set; }
        public List<byte> Parameters { get; set; }
        public byte[] Checksum
        {
            get
            {
                int rc = 0;
                byte[] Len = BitConverter.GetBytes( Parameters.Count );
                rc += (int)StartOfPacket + (int)RFU + (int)Destination + (int)Command + (int)Options;
                Array.ForEach( Len, L => rc += (int)L );
                Parameters.ForEach( B => rc += (int)B );
                return BitConverter.GetBytes( (Int16)rc );
            }
        }

        public PUKCommand( byte[] _Bytes )
        {
            Parameters = new List<byte>( );
            CommandBytes = _Bytes;
        }

        public PUKCommand( PUKCommandType _Type )
        {
            Init( _Type, 0x00, null );
        }


        public PUKCommand( PUKCommandType _Type, byte _Options )
        {
            Init( _Type, _Options, null );
        }


        public PUKCommand( PUKCommandType _Type, byte[] _Parameters )
        {
            Init( _Type, 0x00, _Parameters );
        }


        public PUKCommand( PUKCommandType _Type, byte _Options, byte[] _Parameters )
        {
            Init( _Type, _Options, _Parameters );
        }

        protected void Init( PUKCommandType _Type, byte _Options, byte[] _Parameters )
        {
            StartOfPacket = 0x02;
            RFU = 0x00;
            byte[] Type = BitConverter.GetBytes( (Int16)_Type );

            Destination = Type[1];
            Command = Type[0];

            Options = _Options;
            if( null != _Parameters && 0 < _Parameters.Length )
            {
                Parameters = new List<byte>( _Parameters );
            }
            else
            {
                Parameters = new List<byte>( );
            }
        }
    }

}
