using System;
using System.Reflection;

namespace Jaxis.Inventory.Data.IBLDataItems
{
    /// <summary>
    /// A Business Logic interface which represents the Categories table in the BevMetMobile Database.
    /// </summary>
    public interface IBLCategory : ICategory, INamedObject
    {
        [Obfuscation(Exclude = true)]
        Guid CategoryID { get; set; }
        Guid? ParentID { get; set; }
        string Name { get; set; }
        string Description { get; set; }
        IBLStandardNozzle Nozzle { get; set; }
    }
}