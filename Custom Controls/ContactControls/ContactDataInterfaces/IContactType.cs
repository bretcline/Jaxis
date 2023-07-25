using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ContactDataInterfaces
{
    public interface IContactType : IDataObject
    {
        #region Properties

        string Description
        {
            get; set;
        }

        string Name
        {
            get; set;
        }

        #endregion Properties
    }
}