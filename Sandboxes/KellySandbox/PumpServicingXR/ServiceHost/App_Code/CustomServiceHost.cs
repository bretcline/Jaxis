using System;
using System.Collections.Generic;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.Web.Configuration;
using log4net;

namespace WFT.PSService.ServiceHost
{
    class PSAppServerHostFactory : ServiceHostFactory
    {
        private log4net.ILog logger = log4net.LogManager.GetLogger( "AppServerHost" );

        protected override System.ServiceModel.ServiceHost CreateServiceHost( Type serviceType, Uri[ ] baseAddresses )
        {
            string baseAddressStr = WebConfigurationManager.AppSettings[ "HttpBaseAddress" ];
            Uri baseAddress = new Uri( baseAddressStr + @"/AppServer.svc" );
            if( baseAddressStr.EndsWith( @"/" ) )
                baseAddress = new Uri( baseAddressStr + "AppServer.svc" );

            logger.InfoFormat( "Using base address '{0}'.", baseAddress.AbsoluteUri );

            System.ServiceModel.ServiceHost serviceHost = new System.ServiceModel.ServiceHost( serviceType, new Uri[ ] { baseAddress } );
            return serviceHost;
        }
    }
}
