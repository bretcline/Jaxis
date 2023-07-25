using Jaxis.Inventory.Data.IBLDataItems;

namespace Jaxis.Inventory.Data
{
    public class StandardPriceBLManager : BLManager<IStandardPrice, IBLStandardPrice>, IStandardPriceBLManager { }

    public class QualityBLManager : BLManager<IQuality, IBLQuality>, IQualityBLManager { }

}