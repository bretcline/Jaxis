using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Jaxis.DrinkInventory.Reporting.DataInterfaces;
using JaxisExtensions;
using Jaxis.Util.Log4Net;

namespace Jaxis.DrinkInventory.Reporting.Data
{
    public class DataProvider : IDataProvider
    {
        private const int SET_SIZE = 250;
        private static DateTime m_OrganizationLoadTime;
        private static List<Guid> m_ValidOrganizations;
        private static List<Guid> ValidOrganizations
        {
            get
            {
                if ( DateTime.Now.Add( new TimeSpan( 0, -5, 0 ) ) > m_OrganizationLoadTime )
                {
                    m_OrganizationLoadTime = DateTime.Now;
                    using ( var factory = CreateFactory( ) )
                    {
                        m_ValidOrganizations = factory.Organization.GetAll( )
                            .Select( o => o.OrganizationId )
                            .ToList( );
                    }
                }
                return m_ValidOrganizations;
            }
        }
        private static Func<IDataManagerFactory> m_createFactory;
        private static IDataManagerFactory CreateFactory( )
        {
            var create = m_createFactory ?? ( m_createFactory = ( ( ) => { return DataManagerFactory.Get( ); } ) );
            return create( );
        }

        // UPC
        public IUPC GetUPC( Guid _organizationId, string _ItemNumber )
        {
            using ( var factory = CreateFactory( ) )
            {
                return factory.UPC.GetAll( )
                    .Where( u => u.ItemNumber == _ItemNumber )
                    .FirstOrDefault( );
            }
        }

        public bool SaveUPC( Guid _organizationId, IUPC _upc )
        {
            return SaveUPCs( _organizationId, new List<IUPC> { _upc } );
        }

        public bool SaveUPCs( Guid _organizationId, List<IUPC> _upcs )
        {
            return Save<IUPC>( _organizationId, _upcs );
        }

        // Pour
        public bool SavePour( Guid _organizationId, IPour _pour )
        {
            return SavePours( _organizationId, new List<IPour> { _pour } );
        }

        public bool SavePours( Guid _organizationId, List<IPour> _pours )
        {
            return Save<IPour>( _organizationId, _pours );
        }

        public bool UpdatePour( Guid _organizationId, IPourUpdate _pourUpdate )
        {
            return UpdatePours( _organizationId, new List<IPourUpdate> { _pourUpdate } );
        }

        public bool UpdatePours( Guid _organizationId, List<IPourUpdate> _pourUpdates )
        {
            bool rc = ValidateOrganization( _organizationId );
            if ( rc )
	        {
		        using ( var factory = CreateFactory( ) )
                {
                    var manager = factory.Pour;
                    try
                    {
                        var sets = _pourUpdates.GetSets<IPourUpdate>( SET_SIZE );
                        foreach ( var set in sets )
                        {
                            var keys = set.Select( s => s.PourId ).ToList( );
                            var pours = manager.GetAll( )
                                .Where( p => keys.Contains( p.PourId ) )
                                .ToList( );
                            if ( keys.Count != pours.Count )
                            {
                                throw new Exception( "Not all pours were found to update." );
                            }
                            var updates = ( from s in set
                                            join p in pours on s.PourId equals p.PourId
                                            select new { p, s } ).ToList( );
                            updates.ForEach( u =>
                                {
                                    u.p.POSTicketItemId = u.s.POSTicketItemId.HasValue ? u.s.POSTicketItemId.Value : u.p.POSTicketItemId;
                                    u.p.Status = u.s.Status.HasValue ? u.s.Status.Value : u.p.Status;
                                    manager.Save( u.p );
                                } );
                        }
                        factory.SaveChanges( );
                    }
                    catch ( Exception _ex )
                    {
                        Log.Exception( _ex );
                        rc = false;
                    }
                }
            }
            return rc;
        }

        // Alert
        public bool SaveAlert( Guid _organizationId, IAlert _alert )
        {
            return SaveAlerts( _organizationId, new List<IAlert> { _alert } );
        }

        public bool SaveAlerts( Guid _organizationId, List<IAlert> _alerts )
        {
            return Save<IAlert>( _organizationId, _alerts, ( a ) => a.IsNew = true );
        }

        // Device
        public bool SaveDevice( Guid _organizationId, IDevice _device )
        {
            return SaveDevices( _organizationId, new List<IDevice> { _device } );
        }

        public bool SaveDevices( Guid _organizationId, List<IDevice> _devices )
        {
            return Save<IDevice>( _organizationId, _devices );
        }

        // POSTicketItem
        public bool SavePOSTicketItem( Guid _organizationId, IPOSTicketItem _posTicketItem )
        {
            return SavePOSTicketItems( _organizationId, new List<IPOSTicketItem> { _posTicketItem } );
        }

        public bool SavePOSTicketItems( Guid _organizationId, List<IPOSTicketItem> _posTicketItems )
        {
            return Save<IPOSTicketItem>( _organizationId, _posTicketItems );
        }

        public bool UpdatePOSTicketItem( Guid _organizationId, IPOSUpdate _posUpdate )
        {
            return UpdatePOSTicketItems( _organizationId, new List<IPOSUpdate> { _posUpdate } );
        }

        public bool UpdatePOSTicketItems( Guid _organizationId, List<IPOSUpdate> _posUpdates )
        {
            bool rc = ValidateOrganization( _organizationId );
            if ( rc )
	        {
		        using ( var factory = CreateFactory( ) )
                {
                    var manager = factory.POSTicketItem;
                    try
                    {
                        var sets = _posUpdates.GetSets<IPOSUpdate>( SET_SIZE );
                        foreach ( var set in sets )
                        {
                            var keys = set.Select( s => s.POSTicketItemId ).ToList( );
                            var tickets = manager.GetAll( )
                                .Where( t => keys.Contains( t.POSTicketItemId ) )
                                .ToList( );
                            if ( keys.Count != tickets.Count )
                            {
                                throw new Exception( "Not all tickets were found to update." );
                            }
                            var updates = ( from s in set
                                            join t in tickets on s.POSTicketItemId equals t.POSTicketItemId 
                                            select new { t, s } ).ToList( );
                            updates.ForEach( u =>
                                {
                                    u.t.Status = u.s.Status;
                                    manager.Save( u.t );
                                } );
                        }
                        factory.SaveChanges( );
                    }
                    catch ( Exception _ex )
                    {
                        Log.Exception( _ex );
                        rc = false;
                    }
                }
            }
            return rc;
        }

        // Helpers
        private bool Save<T>( Guid _organizationId, List<T> _list, Action<T> _beforeSave = null ) where T : IDomainObject
        {
            bool rc = ValidateOrganization( _organizationId );
            if ( rc )
            {
                using ( var factory = CreateFactory( ) )
                {
                    var manager = factory.Manage<T>( );
                    try
                    {
                        if ( _beforeSave == null )
                        {
                            _list.ForEach( l => manager.Save( l ) );
                        }
                        else
                        {
                            _list.ForEach( l =>
                            {
                                _beforeSave( l );
                                manager.Save( l );
                            } );
                        }
                        factory.SaveChanges( );
                    }
                    catch ( Exception _ex )
                    {
                        Log.Exception( _ex );
                        rc = false;
                    }
                }
            }
            return rc;
        }

        private bool ValidateOrganization( Guid _organizationId )
        {
            return ValidOrganizations.Contains( _organizationId );
        }
    }
}
