using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ContactDataInterfaces
{
    public interface IContact : IDataObject
    {
        #region Properties

        List<IAddress> AddressList
        {
            get; set;
        }

        ICompany Company
        {
            get; set;
        }

        IContactType ContactType
        {
            get; set;
        }

        string FirstName
        {
            get; set;
        }

        string LastName
        {
            get; set;
        }

        string MiddleName
        {
            get; set;
        }

        List<IPhoneNumber> PhoneNumberList
        {
            get; set;
        }

        IAddress PrimaryAddress
        {
            get; set;
        }

        IPhoneNumber PrimaryPhoneNumber
        {
            get; set;
        }

        #endregion Properties
    }
}