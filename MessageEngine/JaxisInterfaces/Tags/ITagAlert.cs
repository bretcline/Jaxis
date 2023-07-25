using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Jaxis.Interfaces.Tags
{
    public interface ITagAlertMessage : ITag, IAlertMessage
    {
        double BatteryVoltage { get; }

        Dictionary<string, object> Parameters { get; }
    }
}