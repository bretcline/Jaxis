using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DBValidation
{
    public class Entity
    {

        #region IEntity Members

        public string Name { get; set; }

        public string HTMLOutput
        {
            get
            {
                return string.Format( "<H3>{0}<H3><H1>{1}</H1><H3>{2}, {3} {4}<H3>", Name, Address, City, State, Zip );
            }
        }

        #endregion

        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zip { get; set; }

    }

}
