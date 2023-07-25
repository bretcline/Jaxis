using System;
using System.Collections.Concurrent;
using System.Globalization;
using System.Linq;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using Jaxis.BeverageManagement.Plugin.PourDataService;
using Jaxis.Engine.Base.Device;
using Jaxis.Interfaces;
using Jaxis.Inventory.Data;
using Jaxis.Inventory.Data.IBLDataItems;
using Jaxis.MessageLibrary;
using Jaxis.Util.Log4Net;
using JaxisExtensions;
using JaxisMath;
using Jaxis.Interfaces.Tags;
using SubSonic.Query;
using IDevice = Jaxis.Inventory.Data.IDevice;
using IPour = Jaxis.Inventory.Data.IPour;
using ITag = Jaxis.Inventory.Data.ITag;

namespace Jaxis.BeverageManagement.Plugin
{
    /*
        <DeviceConfig>
            <AssemblyName>Jaxis.BeverageManagement.Plugin.dll</AssemblyName>
            <AssemblyType>Jaxis.BeverageManagement.Plugin.DataConsumer</AssemblyType>
            <AssemblyVersion>1.0</AssemblyVersion>
            <ID>101</ID>
            <Name>Jaxis Consumer</Name>
            <Type>DataConsumer</Type>
            <State>Started</State>
            <ProducerMessageType>32</ProducerMessageType>
            <ConsumerMessageType>1</ConsumerMessageType>
            <Options>
                <string>http://localhost:8223/HostWCFService/PourEngineService/</string>
                <string>10</string>
                <string></string>
                <string>35</string><!-- bottle volumn less than this, auto-detach.  0 == no auto-detach -->
                <string>false</string>
            </Options>
        </DeviceConfig>
    */

    class DataConsumer : BaseBevManDevice, IConsumer, IProducer
    {
        protected class HeartbeatMessage : IDisposable
        {
            public PhaseMessage Phase { get; set; }
            public IDevice Device { get; set; }

            public void Dispose()
            {
                Phase = null;
                Device = null;
            }
        }


        [Obfuscation(Exclude = true)]
        public static DeviceConfig GetDefaultDeviceConfig()
        {
            var rc = new DeviceConfig();
            System.Reflection.Assembly Asm = System.Reflection.Assembly.GetExecutingAssembly();
            rc.AssemblyName = Asm.ManifestModule.Name;
            rc.AssemblyType = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName;
            rc.AssemblyVersion = System.Diagnostics.FileVersionInfo.GetVersionInfo(BaseDevice.GetPath() + "\\" + rc.AssemblyName).FileVersion;
            rc.ID = Guid.NewGuid().ToString();
            rc.Name = "Jaxis Consumer";
            rc.Type = DeviceType.DataConsumer;
            rc.State = DeviceState.Stopped;
            rc.ProducerMessageType = 32;
            rc.ConsumerMessageType = 1;

            rc.Options.Add(new DeviceConfigOption
            {
                Name = "WCFPath",
                Value = "http://localhost:8223/HostWCFService/PourEngineService/"
            });
            rc.Options.Add(new DeviceConfigOption {Name = "MaxSpoutDiameter", Value = "10"});
            rc.Options.Add(new DeviceConfigOption {Name = "Allow Multi-Pour", Value = "true"});
            rc.Options.Add(new DeviceConfigOption {Name = "AutoDetachVolume", Value = "35"});
            rc.Options.Add(new DeviceConfigOption {Name = "Simulator", Value = "false"});
            rc.Options.Add(new DeviceConfigOption { Name = "Allow Half-Pour", Value = "true" });
            return rc;
        }

        protected const string DATE_TIME_FORMAT = "yyyy-MM-dd'T'HH:mm:ss.fffK";
        protected Single m_MaxSpoutDiameter = 10.0f;
        protected int m_EmptyAmount = 0;
        private int m_SkipCount = 0;
        private DateTime m_SkipTime = DateTime.Now;
        private TimeSpan m_SkipInterval = new TimeSpan( 0, 0, 0, 30 );
        private IBLStandardPour m_LiquorStandardPour;
        private IBLStandardPour m_BeerStandardPour;
        private IBLStandardPour m_WineStandardPour;
        private IBLCategory m_Liquor;
        private IBLCategory m_Wine;
        private IBLCategory m_Beer;
        private ILocation m_UnknownLocation;
        private IUPCItem m_UnknownUPC;
        private Task m_ActivityCleanup;
        private bool m_AllowMultiPour;
        private bool m_AllowHalfPour;

        BlockingCollection<HeartbeatMessage> m_HeartbeatMessages = new BlockingCollection<HeartbeatMessage>();
        private Task m_HeartbeatProcessor;

        private Dictionary< Guid, IStandardNozzle> m_Nozzles = new Dictionary<Guid, IStandardNozzle>();
                
        public DataConsumer( )
            : this(GetDefaultDeviceConfig())
        {
        }


        public DataConsumer( IDeviceConfig _config )
            : base( _config )
        {

        }

        public string ConnectionString { get { return m_ConnectionString; } }

        public override void Start( )
        {
            try
            {
                m_StandardNozzle = BLManagerFactory.Get().ManageStandardNozzles().Get(Guid.Empty);
                m_LiquorStandardPour = DataManagerFactory.Get().Manage<IStandardPour>().GetAll().Where(n => n.Name == "Liquor").FirstOrDefault() as IBLStandardPour;
                m_BeerStandardPour = DataManagerFactory.Get().Manage<IStandardPour>().GetAll().Where(n => n.Name == "Beer").FirstOrDefault() as IBLStandardPour;
                m_WineStandardPour = DataManagerFactory.Get().Manage<IStandardPour>().GetAll().Where(n => n.Name == "Wine").FirstOrDefault() as IBLStandardPour; 

                m_Liquor = DataManagerFactory.Get().Manage<ICategory>().GetAll().Where(n => n.Name == "Liquor").FirstOrDefault() as IBLCategory;
                m_Beer = DataManagerFactory.Get().Manage<ICategory>().GetAll().Where(n => n.Name == "Beer").FirstOrDefault() as IBLCategory;
                m_Wine = DataManagerFactory.Get().Manage<ICategory>().GetAll().Where(n => n.Name == "Wine").FirstOrDefault() as IBLCategory;

                m_UnknownLocation = DataManagerFactory.Get().Manage<ILocation>().Get(Guid.Empty);
                m_UnknownUPC = DataManagerFactory.Get().Manage<IUPCItem>().Get(Guid.Empty);


                LoadAllTags();
                LoadAllNozzles();
                LoadDeviceLocations();

                m_MaxSpoutDiameter = Config.GetMaxSpoutDiameter( );
                m_EmptyAmount = Config.GetAutoDetachVolume( );
                m_AllowMultiPour = Config.GetMultiPour();
                m_AllowHalfPour = Config.GetHalfPour();

                m_SkipTime = DateTime.Now;
                State = DeviceState.Started;
                Config.State = DeviceState.Started;

                m_ActivityCleanup = Task.Factory.StartNew(CleanupActivityLog);

                m_HeartbeatProcessor = Task.Factory.StartNew(ProcessHeartbeat);

                this.AutoMove = MoveTag;
            }
            catch( Exception exp )
            {
                Log.WriteException( "DataConsumer::Start", exp );
            }
        }

