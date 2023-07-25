using System;
using System.Configuration;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace BeverageReports
{
    public partial class rptPourSummary : DevExpress.XtraReports.UI.XtraReport
    {
        public rptPourSummary(DateTime _startDate, DateTime _endDate)
        {
            InitializeComponent();
            rpttaPourSummary.Connection.ConnectionString =
                ConfigurationManager.ConnectionStrings["BeverageMonitor"].ConnectionString;
            this.rpttaPourSummary.Fill(this.dsPourSummaryByUPC1.rptPourSummary, _startDate, _endDate);
        }
    }
}
