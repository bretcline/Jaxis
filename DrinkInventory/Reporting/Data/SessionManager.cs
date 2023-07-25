using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Jaxis.DrinkInventory.Reporting.Data.POCO;
using Jaxis.DrinkInventory.Reporting.DataInterfaces;

namespace Jaxis.DrinkInventory.Reporting.Data
{
    public partial class SessionManager
    {
        public override void Hydrate( ISession _item, bool _includeRelatedObjects = false )
        {
            if ( _item.User == null )
            {
                var user = m_Factory.User.Get( _item.UserId );
                ( ( Session ) _item ).NavUser = ( User ) user;
            }
        }
    }
}
