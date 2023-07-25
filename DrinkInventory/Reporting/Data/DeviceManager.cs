using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Jaxis.DrinkInventory.Reporting.DataInterfaces;

namespace Jaxis.DrinkInventory.Reporting.Data
{
    public partial class DeviceManager
    {
        // Return the object if it is in the context; go get it if not.
        // This resembles TryGetObjectByKey, but in this case, the id is not the key.
        public override IDevice Get( Guid _id )
        {
            var deviceInOSM = m_Context.GetAllEntries( )
                .Where( o => o.Entity is IDevice && ( ( IDevice ) o.Entity ).DeviceId == _id )
                .FirstOrDefault( );

            var rc = deviceInOSM == null ? GetAll( ).Where( u => u.DeviceId == _id ).FirstOrDefault( ) : deviceInOSM.Entity;

            return rc as IDevice;
        }

        // This override is necessary because Device has a key that is not Guid.
        public override IDevice Save( IDevice _item )
        {
            return Save( _item, ( ) => GetAll( ).Where( d => d.DeviceNumber == _item.DeviceNumber ).FirstOrDefault( ) );
        }
    }
}
