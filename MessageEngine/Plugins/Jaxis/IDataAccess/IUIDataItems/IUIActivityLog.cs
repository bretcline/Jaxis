using System;
using Jaxis.MessageLibrary;

namespace Jaxis.Inventory.Data
{
    public interface IUIActivityLog
    {
        string TagNumber { get; }
        string Location { get; }
        DateTime ActivityTime { get; }
        TagPhaseType Type { get; }
    }

    public interface IUITagActivity
    {
        string TagNumber { get; }
        string Location { get; }
        DateTime ActivityTime { get; }
        TagPhaseType Type { get; }
    }
}