using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace IntroToMVC.UI.MVC.Controllers
{
    //This is just a C# class. It gets instantiated when its functionality is called from a View.
    public class HomeController : Controller
    {
        //This is a special method called an Action
        public ActionResult Index()
        {
            return View();
        }

        //We removed the About action that we got by default with MVC because we aren't going to use it.
        //We replaced it with an action and view called IntroToRazor:

        public ActionResult IntroToRazor()
        {
            //By default this method returns a view with the same name as this action:
            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}