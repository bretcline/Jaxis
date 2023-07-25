using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using System.Windows.Media.Media3D;
using DevExpress.XtraEditors;
using DevExpress.Skins;
using DevExpress.LookAndFeel;
using DevExpress.UserSkins;
using JawsViewerWPF;
using ZStarDevice;
using DevExpress.XtraGrid.Views.Grid;
using System.Windows.Forms.Integration;

namespace Jaws
{
    public partial class Form1 : XtraForm
    {
        private ZStar m_ZStar;
        private JawsViewerWPF.Jaws3DViewer ScanTeeth;
        private JawsViewerWPF.Jaws3DViewer HistoryTeeth;

        private BindingList<RawDataRead> m_gridDataListAdmin;

        private BindingList<DataRead> m_gridDataListUpper;
        private DevExpress.XtraCharts.Series m_XSeriesUpper;
        private DevExpress.XtraCharts.Series m_YSeriesUpper;
        private DevExpress.XtraCharts.Series m_ZSeriesUpper;

        private BindingList<DataRead> m_gridDataListLower;
        private DevExpress.XtraCharts.Series m_XSeriesLower;
        private DevExpress.XtraCharts.Series m_YSeriesLower;
        private DevExpress.XtraCharts.Series m_ZSeriesLower;

        private History m_History;
        private bool m_Paused = false;
        private BindingList<DataRead> m_HistoryList;
        private BindingList<DataRead> m_gridDataListHRawUpper;
        private DevExpress.XtraCharts.Series m_XSeriesHRawUpper;
        private DevExpress.XtraCharts.Series m_YSeriesHRawUpper;
        private DevExpress.XtraCharts.Series m_ZSeriesHRawUpper;
        private BindingList<DataRead> m_gridDataListHRawLower;
        private DevExpress.XtraCharts.Series m_XSeriesHRawLower;
        private DevExpress.XtraCharts.Series m_YSeriesHRawLower;
        private DevExpress.XtraCharts.Series m_ZSeriesHRawLower;
        private int m_Interval;
        

        public Form1()
        {
            m_History = new History();
            m_ZStar = new ZStar();
            m_ZStar.Load();

            InitializeComponent();
        }

        private void Start_Click(object sender, EventArgs e)
        {
            m_gridDataListUpper = new BindingList<DataRead>();
            gridControlUpper.DataSource = m_gridDataListUpper;

            m_XSeriesUpper = new DevExpress.XtraCharts.Series("Upper X Angle", DevExpress.XtraCharts.ViewType.Line);
            m_YSeriesUpper = new DevExpress.XtraCharts.Series("Upper Y Angle", DevExpress.XtraCharts.ViewType.Line);
            m_ZSeriesUpper = new DevExpress.XtraCharts.Series("Upper Z Angle", DevExpress.XtraCharts.ViewType.Line);

            DevExpress.XtraCharts.LineSeriesView m_XSeriesUpperView = (DevExpress.XtraCharts.LineSeriesView)m_XSeriesUpper.View;
            m_XSeriesUpperView.LineMarkerOptions.Visible = false;

            DevExpress.XtraCharts.LineSeriesView m_YSeriesUpperView = (DevExpress.XtraCharts.LineSeriesView)m_YSeriesUpper.View;
            m_YSeriesUpperView.LineMarkerOptions.Visible = false;

            DevExpress.XtraCharts.LineSeriesView m_ZSeriesUpperView = (DevExpress.XtraCharts.LineSeriesView)m_ZSeriesUpper.View;
            m_ZSeriesUpperView.LineMarkerOptions.Visible = false;


            m_gridDataListLower = new BindingList<DataRead>();
            gridControlLower.DataSource = m_gridDataListLower;

            m_XSeriesLower = new DevExpress.XtraCharts.Series("Lower X Angle", DevExpress.XtraCharts.ViewType.Line);
            m_YSeriesLower = new DevExpress.XtraCharts.Series("Lower Y Angle", DevExpress.XtraCharts.ViewType.Line);
            m_ZSeriesLower = new DevExpress.XtraCharts.Series("Lower Z Angle", DevExpress.XtraCharts.ViewType.Line);

            DevExpress.XtraCharts.LineSeriesView m_XSeriesLowerView = (DevExpress.XtraCharts.LineSeriesView)m_XSeriesLower.View;
            m_XSeriesLowerView.LineMarkerOptions.Visible = false;

            DevExpress.XtraCharts.LineSeriesView m_YSeriesLowerView = (DevExpress.XtraCharts.LineSeriesView)m_YSeriesLower.View;
            m_YSeriesLowerView.LineMarkerOptions.Visible = false;

            DevExpress.XtraCharts.LineSeriesView m_ZSeriesLowerView = (DevExpress.XtraCharts.LineSeriesView)m_ZSeriesLower.View;
            m_ZSeriesLowerView.LineMarkerOptions.Visible = false;

            m_HistoryList = new BindingList<DataRead>();

            m_ZStar.Open();

            timer1.Start();
        }

