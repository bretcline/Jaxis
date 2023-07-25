using System;
using System.Collections.Generic;
using System.ServiceModel;
using Jaxis.DrinkInventory.Reporting.DataInterfaces;
using Jaxis.DrinkInventory.Reporting.Data.POCO;

namespace Jaxis.DrinkInventory.Reporting.WcfService
{
    /// There are cautions to be observed by any client of IDataService.
    /// Many of the methods take lists of objects.  The client needs to be 
    /// careful not to send such big lists that he overloads the WCF buffers.
    /// He also needs to be aware that the methods are all-successful-or-none, 
    /// so if he sends too many, he increases his risk of having to send them again.

    [ServiceContract]
    public interface IDataService
    {
        // UPC
        [OperationContract]
        IUPC GetUPC( Guid _organizationId, string _ItemNumber );
        [OperationContract]
        bool SaveUPC( Guid _organizationId, UPC _upc );
        [OperationContract]
        bool SaveUPCs( Guid _organizationId, List<UPC> _upcs );

        // Pour
        [OperationContract]
        bool SavePour( Guid _organizationId, Pour _pour );
        [OperationContract]
        bool SavePours( Guid _organizationId, List<Pour> _pours );
        [OperationContract]
        bool UpdatePour( Guid _organizationId, PourUpdate _pourUpdate );
        [OperationContract]
        bool UpdatePours( Guid _organizationId, List<PourUpdate> _pourUpdates );

        // Alert
        [OperationContract]
        bool SaveAlert( Guid _organizationId, Alert _alert );
        [OperationContract]
        bool SaveAlerts( Guid _organizationId, List<Alert> _alerts );

        // Device
        [OperationContract]
        bool SaveDevice( Guid _organizationId, Device _device );
        [OperationContract]
        bool SaveDevices( Guid _organizationId, List<Device> _devices );

        // POSTicketItem
        [OperationContract]
        bool SavePOSTicketItem( Guid _organizationId, POSTicketItem _posTicketItem );
        [OperationContract]
        bool SavePOSTicketItems( Guid _organizationId, List<POSTicketItem> _posTicketItems );
        [OperationContract]
        bool UpdatePOSTicketItem( Guid _organizationId, POSUpdate _posUpdate );
        [OperationContract]
        bool UpdatePOSTicketItems( Guid _organizationId, List<POSUpdate> _posUpdates );
    }
}
