using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ContactDataInterfaces
{
    public interface IJob : IDataObject
    {
        #region Properties

        List<ICompany> CompanyList
        {
            get; set;
        }

        #endregion Properties
    }
}