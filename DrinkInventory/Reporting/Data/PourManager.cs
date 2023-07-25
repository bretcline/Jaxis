using Jaxis.DrinkInventory.Reporting.Data.POCO;
using Jaxis.DrinkInventory.Reporting.DataInterfaces;

namespace Jaxis.DrinkInventory.Reporting.Data
{
    public partial class PourManager
    {
        public override void Hydrate( DataInterfaces.IPour _item, bool _includeRelatedObjects = false )
        {
            if ( _item.POSTicketItem == null && _item.POSTicketItemId.HasValue )
            {
                ( ( Pour ) _item ).NavPOSTicketItem = ( POSTicketItem ) m_Factory.POSTicketItem.Get( _item.POSTicketItemId.Value );
            }
        }
    }
}
