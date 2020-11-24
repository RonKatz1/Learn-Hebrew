using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LearnHebrew.Models
{
    public class UnseenModel
    {
        public BLL.LearnHebrewEntities.Adult Adult { get; set; }

        //public int AdultID { get; set; }

        public BLL.LearnHebrewEntities.Unseen Unseen { get; set; }
    }
}