        private void Stop_Click(object sender, EventArgs e)
        {
            timer1.Stop();
            if ( null != m_ZStar)
               m_ZStar.Close();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            var Reads = m_ZStar.GetRead();
            foreach (DataRead D in Reads)
            {
                if (0 == D.Sensor)
                {
                    m_gridDataListUpper.Add(D);

                    m_XSeriesUpper.Points.Add(new DevExpress.XtraCharts.SeriesPoint(D.Time, D.AngleX));
                    m_YSeriesUpper.Points.Add(new DevExpress.XtraCharts.SeriesPoint(D.Time, D.AngleY));
                    m_ZSeriesUpper.Points.Add(new DevExpress.XtraCharts.SeriesPoint(D.Time, D.AngleZ));

                    ScanTeeth.UpperData = D;
                }
                if (1 == D.Sensor)
                {
                    m_gridDataListLower.Add(D);

                    m_XSeriesLower.Points.Add(new DevExpress.XtraCharts.SeriesPoint(D.Time, D.AngleX));
                    m_YSeriesLower.Points.Add(new DevExpress.XtraCharts.SeriesPoint(D.Time, D.AngleY));
                    m_ZSeriesLower.Points.Add(new DevExpress.XtraCharts.SeriesPoint(D.Time, D.AngleZ));

                    ScanTeeth.LowerData = D;
                }
                m_HistoryList.Add(D);
            }

            chartControlUpper.Series.Clear();
            chartControlUpper.Series.Add(m_XSeriesUpper);
            chartControlUpper.Series.Add(m_YSeriesUpper);
            chartControlUpper.Series.Add(m_ZSeriesUpper);

            chartControlLower.Series.Clear();
            chartControlLower.Series.Add(m_XSeriesLower);
            chartControlLower.Series.Add(m_YSeriesLower);
            chartControlLower.Series.Add(m_ZSeriesLower);
        }

        private void buttonHStart_Click(object sender, EventArgs e)
        {
            if (false == m_Paused)
            {
                m_Interval = 0;

                int[] selRows = ((GridView)gridControlHistory.MainView).GetSelectedRows();
                HistoryItem selRow = (HistoryItem)(((GridView)gridControlHistory.MainView).GetRow(selRows[0]));
                m_HistoryList = m_History.Load(selRow.Name);

                m_gridDataListHRawUpper = new BindingList<DataRead>();
                gridControlHRawUpper.DataSource = m_gridDataListHRawUpper;
                m_XSeriesHRawUpper = new DevExpress.XtraCharts.Series("Upper X Angle", DevExpress.XtraCharts.ViewType.Line);
                m_YSeriesHRawUpper = new DevExpress.XtraCharts.Series("Upper Y Angle", DevExpress.XtraCharts.ViewType.Line);
                m_ZSeriesHRawUpper = new DevExpress.XtraCharts.Series("Upper Z Angle", DevExpress.XtraCharts.ViewType.Line);
                DevExpress.XtraCharts.LineSeriesView m_XSeriesHRawUpperView = (DevExpress.XtraCharts.LineSeriesView)m_XSeriesHRawUpper.View;
                m_XSeriesHRawUpperView.LineMarkerOptions.Visible = false;
                DevExpress.XtraCharts.LineSeriesView m_YSeriesHRawUpperView = (DevExpress.XtraCharts.LineSeriesView)m_YSeriesHRawUpper.View;
                m_YSeriesHRawUpperView.LineMarkerOptions.Visible = false;
                DevExpress.XtraCharts.LineSeriesView m_ZSeriesHRawUpperView = (DevExpress.XtraCharts.LineSeriesView)m_ZSeriesHRawUpper.View;
                m_ZSeriesHRawUpperView.LineMarkerOptions.Visible = false;
                chartControlHRawUpper.Series.Clear();
                chartControlHRawUpper.Series.Add(m_XSeriesHRawUpper);
                chartControlHRawUpper.Series.Add(m_YSeriesHRawUpper);
                chartControlHRawUpper.Series.Add(m_ZSeriesHRawUpper);

                m_gridDataListHRawLower = new BindingList<DataRead>();
                gridControlHRawLower.DataSource = m_gridDataListHRawLower;
                m_XSeriesHRawLower = new DevExpress.XtraCharts.Series("Lower X Angle", DevExpress.XtraCharts.ViewType.Line);
                m_YSeriesHRawLower = new DevExpress.XtraCharts.Series("Lower Y Angle", DevExpress.XtraCharts.ViewType.Line);
                m_ZSeriesHRawLower = new DevExpress.XtraCharts.Series("Lower Z Angle", DevExpress.XtraCharts.ViewType.Line);
                DevExpress.XtraCharts.LineSeriesView m_XSeriesHRawLowerView = (DevExpress.XtraCharts.LineSeriesView)m_XSeriesHRawLower.View;
                m_XSeriesHRawLowerView.LineMarkerOptions.Visible = false;
                DevExpress.XtraCharts.LineSeriesView m_YSeriesHRawLowerView = (DevExpress.XtraCharts.LineSeriesView)m_YSeriesHRawLower.View;
                m_YSeriesHRawLowerView.LineMarkerOptions.Visible = false;
                DevExpress.XtraCharts.LineSeriesView m_ZSeriesHRawLowerView = (DevExpress.XtraCharts.LineSeriesView)m_ZSeriesHRawLower.View;
                m_ZSeriesHRawLowerView.LineMarkerOptions.Visible = false;
                chartControlHRawLower.Series.Clear();
                chartControlHRawLower.Series.Add(m_XSeriesHRawLower);
                chartControlHRawLower.Series.Add(m_YSeriesHRawLower);
                chartControlHRawLower.Series.Add(m_ZSeriesHRawLower);
            }

            timerH.Start();
        }

