using System;
using System.Linq;
using System.Reflection;
using System.Text;
using Jaxis.Inventory.Data.IDataItems;
using Jaxis.Util.Log4Net;
using Jaxis.Engine.Base.Device;
using Jaxis.Interfaces;
using Jaxis.MessageLibrary.POS;
using Jaxis.Inventory.Data;
using System.Collections.Generic;

namespace Jaxis.BeverageManagement.Plugin
{
    public class TicketConsumer : BaseBevManDevice, IConsumer
    {

        [Obfuscation(Exclude = true)]
        public static DeviceConfig GetDefaultDeviceConfig()
        {
            DeviceConfig rc = new DeviceConfig();
            System.Reflection.Assembly Asm = System.Reflection.Assembly.GetExecutingAssembly();
            rc.AssemblyName = Asm.ManifestModule.Name;
            rc.AssemblyType = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName;
            rc.AssemblyVersion =
                System.Diagnostics.FileVersionInfo.GetVersionInfo(BaseDevice.GetPath() + "\\" + rc.AssemblyName)
                      .FileVersion;
            rc.ID = Guid.NewGuid().ToString();
            rc.Name = "Jaxis TicketConsumer";
            rc.Type = DeviceType.DataConsumer;
            rc.State = DeviceState.Stopped;
            rc.ProducerMessageType = 0;
            rc.ConsumerMessageType = 8;
            return rc;
        }


        public TicketConsumer()
            : this(GetDefaultDeviceConfig())
        {
        }


        public TicketConsumer(IDeviceConfig _config)
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

        public override string Consume(IMessage _message)
        {
            string rc = null;
            m_Mutex.WaitOne();

            try
            {
                if (_message is ITicket)
                {
                    rc = ConsumeTicket(_message as ITicket);
                }
            }
            catch (Exception exp)
            {
                Log.WriteException("TicketConsumer::Consume", exp);
            }
            finally
            {
                m_Mutex.ReleaseMutex();
            }
            return rc;
        }

        private string ConsumeTicket(ITicket _Ticket)
        {
            string rc = null;
            try
            {
                var ticket = DataManagerFactory.Get().Manage<IPOSTicket>().Create();
                ticket.CheckNumber = _Ticket.CheckNumber;
                ticket.Comments = _Ticket.Comments;
                ticket.CustomerTable = _Ticket.Table;
                ticket.Establishment = _Ticket.Establishment;
                ticket.GuestCount = _Ticket.GuestCount;
                ticket.TicketDate = null == _Ticket.Date ? DateTime.Now : Convert.ToDateTime(_Ticket.Date);

                var builder = new StringBuilder();
                _Ticket.RawData.ForEach(i => builder.Append(i + System.Environment.NewLine));
                ticket.RawData = builder.ToString();
                IList<string> result;
                DataManagerFactory.Get().Manage<IPOSTicket>().Save(ticket, out result);

                foreach (var ti in _Ticket.Items)
                {
                    var ticketItem = DataManagerFactory.Get().Manage<IPOSTicketItem>().Create();
                    ticketItem.Comment = ti.Comment;
                    ticketItem.Description = ti.Description;
                    ticketItem.ItemStatus = ti.IsVoid == true ? PosStatus.Void : PosStatus.Pending;
                    ticketItem.Price = Convert.ToDecimal(ti.Price);
                    ticketItem.POSTicketID = ticket.ObjectID;
                    ticketItem.Quantity = ti.Quantity;

                    if (ticketItem.ItemStatus != PosStatus.Void)
                    {
                        InventoryReduction(ticketItem, ticket);
                    }

                    DataManagerFactory.Get().Manage<IPOSTicketItem>().Save(ticketItem, out result);

                    foreach (var m in ti.Modifiers)
                    {
                        var modifier = DataManagerFactory.Get().Manage<IPOSTicketItemModifier>().Create();
                        modifier.Name = m.Name;
                        modifier.Price = Convert.ToDecimal(m.Price);
                        modifier.POSTicketItemID = ticketItem.ObjectID;
                        DataManagerFactory.Get().Manage<IPOSTicketItemModifier>().Save(modifier, out result);
                    }
                }
            }
            catch (Exception exp)
            {
                Log.WriteException("TicketConsumer::ConsumeTicket", exp);
            }
            return rc;
        }

