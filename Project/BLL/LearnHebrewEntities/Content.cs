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
        //[DataMember]
        //public Photo Photo { get; set; }

        //[DataMember]
        //public VoiceFile VoiceFile { get; set; }

        [DataMember]
        public ContentFile PhotoFile { get; set; }
        [DataMember]
        public ContentFile VoiceFile { get; set; }

        public ContentData()
        {
            //this.Photo = new Photo();
            //this.VoiceFile = new VoiceFile();

            this.PhotoFile = new ContentFile();
            this.VoiceFile = new ContentFile();
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

    //[DataContract]
    //public class Photo
    //{
    //    [DataMember]
    //    public string Name { get; set; }
    //    [DataMember]
    //    public string Code { get; set; }
    //    [DataMember]
    //    public string Extention { get; set; }

    //}

    //[DataContract]
    //public class VoiceFile
    //{
    //    [DataMember]
    //    public string Name { get; set; }
    //    [DataMember]
    //    public string Code { get; set; }
    //    [DataMember]
    //    public string Extention { get; set; }
    //}
}