        private void MoveTag(ITag _tag)
        {
            if (null != _tag.CurrentInventory)
            {
                var inv = _tag.CurrentInventory;
                inv.LocationID = _tag.LocationID;
                DataManagerFactory.Get().Manage<IInventory>().Save(inv);
            }
        }

        private void ProcessHeartbeat()
        {
            while (false == m_Stop)
            {
                try
                {
                    HeartbeatMessage heartbeat;
                    m_HeartbeatMessages.TryTake(out heartbeat, 500);
                    if (null != heartbeat)
                    {
                        var msg = heartbeat.Phase;
                        var device = heartbeat.Device;
                        var tag = GetTagForHeartbeat(msg.TagID, device.Name);

                        var man = DataManagerFactory.Get().Manage<IActivityLog>();
                        var log = man.Create();
                        log.ReadTime = msg.ReadTime;
                        log.ActivityTime = msg.ReadTime;
                        log.ActivityType = (int)msg.EventType;
                        log.DeviceID = device.DeviceID;
                        log.LocationID = (m_DeviceLocations.ContainsKey(device.Name)) ? m_DeviceLocations[device.Name] : Guid.Empty;
                        log.SignalStrength = msg.SignalStrength;
                        log.TagID = tag.TagID;
                        log.TagNumber = tag.TagNumber;

                        //man.Save(log);

                        ProduceMessage(log);
                        heartbeat.Dispose();
                    }
                }
                catch (Exception err)
                {
                    Log.WriteException( "ProcessHeartbeat()", err);
                }
            }
        }


        private void CleanupActivityLog()
        {
            while (State != DeviceState.Stopped)
            {
                try
                {
                    string sqlCount = string.Format("SELECT COUNT(1) FROM ActivityLogs WHERE ActivityTime < '{0}'",
                                    DateTime.Now.AddDays(-1));

                    var countCommand = new CodingHorror(sqlCount);
                    var count = countCommand.ExecuteScalar<int>();
                    while( 0 < count )
                    {
                        string sqlString = string.Format("DELETE TOP ( 100 )  FROM ActivityLogs WHERE ActivityTime < '{0}'",
                                                     DateTime.Now.AddDays(-1));
                        var command = new CodingHorror(sqlString);
                        command.Execute();

                        count = countCommand.ExecuteScalar<int>();
                        Thread.Sleep( 50 );
                    }
                    
                    Thread.Sleep(60000);
                }
                catch (Exception err)
                {
                    Log.WriteException( "CleanupActivityLog", err);
                    Thread.Sleep(1000);
                }
            }
        }


        protected void LoadAllNozzles()
        {
            Log.Time("LoadAllNozzles", LogType.Debug, false, () =>
            {
                var items = DataManagerFactory.Get().Manage<IStandardNozzle>().GetAll();

                foreach (var item in items)
                {
                    m_Nozzles[item.StandardNozzleID] = item;
                }
            });
        }


        public override void Stop( )
        {
            m_ActivityCleanup.Wait(500);

            State = DeviceState.Stopped;
            Config.State = DeviceState.Stopped;
        }

        public override string Consume( IMessage _message )
        {
            string rc = null;
            m_Mutex.WaitOne( );

            try
            {
                if( _message is IPour || _message is IdentecPour || _message is TrishPour )
                {
                    rc = ConsumePour( _message );
                }
                else if( _message is PhaseMessage )
                {
                    rc = ConsumePhase( _message );
                }
                else if( _message is TagAlertMessage )
                {
                    rc = ConsumeTagAlert( _message );
                }
                else if( _message is DeviceAlertMessage )
                {
                    rc = ConsumeDeviceAlert( _message );
                }
                else if( _message is TagMove )
                {
                    rc = ConsumeMove( _message );
                }
                else if( _message is Jaxis.MessageLibrary.POS.ITicket )
                {
                    rc = ConsumeTicket( _message as Jaxis.MessageLibrary.POS.ITicket );
                }
            }
            catch( Exception exp )
            {
                Log.WriteException( "DataConsumer :: DataConsumer::Consume", exp );
            }
            finally
            {
                m_Mutex.ReleaseMutex();
            }
            return rc;
        }

        private string ConsumeMove( IMessage _message )
        {
            string rc = string.Empty;
            return rc;
        }

        private string ConsumePhase( IMessage _message )
        {
            string rc = null;

            try
            {
                var msg = _message as PhaseMessage;
                if( null != msg )
                {
                    var device = GetDevice( msg.DeviceID );

                    switch( msg.EventType )
                    {
                        case TagPhaseType.Heartbeat:
                        case TagPhaseType.HeartbeatDetached:
                        {
                            m_HeartbeatMessages.Add( new HeartbeatMessage{ Device = device, Phase = msg}  );
                            //ITag tag = GetTag(msg.TagID, false);
                            //ProcessHeartbeat(msg, device, tag);

                            break;
                        }
                        case TagPhaseType.Disconnect:
                        {
                            ITag tag = GetTag(msg.TagID, device.Name, true);
                            ProcessDisconnect(msg, device, tag);
                            break;
                        }
                        case TagPhaseType.Connect:
                        case TagPhaseType.Dormant:
                        {
                            ITag tag = GetTag(msg.TagID, device.Name, true);
                            PushTagActivity(msg, device, tag);
                            if( msg.UpsideDown )
                            {
                                PushTagAlert(msg, device, tag);
                            }
                            break;
                        }
                        case TagPhaseType.MissedMsg:
                        case TagPhaseType.BadBottleAttach:
                        case TagPhaseType.MissedPour:
                        {
                            ITag tag = GetTag(msg.TagID, device.Name, true);
                            PushTagAlert(msg, device, tag);
                            break;
                        }
                    }
                }
            }
            catch( Exception exp )
            {
                Log.WriteException( "DataConsumer::ConsumePhase", exp );
            }

            return rc;
        }

        //private void ProcessHeartbeat(PhaseMessage _msg, IDevice device, ITag tag )
        //{
        //    var man = DataManagerFactory.Get().Manage<IActivityLog>();
        //    IActivityLog log = man.Create( );
        //    log.ReadTime = _msg.ReadTime;
        //    log.ActivityTime = _msg.ReadTime;
        //    log.ActivityType = (int)_msg.EventType;
        //    log.DeviceID = device.DeviceID;
        //    log.LocationID = tag.LocationID;
        //    log.SignalStrength = _msg.SignalStrength;
        //    log.TagID = tag.TagID;

