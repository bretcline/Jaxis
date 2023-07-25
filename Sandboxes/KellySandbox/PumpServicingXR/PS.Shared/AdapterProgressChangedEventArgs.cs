using System;
namespace WFT.PS.Shared
{
    public class AdapterProgressChangedEventArgs : EventArgs 
    {
        public DataTag Tag { get; private set; }
        public int NumSinceLastProgress { get; private set; }
        public int NumItemsComplete { get; private set; }
        public int TotalItems { get; private set; }
        public string Status { get; private set; }
        public int Type { get; private set; }

        public AdapterProgressChangedEventArgs( int syncType, DataTag tag, int numSinceLastProgress, int numCompleted, int total, string status )
        {
            Tag = tag;
            NumSinceLastProgress = numSinceLastProgress;
            NumItemsComplete = numCompleted;
            TotalItems = total;
            Type = syncType;
            Status = status;
        }
    }
}