using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

//using System.Xml.Serialization;
//using System.Text;
using System.Threading;
using Jaxis.Engine.Base.Device;
using Jaxis.Interfaces;
using Jaxis.Interfaces.Tags;
using Jaxis.Util.Log4Net;
using System.Collections.Concurrent;
using System.Threading.Tasks;
using JaxisExtensions;

namespace Jaxis.Engine
{
    public interface IEngine : IDisposable
    {
        void Start();
        void Stop();
        void Save();
        List<IDevice> GetDeviceList();
        List<IDeviceConfigView> GetDeviceConfigList();
        IDevice GetDevice(string _deviceId);
        EngineState GetEngineState();
        void StartDevice(string _deviceId);
        void StopDevice(string _deviceId);
        void RegisterDevice(IDevice _device);
        DeviceState GetDeviceState(string _deviceId);
        void LoadDevices(List<DeviceConfig> _configs);
        void LoadFilters(List<FilterConfig> _filterConfigs, List<IFilter> _filters);
        void UpdateDevice(DeviceConfig _device);
        //void UpdateDeviceOptions(string _DeviceID, List<DeviceConfigOption> _Options);
        void UpdateFilterOptions(string _deviceId, string _name, List<DeviceConfigOption> _options);
    }

    public class EngineManager
    {
        private static IEngine m_Engine = null;

        public static IEngine GetEngine( )
        {
            if( null == m_Engine )
            {
                m_Engine = new Engine( );
            }
            return m_Engine;
        }
    }

    internal class Engine : IEngine
    {
        private static readonly Mutex m_Mutex = new Mutex();
        class DeviceMessage : IDisposable
        {
            public IConsumer Consumer { get; set; }
            public IMessage Message { get; set; }

            public void Dispose()
            {
                Consumer = null;
                Message = null;
            }
        }

        readonly BlockingCollection<DeviceMessage> m_DeviceMessageCollection = new BlockingCollection<DeviceMessage>( );

        public delegate string Consume( IMessage _message );

        private static readonly List<IDevice> m_Devices = new List<IDevice>( );
        private static List<AppDomain> m_AppDomains = new List<AppDomain>( );
        private static readonly List<IFilter> m_Filters = new List<IFilter>( );
        private static EngineState m_State;

        protected string m_Path = string.Empty;
        private readonly List<Task> m_TaskList = new List<Task>( );
        private Task m_Cleaner;

        internal Engine( )
        {
            m_Path = BaseDevice.GetPath();

            SendInfo( );

            m_State = EngineState.Stopped;
            Load( );
        }
        
        public void Dispose( )
        {
            Save( );
        }

        public void Save( )
        {
            try
            {
                var configFile = m_Path + "\\" + System.Configuration.ConfigurationManager.AppSettings["ConfigFile"];
                System.IO.File.Delete( "OldConfig.xlm" );
                System.IO.File.Move( configFile, "OldConfig.xlm" );
                using( var writer = new StreamWriter( configFile ) )
                {
                    var configs = new DeviceConfigCollection( );

                    foreach( IDevice D in m_Devices )
                    {
                        configs.Configs.Add( D.Config as DeviceConfig );
                    }
                    foreach( IFilter F in m_Filters )
                    {
                        configs.Filters.Add( F.Config as FilterConfig );
                    }
                    DeviceConfig.SerializeObject<DeviceConfigCollection>( writer, configs );
                }
            }
            catch( Exception exp )
            {
                Log.WriteException( "Engine:: Save", exp );
                var configFile = m_Path + "\\" + System.Configuration.ConfigurationManager.AppSettings["ConfigFile"];
                System.IO.File.Move( "OldConfig.xlm", configFile );
            }
        }

        protected void Load( )
        {
            DeviceConfigCollection configs = null;
            //m_Devices = new List<IDevice>();
            //m_Filters = new List<IFilter>();

            try
            {
                var configFile = m_Path + "\\" + System.Configuration.ConfigurationManager.AppSettings["ConfigFile"];
                if( File.Exists( configFile ) )
                {
                    using( var reader = new StreamReader( configFile ) )
                    {
                        var data = reader.ReadToEnd( );
                        configs = DeviceConfig.DeserializeObject<DeviceConfigCollection>( data );
                        if( null != configs )
                        {
                            LoadDevices( configs.Configs );
                            LoadFilters( configs.Filters, m_Filters );
                        }
                    }
                }
                else
                {
                    Log.Error( "No deivce config files." );
                }
            }
            catch( Exception exp )
            {
                Log.WriteException( "Engine:: Load", exp );
            }
        }

