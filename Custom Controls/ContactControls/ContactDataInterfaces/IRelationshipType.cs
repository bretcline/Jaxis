using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ContactDataInterfaces
{
    public interface IRelationshipType : IDataObject
    {
        #region Properties

        string RelationshipType
        {
            get; set;
        }

        #endregion Properties
    }
}