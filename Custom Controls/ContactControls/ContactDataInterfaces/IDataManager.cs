using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ContactDataInterfaces
{
    public interface IDataObject
    {
        #region Properties

        Guid ElementID
        {
            get;
        }

        #endregion Properties
    }
}