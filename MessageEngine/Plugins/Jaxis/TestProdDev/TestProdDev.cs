using System;
using System.Reflection;
using Jaxis.Engine.Base;
using Jaxis.Engine.Base.Device;
using Jaxis.Interfaces;
using Jaxis.Interfaces.Tags;
using Jaxis.MessageLibrary;
using Jaxis.Readers.Identec;
using Jaxis.Util.Log4Net;

namespace Jaxis.Engine.Device
{
    /*
        <DeviceConfig>
            <AssemblyName>TestProdDev.dll</AssemblyName>
            <AssemblyType>Jaxis.Engine.Device.TestProdDev</AssemblyType>
            <AssemblyVersion>1.0</AssemblyVersion>
            <ID>123</ID>
            <Name>Test Prod</Name>
            <Type>DataProducer</Type>
            <State>Stopped</State>
            <ProducerMessageType>1</ProducerMessageType>
            <ConsumerMessageType>0</ConsumerMessageType>
        </DeviceConfig>
    */

    public class TestProdDev : BaseProducerDevice, IProducer
    {
        protected string m_TagID = "12345";
        protected string m_DeviceID = "";
        private int m_MinTime = 5000;
        private int m_MaxTime = 20000;
        private System.Threading.Thread m_PourWorker;
        private System.Threading.Thread m_PhaseWorker;
        private System.Threading.Thread m_TagAlertWorker;
        private System.Threading.Thread m_DeviceAlertWorker;
        private System.Threading.Thread m_TicketWorker;
        private int m_PourCount = 0;

        [Obfuscation(Exclude = true)]
        public static DeviceConfig GetDefaultDeviceConfig()
        {
            DeviceConfig rc = new DeviceConfig();
            System.Reflection.Assembly Asm = System.Reflection.Assembly.GetExecutingAssembly();
            rc.AssemblyName = Asm.ManifestModule.Name; // "TestProdDev.dll";
            rc.AssemblyType = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName;
            rc.AssemblyVersion = System.Diagnostics.FileVersionInfo.GetVersionInfo(BaseDevice.GetPath() + "\\" + rc.AssemblyName).FileVersion; //"1.0";
            rc.ID = Guid.NewGuid().ToString();
            rc.Name = "TestProdDev";
            rc.Type = DeviceType.DataProducer;
            rc.State =DeviceState.Stopped;
            rc.ProducerMessageType = 1;
            rc.ConsumerMessageType = 0;
            return rc;
        }

        
        public TestProdDev( )
            : this(GetDefaultDeviceConfig())
        {
        }

        public TestProdDev( IDeviceConfig _Config )
            : base( _Config )
        {
            //Config.ConsumerMessageType = MessageType.None;
            //Config.Type = DeviceType.DataProducer;
            //Config.State = DeviceState.Started;
            //State = DeviceState.Started;
            //ClientData.GetClientData( ).DeviceID = "1";
        }

        override public void Start( )
        {
            State = DeviceState.Started;
            Config.State = DeviceState.Started;
            m_Stop = false;
            m_PourWorker = new System.Threading.Thread( StartPourThread );
            m_PourWorker.Start( );
            m_PhaseWorker = new System.Threading.Thread( StartPhaseThread );
            m_PhaseWorker.Start( );
            m_TagAlertWorker = new System.Threading.Thread( StartTagAlertThread );
            m_TagAlertWorker.Start( );
            m_DeviceAlertWorker = new System.Threading.Thread( StartDeviceAlertThread );
            m_DeviceAlertWorker.Start( );
            m_TicketWorker = new System.Threading.Thread(StartTicketThread);
            m_TicketWorker.Start();
        }

        override public void Stop( )
        {
            State = DeviceState.Stopped;
            Config.State = DeviceState.Stopped;
            m_Stop = true;
        }