        private void buttonHPause_Click(object sender, EventArgs e)
        {
            m_Paused = true;
            timerH.Stop();
        }

        private void timerH_Tick(object sender, EventArgs e)
        {
            m_Interval++;
            var Reads = new List<DataRead>();

            while ( 0 < m_HistoryList.Count &&
                    m_Interval*1000 > m_HistoryList[0].Time)
            {
                Reads.Add(m_HistoryList[0]);
                m_HistoryList.RemoveAt(0);
            }

            foreach (DataRead D in Reads)
            {
                if (0 == D.Sensor)
                {
                    m_gridDataListHRawUpper.Add(D);

                    m_XSeriesHRawUpper.Points.Add(new DevExpress.XtraCharts.SeriesPoint(D.Time, D.AngleX));
                    m_YSeriesHRawUpper.Points.Add(new DevExpress.XtraCharts.SeriesPoint(D.Time, D.AngleY));
                    m_ZSeriesHRawUpper.Points.Add(new DevExpress.XtraCharts.SeriesPoint(D.Time, D.AngleZ));

                    HistoryTeeth.UpperData = D;
                }
                if (1 == D.Sensor)
                {
                    m_gridDataListHRawLower.Add(D);

                    m_XSeriesHRawLower.Points.Add(new DevExpress.XtraCharts.SeriesPoint(D.Time, D.AngleX));
                    m_YSeriesHRawLower.Points.Add(new DevExpress.XtraCharts.SeriesPoint(D.Time, D.AngleY));
                    m_ZSeriesHRawLower.Points.Add(new DevExpress.XtraCharts.SeriesPoint(D.Time, D.AngleZ));

                    HistoryTeeth.LowerData = D;
                }
            }

            chartControlHRawUpper.Series.Clear();
            chartControlHRawUpper.Series.Add(m_XSeriesHRawUpper);
            chartControlHRawUpper.Series.Add(m_YSeriesHRawUpper);
            chartControlHRawUpper.Series.Add(m_ZSeriesHRawUpper);

            chartControlHRawLower.Series.Clear();
            chartControlHRawLower.Series.Add(m_XSeriesHRawLower);
            chartControlHRawLower.Series.Add(m_YSeriesHRawLower);
            chartControlHRawLower.Series.Add(m_ZSeriesHRawLower);

            if (0 == m_HistoryList.Count)
            {
                timerH.Stop();
                m_Paused = false;
            }

        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            m_History.Save(m_HistoryList, textBox1.Text);
        }

        private void buttonAdminSave_Click(object sender, EventArgs e)
        {

        }

        private void buttonDetectUpper_Click(object sender, EventArgs e)
        {

        }

        private void buttonDetectLower_Click(object sender, EventArgs e)
        {

        }

