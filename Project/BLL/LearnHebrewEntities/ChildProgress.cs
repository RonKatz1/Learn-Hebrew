using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.LearnHebrewEntities
{
    public class ChildProgress
    {

        public int ProgressID { get; set; }
        public int ChildID { get; set; }

        public ProgressData Data { get; set; }


        public class ProgressData
        {
            public char Letter { get; set; }

            public Dictionary<int, string> WrongAnswers { get; set; } //real contentID with wrong answer 

            public Dictionary<int, BLL.LearnHebrewEntities.Content> ChosenContents { get; set; }//all chosen content for child learning session

            //public Dictionary<int, bool> ContentSucceess { get; set; }//all contentIDs with bool to define if answerd correctly
            
            public DateTime Date { get; set; }

            public ProgressData()
            {
                this.ChosenContents = new Dictionary<int, Content>();
                this.WrongAnswers = new Dictionary<int, string>();
            }

        }
    }
}
