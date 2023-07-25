using System;
using System.Collections.Generic;
using System.Linq;
using Jaxis.Util.Log4Net;

namespace Jaxis.Inventory.Data
{
    public class TagDataManager : DataManager<ITag, Tag>, ITagDataManager
    {
        #region IDataManager<ITag> Members

        public IQueryable<ITag> GetAll( )
        {
            return Tag.All( );
        }

        public ITag Get( Guid ID )
        {
            //ITag rc = null;
            //Log.Time("TagDataManager::Get()", LogType.Debug, true, () => { rc = Tag.GetByID(ID); });
            //return rc;
            return Tag.GetByID(ID);
        }

        #endregion
    }
}