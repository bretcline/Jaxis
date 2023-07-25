using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading;
using BeverageMonitor.Entities;
using Jaxis.BeverageManagement.Plugin.PourDataService;
using Jaxis.Engine.Base.Device;
using Jaxis.Interfaces;
using Jaxis.Inventory.Data;
using Jaxis.Inventory.Data.IBLDataItems;
using Jaxis.Util.Log4Net;
using IDevice = Jaxis.Inventory.Data.IDevice;

namespace Jaxis.BeverageManagement.Plugin
{
    public abstract class BaseBevManDevice : AlertableProducerDevice, IConsumer
    {
        public class TimedElement<T>
        {
            public TimedElement( T _item )
            {
                Element = _item;
                Time = DateTime.Now;
            }

            public T Element{ get; set; }
            public DateTime Time{ get; set;}
            public TimeSpan TimeDiff { get { return DateTime.Now - Time; } }
        }

        protected static Mutex m_Mutex = new Mutex( );

        protected static Dictionary<string, TimedElement<IDevice>> m_Devices = new Dictionary<string, TimedElement<IDevice>>();
        protected static Dictionary<string, TimedElement<ITag>> m_Tags = new Dictionary<string, TimedElement<ITag>>();
        protected static Dictionary<Guid, TimedElement<ILocation>> m_Locations = new Dictionary<Guid, TimedElement<ILocation>>();
        protected static Dictionary<string, TimedElement<IUPCItem>> m_UPCs = new Dictionary<string, TimedElement<IUPCItem>>();
        protected static Dictionary<string, Guid> m_DeviceLocations = new Dictionary<string, Guid>();
        protected string m_ConnectionString;

        protected IStandardNozzle m_StandardNozzle = null;

        private TimeSpan m_CacheTimeout = new TimeSpan( 0, 0, 1, 0 );


        protected Action<ITag> AutoMove{ get; set; }


        #region Entity Framework

        protected BeverageMonitorEntities m_Entities = null;

        #endregion 

        public BaseBevManDevice( IDeviceConfig _Config )
            : base( _Config )
        {
            ( _Config as DeviceConfig ).AssemblyVersion = Assembly.GetExecutingAssembly( ).GetName( ).Version.ToString( );
            Config.ConsumerMessageType = Config.ConsumerMessageType;//MessageType.RawData;
            Config.Type = DeviceType.DataConsumer;

            //m_Entities = new BeverageMonitorEntities();
        }

        //protected void Start( )
        //{
        //    m_StandardNozzle = BLManagerFactory.Get().ManageStandardNozzles().Get(Guid.Empty);

        //    LoadAllTags();
        //}

        protected void LoadAllTags()
        {
            Log.Time("LoadAllTags", LogType.Debug, false, () =>
            {
                var tags = DataManagerFactory.Get().Manage<ITag>().GetAll();

                foreach (var tag in tags)
                {
                    m_Tags[tag.TagNumber] = new TimedElement<ITag>(tag);
                }

                m_StandardNozzle = BLManagerFactory.Get().ManageStandardNozzles().Get(Guid.Empty);
            });
        }



        protected void LoadDeviceLocations()
        {
            Log.Time("LoadDeviceLocations", LogType.Debug, false, () =>
            {
                var items = DataManagerFactory.Get().Manage<ILocation>().GetAll();

                foreach (var item in items)
                {
                    if (item.DeviceID.HasValue)
                    {
                        var device = DataManagerFactory.Get().Manage<IDevice>().Get(item.DeviceID.Value);
                        m_DeviceLocations[device.Name] = item.LocationID;
                    }
                }
            });
        }

        //protected IPourEngineService GetClient( )
        //{
        //    return new PourEngineServiceClient( "BeverageManager", Config.GetWCFPath( ) );
        //}

        protected IDevice GetDevice( string _deviceId )
        {
            IDevice rc;
            //Log.Time("GetDevice", LogType.Debug, true, () =>
            {
                try
                {
                    m_Mutex.WaitOne( );
                    {
                        if( !m_Devices.ContainsKey( _deviceId ) || m_Devices[_deviceId].TimeDiff > m_CacheTimeout )
                        {
                            var man = DataManagerFactory.Get().Manage<IDevice>();
                            rc = man.GetAll( ).FirstOrDefault(_d => _d.HardwareID == _deviceId);

                            if( null == rc )
                            {
                                rc = DataManagerFactory.Get( ).Manage<IDevice>( ).Create( );
                                rc.HardwareID = _deviceId;
                                rc.Name = _deviceId;
                                man.Save(rc);
                            }
                            m_Devices[_deviceId] = new TimedElement<IDevice>(rc);
                        }
                        else
                        {
                            rc = m_Devices[_deviceId].Element;
                        }
                    }
                }
                finally
                {
                    m_Mutex.ReleaseMutex( );
                }

            } //);
            return rc;
        }

