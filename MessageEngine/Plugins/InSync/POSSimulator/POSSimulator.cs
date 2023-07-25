using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using Jaxis.Interfaces;
using Jaxis.Engine.Base.Device;
using Jaxis.Util.Log4Net;
using JaxisExtensions;
using Jaxis.MessageLibrary.POS;
using System.IO;
using Jaxis.MessageLibrary;

namespace Jaxis.Readers.InSync
{
    //<DeviceConfig>
    //    <AssemblyName>POSSimulator.dll</AssemblyName>
    //    <AssemblyType>Jaxis.Readers.InSync.POSSimulator</AssemblyType>
    //    <AssemblyVersion>1.0</AssemblyVersion>
    //    <ID>222</ID>
    //    <Name>POS Simulator</Name>
    //    <Type>DataProducer</Type>
    //    <State>Started</State>
    //    <ConsumerMessageType>None</ConsumerMessageType>
    //    <Options>
    //        <string>PourFile.txt</string>
    //        <string>POSFile.txt</string>
    //        <string>10</string>
    //    </Options>
    //</DeviceConfig>


    public class POSSimulator : BaseProducerDevice, IProducer
    {
        [Obfuscation(Exclude = true)]
        public static DeviceConfig GetDefaultDeviceConfig()
        {
            DeviceConfig rc = new DeviceConfig();
            System.Reflection.Assembly Asm = System.Reflection.Assembly.GetExecutingAssembly();
            rc.AssemblyName = Asm.ManifestModule.Name;
            rc.AssemblyType = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName;
            rc.AssemblyVersion = System.Diagnostics.FileVersionInfo.GetVersionInfo(BaseDevice.GetPath() + "\\" + rc.AssemblyName).FileVersion;
            rc.ID = Guid.NewGuid().ToString();
            rc.Name = "POS Simulator";
            rc.Type = DeviceType.DataProducer;
            rc.State = DeviceState.Stopped;
            rc.ProducerMessageType = 0;
            rc.ConsumerMessageType = 1;
            DeviceConfigOption Option1 = new DeviceConfigOption();
            Option1.Name = "PourFile";
            Option1.Value = "PourFile.txt";
            rc.Options.Add(Option1);
            DeviceConfigOption Option2 = new DeviceConfigOption();
            Option2.Name = "POSFile";
            Option2.Value = "POSFile.txt";
            rc.Options.Add(Option2);
            DeviceConfigOption Option3 = new DeviceConfigOption();
            Option3.Name = "SleepTime";
            Option3.Value = "10";
            rc.Options.Add(Option3);
            return rc;
        }

        private System.Threading.Thread m_Worker;

                
        public POSSimulator( )
            : this(GetDefaultDeviceConfig())
        {
        }


        public POSSimulator( IDeviceConfig _Config )
            : base( _Config )
        {
            try
            {
                Log.Debug( _Config.GetType( ).ToString( ) );

                Log.Debug( "Create POSSimulator" );
                //Config.ConsumerMessageType = MessageType.None;
                Config.Type = DeviceType.DataProducer;
                Config.State = DeviceState.Stopped;
                State = DeviceState.Stopped;
            }
            catch( Exception exp )
            {
                Log.WriteException( "POSSimulator::POSSimulator", exp );
            }
        }

        override public void Start( )
        {
            try
            {
                Log.Debug( string.Format( "Start POSSimulator" ) );

                if( null != m_DeviceConfig )
                {
//                    CreateDataElements( );

                    State = DeviceState.Started;
                    Config.State = DeviceState.Started;
                    m_Stop = false;
                    m_Worker = new System.Threading.Thread( StartThread );
                    m_Worker.Start( );
                }
            }
            catch( Exception exp )
            {
                Log.WriteException( "POSSimulator:: Start", exp );
            }
        }

