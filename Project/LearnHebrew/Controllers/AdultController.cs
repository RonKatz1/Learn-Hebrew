using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LearnHebrew.Controllers
{
    public class AdultController : Controller
    {
        //private string ContentPhotoPath = "C:/Users/tal/Documents/GitHub/Learn-Hebrew/Project/Photos";
        //private string ContentVoicePath = "C:/Users/tal/Documents/GitHub/Learn-Hebrew/Project/Voice";

        private string ContentFilePath = "C:/Users/tal/Documents/GitHub/Learn-Hebrew/ContentFiles";

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

        public ActionResult UplaodContents()
        {
            Models.AdultModel m = new Models.AdultModel();

            if (Auxiliray.Session.AdultInSession == null)
                return Content("fail upload content page");

            m.Adult = Auxiliray.Session.AdultInSession;

            return View("~/Views/Adult/UploadContent.cshtml", m);
        }


        [HttpPost]
        public ActionResult SaveContent(FormCollection col, HttpPostedFileBase Photo, HttpPostedFileBase Voice)
        {

            BLL.LearnHebrewEntities.Content content = new BLL.LearnHebrewEntities.Content();

            try
            {
                var AdultID = int.Parse(col["AdultID"]);
                if (AdultID == 0)
                    return Content("fail sace content");

                var word = col["Word"];
                content.AdultID = AdultID;
                content.Word = word;

                if(Photo != null)
                {
                    content.Data.PhotoFile = CreateFile(Photo, word);

                    //var photo = new BLL.LearnHebrewEntities.ContentFile();
                    //photo.Code = Guid.NewGuid().ToString();
                    //photo.Name = word;

                    //string fileExtention = "";
                    //if (Photo.ContentLength > 0 && !string.IsNullOrEmpty(Photo.FileName))
                    //{
                    //    fileExtention = Path.GetExtension(Photo.FileName).Split('.')[Path.GetExtension(Photo.FileName).Split('.').Length - 1];
                    //    photo.Extention = fileExtention;
                    //}
                    //if (Photo.ContentLength > 0 && !string.IsNullOrEmpty(photo.Extention))
                    //{
                    //    // get a stream
                    //    var stream = Photo.InputStream;
                    //    // and optionally write the file to disk
                    //    //var fileName = Path.GetFileName(Photo.FileName);
                    //    var filename = word;
                    //    var path = Path.Combine(ContentPhotoPath, photo.Code + "." + photo.Extention);
                    //    using (var fileStream = System.IO.File.Create(path))
                    //    {
                    //        stream.CopyTo(fileStream);
                    //    }
                    //}  
                }

                if(Voice != null)
                {
                    content.Data.VoiceFile = CreateFile(Voice, word);
                }

                var contentID = BLL.Services.ContentServices.Save(content);

                if(contentID != 0)
                {
                    return Content("OK");
                }
                else
                {
                    return Content("fail");
                }

            }
            catch(Exception ex)
            {
                return Content(ex.Message + " -- " + ex.StackTrace);
            }
        }

        public ActionResult ConfirmContents()
        {

            Models.AdultModel m = new Models.AdultModel();

            try
            {
                m.Adult = Auxiliray.Session.AdultInSession;
                if (m.Adult == null)
                    return Content("fail");

                var allContents = BLL.Services.ContentServices.LoadAllContents();
                if(allContents != null && allContents.Count() > 0)
                {
                    m.Contents = allContents.Where(c=>c.AdultID != m.Adult.AdultID).ToList();
                    m.Path = ContentFilePath;
                }
                else
                {
                    m.Contents = new List<BLL.LearnHebrewEntities.Content>();
                    m.Path = "";
                }

                return View("~/Views/Adult/ConfirmContents.cshtml", m);

            }
            catch (Exception ex)
            {
                return Content(ex.Message + " -- " + ex.StackTrace);
            }
        }

        private BLL.LearnHebrewEntities.ContentFile CreateFile(HttpPostedFileBase file, string word)
        {
            var contentFile = new BLL.LearnHebrewEntities.ContentFile();
            contentFile.Code = Guid.NewGuid().ToString();
            contentFile.Name = word;

            string fileExtention = "";
            if (file.ContentLength > 0 && !string.IsNullOrEmpty(file.FileName))
            {
                fileExtention = Path.GetExtension(file.FileName).Split('.')[Path.GetExtension(file.FileName).Split('.').Length - 1];
                contentFile.Extention = fileExtention;
            }
            if (file.ContentLength > 0 && !string.IsNullOrEmpty(contentFile.Extention))
            {
                // get a stream
                var stream = file.InputStream;
                // and optionally write the file to disk
                //var fileName = Path.GetFileName(Photo.FileName);
                //var filename = word;
                var path = Path.Combine(ContentFilePath, contentFile.Code + "." + contentFile.Extention);
                using (var fileStream = System.IO.File.Create(path))
                {
                    stream.CopyTo(fileStream);
                }
            }
            return contentFile;
        }

        public ActionResult ConnectToChild()
        {
            Models.AdultModel m = new Models.AdultModel();
            m.Adult = Auxiliray.Session.AdultInSession;

            return View("~/Views/Adult/ConnectToChild.cshtml", m);
        }

        [HttpPost]
        public ActionResult SaveChildConnection(FormCollection col)
        {
            try
            {
                Models.AdultModel m = new Models.AdultModel();
                BLL.LearnHebrewEntities.Adult adult = new BLL.LearnHebrewEntities.Adult();
                if (Auxiliray.Session.AdultInSession != null)
                    adult = Auxiliray.Session.AdultInSession;
                else
                    return Content("fail");

                var childName = col["ChildName"];
                var childPassword = col["ChildPassword"];

                var child = BLL.Services.ChildServices.LoadChildByNameandPassword(childName, childPassword);

                if (child == null)
                    return Content("cant find child");

                if(adult.Data.ChildsIDs != null && adult.Data.ChildsIDs.Count() > 0)
                {
                    if (!adult.Data.ChildsIDs.Contains(child.ChildID))
                        adult.Data.ChildsIDs.Add(child.ChildID);
                }
                else
                {
                    adult.Data.ChildsIDs = new List<int>();
                    adult.Data.ChildsIDs.Add(child.ChildID);
                }

                int adultID = BLL.Services.AdultServices.Save(adult);

                if (adultID == 0)
                    return Content("fail to save");


                Auxiliray.Session.AdultInSession = adult;
                m.Adult = adult;

                return View("~/Views/Adult/Index.cshtml", m);

            }
            catch (Exception ex)
            {
                return Content("cant connect to child");
            }
        }
    }
}