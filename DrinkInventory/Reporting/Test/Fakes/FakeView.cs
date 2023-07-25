using System;
using System.Data;
using Jaxis.DrinkInventory.Reporting.DataInterfaces;

namespace Jaxis.DrinkInventory.Reporting.Test.Fakes
{
    public class FakeView : FakeDomainObject, IReport
    {
        public FakeView()
        {
            ReportId = Guid.NewGuid();
        }

        public Guid ReportId { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public string ShortName { get; set; }
        public Guid SectionId { get; set; }
        public int Order { get; set; }
        public string SelectCommand { get; set; }

        public DataTable GetData(object[] _parameterValues)
        {
            throw new NotImplementedException();
        }

        public string ReportClassName { get; set; }

        public DataTable GetData(Guid _sessionId)
        {
            return new DataTable();
        }

        public override Guid Id
        {
            get { return ReportId; }
            set { ReportId = value; }
        }
    }
}
