using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using Jaxis.Util.Log4Net;
using Jaxis.Engine.Base.Device;
using Jaxis.Interfaces;
using Jaxis.Inventory.Data;
using Jaxis.MessageLibrary;

namespace Jaxis.BeverageManagement.Plugin
{
    public class PourPosReconcile : BaseProducerDevice, IProducer
    {

      //<DeviceConfig>
      //  <AssemblyName>Jaxis.BeverageManagement.Plugin.dll</AssemblyName>
      //  <AssemblyType>Jaxis.BeverageManagement.Plugin.PourPosReconcile</AssemblyType>
      //  <AssemblyVersion>1.0</AssemblyVersion>
      //  <ID>321</ID>
      //  <Name>Pour Pos Reconcile</Name>
      //  <Type>DataProducer</Type>
      //  <State>Started</State>
      //  <ProducerMessageType>64</ProducerMessageType>
      //  <ConsumerMessageType>0</ConsumerMessageType>
      //  <Options>
      //    <DeviceConfigOption>
      //      <Name>Connect String</Name>
      //      <Value>http://localhost:8223/HostWCFService/PourEngineService/</Value>
      //    </DeviceConfigOption>
      //    <DeviceConfigOption>
      //      <Name>Poll Interval</Name>
      //      <Value>1</Value>
      //    </DeviceConfigOption>
      //    <DeviceConfigOption>
      //      <Name>Alert Interval</Name>
      //      <Value>360</Value>
      //    </DeviceConfigOption>
      //  </Options>
      //  <Filters>
      //  </Filters>
      //</DeviceConfig>


        private System.Threading.Thread m_WorkerThd = null;

        [Obfuscation(Exclude = true)]
        public static DeviceConfig GetDefaultDeviceConfig()
        {
            DeviceConfig rc = new DeviceConfig();
            System.Reflection.Assembly Asm = System.Reflection.Assembly.GetExecutingAssembly();
            rc.AssemblyName = Asm.ManifestModule.Name;
            rc.AssemblyType = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName;
            rc.AssemblyVersion = System.Diagnostics.FileVersionInfo.GetVersionInfo(BaseDevice.GetPath() + "\\" + rc.AssemblyName).FileVersion;
            rc.ID = Guid.NewGuid().ToString();
            rc.Name = "Jaxis PourPosReconcile";
            rc.Type = DeviceType.DataProducer;
            rc.State = DeviceState.Stopped;
            rc.ProducerMessageType = 64;
            rc.ConsumerMessageType = 0;
            
            var option = new DeviceConfigOption();
            option.Name = "Connect String";
            option.Value = "http://localhost:8223/HostWCFService/PourEngineService/";
            rc.Options.Add(option);
            
            option = new DeviceConfigOption();
            option.Name = "Poll Interval (in seconds)";
            option.Value = "30"; // 30 minutes
            rc.Options.Add(option);
            
            option = new DeviceConfigOption();
            option.Name = "Alert Interval (in minutes)";
            option.Value = "180"; // 3 hours
            rc.Options.Add(option);

            option = new DeviceConfigOption();
            option.Name = "Pour Window (in minutes)";
            option.Value = "15"; // 3 hours
            rc.Options.Add(option);
            
            return rc;
        }

        //private void CreateTestData()
        //{
        //    IList<string> Result;

        //    IQueryable<IUPCItem> UPC = DataManagerFactory.Get().Manage<IUPCItem>().GetAll().Where(U => U.Name.Contains("Absolut"));
        //    IQueryable<IStandardPour> StandardPour = DataManagerFactory.Get().Manage<IStandardPour>().GetAll().Where(S => S.Name.Contains("Liquor"));
        //    IQueryable<ILocation> Location = DataManagerFactory.Get().Manage<ILocation>().GetAll().Where(L => L.POSAlias.Equals("Bar"));


