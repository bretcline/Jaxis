using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace Jaxis.RFID.Readers
{
    public interface IRFIDConfig
    {
        void AddValue( string _Key, string _Value );
        Dictionary<string, string> GetConfig( );
        string FormatDefinitionPath { get; set; }
    }
}
