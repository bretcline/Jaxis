using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using Jaxis.DrinkInventory.Reporting.DataInterfaces;

namespace Jaxis.DrinkInventory.Reporting.Data.POCO
{
    [DataContract]
    public class PourUpdate : IPourUpdate
    {
        [DataMember]
        public Guid PourId { get; set; }
        [DataMember]
        public Guid? POSTicketItemId { get; set; }
        [DataMember]
        public int? Status { get; set; }
    }
}
