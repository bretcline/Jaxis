using System;

namespace Jaxis.Inventory.Data
{
    public interface IParLevel : IDataObject<IParLevel>
    {
        Guid ParLevelID { get; set; }
        Guid LocationID { get; set; }
        Guid UPCID { get; set; }
        double BottleCount { get; set; }
    }
    
    public interface IBLParLevel : IParLevel
    {
        Guid ParLevelID { get; set; }
        Guid LocationID { get; set; }
        Guid UPCID { get; set; }
        double BottleCount { get; set; }
    }
}