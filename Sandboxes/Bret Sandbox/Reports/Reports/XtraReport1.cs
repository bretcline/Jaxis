using System;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraCharts;
using DevExpress.XtraReports.UI;
using Jaxis.Utilities.Database;

namespace Reports
{
    public partial class XtraReport1 : DevExpress.XtraReports.UI.XtraReport
    {
        public XtraReport1()
        {
            InitializeComponent();

            string connStr = ConfigurationManager.ConnectionStrings["BeverageMonitor"].ConnectionString;
            var db = new SqlTool(connStr);

            using (var reader = db.ExecuteReader(string.Format("EXEC dbo.rptCategoryCosts '{0}', '{1}'", "2011-11-13 04:00:00.000", "2011-11-14 04:00:00.000")))
            {
                while( reader.Read() )
                {
                    var category = reader.GetString(reader.GetOrdinal("Category"));
                    var pourCosts = reader.GetDouble(reader.GetOrdinal("PourCosts"));
                    xrCosts.Series["Cost"].Points.Add(new SeriesPoint(category, pourCosts));

                    var idealCosts = reader.GetDouble(reader.GetOrdinal("IdealCost"));
                    xrCosts.Series["Ideal Cost"].Points.Add(new SeriesPoint(category, idealCosts));

                    var totalSales = reader.GetDecimal(reader.GetOrdinal("TotalSales"));
                    xrCosts.Series["Total Sales"].Points.Add(new SeriesPoint(category, totalSales));
                }
            }
        }

    }
}
