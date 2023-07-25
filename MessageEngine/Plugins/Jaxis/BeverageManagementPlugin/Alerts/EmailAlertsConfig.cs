using Jaxis.Interfaces;

namespace Jaxis.BeverageManagement.Plugin.Alerts
{
    /*
          <Options>
            <string></string>  --Host
            <string></string>  --Port
            <string></string>  --Enable SSL (true/false)
            <string></string>  --From Account
            <string></string>  --Password
            <string></string> -- email address,(1-n)|AlertType,(1-n);
            <string></string>  --DeliveryMethod
          </Options>
    */
    public static class EmailAlertsConfig
    {
        static public string GetHost( this IDeviceConfig _Config )
        {
            return _Config.GetOptionData( 0 );
        }

        static public int GetPort( this IDeviceConfig _Config )
        {
            return _Config.GetOptionData( 1, int.Parse, 587 );
        }

        static public bool GetEnabledSSL( this IDeviceConfig _Config )
        {
            return _Config.GetOptionData( 2, bool.Parse, true );
        }

        static public string GetFromAccount( this IDeviceConfig _Config )
        {
            return _Config.GetOptionData( 3 );
        }

        static public string GetPassword( this IDeviceConfig _Config )
        {
            return _Config.GetOptionData( 4 );
        }

        static public string GetAddresses( this IDeviceConfig _Config )
        {
            return _Config.GetOptionData( 5 );
        }

        static public string GetDeliveryMethod( this IDeviceConfig _Config )
        {
            return _Config.GetOptionData( 6, "Network" );
        }

    }
}