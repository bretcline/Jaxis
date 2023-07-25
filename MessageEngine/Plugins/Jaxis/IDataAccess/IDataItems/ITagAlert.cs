using System;
using Jaxis.Interfaces;

namespace Jaxis.Inventory.Data
{
    public interface ITagAlert : IDataObject<ITagAlert>, IMessageWrapper, IAlertMessage
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