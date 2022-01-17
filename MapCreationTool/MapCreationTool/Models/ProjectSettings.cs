using MapCreationTool.Models;
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

		private string heightMapName;
		private string diffuseMapName;
		private string grassMapName;
		private string otherMapName;

		private MapSizeDefinition mapSizeDefinition;

		public string HeightMapName { get => heightMapName; set => heightMapName = value; }
		public string DiffuseMapName { get => diffuseMapName; set => diffuseMapName = value; }
		public string GrassMapName { get => grassMapName; set => grassMapName = value; }
		public string OtherMapName { get => otherMapName; set => otherMapName = value; }

		public MapSizeDefinition MapSizeDefinition { get => mapSizeDefinition; set => mapSizeDefinition = value; }
		
		

		// Might allow enabling and disabling for map compilation
		//public bool useHeightMap;
		//public bool useDiffuseMap;
		//public bool useGrassMap;
		//public bool useOtherMap;
	}
}
