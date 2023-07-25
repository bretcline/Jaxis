using System;
using System.Collections.Generic;

namespace LFI.Sync.DataManager
{
    public interface IReaderTransaction<T> : ITransaction
    {
        List<T> FromReader( TransactionReader reader );
    }
}
