using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Jaxis.BeverageManagement.Plugin.svcCloudNotification;
using Jaxis.Engine.Base.Device;
using Jaxis.Interfaces;
using Jaxis.Inventory.Data;
using Jaxis.MessageLibrary;
using Jaxis.Util.Log4Net;
using JaxisExtensions;

namespace Jaxis.BeverageManagement.Plugin.Alerts
{
    public class CloudAlerts : BaseDevice, IConsumer
    {
        private readonly Dictionary<AlertTypes, List<string>> m_Alerts = new Dictionary<AlertTypes, List<string>>();
        private string m_Organization = string.Empty;
        private CloudNotificationClient m_CloudService;

        public CloudAlerts(IDeviceConfig _config)
            : base( _config )
        {
            Config.Type = DeviceType.DataConsumer;
            State = DeviceState.Stopped;

            Config.Type = DeviceType.DataConsumer;
            State = DeviceState.Stopped;

            BuildAlerList(_config.GetAddresses());
        }

        private void BuildAlerList(string _getAddresses)
        {
            var groups = _getAddresses.Split(';');
            foreach (string group in groups)
            {
                var items = new List<string>(group.Split('|'));
                if (2 == items.Count)
                {
                    var alerts = new List<string>(items[1].Split(','));
                    foreach (var alert in alerts)
                    {
                        AlertTypes alertType;
                        Enum.TryParse(alert, true, out alertType);
                        if (false == m_Alerts.ContainsKey(alertType))
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

            var org = DataManagerFactory.Get().Manage<IOrganization>().GetAll().FirstOrDefault();
            if (null != org)
            {
                m_Organization = org.Name;
            }
            m_CloudService = new svcCloudNotification.CloudNotificationClient( );

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
                    PostToCloud(msg);
                }
            }
            catch( Exception exp )
            {
                Log.WriteException( "EmailAlerts::Consume", exp );
            }

            return rc;
        }


        private void PostToCloud(IAlertMessage _alert)
        {
            try
            {
                if (m_Alerts.ContainsKey(_alert.AlertType))
                {
                    var addresses = m_Alerts[_alert.AlertType];

                    var message =  new svcCloudNotification.AlertMessage();
                    message.Addresses = new List<string>();
                    message.Addresses.AddRange( addresses );

                    var messageBuilder = new StringBuilder();
                    if (_alert is TagAlertMessage)
                    {
                        var alert = _alert as TagAlertMessage;
                        foreach (var value in alert.Parameters)
                        {
                            var name = string.Empty;
                            switch (value.Key)
                            {
                                case "TagID":
                                    {
                                        name = BLManagerFactory.Get().ManageUPCs().GetUPCNameByTagNumber(value.Value.ToString());
                                        name = string.Format("{0} - UPC: {1}{2}", value.Value, name, Environment.NewLine);
                                        break;
                                    }
                                case "DeviceID":
                                    {
                                        name = BLManagerFactory.Get().ManageLocations().GetLocationByDeviceID(value.Value.ToString());
                                        name = string.Format("{0} - Location: {1}{2}", value.Value, name, Environment.NewLine);
                                        break;
                                    }
                                case "EventTime":
                                    {
                                        name = BLManagerFactory.Get().ManageLocations().GetLocationByDeviceID(value.Value.ToString());
                                        name = string.Format("{0}{1}", value.Value, Environment.NewLine);
                                        break;
                                    }
                                default:
                                    {
                                        name = string.Format("{0}{1}", value.Value, Environment.NewLine);
                                        break;
                                    }
                            }
                            messageBuilder.Append(String.Format("{0} - {1}{2}", value.Key, name, Environment.NewLine));
                        }
                    }


                    message.Message = messageBuilder.Append(_alert.AlertMessage).ToString();
                    message.Subject = string.Format("{1} ALERT: {0}", _alert.AlertType.GetEnumDescription(), m_Organization);
                    m_CloudService.PushAlert( message );
                }
            }
            catch( Exception exp )
            {
                Log.WriteException( "EmailAlerts:: SendEmailAlertMsg", exp );
            }
        }
    }
}