using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LFI.Sync.Shared
{
    public class SyncEventArgs : EventArgs
    {
        public SyncEventArgs( bool async, bool isSyncing )
        {
            this.IsAsync = async;
            this.IsSyncInProgress = isSyncing;
        }

        public bool IsAsync;
        public bool IsSyncInProgress;
    }
}