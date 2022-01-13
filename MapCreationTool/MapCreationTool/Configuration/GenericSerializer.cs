using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace MapCreationTool.Configuration
{
    public class GenericSerializer<T> where T : class
    {
        public T DeserializeFromFile(string path)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(T));
            using (Stream fileStream = File.OpenRead(path))
            {
                T result = serializer.Deserialize(fileStream) as T;
                return result;
            }
        }

        public void SerializeToFile(string path, T settings)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(T));
            using (Stream fileStream = File.Create(path))
            {
                serializer.Serialize(fileStream, settings);
            }
        }
    }
}
