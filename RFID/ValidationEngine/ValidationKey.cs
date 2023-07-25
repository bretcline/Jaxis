using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ValidationEngine
{
    public class ValidationKey : IValidationKey
    {

        #region IValidationKey Members

        public string Key { get; set; }
        public Action<IValidationResults> ProcessValidation { get; set; }
        public DateTime TimeStamp { get; set; }

        #endregion
    }
}
