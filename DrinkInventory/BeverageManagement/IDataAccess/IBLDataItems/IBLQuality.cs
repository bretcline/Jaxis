using System;

namespace Jaxis.Inventory.Data.IBLDataItems
{
    public interface IBLQuality : IQuality
    {
        Guid QualityID { get; set; }
        int QualityLevel { get; set; }
        string Name { get; set; }
    }
}