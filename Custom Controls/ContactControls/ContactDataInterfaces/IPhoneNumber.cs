using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ContactDataInterfaces
{
    public interface IPhoneNumber : IDataObject
    {
        #region Properties

        string AreaCode
        {
            get; set;
        }

        string Extension
        {
            get; set;
        }

        Guid ParentID
        {
            get; set;
        }

        IPhoneNumberType PhoneNumberType
        {
            get; set;
        }

        string Prefix
        {
            get; set;
        }

        string Suffix
        {
            get; set;
        }

        #endregion Properties
    }
}