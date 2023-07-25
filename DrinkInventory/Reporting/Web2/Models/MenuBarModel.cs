using System.Collections.Generic;
using System.Web.Mvc;
using Jaxis.DrinkInventory.Reporting.Web2.Infrastructure;

namespace Jaxis.DrinkInventory.Reporting.Web2.Models
{
    public class MenuBarModel
    {
        public MvcHtmlString Render(HtmlHelper _htmlHelper)
        {
            var urlHelper = new UrlHelper(_htmlHelper.ViewContext.RequestContext);
            var currentController = _htmlHelper.ViewContext.RouteData.GetRequiredString("controller");
            var menuBar = new Div().Class("menuBar");

            foreach (var item in Items)
            {
                var href = urlHelper.RouteUrl(new { controller = item.ControllerName, action = "Index" });
                var a = new A().Href(href).InnerHtml(item.DisplayText);
                var selected = string.Compare(currentController, item.ControllerName, true) == 0;
                a.Class(selected ? "selected" : "normal");
                menuBar.AppendInnerHtml(a);
            }

            return menuBar.ToHtml();
        }

        public List<TabItem> Items { get; set; }
    }
}
