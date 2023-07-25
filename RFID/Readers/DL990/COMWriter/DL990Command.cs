using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO.Ports;

namespace COMWriter
{
    [Flags]
    public enum DL990CommandType
    {
        ReadSerialNumber = 0x83,
        ISO_Inventory = 0x10,
        ISO_GetSystemInformation = 0x2b, //0x1B,
        ISO_Write = 0x12,
        ISO_Read = 0x11,
    }

	public class DL990Command
    {
        // SOF      RFU     DST     CMD     OPT     Len(LSB)   Len(MSB) P-- CS(LSB) CS(MSB)
        // 0x02     0x00    Destination     Options                     Parameters
        //                          Command            --   Length   -      Checksum
        public int CommandLength
        {
            get
            {
                return 6 + Data.Count;
            }
        }
        public int ISOCommandLength
        {
            get
            {
                return 5 + Data.Count;
            }
        }
        public DL990CommandType CommandType
        {
            get
            {
                //byte[] Type = new byte[1];

                //Type[0] = Command;

                //return (DL990CommandType)BitConverter.ToInt16( Type, 0 );

                return (DL990CommandType)Command;
            }
        }

        public byte[] CommandBytes
        {
            get
            {
                byte[] rc = new byte[DataLength + 5];  //2];
                rc[0] = 0x02;
                rc[1] = StationID;
                rc[2] = DataLength;
                rc[3] = Command;
                Array.Copy( Data.ToArray( ), 0, rc, 4, Data.Count );
                Array.Copy( Checksum, 0, rc, rc.Length - 2, 1 );
                rc[rc.Length - 1] = 0x03;
                return rc;
            }

            set
            {
                if( 9 < value.Length )
                {
                    byte[] Params = new byte[value.Length - 4];
                    Array.Copy( value, 4, Params, 0, Params.Length );
                    Data.AddRange( Params );
                }
                StationID = value[1];
                Command = value[3];
            }
        }

        public byte[] ISOCommandBytes
        {
            get
            {
                byte[] rc = new byte[DataLength + 4];  //2];
                rc[0] = 0x02;
                rc[1] = StationID;
                rc[2] = (byte)Data.Count;
                rc[3] = Command;
                Array.Copy(Data.ToArray(), 0, rc, 4, Data.Count);
                rc[rc.Length - 1] = 0x03;
                return rc;
            }

            set
            {
                if (9 < value.Length)
                {
                    byte[] Params = new byte[value.Length - 4];
                    Array.Copy(value, 4, Params, 0, Params.Length);
                    Data.AddRange(Params);
                }
                StationID = value[1];
                Command = value[3];
            }
        }

        public byte StationID { get; set; }
        public byte DataLength
        {
            get
            {
                return (byte)(Data.Count + 1);// + 3);
            }
        }
        public byte Command { get; set; }
        public List<byte> Data { get; set; }
        public byte[] Checksum
        {
            get
            {
                int rc = 0;
                byte[] Len = BitConverter.GetBytes( Data.Count );
                rc = (int)StationID ^ (int)DataLength ^ (int)Command;
                Array.ForEach( Len, L => rc = rc ^ (int)L );
                Data.ForEach( B => rc += (int)B );
                return BitConverter.GetBytes( (Int16)rc );
            }
        }
        public byte ETX { get; set; }

        public DL990Command( byte[] _Bytes )
        {
            //StationID = 0;
            Data = new List<byte>( );
            CommandBytes = _Bytes;
        }

        public DL990Command( DL990CommandType _Type )
        {
            Init( _Type, 0x00, null );
        }


        public DL990Command( DL990CommandType _Type, byte _Options )
        {
            Init( _Type, _Options, null );
        }


        public DL990Command( DL990CommandType _Type, byte[] _Parameters )
        {
            Init( _Type, 0x00, _Parameters );
        }


        public DL990Command( DL990CommandType _Type, byte _Options, byte[] _Parameters )
        {
            Init( _Type, _Options, _Parameters );
        }

        protected void Init( DL990CommandType _Type, byte _Options, byte[] _Parameters )
        {
            StationID = 0x02;
            StationID = 0x00;

            Command = (byte)_Type;

            if( null != _Parameters && 0 < _Parameters.Length )
            {
                Data = new List<byte>( _Parameters );
            }
            else
            {
                Data = new List<byte>( );
            }

            ETX = 0x03;
        }
    }
}