        //    man.Save( log );
            
        //    ProduceMessage(log);
        //}

        private void ProcessDisconnect(PhaseMessage _msg, IDevice _device, ITag _tag )
        {
            PushTagActivity(_msg, _device, _tag);

            IBLInventory Inv = BLManagerFactory.Get().ManageInventory().GetInventoryByTag(_tag.TagID);
            if (null != Inv)
            {
                if (m_EmptyAmount != 0 && m_EmptyAmount > Inv.Amount ) 
                {
                    BLManagerFactory.Get().ManageTags().UnBrand(_tag as IBLTag, ExitReasons.Dispensed);
                }
                if ( Inv.Amount > 0 ) 
                {
                    _msg.EventType = TagPhaseType.NonEmptyBottle;
                    PushTagAlert(_msg, _device, _tag);
                }
            }
        }

        private void PushTagAlert(PhaseMessage _msg, IDevice _device, ITag _tag )
        {
            try
            {
                var man = DataManagerFactory.Get().Manage<ITagAlert>();
                ITagAlert alert = null;
                if( _msg.UpsideDown )
                {
                    
                }
                else
                {
                    alert = man.Create();
                    alert.DeviceID = _device.DeviceID;
                    alert.LocationID = _tag.LocationID;
                    alert.AlertTime = _msg.ReadTime;
                    alert.AlertType = (int)_msg.EventType;
                    alert.TagID = _tag.TagID;
                    alert.Severity = 1;
                    if (_msg.EventType == TagPhaseType.NonEmptyBottle)
                    {
                        var inv = BLManagerFactory.Get().ManageInventory().GetInventoryByTag(_tag.TagID);
                        if (null != inv)
                        {
                            string name = "Unknown";
                            var loc = GetLocation(_tag.LocationID);
                            if (null != loc)
                            {
                                name = GetLocation(_tag.LocationID).Name;
                            }
                            alert.Message = string.Format("Tag {0} on {2} at {3} was removed with {1} ml remaining.", _tag.TagNumber, Math.Round(inv.Amount, 2), inv.UPC.Name, name ); // _tag.Quantity);
                        }

                        var message = new TagAlertMessage
                        {
                            AlertClass = AlertClasses.Tags,
                            Driver = Config.Name,
                            DeviceID = _device.HardwareID,
                            ReadTime = _msg.ReadTime,
                            TagID = _tag.TagNumber,
                            BatteryVoltage = _msg.BatteryVoltage
                        };
                        SendAlert(message, AlertTypes.NonEmptyBottle, alert.Message, true);
                    }
                }
                if( null != alert )
                {
                    man.Save( alert );
                    ProduceMessage(alert);
                }
            }
            catch (Exception err)
            {
                Log.Exception( err );
            }
        }

        private void PushTagActivity(PhaseMessage _msg, IDevice _device, ITag _tag )
        {
            var man = DataManagerFactory.Get().Manage<ITagActivity>();
            var activity = man.Create();
            activity.ActivityTime = _msg.ReadTime;
            activity.ActivityType = (int) _msg.EventType;
            activity.DeviceID = _device.DeviceID;
            activity.LocationID = _tag.LocationID;
            activity.SignalStrength = _msg.SignalStrength;
            activity.TagID = _tag.TagID;
            man.Save( activity );
            ProduceMessage(activity);
        }

        private string ConsumePour( IMessage _message )
        {
            string rc = null;
            try
            {
                //Task.Factory.StartNew(() =>
                {
                    IPour pour = null;
                    if (_message is IdentecPour)
                    {
                        pour = ProcessIdentecPour(_message);
                    }
                    else if (_message is TrishPour)
                    {
                        pour = ProcessTrishPour(_message);
                    }
                    else if (_message is Pour)
                    {
                        pour = _message as IPour;
                        PushPour(pour, null);
                    }
                }//);
            }
            catch( Exception exp )
            {
                Log.WriteException( "DataConsumer::ConsumePour", exp );
            }
            return rc;
        }

        private IPour ProcessIdentecPour( IMessage _message )
        {
            IPour rc = null;
            Log.Time("ProcessIdentecPour", LogType.Debug, false, () =>
            {
                var tagMan = DataManagerFactory.Get().Manage<ITag>();
                var man = DataManagerFactory.Get().Manage<Jaxis.Inventory.Data.IPour>();
                rc = man.Create();
                rc.Status = 2;

                var msg = _message as IdentecPour;
                if( null != msg )
                {
                    var device = GetDevice( msg.DeviceID );
                    var tag = GetTag(msg.TagID, device.Name, true);

                    Log.Debug( string.Format( "DataConsumer :: Pour - TagID: {0}  Device: {1}  Event Type:{2}", msg.TagID, msg.DeviceID, msg.PourCount ) );

                    rc.RawData = Convert.ToBase64String( msg.RawData );
                    rc.TagID = tag.TagID;
                    rc.DeviceID = device.DeviceID;
                    rc.BatteryVoltage = msg.BatteryVoltage;
                    rc.Temperature = msg.Temperature;
                    rc.HardwareID = msg.DeviceID;
                    rc.PourTime = msg.ReadTime;

                    foreach( var a in msg.Angles )
                    {
                        rc.Duration += a.Duration.Ticks;
                    }

                    var location = GetLocation( tag.LocationID );
                    var upc = m_UnknownUPC;
                    var inv = BLManagerFactory.Get().ManageInventory().GetInventoryByTag(tag.TagID);
                    if (null != inv)
                    {
                        location = GetLocation(inv.LocationID);
                        upc = inv.UPC;
                    }
                    else
                    {
                        
                    }
                    //var upc = Log.Time<IUPCItem>( "GetUPC", LogType.Debug, false, ( ) => DataManagerFactory.Get().Manage<Jaxis.Inventory.Data.IUPCItem>().Get( (null != tag.CurrentInventory ) ? tag.CurrentInventory.UPCID : Guid.Empty) );
                
                    if( null != upc )
                    {
                        rc.LocationID = location.LocationID;
                        rc.UPCID = upc.UPCID;
                        //Log.Time("New Calc", LogType.Debug, false,
                        //        () => { rc.Volume = CalculatePour2(msg, tag, upc, inv, upc); });
                        rc.Volume = CalculatePour2(msg, tag, upc, inv, upc);

                        if (null != inv)
                        {
                            rc.AmountLeft = inv.Amount - rc.Volume;
                        }

                        // Support for multiple pours in one pass - FOR LIQUOR ONLY
                        if (upc.RootCategoryID == m_Liquor.CategoryID)
                        {
                            ProcessLiquorPours(msg, rc, tag, inv, upc, location);
                        }
                        else
                        {
                            ProcessSinglePour(msg, rc, tag, inv, upc, location);
                        }

                        if (0 < rc.Volume && null != inv)
                        {
                            inv.Amount = (0 < inv.Amount - rc.Volume)
                                            ? inv.Amount - rc.Volume
                                            : 0;
                            inv.Amount = rc.AmountLeft;
                            BLManagerFactory.Get().ManageInventory().Save(inv);
                            //tagMan.Save(tag);
                        }


                        //TODO: Add alert logic for unknown pours?
                        //if (upc.ObjectID == Guid.Empty)
                        //{
                        //    ITagAlert Alert = DataManagerFactory.Get().Manage<ITagAlert>().Create();
                        //    Alert.DeviceID = device.DeviceID;
                        //    Alert.LocationID = location.LocationID;
                        //    Alert.AlertTime = msg.ReadTime;
                        //    Alert.AlertType = 1;
                        //    Alert.TagID = tag.TagID;
                        //    Alert.Severity = 1;
                        //    Alert.Message = string.Format("Unknows UPC for tag {0} poured {1}.", tag.TagNumber, tag.Quantity.Value);
                        //    Alert.Save();
                        //    ProcessWCFCommand(Alert as TagAlert, (client, msg) => client.PushTagAlert(msg.GetInternalData()));
                        //}
                    }
                }
            });
            return rc;
        }

