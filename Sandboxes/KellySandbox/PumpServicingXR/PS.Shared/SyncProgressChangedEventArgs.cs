using System;

namespace WFT.PS.Shared
{
    public class SyncProgressChangedEventArgs : EventArgs
    {
        public int Progress { get; private set; }
        public int Type { get; private set; }
        public DataTag Tag { get; private set; }
        public int CurrentItemCount { get; private set; }
        public int TotalItemCount { get; private set; }
        public string Message { get; private set; }

        public SyncProgressChangedEventArgs( int syncType, DataTag tag, int currentItemCount, int totalItemCount, int totalProgress )
        {
            CurrentItemCount = currentItemCount;
            TotalItemCount = totalItemCount;
            Progress = totalProgress;
            Type = syncType;
            Tag = tag;
        }

        public SyncProgressChangedEventArgs( int syncType, DataTag tag, int currentItemCount, int totalItemCount, int totalProgress, string message )
            : this( syncType, tag, currentItemCount, totalItemCount, totalProgress )
        {
            Message = message;
        }
    }
}