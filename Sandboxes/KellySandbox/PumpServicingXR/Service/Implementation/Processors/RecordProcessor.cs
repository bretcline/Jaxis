using System;
using LFI.Sync.DataManager;
using WFT.PSService.ServiceLibrary;

namespace WFT.PSService.Service
{
    public class RecordProcessor
    {
        private log4net.ILog logger = log4net.LogManager.GetLogger( "RecordProcessor" );
        private PersistenceManager persistenceMgr;

        public RecordProcessor( PersistenceManager _persistenceManager )
        {
            this.persistenceMgr = _persistenceManager;
        }

        /// <summary>
        /// Assuming that for each data object type there is a corresponding transaction type, this method can take any data object
        /// type and either insert it into the server database or update an existing record there.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="U"></typeparam>
        /// <param name="sync"></param>
        /// <param name="dataObject"></param>
        /// <returns></returns>
        public ServiceResult Process<T, U>( SyncContext sync, T dataObject ) where T : XRData<T> where U : BaseTransaction<T>, new( )
        {
            // identification and typeName are only used in log messages
            string identification = sync.DeviceName;
            string typeName = dataObject.GetType( ).Name;

            ServiceResult result = new ServiceResult( );
            try
            {
                logger.DebugFormat( "{0} --------------------PUT {1} -------------------", identification, typeName );
                DataManager dataManager = persistenceMgr.GetWSDataManager( );

                /// The method is not called with an instance of BaseTransaction<T>, only a type.  When the constructor is
                /// called (via Activator.CreateInstance) with a TablePrimaryKey parameter, the resulting transaction is set
                /// to do a SELECT COUNT.  
                U trans = ( U )Activator.CreateInstance( typeof( U ), new object[ ] { dataObject.TablePrimaryKey } );

                /// When the result from GetResultCount is 1, the record was found in the server database and needs to be updated.
                /// The 'signal' for that is that the dataObject.PrimaryKey (null for insert) is set here.
                if ( dataManager.GetResultCount( trans ) == 1 )
                {
                    dataObject.PrimaryKey = dataObject.TablePrimaryKey;
                }

                /// Again, given only the type of BaseTransaction<T> required, we instantiate another transaction.  This time, the
                /// constructor used is the one that accepts a dataObject; this constructor returns a transaction that is set to
                /// do INSERT or UPDATE (based on whether dataObject.PrimaryKey is nul or not).
                trans = ( U )Activator.CreateInstance( typeof( U ), new object[ ] { dataObject } );

                if ( trans.TransactionType == TransactionType.Insert )
                {
                    logger.DebugFormat( "{0} Preparing to INSERT {1} '{2}'.", identification, typeName, dataObject.ID.ToString( ) );
                }
                else
                {
                    logger.DebugFormat( "{0} Preparing to UPDATE {1} '{2}'.", identification, typeName, dataObject.PrimaryKey );
                }

                result = Put<T>( dataManager, trans, dataObject, typeName );
                logger.DebugFormat( "{0} Put{1}: {2}", identification, typeName, trans.GetAndClearErrors( ) );

                if ( result.Success == false )
                    throw new Exception( String.Format( "{0} Failed to update {1} to database. {2}", identification, typeName, result.Message ) );

                logger.DebugFormat( "{0} Put {1} succeeded for {1} '{2}'.", identification, typeName, result.ObjectGuid.ToString( ) );
                logger.DebugFormat( "{0} --------------------END {1} -------------------", identification, typeName );

            }
            catch ( Exception ex )
            {
                logger.Error( 
                    String.Format( "!x!x!x!x!x!x!x!x!x!x!x!x!x!x!PUT {0} FAILED!x!x!x!x!x!x!x!x!x!x!x!x!x!x!", typeName ), 
                    ex );

                result.Success = false;

                if ( String.IsNullOrEmpty( result.Message ) )
                    result.Message = PersistenceManager.GetExceptionString( ex );
            }
            return result;
        }

        /// <summary>
        /// By the time this generic method is called, the ITransaction object already knows whether it is an INSERT or an UPDATE.
        /// </summary>
        protected static ServiceResult Put<T>( DataManager dataMgr, ITransaction trans, XRData<T> dataObject, string typeName )
        {
            log4net.ILog logger = log4net.LogManager.GetLogger( String.Format( "RecordProcessor for {0}", typeName ) );
            ServiceResult outResult = new ServiceResult( );

            // adjust dates to their data-source local time
            TimeSpan offset = TimeSpan.FromHours( dataMgr.TimeZoneOffset );

            // XRData<T> knows how to adjust the LastModifiedDate; the subclasses are responsible for their own specific dates.
            dataObject.AdjustDates( offset );

            TransactionResult wsmResult = dataMgr.PutData( trans );
            outResult.Success = wsmResult.Success;

            return outResult;
        }
    }
}
