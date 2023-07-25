using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using Jaxis.Interfaces;

namespace CloudAlerts
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "ICloudNotification" in both code and config file together.
    [ServiceContract]
    public interface ICloudNotification
    {
        [OperationContract]
        void PushAlert( AlertMessage _alert);

        //[OperationContract]
        //void PushMailMessage(MailMessage _message);
    }


    // Use a data contract as illustrated in the sample below to add composite types to service operations.
    [DataContract]
    public class AlertMessage
    {
        //[DataMember]
        //public Guid SiteID { get; set; }

        //[DataMember]
        //public AlertTypes Type { get; set; }

        [DataMember]
        public string Message { get; set; }

        [DataMember]
        public List<string> Addresses { get; set; }

        [DataMember]
        public string Subject { get; set; }

    }
}
