using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LearnHebrew.Controllers
{
    public class LoginController : Controller
    {

        public ActionResult SaveChild(FormCollection coll)
        {
            try
            {
                BLL.LearnHebrewEntities.Child child = new BLL.LearnHebrewEntities.Child();

                var name = coll["ChildName"];
                var color = coll["ChildColor"];

                if(!string.IsNullOrEmpty(name) && !string.IsNullOrEmpty(color))
                {
                    child.Name = name;
                    child.Color = color;
                    child.Password = "1";

                    var childID = BLL.Services.ChildServices.Save(child);
                    child.Password = childID.ToString();
                }

                return Content("save successful");
                //return View("ChildPassword",child.Password);
            }
            catch(Exception ex)
            {
                return Content("fail");
            }
        }

        public ActionResult AdultLogin(FormCollection coll)
        {
            LearnHebrew.Models.AdultModel m = new Models.AdultModel();
            var adult = new BLL.LearnHebrewEntities.Adult();

            try
            {
                var name = coll["AdultName"];
                var password = coll["AdultPassword"];

                //if (Auxiliray.Session.AdultInSession != null && Auxiliray.Session.AdultInSession.Name == name && Auxiliray.Session.AdultInSession.Password == password)
                //{
                //    m.Adult = Auxiliray.Session.AdultInSession;
                //    return View("~/Views/Adult/Index.cshtml", m);
                //}
                //else
                //{
                //    adult = BLL.Services.AdultServices.LoadAdultByNameAndPassword(name, password);
                //}

                adult = BLL.Services.AdultServices.LoadAdultByNameAndPassword(name, password);


                if (adult == null || adult.AdultID == 0)
                {
                    Models.messageModel message = new Models.messageModel();
                    message.message = "פרטי התחברות שגויים";
                    return View("~/Views/Login/AdultLogin.cshtml", message);
                }

                m.Adult = adult;
                Auxiliray.Session.AdultInSession = adult;
                return View("~/Views/Adult/Index.cshtml", m);

            }
            catch (Exception ex)
            {
                Models.messageModel message = new Models.messageModel();
                message.message = "";
                return View("~/Views/Login/AdultLogin.cshtml", message);
            }
        }

        public ActionResult Logout()
        {
            Auxiliray.Session.AdultInSession = null;
            Auxiliray.Session.ChildInSession = null;
            return View("~/Views/Home/Index.cshtml");
        }
    }
}