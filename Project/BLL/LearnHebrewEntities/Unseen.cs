using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace BLL.LearnHebrewEntities
{
    public class Unseen
    {
        public int UnseenID { get; set; }
        public int AdultID { get; set; }
        public UnseenData Data { get; set; }

        public Unseen()
        {
            Data = new UnseenData();
        }

        [DataContract]
        public class UnseenData
        {
            [DataMember]
            public string UnseenName { get; set; }

            [DataMember]
            public Dictionary<string, ContentFile> UnseenContents;

            public UnseenData()
            {
                UnseenContents = new Dictionary<string, ContentFile>();
            }
        }

        [DataContract]
        public class ContentFile
        {
            [DataMember]
            public string Name { get; set; }
            [DataMember]
            public string Code { get; set; }
            [DataMember]
            public string Extention { get; set; }
        }
    }
}
