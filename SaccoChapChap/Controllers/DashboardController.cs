using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SaccoChapChap.Controllers
{
    public class DashboardController : Controller
    {
        //
        // GET: /Dashboard/
        [Authorize]
        public ActionResult Index()
        {
            ViewData["title"] = "Dashboard";
            return View();
        }

    }
}
