using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Jaxis.Util.Log4Net;
using SubSonic.Schema;

namespace Jaxis.Inventory.Data
{
    public abstract class BLManager<IDATA, IBLDATA> where IBLDATA : IDATA, IDataObject<IDATA>
    {
        Dictionary<Guid, KeyValuePair<IBLDATA, DateTime>> m_DataCache = new Dictionary<Guid, KeyValuePair<IBLDATA, DateTime>>( );

        protected Action<IBLDATA> OnCreate { private get; set; }
        protected Action<IBLDATA> OnDelete { private get; set; }

        public IBLDATA Create( )
        {
            IBLDATA rc = (IBLDATA) DataManagerFactory.Get().Manage<IDATA>().Create();
            if( null != OnCreate )
            {
                OnCreate(rc);
            }
            return rc;
        }

        public virtual bool Save( IBLDATA item )
        {
            bool rc = false;
            
            IList<string> error;
            rc = DataManagerFactory.Get( ).Manage<IDATA>( ).Save( (IDATA)item, out error );
            m_DataCache[item.ObjectID] = new KeyValuePair<IBLDATA, DateTime>( item, DateTime.Now );
            
            return rc;
        }

        public virtual bool Delete( IBLDATA item )
        {
            IList<string> error;

            if (null != OnDelete)
            {
                OnDelete(item);
            }

            return DataManagerFactory.Get( ).Manage<IDATA>( ).Delete( item, out error );
        }

        public virtual IBLDATA Get( Guid ID )
        {
            var t = typeof (IBLDATA);
            //return Log.Time(string.Format("Get - {0}", t.Name), LogType.Debug, true, () =>
            {
                var rc = default(IBLDATA);
                if (!m_DataCache.ContainsKey(ID))
                {
                    rc = (IBLDATA)DataManagerFactory.Get().Manage<IDATA>().Get(ID);
                    if (null != rc)
                    {
                        m_DataCache.Add(ID, new KeyValuePair<IBLDATA, DateTime>(rc, DateTime.Now));
                    }
                }
                else
                {
                    var now = DateTime.Now;
                    if (now - m_DataCache[ID].Value > new TimeSpan(0, 0, 0, 0, 500))
                    {
                        rc = (IBLDATA)DataManagerFactory.Get().Manage<IDATA>().Get(ID);
                        m_DataCache[ID] = new KeyValuePair<IBLDATA, DateTime>(rc, now);
                    }
                    else
                    {
                        rc = m_DataCache[ID].Key;
                    }
                }
                return rc;
            }
            //);
        }

        public virtual IEnumerable<IBLDATA> GetAll( )
        {
            Type t = typeof (IBLDATA);
            return Log.Time( string.Format("GetAll - {0}", t.Name ), LogType.Debug, true, () =>
            {
                var manager = DataManagerFactory.Get().Manage<IDATA>();
                var data = manager.GetAll();
                return data.ToList().Cast<IBLDATA>();
            });
        }
    }
}
