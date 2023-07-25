using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ValidationEngine
{
    public interface IEngine
    {
        void SubmitForProcessing( IValidationKey _Key );
        Action<IValidationResults> DefaultProcessValidation { get; set; }
    }

}