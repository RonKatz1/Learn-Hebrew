using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LearnHebrew.Controllers
{
    public class ContentController : Controller
    {

        public ActionResult UplaodContents()
        {
            return View("~/Views/Adult/UploadContent.cshtml");
        }

    }
}