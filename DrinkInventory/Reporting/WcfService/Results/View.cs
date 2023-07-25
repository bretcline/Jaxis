using System;
using System.Runtime.Serialization;
using Jaxis.DrinkInventory.Reporting.DataInterfaces;

namespace Jaxis.DrinkInventory.Reporting.WcfService.Results
{
    [DataContract]
    public class View
    {
        public View()
        {
        }

        public View(IView _view)
        {
            ViewId = _view.ViewId;
            Name = _view.Name;
            Type = _view.Type;
            ShortName = _view.ShortName;
            ReportClassName = _view.ReportClassName;
        }

        [DataMember]
        protected string ReportClassName { get; set; }

        [DataMember]
        public Guid ViewId { get; set; }

        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public string Type { get; set; }

        [DataMember]
        public string ShortName { get; set; }
    }
}
