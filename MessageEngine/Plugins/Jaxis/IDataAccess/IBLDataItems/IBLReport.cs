using System;
using System.Collections.Generic;

namespace Jaxis.Inventory.Data.IBLDataItems
{
    public interface IBLReport : IReport
    {
        Guid ReportID { get; set; }
        string Name { get; set; }
        string Description { get; set; }
        string Command { get; set; }
        string ReportFile { get; set; }
        bool DateBound { get; set; }
        bool Active { get; set; }

        IList<IBLReportParameter> Parameters { get; } 
    }

    public interface IBLReportParameter
    {
        Guid ReportParameterID { get; set; }
        Guid ReportID { get; set; }
        string Name { get; set; }
        string DataType { get; set; }
        string SQLName { get; set; }
        string DefaultValue { get; set; }
    }
}