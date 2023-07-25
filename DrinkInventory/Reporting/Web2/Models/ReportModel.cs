using System;
using System.Collections.Generic;

namespace Jaxis.DrinkInventory.Reporting.Web2.Models
{
    public class ReportModel
    {
        public Guid SelectedReportId { get; set; }
        public List<Data.POCO.Report> Reports { get; set; }
        public ReportViewModel ReportViewModel { get; set; }
    }
}
