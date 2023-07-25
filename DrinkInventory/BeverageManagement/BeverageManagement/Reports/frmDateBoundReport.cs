using System;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BeverageManagement.Reports;
using BeverageReports;
using DevExpress.CodeParser.Diagnostics;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraPrinting;
using DevExpress.XtraReports.UI;
using Jaxis.Inventory.Data;
using Jaxis.Inventory.Data.IBLDataItems;
using Jaxis.Inventory.Data.Reports;
using Jaxis.Utilities.Database;
using Jaxis.Util.Log4Net;
using Log = Jaxis.Util.Log4Net.Log;

namespace BeverageManagement
{
    public enum ReportTypes
    {
        DateBound,
        PourSummaryByUPC,
        PourSummaryByTag,
        AlertSummary,
        SummaryReport,
        REPX,
    }

    public partial class frmDateBoundReport : DevExpress.XtraEditors.XtraForm
    {
        public frmDateBoundReport( )
        {
            InitializeComponent( );
        }

        public IBLReport ReportItem { get; set; }
        public ReportTypes ReportType { get; set; }

        private void frmXtraReport_Load( object sender, EventArgs e )
        {
            btnPrint.Visible = false;

            rdoDates.SelectedIndex = 0;
            dteStartTime.DateTime = DateTime.Today.AddHours(4);
            dteEndTime.DateTime = DateTime.Today.AddDays(1).AddHours(4);
            dteEndTime.Enabled = dteStartTime.Enabled = false;
            switch (ReportType)
            {
                case ReportTypes.PourSummaryByTag:
                {
                    break;
                }
                case ReportTypes.PourSummaryByUPC:
                {
                    break;
                }
                case ReportTypes.AlertSummary:
                {
                    break;
                }
            }
        }
        
        private void btnQuery_Click(object sender, EventArgs e)
        {
            XtraReport rpt = null;
            try
            {
                Cursor = Cursors.WaitCursor;
                var startTime = dteStartTime.DateTime;
                var endTime = dteEndTime.DateTime;
                switch (ReportType)
                {
                    case ReportTypes.REPX:
                    {
                        var report = new XtraReport();
                        report.LoadLayout( "" );
                        break;
                    }
                    case ReportTypes.PourSummaryByTag:
                    {
                        rpt = new rptPourSummaryByTag(startTime, endTime);
                        break;
                    }
                    case ReportTypes.PourSummaryByUPC:
                    {
                        rpt = new rptPourSummary(startTime, endTime);
                        break;
                    }
                    case ReportTypes.AlertSummary:
                    {
                        rpt = new rptAlerts(startTime, endTime);
                        break;
                    }
                    case ReportTypes.SummaryReport:
                    {
                        rpt = new rptSummary(startTime, endTime);
                        break;
                    }
                    default:
                    {
                        btnPrint.Visible = true;
                        var query = ReportItem.Command;

                        var data = ProcessCommand(ReportItem, startTime, endTime);

                        grdReportData.DataSource = data;

                        var mainView = grdReportData.MainView as GridView;
                        if (null != mainView)
                        {
                            foreach (GridColumn column in mainView.Columns)
                            {
                                if (column.ColumnType == typeof(DateTime))
                                {
                                    column.DisplayFormat.FormatString = "MM/dd/yyyy hh:mm:ss";
                                }
                                else if (column.ColumnType == typeof(Decimal))
                                {
                                    column.DisplayFormat.FormatString = "c2";
                                }
                            }
                        }

                        break;
                    }
                }
            }
            catch (Exception err)
            {
                Log.WriteException("Reporting Error", err);
            }
            finally
            {
                Cursor = Cursors.Default;
            }
            if( null != rpt )
            {
                rpt.ShowPreview();
            }
        }

        private void btnClose_Click( object sender, EventArgs e )
        {
            this.Close();
        }

        private void rdoDates_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (rdoDates.SelectedIndex)
            {
                case 0:
                {
                    dteStartTime.DateTime = DateTime.Today.AddHours(4);
                    dteEndTime.DateTime = DateTime.Today.AddDays(1).AddHours(4);
                    dteEndTime.Enabled = dteStartTime.Enabled = false;
                    break;
                }
                case 1:
                {
                    dteStartTime.DateTime = DateTime.Today.AddDays(-1).AddHours(4);
                    dteEndTime.DateTime = DateTime.Today.AddHours(4);
                    dteEndTime.Enabled = dteStartTime.Enabled = false;
                    break;
                }
                case 2:
                {
                    dteEndTime.Enabled = dteStartTime.Enabled = true;
                    break;
                }
            }
        }


        private DataTable ProcessCommand(IBLReport ReportItem, DateTime _start, DateTime _end)
        {
            var rc = new DataSet();
            try
            {
                string connectionString = ConfigurationManager.ConnectionStrings["BeverageMonitor"].ConnectionString;

                using (var DBConn = new SqlTool(connectionString))
                {
                    var param = new SqlParameterList();
                    param.AddInParameter("@StartTime", _start);
                    param.AddInParameter("@EndTime", _end);
                    DBConn.ExecuteProc( ReportItem.Command, param, ref rc, "ReportData" );
                }
            }
            catch (Exception err)
            {
                Log.WriteException("frmGridReport::ProcessCommand", err);
            }
            return rc.Tables["ReportData"];
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            ReportFomat.Format(grdReportData, printingSystem1, ReportItem, string.Format( "From {0} to {1}", dteStartTime.DateTime, dteEndTime.DateTime ) );
        }
    }
}