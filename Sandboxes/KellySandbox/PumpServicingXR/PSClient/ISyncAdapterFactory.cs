using WFT.PS.Shared;
using LFI.Sync.DataManager;
using WFT.PSService.ServiceLibrary;

namespace PSClient
{
    public interface ISyncAdapterFactory
    {
        ISyncAdapter<T> GetSyncAdapter<T>( DataTag tag, DataManager dataManager, SyncContext context, ISyncTransactionFactory factory ) where T : IBaseData;
    }
}
