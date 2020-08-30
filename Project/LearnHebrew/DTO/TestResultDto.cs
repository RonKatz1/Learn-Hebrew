using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using System.Threading.Tasks;

namespace LearnHebrew.DTO
{
    public class TestResultDto
    {
        public String[] WrongAnswers { get; set; }

        public int[] CorresponsingContentID { get; set; }

        public int ProgressID { get; set; }

        public char GameLetter { get; set; }
    }
}