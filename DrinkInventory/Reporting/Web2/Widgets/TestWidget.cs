using System;

namespace Jaxis.DrinkInventory.Reporting.Web2.Widgets
{
    public class TestWidget : IWidget
    {
        public Guid Id { get; set; }
        public string Name { get; set; }

        public string ViewName
        {
            get { return "_TestWidget"; }
        }

        public void UpdateData(Guid _sessionId)
        {
        }

        public string Message
        {
            get { return "This is a test widget."; }   
        }

        //public IWidgetModel CreateViewModel(Guid _sessionId)
        //{
        //    return new TestWidgetModel {Id = Id, Name = Name};
        //}
    }
}