        public void LoadFilters( List<FilterConfig> _filterConfigs, List<IFilter> _filters )
        {
            try
            {
                if( null != _filterConfigs && 0 < _filterConfigs.Count )
                {
                    var i = 0;
                    _filterConfigs.ForEach( F =>
                    {
                        var conf = new object[] { F };
                        var fullPath = m_Path + "\\" + F.AssemblyName;
                        //if( File.Exists( F.AssemblyName ) )
                        if( File.Exists( fullPath ) )
                        {
                            var asm = Assembly.LoadFrom( fullPath );
                            //Assembly Asm = Assembly.LoadFrom(F.AssemblyName);
                            var tps = asm.GetTypes( );
                            Array.ForEach( tps, T =>
                            {
                                if( F.AssemblyType.Equals( T.ToString( ) ) )
                                {
                                    var newFilter = (IFilter)Activator.CreateInstance( T, conf );
                                    _filters.Add( newFilter );
                                }
                            } );
                            i++;
                        }
                    } );
                }
            }
            catch( Exception exp )
            {
                Log.WriteException( "Engine:: LoadFilters", exp );
            }
        }

        public void LoadDevices( List<DeviceConfig> _configs )
        {
            try
            {
                if( null != _configs && 0 < _configs.Count )
                {
                    _configs.ForEach( D =>
                    {
                        try
                        {
                            Guid id;
                            if (false == Guid.TryParse(  D.ID, out id ))
                            {
                                D.ID = Guid.NewGuid().ToString();
                            }
                            string FullPath = m_Path + "\\" + D.AssemblyName;
                            if( File.Exists( FullPath ) )
                            {
                                Log.Debug(string.Format("Device: {0} - {2} at {1}", D.Name, FullPath, D.ID));
                                var asm = Assembly.LoadFrom(FullPath);
                                var type = asm.GetType(D.AssemblyType);
                                var newDevice = (IDevice)Activator.CreateInstance(type, new object[] { D });
                                LoadFilters( D.Filters, newDevice.Filters );
                                RegisterDevice( newDevice );
                            }
                            else
                            {
                                Log.Error(string.Format("UNABLE TO LOAD - Device: {0} - {2} at {1}", D.Name, FullPath, D.ID));
                            }
                        }
                        catch (Exception err )
                        {
                            Log.Exception( err );
                        }
                    } );
                }
            }
            catch( Exception exp )
            {
                Log.WriteException( "Engine:: LoadDevices", exp );
            }
        }

        public List<IDevice> GetDeviceList( )
        {
            return m_Devices;
        }


        public List<IDeviceConfigView> GetDeviceConfigList()
        {
            var rc = new List<IDeviceConfigView>();

            try
            {
                List<IDevice> devices = GetDeviceList();
                rc.AddRange(devices.Select(D => D.Config as IDeviceConfigView));
            }
            catch (Exception exp)
            {
                Log.WriteException("EngineConfigService:: GetDeviceList", exp);
            }
            return rc;
        }

        public IDevice GetDevice(string _DeviceID)
        {
            IDevice rc = null;

            var dev = m_Devices.Find( D => D.Config.ID == _DeviceID );
            if (null != dev)
            {
                rc = dev;
            }
            return rc;
        }

        public DeviceState GetDeviceState(string _DeviceID)
        {
            var rc = DeviceState.Stopped;
            try
            {
                var dev = m_Devices.Find( D => D.Config.ID == _DeviceID );
                if( null != dev )
                {
                    rc = dev.Config.State;
                }
            }
            catch( Exception exp )
            {
                Log.WriteException( "Engine:: GetDeviceState", exp );
            }
            return rc;
        }

        //public void UpdateDeviceOptions(string _DeviceID, List<DeviceConfigOption> _Options)
        //{
        //    try
        //    {
        //        IDevice Dev = m_Devices.Find( D => D.Config.ID == _DeviceID );
        //        if( null != Dev )
        //        {
        //            StopDevice( _DeviceID );
        //            Dev.Config.Options = _Options;
        //            StartDevice( _DeviceID );
        //            Save();
        //        }
        //    }
        //    catch( Exception exp )
        //    {
        //        Log.WriteException( "Engine:: UpdateDeviceOptions", exp );
        //    }
        //}

        public void UpdateDevice(DeviceConfig _device)
        {
            try
            {
                var dev = m_Devices.Find(D => D.Config.ID == _device.ID);
                StopDevice(dev.Config.ID);
                dev.Config.ID = _device.ID;
                dev.Config.ConsumerMessageType = _device.ConsumerMessageType;
                dev.Config.Name = _device.Name;
                dev.Config.ProducerMessageType = _device.ProducerMessageType;
                dev.Config.Options = _device.Options;
                dev.Config.State = _device.State;

                foreach (var filter in _device.Filters)
                {
                    var fil = dev.Filters.Find(F => F.Config.Name == filter.Name);
                    if (null != fil)
                    {
                        fil.Config.Options = filter.Options;
                    }
                    var fc = ((DeviceConfig) dev.Config).Filters.Find(F => F.Name == filter.Name);
                    if (null != fc)
                    {
                        fc.Options = filter.Options;
                    }
                }

                if (DeviceState.Started == dev.Config.State)
                {
                    StartDevice(dev.Config.ID);
                }
                Save();
            }
            catch (Exception exp)
            {
                Log.WriteException("Engine:: UpdateDeviceOptions", exp);
            }
        }

