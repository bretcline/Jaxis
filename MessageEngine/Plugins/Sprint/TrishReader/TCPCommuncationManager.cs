using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Jaxis.Util.Log4Net;

namespace Jaxis.Readers.Trish
{
    public class TCPCommuncationManager : ICommunicationManager
    {
        private const int BUFFER_SIZE = 4096;
        private const string LOG_TYPE = "TrishTCP";

        protected StringBuilder m_Data = new StringBuilder();
        private bool m_Stop;
        private TcpClient m_TcpClient;
        private IAsyncResult m_SocketResult = null;

        string m_IPAddress = string.Empty;
        private int m_Port;

        public TCPCommuncationManager(string _ipAddress, string _port)
        {
            m_IPAddress = _ipAddress;
            m_Port = Convert.ToInt32(_port);
        }

        public string Data
        {
            get
            {
                string rc = string.Empty;
                lock (m_Data)
                {
                    rc = m_Data.ToString();
                    m_Data.Clear();
                }
                return rc;
            }

            set
            {
                lock (m_Data)
                {
                    m_Data.Append(value);
                }
            }
        }

        public bool Open()
        {
            bool rc = false;
            rc = Log.Wrap<bool>(LOG_TYPE, "TCPCommuncationManager::Open", LogType.Debug, true, () =>
            {
                var e = new IPEndPoint(IPAddress.Parse(m_IPAddress), m_Port);

                m_TcpClient = new TcpClient( );
                m_TcpClient.Connect( e );

                Log.Debug(LOG_TYPE, "m_TcpClient Connected");

                NetworkStream ns = m_TcpClient.GetStream();

                //byte[] buffer = new byte[BUFFER_SIZE];
                //ns.Read(buffer, 0, BUFFER_SIZE);
                //Log.Debug( Encoding.ASCII.GetString( buffer ) );
                //Log.Debug( "Send UID");
                //byte[] data = Encoding.ASCII.GetBytes("root");
                //ns.Write(data, 0, data.Length);
                //Thread.Sleep( 1000 );
                
                //buffer = new byte[BUFFER_SIZE];
                //ns.Read(buffer, 0, BUFFER_SIZE);
                //Log.Debug(Encoding.ASCII.GetString(buffer));

                //Log.Debug("Send PWD");
                //data = Encoding.ASCII.GetBytes("dbps");
                //ns.Write(data, 0, data.Length);

                return true;
            });
            return rc;
        }

        public bool Close()
        {
            bool rc = false;
            m_TcpClient.Close();
            rc = true;
            return rc;
        }

        public void WriteData(string _data)
        {
            try
            {
                if (!m_TcpClient.Connected)
                {
                    Open( );
                }
                if (m_TcpClient.Connected)
                {
                    NetworkStream ns = m_TcpClient.GetStream();

                    byte[] data = Encoding.ASCII.GetBytes(_data);
                    ns.Write(data, 0, data.Length);

                    byte[] received = new byte[BUFFER_SIZE];
                    ns.BeginRead(received, 0, 0, new AsyncCallback(OnBeginRead), ns);
                }

            }
            catch (Exception err)
            {
                Log.WriteException("TrishReader-TCP::WriteData( )", err);
            }
        }

        private void OnBeginRead(IAsyncResult ar)
        {
            try
            {
                NetworkStream ns = (NetworkStream)ar.AsyncState;
                byte[] received = new byte[BUFFER_SIZE];

                ns.EndRead(ar);

                while (ns.DataAvailable)
                {
                    ns.Read(received, 0, BUFFER_SIZE);
                    Data = Encoding.ASCII.GetString(received).Trim();
                    received = new byte[BUFFER_SIZE];
                }
            }
            catch (Exception err)
            {
                Log.WriteException( "TrishReader-TCP::OnBeginRead( )", err);
            }
        }
    }
}