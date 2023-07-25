using System;

namespace Jaxis.Inventory.Data
{
    public interface IStandardPour : IDataObject<IStandardPour>
    {
        Guid StandardPourID { get; set; }
        string Name { get; set; }
        double PourStandard { get; set; }
        double StandardVariance { get; set; }
        bool SystemStandard { get; set; }
        Guid? CategoryID { get; set; }
//        double PourDouble { get; set; }
//        double DoubleVariance { get; set; }
    }
}