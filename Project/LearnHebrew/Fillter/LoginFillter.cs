using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace LearnHebrew.Fillter
{
    public class LoginFillter : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (LearnHebrew.Auxiliray.Session.AdultInSession == null && LearnHebrew.Auxiliray.Session.ChildInSession == null)
            {
                //if (filterContext.HttpContext.Request.HttpMethod.Equals("POST"))
                //{
                //    filterContext.Result = new RedirectResult("~/Home/SiteIndex");
                //}
                //else
                //{
                //    filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary { { "controller", "Home" }, { "action", "SiteIndex" } });
                //}

                filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary { { "controller", "Home" }, { "action", "SiteIndex" } });

            }
            base.OnActionExecuting(filterContext);
        }
    }
}