        public void StartPourThread( )
        {
            try
            {
                Random R = new Random( DateTime.Now.Millisecond );
                while( !m_Stop )
                {
                    IdentecPour Message = new IdentecPour { Driver = "Pour" };
                    System.Threading.Thread.Sleep( R.Next( m_MinTime, m_MaxTime ) );

                    AngleSlot Angle1 = new AngleSlot( );
                    Angle1.Angle = ( 60 + 81 ) / 2;
                    Angle1.Duration = TimeSpan.FromSeconds( R.Next( 1, 2 ) );
                    Message.Angles.Add( Angle1 );

                    AngleSlot Angle2 = new AngleSlot( );
                    Angle2.Angle = ( 81 + 87 ) / 2;
                    Angle2.Duration = TimeSpan.FromSeconds( R.Next( 1, 2 ) );
                    Message.Angles.Add( Angle2 );

                    AngleSlot Angle3 = new AngleSlot( );
                    Angle3.Angle = ( 87 + 92 ) / 2;
                    Angle3.Duration = TimeSpan.FromSeconds( R.Next( 1, 2 ) );
                    Message.Angles.Add( Angle3 );

                    AngleSlot Angle4 = new AngleSlot( );
                    Angle4.Angle = ( 92 + 98 ) / 2;
                    Angle4.Duration = TimeSpan.FromSeconds( R.Next( 1, 3 ) );
                    Message.Angles.Add( Angle4 );

                    AngleSlot Angle5 = new AngleSlot( );
                    Angle5.Angle = ( 98 + 103 ) / 2;
                    Angle5.Duration = TimeSpan.FromSeconds( R.Next( 1, 2 ) );
                    Message.Angles.Add( Angle5 );

                    AngleSlot Angle6 = new AngleSlot( );
                    Angle6.Angle = ( 103 + 106 ) / 2;
                    Angle6.Duration = TimeSpan.FromSeconds( R.Next( 1, 3 ) );
                    Message.Angles.Add( Angle6 );

                    AngleSlot Angle7 = new AngleSlot( );
                    Angle7.Angle = ( 106 + 112 ) / 2;
                    Angle7.Duration = TimeSpan.FromSeconds( R.Next( 1, 2 ) );
                    Message.Angles.Add( Angle7 );

                    AngleSlot Angle8 = new AngleSlot( );
                    Angle8.Angle = ( 112 + 115 ) / 2;
                    Angle8.Duration = TimeSpan.FromSeconds( R.Next( 1, 2 ) );
                    Message.Angles.Add( Angle8 );

                    AngleSlot Angle9 = new AngleSlot( );
                    Angle9.Angle = ( 115 + 121 ) / 2;
                    Angle9.Duration = TimeSpan.FromSeconds( R.Next( 1, 3 ) );
                    Message.Angles.Add( Angle9 );

                    AngleSlot Angle10 = new AngleSlot( );
                    Angle10.Angle = ( 121 + 142 ) / 2;
                    Angle10.Duration = TimeSpan.FromSeconds( R.Next( 1, 2 ) );
                    Message.Angles.Add( Angle10 );

                    AngleSlot Angle11 = new AngleSlot( );
                    Angle11.Angle = ( 142 + 160 ) / 2;
                    Angle11.Duration = TimeSpan.FromSeconds( R.Next( 1, 3 ) );
                    Message.Angles.Add( Angle11 );

                    AngleSlot Angle12 = new AngleSlot( );
                    Angle12.Angle = ( 160 + 180 ) / 2;
                    Angle12.Duration = TimeSpan.FromSeconds( R.Next( 1, 3 ) );
                    Message.Angles.Add( Angle12 );

                    Message.TagID = m_TagID;

                    if( m_PourCount < 1 )
                    {
                        Message.DeviceID = m_DeviceID;
                        Message.SignalStrength = 1;
                    }
                    else if( m_PourCount < 2 )
                    {
                        Message.DeviceID = "2";
                        Message.SignalStrength = 2;
                    }
                    else
                    {
                        Message.DeviceID = m_DeviceID;
                        Message.SignalStrength = 1;
                    }
                    //Message.SignalStrength = 1;
                    Message.BatteryVoltage = (double)R.Next( 3, 9 ) + R.NextDouble( );
                    Message.Temperature = (double)R.Next( 32, 70 ) + R.NextDouble( );
                    m_PourCount++;
                    Message.PourCount = m_PourCount;
                    Message.ReadTime = DateTime.Now;
                    //Message.Type = MessageType.RawData;

                    ProduceMessage( Message );

                    CalcPour ClacMessage = new CalcPour();
                    ClacMessage.BatteryVoltage = Message.BatteryVoltage;
                    ClacMessage.DeviceID = Message.DeviceID;
                    ClacMessage.Driver = Message.Driver;
                    ClacMessage.OriginalType = OriginalMessageType.Identec;
                    ClacMessage.PourAmount = 50;
                    ClacMessage.PourCount = Message.PourCount;
                    ClacMessage.PourDuration = Message.ReadTime.Ticks;
                    ClacMessage.ReadTime = Message.ReadTime;
                    ClacMessage.TagNumber = Message.TagID;
                    ClacMessage.Temperature = Message.Temperature;
                    ClacMessage.TotalPoured = 900;
                    ClacMessage.UPCName = "1";

                    ProduceMessage(ClacMessage);
                }
            }
            catch( Exception exp )
            {
                Log.WriteException( "Engine:: StartPourThread", exp );
            }
        }

