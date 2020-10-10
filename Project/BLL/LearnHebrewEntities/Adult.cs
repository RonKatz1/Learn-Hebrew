using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace BLL.LearnHebrewEntities
{
    public class Adult
    {
        public int AdultID { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public AdultData Data { get; set; }

        public Adult()
        {
            Data = new AdultData();
        }

        [DataContract]
        public class AdultData
        {
            [DataMember]
            public bool IsTeacher { get; set; }
            [DataMember]
            public List<int> ChildsIDs { get; set; }
            [DataMember]
            public List<int> ContentIDsConfermed { get; set; }

            public AdultData()
            {
                ChildsIDs = new List<int>();
                ContentIDsConfermed = new List<int>();
            }
        }
    }
}
