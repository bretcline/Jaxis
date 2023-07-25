using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ValidationEngine
{
    public interface IValidationKey
    {
        string Key { get; }
        DateTime TimeStamp { get; }
        Action<IValidationResults> ProcessValidation { get; set; } 
    }
}
