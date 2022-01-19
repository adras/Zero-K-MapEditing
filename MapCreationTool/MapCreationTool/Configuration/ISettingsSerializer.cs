using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MapCreationTool.Configuration
{
    interface ISettingsSerializer<T>
    {

        public T DeserializeFromFile(string path);

        public void SerializeToFile(string path, T settings);
    }
}
