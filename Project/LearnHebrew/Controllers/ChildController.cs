using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LearnHebrew.Controllers
{
    public class ChildController : Controller
    {
        public ActionResult SaveChild(FormCollection coll)
        {
            try
            {
                BLL.LearnHebrewEntities.Child child = new BLL.LearnHebrewEntities.Child();

                var name = coll["ChildName"];
                var color = coll["ChildColor"];

                if (!string.IsNullOrEmpty(name) && !string.IsNullOrEmpty(color))
                {
                    child.Name = name;
                    child.Color = color;
                    child.Password = "1";

                    var childID = BLL.Services.ChildServices.Save(child);
                    child.Password = childID.ToString();
                }

                return Content("save successful");
            }
            catch (Exception ex)
            {
                return Content("fail");
            }
        }
    }
}