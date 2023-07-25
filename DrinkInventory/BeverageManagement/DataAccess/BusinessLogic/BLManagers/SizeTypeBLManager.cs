using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Jaxis.Inventory.Data.IBLDataItems;

namespace Jaxis.Inventory.Data
{
    public class SizeTypeBLManager : BLManager<ISizeType, IBLSizeType>, ISizeTypeBLManager
    {
        protected Dictionary<Guid, IBLSizeType> m_Values = new Dictionary<Guid, IBLSizeType>( );

        public virtual bool Save( IBLSizeType item )
        {
            bool rc = false;

            rc = base.Save(item);
            if( true == rc )
            {
                m_Values[item.ObjectID] = item;
            }

            return rc;
        }

        public virtual bool Delete( IBLSizeType item )
        {
            bool rc = false;
            rc = base.Delete(item);
            if( true == rc && m_Values.ContainsKey( item.ObjectID ))
            {
                m_Values.Remove(item.ObjectID);
            }
            return rc;
        }

        public virtual IBLSizeType Get( Guid ID )
        {
            IBLSizeType rc = default( IBLSizeType );
            if( m_Values.ContainsKey( ID ) )
            {
                rc = m_Values[ID];
            }
            else
            {
                rc = base.Get(ID);
                m_Values[rc.ObjectID] = rc;
            }
            return rc;
        }

        public virtual IEnumerable<IBLSizeType> GetAll( )
        {
            IEnumerable<IBLSizeType> rc = null;

            if (base.GetAll().Count() != m_Values.Count )
            {
                rc = base.GetAll();

                foreach (var blSizeType in rc)
                {
                    m_Values[blSizeType.ObjectID] = blSizeType;
                }
            }
            
            rc = m_Values.Values;

            return rc;
        }
    }
}
