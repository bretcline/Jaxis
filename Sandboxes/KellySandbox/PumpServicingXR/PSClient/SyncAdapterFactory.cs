using LFI.Sync.DataManager;
using WFT.PS.Shared;
using WFT.PSService.ServiceLibrary;
namespace PSClient
{
    public class SyncAdapterFactory : ISyncAdapterFactory
    {
        public ISyncAdapter<T> GetSyncAdapter<T>( DataTag tag, DataManager dataManager, SyncContext context, ISyncTransactionFactory factory ) where T : IBaseData
        {
            switch( tag )
            {
                default:
                    return new SyncAdapter<T>( tag, dataManager, context, factory );
            }
        }
    }
}
