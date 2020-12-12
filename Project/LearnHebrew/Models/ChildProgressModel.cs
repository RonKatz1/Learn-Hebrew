using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LearnHebrew.Models
{
    public class ChildProgressModel
    {
        public int ChildID { get; set; }
        public List<BLL.LearnHebrewEntities.ChildProgress> ChildProgresses { get; set; }

        public int LastOrderBy { get; set; }
        public bool LastIsAsc { get; set; }

        public string GraphLabels { get; set; }
        public string GraphData { get; set; }
        public List<string> GraphLabelsList { get; set; }
        public List<string> GraphDataList { get; set; }
    }
}