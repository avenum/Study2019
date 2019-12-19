using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Study2019.WebUI.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            ViewBag.Hello = "World";
            return View();
        }
        [Authorize]
        public ActionResult Test()
        {
            return View();
        }

    }
}