        protected ITag GetTagForHeartbeat(string _tagId, string _device)
        {
            ITag rc;
            //Log.Time("GetTagForHeartbeat", LogType.Debug, true, () =>
            {
                try
                {
                    m_Mutex.WaitOne();
                    {
                        TimedElement<ITag> tag = null;
                        if (m_Tags.ContainsKey(_tagId))
                        {
                            tag = m_Tags[_tagId];
                        }

                        if (null != tag)
                        {
                            rc = tag.Element;
                        }
                        else
                        {
                            rc = BLManagerFactory.Get().ManageTags().GetTagByTagNumber( _tagId );
                            m_Tags[_tagId] = new TimedElement<ITag>(rc);
                        }
                        if (null == rc)
                        {
                            rc = BLManagerFactory.Get().ManageTags().Create();
                            rc.TagNumber = _tagId;
                            BLManagerFactory.Get().ManageTags().Save(rc as IBLTag);
                            m_Tags[_tagId] = new TimedElement<ITag>(rc);
                        }
                        UpdateTagLocation(rc, _device);
                    }
                }
                finally
                {
                    m_Mutex.ReleaseMutex();
                }
            }//);
            return rc;
        }

        protected ITag GetTag( string _tagId, string _device, bool _refresh )
        {
            ITag rc;
            //Log.Time("GetTag", LogType.Debug, true, () =>
            {
                try
                {
                    m_Mutex.WaitOne();
                    {
                        TimedElement<ITag> tag = null;
                        if (m_Tags.ContainsKey(_tagId))
                        {
                            tag = m_Tags[_tagId];
                        }
                        var man = DataManagerFactory.Get().Manage<ITag>();
                        if (null == tag ||
                            tag.TimeDiff > m_CacheTimeout ||
                            _refresh)
                        {
                            rc = null != tag 
                                ? man.Get(tag.Element.TagID) 
                                : man.GetAll().FirstOrDefault(_t => _t.TagNumber == _tagId);
                            if (null == rc)
                            {
                                rc =
                                    DataManagerFactory.Get().Manage
                                        <ITag>().Create();
                                rc.TagNumber = _tagId;
                                //                            rc.UPCID = Guid.Empty;
                                man.Save(rc);
                            }

                            if (null != tag)
                            {
                                m_Tags[_tagId].Time = DateTime.Now;
                            }
                            else
                            {
                                m_Tags[_tagId] = new TimedElement<ITag>(rc);
                            }
                        }
                        else
                        {
                            rc = m_Tags[_tagId].Element;
                        }

                        if (!rc.StandardNozzleID.HasValue)
                        {
                            rc.Nozzle = m_StandardNozzle;
                        }
                        UpdateTagLocation(rc, _device);
                    }
                }
                finally
                {
                    m_Mutex.ReleaseMutex();
                }
            }//);
            return rc;
        }

        private void UpdateTagLocation(ITag _rc, string _device)
        {
            var locationId = (m_DeviceLocations.ContainsKey(_device)) ? m_DeviceLocations[_device] : _rc.LocationID;
            if (locationId != _rc.LocationID)
            {
                _rc.LocationID = locationId;
                BLManagerFactory.Get().ManageTags().UpdateTagLocations(locationId, _rc.TagID);

                if (null != AutoMove)
                {
                    AutoMove( _rc );
                }

            }
        }

        protected ILocation GetLocation( Guid _locationId )
        {
            ILocation rc;
            //Log.Time("GetLocation", LogType.Debug, false, () =>
            {
                try
                {
                    m_Mutex.WaitOne();
                    {
                        if (!m_Locations.ContainsKey(_locationId) || m_Locations[_locationId].TimeDiff > m_CacheTimeout)
                        {
                            rc = DataManagerFactory.Get().Manage<ILocation>().GetAll().FirstOrDefault(_l => _l.LocationID == _locationId) ??
                                 DataManagerFactory.Get().Manage<ILocation>().Get(Guid.Empty);
                            m_Locations[_locationId] = new TimedElement<ILocation>(rc);
                        }
                        else
                        {
                            rc = m_Locations[_locationId].Element;
                        }
                    }
                }
                finally
                {
                    m_Mutex.ReleaseMutex();
                }
            }//);
            return rc;
        }


        protected IUPCItem GetUpcByTagNumber( ITag _tag )
        {
            IUPCItem rc = null;
            if( !m_UPCs.ContainsKey( _tag.TagNumber ) || m_UPCs[_tag.TagNumber].TimeDiff > m_CacheTimeout )
            {
                //var tag = BLManagerFactory.Get().ManageTags().Get(_tag.TagID);
                //if( null != tag )
                {
                    var inv = BLManagerFactory.Get().ManageInventory().GetInventoryByTag(_tag.TagID);
                    if( null != inv )
                    {
                        m_UPCs[_tag.TagNumber] = new TimedElement<IUPCItem>(inv.UPC);
                    }
                    else
                    {
                        m_UPCs[_tag.TagNumber] = new TimedElement<IUPCItem>(BLManagerFactory.Get().ManageUPCs().Get(Guid.Empty));
                    }
                }
            }
            if (m_UPCs.ContainsKey(_tag.TagNumber))
            {
                rc = m_UPCs[_tag.TagNumber].Element;
            }
            return rc;
        }

    }
}