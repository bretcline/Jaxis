using System;

namespace Jaxis.Inventory.Data
{
    /// <summary>
    /// A Business Logic interface which represents the Categories table in the BevMetMobile Database.
    /// </summary>
    public interface IUICategory
    {
        //Guid CategoryID { get; set; }
        //Guid? ParentID { get; set; }
        string Name { get; set; }
        string Description { get; set; }
        IStandardNozzle Nozzle { get; set; }
    }
}