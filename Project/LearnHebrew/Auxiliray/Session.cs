using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LearnHebrew.Auxiliray
{
    public class Session
    {

        public static BLL.LearnHebrewEntities.Child ChildInSession
        {
            get
            {
                if (HttpContext.Current.Session["ChildInSession"] == null)
                    return null;
                else
                    return (BLL.LearnHebrewEntities.Child)HttpContext.Current.Session["ChildInSession"];
            }
            set
            {
                HttpContext.Current.Session["ChildInSession"] = value;
            }
        }

        public static BLL.LearnHebrewEntities.Adult AdultInSession
        {
            get
            {
                if (HttpContext.Current.Session["AdultInSession"] == null)
                    return null;
                else
                    return (BLL.LearnHebrewEntities.Adult)HttpContext.Current.Session["AdultInSession"];
            }
            set
            {
                HttpContext.Current.Session["AdultInSession"] = value;
            }
        }

        public static string LetterForPrograssFillter
        {
            get
            {
                if (HttpContext.Current.Session["LetterForPrograssFillter"] == null)
                    return "הכל";
                else
                    return (string)HttpContext.Current.Session["LetterForPrograssFillter"];
            }
            set
            {
                HttpContext.Current.Session["LetterForPrograssFillter"] = value;
            }
        }

        //public static string ContentPhotoPath
        //{
        //    get {
        //        var str = "C:\Users\tal\Documents\GitHub\Learn-Hebrew\Project\Photos";
        //        return str;
        //    }
        //}
    }
}