using System;

namespace Jaxis.Inventory.Data.IBLDataItems
{
    /// <summary>
    /// A Business Logic interface which represents the Pours table in the BevMetMobile Database.
    /// </summary>
    public interface IBLPour : IPour
    {
        double Volume { get; set; }
        DateTime PourTime { get; set; }
        double Duration { get; set; }
        double Temperature { get; set; }
        bool Split { get; set; }
    }

    public interface IUIPourPoint : IActivityData
    {
        Guid TagID { get; set; }
        string TagNumber { get; set; }
        string Category { get; set; }
        string UPCName { get; set; }
        string Location { get; set; }
        double Volume { get; set; }
        string Units { get; set; }
        DateTime PourTime { get; set; }
    }

    public interface IActivityData
    {
    }
}