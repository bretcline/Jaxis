using System;
using System.Configuration;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraCharts;
using DevExpress.XtraReports.UI;
using Jaxis.Utilities.Database;

namespace BeverageManagement.Reports
{
    public partial class rptCategorySummaryChart : DevExpress.XtraReports.UI.XtraReport
    {
        public rptCategorySummaryChart( DateTime _startTime, DateTime _endTime, string _connectionString )
        {
            InitializeComponent();

            string connStr = _connectionString;
            var db = new SqlTool(connStr);

            using (var reader = db.ExecuteReader(string.Format("EXEC dbo.rptCategoryCosts '{0}', '{1}'", _startTime, _endTime)))
            {
                while (reader.Read())
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
