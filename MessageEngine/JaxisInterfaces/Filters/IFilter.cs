using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Jaxis.Interfaces
{
    public interface IFilter
    {
        bool Filter( IMessage _Message );

        IFilterConfig Config { get; }
    }
}