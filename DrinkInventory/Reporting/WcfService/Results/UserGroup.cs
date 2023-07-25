using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using Jaxis.DrinkInventory.Reporting.DataInterfaces;

namespace Jaxis.DrinkInventory.Reporting.WcfService.Results
{
    [DataContract]
    public class UserGroup
    {
        public UserGroup()
        {
        }

        public UserGroup(IUserGroup _group)
        {
            UserGroupId = _group.UserGroupId;
            ModifiedOn = _group.ModifiedOn;
            Name = _group.Name;

            // Locations = (from l in _group.Locations.ToList() select new Location(l)).ToList();
        }

        public void CopyTo(IUserGroup _userGroup)
        {
            _userGroup.Name = Name;
        }

        [DataMember]
        public Guid UserGroupId { get; set; }

        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public DateTime ModifiedOn { get; set; }

        [DataMember]
        public List<Organization> Locations { get; set; }
    }
}
