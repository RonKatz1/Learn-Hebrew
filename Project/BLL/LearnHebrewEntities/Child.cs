using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;


namespace BLL.LearnHebrewEntities
{
    public class Child
    {
        public string Name { get; set; }
        public string Password { get; set; }
        public string Color { get; set; }
        public ChildData Data { get; set; }
        public int ChildID { get; set; }

        public Child()
        {
            Data = new ChildData();
        }

        [DataContract]
        public class ChildData
        {
            [DataMember]
            public int GamePoints { get; set; }
        }
    }
}
