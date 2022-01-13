using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MapCreationTool.Configuration
{
    class ProgramSettingsSerializer : ISettingsSerializer<ProgramSettings>
    {
        GenericSerializer<ProgramSettings> serializer;
        
        public ProgramSettingsSerializer()
        {
            serializer = new GenericSerializer<ProgramSettings>();
        }

        public ProgramSettings CreateDefault()
        {
            ProgramSettings defaultSettings = new ProgramSettings
            {

            };
            return defaultSettings;
        }

        public ProgramSettings DeserializeFromFile(string path)
        {
            ProgramSettings result = serializer.DeserializeFromFile(path);
            return result;
        }

        public void SerializeToFile(string path, ProgramSettings settings)
        {
            serializer.SerializeToFile(path, settings);
        }
    }
}
