using System.Collections.Generic;
using Jaxis.Inventory.Data.IBLDataItems;

namespace Jaxis.Inventory.Data
{
    /// <summary>
    /// A class which represents the Organization table in the BevMetMobile Database.
    /// </summary>
    public partial class Organization : IOrganization,  IBLOrganization, IUIOrganization
    {
        public IEnumerable<IOrganization> GetAll( )
        {
            return All( );
        }
    }
}