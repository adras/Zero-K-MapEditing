using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MapCreationTool.Configuration
{
    public class ProjectSettings
    {
        public const string DEFAULT_FILE_NAME = "MapCreationTool.xml";

        public string heightMapName;
        public string diffuseMapName;
        public string grassMapName;
        public string otherMapName;

        // Might allow enabling and disabling for map compilation
        //public bool useHeightMap;
        //public bool useDiffuseMap;
        //public bool useGrassMap;
        //public bool useOtherMap;
    }
}
