using System;

namespace Jaxis.Inventory.Data
{
    public interface ITagMove : IDataObject<ITagMove>
    {
        Guid TagMoveID { get; set; }
        Guid TagID { get; set; }
        Guid DeviceID { get; set; }
        Guid LocationID { get; set; }
        DateTime MoveTime { get; set; }
        double SignalStrength { get; set; }
    }
}