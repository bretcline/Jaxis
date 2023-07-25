using System;
using System.Collections.Generic;
using System.Linq;
using Jaxis.DrinkInventory.Reporting.Data;
using Jaxis.DrinkInventory.Reporting.Data.POCO;
using Jaxis.DrinkInventory.Reporting.DataInterfaces;

namespace Jaxis.DrinkInventory.Reporting.WcfService
{
    // (See comments at the top of IDataService.)

    public class DataService : IDataService
    {
        private IDataProvider m_DataProvider;
        private IDataProvider DataProvider
        {
            // Should be the only reference to the concrete DataProvider.  Should be able to easily swap it out to use another.
            get { return m_DataProvider ?? ( m_DataProvider = new DataProvider( ) ); }
        }

        // UPC
        public IUPC GetUPC( Guid _organizationId, string _ItemNumber )
        {
            return DataProvider.GetUPC( _organizationId, _ItemNumber );
        }

        public bool SaveUPC( Guid _organizationId, UPC _upc )
        {
            return DataProvider.SaveUPC( _organizationId, _upc );
        }

        public bool SaveUPCs( Guid _organizationId, List<UPC> _upcs )
        {
            return DataProvider.SaveUPCs( _organizationId, _upcs.Cast<IUPC>( ).ToList( ) );
        }

        // Pour
        public bool SavePour( Guid _organizationId, Pour _pour )
        {
            return DataProvider.SavePour( _organizationId, _pour );
        }

        public bool SavePours( Guid _organizationId, List<Pour> _pours )
        {
            return DataProvider.SavePours( _organizationId, _pours.Cast<IPour>( ).ToList( ) );
        }

        public bool UpdatePour( Guid _organizationId, PourUpdate _pourUpdate )
        {
            return DataProvider.UpdatePour( _organizationId, _pourUpdate );
        }

        public bool UpdatePours( Guid _organizationId, List<PourUpdate> _pourUpdates )
        {
            return DataProvider.UpdatePours( _organizationId, _pourUpdates.Cast<IPourUpdate>( ).ToList( ) );
        }

        // Alert
        public bool SaveAlert( Guid _organizationId, Alert _alert )
        {
            return DataProvider.SaveAlert( _organizationId, _alert );
        }

        public bool SaveAlerts( Guid _organizationId, List<Alert> _alerts )
        {
            return DataProvider.SaveAlerts( _organizationId, _alerts.Cast<IAlert>( ).ToList( ) );
        }

        // Device
        public bool SaveDevice( Guid _organizationId, Device _device )
        {
            return DataProvider.SaveDevice( _organizationId, _device );
        }

        public bool SaveDevices( Guid _organizationId, List<Device> _devices )
        {
            return DataProvider.SaveDevices( _organizationId, _devices.Cast<IDevice>( ).ToList( ) );
        }

        // POSTicketItem
        public bool SavePOSTicketItem( Guid _organizationId, POSTicketItem _posTicketItem )
        {
            return DataProvider.SavePOSTicketItem( _organizationId, _posTicketItem );
        }

        public bool SavePOSTicketItems( Guid _organizationId, List<POSTicketItem> _posTicketItems )
        {
            return DataProvider.SavePOSTicketItems( _organizationId, _posTicketItems.Cast<IPOSTicketItem>( ).ToList( ) );
        }

        public bool UpdatePOSTicketItem( Guid _organizationId, POSUpdate _posUpdate )
        {
            return DataProvider.UpdatePOSTicketItem( _organizationId, _posUpdate );
        }

        public bool UpdatePOSTicketItems( Guid _organizationId, List<POSUpdate> _posUpdates )
        {
            return DataProvider.UpdatePOSTicketItems( _organizationId, _posUpdates.Cast<IPOSUpdate>( ).ToList( ) );
        }
    }
}