        private void ProcessLiquorPours(IdentecPour msg, IPour rc, ITag tag, IBLInventory inv, IUPCItem upc, ILocation location)
        {
            if (rc.Volume > (1.5*m_LiquorStandardPour.PourStandard) && m_AllowMultiPour)
            {
                ProcessMultiPour(msg, rc, tag, inv, upc, location);
            }
                // Support half pours for specific typs and locations. 
            else if (rc.Volume < (.7*m_LiquorStandardPour.PourStandard) &&
                     true == location.AllowHalfPour &&
                     true == inv.UPC.AllowHalfPour)
            {
                ProcessHalfPour(msg, rc, tag, inv, upc, location);
            }
            else
            {
                ProcessSinglePour(msg, rc, tag, inv, upc, location);
            }
        }

        private void ProcessHalfPour(IdentecPour _msg, IPour _pour, ITag _tag, IBLInventory _inv, IUPCItem _upc, ILocation _location)
        {
            double volume = m_LiquorStandardPour.PourStandard/2;
            _pour.Volume = volume;
            // _Tag.Quantity MLF moved to Inventory 
            double amount = 0;
            if (null != _inv)
            {
                amount = _inv.Amount;
            }

            var roots = BLManagerFactory.Get().ManageCategories().GetRootCategories().FirstOrDefault(r => r.CategoryID == _upc.RootCategoryID);

            var pour = new CalcPour
            {
                BatteryVoltage = _pour.BatteryVoltage,
                DeviceID = _msg.DeviceID,
                Driver = _msg.Driver,
                OriginalType = OriginalMessageType.Identec,
                PourAmount = _pour.Volume,
                PourCount = _msg.PourCount,
                PourDuration = _msg.ReadTime.Ticks,
                RawData = Convert.ToBase64String(_msg.RawData),
                ReadTime = _msg.ReadTime,
                TagNumber = _msg.TagID,
                Temperature = _pour.Temperature,
                TotalPoured = _upc.Size - amount, // _tag.Quantity.Value,
                Location = _location.Name,
                UPCName = _upc.Name,
                Category = (null != roots) ? roots.Name : "Unknown"
            };
            PushPour(_pour, pour);

        }

        private void ProcessMultiPour(IdentecPour msg, IPour rc, ITag tag, IBLInventory _inv, IUPCItem _upc, ILocation location)
        {   
            int shotCount = (int)(rc.Volume / m_LiquorStandardPour.PourStandard);
            double over = rc.Volume - ((double)shotCount * m_LiquorStandardPour.PourStandard );
            if (over > m_LiquorStandardPour.PourStandard / 2.0)
            {
                ++shotCount;
            }
            double newShotSize = rc.Volume/shotCount;

            // _Tag.Quantity MLF moved to Inventory 
            double amount = 0;
            if (null != _inv)
            {
                amount = _inv.Amount;
            }

            for (int i = 0; i < shotCount; ++i)
            {
                var partialPour = rc.Clone() as IPour;
                double duration = (rc.Duration/TimeSpan.TicksPerSecond)/shotCount;
                partialPour.PourTime = partialPour.PourTime.AddSeconds(duration * i);
                partialPour.PourID = Guid.NewGuid();
                partialPour.Volume = newShotSize;

                var roots = BLManagerFactory.Get().ManageCategories().GetRootCategories().FirstOrDefault(r => r.CategoryID == _upc.RootCategoryID);

                // For TrishEmulator Plugin
                var pour = new CalcPour
                {
                    BatteryVoltage = partialPour.BatteryVoltage,
                    DeviceID = msg.DeviceID,
                    Driver = msg.Driver,
                    OriginalType = OriginalMessageType.Identec,
                    PourAmount = partialPour.Volume,
                    PourCount = msg.PourCount,
                    PourDuration = msg.ReadTime.Ticks,
                    RawData = Convert.ToBase64String(msg.RawData),
                    ReadTime = msg.ReadTime,
                    TagNumber = msg.TagID,
                    Temperature = partialPour.Temperature,
                    TotalPoured = _upc.Size - amount,
                    Location = location.Name,
                    UPCName = _upc.Name,
                    Category = (null != roots) ? roots.Name : "Unknown"
                };
                PushPour(partialPour, pour);
            }
        }

        private void ProcessSinglePour(IdentecPour _msg, IPour _pour, ITag _tag, IBLInventory _inv, IUPCItem _upc, ILocation _location)
        {
            // _Tag.Quantity MLF moved to Inventory 
            double amount = 0;
            if (null != _inv)
            {
                amount = _inv.Amount;
            }
            var roots = BLManagerFactory.Get().ManageCategories().GetRootCategories().FirstOrDefault(r => r.CategoryID == _upc.RootCategoryID);
            // For TrishEmulator Plugin
            var Pour = new CalcPour
            {
                BatteryVoltage = _pour.BatteryVoltage,
                DeviceID = _msg.DeviceID,
                Driver = _msg.Driver,
                OriginalType = OriginalMessageType.Identec,
                PourAmount = _pour.Volume,
                PourCount = _msg.PourCount,
                PourDuration = _msg.ReadTime.Ticks,
                RawData = Convert.ToBase64String(_msg.RawData),
                ReadTime = _msg.ReadTime,
                TagNumber = _msg.TagID,
                Temperature = _pour.Temperature,
                TotalPoured = _upc.Size - amount,
                Location = _location.Name,
                UPCName = _upc.Name,
                Category = ( null != roots) ? roots.Name : "Unknown"
            };
            PushPour(_pour, Pour);
        }

