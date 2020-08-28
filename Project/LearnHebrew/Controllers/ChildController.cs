using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
using LearnHebrew.DTO;

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
            return View("~/Views/Child/Index.cshtml");
        }
        public ActionResult LoginPage()
        {
            Models.messageModel message = new Models.messageModel();
            message.message = "";
            return View("~/Views/Login/ChildLogin.cshtml", message);
        }
        public ActionResult ChildLetters()
        {
            return View("~/Views/Child/Alphabet.cshtml");
        }
        public ActionResult RegistrationPage()
        {
            return View("~/Views/Child/ChildRegistration.cshtml");
        }
        public ActionResult IndexPage(FormCollection coll)
        {
            
            Models.childModel m = new Models.childModel();
            Models.messageModel message = new Models.messageModel();
            try
            {
                var name = coll["childName"];
                var password = coll["childPassword"];
                BLL.LearnHebrewEntities.Child child = new BLL.LearnHebrewEntities.Child();
                if (Auxiliray.Session.ChildInSession!=null && Auxiliray.Session.ChildInSession.ChildID == int.Parse(password))
                {
                    child = Auxiliray.Session.ChildInSession;

                }
                else
                {
                    child = BLL.Services.ChildServices.LoadChildByNameandPassword(name, password);

                }

                if (child != null && child.ChildID != 0)
                {
                    Auxiliray.Session.ChildInSession = child;
                    m.child = child;
                    return View("~/Views/Child/Index.cshtml", m);
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
                Auxiliray.Session.ChildInSession = child;
                m.child = child;
                return View("~/Views/Child/Index.cshtml", m);
            }
            catch (Exception ex) { return Content("error"); }
        }
        public ActionResult ReturnToIndex()
        {
            Models.childModel m = new Models.childModel();
            BLL.LearnHebrewEntities.Child child = new BLL.LearnHebrewEntities.Child();
            child = Auxiliray.Session.ChildInSession ;
            m.child = child;
            return View("~/Views/Child/Index.cshtml", m);
        }
        public ActionResult ReturnToAlphabet()
        {
            
            return View("~/Views/Child/Alphabet.cshtml");
        }
        public ActionResult HelpPage()
        {
            return View("~/Views/Child/Help.cshtml");
        }
        public ActionResult GoToGame_22()
        {
            return View("~/Views/Game/Game_22.cshtml");
        }
        public ActionResult GoToGame_21()
        {
            return View("~/Views/Game/Game_21.cshtml");
        }
        public ActionResult GoToGame_20()
        {
            return View("~/Views/Game/Game_20.cshtml");
        }
        public ActionResult GoToGame_19()
        {
            return View("~/Views/Game/Game_19.cshtml");
        }
        public ActionResult GoToGame_17()
        {
            return View("~/Views/Game/Game_17.cshtml");
        }
        public ActionResult GoToGame_16()
        {
            return View("~/Views/Game/Game_16.cshtml");
        }
        public ActionResult GoToGame_13()
        {
            return View("~/Views/Game/Game_13.cshtml");
        }
        public ActionResult GoToGame_12()
        {
            return View("~/Views/Game/Game_12.cshtml");
        }
        public ActionResult GoToGame_9()
        {
            return View("~/Views/Game/Game_9.cshtml");
        }
        public ActionResult GoToGame_8()
        {
            return View("~/Views/Game/Game_8.cshtml");
        }
        public ActionResult GoToGame_6()
        {
            return View("~/Views/Game/Game_6.cshtml");
        }
        public ActionResult GoToGame_5()
        {
            return View("~/Views/Game/Game_5.cshtml");
        }
        public ActionResult GoToGame_1()
        {
            return View("~/Views/Game/Game_1.cshtml");
        }
        public ActionResult GoToGame_7_Hangman()
        {
            return View("~/Views/Game/Game_7_Hangman.cshtml");
        }

        [HttpGet]
        [Route("Child/GetGameContent/{Letter}")]
        public JsonResult GetGameContent(string Letter)
        {
            //Console.WriteLine(Request.Url);
            //string decoded = HttpUtility.UrlDecode(Letter);
            GameContentDto dto = new GameContentDto();
            dto.Letter = Letter;
            var specificContent = BLL.Services.ContentServices.LoadAllContents();
            specificContent = specificContent.Where(c => c.Data.IsApproved && c.Data.UnDotedWord.StartsWith(Letter)).ToList();

            dto.Contents = specificContent;
            dto.ValidLetterName = null;
            dto.InValidLetterName = null;
            return Json(dto, JsonRequestBehavior.AllowGet);

        }

        [HttpGet]
        [Route("Child/GetGameLetters")]
        public JsonResult GetGameLetters(string Letter)
        {
            Console.WriteLine(Request.Url);
            
            GameContentDto dto = new GameContentDto();
            dto.Letter = Letter;

            dto.Contents = null;
            string folderPath = AppDomain.CurrentDomain.BaseDirectory;
            string str = folderPath.Substring(0, folderPath.IndexOf("\\", System.StringComparison.InvariantCultureIgnoreCase) + "\\".Length);
            str = str + "/";
            str = str + folderPath.Substring(str.Length - 1).Replace("\\", "/");
            folderPath = str + "Pictures/Alphabet";
            DirectoryInfo di = new DirectoryInfo(folderPath);
            dto.ValidLetterName = di.GetFiles("*.png").Where(f => f.Name.StartsWith(Letter)).Select(f => f.Name).ToList();
            dto.InValidLetterName = di.GetFiles("*.png").Where(f => !f.Name.StartsWith(Letter)).Select(f => f.Name).ToList();
            return Json(dto, JsonRequestBehavior.AllowGet);

        }

        [HttpGet]
        [Route("Child/GetContentPath/{file}")]
        public String GetContentPath(string file)
        {
            Console.WriteLine(Request.Url);
            var localPath = "http://localhost:58432/ContentFiles/";
            //var filePath = file.Code + "." + file.Extention;

            return localPath;// + filePath;

        }
        public ActionResult GoToGame(char letter, string name, string needContent)
        {
            Models.GameInformationModel m = new Models.GameInformationModel();
            m.Letter = letter;
            if (needContent == "true")
            {
                var specificContent = BLL.Services.ContentServices.LoadAllContents();
                specificContent = specificContent.Where(c => c.Data.IsApproved && c.Data.UnDotedWord.StartsWith(Char.ToString(letter))).ToList();

                m.Contents = specificContent;
            }
            else// needContent == "false"
            {
                //string folderPath = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName + "/Users/ron katz/Documents/GitHub/Learn-Hebrew/Project/LearnHebrew/Pictures/Alphabet";

                string folderPath = AppDomain.CurrentDomain.BaseDirectory;
                string str = folderPath.Substring(0, folderPath.IndexOf("\\", System.StringComparison.InvariantCultureIgnoreCase) + "\\".Length);
                str = str + "/";
                str = str + folderPath.Substring(str.Length - 1).Replace("\\", "/");
                folderPath = str + "Pictures/Alphabet";
                DirectoryInfo di = new DirectoryInfo(folderPath);
                m.ValidLetterName = di.GetFiles("*.png").Where(f => f.Name.StartsWith(Char.ToString(letter))).Select(f => f.Name).ToList();
                m.InValidLetterName = di.GetFiles("*.png").Where(f => !f.Name.StartsWith(Char.ToString(letter))).Select(f => f.Name).ToList();
            }
            var view = "~/Views/Game/Game_" + name + ".cshtml";
            return View(view, m);
           
        }
        public ActionResult GoToTest(char letter, string name, string needContent)
        {
            Models.TestModel m = new Models.TestModel();
            m.Letter = letter;
            m.gameName = name;
            m.contentNeeded = needContent;
            var specificContent = BLL.Services.ContentServices.LoadAllContents();
            specificContent = specificContent.Where(c => c.Data.IsApproved && c.Data.UnDotedWord.StartsWith(Char.ToString(letter))).ToList();
            specificContent = specificContent.OrderBy(x => Guid.NewGuid()).ToList();
            m.questions = specificContent.Take(4).ToList();

            //if (Auxiliray.Session.ChildInSession.ChildID != -1)
            //{
            //    BLL.LearnHebrewEntities.ChildProgress childProgress = new BLL.LearnHebrewEntities.ChildProgress();
            //    childProgress.ChildID = Auxiliray.Session.ChildInSession.ChildID;
            //    childProgress.Data.ChosenContents = m.questions.ToDictionary(k => k.ContentID, v => v);
            //    childProgress.Data.Date = DateTime.Now;
            //    var progressID = BLL.Services.ChildProgressServices.save(childProgress);
            //}
            var view = "~/Views/Child/Test.cshtml";
            return View(view, m);

        }

        [HttpPost]
        public ActionResult SaveChildProgress(string[] wrongAnswers, string[] wrongAnswersCorrection)
        {
            BLL.LearnHebrewEntities.Child child = new BLL.LearnHebrewEntities.Child();
            try
            {
                child = Auxiliray.Session.ChildInSession;
             
                if (child == null)// the user is not found
                    return Content("failed to find child");

                //loop frew wrongAnswers
                //  object (contentid : wrong word)
                //  wrongAnswers dictionary .add (contentid, word) 

                //load child progress by progress id
                //insert wrongAnswers into wrongAnswers dictionary into progress
                //save progress again with all relevent data
            }
            catch (Exception ex)
            {
                return Content("fail");
            }

            return Content("OK");
        }
    }
}