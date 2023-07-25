using System;
using System.Collections.Generic;
using LFI.Sync.DataManager;
using WFT.PSService.ServiceLibrary;

namespace PSClient
{
    public interface IResolveAdapter<T> : ISyncAdapter<T> where T : IBaseData
    {
        int Resolve( Func<SyncContext, IList<T>> resolveData, DateTime lastResolve );
    }
}
