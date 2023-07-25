using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Jaxis.DrinkInventory.Reporting.DataInterfaces;

namespace Jaxis.DrinkInventory.Reporting.Data.POCO
{
    public partial class SecurityView
    {
        public Guid SecurityViewId
        {
            get
            {
                return Guid.Empty;
            }
            set
            {
                throw new NotImplementedException( );
            }
        }

        #region IDomainObject Members

        public DateTime ModifiedOn
        {
            get
            {
                return DateTime.Now;
            }
            set
            {
                throw new NotImplementedException( );
            }
        }

        #endregion
    }
}
