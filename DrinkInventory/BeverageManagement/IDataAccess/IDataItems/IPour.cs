using System;

namespace Jaxis.Inventory.Data
{
    /// <summary>
    /// A class which represents the mobPours table in the BevMetMobile Database.
    /// </summary>
    public interface IPour : IDataObject<IPour>
    {
        Guid PourID { get; set; }
        Guid TagID { get; set; }
        Guid DeviceID { get; set; }
        Guid UPCID { get; set; }
        Guid? POSTicketItemID { get; set; }
        string HardwareID { get; set; }
        double Volume { get; set; }
        DateTime PourTime { get; set; }
        double Duration { get; set; }
        double Temperature { get; set; }
        double BatteryVoltage { get; set; }
        string RawData { get; set; }
        bool Alerted { get; set; }
        Guid LocationID { get; set; }
        double AmountLeft { get; set; }
        int? Status { get; set; }
    }
}