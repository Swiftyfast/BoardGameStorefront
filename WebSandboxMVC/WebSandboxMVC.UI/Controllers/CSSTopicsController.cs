using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebSandboxMVC.UI.Controllers
{
    public class CSSTopicsController : Controller
    {
        // GET: CSSTopics
        public ActionResult Index()
        {
            return RedirectToAction("Colors");
        }

        public ActionResult Colors()
        {
            return View();
        }

        public ActionResult Fonts()
        {
            return View();
        }

        public ActionResult Images()
        {
            return View();
        }
    }
}