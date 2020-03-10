using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LearnHebrew.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult ChildLogin()
        {
            Models.messageModel message = new Models.messageModel();
            message.message = "";
            return View("~/Views/Login/ChildLogin.cshtml", message);
        }

        public ActionResult AdultLogin()
        {
            return View("~/Views/Login/AdultLogin.cshtml");
        }

        public ActionResult AdultRegistration()
        {
            return View("~/Views/Adult/AdultRegistration.cshtml");
        }
        public ActionResult SiteIndex()
        {
            Auxiliray.Session.ChildInSession = null;
            return View("~/Views/Home/index.cshtml");
        }
    }
}