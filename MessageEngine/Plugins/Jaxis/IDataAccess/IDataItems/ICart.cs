using System;

namespace Jaxis.Inventory.Data
{
    /// <summary>
    /// A class which represents the Carts table in the BevMetMobile Database.
    /// </summary>
    public interface ICart : INameDescription, IDataObject<ICart>
    {
        Guid EventID { get; set; }
    }
}