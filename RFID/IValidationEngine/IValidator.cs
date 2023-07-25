using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ValidationEngine
{
    public interface IValidator
    {
        IValidationResults Validate( IValidationKey _Key );
    }
}
