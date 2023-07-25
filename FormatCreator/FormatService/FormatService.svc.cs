using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using LFI.RFID.Format;
using LFI.RFID.Editor;
using LFI.RFID.FormatServer;
using System.Configuration;

namespace FormatService
{
    // NOTE: If you change the class name "Service1" here, you must also update the reference to "Service1" in Web.config and in the associated .svc file.
    public class FormatService : IFormatService
    {
        private FormatManager formatManager;
        private DataForwarder forwarder;

        public FormatService( )
        {
            string formatPath = ConfigurationManager.AppSettings.Get( "FormatPath" );
            formatManager = new FormatManager( formatPath );
            forwarder = new DataForwarder( );

        }

        public FormatDef GetFormat( Guid formatID )
        {
            return formatManager.GetFormatByID( formatID );
        }

        public string GetFormatAsString( Guid formatID )
        {
            FormatDef def = formatManager.GetFormatByID( formatID );
            if( def == null )
                return string.Empty;
            else
                return formatManager.GetFormatXML( def );
        }

        public void PostTagData( TagData data )
        {
            forwarder.Forward( data );
        }

    }
}
