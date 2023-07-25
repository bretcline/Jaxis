using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace BeverageManagement.Reports
{
    public partial class rptInventory : DevExpress.XtraReports.UI.XtraReport
    {
        public rptInventory(DateTime _startTime, DateTime _endTime, string _connectionString )
        {
            InitializeComponent();

            //rptInventoryAdapter.Connection.ConnectionString = _connectionString;
            //this.rptInventoryAdapter.Fill(this.dsInventory1.rptInventory);
        }
    }
}
