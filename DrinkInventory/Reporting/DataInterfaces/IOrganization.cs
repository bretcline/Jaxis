using System;
using System.Collections.Generic;

namespace Jaxis.DrinkInventory.Reporting.DataInterfaces
{
    public partial interface IOrganization : IDomainObject
    {
        List<Guid> UserGroupIds { get; set; }
        List<IUserGroup> UserGroups { get; set; }
    }
}
