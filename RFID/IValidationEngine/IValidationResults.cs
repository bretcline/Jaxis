using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ValidationEngine
{
    public interface IValidationResults
    {
        bool IsValid { get; }
        bool IsCurrent { get; }
        IEntity Entity { get; }
        string Results { get; }
    }
}
