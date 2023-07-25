using System;
using LFI.Sync.DataManager;
using WFT.PS.Shared;

namespace PSClient
{
    public interface ISyncTransactionFactory
    {
        /// <summary>
        /// Builds transaction for insertion of sync downloads into client database
        /// </summary>
        ITransaction BuildPutTransaction( IBaseData item, DataTag dataTag );

        /// <summary>
        /// Builds a transaction to fetch items from client database to be upload-synced to server
        /// </summary>
        IReaderTransaction<T> BuildUploadTransaction<T>( DataTag dataTag, DateTime lastModified );

        /// <summary>
        /// Builds the resolve transactions, used for resolving data intergrity contraints, such as deletes, immediately
        /// before any uploads are processed and sent to the server.
        /// </summary>
        ITransaction BuildResolveTransaction( DataTag dataTag, DateTime lastModified );
    }
}