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

        //private string ContentFilePath = "C:/Users/tal/Documents/GitHub/Learn-Hebrew/Project/LearnHebrew/ContentFiles";
        private string ContentFilePath = "C:/Users/ron katz/Documents/GitHub/Learn-Hebrew/Project/LearnHebrew/ContentFiles";

        //public ActionResult Index()
        //{
        //    return View();
        //}

        public ActionResult Index(/*int AdultID*/)
        {
            Models.AdultModel m = new Models.AdultModel();

            if (Auxiliray.Session.AdultInSession != null)
                m.Adult = Auxiliray.Session.AdultInSession;
            else
                m.Adult = new BLL.LearnHebrewEntities.Adult();

            return View("~/Views/Adult/Index.cshtml", m);
        }

        public ActionResult SaveAdult(FormCollection coll)
        {
            try
            {
                BLL.LearnHebrewEntities.Adult adult = new BLL.LearnHebrewEntities.Adult();

                var name = coll["AdultName"];
                var password = coll["AdultPassword"];
                var isTeacher = coll["IsSignAsTeacher"] != null ? coll["IsSignAsTeacher"] : "false";

                if (!string.IsNullOrEmpty(name) && !string.IsNullOrEmpty(password))
                {
                    adult.Name = name;
                    adult.Password = password;
                    adult.Data.IsTeacher = isTeacher.Equals("false") ? false : true;
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
            Models.AdultModel m = new Models.AdultModel();
            BLL.LearnHebrewEntities.Content content = new BLL.LearnHebrewEntities.Content();

            try
            {
                var AdultID = int.Parse(col["AdultID"]);
                if (AdultID == 0)
                    return Content("fail sace content");

                var word = col["Word"];
                var UnDotedWord = col["UnDotedWord"];
                content.AdultID = AdultID;
                content.Word = word;
                content.Data.DateCreated = DateTime.Now;
                content.Data.UnDotedWord = UnDotedWord;
                if(Photo != null)
                {
                    content.Data.PhotoFile = CreateFile(Photo, word);
                }

                if(Voice != null)
                {
                    content.Data.VoiceFile = CreateFile(Voice, word);
                }

                var contentID = BLL.Services.ContentServices.Save(content);

                if(contentID != 0)
                {
                    //return Content("OK");
                    if (Auxiliray.Session.AdultInSession == null && Auxiliray.Session.AdultInSession.AdultID != AdultID)
                        return Content("fail upload content page");

                    m.Adult = Auxiliray.Session.AdultInSession;

                    return View("~/Views/Adult/UploadContent.cshtml", m);
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
                //get content that doesnt get 5 confirmed(IsApproved) or 3 unConfirmed(HideConeten)
                allContents = allContents != null && allContents.Count() > 0 ? allContents.Where(c => !c.Data.HideUnAprroverdContent && !c.Data.IsApproved).ToList() : new List<BLL.LearnHebrewEntities.Content>();


                if(allContents != null && allContents.Count() > 0)
                {
                    if (m.Adult.Data.ContentIDsConfermed == null || m.Adult.Data.ContentIDsConfermed.Count() <= 0)
                        m.Adult.Data.ContentIDsConfermed = new List<int>();

                    m.Contents = allContents.Where(c=>c.AdultID != m.Adult.AdultID && !c.Data.IsApproved && !m.Adult.Data.ContentIDsConfermed.Contains(c.ContentID)).ToList();
                    m.Contents = m.Contents != null && m.Contents.Count() > 0 ? m.Contents.OrderBy(c => c.Data.DateCreated).ToList() : new List<BLL.LearnHebrewEntities.Content>();
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

        [HttpPost]
        public ActionResult SaveConfirmedContent(int ContentID, bool IsConfirmed)
        {
            BLL.LearnHebrewEntities.Content content = new BLL.LearnHebrewEntities.Content();
            BLL.LearnHebrewEntities.Adult adult = new BLL.LearnHebrewEntities.Adult();
            try
            {
                content = BLL.Services.ContentServices.LoadByID(ContentID);
                adult = Auxiliray.Session.AdultInSession;

                if(content == null)
                    return Content("fail");
                if(adult == null)
                    return Content("fail");

                if (IsConfirmed)
                    content.Data.ApprovedCount += 1;
                else
                    content.Data.DisApprovedCount += 1;

                if (content.Data.ApprovedCount >= 5)
                    content.Data.IsApproved = true;

                if (content.Data.DisApprovedCount >= 3)
                {
                    //delete content from contents
                    //delete content from adult content list

                    content.Data.HideUnAprroverdContent = true;
                }


                if (adult.Data.ContentIDsConfermed == null || adult.Data.ContentIDsConfermed.Count() <= 0)
                    adult.Data.ContentIDsConfermed = new List<int>();

                adult.Data.ContentIDsConfermed.Add(ContentID);

                BLL.Services.AdultServices.Save(adult);
                BLL.Services.ContentServices.Save(content);

                Auxiliray.Session.AdultInSession = adult;

            }
            catch (Exception ex)
            {
                return Content("fail");
            }

            return Content("OK");
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
                    adult = BLL.Services.AdultServices.LoadAdultByID(Auxiliray.Session.AdultInSession.AdultID);
                else
                    return Content("fail");

                var childName = col["ChildName"];
                var childPassword = col["ChildPassword"];

                var child = BLL.Services.ChildServices.LoadChildByNameandPassword(childName, childPassword);

                if (child == null || child.ChildID == 0)
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

        public ActionResult ChildProgress()
        {
            Models.AdultModel m = new Models.AdultModel();
            m.Adult = Auxiliray.Session.AdultInSession;

            var adultChilds = new List<BLL.LearnHebrewEntities.Child>();
            if (m.Adult != null && m.Adult.Data != null && m.Adult.Data.ChildsIDs != null && m.Adult.Data.ChildsIDs.Count() > 0)
                adultChilds = BLL.Services.ChildServices.LoadAllChildsByIds(m.Adult.Data.ChildsIDs);

            m.AdultChilds = adultChilds;

            return View("~/Views/Adult/ChildProgress.cshtml", m);
        }

        public ActionResult ShowChildProgress(int ChildID, string letterFilltered = "הכל", int OrderBy = 0, bool IsAsc = true)
        {
            Models.ChildProgressModel m = new Models.ChildProgressModel();

            try
            {
                var progresses = BLL.Services.ChildProgressServices.LoadAllChildProgressesByChildID(ChildID);

                if(!letterFilltered.Equals("הכל") && progresses != null && progresses.Count() > 0)
                {
                    progresses = progresses.Where(p => p.Data.Letter.ToString().Equals(letterFilltered)).ToList();
                }

                switch(OrderBy)
                {
                    case 0:
                        if(IsAsc)
                            m.ChildProgresses = progresses != null && progresses.Count() > 0 ? progresses.Where(p => p.Data.EndDate < DateTime.MaxValue).OrderBy(p => p.Data.Date).ToList() : new List<BLL.LearnHebrewEntities.ChildProgress>();
                        else
                            m.ChildProgresses = progresses != null && progresses.Count() > 0 ? progresses.Where(p => p.Data.EndDate < DateTime.MaxValue).OrderByDescending(p => p.Data.Date).ToList() : new List<BLL.LearnHebrewEntities.ChildProgress>();
                        break;
                    case 1:
                        if(IsAsc)
                            m.ChildProgresses = progresses != null && progresses.Count() > 0 ? progresses.Where(p => p.Data.EndDate < DateTime.MaxValue).OrderBy(p => p.Data.Letter).ToList() : new List<BLL.LearnHebrewEntities.ChildProgress>();
                        else
                            m.ChildProgresses = progresses != null && progresses.Count() > 0 ? progresses.Where(p => p.Data.EndDate < DateTime.MaxValue).OrderByDescending(p => p.Data.Letter).ToList() : new List<BLL.LearnHebrewEntities.ChildProgress>();
                        break;
                    default:
                        m.ChildProgresses = progresses != null && progresses.Count() > 0 ? progresses.Where(p => p.Data.EndDate < DateTime.MaxValue).OrderByDescending(p => p.Data.Date).ToList() : new List<BLL.LearnHebrewEntities.ChildProgress>();
                        break;
                }
                //m.ChildProgresses = progresses != null && progresses.Count() > 0 ? progresses.Where(p=>p.Data.EndDate < DateTime.MaxValue).OrderByDescending(p=>p.Data.Date).ToList() : new List<BLL.LearnHebrewEntities.ChildProgress>();
                m.ChildID = ChildID;
                LearnHebrew.Auxiliray.Session.LetterForPrograssFillter = letterFilltered;

                m.LastIsAsc = IsAsc;
                m.LastOrderBy = OrderBy;
            }
            catch(Exception ex)
            {

            }

            return View("~/Views/Adult/_ChildProgressInfo.cshtml", m);
        }

        public ActionResult CreateUnseen()
        {

            Models.UnseenModel m = new Models.UnseenModel();

            if (Auxiliray.Session.AdultInSession == null)
                return Content("fail");

            BLL.LearnHebrewEntities.Unseen unseen = new BLL.LearnHebrewEntities.Unseen();

            unseen.AdultID = Auxiliray.Session.AdultInSession.AdultID;

            m.Adult = Auxiliray.Session.AdultInSession;

            return View("~/Views/Adult/CreateUnseen.cshtml", m);

        }

        public ActionResult ShowExistsUnseen()
        {
            return View();
        }
    }
}