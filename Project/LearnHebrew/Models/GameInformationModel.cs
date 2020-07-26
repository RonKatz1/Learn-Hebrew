using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LearnHebrew.Models
{
    public class GameInformationModel
    {
        public List<BLL.LearnHebrewEntities.Content> Contents { get; set; }

        public char Letter { get; set; }

        public List<string> ValidLetterName { get; set; }

        public List<string> InValidLetterName { get; set; }
    }
}