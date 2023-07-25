using System;
using System.Collections.Generic;
using LFI.Sync.DataManager;
using WFT.PSService.ServiceLibrary;
using WFT.PS.Shared;

namespace PSClient
{
    public interface IUploadAdapter<T> : ISyncAdapter<T> where T : IBaseData
    {
        event EventHandler<ItemXRefChangedEventArgs> ItemXRefChanged;
        int Upload( Func<SyncContext, T, ServiceResult> uploadToServer, DateTime lastUpload );
        int Upload( Func<SyncContext, List<T>, List<ServiceResult>> uploadToServer, DateTime lastUpload );
    }
}
