using System;
using System.Configuration;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace BeverageManagement.Reports
{
    public partial class rptSummary : DevExpress.XtraReports.UI.XtraReport
    {
        public rptSummary(DateTime _startTime, DateTime _endTime)
        {
            InitializeComponent();

            var ConnectionString = ConfigurationManager.ConnectionStrings["BeverageMonitor"].ConnectionString;

            xrSubreport1.ReportSource = new rptCategorySummary( _startTime, _endTime, ConnectionString );
            xrSubreport2.ReportSource = new rptInventory(_startTime, _endTime, ConnectionString);
            xrSubreport3.ReportSource = new rptCategorySummaryChart(_startTime, _endTime, ConnectionString);
        }

    }
}