        private void xtraTabs_SelectedPageChanged(object sender, DevExpress.XtraTab.TabPageChangedEventArgs e)
        {
            m_ZStar.Close();

            if (1 == xtraTabs.SelectedTabPageIndex)
            {
                gridControlHistory.DataSource = m_History.GetHistory();
            }
            else if (2 == xtraTabs.SelectedTabPageIndex)
            {
                comboBoxARead.DataSource = m_ZStar.Detect();
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            ScanTeeth = new JawsViewerWPF.Jaws3DViewer();

            ScanTeeth.Config = new Jaws3DViewerConfig
            {
                CameraPosition = new Point3D(Convert.ToInt32(textBoxCPX.Text), Convert.ToInt32(textBoxCPY.Text), Convert.ToInt32(textBoxCPZ.Text)),
                MoveX = checkBoxMoveX.Checked,
                MoveY = checkBoxMoveY.Checked,
                MoveZ = checkBoxMoveZ.Checked,
                MoveLower = checkBoxMoveLower.Checked,
                MoveUpper = checkBoxMoveUpper.Checked,
                LowerOffset = Convert.ToInt32(textBoxLowerOffset.Text),
                UpperOffset = Convert.ToInt32(textBoxLowerOffset.Text)
            };

            WPFHostNewScan.Child = ScanTeeth;

            HistoryTeeth = new JawsViewerWPF.Jaws3DViewer();

            HistoryTeeth.Config = new Jaws3DViewerConfig
            {
                CameraPosition = new Point3D(Convert.ToInt32(textBoxHCPX.Text), Convert.ToInt32(textBoxHCPY.Text), Convert.ToInt32(textBoxHCPZ.Text)),
                MoveX = checkBoxHMoveX.Checked,
                MoveY = checkBoxHMoveY.Checked,
                MoveZ = checkBoxHMoveZ.Checked,
                MoveLower = checkBoxHMoveLower.Checked,
                MoveUpper = checkBoxHMoveUpper.Checked,
                LowerOffset = Convert.ToInt32(textBoxHLowerOffset.Text),
                UpperOffset = Convert.ToInt32(textBoxHLowerOffset.Text)
            };

            WPFHostScanHistory.Child = HistoryTeeth;
        }

        private void buttonSet_Click(object sender, EventArgs e)
        {
            ScanTeeth = new JawsViewerWPF.Jaws3DViewer();
            ScanTeeth.Config = new Jaws3DViewerConfig
            {
                CameraPosition = new Point3D(Convert.ToInt32(textBoxCPX.Text), Convert.ToInt32(textBoxCPY.Text), Convert.ToInt32(textBoxCPZ.Text)),
                MoveX = checkBoxMoveX.Checked,
                MoveY = checkBoxMoveY.Checked,
                MoveZ = checkBoxMoveZ.Checked,
                MoveLower = checkBoxMoveLower.Checked,
                MoveUpper = checkBoxMoveUpper.Checked,
                LowerOffset = Convert.ToInt32(textBoxLowerOffset.Text),
                UpperOffset = Convert.ToInt32(textBoxLowerOffset.Text)
            };
            WPFHostNewScan.Child = ScanTeeth;
        }

        private void buttonhSet_Click(object sender, EventArgs e)
        {
            HistoryTeeth = new JawsViewerWPF.Jaws3DViewer();
            HistoryTeeth.Config = new Jaws3DViewerConfig
            {
                CameraPosition = new Point3D(Convert.ToInt32(textBoxHCPX.Text), Convert.ToInt32(textBoxHCPY.Text), Convert.ToInt32(textBoxHCPZ.Text)),
                MoveX = checkBoxHMoveX.Checked,
                MoveY = checkBoxHMoveY.Checked,
                MoveZ = checkBoxHMoveZ.Checked,
                MoveLower = checkBoxHMoveLower.Checked,
                MoveUpper = checkBoxHMoveUpper.Checked,
                LowerOffset = Convert.ToInt32(textBoxHLowerOffset.Text),
                UpperOffset = Convert.ToInt32(textBoxHLowerOffset.Text)
            };
            WPFHostScanHistory.Child = HistoryTeeth;
        }

        private void buttonAStart_Click(object sender, EventArgs e)
        {
            m_gridDataListAdmin = new BindingList<RawDataRead>();
            gridControlAdmin.DataSource = m_gridDataListAdmin;
            
            m_ZStar.Open(0, comboBoxARead.Text);

            timer2.Start();
        }

        private void buttonAStop_Click(object sender, EventArgs e)
        {
            timer2.Stop();
            m_ZStar.Close(0);
        }

        private void buttonRefeash_Click(object sender, EventArgs e)
        {
            comboBoxARead.DataSource = m_ZStar.Detect();
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            var Reads = m_ZStar.GetRawRead();
            foreach (RawDataRead D in Reads)
            {
                m_gridDataListAdmin.Add(D);
            }
        }
    }
}