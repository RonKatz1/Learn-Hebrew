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
            }
            catch(Exception ex)
            {
                return Content("fail");
            }
        }

        public ActionResult AdultLogin(FormCollection coll)
        {
            LearnHebrew.Models.AdultModel m = new Models.AdultModel();

            try
            {

                var name = coll["AdultName"];
                var password = coll["AdultPassword"];

                var adult = BLL.Services.AdultServices.LoadAdultByNameAndPassword(name, password);

                if (adult == null)
                {
                    return Content("fail");
                }

                m.Adult = adult;

                return View("~/Views/Adult/Index.cshtml", m);

            }
            catch (Exception ex)
            {
                return Content("fail");
            }
        }
    }
}