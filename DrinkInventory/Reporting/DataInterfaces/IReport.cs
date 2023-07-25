using System;
using System.Data;

namespace Jaxis.DrinkInventory.Reporting.DataInterfaces
{
    public partial interface IReport : IDomainObject
    {
        DataTable GetData(object[] _parameterValues);
    }
}
