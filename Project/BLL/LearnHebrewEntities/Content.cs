using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace BLL.LearnHebrewEntities
{
    public class Content
    {
        public int ContentID { get; set; }
        public string Word { get; set; }
        public int AdultID { get; set; }

        public ContentData Data { get; set; }

        public Content()
        {
            this.Data = new ContentData();
        }
    }

    [DataContract]
    public class ContentData
    {
        [DataMember]
        public DateTime DateCreated { get; set; }
        [DataMember]
        public bool IsApproved { get; set; }
        [DataMember]
        public bool HideUnAprroverdContent { get; set; }
        [DataMember]
        public int ApprovedCount { get; set; }
        [DataMember]
        public int DisApprovedCount { get; set; }
        [DataMember]
        public string UnDotedWord { get; set; }

        [DataMember]
        public ContentFile PhotoFile { get; set; }
        [DataMember]
        public ContentFile VoiceFile { get; set; }

        public ContentData()
        {
            this.PhotoFile = new ContentFile();
            this.VoiceFile = new ContentFile();
            this.DateCreated = DateTime.MinValue;
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
