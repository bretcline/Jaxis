using System;
namespace WFT.PS.Shared
{
    public class ItemXRefChangedEventArgs : EventArgs
    {
        public DataTag DataTag { get; private set; }
        public Guid ItemID { get; private set; }
        public string NewXRef { get; private set; }

        public ItemXRefChangedEventArgs( DataTag dataTag, Guid itemID, string newXRef )
        {
            DataTag = dataTag;
            ItemID = itemID;
            NewXRef = newXRef;
        }
    }
}