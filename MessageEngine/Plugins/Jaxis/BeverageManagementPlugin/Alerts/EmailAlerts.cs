using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using Jaxis.Engine.Base.Device;
using Jaxis.Interfaces;
using Jaxis.Inventory.Data;
using Jaxis.MessageLibrary;
using Jaxis.Util.Log4Net;
using JaxisExtensions;

namespace Jaxis.BeverageManagement.Plugin.Alerts
{
    /*
    <DeviceConfig>
      <AssemblyName>Jaxis.BeverageManagement.Plugin.dll</AssemblyName>
      <AssemblyType>Jaxis.BeverageManagement.Plugin.EmailAlerts</AssemblyType>
      <AssemblyVersion>1.0</AssemblyVersion>
      <ID>458589456</ID>
      <Name>Email Alerts</Name>
      <Type>DataProducerConsumer</Type>
      <State>Started</State>
      <ConsumerMessageType>1</ConsumerMessageType>
      <ProducerMessageType>0</ProducerMessageType>
      <Options>
        <string></string>  --Host
        <string></string>  --Port
        <string></string>  --Enable SSL (true/false)
        <string></string>  --From Account
        <string></string>  --Password
        <string></string> -- email address,(1-n)|AlertType,(1-n);
        <string></string>  --DeliveryMethod
      </Options>
    </DeviceConfig>
    */


    public class EmailAlerts : BaseDevice, IConsumer
    {
        private Dictionary<AlertTypes, List<string>> m_Alerts = new Dictionary<AlertTypes, List<string>>();
        private MailAddress m_From = null;
        private SmtpClient m_SMTPClient = null;
        private string m_Organization = string.Empty;

        public EmailAlerts( IDeviceConfig _Config )
            : base( _Config )
        {
            Config.Type = DeviceType.DataConsumer;
            State = DeviceState.Stopped;

            m_From = new MailAddress(_Config.GetFromAccount());
            BuildAlerList( _Config.GetAddresses( ) );

            SmtpDeliveryMethod deliveryMethod;
            Enum.TryParse(_Config.GetDeliveryMethod(), true, out deliveryMethod);

            m_SMTPClient = new SmtpClient();
            m_SMTPClient.Credentials = new NetworkCredential(m_From.Address, _Config.GetPassword());
            m_SMTPClient.Port = _Config.GetPort();
            m_SMTPClient.Host = _Config.GetHost();
            m_SMTPClient.EnableSsl = _Config.GetEnabledSSL();
            //m_SMTPClient.Timeout = 5;
        }

        private void BuildAlerList(string _getAddresses)
        {
            var groups = _getAddresses.Split(';');
            foreach (string group in groups)
            {
                var items = new List<string>( group.Split('|') );
                if( 2 == items.Count )
                {
                    var alerts = new List<string>( items[1].Split(',') );
                    foreach (var alert in alerts)
                    {
                        AlertTypes alertType;
                        Enum.TryParse(alert, true, out alertType);
                        if( false == m_Alerts.ContainsKey( alertType ) )
                        {
                            m_Alerts.Add(alertType, new List<string>());
                        }
                        m_Alerts[alertType].AddRange(items[0].Split(','));
                    }
                }
            }
        }

        override public void Start( )
        {
            Config.State = DeviceState.Started;

            //var org = DataManagerFactory.Get().Manage<IOrganization>().GetAll().FirstOrDefault();
            //if (null != org)
            //{
            //    m_Organization = org.Name;
            //}
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
                    SendEmailAlertMsg( msg );
                }
            }
            catch( Exception exp )
            {
                Log.WriteException( "EmailAlerts::Consume", exp );
            }

            return rc;
        }


        private void SendEmailAlertMsg(IAlertMessage _alert)
        {
            try
            {
                if (m_Alerts.ContainsKey(_alert.AlertType))
                {
                    var addresses = m_Alerts[_alert.AlertType];

                    using (var message = new MailMessage(m_From, new MailAddress(addresses[0])))
                    {
                        for (int i = 1; i < addresses.Count; ++i)
                        {
                            message.To.Add(new MailAddress(addresses[i]));
                        }
                        var messageBuilder = new StringBuilder();
                        //if (_alert is TagAlertMessage)
                        //{
                        //    var alert = _alert as TagAlertMessage;
                        //    foreach (var value in alert.Parameters)
                        //    {
                        //        var name = string.Empty;
                        //        switch (value.Key)
                        //        {
                        //            case "TagID":
                        //            {
                        //                name = BLManagerFactory.Get().ManageUPCs().GetUPCNameByTagNumber( value.Value.ToString());
                        //                name = string.Format("{0} - UPC: {1}{2}", value.Value, name, Environment.NewLine);
                        //                break;
                        //            }
                        //            case "DeviceID":
                        //            {
                        //                name = BLManagerFactory.Get().ManageLocations().GetLocationByDeviceID(value.Value.ToString());
                        //                name = string.Format("{0} - Location: {1}{2}", value.Value, name, Environment.NewLine);
                        //                break;
                        //            }
                        //            case "EventTime":
                        //            {
                        //                name = BLManagerFactory.Get().ManageLocations().GetLocationByDeviceID(value.Value.ToString());
                        //                name = string.Format("{0}{1}", value.Value, Environment.NewLine);
                        //                break;
                        //            }
                        //            default:
                        //            {
                        //                name = string.Format("{0}{1}", value.Value, Environment.NewLine);
                        //                break;
                        //            }
                        //        }
                        //        messageBuilder.Append(String.Format("{0} - {1}{2}", value.Key, name, Environment.NewLine));
                        //    }
                        //}
                        message.Body = messageBuilder.Append( _alert.AlertMessage ).ToString();
                        message.BodyEncoding = Encoding.UTF8;
                        message.Subject = string.Format("{1} ALERT: {0}", _alert.AlertType.GetEnumDescription(), m_Organization );
                        message.SubjectEncoding = Encoding.UTF8;
                        lock (m_SMTPClient)
                        {
                            m_SMTPClient.Send(message);
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
}