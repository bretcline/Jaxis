using System;
using System.Collections.Generic;
using DevExpress.XtraReports.UI;

namespace Jaxis.DrinkInventory.Reporting.Web2.Models
{
    public class ReportViewModel
    {
        public Guid ReportId { get; set; }
        public string ReportName { get; set; }
        public string ShortName { get; set; }
        public List<Data.POCO.Parameter> Parameters { get; set; }
        public XtraReport XtraReport { get; set; }

    }
}
