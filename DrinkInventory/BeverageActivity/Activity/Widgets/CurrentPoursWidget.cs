using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.Utils;
using DevExpress.XtraBars.Docking;
using DevExpress.XtraCharts;
using DevExpress.XtraEditors;
using Jaxis.Interfaces;
using Jaxis.Inventory.Data;
using Jaxis.Inventory.Data.IBLDataItems;
using Jaxis.MessageLibrary;
using Jaxis.Util.Log4Net;

namespace BeverageActivity.Forms.Activity.Widgets
{
    public partial class CurrentPoursWidget : QueueingWidget<CalcPour>, IActivityControl, ILoadable
    {
        public CurrentPoursWidget( )
        {
            InitializeComponent( );
            PourChartMax = 10;
            MessageType = new List<Type> { typeof(CalcPour) };
            m_Database = new BeverageMonitorDB();
        }

        private BeverageMonitorDB m_Database;

        #region IActivityControl Members

        //public List<Type> MessageType { get; protected set; }
        //BlockingCollection<DataPour> m_Pours = new BlockingCollection<DataPour>();
        //private Task m_Processor = null;
        //private bool m_Running;

        //public bool AddActivityItem( object _item )
        //{
        //    bool rc = true;

        //    //if( InvokeRequired )
        //    //{
        //    //    Invoke( (MethodInvoker)( ( ) => AddActivityItem( _item ) ) );
        //    //}
        //    //else
        //    {
        //        if (_item is DataPour)
        //        {
        //            m_Pours.Add( _item as DataPour );
        //            //AddNewPour(_item as DataPour);
        //        }
        //    }
        //    return rc;
        //}

        public string DisplayName { get { return "Current Pours"; } }
        public Guid DisplayID { get { return new Guid("1541739C-1975-417D-A026-A37688E42F25"); } }
        public object ControlTag { get; set; }

        #endregion

        private IEnumerable<IBLCategory> m_RootCategories = null;
        Dictionary<string, int> m_MessageCount = new Dictionary<string, int>();
        public int PourChartMax{ get; set; }

        private void LoadPreviousPours( )
        {
            foreach (Series series in this.chartCurrentPours.Series)
            {
                series.LabelsVisibility = DefaultBoolean.True;
            }

            this.chartCurrentPours.RuntimeSelection = true;
            this.chartCurrentPours.ObjectHotTracked -= new DevExpress.XtraCharts.HotTrackEventHandler(this.ChartCurrentPoursObjectHotTracked);
            this.chartCurrentPours.ObjectHotTracked += new DevExpress.XtraCharts.HotTrackEventHandler(this.ChartCurrentPoursObjectHotTracked);


            IList<IUIPourPoint> pours = null;
            Log.Time( "Get Tag Activity", LogType.Debug, true, ( ) =>
            {
                pours = BLManagerFactory.Get( ).ManagePours( ).GetPourPoints( PourChartMax );
            } );

            foreach( var uiPourPoint in pours )
            {
                AddPourPoint( uiPourPoint );
            }
            var diagram = chartCurrentPours.Diagram as XYDiagram;
            if (diagram != null)
            {
                IBLStandardPour rc = null;
                var manager = BLManagerFactory.Get().ManageStandardPours();
                var data = manager.GetAll();
                foreach (ConstantLine line in diagram.AxisY.ConstantLines)
                {
                    BuildConstantLine(data, line);
                    //rc = data.Where(p => p.Name.Equals(line.Name, StringComparison.InvariantCultureIgnoreCase)).FirstOrDefault();
                    //if (null != rc)
                    //{
                    //    var pourStandard = BLManagerFactory.Get().ConvertPourToUnits(rc.PourStandard);
                    //    line.AxisValueSerializable = pourStandard.ToString();
                    //}
                }
                foreach( SecondaryAxisY yAxis in diagram.SecondaryAxesY )
                {
                    foreach (ConstantLine line in yAxis.ConstantLines)
                    {
                        BuildConstantLine(data, line);
                    }
                }
                foreach (Strip strip in diagram.AxisY.Strips)
                {
                    BuildVarianceStrip(data, strip);
                }
                foreach (SecondaryAxisY yAxis in diagram.SecondaryAxesY)
                {
                    foreach (Strip strip in yAxis.Strips)
                    {
                        BuildVarianceStrip(data, strip);
                    }
                }
            }
        }

        private void BuildVarianceStrip(IEnumerable<IBLStandardPour> data, Strip strip)
        {
            IBLStandardPour rc;
            rc = data.Where(p => p.Name.Equals(strip.Name.Replace("Strip", ""), StringComparison.InvariantCultureIgnoreCase)).FirstOrDefault();
            if (null != rc)
            {
                var pourStandard = BLManagerFactory.Get().ConvertPourToUnits(rc.PourStandard);
                var variance = rc.StandardVariance * pourStandard;
                strip.MinLimit.AxisValueSerializable = "0.0";
                strip.MaxLimit.AxisValueSerializable = "0.1";

                strip.MaxLimit.AxisValueSerializable = (pourStandard + variance).ToString(CultureInfo.InvariantCulture);
                strip.MinLimit.AxisValueSerializable = (pourStandard - variance).ToString(CultureInfo.InvariantCulture);
            }
        }

