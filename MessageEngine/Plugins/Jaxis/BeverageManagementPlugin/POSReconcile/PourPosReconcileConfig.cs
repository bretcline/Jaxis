using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Jaxis.Interfaces;

namespace Jaxis.BeverageManagement.Plugin
{
    public static class PourPosReconcileConfig
    {
        static public string GetConnectStringPort(this IDeviceConfig _Config)
        {
            return _Config.Options[0].Value;
        }

        static public int GetPollInterval(this IDeviceConfig _Config)
        {
            return Convert.ToInt32(_Config.Options[1].Value) * 1000;
        }

        static public int GetAlertInterval(this IDeviceConfig _Config)
        {
            return Convert.ToInt32(_Config.Options[2].Value) * 1000 * 60;
        }

        static public int GetPourWindow(this IDeviceConfig _Config)
        {
            return Convert.ToInt32(_Config.Options[3].Value);
        }
    }
}
