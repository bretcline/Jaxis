using System;
using System.Configuration;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace BeverageReports
{
    public partial class rptPourSummaryByTag : DevExpress.XtraReports.UI.XtraReport
    {
        public rptPourSummaryByTag(DateTime _startDate, DateTime _endDate)
        {
            InitializeComponent();
            rptPourSummaryByTagTableAdapter.Connection.ConnectionString =
                ConfigurationManager.ConnectionStrings["BeverageMonitor"].ConnectionString;
            this.rptPourSummaryByTagTableAdapter.Fill(this.bevMetMobileDataSet21.rptPourSummaryByTag, _startDate, _endDate);
        }
    }
}