        private void BuildConstantLine(IEnumerable<IBLStandardPour> data, ConstantLine line)
        {
            IBLStandardPour rc;
            rc = data.Where(p => p.Name.Equals(line.Name, StringComparison.InvariantCultureIgnoreCase)).
                FirstOrDefault();
            if (null != rc)
            {
                var pourStandard = BLManagerFactory.Get().ConvertPourToUnits(rc.PourStandard);
                line.AxisValueSerializable = pourStandard.ToString();
            }
        }

        private void LoadRootCategories( )
        {
            Log.Time( "Get Root Categories", LogType.Debug, true, ( ) =>
            {
                m_RootCategories =
                    BLManagerFactory.Get().ManageCategories().
                        GetRootCategories();
            } );

            m_MessageCount["Unknown"] = 0;
            foreach (var cat in m_RootCategories)
            {
                m_MessageCount[cat.Name] = 0;
            }
        }

        protected override void ProcessItem(CalcPour _item)
        {
            Log.Time( "CurrentPourWidget::ProcessItem()", LogType.Debug, false, () =>
            {
                var pourPoint = new PourPoint
                {
                    Volume = _item.PourAmount,
                    Category = _item.Category,
                    PourTime = _item.ReadTime,
                    TagID = _item.TagID,
                    TagNumber = _item.TagNumber,
                    UPCName = _item.UPCName,
                    Location = _item.Location,
                    Units = BLManagerFactory.Get().GetDefaultSizeType().Abbreviation
                };
                AddPourPoint(pourPoint);
            } );
        }


        private void AddPourPoint( DataPour _pour, IBLTag _tag, IBLCategory _category )
        {
            try
            {
                var pourPoint = new PourPoint
                {
                    Volume = _pour.Volume,
                    Category = ( null != _category ) ? _category.Name : string.Empty,
                    PourTime = _pour.PourTime,
                    TagID = _tag.TagID,
                    TagNumber = _tag.TagNumber,
                    UPCName = _tag.UPC.Name,
                    Location = _tag.CurrentLocation.Name,
                    Units = BLManagerFactory.Get().GetDefaultSizeType().Abbreviation
                };
                AddPourPoint(pourPoint);
            }
            catch (Exception err)
            {
                Log.Exception( err );
            }
        }

        private void AddPourPoint(IUIPourPoint _pourPoint)
        {
            Log.Time("AddPourPoint", LogType.Debug, false, () =>
            {
                if (null != _pourPoint)
                {
                    string catName = _pourPoint.Category;
                    if (_pourPoint.UPCName.Equals("UNKNOWN", StringComparison.CurrentCultureIgnoreCase))
                    {
                        catName = "Unknown";
                    }
                    Log.Debug(string.Format("TagID {2} UPC {0}, Pour {1} Location{3}", _pourPoint.UPCName, _pourPoint.Volume, _pourPoint.TagNumber, _pourPoint.Location));

                    var point = new SeriesPoint(m_MessageCount[catName]++, new object[] { _pourPoint.Volume });
                    string label = string.Format("{1}{0}{2:0.00} {4}{0}{3:T}{0}{5}", System.Environment.NewLine, _pourPoint.UPCName,
                                                    _pourPoint.Volume, _pourPoint.PourTime, _pourPoint.Units, _pourPoint.Location);
                    point.Tag = label;

                    if (PourChartMax < chartCurrentPours.Series[catName].Points.Count)
                    {
                        chartCurrentPours.Series[catName].Points.RemoveAt(0);
                    }
                    chartCurrentPours.Series[catName].Points.Add(point);
                }
            });
        }
        
        private void CheckedChanged( object _sender, EventArgs _e )
        {
            chartCurrentPours.Series["Wine"].Visible = chkWine.Checked;
            chartCurrentPours.Series["Beer"].Visible = chkBeer.Checked;
            chartCurrentPours.Series["Liquor"].Visible = chkLiquor.Checked;
            chartCurrentPours.Series["Unknown"].Visible = chkUnknown.Checked;

            chartCurrentPours.RefreshData( );
        }

        private void ChartCurrentPoursObjectHotTracked( object _sender, HotTrackEventArgs _e )
        {
            var point = _e.AdditionalObject as SeriesPoint;

            if( point != null )
            {
                var label = (string)point.Tag;
                if( !string.IsNullOrWhiteSpace( label ) )
                {
                    ttcTooltips.ShowHint( label );
                }
            }
            else
            {
                ttcTooltips.HideHint( );
            }
        }
        
        private void CurrentPours_Load( object _sender, EventArgs _e )
        {
            if( !DesignMode )
            {
                LoadRootCategories();
                LoadPreviousPours( );

//                m_Running = true;
//                m_Processor = Task.Factory.StartNew(AddNewPour);
            }
        }

        //private void CurrentPoursWidget_VisibleChanged(object sender, EventArgs e)
        //{
        //    if (this.Visible == false)
        //    {
        //        m_Running = false;
        //        if( null != m_Processor && m_Processor.Status == TaskStatus.Running )
        //        {
        //            m_Processor.Wait(250);
        //        }
        //    }
        //    else
        //    {
        //        m_Running = true;
        //        if (null == m_Processor || m_Processor.Status != TaskStatus.Running)
        //        {
        //            m_Processor = Task.Factory.StartNew(AddNewPour);
        //        }
        //    }
        //}
    }
}
