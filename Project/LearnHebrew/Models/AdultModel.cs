using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LearnHebrew.Models
{
    public class AdultModel
    {
        public BLL.LearnHebrewEntities.Adult Adult { get; set; }

        public List<BLL.LearnHebrewEntities.Content> Contents { get; set; }

        public BLL.LearnHebrewEntities.Content ContentChosen { get; set; }

        public string Path { get; set; }
    }
}