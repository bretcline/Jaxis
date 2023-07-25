using System;

namespace WFT.PS.Shared
{
    public class DataChangedEventArgs : EventArgs
    {
        public DataChangedEventArgs( DataTag dataTag )
        {
            DataTag = dataTag;
        }

        public DataTag DataTag { get; private set; }
    }
}