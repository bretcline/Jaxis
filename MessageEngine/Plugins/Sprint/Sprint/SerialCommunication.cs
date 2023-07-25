using System;
using System.Text;
using System.IO.Ports;
using System.Threading;

namespace PCComm
{
    public interface ICommunicationManager
    {
        string Data { get; set; }
        bool Open();
        bool Close();
        void WriteData( string _data );
    }

    public class SerialCommunication : ICommunicationManager
    {
        #region Manager Enums
        /// <summary>
        /// enumeration to hold our transmission types
        /// </summary>
        public enum TransmissionType 
        { 
            Text, 
            Hex 
        }

       #endregion

        #region Manager Variables
        private SerialPort comPort = new SerialPort();
        #endregion

        #region Manager Properties
        /// <summary>
        /// Property to hold the BaudRate
        /// of our manager class
        /// </summary>
        public int BaudRate { get; set; }

        /// <summary>
        /// property to hold the Parity
        /// of our manager class
        /// </summary>
        public Parity Parity { get; set; }

        /// <summary>
        /// property to hold the StopBits
        /// of our manager class
        /// </summary>
        public StopBits StopBits { get; set; }

        /// <summary>
        /// property to hold the DataBits
        /// of our manager class
        /// </summary>
        public int DataBits { get; set; }

        /// <summary>
        /// property to hold the PortName
        /// of our manager class
        /// </summary>
        public string PortName { get; set; }

        /// <summary>
        /// property to hold our TransmissionType
        /// of our manager class
        /// </summary>
        public TransmissionType CurrentTransmissionType { get; set; }


        protected string m_Data = string.Empty;
        public string Data
        {
            get
            {
                string rc = string.Empty;
                lock( m_Data )
                {
                    rc = m_Data;
                    m_Data = string.Empty;
                }
                return rc;
            }

            set
            {
                lock( m_Data )
                {
                    m_Data = value;
                }
            }
        }
        #endregion

        #region Manager Constructors
        /// <summary>
        /// Constructor to set the properties of our Manager Class
        /// </summary>
        /// <param name="baud">Desired BaudRate</param>
        /// <param name="par">Desired Parity</param>
        /// <param name="sBits">Desired StopBits</param>
        /// <param name="dBits">Desired DataBits</param>
        /// <param name="name">Desired PortName</param>
        public SerialCommunication( int baud, Parity par, StopBits sBits, int dBits, string name )
        {
            BaudRate = baud;
            Parity = par;
            StopBits = sBits;
            DataBits = dBits;
            PortName = name;
            comPort.DataReceived += new SerialDataReceivedEventHandler(AsyncDataReceived);
        }

        /// <summary>
        /// Comstructor to set the properties of our
        /// serial port communicator to nothing
        /// </summary>
        public SerialCommunication()
        {
            PortName = "COM1";
            comPort.DataReceived += new SerialDataReceivedEventHandler(AsyncDataReceived);
        }
        #endregion

        #region WriteData
        public void WriteData(string msg)
        {
            if( !( comPort.IsOpen == true ) )
            {
                comPort.Open( );
            }

            switch (CurrentTransmissionType)
            {
                case TransmissionType.Text:
                {
                    comPort.Write( msg );
                    break;
                }
                case TransmissionType.Hex:
                {
                    try
                    {
                        byte[] newMsg = HexToByte( msg );
                        comPort.Write( newMsg, 0, newMsg.Length );
                    }
                    catch
                    {
                        //display error message
                    }
                    finally
                    {
                    }
                    break;
                }
                default:
                {    
                    comPort.Write( msg );
                    break;
                }
            }
        }
        #endregion

        #region HexToByte
        /// <summary>
        /// method to convert hex string into a byte array
        /// </summary>
        /// <param name="msg">string to convert</param>
        /// <returns>a byte array</returns>
        private byte[] HexToByte(string msg)
        {
            msg = msg.Replace(" ", "");
            byte[] comBuffer = new byte[msg.Length / 2];
            for( int i = 0; i < msg.Length; i += 2 )
            {
                comBuffer[i / 2] = (byte)Convert.ToByte( msg.Substring( i, 2 ), 16 );
            }
            return comBuffer;
        }
        #endregion

        #region ByteToHex
        /// <summary>
        /// method to convert a byte array into a hex string
        /// </summary>
        /// <param name="comByte">byte array to convert</param>
        /// <returns>a hex string</returns>
        private string ByteToHex(byte[] comByte)
        {
            StringBuilder builder = new StringBuilder(comByte.Length * 3);
            foreach( byte data in comByte )
            {
                builder.Append( Convert.ToString( data, 16 ).PadLeft( 2, '0' ).PadRight( 3, ' ' ) );
            }
            return builder.ToString( ).ToUpper( );
        }
        #endregion

        #region Open
        public bool Open()
        {
            bool rc = false;
            try
            {
                if( comPort.IsOpen == true )
                {
                    comPort.Close( );
                }

                comPort.BaudRate = BaudRate;
                comPort.DataBits = DataBits;
                comPort.StopBits = StopBits;
                comPort.Parity = Parity;
                comPort.PortName = PortName;

                comPort.Open();
                rc = true;
            }
            catch 
            {
            }
            return rc;
        }

        public bool Close( )
        {
            bool rc = false;
            try
            {
                if (comPort.IsOpen == true)
                {
                    comPort.Close();
                }
                rc = true;
            }
            catch (Exception)
            {
                
                throw;
            }
            return rc;
        }

        #endregion

        #region AsyncDataReceived
        /// <summary>
        /// method that will be called when theres data waiting in the buffer
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void AsyncDataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            switch (CurrentTransmissionType)
            {
                case TransmissionType.Hex:
                {
                    int bytes = comPort.BytesToRead;
                    byte[] comBuffer = new byte[bytes];
                    comPort.Read( comBuffer, 0, bytes );
                    break;
                }
                default:
                {
                    Data += comPort.ReadExisting( );
                    break;
                }
            }
        }
        #endregion
    }
}
