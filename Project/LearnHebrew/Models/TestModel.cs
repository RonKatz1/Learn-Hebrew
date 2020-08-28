using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LearnHebrew.Models
{
    public class TestModel
    {
        public List<BLL.LearnHebrewEntities.Content> questions { get; set; }

        public char Letter { get; set; }

        public string gameName { get; set; }

        public string contentNeeded { get; set; }
    }
}