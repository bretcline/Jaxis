using System;
using System.Collections.Generic;
using WFT.PS.Shared;
using LFI.Sync.DataManager;
using WFT.PSService.Data;
using WFT.PSService.ServiceLibrary;
using System.Runtime.Remoting;
using System.Reflection;

namespace PSClient
{
    public class TransactionFactory : ISyncTransactionFactory
    {
        private static TransactionFactory instance;
        private TransactionFactory( )
        {
        }

        public static TransactionFactory GetInstance( )
        {
            if( instance == null )
                instance = new TransactionFactory( );
            return instance;
        }

        public ITransaction BuildPutTransaction( IBaseData item, DataTag dataTag )
        {
            switch( dataTag )
            {
                //case DataTag.AssetGroupInfo:
                //    return new AssetGroupInfoTransaction( item );
                default:
                    return null;
            }
        }

        /// <summary>
        /// Builds a transaction to fetch items to be uploaded.
        /// </summary>
        public IReaderTransaction<T> BuildUploadTransaction<T>( DataTag dataTag, DateTime lastModified )
        {
            IReaderTransaction<T> rc = null;
            // For each DataObject 'T', there must be a matching class 'T'Transaction that implements IReaderTransaction.
            // Note: T and its corresponding Transaction are not in the same namespace; else we could have used
            // typeof( T ).FullName below instead of "WFT.PSService.Data." + typeof( T ).Name.
            try
            {
                ObjectHandle objectHandle = Activator.CreateInstance( 
                    "Data",                                                     // Assembly name (without .dll) 
                    "WFT.PSService.Data." + typeof( T ).Name + "Transaction",   // Fully qualified class name
                    false,                                                      // ignoreCase
                    BindingFlags.Default, 
                    null,                                                       // Binder
                    new object[ ] { lastModified },                             // constructor parameters
                    null,                                                       // CultureInfo
                    null,                                                       // ActivationAttributes
                    null );                                                     // Evidence (security info)
                rc = ( IReaderTransaction<T> ) objectHandle.Unwrap( );
            }
            catch
            {
                // just return null
            }
            return rc;
        }

        /// <summary>
        /// Builds the resolve transactions, used for resolving data integrity constraints, such as deletes, immediately
        /// before any uploads are processed and sent to the server.
        /// </summary>
        /// <param name="dataTag">The data tag.</param>
        /// <param name="sourceDB">The source DB.</param>
        /// <param name="lastModified">The last modified date.</param>
        /// <returns></returns>
        public ITransaction BuildResolveTransaction( DataTag dataTag, DateTime lastModified )
        {
            switch( dataTag )
            {
                //case DataTag.JobInfo:
                //    return new JobInfoTransaction( new Guid( sourceDB ), lastModified, true );
                default:
                    return null;
            }
        }
    }
}