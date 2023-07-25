using System;
using Jaxis.Interfaces;
using Jaxis.Interfaces.Tags;

namespace Jaxis.MessageLibrary
{
    public class TagRead : BaseMessage, ITagRead
    {
        #region ITagRead Members

        public double SignalStrength { get; set; }

        public string DeviceID { get; set; }

        public string TagID { get; set; }

        #endregion ITagRead Members
    }

    public class AlertNotification : BaseMessage, IAlertMessage
    {
        public AlertNotification( )
        {

        }

        public AlertNotification( string _message, DeliveryTypes _type )
        {
            AlertMessage = _message;
            DeliveryType = _type;
        }

        public string AlertMessage { get; set; }

        public DeliveryTypes DeliveryType{ get; set; }

        public AlertClasses AlertClass { get; set; }

        public AlertTypes AlertType {get; set; }
    }
}