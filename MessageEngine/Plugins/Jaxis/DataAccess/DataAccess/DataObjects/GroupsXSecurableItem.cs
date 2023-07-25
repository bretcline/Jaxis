using System.Collections.Generic;
using Jaxis.Inventory.Data.IBLDataItems;

namespace Jaxis.Inventory.Data
{
    /// <summary>
    /// A class which represents the secGroupXAction table in the BevMetMobile Database.
    /// </summary>
    public partial class GroupsXSecurableItem : IGroupsXSecurableItem, IBLGroupsXSecurableItem, IUIGroupsXSecurableItem
    {
        public IEnumerable<IGroupsXSecurableItem> GetAll( )
        {
            return All( );
        }
    }
}