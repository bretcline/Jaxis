using System;
using System.Collections.Generic;

namespace Jaxis.DrinkInventory.Reporting.DataInterfaces
{
    public partial interface IUserGroup : IDomainObject
    {
        IOrganization Organization { get; set; }    // <- Owner Organization

        List<Guid> AreaIds { get; set; }
        List<Guid> OrganizationIds { get; set; }
        List<Guid> UserIds { get; set; }

        List<IArea> Areas { get; set; }
        List<IOrganization> Organizations { get; set; }
        List<IUser> Users { get; set; }
    }
}
