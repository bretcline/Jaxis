using System;
using System.Runtime.Serialization;
using Jaxis.DrinkInventory.Reporting.DataInterfaces;

namespace Jaxis.DrinkInventory.Reporting.WcfService.Results
{
    [DataContract]
    public class Area
    {
        public Area() { }

        public Area(IArea _businessArea)
        {
            AreaId = _businessArea.AreaId;
            Name = _businessArea.Name;
            ShortName = _businessArea.ShortName;
            Controller = _businessArea.Controller;
        }

        [DataMember]
        private Guid AreaId { get; set; }

        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public string ShortName { get; set; }

        [DataMember]
        public string Controller { get; set; }
    }
}
