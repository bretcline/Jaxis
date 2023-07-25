using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Jaxis.Interfaces;
using System.IO.Ports;
using Jaxis.MessageLibrary;

namespace Jaxis.Readers.Sprint
{
    public static class TCPConfigs
    {

        static public int GetPort(this IDeviceConfig _Config)
        {
            return Convert.ToInt32(_Config.Options[0].Value);
        }

        static public Dictionary<OriginalMessageType, bool> GetSupportedMessages( this IDeviceConfig _Config )
        {
            Dictionary<OriginalMessageType, bool> rc = new Dictionary<OriginalMessageType, bool>();

            if( 1 < _Config.Options.Count )
            {
                string[] messages = _Config.Options[1].Value.Split(',');
                foreach (var message in messages)
                {
                    OriginalMessageType type;
                    if( Enum.TryParse(message, true, out type) )
                    {
                        rc[type] = true;
                    }
                }
            }

            return rc;
        }


        static public bool IsTrishSupported(this IDeviceConfig _Config)
        {
            bool rc = false;

            if (1 < _Config.Options.Count)
            {
                if (_Config.Options[1].Value.Contains("Trish"))
                {
                    rc = true;
                }
            }
            
            return rc;
        }

        static public bool IsIdentecSupported(this IDeviceConfig _Config)
        {
            bool rc = false;

            if (1 < _Config.Options.Count)
            {
                if (_Config.Options[1].Value.Contains("Identec"))
                {
                    rc = true;
                }
            }

            return rc;
        }
    }
}
