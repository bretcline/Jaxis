using System;

namespace ContactDataInterfaces
{
    public interface IAddressType : IDataObject
    {
        #region Properties

        Guid AddressTypeID
        {
            get; set;
        }

        string AddressTypeName
        {
            get; set;
        }

        #endregion Properties
    }
}