        private IPour ProcessTrishPour( IMessage _message )
        {
            var rc = DataManagerFactory.Get( ).Manage<Jaxis.Inventory.Data.IPour>( ).Create( );
            rc.Status = 2;
            var tagMan = DataManagerFactory.Get().Manage<ITag>();
            var msg = _message as TrishPour;
            if( null != msg )
            {
                ITag tag = GetTag( msg.PLUNumber.ToString( ), "", true );
                IDevice device = GetDevice( msg.DeviceID );
                // _Tag.Quantity MLF moved to Inventory 
                double Amount = 0;

                Log.Debug( string.Format( "DataConsumer :: Pour - TagID: {0}  Device: {1}  Event Type:{2}", msg.PLUNumber, msg.ServingBar, msg.Sequence ) );
                rc.RawData = Convert.ToString( msg.RawData );
                
                rc.TagID = tag.TagID;
                rc.DeviceID = device.DeviceID;
                rc.UPCID = (null != tag.CurrentInventory) ? tag.CurrentInventory.UPCID : Guid.Empty;
                rc.Temperature = msg.Temperature;
                rc.Duration = ( DateTime.Now - msg.ReadTime ).Ticks;
//                rc.PourCount = Msg.Sequence;
                rc.Volume = msg.PouredLiters;
                rc.PourTime = msg.ReadTime;

                ILocation location = GetLocation( tag.LocationID );
                rc.LocationID = location.LocationID;

                var inv = BLManagerFactory.Get().ManageInventory().GetInventoryByTag(tag.TagID);
                if (null != inv)
                {
                    Amount = inv.Amount;
                }

                if (0 < rc.Volume)
                {
                    Amount = (0 < Amount - rc.Volume) ? Amount - rc.Volume : 0;
                    if (null != inv)
                    {
                        inv.Amount = Amount;
                        BLManagerFactory.Get().ManageInventory().Save(inv);
                    }
                }

                // For TrishEmulator Plugin
                var Pour = new CalcPour
                {
                    BatteryVoltage = 0,
                    DeviceID = msg.DeviceID,
                    Driver = msg.Driver,
                    OriginalType = OriginalMessageType.Trish,
                    PourAmount = msg.PouredLiters,
                    PourCount = msg.Sequence,
                    PourDuration = msg.ReadTime.Ticks,
                    RawData = Convert.ToString(msg.RawData),
                    ReadTime = msg.ReadTime,
                    TagNumber = tag.TagNumber,
                    Temperature = msg.Temperature,
                    TotalPoured = msg.TotalLiters,
                    Location = location.Name,
                    UPCName = (null != tag.CurrentInventory) ? inv.UPC.Name : "Unknown",
                    Category = "Beer"
                };
                PushPour(rc, Pour);
            }
            return rc;
        }

        private void PushPour( IPour _pour, CalcPour _CalcPour )
        {
            try
            {
                Log.Wrap<bool>( "DataConsumer::Push Pour", LogType.Debug, true, ( ) =>
                {
                    // MLF added to support TrishEmulator
                    if( null != _CalcPour )
                    {
                        ProduceMessage( _CalcPour );
                    }
                    
                    DataManagerFactory.Get().Manage<IPour>().Save(_pour);
                    ProduceMessage(_pour as Pour);
                    //ProcessWCFCommand( _pour as Pour, ( client, pour ) => client.PushPourData( pour.GetInternalData()));
                    return true;
                } );
            }
            catch( Exception exp )
            {
                Log.WriteException( "DataConsumer::ConsumePour", exp );
            }
        }

        private string ConsumeTagAlert( IMessage _Message )
        {
            bool bMsgSent = false;
            string rc = null;
            var man = DataManagerFactory.Get().Manage<ITagAlert>();
            var phase = man.Create();

            try
            {
                var msg = _Message as TagAlertMessage;
                if( null != msg )
                {
                    var device = GetDevice( msg.DeviceID );
                    var tag = GetTag(msg.TagID, device.Name, true);

                    phase.TagID = tag.TagID;
                    phase.AlertTime = msg.ReadTime;
                    phase.Message = msg.AlertType.ToString( );

                    if( AlertTypes.MissedPour == msg.AlertType )
                    {
                        phase.AlertType = (int)TagPhaseType.MissedPour;
                    }
                    else if( AlertTypes.MissedMsg == msg.AlertType )
                    {
                        phase.AlertType = (int)TagPhaseType.MissedMsg;
                    }
                    else if( AlertTypes.BadBottleAttach == msg.AlertType )
                    {
                        phase.AlertType = (int)TagPhaseType.BadBottleAttach;
                    }
                    else if (AlertTypes.NonEmptyBottle == msg.AlertType)
                    {
                        phase.AlertType = (int)TagPhaseType.NonEmptyBottle;
                    }
                    phase.DeviceID = device.DeviceID;
                    //phase.BatteryVoltage = msg.BatteryVoltage;
                    man.Save( phase );
                    ProduceMessage(phase);
                    //ProcessWCFCommand( Phase as TagAlert, ( client, pour ) => client.PushTagAlert( pour.GetInternalData( ) ) );
                }
                bMsgSent = true;
//                PushErrorTagAlerts( );
            }
            catch( Exception exp )
            {
                Log.WriteException( "DataConsumer::ConsumeTagAlert", exp );
                //TODO: Fix this
                if( false == bMsgSent )
                    man.Save(phase);

                //    m_ErrorTagAlerts.Add( Phase );
            }

            return rc;
        }

        //private ITag ProcessMove(TagAlertMessage msg, out IDevice device)
        //{
        //    device = GetDevice( msg.DeviceID );
        //    return GetTag( msg.TagID );
        //}

        private string ConsumeTicket( Jaxis.MessageLibrary.POS.ITicket _Message )
        {
            string rc = string.Empty;
            bool bMsgSent = false;
            try
            {
                bMsgSent = Log.Wrap<bool>( "DataConsumer::ConsumeTicket()", LogType.Debug, true, ( ) =>
                {
                    // TODO:  Fix this.
                    //Log.Debug( string.Format( "TicketID {0} Establishment {1}", _message.CheckNumber, _message.Establishment ) );
                    //Log.Debug( string.Format( "{0}{1}", System.Environment.NewLine, _message.ToString( ) ) );
                    //m_Client.PushTicket( _message );
                    return true;
                } );
            }
            catch( Exception exp )
            {
                Log.WriteException( "DataConsumer::ConsumeTicket", exp );
            }
            return rc;
        }