        private void InventoryReduction( IPOSTicketItem ti, IPOSTicket _ticket )
        {
            try
            {
                var item =
                    DataManagerFactory.Get().Manage<ITicketItemAlias>().GetAll().FirstOrDefault(a => a.Description == ti.Description);

                if (null != item && item.PosUPC.HasValue)
                {
                    var location =
                        DataManagerFactory.Get()
                                          .Manage<ILocation>()
                                          .GetAll()
                                          .FirstOrDefault(l => l.POSAlias == _ticket.Establishment);

                    if (null != location)
                    {
                        for (int i = 0; i < ti.Quantity; i++)
                        {
                            BLManagerFactory.Get().ManageInventory().RemoveUnTaggedFromInventory(item.PosUPC.Value
                                                                                                 , location.LocationID
                                                                                                 , ExitReasons.POSRingup);
                            ti.ItemStatus = PosStatus.Complete;
                        }
                    }
                }
            }
            catch (Exception exp)
            {
                Log.WriteException("TicketConsumer::ConsumeTicket", exp);
            }
        }
    

    // Entity Framework Version.
        //private string ConsumeTicket(ITicket _Ticket)
        //{
        //    IList<string> Result;
        //    string rc = null;

        //    try
        //    {

        //        using (var data = new BeverageMonitorEntities())
        //        {
        //            //IPOSTicket ticket = DataManagerFactory.Get().Manage<IPOSTicket>().Create();
        //            var ticket = new BeverageMonitor.Entities.POSTicket
        //            {
        //                CheckNumber = _Ticket.CheckNumber,
        //                Comments = _Ticket.Comments,
        //                CustomerTable = _Ticket.Table,
        //                Establishment = _Ticket.Establishment,
        //                GuestCount = _Ticket.GuestCount
        //            };
        //            if (null == _Ticket.Date)
        //            {
        //                ticket.TicketDate = DateTime.Now;
        //            }
        //            else
        //            {
        //                ticket.TicketDate = Convert.ToDateTime(_Ticket.Date);
        //            }
        //            var builder = new StringBuilder();
        //            _Ticket.RawData.ForEach(i => builder.Append(i + System.Environment.NewLine));
        //            ticket.RawData = builder.ToString();
        //            //DataManagerFactory.Get().Manage<IPOSTicket>().Save(ticket, out Result);

        //            foreach (var TI in _Ticket.Items)
        //            {
        //                //var ticketItem = DataManagerFactory.Get().Manage<IPOSTicketItem>().Create();
        //                var ticketItem = new BeverageMonitor.Entities.POSTicketItem
        //                {
        //                    Comment = TI.Comment,
        //                    Description = TI.Description,
        //                    Status =
        //                        TI.IsVoid == true ? (int)PosStatus.Void : (int)PosStatus.Pending,
        //                    Price = Convert.ToDecimal(TI.Price),
        //                    POSTicketID = ticket.POSTicketID,
        //                    Quantity = TI.Quantity
        //                };
        //                //DataManagerFactory.Get().Manage<IPOSTicketItem>().Save(ticketItem, out Result);

        //                foreach (var M in TI.Modifiers)
        //                {
        //                    //var modifier = DataManagerFactory.Get().Manage<IPOSTicketItemModifier>().Create();
        //                    var modifier = new BeverageMonitor.Entities.POSTicketItemModifier
        //                    {
        //                        Name = M.Name,
        //                        Price = Convert.ToDecimal(M.Price),
        //                        POSTicketItemID = ticketItem.POSTicketItemID
        //                    };
        //                    //DataManagerFactory.Get().Manage<IPOSTicketItemModifier>().Save(modifier, out Result);
        //                }
        //            }
        //            data.SaveChanges();
        //        }
        //    }
        //    catch (Exception exp)
        //    {
        //        Log.WriteException("TicketConsumer::ConsumeTicket", exp);
        //    }

        //    return rc;
        //}

    }
}
