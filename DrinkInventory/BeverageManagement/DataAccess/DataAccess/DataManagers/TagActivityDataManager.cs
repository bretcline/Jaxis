using System;
using System.Linq;

namespace Jaxis.Inventory.Data
{
    public class TagActivityDataManager : DataManager<ITagActivity, TagActivity>, ITagActivityDataManager, IDataManager
    {
        #region IDataManager<ITag> Members

        public IQueryable<ITagActivity> GetAll( )
        {
            return TagActivity.All( );
        }

        public ITagActivity Get( Guid ID )
        {
            return TagActivity.GetByID( ID );
        }

        #endregion
    }
}