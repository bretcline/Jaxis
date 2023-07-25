using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using Jaxis.DrinkInventory.Reporting.DataInterfaces;

namespace Jaxis.DrinkInventory.Reporting.Data.POCO
{
    public partial class UserGroup
    {
        [DataMember]
        public List<Guid> AreaIds { get; set; }
        [DataMember]
        public List<Guid> OrganizationIds { get; set; }
        [DataMember]
        public List<Guid> UserIds { get; set; }
        [DataMember]
        public List<Organization> NavOrganizations { get; set; }
        [DataMember]
        public List<Area> NavAreas { get; set; }
        [DataMember]
        public List<User> NavUsers { get; set; }
        
        public IOrganization Organization // <- OwnerOrganization
        {
            get 
            {
                return NavOrganization;
            }
            set 
            {
                NavOrganization = ( Organization ) value;
                if ( value != null )
                {
                    OrganizationId = value.OrganizationId;
                }
            }
        }

        public List<IOrganization> Organizations
        {
            get
            {
                List<IOrganization> rc = null;
                if ( NavOrganizations != null )
                {
                    rc = NavOrganizations.Cast<IOrganization>( ).ToList( );
                }
                return rc;
            }
            set
            {
                if ( value != null )
                {
                    NavOrganizations = value.Cast<Organization>( ).ToList( );
                }
                else
                {
                    NavOrganizations = null;
                }
            }
        }

        public List<IArea> Areas
        {
            get
            {
                List<IArea> rc = null;
                if ( NavAreas != null )
                {
                    rc = NavAreas.Cast<IArea>( ).ToList( );
                }
                return rc;
            }
            set
            {
                if ( value != null )
                {
                    NavAreas = value.Cast<Area>( ).ToList( );
                }
                else
                {
                    NavAreas = null;
                }
            }
        }

        public List<IUser> Users
        {
            get
            {
                List<IUser> rc = null;
                if ( NavUsers != null )
                {
                    rc = NavUsers.Cast<IUser>( ).ToList( );
                }
                return rc;
            }
            set
            {
                if ( value != null )
                {
                    NavUsers = value.Cast<User>( ).ToList( );
                }
                else
                {
                    NavUsers = null;
                }
            }
        }
    }
}
