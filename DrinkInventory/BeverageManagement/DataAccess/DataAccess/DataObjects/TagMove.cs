using System;
using System.Collections.Generic;

namespace Jaxis.Inventory.Data
{
    public partial class TagMove : ITagMove
    {


        #region ITagMove Members


        public IDevice CurrentDevice
        {
            get
            {
                throw new NotImplementedException( );
            }
            set
            {
                throw new NotImplementedException( );
            }
        }

        public ILocation CurrentLocation
        {
            get
            {
                throw new NotImplementedException( );
            }
            set
            {
                throw new NotImplementedException( );
            }
        }

        #endregion

        #region IDataObject<ITagMove> Members


        public IEnumerable<ITagMove> GetAll( )
        {
            return All( );
        }

        #endregion
    }
}