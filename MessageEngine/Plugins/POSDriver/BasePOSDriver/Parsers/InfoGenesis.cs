using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using Jaxis.MessageLibrary.POS;

namespace Jaxis.Readers.POS.Parsers
{
    public class InfoGenesis : IParser
    {
        public ITicket ParseData( string _Data )
        {
            Ticket rc = new Ticket( );
            List<string> Data = new List<string>( _Data.Split( new char[] { '\n' } ) );
            rc.RawData = Data;

            string[] Sections = Regex.Split( _Data, "=*=" );

            ParseHeader( rc, Sections[1] );
            //Ticket Items
            
            if( 3 < Sections.Length )
                rc.Items = ParseLineItems( Sections[2] );

            return rc;
        }

        public void ClearCache()
        {
            throw new NotImplementedException();
        }

        private void ParseHeader( Ticket _Ticket, string _Header )
        {
            Match Check = Regex.Match( _Header, @"Check:\s*[0-9]*" );
            _Ticket.CheckNumber = Check.Value.Trim();

            Match Guests = Regex.Match( _Header, @"Guests:\s*[0-9]*" );
            _Ticket.GuestCount = Convert.ToInt32( Guests.Value );

            Match Table = Regex.Match( _Header, @"(Table|Room):\s*[0-9]*" );
            _Ticket.Table = Table.Value.Trim();

            //Match Establishment = Regex.Match( _Header, @"Check:\s*[0-9]*" );
            //_Ticket.Establishment = 
        }

        private List<ITicketItem> ParseLineItems( string _Section )
        {
            // Line items
            var Items = new List<ITicketItem>( );
            var TicketItemList = Regex.Matches( _Section, @"(\d+) (\w*.*) (\d+\.\d+)" );

            //(\s*(\d+)\s(\w*.*)\s(\d+\.\d+)\n(\s*(\w*.*\n)\n)*)

            //MatchCollection TicketItemList = Regex.Matches( _Section, @"(\d+) (\w*.*) (\d+\.\d+)" );
            foreach( Match M in TicketItemList )
            {
                TicketItem Item = new TicketItem( );
                GroupCollection GC = M.Groups;
                Item.Quantity = Convert.ToInt32( GC[1].Value );
                Item.Description = GC[2].Value.Trim();
                Item.Price = Convert.ToDecimal(GC[3].Value, CultureInfo.InvariantCulture);
                Items.Add( Item );
            }
            return Items;
        }        
    }
}
