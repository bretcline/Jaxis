using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using Jaxis.DrinkInventory.Reporting.DataInterfaces;

namespace Jaxis.DrinkInventory.Reporting.Data.POCO
{
    public partial class Organization
    {
        [DataMember]
        public List<Guid> UserGroupIds { get; set; }

        public List<IUserGroup> UserGroups
        {
            get
            {
                List<IUserGroup> rc = null;
                if ( NavUserGroups != null )
                {
                    rc = NavUserGroups.Cast<IUserGroup>( ).ToList( );
                }
                return rc;
            }
            set
            {
                if ( value != null )
                {
                    NavUserGroups = value.Cast<UserGroup>( ).ToList( );
                }
                else
                {
                    NavUserGroups = null;
                }
            }
        }
    }
}
