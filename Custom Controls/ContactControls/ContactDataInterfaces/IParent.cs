﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ContactDataInterfaces
{
    public interface IParent : IDataObject
    {
        #region Properties

        string Name
        {
            get; set;
        }

        #endregion Properties
    }
}