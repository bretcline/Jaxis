using System;
using Jaxis.Interfaces;

namespace Jaxis.Inventory.Data.IUIDataItems
{
// ReSharper disable InconsistentNaming
    public interface IUIPosPour : IUIPosDisplay
// ReSharper restore InconsistentNaming
    {
#pragma warning disable 108,114
        PosStatus Status { get; }
#pragma warning restore 108,114
        double PourAmount { get; }
        string Type { get; }
        string Category { get; }
        DateTime PourTime { get; }
    }
}
