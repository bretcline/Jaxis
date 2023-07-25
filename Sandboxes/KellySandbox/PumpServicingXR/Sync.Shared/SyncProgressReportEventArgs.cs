using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LFI.Sync.Shared
{
    public class SyncProgressReportEventArgs : EventArgs
    {
        public int SyncType;
        public int Progress;
        public string Text;

        public SyncProgressReportEventArgs( int syncType, int progress, string progressText )
        {
            SyncType = syncType;
            Progress = progress;
            Text = progressText;
        }
    }
}
