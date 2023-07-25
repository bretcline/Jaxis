using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net;
using System.Net.Mail;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using Jaxis.Engine.Base.Device;
using Jaxis.Interfaces;
using Jaxis.Util.Log4Net;
using JaxisExtensions;

namespace Jaxis.BeverageManagement.Plugin.Alerts
{
    public class UdpAlerts : BaseDevice, IConsumer
    {
        //private Dictionary<AlertTypes, List<string>> m_Alerts = new Dictionary<AlertTypes, List<string>>();
        private UdpUser m_Client = null;

        public UdpAlerts(IDeviceConfig _Config)
            : base( _Config )
        {
            Config.Type = DeviceType.DataConsumer;
            State = DeviceState.Stopped;
        }

        override public void Start( )
        {
            Config.State = DeviceState.Started;

            m_Client = UdpUser.ConnectTo(m_DeviceConfig.GetUdpHost(), m_DeviceConfig.GetUdpPort() );
        }

        override public void Stop( )
        {
            Config.State = DeviceState.Stopped;
        }

        public override string Consume( IMessage _message )
        {
            string rc = null;

            try
            {
                var msg = _message as IAlertMessage;
                if( null != msg )
                {
                    SendAlertMsg( msg );
                }
            }
            catch( Exception exp )
            {
                Log.WriteException( "EmailAlerts::Consume", exp );
            }

            return rc;
        }


        private void SendAlertMsg(IAlertMessage _alert)
        {
            try
            {
               // if (m_Alerts.ContainsKey(_alert.AlertType))
                {
               //     var addresses = m_Alerts[_alert.AlertType];
                    {
                        lock (m_Client)
                        {
                            m_Client.Send(_alert.AlertMessage);
                        }
                    }
                }
            }
            catch( Exception exp )
            {
                Log.WriteException( "EmailAlerts:: SendEmailAlertMsg", exp );
            }
        }
    }




    public struct Received
    {
        public IPEndPoint Sender;
        public string Message;
    }

    abstract class UdpBase
    {
        protected UdpClient Client;

        protected UdpBase()
        {
            Client = new UdpClient();
        }
    }

    //Client
    class UdpUser : UdpBase
    {
        private UdpUser() { }

        public static UdpUser ConnectTo(string hostname, int port)
        {
            var connection = new UdpUser();
            connection.Client.Connect(hostname, port);
            return connection;
        }

        public void Send(string message)
        {
            var datagram = Encoding.ASCII.GetBytes(message);
            Client.Send(datagram, datagram.Length);
        }
    }
}