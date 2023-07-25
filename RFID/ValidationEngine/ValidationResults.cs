using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ValidationEngine
{
    public class ValidationResults : IValidationResults
    {
        #region IValidationResults Members

        public bool IsValid { get; set; }
        public bool IsCurrent { get; set; }
        public IEntity Entity { get; set; }
        public string Results { get; set; }


        #endregion
    }
}
