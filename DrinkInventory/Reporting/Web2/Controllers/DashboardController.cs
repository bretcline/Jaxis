using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Jaxis.DrinkInventory.Reporting.Web2.Infrastructure;
using Jaxis.DrinkInventory.Reporting.Web2.Models;
using Jaxis.DrinkInventory.Reporting.Web2.ReportingService;
using Jaxis.DrinkInventory.Reporting.Web2.Widgets;

namespace Jaxis.DrinkInventory.Reporting.Web2.Controllers
{
    public class DashboardController : BaseController
    {
        [UserSessionFilter] [HttpGet] public ActionResult Index()
        {
            return HandleAction(() =>
            {
                var model = new DashboardModel {VisibleWidgets = new List<IWidget>()};
                var allWidgets = WidgetFactory.All.ToList();
                var result = (GetVisibleWidgetIdsResult) ServiceFactory.Reporting.
                    WithService(_s => _s.GetVisibleWidgetIds(SessionId));

                foreach (var id in result.WidgetIds)
                {
                    var widget = (from w in allWidgets where w.Id == id select w).FirstOrDefault();
                    if (widget != null)
                    {
                        model.VisibleWidgets.Add(widget);
                        allWidgets.Remove(widget);
                    }
                }

                model.HiddenWidgets = allWidgets;
                return View(model);

            }, View());
        }

        public ActionResult GetWidgetContent(Guid _widgetId)
        {
            return HandleAction(() =>
            {
                var widget = WidgetFactory.GetWidget(_widgetId);
                widget.UpdateData(SessionId);
                return PartialView(widget.ViewName, widget);
            }, Content("Could not get content for widget.  Please try refreshing the page"));
        }

        public ActionResult SaveWidgetInfo(string _visibleWidgetIds)
        {
            return HandleAction(() =>
            {
                if (_visibleWidgetIds != null)
                {
                    var widgetIds = 
                        (from id in _visibleWidgetIds.Split(',')
                        where !string.IsNullOrWhiteSpace(id)
                        select Guid.Parse(id)).ToList();

                    ServiceFactory.Reporting.WithService(_s =>
                        _s.SetVisibleWidgetIds(SessionId, widgetIds));
                }

                return new EmptyResult();
            });
        }
    }
}
