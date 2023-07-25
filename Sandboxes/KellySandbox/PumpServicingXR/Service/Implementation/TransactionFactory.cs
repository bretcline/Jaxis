using System;
using System.Reflection;
using System.Runtime.Remoting;
using LFI.Sync.DataManager;
using WFT.PS.Shared;
using WFT.PSService.Data;

namespace WFT.PSService.Service
{
    public class TransactionFactory
    {
        public static ITransaction BuildXRPutTransaction( DataTag tag, IBaseData putObj )
        {
            ITransaction rc = null;
            // For each DataTag, there must be a matching class +Transaction that implements ITransaction.
            // Note: DataTag and its corresponding Transaction are not in the same namespace; thus, we must supply
            // "WFT.PSService.Data.".
            try
            {
                ObjectHandle objectHandle = Activator.CreateInstance(
                    "Data",                                                     // Assembly name (without .dll) 
                    "WFT.PSService.Data." + tag.ToString( ) + "Transaction",    // Fully qualified class name
                    false,                                                      // ignoreCase
                    BindingFlags.Default,
                    null,                                                       // Binder
                    new object[ ] { putObj },                                   // constructor parameters
                    null,                                                       // CultureInfo
                    null,                                                       // ActivationAttributes
                    null );                                                     // Evidence (security info)
                rc = ( ITransaction ) objectHandle.Unwrap( );
            }
            catch
            {
                // just return null
            }
            return rc;
        }
    }
}
