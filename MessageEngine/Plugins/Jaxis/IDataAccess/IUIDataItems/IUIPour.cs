using System;

namespace Jaxis.Inventory.Data
{
    /// <summary>
    /// A Business Logic interface which represents the Pours table in the BevMetMobile Database.
    /// </summary>
    public interface IUIPour
    {
        double Volume { get; set; }
        DateTime PourTime { get; set; }
        double Duration { get; set; }
        double Temperature { get; set; }
    }
}