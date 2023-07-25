using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Jaxis.DrinkInventory.Reporting.DataInterfaces
{
    public interface IDataProvider
    {
        IUPC GetUPC( Guid _organizationId, string _ItemNumber );
        bool SaveUPC( Guid _organizationId, IUPC _upc );
        bool SaveUPCs( Guid _organizationId, List<IUPC> _upcs );
        bool SavePour( Guid _organizationId, IPour _pour );
        bool SavePours( Guid _organizationId, List<IPour> _pours );
        bool UpdatePour( Guid _organizationId, IPourUpdate _pourUpdate );
        bool UpdatePours( Guid _organizationId, List<IPourUpdate> _pourUpdate );
        bool SaveAlert( Guid _organizationId, IAlert _alert );
        bool SaveAlerts( Guid _organizationId, List<IAlert> _alerts );
        bool SaveDevice( Guid _organizationId, IDevice _device );
        bool SaveDevices( Guid _organizationId, List<IDevice> _devices );
        bool SavePOSTicketItem( Guid _organizationId, IPOSTicketItem _posTicketItem );
        bool SavePOSTicketItems( Guid _organizationId, List<IPOSTicketItem> _posTicketItems );
        bool UpdatePOSTicketItem( Guid _organizationId, IPOSUpdate _posUpdate );
        bool UpdatePOSTicketItems( Guid _organizationId, List<IPOSUpdate> _posUpdates );
    }
}
