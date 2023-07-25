using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ContactDataInterfaces
{
    public interface ICompany : IDataObject
    {
        #region Properties

        List<IAddress> AddressList
        {
            get;
        }

        string CompanyName
        {
            get; set;
        }

        List<IPhoneNumber> PhoneNumberList
        {
            get;
        }

        IAddress PrimaryAddress
        {
            get; set;
        }

        IContact PrimaryContact
        {
            get; set;
        }

        IPhoneNumber PrimaryPhoneNumber
        {
            get; set;
        }

        IRelationshipType Relationship
        {
            get; set;
        }

        #endregion Properties

        #region Methods

        void AddAddress( IAddress _Address );

        void AddPhoneNumber( IPhoneNumber _PhoneNumber );

        #endregion Methods
    }
}