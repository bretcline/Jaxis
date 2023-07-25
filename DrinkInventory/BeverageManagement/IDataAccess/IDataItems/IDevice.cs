using System;
using Jaxis.Interfaces;

namespace Jaxis.Inventory.Data
{

    public enum DeviceAlertType
    {
        CannotConnect = 0,
        NotReadingTags = 1,
    }

    public interface IDevice : IDataObject<IDevice>
    {
        Guid DeviceID { get; set; }
        string HardwareID { get; set; }
        string Name { get; set; }
        string Description { get; set; }
        string Settings { get; set; }
    }

    public interface IDeviceAlert : IDataObject<IDeviceAlert>, IMessageWrapper
    {
        Guid DeviceAlertID { get; set; }
        Guid DeviceID { get; set; }
        int AlertType { get; set; }
        string Message { get; set; }
        DateTime AlertTime { get; set; }
    }
}