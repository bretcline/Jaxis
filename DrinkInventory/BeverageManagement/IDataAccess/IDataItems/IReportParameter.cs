using System;

namespace Jaxis.Inventory.Data
{
    public interface IReportParameter : IDataObject<IReportParameter>
    {
        Guid ReportParameterID { get; set; }
        Guid ReportID { get; set; }
        string Name { get; set; }
        string DataType { get; set; }
        string SQLName { get; set; }
        string DefaultValue { get; set; }
        
    }
}