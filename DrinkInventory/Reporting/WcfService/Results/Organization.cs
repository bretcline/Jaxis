using System;
using System.Runtime.Serialization;
using Jaxis.DrinkInventory.Reporting.DataInterfaces;

namespace Jaxis.DrinkInventory.Reporting.WcfService.Results
{
    [DataContract]
    public class Organization
    {
        public Organization()
        {
        }

        public Organization(IOrganization _busOrganization)
        {
            OrganizationId = _busOrganization.OrganizationId;
            ModifiedOn = _busOrganization.ModifiedOn;
            ShortName = _busOrganization.ShortName;
            Name = _busOrganization.Name;
            ParentId = _busOrganization.ParentId;
        }

        [DataMember]
        public Guid OrganizationId { get; private set; }

        [DataMember]
        public DateTime ModifiedOn { get; private set; }

        [DataMember]
        public string ShortName { get; set; }

        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public Guid? ParentId { get; set; }

        public void CopyTo(IOrganization _location)
        {
            _location.Name = Name;
            _location.ParentId = ParentId;
            _location.ShortName = ShortName;
        }
    }
}
