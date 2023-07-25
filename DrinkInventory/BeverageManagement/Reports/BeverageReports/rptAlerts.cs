using System;
using System.Configuration;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace BeverageReports
{
    public partial class rptAlerts : DevExpress.XtraReports.UI.XtraReport
    {
        public rptAlerts( DateTime _startDate, DateTime _endDate )
        {
            InitializeComponent();
            rptAlertsTableAdapter.Connection.ConnectionString =
                ConfigurationManager.ConnectionStrings["BeverageMonitor"].ConnectionString;
            this.rptAlertsTableAdapter.Fill(this.dsAlerts1.rptAlerts, _startDate, _endDate);
        }

    }
}
