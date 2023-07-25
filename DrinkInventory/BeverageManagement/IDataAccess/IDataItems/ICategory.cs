using System;

namespace Jaxis.Inventory.Data
{
    /// <summary>
    /// A class which represents the $1
    /// </summary>
    public interface ICategory : INameDescription, IDataObject<ICategory>
    {
        Guid CategoryID { get; set; }
        Guid? ParentID { get; set; }
        IStandardNozzle Nozzle { get; set; }
    }
}