        private string ConsumeDeviceAlert( IMessage _Message )
        {
            string rc = string.Empty;
            bool bMsgSent = false;
            DeviceAlert Alert = new DeviceAlert( );

            try
            {
                DeviceAlertMessage Msg = _Message as DeviceAlertMessage;
                if( null != Msg )
                {
                    IDevice device = GetDevice( Msg.DeviceID );
                    Alert.Message = Msg.AlertType.ToString();
                    Alert.Driver = Msg.Driver;
                    Alert.DeviceID = device.DeviceID;
                    Alert.AlertTime = Msg.ReadTime;
                    if( AlertTypes.CannotConnect == Msg.AlertType )
                    {
                        Alert.AlertType = (int)DeviceAlertType.CannotConnect;
                    }
                    else if( AlertTypes.NotReadingTags == Msg.AlertType )
                    {
                        Alert.AlertType = (int)DeviceAlertType.NotReadingTags;
                    }
                }
                Alert.Save();
                ProduceMessage(Alert);
                //ProcessWCFCommand( Alert, ( _client, alert ) => _client.PushDeviceAlert( alert.GetInternalData( ) ));
                bMsgSent = true;
            }
            catch( Exception exp )
            {
                Log.WriteException( "DataConsumer::ConsumeDeviceAlert", exp );
                if( false == bMsgSent )
                    Alert.Save( );
            }
            return rc;
        }

        private IStandardNozzle GetTagNozzle( ITag _tag )
        {
            IStandardNozzle nozzle = null;
            if( _tag.StandardNozzleID.HasValue && m_Nozzles.ContainsKey( _tag.StandardNozzleID.Value ))
            {
                nozzle = m_Nozzles[_tag.StandardNozzleID.Value];
            }
            else if (_tag.StandardNozzleID.HasValue)//if( null == nozzle )
            {
                nozzle = DataManagerFactory.Get().Manage<IStandardNozzle>().Get(_tag.StandardNozzleID.Value);
            }
            else
            {
                nozzle = m_StandardNozzle;
            }
            return nozzle;
        }

        private double CalculatePour2(IdentecPour _msg, ITag _tag, IUPCItem _upcItem, IBLInventory _inventory, IUPCItem _upc )
        {
            double rc = 0;

            try
            {
                double nozzleArea = 0.0;
                //Log.Time("Nozzle Area", LogType.Debug, true, () =>
                {
                    IStandardNozzle nozzle = GetTagNozzle(_tag);

                    /// HACK: using the old formula for now.
                    nozzleArea = (nozzle.Width + nozzle.Length) / 2.0;
                }//);

                double amount = _upc.Size;
                if (null != _inventory)
                {
                    amount = _inventory.Amount;
                }

                // A = amt in bottle at start of pour in ml
                // B = bottle size in ml
                // C = nozzle or bottle mouth size in mm
                // D - O is the pour time in sec for each of the 12 angle buckets defined by Identec

                //Log.Debug(string.Format("Tag Number: {3}, Bottle Amount {0} - Bottle Amount {1} - Nozzle Area {2}", Amount, _upcItem.Size, nozzleArea, _tag.TagNumber));
                //Log.Time("Get Calculate Pour", LogType.Debug, true, () =>
                {

                    double A = amount;
                    double B = _upcItem.Size;
                    double C = nozzleArea;
                    double D =_msg.Angles[11].Duration.TotalSeconds;
                    double E =_msg.Angles[10].Duration.TotalSeconds;
                    double F =_msg.Angles[9].Duration.TotalSeconds;
                    double G =_msg.Angles[8].Duration.TotalSeconds;
                    double H =_msg.Angles[7].Duration.TotalSeconds;
                    double I =_msg.Angles[6].Duration.TotalSeconds;
                    double J =_msg.Angles[5].Duration.TotalSeconds;
                    double K =_msg.Angles[4].Duration.TotalSeconds;
                    double L =_msg.Angles[3].Duration.TotalSeconds;
                    double M =_msg.Angles[2].Duration.TotalSeconds;
                    double N =_msg.Angles[1].Duration.TotalSeconds;
                    double O =_msg.Angles[0].Duration.TotalSeconds;
                    double P = (0 == _upcItem.PourModifier)
                                    ? 1
                                    : _upcItem.PourModifier;

                    //// Nozzle Formula
                    //string nozzelFormula =
                    //    "((O*1451466.817)+(N*1162207.205)+(M*1119829.205)*(L*1028268.259)+(K*716354.07)+(J*787097.8597)+(I*636230.4361)+(H*672757.142)+(G*784567.6121)-(F*594123.2325)+(E*77643.71237)+(D*1026594.791)+(A*0.009509382)-(Q*45.36465089)+(C*3.111171619)-(B*0.009348915)+1.753467275)*P";
                    //var newValue = j.Calculate(nozzelFormula);
                    // Free Formula
                    // (T12*13803757.16)+(T11*8310923.107)-(T10*336085.8639)-(T9*3164219.029)+(T8*14782687.31)+(T7*4273612.29)+(T6*2549350.68)+(T5*2681873.793)+(T4*2241746.62)+(T3*1442089.714)-(T2*2736392.779)-(T1*803027.9229)+(Amt Remaining*0.073340262)+(Mouth Area*0.77218773)-(Bottle Size*0.045350457)-139.6599175

                    //Formula FreePour=(0.011431196*B)-(0.003585101*A)+(29.39723249*C)+(88.09234641*D)+(109.6501991*E)-(2.725176882*F)-(24.3226407*G)+(65.30928584*H)+(68.0125991*I)+(50.76890921*J)+(45.64152012*K)+(34.67409344*L)+(30.24024307*M)+(17.38881007*N)+(6.315538232*O)-526.6571936
                    //NozzlePour=(0.004641785*A)+(0.003056625*B)+(3.068437631*C)+(17.71355135*D)+(10.51479802*E)+(12.86220136*F)+(12.50647827*G)+(10.17918832*H)+(12.58714105*I)+(9.447935279*J)+(10.22676952*K)-(2.229548568*L)-(7.437064065*M)-(5.651333081*N)-(4.344187211*O)-(8.961424576)

                    //string Formula = "((0.004641785*A)+(0.003056625*B)+(3.068437631*C)+(17.71355135*D)+(10.51479802*E)+(12.86220136*F)+(12.50647827*G)+(10.17918832*H)+(12.58714105*I)+(9.447935279*J)+(10.22676952*K)-(2.229548568*L)-(7.437064065*M)-(5.651333081*N)-(4.344187211*O)-(8.961424576))*P";
                    if (m_MaxSpoutDiameter < nozzleArea)
                    {
                        //Formula FreePour
                        rc = ((0.011431196*B) - (0.003585101*A) +
                                (29.39723249*C) + (88.09234641*D) +
                                (109.6501991*E) -
                                (2.725176882*F) - (24.3226407*G) +
                                (65.30928584*H) + (68.0125991*I) +
                                (50.76890921*J) +
                                (45.64152012*K) + (34.67409344*L) +
                                (30.24024307*M) + (17.38881007*N) +
                                (6.315538232*O) -
                                526.6571936)*P;
                    }
                    else
                    {
                        rc = ((0.004641785*A) + (0.003056625*B) +
                                (3.068437631*C) + (17.71355135*D) +
                                (10.51479802*E) +
                                (12.86220136*F) + (12.50647827*G) +
                                (10.17918832*H) + (12.58714105*I) +
                                (9.447935279*J) +
                                (10.22676952*K) - (2.229548568*L) -
                                (7.437064065*M) - (5.651333081*N) -
                                (4.344187211*O) -
                                (8.961424576))*P;
                    }
                    if (rc < 0)
                    {
                        long duration = _msg.Angles.Sum(slot => slot.Duration.Ticks);
                        Log.Debug(string.Format("Pour went negative {2} on Tag {0} of {1} - PourTime: {3}", _tag.TagNumber, rc, _upcItem.Name, duration));
                        // calculation went negative...we assume its a very small pour..like a splash. 
                        // this value is .25 oz...in ml;
                        rc = 7.0;
                    }
                }//);
                Log.Debug( string.Format( "Pour from {2} Tag {0} of {1}", _tag.TagNumber, rc, _upcItem.Name) );
                //Log.Debug(string.Format("DataConsumer :: T {4} D {5} Amount {0} Pour {6} Size {1} Nozzle Diameter {2} Angle Count {3}", Amount, _upcItem.Size, _tag.Nozzle.CalculateNozzleArea(), _msg.Angles.Count, _msg.TagID, _msg.DeviceID, rc));
            }
            catch (Exception exp)
            {
                Log.WriteException("DataConsumer::CalculatePour", exp);
            }
            //Log.Time("Modify Pour Calc", LogType.Debug, true, () =>
            {
                rc = ModifyPourCalculation(_msg, _tag, _upcItem, rc);
            }//);

            return rc;
        }


