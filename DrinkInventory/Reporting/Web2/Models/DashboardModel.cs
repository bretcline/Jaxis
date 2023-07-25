using System.Collections.Generic;
using Jaxis.DrinkInventory.Reporting.Web2.Widgets;

namespace Jaxis.DrinkInventory.Reporting.Web2.Models
{
    public class DashboardModel
    {
        public List<IWidget> VisibleWidgets { get; set; }
        public List<IWidget> HiddenWidgets { get; set; }
    }
}
