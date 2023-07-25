using System;

namespace Jaxis.Inventory.Data
{
    public interface IQuality : IDataObject<IQuality>
    {
        Guid QualityID { get; set; }
        int QualityLevel { get; set; }
        string Name { get; set; }
    }
}