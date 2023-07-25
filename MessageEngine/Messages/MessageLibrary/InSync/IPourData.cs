using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Jaxis.Interfaces;

namespace Jaxis.MessageLibrary.InSync
{
    public interface IPourData : IPour, IMessage
    {
        string TagID { get; }

        string DeviceID { get; }
        
        double BatteryVoltage { get; }

        double PourAmount { get; }

        long PourTime { get; }

        int PourCount { get; }

        double Temperature { get; }

        string RawData { get; }
    }
}
