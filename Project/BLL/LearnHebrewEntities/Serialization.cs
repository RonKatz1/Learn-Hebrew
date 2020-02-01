using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization.Json;
using System.Xml;
using System.IO;

namespace BLL.LearnHebrewEntities
{
    public class Serialization
    {
        public static T Deserialize2<T>(string json)
        {
            T obj = Activator.CreateInstance<T>();

            if (json == null)
                return obj;

            using (XmlDictionaryReader jsonReader = JsonReaderWriterFactory.CreateJsonReader(Encoding.UTF8.GetBytes(json), XmlDictionaryReaderQuotas.Max))
            {
                DataContractJsonSerializer serializer = new DataContractJsonSerializer(obj.GetType());
                obj = (T)serializer.ReadObject(jsonReader);
                return obj;
            }
        }

        public static string Serialize2<T>(T obj)
        {
            DataContractJsonSerializer serializer = new DataContractJsonSerializer(obj.GetType());
            using (MemoryStream ms = new MemoryStream())
            {
                serializer.WriteObject(ms, obj);
                return Encoding.UTF8.GetString(ms.ToArray());
            }
        }
    }
}
