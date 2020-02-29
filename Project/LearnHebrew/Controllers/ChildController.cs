using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace LearnHebrew.Controllers
{
    public class ChildController : Controller
    {
        public int SaveChild(string Name, string Color)
        {
            try
            {
                BLL.LearnHebrewEntities.Child child = new BLL.LearnHebrewEntities.Child();
                var name = Name;
                var color = Color;
                if (name!=null && color!=null)
                {
                    child.Name = name;
                    child.Color = color;
                    child.Password = "1";

                    var childID = BLL.Services.ChildServices.Save(child);
                    child.Password = childID.ToString();
                    return int.Parse(child.Password);
                }
                else
                {
                    return -1;
                }
                
            }
            catch (Exception ex)
            {
                return -1;
            }
        }

        public ActionResult UserPage()
        {
            return View("~/Views/User/ChildUser.cshtml");
        }
        public ActionResult LoginPage()
        {
            Models.messageModel message = new Models.messageModel();
            message.message = "";
            return View("~/Views/Login/ChildLogin.cshtml", message);
        }
        public ActionResult ChildLetters()
        {
            return View("~/Views/User/Letters.cshtml");
        }
        public ActionResult RegistrationPage()
        {
            return View("~/Views/Registration/ChildRegistration.cshtml");
        }
        public ActionResult ChildQuery(FormCollection coll)
        {
            
            Models.childModel m = new Models.childModel();
            Models.messageModel message = new Models.messageModel();
            try
            {
                var name = coll["childName"];
                var password = coll["childPassword"];
                BLL.LearnHebrewEntities.Child child = BLL.Services.ChildServices.LoadChildByNameandPassword(name, password);
                if (child != null && child.ChildID != 0)
                {
                    
                    m.child = child;
                    return View("~/Views/User/ChildUser.cshtml", m);
                }
                else
                {
                    message.message = "טעות";
                    return View("~/Views/Login/ChildLogin.cshtml", message);
                }

            }
            catch (Exception ex)
            {
                message.message = "טעות";
                return View("~/Views/Login/ChildLogin.cshtml", message);
            }

        
        }
        public ActionResult  LoadChildGuest()
        {
            Models.childModel m = new Models.childModel();
            try
            {
                BLL.LearnHebrewEntities.Child child = new BLL.LearnHebrewEntities.Child();
                child.ChildID = -1;
                child.Name = "אורח";
                child.Password = "-1";
                child.Data = null;
                m.child = child;
                return View("~/Views/User/ChildUser.cshtml", m);
            }
            catch (Exception ex) { return Content("error"); }
        }
    }
}