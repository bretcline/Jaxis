using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ContactDataInterfaces
{
    public interface IAddress : IDataObject
    {
        #region Properties

        IAddressType AddressType
        {
            get; set;
        }

        string City
        {
            get; set;
        }

        string Country
        {
            get; set;
        }

        Guid ParentID
        {
            get; set;
        }

        string State
        {
            get; set;
        }

        string Street
        {
            get; set;
        }

        string Zip
        {
            get; set;
        }

        #endregion Properties
    }
}