        private double CalculatePour( IdentecPour _msg, ITag _tag, IUPCItem _upcItem )
        {
            double rc = 0;

            try
            {
                double nozzleArea = 0.0;
                if( null != _tag.Nozzle )
                {
                    nozzleArea = _tag.Nozzle.CalculateNozzleArea( );

                    /// HACK: using the old formula for now.
                    nozzleArea = (_tag.Nozzle.Width + _tag.Nozzle.Length)/2.0;
                }
                double Amount = 0;
                var inv = BLManagerFactory.Get().ManageInventory().GetInventoryByTag(_tag.TagID);
                if (null != inv)
                {
                    Amount = inv.Amount;
                }

                // A = amt in bottle at start of pour in ml
                // B = bottle size in ml
                // C = nozzle or bottle mouth size in mm
                // D - O is the pour time in sec for each of the 12 angle buckets defined by Identec

                Log.Debug(string.Format("Tag Number: {3}, Bottle Amount {0} - Bottle Amount {1} - Nozzle Area {2}", Amount, _upcItem.Size, nozzleArea, _tag.TagNumber));

                var P = new Parser.MathParser( );
                P.m_Parameters.Add(Parser.Parameters.A, Convert.ToDecimal(Amount));
                P.m_Parameters.Add( Parser.Parameters.B, _upcItem.Size );
                P.m_Parameters.Add( Parser.Parameters.C, Convert.ToDecimal( nozzleArea ) );
                P.m_Parameters.Add( Parser.Parameters.D, Convert.ToDecimal( _msg.Angles[11].Duration.TotalSeconds ) );
                P.m_Parameters.Add( Parser.Parameters.E, Convert.ToDecimal( _msg.Angles[10].Duration.TotalSeconds ) );
                P.m_Parameters.Add( Parser.Parameters.F, Convert.ToDecimal( _msg.Angles[9].Duration.TotalSeconds ) );
                P.m_Parameters.Add( Parser.Parameters.G, Convert.ToDecimal( _msg.Angles[8].Duration.TotalSeconds ) );
                P.m_Parameters.Add( Parser.Parameters.H, Convert.ToDecimal( _msg.Angles[7].Duration.TotalSeconds ) );
                P.m_Parameters.Add( Parser.Parameters.I, Convert.ToDecimal( _msg.Angles[6].Duration.TotalSeconds ) );
                P.m_Parameters.Add( Parser.Parameters.J, Convert.ToDecimal( _msg.Angles[5].Duration.TotalSeconds ) );
                P.m_Parameters.Add( Parser.Parameters.K, Convert.ToDecimal( _msg.Angles[4].Duration.TotalSeconds ) );
                P.m_Parameters.Add( Parser.Parameters.L, Convert.ToDecimal( _msg.Angles[3].Duration.TotalSeconds ) );
                P.m_Parameters.Add( Parser.Parameters.M, Convert.ToDecimal( _msg.Angles[2].Duration.TotalSeconds ) );
                P.m_Parameters.Add( Parser.Parameters.N, Convert.ToDecimal( _msg.Angles[1].Duration.TotalSeconds ) );
                P.m_Parameters.Add( Parser.Parameters.O, Convert.ToDecimal( _msg.Angles[0].Duration.TotalSeconds ) );
                P.m_Parameters.Add(Parser.Parameters.P, Convert.ToDecimal((0 == _upcItem.PourModifier) ? 1 : _upcItem.PourModifier));


                //var j = new Parser.MathParser();
                //j.m_Parameters.Add(Parser.Parameters.A, Convert.ToDecimal(_tag.Quantity.Value));
                //j.m_Parameters.Add(Parser.Parameters.B, _upcItem.Size);
                //j.m_Parameters.Add(Parser.Parameters.C, Convert.ToDecimal(_tag.Nozzle.CalculateNozzleArea()));
                //j.m_Parameters.Add(Parser.Parameters.D, Convert.ToDecimal(_msg.Angles[0].Duration.TotalSeconds));
                //j.m_Parameters.Add(Parser.Parameters.E, Convert.ToDecimal(_msg.Angles[1].Duration.TotalSeconds));
                //j.m_Parameters.Add(Parser.Parameters.F, Convert.ToDecimal(_msg.Angles[2].Duration.TotalSeconds));
                //j.m_Parameters.Add(Parser.Parameters.G, Convert.ToDecimal(_msg.Angles[3].Duration.TotalSeconds));
                //j.m_Parameters.Add(Parser.Parameters.H, Convert.ToDecimal(_msg.Angles[4].Duration.TotalSeconds));
                //j.m_Parameters.Add(Parser.Parameters.I, Convert.ToDecimal(_msg.Angles[5].Duration.TotalSeconds));
                //j.m_Parameters.Add(Parser.Parameters.J, Convert.ToDecimal(_msg.Angles[6].Duration.TotalSeconds));
                //j.m_Parameters.Add(Parser.Parameters.K, Convert.ToDecimal(_msg.Angles[7].Duration.TotalSeconds));
                //j.m_Parameters.Add(Parser.Parameters.L, Convert.ToDecimal(_msg.Angles[8].Duration.TotalSeconds));
                //j.m_Parameters.Add(Parser.Parameters.M, Convert.ToDecimal(_msg.Angles[9].Duration.TotalSeconds));
                //j.m_Parameters.Add(Parser.Parameters.N, Convert.ToDecimal(_msg.Angles[10].Duration.TotalSeconds));
                //j.m_Parameters.Add(Parser.Parameters.O, Convert.ToDecimal(_msg.Angles[11].Duration.TotalSeconds));
                //j.m_Parameters.Add(Parser.Parameters.P, Convert.ToDecimal((0 == _upcItem.PourModifier) ? 1 : _upcItem.PourModifier));
                //j.m_Parameters.Add(Parser.Parameters.Q, Convert.ToDecimal(_tag.Nozzle.Shape));

                //// Nozzle Formula
                //string nozzelFormula =
                //    "((O*1451466.817)+(N*1162207.205)+(M*1119829.205)*(L*1028268.259)+(K*716354.07)+(J*787097.8597)+(I*636230.4361)+(H*672757.142)+(G*784567.6121)-(F*594123.2325)+(E*77643.71237)+(D*1026594.791)+(A*0.009509382)-(Q*45.36465089)+(C*3.111171619)-(B*0.009348915)+1.753467275)*P";
                //var newValue = j.Calculate(nozzelFormula);


                // Free Formula
                // (T12*13803757.16)+(T11*8310923.107)-(T10*336085.8639)-(T9*3164219.029)+(T8*14782687.31)+(T7*4273612.29)+(T6*2549350.68)+(T5*2681873.793)+(T4*2241746.62)+(T3*1442089.714)-(T2*2736392.779)-(T1*803027.9229)+(Amt Remaining*0.073340262)+(Mouth Area*0.77218773)-(Bottle Size*0.045350457)-139.6599175

                //Formula FreePour=(0.011431196*B)-(0.003585101*A)+(29.39723249*C)+(88.09234641*D)+(109.6501991*E)-(2.725176882*F)-(24.3226407*G)+(65.30928584*H)+(68.0125991*I)+(50.76890921*J)+(45.64152012*K)+(34.67409344*L)+(30.24024307*M)+(17.38881007*N)+(6.315538232*O)-526.6571936
                //NozzlePour=(0.004641785*A)+(0.003056625*B)+(3.068437631*C)+(17.71355135*D)+(10.51479802*E)+(12.86220136*F)+(12.50647827*G)+(10.17918832*H)+(12.58714105*I)+(9.447935279*J)+(10.22676952*K)-(2.229548568*L)-(7.437064065*M)-(5.651333081*N)-(4.344187211*O)-(8.961424576)

                string Formula = "((0.004641785*A)+(0.003056625*B)+(3.068437631*C)+(17.71355135*D)+(10.51479802*E)+(12.86220136*F)+(12.50647827*G)+(10.17918832*H)+(12.58714105*I)+(9.447935279*J)+(10.22676952*K)-(2.229548568*L)-(7.437064065*M)-(5.651333081*N)-(4.344187211*O)-(8.961424576))*P";
                if( m_MaxSpoutDiameter < nozzleArea )
                {
                    //Formula FreePour
                    Formula = "((0.011431196*B)-(0.003585101*A)+(29.39723249*C)+(88.09234641*D)+(109.6501991*E)-(2.725176882*F)-(24.3226407*G)+(65.30928584*H)+(68.0125991*I)+(50.76890921*J)+(45.64152012*K)+(34.67409344*L)+(30.24024307*M)+(17.38881007*N)+(6.315538232*O)-526.6571936)*P";
                }
                else
                {
                    Formula = "((0.004641785*A)+(0.003056625*B)+(3.068437631*C)+(17.71355135*D)+(10.51479802*E)+(12.86220136*F)+(12.50647827*G)+(10.17918832*H)+(12.58714105*I)+(9.447935279*J)+(10.22676952*K)-(2.229548568*L)-(7.437064065*M)-(5.651333081*N)-(4.344187211*O)-(8.961424576))*P";
                }
                if( !string.IsNullOrEmpty( Formula ) )
                {
                    Decimal pourAmount = P.Calculate( Formula );
                    rc = Convert.ToDouble(pourAmount, CultureInfo.InvariantCulture);

                    if( rc < 0 )
                    {
                        // calculation went negative...we assume its a very small pour..like a splash. 
                        // this value is .25 oz...in ml;
                        rc = 7.0;
                    }
                    Log.Debug( string.Format( "DataConsumer :: T {4} D {5} Amount {0} Pour {6} Size {1} Nozzle Diameter {2} Angle Count {3}", Amount, _upcItem.Size, _tag.Nozzle.CalculateNozzleArea( ), _msg.Angles.Count, _msg.TagID, _msg.DeviceID, rc ) );
                }
                else
                {
                    Log.Debug( "DataConsumer :: No Formula" );
                }
            }
            catch( Exception exp )
            {
                Log.WriteException( "DataConsumer::CalculatePour", exp );
            }

            rc = ModifyPourCalculation(_msg, _tag, _upcItem, rc);

            return rc;
        }

