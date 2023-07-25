using System;

namespace Jaxis.DrinkInventory.Reporting.DataInterfaces
{
    public partial interface ISession : IDomainObject
    {
        IUser User { get; set; }
        string UserName { get; set; }
    }
}