        public void StartPhaseThread( )
        {
            try
            {
                Random R = new Random( DateTime.Now.Millisecond );
                while( !m_Stop )
                {
                    PhaseMessage Message = new PhaseMessage { Driver = "Phase" };
                    System.Threading.Thread.Sleep( R.Next( m_MinTime, m_MaxTime ) );
                    Message.TagID = m_TagID;
                    Message.DeviceID = m_DeviceID;
                    Message.SignalStrength = 1;
                    Message.Temperature = (double)R.Next( 32, 70 ) + R.NextDouble( );
                    Message.ReadTime = DateTime.Now;
                    Message.BatteryVoltage = (double)R.Next( 3, 9 ) + R.NextDouble( );
                    Message.EventType = (TagPhaseType)R.Next( 0, 4 );
                    //Message.Type = MessageType.RawData;

                    ProduceMessage( Message );
                }
            }
            catch( Exception exp )
            {
                Log.WriteException( "Engine:: StartPhaseThread", exp );
            }
        }

        public void StartTagAlertThread( )
        {
            try
            {
                Random R = new Random( DateTime.Now.Millisecond );
                while( !m_Stop )
                {
                    TagAlertMessage Message = new TagAlertMessage { Driver = "TagAlert"};
                    System.Threading.Thread.Sleep( R.Next( m_MinTime, m_MaxTime ) );
                    Message.TagID = m_TagID;
                    Message.ReadTime = DateTime.Now;
                    Message.DeviceID = m_DeviceID;
                    Message.BatteryVoltage = (double)R.Next( 3, 9 ) + R.NextDouble( );
                    //                    Message.Parameters = Convert.ToString( R.Next( 1, 5 ) ).ToString( );
                    Message.AlertType = (AlertTypes)R.Next( 100, 104 );
                    //Message.Type = MessageType.RawData;

                    ProduceMessage( Message );
                }
            }
            catch( Exception exp )
            {
                Log.WriteException( "Engine:: StartTagAlertThread", exp );
            }
        }

        public void StartDeviceAlertThread( )
        {
            try
            {
                Random R = new Random( DateTime.Now.Millisecond );
                while( !m_Stop )
                {
                    DeviceAlertMessage Message = new DeviceAlertMessage { Driver = "DeviceAlert"};
                    System.Threading.Thread.Sleep( R.Next( m_MinTime, m_MaxTime ) );
                    Message.ReadTime = DateTime.Now;
                    Message.DeviceID = m_DeviceID;
                    Message.AlertType = (AlertTypes)R.Next( 0, 2 );
                    //Message.Type = MessageType.RawData;

                    ProduceMessage( Message );
                }
            }
            catch( Exception exp )
            {
                Log.WriteException( "Engine:: StartDeviceAlertThread", exp );
            }
        }

        public void StartTicketThread()
        {
            try
            {
                Random R = new Random(DateTime.Now.Millisecond);
                while (!m_Stop)
                {
                    Jaxis.MessageLibrary.POS.Ticket Ticket = new Jaxis.MessageLibrary.POS.Ticket { Driver = "Ticket" };
                    System.Threading.Thread.Sleep(R.Next(m_MinTime, m_MaxTime));
                    Ticket.ReadTime = DateTime.Now;
                    Ticket.CheckNumber = "CheckNumber1";
                    Ticket.Comments = "Comments1";
                    Ticket.Date = Convert.ToString(DateTime.Now);
                    Ticket.Establishment = "Establishment1";
                    Ticket.GuestCount = 1;
                    Ticket.Server = "Server1";

                    Jaxis.MessageLibrary.POS.TicketItem Item = new Jaxis.MessageLibrary.POS.TicketItem();
                    Item.Comment = "Comment3";
                    Item.Description = "Description3";
                    Item.IsVoid = false;
                    Item.Price = 10.1M;
                    Item.Quantity = 3;
                    Item.UPC = "UPC3";
                    Jaxis.MessageLibrary.POS.TicketItemModifier Modifier = new MessageLibrary.POS.TicketItemModifier();
                    Modifier.Name = "Modifier2";
                    Modifier.Price = 11.2M;
                    Item.Modifiers.Add(Modifier);
                    Ticket.Items.Add(Item);

                    Ticket.RawData.Add("Test");
                    Ticket.RawData.Add("Test");
                    Ticket.RawData.Add("Test");
                    Ticket.RawData.Add("Test");

                    ProduceMessage(Ticket);
                }
            }
            catch (Exception exp)
            {
                Log.WriteException("Engine:: StartTicketThread", exp);
            }
        }

    }
}