        //    //List<IUPCItem> ll = UPC.ToList();
        //    if ( null != UPC && 0 < UPC.Count() &&
        //         null != StandardPour && 0 < StandardPour.Count() &&
        //         null != Location && 0 < Location.Count() )
        //    {
        //        IRecipe Recipe = DataManagerFactory.Get().Manage<IRecipe>().Create();
        //        Recipe.Description = "Absolut Shot";
        //        DataManagerFactory.Get().Manage<IRecipe>().Save(Recipe,out Result);

        //        Jaxis.Inventory.Data.IDataItems.ITicketItemAlias Alias = DataManagerFactory.Get().Manage<Jaxis.Inventory.Data.IDataItems.ITicketItemAlias>().Create();
        //        Alias.Description = "Absolut Shot";
        //        Alias.RecipeID = Recipe.RecipeID;
        //        DataManagerFactory.Get().Manage<Jaxis.Inventory.Data.IDataItems.ITicketItemAlias>().Save(Alias, out Result);

        //        IIngredient Ingredient = DataManagerFactory.Get().Manage<IIngredient>().Create();
        //        Ingredient.Number = 1;
        //        Ingredient.RecipeID = Recipe.ObjectID;
        //        Ingredient.Type = Convert.ToInt32(IngredientRequirementType.Required);
        //        Ingredient.ManufacturerID = Guid.Parse("463febfa-fc4b-4ab5-ba65-f687fa387993");  // Guid.Parse("1f05db3f-124e-4f6e-98c4-73069fc0704b"); // UPC.First().ObjectID;
        //        Ingredient.CategoryID = UPC.ToArray()[2].CategoryID;
        //        Ingredient.Quality = 1;
        //        Ingredient.StandardPourID = StandardPour.First().StandardPourID;
        //        DataManagerFactory.Get().Manage<IIngredient>().Save(Ingredient, out Result);
                
        //        //IIngredient Ingredient2 = DataManagerFactory.Get().Manage<IIngredient>().Create();
        //        //Ingredient2.Number = 2;
        //        //Ingredient2.RecipeID = Recipe.ObjectID;
        //        //Ingredient2.Type = Convert.ToInt32(IngredientType.OneOf);
        //        //Ingredient2.UPCID = UPC.ToArray()[2].ObjectID;
        //        //DataManagerFactory.Get().Manage<IIngredient>().Save(Ingredient2, out Result);
        //        //IIngredient Ingredient3 = DataManagerFactory.Get().Manage<IIngredient>().Create();
        //        //Ingredient3.Number = 2;
        //        //Ingredient3.RecipeID = Recipe.ObjectID;
        //        //Ingredient3.Type = Convert.ToInt32(IngredientType.OneOf);
        //        //Ingredient3.UPCID = UPC.ToArray()[3].ObjectID;
        //        //DataManagerFactory.Get().Manage<IIngredient>().Save(Ingredient3, out Result);
        //        //IIngredient Ingredient4 = DataManagerFactory.Get().Manage<IIngredient>().Create();
        //        //Ingredient4.Number = 3;
        //        //Ingredient4.RecipeID = Recipe.ObjectID;
        //        //Ingredient4.Type = Convert.ToInt32(IngredientType.OneOf);
        //        //Ingredient4.UPCID = UPC.ToArray()[4].ObjectID;
        //        //DataManagerFactory.Get().Manage<IIngredient>().Save(Ingredient4, out Result);
        //        //IIngredient Ingredient5 = DataManagerFactory.Get().Manage<IIngredient>().Create();
        //        //Ingredient5.Number = 3;
        //        //Ingredient5.RecipeID = Recipe.ObjectID;
        //        //Ingredient5.Type = Convert.ToInt32(IngredientType.OneOf);
        //        //Ingredient5.UPCID = UPC.ToArray()[5].ObjectID;
        //        //DataManagerFactory.Get().Manage<IIngredient>().Save(Ingredient5, out Result);
        //        //IIngredient Ingredient6 = DataManagerFactory.Get().Manage<IIngredient>().Create();
        //        //Ingredient6.Number = 4;
        //        //Ingredient6.RecipeID = Recipe.ObjectID;
        //        //Ingredient6.Type = Convert.ToInt32(IngredientType.Optional);
        //        //Ingredient6.UPCID = UPC.ToArray()[6].ObjectID;
        //        //DataManagerFactory.Get().Manage<IIngredient>().Save(Ingredient6, out Result);
        //        //IIngredient Ingredient7 = DataManagerFactory.Get().Manage<IIngredient>().Create();
        //        //Ingredient7.Number = 5;
        //        //Ingredient7.RecipeID = Recipe.ObjectID;
        //        //Ingredient7.Type = Convert.ToInt32(IngredientType.Required);
        //        //Ingredient7.UPCID = UPC.ToArray()[7].ObjectID;
        //        //DataManagerFactory.Get().Manage<IIngredient>().Save(Ingredient7, out Result);

