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

        public ProgressDate Data { get; set; }


        public class ProgressDate
        {
            public char Letter { get; set; }
            //public string Word { get; set; }
            public Dictionary<int, BLL.LearnHebrewEntities.Content> ChosenContents { get; set; }

            public Dictionary<int, bool> ContentSucceess { get; set; }
            
            public DateTime Date { get; set; }

            public ProgressDate()
            {
                this.ChosenContents = new Dictionary<int, Content>();
                this.ContentSucceess = new Dictionary<int, bool>();
            }

        }
    }
}
