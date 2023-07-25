using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using LFI.RFID.Format;

namespace LFI.RFID.FormatServer
{
    public interface IDataForwarder
    {
        // Note: Concretes can add logic to toss out TagData for Formats they don't care about
        void Forward(TagData data);
    }    
}
