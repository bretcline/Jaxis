using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using Jaxis.DrinkInventory.Reporting.DataInterfaces;

namespace Jaxis.DrinkInventory.Reporting.Data
{
    public partial class UPCManager
    {
        // Return the object if it is in the context; go get it if not.
        // This resembles TryGetObjectByKey, but in this case, the id is not the key.
        public override IUPC Get( Guid _id )
        {
            var upcInOSM = m_Context.GetAllEntries( )
                .Where( o => o.Entity is IUPC && ( ( IUPC ) o.Entity ).UPCId == _id )
                .FirstOrDefault( );

            var rc = upcInOSM == null ? GetAll( ).Where( u => u.UPCId == _id ).FirstOrDefault( ) : upcInOSM.Entity;

            return rc as IUPC;
        }

        // This override is necessary because UPC has a key that is not Guid.
        public override IUPC Save( IUPC _item )
        {
            return Save( _item, ( ) => GetAll( ).Where( u => u.ItemNumber == _item.ItemNumber ).FirstOrDefault( ) );
        }
    }
}
