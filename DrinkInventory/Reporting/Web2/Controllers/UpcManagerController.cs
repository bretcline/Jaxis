using System;
using System.Linq;
using System.Web.Mvc;
using Jaxis.DrinkInventory.Reporting.Data.POCO;
using Jaxis.DrinkInventory.Reporting.Web2.Infrastructure;
using Jaxis.DrinkInventory.Reporting.Web2.ReportingService;

namespace Jaxis.DrinkInventory.Reporting.Web2.Controllers
{
    public class UpcManagerController : BaseController
    {
        [UserSessionFilter] [HttpGet] public ActionResult Index()
        {
            return View();
        }

        public JsonResult GetUpcs(string _filter, bool _showValidated)
        {
            return HandleAjaxGetAction(() =>
            {
                var upcsResult = (GetUpcsResult)ServiceFactory.Reporting.WithService(
                    _svc => _svc.GetUpcs(SessionId, _filter, _showValidated));
                return from u in upcsResult.Upcs select new { u.UPCId, u.Name };
            });
        }

        public JsonResult GetUpc(Guid _upcId)
        {
            return HandleAjaxGetAction(() =>
            {
                var result = (GetUpcResult)ServiceFactory.Reporting.WithService(_svc => _svc.GetUpc(SessionId, _upcId));
                return result.Upc;
            });
        }

        public JsonResult NewUpc()
        {
            return HandleAjaxGetAction(() =>
            {
                var result = (GetUpcResult)ServiceFactory.Reporting.WithService(_svc => _svc.NewUpc(SessionId));
                return result.Upc;
            });
        }

        public JsonResult DeleteUpc(Guid _upcId)
        {
            return HandleAjaxPostAction(() =>
            {
                ServiceFactory.Reporting.WithService(_svc => _svc.DeleteUpc(SessionId, _upcId));
                return null;
            }, "UPC Deleted");
        }

        public JsonResult SaveUpc(UPC _upc)
        {
            return HandleAjaxPostAction(() =>
            {
                var saveResult = (GetUpcResult)ServiceFactory.Reporting.WithService(_svc => _svc.SaveUpc(SessionId, _upc));
                return saveResult.Upc;
            }, "UPC Saved");
        }
    }
}
