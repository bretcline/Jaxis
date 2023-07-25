using System;
using System.Linq;

namespace Jaxis.Inventory.Data
{
    public class TagMoveDataManager : DataManager<ITagMove, TagMove>, ITagMoveDataManager, IDataManager
    {
        #region IDataManager<ITag> Members

        public IQueryable<ITagMove> GetAll( )
        {
            return TagMove.All( );
        }

        public ITagMove Get( Guid ID )
        {
            return TagMove.GetByID( ID );
        }

        #endregion
    }
}