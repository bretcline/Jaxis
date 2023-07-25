using System.Collections.Generic;
using System.Linq;
using Jaxis.Inventory.Data.IBLDataItems;
using Jaxis.Inventory.Data.IDataItems;

namespace Jaxis.Inventory.Data.BusinessLogic.BLManagers
{
// ReSharper disable InconsistentNaming
    public class TicketItemAliasBLManager : BLManager<ITicketItemAlias, IBLTicketItemAlias>, ITicketItemAliasBLManager
// ReSharper restore InconsistentNaming
    {
        private BeverageMonitorDB m_Database = null;

        private BeverageMonitorDB GetDB( )
        {
            if( null == m_Database )
                m_Database = new BeverageMonitorDB();
            return m_Database;
        }


        public IEnumerable<string> GetUnknownAliases()
        {
            var rc = new List<string>();
            var db = GetDB();
            var proc = db.GetUnknownAliases();
            using( var reader = proc.ExecuteReader() )
            {
                while( reader.Read() )
                {
                    rc.Add( reader.GetString(0));
                }
            }

            return rc;
        }
    }
}
