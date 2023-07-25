using System;
using System.Collections.Generic;
using System.Linq;

namespace Jaxis.Inventory.Data
{
    /// <summary>
    /// A Business Logic interface which represents the Actions table in the BevMetMobile Database.
    /// </summary>
    public interface IUISecurableItem
    {
        //Guid SecurableItemID { get; set; }
        string Name { get; set; }
        string Description { get; set; }
    }
}