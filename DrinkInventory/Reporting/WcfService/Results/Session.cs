using System;
using System.Runtime.Serialization;
using Jaxis.DrinkInventory.Reporting.DataInterfaces;

namespace Jaxis.DrinkInventory.Reporting.WcfService.Results
{
    [DataContract]
    public class Session
    {
        public Session()
        {
        }

        public Session(ISession _businessSession)
        {
            SessionId = _businessSession.SessionId;
            ModifiedOn = _businessSession.ModifiedOn;
            UserName = _businessSession.User.UserName;
        }

        [DataMember]
        public Guid SessionId { get; set; }

        [DataMember]
        public DateTime ModifiedOn { get; set; }

        [DataMember]
        public string UserName { get; set; }
    }
}
