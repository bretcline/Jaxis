using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Jaxis.Interfaces;

namespace Jaxis.MessageLibrary
{
    public interface IPourData : IPour, IMessage
    {
        string TagNumber { get; }

        string DeviceID { get; }
        
        double BatteryVoltage { get; }

        double PourAmount { get; }

        int PourCount { get; }

        double Temperature { get; }

        string RawData { get; }
    }
}
