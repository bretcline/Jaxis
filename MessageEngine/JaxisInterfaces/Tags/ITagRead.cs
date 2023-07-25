using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Jaxis.Interfaces;

namespace Jaxis.Interfaces.Tags
{
    public interface ITagRead : ITag
    {
        double SignalStrength { get; }

        string DeviceID { get; }
    }
}