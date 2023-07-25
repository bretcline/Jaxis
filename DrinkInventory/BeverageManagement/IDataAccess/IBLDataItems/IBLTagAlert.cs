using System;

namespace Jaxis.Inventory.Data.IBLDataItems
{
    public interface IBLTagAlert : ITagAlert, IUITagActivity
    {
        Guid TagAlertID { get; set; }
        Guid TagID { get; set; }
        int AlertType { get; set; }
        Guid DeviceID { get; set; }
        Guid LocationID { get; set; }
        string Message { get; set; }
        int Severity { get; set; }
        DateTime AlertTime { get; set; }
    }
}