        //        IPOSTicket Ticket = DataManagerFactory.Get().Manage<IPOSTicket>().Create();
        //        Ticket.CheckNumber = "5";
        //        Ticket.Comments = "Comments";
        //        Ticket.CustomerTable = "CustomerTable";
        //        Ticket.Establishment = Location.First().POSAlias;
        //        Ticket.GuestCount = 1;
        //        Ticket.TicketDate = DateTime.Now;
        //        DataManagerFactory.Get().Manage<IPOSTicket>().Save(Ticket, out Result);

        //        IPOSTicketItem TicketItem = DataManagerFactory.Get().Manage<IPOSTicketItem>().Create();
        //        TicketItem.Comment = "Comment";
        //        TicketItem.Description = "Absolut Shot";
        //        TicketItem.Price = 10;
        //        TicketItem.Quantity = 1;
        //        TicketItem.POSTicketID = Ticket.ObjectID;
        //        DataManagerFactory.Get().Manage<IPOSTicketItem>().Save(TicketItem, out Result);

        //        Jaxis.Inventory.Data.IPour Pour = DataManagerFactory.Get( ).Manage<Jaxis.Inventory.Data.IPour>( ).Create( );
        //        Pour.TagID = DataManagerFactory.Get().Manage<ITag>().GetAll().First().ObjectID;
        //        Pour.DeviceID = DataManagerFactory.Get().Manage<Jaxis.Inventory.Data.IDevice>().GetAll().First().ObjectID;
        //        Pour.BatteryVoltage = 1.0;
        //        Pour.Temperature = 1.0;
        //        Pour.PourTime = DateTime.Now;
        //        Pour.Duration = 1.0;
        //        Pour.Volume = 40;
        //        Pour.LocationID = Location.First().LocationID;
        //        Pour.UPCID = UPC.ToArray()[2].ObjectID;
        //        DataManagerFactory.Get().Manage<Jaxis.Inventory.Data.IPour>().Save(Pour, out Result);

        //        //Jaxis.Inventory.Data.IPour Pour2 = DataManagerFactory.Get( ).Manage<Jaxis.Inventory.Data.IPour>( ).Create( );
        //        //Pour2.TagID = DataManagerFactory.Get().Manage<ITag>().GetAll().First().ObjectID;
        //        //Pour2.DeviceID = DataManagerFactory.Get().Manage<Jaxis.Inventory.Data.IDevice>().GetAll().First().ObjectID;
        //        //Pour2.BatteryVoltage = 1.0;
        //        //Pour2.Temperature = 1.0;
        //        //Pour2.PourTime = DateTime.Now;
        //        //Pour2.Duration = 1.0;
        //        //Pour2.Volume = 44;
        //        //Pour2.LocationID = Location.First().LocationID;
        //        //Pour2.UPCID = UPC.ToArray()[0].ObjectID;
        //        //DataManagerFactory.Get().Manage<Jaxis.Inventory.Data.IPour>().Save(Pour2, out Result);
        //    }
        //}

                
        public PourPosReconcile( )
            : this(GetDefaultDeviceConfig())
        {
        }



