using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using Jaxis.Interfaces;
using Jaxis.Inventory.Data.IBLDataItems;
using Jaxis.Inventory.Data.IUIDataItems;

namespace Jaxis.Inventory.Data
{
    public partial class DataPour : ITagActivityItem
    {
        public string ConvertedVolume
        {
            get
            {
                var volume = BLManagerFactory.Get().ConvertPourToUnits(Volume);
                var units = BLManagerFactory.Get().GetDefaultSizeType().Abbreviation;
                return string.Format( "{0:0.00} {1}", volume, units );
            }
        }

        [DataMember]
        public string TagNumber { get; set; }
    }



    /// <summary>
    /// A class which represents the mobPours table in the BevMetMobile Database.
    /// </summary>
    public partial class Pour : IPour, IBLPour, IUIPour, IMessageWrapper
    {
        public IEnumerable<IPour> GetAll( )
        {
            return All( );
        }

        public string HardwareID { get; set; }

        #region IMessage

        public string Driver { get; set; }
        ulong IMessageWrapper.Type { get; set; }
        ulong IMessage.Type { get { return (this as IMessageWrapper).Type; } }
        public DateTime ReadTime { get; set; }

        #endregion
        
    }
}
