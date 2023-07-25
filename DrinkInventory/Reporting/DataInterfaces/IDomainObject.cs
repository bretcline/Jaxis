using System;

namespace Jaxis.DrinkInventory.Reporting.DataInterfaces
{
    public interface IDomainObject
    {
        Guid Id { get; set; }
        DateTime ModifiedOn { get; set; }
        bool IsNew { get; set; }
    }
}