        private double ModifyPourCalculation(IdentecPour _msg, ITag _tag, IUPCItem _upcItem, double _rc)
        {
            double rc = _rc;

            IStandardNozzle nozzle = GetTagNozzle(_tag);

            // RollerBall Fixed Pour.
            if (NozzleShapes.RollerBall == (NozzleShapes)nozzle.Shape && 0 == (int)nozzle.CalculateNozzleArea())
            {
                rc = m_LiquorStandardPour.PourStandard;
            }
            else // HACK:  This is to deal with Canadian 1oz pours till we can collect more data.
            if (m_LiquorStandardPour.PourStandard < 35 && _msg.Duration < new TimeSpan(0, 0, 0, 3, 500))
            {
                double orig = rc;
                rc = rc * 0.82;
                Log.Debug(string.Format("DataConsumer :: Orig {0}, New {1} - Time {2}", orig, rc, _msg.Duration));
            }
            else // RollerBall partial pour 
            if (NozzleShapes.RollerBall == (NozzleShapes)nozzle.Shape )
            {
                if (rc > m_LiquorStandardPour.PourStandard)
                {
                    rc = m_LiquorStandardPour.PourStandard;
                }
            }
            return rc;
        }


        private void ProcessMove(ITag _tag, IDevice _device)
        {
            //var location = GetLocation(_tag.LocationID);

            //if (location.DeviceID != _device.DeviceID && _device.)
            //{
            //    var newLocation = BLManagerFactory.Get().ManageLocations().GetAll().Where(l => l.DeviceID == _device.DeviceID).FirstOrDefault();
            //    if (null != newLocation)
            //    {
            //        _tag.LocationID = location.LocationID;
            //        _tag.Save();
            //    }
            //}
        }
    }
}