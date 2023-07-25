using System;
using System.Reflection;
using Jaxis.Inventory.Data.IDataItems;
using Jaxis.Util.Log4Net;
using Jaxis.Engine.Base.Device;
using Jaxis.Interfaces;
using Jaxis.MessageLibrary.POS;
using Jaxis.Inventory.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IDevice = Jaxis.Inventory.Data.IDevice;

namespace Jaxis.BeverageManagement.Plugin.POSConsumption
{
    public class InventoryReduction : BaseBevManDevice, IConsumer
    {
        [Obfuscation(Exclude = true)]
        public static DeviceConfig GetDefaultDeviceConfig()
        {
            var rc = new DeviceConfig();
            var asm = System.Reflection.Assembly.GetExecutingAssembly();
            rc.AssemblyName = asm.ManifestModule.Name;
            rc.AssemblyType = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName;
            rc.AssemblyVersion = System.Diagnostics.FileVersionInfo.GetVersionInfo(BaseDevice.GetPath() + "\\" + rc.AssemblyName).FileVersion;
            rc.ID = Guid.NewGuid().ToString();
            rc.Name = "Jaxis POS Inventory Reduction";
            rc.Type = DeviceType.DataConsumer;
            rc.State = DeviceState.Stopped;
            rc.ProducerMessageType = 0;
            rc.ConsumerMessageType = 8;
            return rc;
        }

                
        public InventoryReduction( )
            : this(GetDefaultDeviceConfig())
        {
        }


        public InventoryReduction(IDeviceConfig _config)
            : base(_config)
        {
        }

        public override void Start()
        {
            State = DeviceState.Started;
            Config.State = DeviceState.Started;
        }

        public override void Stop()
        {
            State = DeviceState.Stopped;
            Config.State = DeviceState.Stopped;
        }

        public override string Consume( IMessage _message )
        {
            string rc = null;
            m_Mutex.WaitOne( );

            try
            {
                if (_message is ITicket )
                {
                    rc = ConsumeTicket( _message as ITicket);
                }
            }
            catch( Exception exp )
            {
                Log.WriteException("TicketConsumer::Consume", exp);
            }
            finally
            {
                m_Mutex.ReleaseMutex();
            }
            return rc;
        }

        private string ConsumeTicket(ITicket _ticket)
        {
            string rc = null;

            try
            {
                foreach (var ti in _ticket.Items)
                {
                    var item = DataManagerFactory.Get().Manage<ITicketItemAlias>().GetAll().FirstOrDefault(a => a.Description == ti.Description);

                    if( null != item && item.PosUPC.HasValue)
                    {
                        var location = DataManagerFactory.Get().Manage<ILocation>().GetAll().FirstOrDefault(l => l.POSAlias == _ticket.Establishment);

                        if( null != location )
                        {
                            for (int i = 0; i < ti.Quantity; i++)
                            {
                                BLManagerFactory.Get().ManageInventory().RemoveUnTaggedFromInventory(item.PosUPC.Value
                                                                                                 , location.LocationID
                                                                                                 , ExitReasons.POSRingup);
                                
                            }
                        }
                    }
                }
            }
            catch (Exception exp)
            {
                Log.WriteException("TicketConsumer::ConsumeTicket", exp);
            }
            return rc;
        }
    }
}
