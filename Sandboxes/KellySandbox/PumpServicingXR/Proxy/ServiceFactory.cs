using System;
using System.ServiceModel;
using System.Configuration;
using System.IO;
using WFT.PSService.ServiceLibrary;

namespace WFT.PSService.Proxy
{
	public class ServiceFactory
	{
		private static string appBaseAddr = String.Empty;

		#region Config
		static ServiceFactory()
		{
			try
			{
				ExeConfigurationFileMap fileMap = new ExeConfigurationFileMap();
				fileMap.ExeConfigFilename = Directory.GetCurrentDirectory() + @"\Proxy.config";
				System.Configuration.Configuration config = ConfigurationManager.OpenMappedExeConfiguration(fileMap, ConfigurationUserLevel.None);

				if (config.AppSettings.Settings["AppServerAddress"] != null)
					appBaseAddr = config.AppSettings.Settings["AppServerAddress"].Value;
			}
			catch (Exception ex)
			{
				throw new Exception( "Problem initializing ServerFactory from Proxy.config file. Check that file exists and try again.", ex );
			}
		}

        public static string GetServerAddress()
        {
            string address = appBaseAddr;
            if (address.Contains(@"/"))
            {
                int index = address.LastIndexOf(@"/");
                address = address.Substring(0, index);
            }
            return address;
        }

		public static bool UpdateServerAddress(string baseAddress)
		{
			if (ValidateServerAddress(baseAddress) == false)
				return false;

			appBaseAddr = baseAddress + "/AppServer.svc";

			ExeConfigurationFileMap fileMap = new ExeConfigurationFileMap();
			fileMap.ExeConfigFilename = Directory.GetCurrentDirectory() + @"\Proxy.config";
			System.Configuration.Configuration config = ConfigurationManager.OpenMappedExeConfiguration(fileMap, ConfigurationUserLevel.None);

			if (config != null)
			{
				if (config.AppSettings.Settings["AppServerAddress"] != null)
					config.AppSettings.Settings["AppServerAddress"].Value = appBaseAddr;
				else
					config.AppSettings.Settings.Add("AppServerAddress", appBaseAddr);
			}

			return true;
		}

		public static bool ValidateWSDatabase()
		{
#warning work this out
            //string appAddr = GetServerAddress() + "/InfoServer.svc";

            //EndpointAddress ep = new EndpointAddress(appAddr + "/DataSourceConfig");
            //IDataSourceConfigAccessor serverConfigAccessor = ChannelFactory<IDataSourceConfigAccessor>.CreateChannel(GetBinding(), ep);
            //string result = serverConfigAccessor.ValidateWSDatabase();
			//return result == "SUCCESS";

            return true;
		}

		// validate web service address
		public static bool ValidateServerAddress(string baseAddress)
        {
#warning work this out
            //string appAddr = baseAddress + "/InfoServer.svc";

            //try
            //{
            //    EndpointAddress ep = new EndpointAddress(appAddr + "/DataSourceConfig");
            //    IDataSourceConfigAccessor serverConfigAccessor = ChannelFactory<IDataSourceConfigAccessor>.CreateChannel(GetBinding(), ep);
            //    serverConfigAccessor.GetServerTime(new SyncContext());
            //    return true;
            //}
            //catch
            //{
            //    return false;
            //}
            return true;
		}
		#endregion
        
		public static ServiceLibrary.IRecordAccessor GetRecordAccessor( )
        {
            return GetRecordAccessor( appBaseAddr );
        }

        public static ServiceLibrary.IRecordAccessor GetRecordAccessor( string appServerUrl )
        {
            EndpointAddress ep = new EndpointAddress( appServerUrl + "/Record" );
            ServiceLibrary.IRecordAccessor serverAccessor = ChannelFactory<ServiceLibrary.IRecordAccessor>.CreateChannel( GetBinding( ), ep );

            return serverAccessor;
        }

		private static BasicHttpBinding GetBinding()
		{
            //TODO: mws -- the binding configuration is setup manually here
            // this would bypass what is in the config and could be the 
            // reason for a timeout after an hour?
			BasicHttpBinding binding = new BasicHttpBinding();
			binding.MaxBufferSize = 2147483647;
			binding.MaxReceivedMessageSize = 2147483647;
			binding.OpenTimeout = TimeSpan.FromMinutes(2.0d);
			binding.SendTimeout = TimeSpan.FromMinutes(60.0d);
			binding.CloseTimeout = TimeSpan.FromMinutes(2.0d);

			return binding;
		}

		/*public static void CloseAccessor(T accessor)
		{
			try
			{
				((ICommunicationObject)accessor).Close();
			}
			catch (Exception ex)
			{
				throw new Exception("Failed to close accessor.", ex);
			}
		}*/
	}
}
