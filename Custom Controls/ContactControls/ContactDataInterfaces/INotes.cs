using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ContactDataInterfaces
{
    public interface INotes : IDataObject
    {
        #region Properties

        string Note
        {
            get; set;
        }

        string NoteTitle
        {
            get; set;
        }

        IParent Parent
        {
            get; set;
        }

        IParentType ParentType
        {
            get; set;
        }

        #endregion Properties
    }
}