using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Jaxis.DrinkInventory.Reporting.DataInterfaces;

namespace Jaxis.DrinkInventory.Reporting.Data.POCO
{
    public partial class Session
    {
        public IUser User
        {
            get
            {
                return NavUser;
            }
            set
            {
                NavUser = ( User ) value;
                if ( value != null )
                {
                    UserId = value.UserId;
                }
            }
        }

        public string UserName
        {
            get
            {
                string rc = null;
                if( User != null )
                {
                    rc = User.UserName;
                }
                return rc;
            }
            set
            {
                if ( User != null )
                {
                    User.UserName = value;
                }
            }
        }
    }
}
