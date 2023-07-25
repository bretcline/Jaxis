using System;
using LFI.Sync.DataManager;
using WFT.PSService.ServiceLibrary;
using System.Collections.Generic;
using WFT.PS.Shared;

namespace PSClient
{
    public interface IDownloadAdapter<T> : ISyncAdapter<T> where T : IBaseData
    {
        event EventHandler<DataChangedEventArgs> DataChanged;
        int Download( Func<SyncContext, IList<T>> getData, int numItemsToDownload, DateTime lastDownload );
    }
}
