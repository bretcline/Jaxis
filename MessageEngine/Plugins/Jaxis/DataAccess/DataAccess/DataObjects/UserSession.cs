using System;
using System.Collections.Generic;
using Jaxis.Inventory.Data.IBLDataItems;

namespace Jaxis.Inventory.Data
{
    /// <summary>
    /// A class which represents the secUserSessions table in the BevMetMobile Database.
    /// </summary>
    public partial class UserSession : IUserSession, IBLUserSession
    {
        public IEnumerable<IUserSession> GetAll( )
        {
            return All( );
        }
        partial void OnCreated( )
        {
            SessionID = Guid.NewGuid();
            StartTime = DateTime.Now;
        }
    }
}