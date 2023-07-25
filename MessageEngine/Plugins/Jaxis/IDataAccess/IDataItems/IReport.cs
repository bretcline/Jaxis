using System;
using System.Collections.Generic;

namespace Jaxis.Inventory.Data
{
    public interface IReport : IDataObject<IReport>
    {
        Guid ReportID { get; set; }
        string Name { get; set; }
        string Description { get; set; }
        string Command { get; set; }
        string ReportFile { get; set; }

        IList<IReportParameter> Parameters { get; }
    }
}