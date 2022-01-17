using MapCreationTool.Models;
using System;
using System.Collections.Generic;
using System.IO;
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
        private string metalMapName;

        private int minHeight;
        private int maxHeight;

        private MapSizeDefinition mapSizeDefinition;

        public string HeightMapName { get => heightMapName; set => heightMapName = value; }
        public string DiffuseMapName { get => diffuseMapName; set => diffuseMapName = value; }
        public string GrassMapName { get => grassMapName; set => grassMapName = value; }
        public string MetalMapName { get => metalMapName; set => metalMapName = value; }

        public MapSizeDefinition MapSizeDefinition { get => mapSizeDefinition; set => mapSizeDefinition = value; }

        public int MinHeight { get => minHeight; set => minHeight = value; }
        public int MaxHeight { get => maxHeight; set => maxHeight = value; }

        internal static ProjectSettings OpenOrCreateDefault(string settingsPath)
        {
            ProjectSettingsSerializer serializer = new ProjectSettingsSerializer();
            ProjectSettings projectSettings;
            if (!File.Exists(settingsPath))
            {
                projectSettings = serializer.CreateDefault();
                serializer.SerializeToFile(settingsPath, projectSettings);
            }
            else
            {
                projectSettings = serializer.DeserializeFromFile(settingsPath);
            }

            return projectSettings;
        }



        // Might allow enabling and disabling for map compilation
        //public bool useHeightMap;
        //public bool useDiffuseMap;
        //public bool useGrassMap;
        //public bool useOtherMap;
    }
}
