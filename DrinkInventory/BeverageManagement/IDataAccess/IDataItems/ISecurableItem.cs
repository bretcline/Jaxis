using System;
using System.Collections.Generic;
using System.Linq;

namespace Jaxis.Inventory.Data
{
    /// <summary>
    /// A class which represents the secActions table in the BevMetMobile Database.
    /// </summary>
    public interface ISecurableItem : INameDescription, IDataObject<ISecurableItem>
    {
    }
}