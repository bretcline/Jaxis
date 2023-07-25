using System;
using System.Collections.Generic;
using System.Reflection;
using Jaxis.Engine;
using Jaxis.Engine.Base.Device;
using Jaxis.Interfaces;
using Jaxis.Util.Log4Net;
using Jaxis.Utility.Encryption;

namespace EngineConfigWCF
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "POSClientInterface" in both code and config file together.
    public class EngineConfigService : IEngineConfigService
    {
        private static IEngine m_Engine;

        public static void SetEngine( IEngine _Engine )
        {
            m_Engine = _Engine;
        }

        public List<DeviceConfig> GetDeviceList( )
        {
            List<DeviceConfig> rc = new List<DeviceConfig>( );

            try
            {
                if( null != m_Engine )
                {
                    List<IDevice> Devices = m_Engine.GetDeviceList( );
                    foreach( IDevice D in Devices )
                    {
                        rc.Add( D.Config as DeviceConfig );
                    }
                }
            }
            catch( Exception exp )
            {
                Log.WriteException( "EngineConfigService:: GetDeviceList", exp );
            }
            return rc;
        }

        public DeviceState GetDeviceState( DeviceConfig _Device )
        {
            DeviceState rc = DeviceState.Stopped;
            try
            {
                if( null != m_Engine )
                {
                    rc = m_Engine.GetDeviceState( _Device.ID );
                }
            }
            catch( Exception exp )
            {
                Log.WriteException( "EngineConfigService:: GetDeviceState", exp );
            }

            return rc;
        }

        public void StartDevice( DeviceConfig _Device )
        {
            try
            {
                if( null != m_Engine )
                {
                    m_Engine.StartDevice( _Device.ID );
                }
            }
            catch( Exception exp )
            {
                Log.WriteException( "EngineConfigService:: StartDevice", exp );
            }
        }

        public void StopDevice( DeviceConfig _Device )
        {
            try
            {
                if( null != m_Engine )
                {
                    m_Engine.StopDevice( _Device.ID );
                }
            }
            catch( Exception exp )
            {
                Log.WriteException( "EngineConfigService:: StopDevice", exp );
            }
        }

        public string GetEventLog( )
        {
            string rc = null;

            try
            {
                string fname = BaseDevice.GetPath() + GetLogFileName( "RollingFile" );
                if( null != fname )
                {
                    // MLF File is locked unless you add <lockingModel type="log4net.Appender.FileAppender+MinimalLock" /> to app.config...
                    rc = System.IO.File.ReadAllText( fname );
                }
            }
            catch( Exception exp )
            {
                Log.WriteException( "EngineConfigService:: GetEventLog", exp );
            }
            return rc;
        }

        public EngineState GetEngineState( )
        {
            EngineState rc = EngineState.Stopped;
            try
            {
                if( null != m_Engine )
                {
                    rc = m_Engine.GetEngineState( );
                }
            }
            catch( Exception exp )
            {
                Log.WriteException( "EngineConfigService:: GetEngineState", exp );
            }
            return rc;
        }

        public void StopEngine( )
        {
            try
            {
                if( null != m_Engine )
                {
                    m_Engine.Stop( );
                }
            }
            catch( Exception exp )
            {
                Log.WriteException( "EngineConfigService:: StopEngine", exp );
            }
        }

        public void StartEngine( )
        {
            try
            {
                if( null != m_Engine )
                {
                    m_Engine.Start( );
                }
            }
            catch( Exception exp )
            {
                Log.WriteException( "EngineConfigService:: StartEngine", exp );
            }
        }

        public string GetEngineRev( )
        {
            string rc = null;
            try
            {
                rc = Assembly.GetExecutingAssembly( ).GetName( ).Version.ToString( );
            }
            catch( Exception exp )
            {
                Log.WriteException( "EngineConfigService:: GetEngineRev", exp );
            }
            return rc;
        }

        public List<DeviceConfig> GetLocalDevices()
        {
            List<DeviceConfig> Configs = new List<DeviceConfig>();
            try
            {
                var path = BaseDevice.GetPath();
                string[] files = System.IO.Directory.GetFiles(path, "*.dll");
                foreach (string f in files)
                {
                    try
                    {
                        Log.Debug( f );
                        Assembly Asm = Assembly.LoadFrom(f);
                        if (null != Asm)
                        {
                            Type[] Types = Asm.GetTypes();
                            if (null != Types)
                            {
                                foreach (Type T in Types)
                                {
                                    try
                                    {
                                        if (null != T.GetInterface("IDevice"))
                                        {
                                            var obj = (IDevice)Activator.CreateInstance(T);
                                            if (obj is IDevice)
                                            {
                                                Configs.Add(((IDevice)obj).Config as DeviceConfig);
                                            }
                                            else
                                            {
                                                MethodInfo M = T.GetMethod("GetDefaultDeviceConfig");
                                                if (null != M)
                                                {
                                                    Log.Debug(string.Format("Type: {0}", T.Name));
                                                    Configs.Add((DeviceConfig)M.Invoke(null, null));
                                                }
                                            }
                                        }
                                    }
                                    catch (Exception e)
                                    {
                                        Log.WriteException("EngineConfigService::GetLocalDevices 3", e);
                                    }
                                }
                            }
                        }
                    }
                    catch (Exception err)
                    {
                        Log.WriteException("EngineConfigService::GetLocalDevices 2", err);
                    }
                }
            }
            catch (Exception exp)
            {
                Log.WriteException("EngineConfigService::GetLocalDevices 1", exp);
            }
            return Configs;
        }

        public void AddDevice(DeviceConfig _Config)
        {
            try
            {
                LoadDevice(_Config);
                m_Engine.Save();
            }
            catch (Exception exp)
            {
                Log.WriteException("EngineConfigService:: AddDevice", exp);
            }
        }

        public List<FilterConfig> GetLocalFilters()
        {
            List<FilterConfig> Configs = new List<FilterConfig>();
            try
            {
                string[] files = System.IO.Directory.GetFiles(BaseDevice.GetPath(), "*.dll");
                foreach (string f in files)
                {
                    Assembly Asm = Assembly.LoadFrom(f);
                    if (null != Asm)
                    {
                        Type[] Types = Asm.GetTypes();
                        if (null != Types)
                        {
                            foreach (Type T in Types)
                            {
                                if (null != T.GetInterface("IFilter"))
                                {
                                    MethodInfo M = T.GetMethod("GetDefaultFilterConfig");
                                    if (null != M)
                                    {
                                        Configs.Add((FilterConfig)M.Invoke(null, null));
                                    }
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception exp)
            {
                Log.WriteException("EngineConfigService:: GetLocalFilters", exp);
            }
            return Configs;
        }

        public void AddFilter(DeviceConfig _Device, FilterConfig _Config)
        {
            try
            {
                LoadFilter(_Device, _Config);
                m_Engine.Save();
            }
            catch (Exception exp)
            {
                Log.WriteException("EngineConfigService:: AddFilter", exp);
            }
        }

        private void LoadDevice( DeviceConfig Config )
        {
            try
            {
                Assembly Asm = Assembly.LoadFrom(string.Format("{0}\\{1}", BaseDevice.GetPath(), Config.AssemblyName));
                IDevice NewDevice = (IDevice)Activator.CreateInstance(Asm.GetType(Config.AssemblyType), new object[] { Config });
                m_Engine.RegisterDevice( NewDevice );
            }
            catch( Exception exp )
            {
                Log.WriteException( "EngineConfigService:: LoadDevice", exp );
            }
        }

        private void LoadFilter(DeviceConfig Device, FilterConfig Config)
        {
            try
            {
                //m_Engine.LoadFilters(Device.Filters, Config);

                IDevice Dev = m_Engine.GetDevice(Device.ID);
                Assembly Asm = Assembly.LoadFrom(string.Format("{0}\\{1}", BaseDevice.GetPath(), Config.AssemblyName));
                IFilter NewFilter = (IFilter)Activator.CreateInstance(Asm.GetType(Config.AssemblyType), new object[] { Config });
                (Dev.Config as DeviceConfig).Filters.Add(Config);
                Dev.Filters.Add(NewFilter);
            }
            catch (Exception exp)
            {
                Log.WriteException("EngineConfigService:: LoadFilter", exp);
            }
        }

        public string DownloadDevicePlugin(DeviceConfig _Config, byte[] _Device, List<string> DependentNames, List<byte[]> Dependents, byte[] _iv)
        {
            int i = 0;
            string rc = null;
            try
            {
                if (null != _Config)
                {
                    System.IO.File.WriteAllBytes(BaseDevice.GetPath() + "\\" + _Config.AssemblyName, BlowFishEncryption.Decrypt(_Device, _iv));
                    foreach (string Dependent in DependentNames)
                    {
                        if (i < Dependents.Count)
                        {
                            System.IO.File.WriteAllBytes(BaseDevice.GetPath() + "\\" + Dependent, BlowFishEncryption.Decrypt(Dependents[i], _iv));
                        }
                        i++;
                    }
//                    LoadDevice(_Config);
                }
            }
            catch( Exception exp )
            {
                Log.WriteException( "EngineConfigService:: DownloadDevicePlugin", exp );
                rc = exp.ToString( );
            }
            return rc;
        }

        public string DownloadFilterPlugin( DeviceConfig _Device, FilterConfig _Config, byte[] _Filter, byte[] _iv )
        {
            string rc = null;
            try
            {
                if( null != _Config )
                {
                    System.IO.File.WriteAllBytes(BaseDevice.GetPath() + "\\" + _Config.AssemblyName, BlowFishEncryption.Decrypt(_Filter, _iv));
                    //List<IDevice> Devices = m_Engine.GetDeviceList( );
                    //List<FilterConfig> Configs = new List<FilterConfig>( );
                    //Configs.Add( _Config );
                    //if( null != Devices.Find( D => D.Config.Name == _Device.Name ) )
                    //{
                    //    m_Engine.LoadFilters( Configs, Devices.Find( D => D.Config.Name == _Device.Name ).Filters );
                    //}
                }
            }
            catch( Exception exp )
            {
                Log.WriteException( "EngineConfigService:: DownloadFilterPlugin", exp );
                rc = exp.ToString( );
            }
            return rc;
        }

        public void UpdateOptions( DeviceConfig _Config )
        {
            try
            {
                //m_Engine.UpdateDeviceOptions( _Config.ID, _Config.Options );
                m_Engine.UpdateDevice( _Config );
            }
            catch( Exception exp )
            {
                Log.WriteException( "EngineConfigService:: UpdateOptions", exp );
            }
        }

        public void UpdateFilterOptions( DeviceConfig _Config, FilterConfig _FilterConfig )
        {
            try
            {
                m_Engine.UpdateFilterOptions( _Config.ID, _FilterConfig.Name, _FilterConfig.Options );
            }
            catch( Exception exp )
            {
                Log.WriteException( "EngineConfigService:: UpdateFilterOptions", exp );
            }
        }

        public void UnLoadDevice( DeviceConfig _Config )
        {
            try
            {
            }
            catch( Exception exp )
            {
                Log.WriteException( "EngineConfigService:: UnLoadDevice", exp );
            }
        }

        public void UnLoadFilter( DeviceConfig _Config, FilterConfig _FilterConfig )
        {
            try
            {
            }
            catch( Exception exp )
            {
                Log.WriteException( "EngineConfigService:: UnLoadFilter", exp );
            }
        }

        private static string GetLogFileName( string _AppenderName )
        {
            string rc = null;
            try
            {
                log4net.Repository.ILoggerRepository RootRep;
                RootRep = log4net.LogManager.GetRepository( );

                foreach( log4net.Appender.IAppender iApp in RootRep.GetAppenders( ) )
                {
                    if( iApp.Name.CompareTo( _AppenderName ) == 0 && iApp is log4net.Appender.FileAppender )
                    {
                        log4net.Appender.FileAppender fApp = (log4net.Appender.FileAppender)iApp;
                        rc = fApp.File;
                    }
                }
            }
            catch( Exception exp )
            {
                Log.WriteException( "EngineConfigService:: GetLogFileName", exp );
            }
            return rc;
        }
    }
}