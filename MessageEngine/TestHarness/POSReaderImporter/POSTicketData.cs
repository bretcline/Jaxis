using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Runtime;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Jaxis.BeverageManagement.Plugin;
using Jaxis.Engine.Base.Device;
using Jaxis.Interfaces;
using Jaxis.MessageLibrary.POS;
using Jaxis.Util.Log4Net;
using IMessage = Jaxis.Interfaces.IMessage;

namespace POSReaderImporter
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
                    //ThreadPool.QueueUserWorkItem(new WaitCallback(ConsumeTicketCallback), _message );
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

        private void ConsumeTicketCallback(object ticket)
        {
            ConsumeTicket( ticket as ITicket );
        }
        private string ConsumeTicket(ITicket _Ticket)
        {
            var rc = string.Empty;
            try
            {
                var ticketDate = null == _Ticket.Date ? DateTime.Now : Convert.ToDateTime(_Ticket.Date, new CultureInfo("fr-ca"));

                Jaxis.Inventory.Data.POSTicket ticket = null;
                if (_Ticket.TouchCount > 1)
                {
                    var items = Jaxis.Inventory.Data.POSTicket.All().Where(x => x.CheckNumber == _Ticket.CheckNumber).ToList();
                    var item = items.FirstOrDefault(x => x.TicketDate.Date == ticketDate.Date);

                    //var item = (from n in items
                    //            group n by n.POSTicketID into g
                    //            select g.OrderByDescending(t => t.TicketDate).FirstOrDefault()).FirstOrDefault();

                    ticket = item;
                }
                if (null == ticket)
                {
                    ticket = new Jaxis.Inventory.Data.POSTicket();
                }

                // This is a duplicate ticket...if the transaction number is the same what we already have, it's a duplicate.
                if (ticket.CheckNumber == _Ticket.CheckNumber && ticket.TransactionID == _Ticket.TransactionID)
                {
                    ticket.TouchCount = _Ticket.TouchCount;
                    ticket.Save();
                }
                else
                {
                    ticket.TouchCount = _Ticket.TouchCount;
                    ticket.CheckNumber = _Ticket.CheckNumber;
                    ticket.Comments = _Ticket.Comments;
                    ticket.CustomerTable = _Ticket.Table;
                    ticket.Establishment = _Ticket.Establishment;
                    ticket.GuestCount = _Ticket.GuestCount;
                    ticket.Server = _Ticket.ServerNumber;
                    ticket.ServerName = _Ticket.Server;
                    ticket.TicketDate = ticketDate;
                    ticket.RawData += _Ticket.GetRawData();
                    ticket.TicketTotal = _Ticket.CheckTotal;
                    ticket.DataSource = _Ticket.DataSource;
                    ticket.TransactionID = _Ticket.TransactionID;

                    ticket.Save();

                    foreach (var tvaItem in _Ticket.TVAItems)
                    {
                        if (ticket.TouchCount > 1)
                        {
                            var tva =
                                Jaxis.Inventory.Data.POSTVADatum.All()
                                    .FirstOrDefault(
                                        t => t.POSTicketID == ticket.POSTicketID && t.Percentage == tvaItem.Percentage);
                            if (null != tva)
                            {
                                tva.Amount = tvaItem.Amount;
                                tva.Total = tvaItem.Total;
                                tva.Save();
                            }
                        }
                        else
                        {
                            var tva = new Jaxis.Inventory.Data.POSTVADatum
                                      {
                                          POSTicketID = ticket.POSTicketID,
                                          Amount = tvaItem.Amount,
                                          Percentage = (decimal?) tvaItem.Percentage,
                                          Total = tvaItem.Total
                                      };

                            tva.Save();
                        }
                    }

                    foreach (var paymentItem in _Ticket.PaymentItems)
                    {
                        if (ticket.TouchCount > 1)
                        {
                            var payment =
                                Jaxis.Inventory.Data.POSPaymentDatum.All()
                                    .FirstOrDefault(
                                        t =>
                                            t.POSTicketID == ticket.POSTicketID &&
                                            t.AccountNumber ==
                                            paymentItem.AccountNumber);
                            if (null != payment)
                            {
                                payment.CustomerName = paymentItem.CustomerName;
                                payment.Payment = paymentItem.Payment;
                                payment.PaymentType = paymentItem.PaymentType;
                                payment.RoomNumber = paymentItem.RoomNumber;

                                payment.Save();
                            }
                        }
                        else
                        {
                            var payment = new Jaxis.Inventory.Data.POSPaymentDatum
                                          {
                                              POSTicketID = ticket.POSTicketID,
                                              AccountNumber =
                                                  paymentItem.AccountNumber,
                                              CustomerName = paymentItem.CustomerName,
                                              Payment = paymentItem.Payment,
                                              PaymentType = paymentItem.PaymentType,
                                              RoomNumber = paymentItem.RoomNumber
                                          };

                            payment.Save();
                        }
                    }
                    //if (_Ticket.Items.Count > 5 * _Ticket.GuestCount)
                    //{
                    //    Console.WriteLine($"Way to many ticket items {_Ticket.Items.Count} for {_Ticket.GuestCount} on ticket {_Ticket.CheckNumber}");
                    //}


                    foreach (var ticketItem in _Ticket.Items)
                    {
                        var ti = new Jaxis.Inventory.Data.POSTicketItem();
                        ti.POSTicketID = ticket.POSTicketID;
                        ti.Comment = ticketItem.Comment;
                        ti.Description = ticketItem.Description;
                        ti.Price = Convert.ToDecimal(ticketItem.Price);
                        ti.Quantity = ticketItem.Quantity;
                        ti.Credit = ticketItem.Credit;

                        ti.Save();

                        foreach (var m in ticketItem.Modifiers)
                        {
                            var modifier = new Jaxis.Inventory.Data.POSTicketItemModifier
                                           {
                                               Name = m.Name,
                                               Price = Convert.ToDecimal(m.Price),
                                               POSTicketItemID = ti.POSTicketItemID
                                           };
                            modifier.Save();
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
