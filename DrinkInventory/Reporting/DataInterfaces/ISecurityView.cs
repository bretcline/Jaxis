using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace Jaxis.DrinkInventory.Reporting.DataInterfaces
{
    public partial interface ISecurityView : IDomainObject
    {
        Guid SecurityViewId { get; set; }
    }
}