        public PourPosReconcile(IDeviceConfig _config)
            : base(_config)
        {
            //CreateTestData();
        }

        public override void Start()
        {
            try
            {
                m_WorkerThd = new System.Threading.Thread( PollThread );
                m_WorkerThd.Start();

                State = DeviceState.Started;
                Config.State = DeviceState.Started;
            }
            catch (Exception exp)
            {
                Log.WriteException("PourPosReconcile::Start", exp);
            }
        }

        public override void Stop()
        {
            State = DeviceState.Stopped;
            Config.State = DeviceState.Stopped;
            if( null != m_WorkerThd )
            {
                m_WorkerThd.Join( );
            }
        }

        private void SendAlert(AlertTypes _Type, Guid _ObjectID)
        {
//            var alert = new MessageLibrary.ReconcileAlert {AlertType = _Type, ObjectID = _ObjectID.ToString()};
//            ProduceMessage (alert);
        }

        private void SendAlerts(int _alertInterval)
        {
           // IList<string> result;

           // // Query all Pours that have not been alerted or reconciled and have a PourTime < current time - alert interval
           // var pours = DataManagerFactory.Get().Manage<Jaxis.Inventory.Data.IPour>().GetAll().Where(P => P.POSTicketItemID == null && P.Alerted == false && P.PourTime < DateTime.Now.AddSeconds(-1 * _alertInterval));
           // foreach (var P in pours)
           // {
           //     SendAlert(AlertTypes.UnreconciledPour, P.ObjectID);
           //     P.Alerted = true;
           //     DataManagerFactory.Get().Manage<Jaxis.Inventory.Data.IPour>().Save(P, out result);
           // }

           // // Query all TicketItems that have not been alerted or reconciled and have a TicketDate < current time - alert interval
           // //IQueryable<IPOSTicketItem> TicketItems = from T in DataManagerFactory.Get().Manage<IPOSTicket>().GetAll()
           // //                                         from I in DataManagerFactory.Get().Manage<IPOSTicketItem>().GetAll()
           // //                                         where I.POSTicketID == T.POSTicketID && I.Reconciled == false && I.Alerted == false && T.TicketDate < DateTime.Now.AddSeconds(-1 * _AlertInterval)
           // //                                         select I;
           // var ticketItems = DataManagerFactory.Get().Manage<IPOSTicketItem>().GetAll().Where(I => I.Reconciled != I.Quantity && 0 == (I.Status&(int)PosStatus.Alert) ).ToList();
           // foreach (var ti in ticketItems)
           // {
           //     var tickets = DataManagerFactory.Get().Manage<IPOSTicket>().GetAll().Where( T => T.POSTicketID == ti.POSTicketID && T.TicketDate < DateTime.Now.AddSeconds(-1 * _alertInterval) ).ToList();
           //     if (0 != tickets.Count())
           //     {
           //         SendAlert(AlertTypes.UnreconciledTicketItem, ti.ObjectID);
           //         ti.ItemStatus = PosStatus.Alert;
           //         DataManagerFactory.Get().Manage<IPOSTicketItem>().Save(ti, out result);
           //     }
           //}
        }

        public void PollThread()
        {
            //var reconcile = new Reconcile();

            //int sleepTime = 0; // Used to respond to the Device Stop commnad, don't want to sleep to long
            //var lastALertProcess = DateTime.Now;
            //var pollInterval = Config.GetPollInterval();
            //var pourWindow = Config.GetPourWindow();

            //reconcile.ConsolidatedReconcile(pourWindow);

            //while (DeviceState.Started == State )
            //{
            //    if (sleepTime > pollInterval)
            //    {
            //        reconcile.ConsolidatedReconcile(pourWindow);
            //        sleepTime = 0;
            //    }
            //    else
            //    {
            //        sleepTime += 5 * 1000; // 5 secs
            //        System.Threading.Thread.Sleep(5 * 1000);
            //    }
            //}
        }
    }
}
