using System.Collections.Generic;
using Jaxis.Inventory.Data.IBLDataItems;

namespace Jaxis.Inventory.Data
{
    /// <summary>
    /// A class which represents the secUsers table in the BevMetMobile Database.
    /// </summary>
    public partial class User : IUser, IBLUser
    {
        public IEnumerable<IUser> GetAll( )
        {
            return All( );
        }
    }
}