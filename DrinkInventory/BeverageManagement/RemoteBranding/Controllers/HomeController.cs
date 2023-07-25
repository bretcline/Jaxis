using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RemoteBranding.Models;

namespace RemoteBranding.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Message = "Remote Branding Station";

            var bottle = new BrandedBottle();

            return View( bottle );
        }

        [HttpPost]
        public ActionResult Index( BrandedBottle model )
        {

            return RedirectToAction("Index");
        }
    }
}
