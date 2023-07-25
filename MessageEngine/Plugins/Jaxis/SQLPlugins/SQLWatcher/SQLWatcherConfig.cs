using System;
using System.Collections.Generic;
using System.Linq;
using Jaxis.Interfaces;

namespace SQLWatcher
{
    static class SQLWatcherConfig
    {
        static public string GetConnectionString(this IDeviceConfig _Config)
        {
            return _Config.GetOptionData(0, "");
        }

        static public string GetTableName(this IDeviceConfig _Config)
        {
            return _Config.GetOptionData(1, "");
        }

        static public string GetColumnName(this IDeviceConfig _Config)
        {
            return _Config.GetOptionData(2, "");
        }
        
        static public string GetDataType(this IDeviceConfig _Config)
        {
            return _Config.GetOptionData(3, "String");
        }
        
        static public string GetMinValue(this IDeviceConfig _Config)
        {
            return _Config.GetOptionData(4, "0");
        }

        static public string GetMaxValue(this IDeviceConfig _Config)
        {
            return _Config.GetOptionData(5, "0");
        }

        static public int GetQueryInterval(this IDeviceConfig _Config)
        {
            return _Config.GetOptionData(6, Int32.Parse, 5);
        }

        static public int GetRetentionCycle(this IDeviceConfig _Config)
        {
            return _Config.GetOptionData(7, Int32.Parse, 5);
        }

    }
}