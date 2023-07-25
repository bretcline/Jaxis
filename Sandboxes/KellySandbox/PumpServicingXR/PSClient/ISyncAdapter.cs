using System;
using LFI.Sync.Shared;
using LFI.Sync.DataManager;
using WFT.PS.Shared;

namespace PSClient
{
    public interface ISyncAdapter<T> where T : IBaseData
    {
        event EventHandler<AdapterProgressChangedEventArgs> ProgressChanged;
        event EventHandler<ErrorReportedEventArgs> ErrorReported;
    }
}
