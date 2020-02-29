using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LearnHebrew.Controllers
{
    public class AdultController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult SaveAdult(FormCollection coll)
        {
            try
            {
                BLL.LearnHebrewEntities.Adult adult = new BLL.LearnHebrewEntities.Adult();

                var name = coll["AdultName"];
                var password = coll["AdultPassword"];

                if (!string.IsNullOrEmpty(name) && !string.IsNullOrEmpty(password))
                {
                    adult.Name = name;
                    adult.Password = password;
                }
                else
                {
                    return Content("fail");
                }

                var adultID = BLL.Services.AdultServices.Save(adult);

                if(adultID == 0)
                {
                    return Content("fail");
                }

                return View("~/Views/Login/AdultLogin.cshtml");
            }
            catch (Exception ex)
            {
                return Content("fail");
            }
        }
    }
}