using System;
using System.Runtime.Serialization;
using Jaxis.DrinkInventory.Reporting.DataInterfaces;

namespace Jaxis.DrinkInventory.Reporting.WcfService.Results
{
    [DataContract]
    public class Section
    {
        public Section () { }

        public Section(ISection _businessSection)
        {
            SectionId = _businessSection.SectionId;
            AreaId = _businessSection.AreaId;
            Name = _businessSection.Name;
            ShortName = _businessSection.ShortName;
        }

        [DataMember]
        public Guid SectionId { get; set; }

        [DataMember]
        public string ShortName { get; set; }

        [DataMember]
        public string Name { get; set; }
        
        [DataMember]
        public Guid AreaId { get; set; }
    }
}
