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
            Auxiliray.Session.AdultInSession = new BLL.LearnHebrewEntities.Adult();
            Auxiliray.Session.ChildInSession = new BLL.LearnHebrewEntities.Child();
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
            Auxiliray.Session.AdultInSession = new BLL.LearnHebrewEntities.Adult();
            Auxiliray.Session.ChildInSession = new BLL.LearnHebrewEntities.Child();
            return View("~/Views/Home/index.cshtml");
        }

        public ActionResult LogOut()
        {
            Session.Clear();
            Session.Abandon();
            Session.RemoveAll();
            return RedirectToAction("SiteIndex", "Home");
        }
    }
}