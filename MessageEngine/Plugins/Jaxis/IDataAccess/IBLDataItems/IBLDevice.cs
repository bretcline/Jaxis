using System;

namespace Jaxis.Inventory.Data.IBLDataItems
{
    public interface IBLDevice : IDevice
    {
        Guid DeviceID { get; set; }
        string HardwareID { get; set; }
        string Name { get; set; }
        string Description { get; set; }
        string Settings { get; set; }
    }
}