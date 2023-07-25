using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlServerCe;

namespace PassivePickup
{
    public class Customer
    {
        public Guid CustomerID { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string TagID { get; set; }
        public bool Paid { get; set; }
        public bool InvalidAddress { get; set; }

        public Customer( SqlCeDataReader _Reader )
        {
            CustomerID = _Reader.GetGuid( _Reader.GetOrdinal( "CustomerID" ) );
            Name = _Reader.GetString( _Reader.GetOrdinal( "Name" ) );
            Address = _Reader.GetString( _Reader.GetOrdinal( "Address" ) );
            TagID = _Reader.GetString( _Reader.GetOrdinal( "TagID" ) );
            Paid = _Reader.GetBoolean( _Reader.GetOrdinal( "Paid" ) );
            InvalidAddress = _Reader.GetBoolean( _Reader.GetOrdinal( "InvalidAddress" ) );
        }
    }
}
