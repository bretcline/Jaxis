using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Jaxis.DrinkInventory.Reporting.Data.POCO;
using TestDataService.DataService;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System.Xml;
using System.Xml.Serialization;
using System.Runtime.Serialization;

namespace TestDataService
{
    class Program
    {
        static void Main( string[ ] args )
        {
            var p = new Program( );
            p.Run( );
        }

        private void Run( )
        {
            var tickets = CreateSomeTickets( );
            var dataProxy = new DataServiceClient( );
            var orgId = new Guid( "64183F10-8151-42DF-82BF-C5CB1177DA30" );
            long size = GetSerializedSize( tickets );
            dataProxy.SavePOSTicketItems( orgId, tickets );

            var updates = GetSomeUpdates( tickets );
            dataProxy.UpdatePOSTicketItems( orgId, updates );
        }

        private List<POSUpdate> GetSomeUpdates( List<POSTicketItem> _tickets )
        {
            return _tickets.Select( t => new POSUpdate { POSTicketItemId = t.POSTicketItemId, Status = 3 } ).ToList( );
        }

        private List<POSTicketItem> CreateSomeTickets( )
        {
            var rc = new List<POSTicketItem>( );
            var keys = Enumerable.Range( 1001, 2000 );
            keys.ToList( ).ForEach( k =>
                {
                    var i = k.ToString( );
                    var ticket = new POSTicketItem( );
                    CreateATicket( ticket, i );
                    rc.Add( ticket );
                } );
            return rc;
        }

        private static void CreateATicket( POSTicketItem ticket, string i )
        {
            ticket.Id = Guid.NewGuid( );
            ticket.CheckNumber = string.Format( "{0}", i );
            ticket.Comment = string.Format( "Comment about #{0}", i );
            ticket.Description = string.Format( "Ticket #{0}", i );
            ticket.Establishment = "Establishment";
            ticket.IsNew = true;
            ticket.LocationName = "Location";
            ticket.ModifiedOn = DateTime.Now;
            ticket.OrganizationId = new Guid( "64183F10-8151-42DF-82BF-C5CB1177DA30" );
            ticket.Price = 0;
            ticket.Quantity = 1;
            ticket.Status = 0;
            ticket.TicketDate = DateTime.Now.AddDays( -5.0 );
        }

        private static long GetObjectSize( object obj )
        {
            var bf = new BinaryFormatter( );
            var ms = new MemoryStream( );
            bf.Serialize( ms, obj );
            var size = ms.Length;
            ms.Dispose( );
            return size;
        }

        private static long GetSerializedSize ( object obj )
        {
            long size;
            using ( var ms = new MemoryStream( ) )
            {
                var s = new DataContractSerializer( obj.GetType( ) );
                s.WriteObject( ms, obj );
                size = ms.Length;

                //using ( var fs = new FileStream( @"test.xml", FileMode.Create ) )
                //{
                //    ms.WriteTo( fs );
                //    fs.Close( );
                //}
            }
            return size;
        }
    }
}