        public void UpdateFilterOptions(string _deviceId, string _name, List<DeviceConfigOption> _options)
        {
            try
            {
                var dev = m_Devices.Find( D => D.Config.ID == _deviceId );
                if( null != dev )
                {
                    StopDevice(_deviceId);
                    var fil = dev.Filters.Find(F => F.Config.Name == _name);
                    if( null != fil )
                    {
                        fil.Config.Options = _options;
                    }
                    var fc = ((DeviceConfig) dev.Config).Filters.Find(F => F.Name == _name);
                    if (null != fc)
                    {
                        fc.Options = _options;
                    }

                    if (DeviceState.Started == dev.Config.State)
                    {
                        StartDevice(dev.Config.ID);
                    }
                    Save();
                }
            }
            catch( Exception exp )
            {
                Log.WriteException( "Engine:: UpdateFilterOptions", exp );
            }
        }

        //public void UnLoadDevice(string _DeviceID)
        //{
        //IDevice Dev = m_Devices.Find(D => D.Config.ID == _DeviceID);
        //if (null != Dev)
        //{
        //Dev.Stop;
        //m_Devices.Remove(Dev);
        //if (Dev.Config.Type == DeviceType.DataProducer)
        //{
        //    ((IProducer)Dev).Produce -= Produce;
        //}
        //Dev.Dispose();
        //AppDomain Ad = m_AppDomains.Find(A => A.FriendlyName == ((DeviceConfig)Dev.Config).AssemblyName);
        //AppDomain.Unload(Ad);
        //}
        //}

        public void StopDevice( string _deviceId )
        {
            try
            {
                var dev = m_Devices.Find( D => D.Config.ID == _deviceId );
                if( null != dev )
                {
                    Log.Debug( string.Format( "Stopping: {0} - {1}", dev.HardwareID, dev.Config.Name ) );
                    dev.Stop( );
                    Save();
                }
            }
            catch( Exception exp )
            {
                Log.WriteException( "Engine:: StopDevice", exp );
            }
        }

        public void StartDevice( string _deviceId )
        {
            try
            {
                var dev = m_Devices.Find( D => D.Config.ID == _deviceId );
                if( null != dev )
                {
                    Log.Debug( string.Format( "Starting: {0} - {1}", dev.HardwareID, dev.Config.Name ) );
                    dev.Start( );
                    Save();
                }
            }
            catch( Exception exp )
            {
                Log.WriteException( "Engine:: StartDevice", exp );
            }
        }

        public EngineState GetEngineState( )
        {
            return m_State;
        }

        public void CleanupTasks( )
        {
            while (m_State == EngineState.Started)
            {
                try
                {
                    Thread.Sleep( 10000 );
                    if (0 < m_TaskList.Count)
                    {
                        try
                        {
                            m_Mutex.WaitOne();
                            var completed = m_TaskList.Where(t => t.IsCompleted).ToList();
                            //Log.Debug( string.Format("{0} tasks of {1} that are complete", completed.Count(), m_TaskList.Count));
                            foreach (var task in completed)
                            {
                                m_TaskList.Remove(task);
                                task.Dispose();
                            }
                        }
                        finally
                        {
                            m_Mutex.ReleaseMutex();
                        }
                    }
                }
                catch (Exception exp)
                {
                    Log.WriteException("Engine:: CleanupTasks", exp);
                }
            }
        }


        public void Start( )
        {
            try
            {
                Log.Debug( "Engine::Start" );
                if( null != m_Devices )
                {
                    //m_Devices.AsParallel( ).ForAll( D =>
                    m_Devices.ForEach( D =>
                    {
                        Log.Debug(string.Format("Engine::Start {0} {2} - {1}", D.Config.State, D.Config.AssemblyType, D.Config.Name));
                        if( D.InitalState == DeviceState.Started )
                        {
                            D.Start( );
                        }
                    });
                }
                m_State = EngineState.Started;

                m_Cleaner = Task.Factory.StartNew(CleanupTasks);
                //m_Cleaner.Start();
            }
            catch( Exception exp )
            {
                Log.WriteException( "Engine:: Start", exp );
            }
        }