        override public void Stop( )
        {
            try
            {
                State = DeviceState.Stopped;
                Config.State = DeviceState.Stopped;
            }
            catch( Exception exp )
            {
                Log.WriteException( "POSSimulator:: Stop", exp );
            }
            finally
            {
                m_Stop = true;
            }
        }

        public void CreateDataElements( )
        {
            POSRecords Rec = new POSRecords( );
            List<Ticket> Tickets = new List<Ticket>( );

            for( int i = 0; i < 3; ++i )
            {
                Ticket T = new Ticket( );
                T.CheckNumber = i.ToString( );
                T.Establishment = "Bar None";
                T.GuestCount = 1;
                T.Date = DateTime.Now.ToString( "yyyy-mm-dd HH:MM:SS" );
                //T.Type = MessageType.RawData;

                List<ITicketItem> Items = new List<ITicketItem>( );

                for( int j = 0; j < 3; ++j )
                {
                    var Item = new TicketItem( );
                    Item.Comment = "Item " + j;
                    Item.Description = "Jose Quervo";
                    Item.IsVoid = false;
                    Item.Price = 10.00M;
                    Item.Quantity = 1;

                    var Mods = new List<ITicketItemModifier>( );

                    Mods.Add( new TicketItemModifier( ) { Name = "Sample Modifier", Price = 2.50M } );

                    Item.Modifiers = Mods;

                    Items.Add( Item );
                }

                T.Items = Items;

                Tickets.Add( T );
            }
            Rec.TicketList = Tickets;

            using( StreamWriter Writer = new StreamWriter( m_DeviceConfig.GetPOSFile( ) ) )
            {
                Writer.SerializeObject<POSRecords>( Rec );
            }

            var PRec = new PourRecords( );
            var Pours = new List<PourData>( );

            for( int i = 0; i < 3; ++i )
            {
                PourData Pour = new PourData( );
                Pour.Temperature = 22.5;
                Pour.TagNumber = "0450001151";
                //Pour.PourTime = 3000000;
                Pour.PourAmount = 35;
                //Pour.EventID = "23";
                Pours.Add( Pour );
            }
            PRec.PourList = Pours;

            using( StreamWriter Writer = new StreamWriter( m_DeviceConfig.GetPourFile( ) ) )
            {
                Writer.SerializeObject<PourRecords>( PRec );
            }
        }


        public void StartThread( )
        {
            try
            {
                while( !m_Stop )
                {
                    System.Threading.Thread.Sleep( m_DeviceConfig.GetSleepTime( ) * 1000 );

                    string Data = string.Empty;

                    using( StreamReader Reader = new StreamReader( m_DeviceConfig.GetPOSFile( ) ) )
                    {
                        Data = Reader.ReadToEnd( );
                    }
                    POSRecords POSEntries = Data.DeserializeObject<POSRecords>( );

                    using( StreamReader Reader = new StreamReader( m_DeviceConfig.GetPourFile( ) ) )
                    {
                        Data = Reader.ReadToEnd( );
                    }
                    PourRecords Pours = Data.DeserializeObject<PourRecords>( );

                    POSEntries.TicketList.ForEach( T => ProduceMessage( T ) );
                    Pours.PourList.ForEach( T => ProduceMessage( T ) );
                }
            }
            catch( Exception exp )
            {
                Log.WriteException( "POSSimulator:: StartThread", exp );
            }
        }
        
        private void SendPOSMessage( IMessage _Tag )
        {
            try
            {
                if( _Tag != null )
                {
                    ProduceMessage( _Tag );
                }
            }
            catch( Exception exp )
            {
                Log.WriteException( "POSSimulator:: SendPOSMessage", exp );
            }
        }

        private void SendPourMessage( IMessage _Tag )
        {
            try
            {
                if( _Tag != null )
                {
                    ProduceMessage( _Tag );
                }
            }
            catch( Exception exp )
            {
                Log.WriteException( "POSSimulator:: SendPourMessage", exp );
            }
        }
    }
}
