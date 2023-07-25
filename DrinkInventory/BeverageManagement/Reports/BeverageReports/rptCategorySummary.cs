using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace BeverageManagement.Reports
{
    public partial class rptCategorySummary : DevExpress.XtraReports.UI.XtraReport
    {
        public rptCategorySummary(DateTime _startTime, DateTime _endTime, string _connectionString )
        {
            InitializeComponent();

            rptSummaryByGroupAdapter.Connection.ConnectionString = _connectionString;
            this.rptSummaryByGroupAdapter.Fill(this.dsSummary1.rptSummaryByGroup);

        }

    }
}
