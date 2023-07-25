using System;
using System.Collections.Generic;
using Jaxis.Interfaces;
using Jaxis.Util.Log4Net;

namespace Jaxis.Engine.Base.Device
{
    public abstract class AlertableProducerDevice : BaseProducerDevice
    {
        protected  Dictionary<AlertTypes, bool> m_Alerts = new Dictionary<AlertTypes, bool>();

        public AlertableProducerDevice(IDeviceConfig _config)
            : base( _config )
        {
        }

        protected void SendAlert<T>(T _alertMessage, AlertTypes _type, string _message, bool _forceAlert = false ) where T : IAlertMessage
        {
            try
            {
                if (!m_Alerts.ContainsKey(_type))
                {
                    m_Alerts[_type] = false;
                }

                if (!m_Alerts[_type] || true == _forceAlert )
                {
                    _alertMessage.AlertType = _type;
                    _alertMessage.ReadTime = DateTime.Now;

                    _alertMessage.AlertMessage = _message;

                    ProduceMessage(_alertMessage);

                    m_Alerts[_type] = true;
                }
            }
            catch (Exception exp)
            {
                Log.WriteException("AlertableProducerDevice:: SendAlert", exp);
            }
        }

        protected bool AlertStatus( AlertTypes _type )
        {
            if (!m_Alerts.ContainsKey(_type))
            {
                m_Alerts[_type] = false;
            }
            return m_Alerts[_type];
        }

        protected bool ClearAlert(AlertTypes _type)
        {

            bool rc = false;
            if (!m_Alerts.ContainsKey(_type))
            {
                m_Alerts[_type] = false;
            }
            rc = m_Alerts[_type];
            m_Alerts[_type] = false;
            return rc;
        }
    }
}