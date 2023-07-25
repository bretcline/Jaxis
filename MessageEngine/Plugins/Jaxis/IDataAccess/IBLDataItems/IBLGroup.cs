using System;

namespace Jaxis.Inventory.Data.IBLDataItems
{
    /// <summary>
    /// A Business Logic interface which represents the Groups table in the BevMetMobile Database.
    /// </summary>
    public interface IBLGroup : IGroup
    {
        Guid GroupID { get; set; }
        string Name { get; set; }
    }

}