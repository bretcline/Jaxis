using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using System.Xml.Serialization;
using DevExpress.Data;
using DevExpress.Skins;
using DevExpress.LookAndFeel;
using DevExpress.UserSkins;
using DevExpress.Utils;
using DevExpress.XtraGrid.Views.Grid;
using Jaxis.Controls.GlassFill;
using Jaxis.Engine;
using Jaxis.Inventory.Data;


namespace PourChallenge
{

    public partial class Form1 : Form
    {
        private Thread m_MessageProcessor = null;
        private EngineServiceDll m_Engine;
        BindingList<PourContestant> m_LeaderBoard = new BindingList<PourContestant>();

        private Guid SizeTypeID = new Guid( "A65E817C-58B7-43D2-B72D-66F393C81FA8" );

        public Form1()
        {
            InitializeComponent();

            m_Engine = new EngineServiceDll();


            m_MessageProcessor = new Thread(ProcessMessages);
            m_MessageProcessor.Start();

            Thread.Sleep(15000);

            var plugin = new PourChallengeCollector();
            plugin.ProcessPour = ProcessPour;
            m_Engine.RegisterDevice( plugin );
            plugin.Start();

            grdTopPours.DataSource = m_LeaderBoard;

            var variance = gvLeaders.Columns["Variance"];
            variance.DisplayFormat.FormatType = FormatType.Custom;
            variance.DisplayFormat.FormatString = "P";
            variance.SortOrder = ColumnSortOrder.Ascending;
        }

        protected void ProcessMessages( )
        {
            m_Engine.Start();
        }

        protected void ProcessPour( Pour _pour )
        {
            if (InvokeRequired)
            {
                Invoke((MethodInvoker)(() => ProcessPour(_pour)));
            }
            else
            {
                double d = _pour.Volume;

                var upc = BLManagerFactory.Get().ManageUPCs().Get(_pour.UPCID);
                var root = BLManagerFactory.Get().ManageCategories().Get(upc.RootCategoryID);
                var standard = BLManagerFactory.Get().ManageStandardPours().Get(root.Name);

                var value = (int)((_pour.Volume / standard) * 100);

                ctrGlassFill.FillLevel = value;
                ctrGlassFill.GlassType = GlassTypes.Shot;
                ctrGlassFill.Fill();

                var pourConversion = BLManagerFactory.Get().GetAdminValue("Pour Conversion");

                var sizeType = BLManagerFactory.Get().ManageSizeTypes().Get(new Guid(pourConversion));
                var volume = (d * sizeType.ConversionMultiplier);
                dgPour.Text = volume.ToString("000.00");
                var variance = (volume - standard) / standard;

                var person = new PourContestant
                {
                    Name = "<Pending>",
                    PourAmount = volume.ToString("00.0000"),
                    PourTime = _pour.PourTime.ToShortTimeString(),
                    Variance = Math.Abs(variance),
                    OverUnder = (variance > 0) ? "Over" : "Under"

                };

                m_LeaderBoard.Add( person );
            }
        }


        private void gvLeaders_ShowingEditor(object sender, CancelEventArgs e)
        {
            try
            {
                var view = sender as GridView;
                if (view != null)
                {
                    var item = view.GetFocusedRow() as PourContestant;
                    var price = view.Columns["Name"];
                    if (view.FocusedColumn != price)
                    {
                        e.Cancel = true;
                    }
                }
            }
            catch
            {
            }
        }

        private void gvLeaders_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            if (e.Column.FieldName == "Name" && e.Value.ToString() != "<Pending>")
            {
                var pourGuy = gvLeaders.GetFocusedRow() as PourContestant;
                using (var writer = new StreamWriter("LeaderBoard.xml", true))
                {
                    writer.WriteLine(SerializeToString( pourGuy ));
                }
            }
        }

        public static string SerializeToString(PourContestant obj)
        {
            var serializer = new XmlSerializer(obj.GetType());

            using (var writer = new StringWriter())
            {
                serializer.Serialize(writer, obj);

                return writer.ToString();
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            m_Engine.Stop();
        }

    }


    public class PourContestant
    {
        public string Name { get; set; }
        public string PourTime { get; set; }
        public string PourAmount { get; set; }
        public double Variance { get; set; }
        public string OverUnder { get; set; }
    }
}