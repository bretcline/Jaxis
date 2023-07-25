using System;

namespace Jaxis.Inventory.Data.IBLDataItems
{
    /// <summary>
    /// A Business Logic interface which represents the Carts table in the BevMetMobile Database.
    /// </summary>
    public interface IBLCart : ICart
    {
        Guid EventID { get; set; }
        string Name { get; set; }
        string Description { get; set; }
    }
}