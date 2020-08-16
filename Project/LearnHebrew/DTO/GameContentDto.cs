using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using System.Threading.Tasks;

namespace LearnHebrew.DTO
{
    public class GameContentDto
    {
        public List<BLL.LearnHebrewEntities.Content> Contents { get; set; }

        public String Letter { get; set; }

        public List<string> ValidLetterName { get; set; }

        public List<string> InValidLetterName { get; set; }
    }
}