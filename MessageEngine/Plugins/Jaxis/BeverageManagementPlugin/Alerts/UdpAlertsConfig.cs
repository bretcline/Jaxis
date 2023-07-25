using Jaxis.Interfaces;

namespace Jaxis.BeverageManagement.Plugin.Alerts
{
    public static class UdpAlertsConfig
    {
        public static string GetUdpHost(this IDeviceConfig _Config)
        {
            return _Config.GetOptionData(0);
        }

        public static int GetUdpPort(this IDeviceConfig _Config)
        {
            return _Config.GetOptionData(1, int.Parse, 32123);
        }
    }
}