        public void Stop( )
        {
            try
            {
                m_Cleaner.Wait(1500);
                try
                {
                    Task.WaitAll(m_TaskList.ToArray(), 1500);

                    m_TaskList.Clear();
                }
                catch (Exception exp)
                {
                    Log.WriteException("Engine::Stop - stop all tasks", exp);
                }

                if( null != m_Devices )
                {
                    m_Devices.ForEach( D => D.Stop( ) );
                }
                m_State = EngineState.Stopped;
            }
            catch( Exception exp )
            {
                Log.WriteException( "Engine:: Stop", exp );
            }
        }

        public void RegisterDevice( IDevice _device )
        {
            try
            {
                var types = _device.GetType( ).GetInterfaces( );

                if( types.Contains( typeof( IProducer ) ) )
                {
                    ( (IProducer)_device ).Produce = Produce;
                    _device.Type |= DeviceType.DataProducer;
                }
                if( ( types.Contains( typeof( IConsumer ) ) ) )
                {
                    _device.Type |= DeviceType.DataConsumer;
                }

                Log.Debug( string.Format( "Register Device {2} {0} - {1}", _device.HardwareID, _device.Type, _device.Config.AssemblyType ) );

                m_Devices.Add( _device );
            }
            catch( Exception exp )
            {
                Log.WriteException( "Engine:: RegisterDevice", exp );
            }
        }

        public string Produce( IMessage _message )
        {
            string rc = null;

            try
            {
                if( null != m_Devices && null != _message )
                {
                    foreach( var d in m_Devices )
                    {
                        if( 0 != ( d.Config.ConsumerMessageType & _message.Type ) 
                            && d is IConsumer 
                            // Make sure the message didnt come from yourself.
                            && 0 != d.Config.AssemblyType.CompareTo( _message.Driver ))
                        {
                            try
                            {
                                var value = _message.Clone() as IMessage;

                                var msg = new DeviceMessage() { Consumer = d as IConsumer, Message = value };
                                m_DeviceMessageCollection.Add(msg);
                                try
                                {
                                    m_Mutex.WaitOne();
                                    m_TaskList.Add(Task.Factory.StartNew(ProcessConsume));
                                }
                                finally
                                {
                                    m_Mutex.ReleaseMutex();
                                }
                            }
                            catch (Exception exp)
                            {
                                Log.WriteException(string.Format("Engine::Produce - {0} : {1}", _message.Driver, _message.Type ), exp);
                            }
                        }
                    }
                }
            }
            catch( Exception exp )
            {
                Log.WriteException("Engine:: Produce", exp);
            }
            return rc;
        }
        
        protected void ProcessConsume( )
        {
            try
            {
                using (var ds = m_DeviceMessageCollection.Take())
                {
                    if (null != ds)
                    {
                        var success = ds.Consumer.Filters.Aggregate(true, (current, F) => current | F.Filter(ds.Message));
                        if (success == true && ds.Consumer.Config.State != DeviceState.Stopped)
                        {
                            ds.Consumer.Consume(ds.Message);
                        }
                    }
                    Log.Debug( string.Format( "Message Count = {0}", m_DeviceMessageCollection.Count) );
                }
            }
            catch (Exception exp)
            {
                Log.WriteException("Engine::ProcessConsume", exp);
            }
        }

        private void SendInfo( )
        {
            //            WebRequest req = null;
            //            WebResponse rsp = null;
            //
            //            try
            //            {
            //                string fileName = "C:\test.xml";
            //                string uri = "http://localhost/PostXml/Default.aspx";
            //                req = WebRequest.Create(uri);
            //                //req.Proxy = WebProxy.GetDefaultProxy(); // Enable if using proxy
            //                req.Method = "POST";        // Post method
            //                req.ContentType = "text/xml";     // content type
            //                // Wrap the request stream with a text-based writer
            //                StreamWriter writer = new StreamWriter(req.GetRequestStream());
            //                // Write the XML text into the stream
            //                writer.WriteLine(this.GetTextFromXMLFile(fileName));
            //                writer.Close();
            //                // Send the data to the webserver
            //                rsp = req.GetResponse();
            //            }
            //            catch(WebException webEx)
            //            {
            //            }
            //       catch(Exception ex)
            //       {
            //
            //       }
            //       finally
            //       {
            //        if(req != null) req.GetRequestStream().Close();
            //        if(rsp != null) rsp.GetResponseStream().Close();
            //       }Function to read xml data from local system
            //      /// <summary>
            //      /// Read XML data from file
            //      /// </summary>
            //      /// <param name="file"></param>
            //      /// <returns>returns file content in XML string format</returns>
            //      private string GetTextFromXMLFile(string file)
            //      {
            //       StreamReader reader = new StreamReader(file);
            //       string ret = reader.ReadToEnd();
            //       reader.Close();
            //       return ret;
            //      }
        }
    }
}