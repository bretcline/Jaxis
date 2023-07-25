using System.Collections.Generic;
using System.Linq;

namespace Jaxis.Inventory.Data
{
    public abstract class BLOnlyManager<IENTITY, ENTITY> where ENTITY : class, IENTITY, new( )
    {

        protected List<IENTITY> m_DataItems = new List<IENTITY>( );

        public IENTITY Create( )
        {
            var rc = new ENTITY( );
            return rc;
        }

        public virtual bool Save( IENTITY item  )
        {
            IList<string> Results = new List<string>( );

            m_DataItems.Add( item );

            return true;
        }

        public virtual bool Delete( IENTITY item )
        {
            m_DataItems.Remove( item );
            return true;
        }

        public IEnumerable<IENTITY> GetAll( )
        {
            return m_DataItems;
        }
    }
}