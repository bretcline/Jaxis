using System;

namespace Jaxis.Inventory.Data
{
    /// <summary>
    /// A Business Logic interface which represents the Groups table in the BevMetMobile Database.
    /// </summary>
    public interface IUIGroup
    {
        //Guid GroupID { get; set; }
        string Name { get; set; }
    }

}