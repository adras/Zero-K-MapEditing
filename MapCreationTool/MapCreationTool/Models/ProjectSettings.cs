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

        private string startLocations;

        private int minHeight;
        private int maxHeight;

        private MapSizeDefinition mapSizeDefinition;

        public string HeightMapName { get => heightMapName; set => heightMapName = value; }
        public string DiffuseMapName { get => diffuseMapName; set => diffuseMapName = value; }
        public string GrassMapName { get => grassMapName; set => grassMapName = value; }
        public string MetalMapName { get => metalMapName; set => metalMapName = value; }

        public string StartLocations { get => startLocations; set => startLocations = value; }

        public MapSizeDefinition MapSizeDefinition { get => mapSizeDefinition; set => mapSizeDefinition = value; }

        public int MinHeight { get => minHeight; set => minHeight = value; }
        public int MaxHeight { get => maxHeight; set => maxHeight = value; }

        internal static ProjectSettings OpenOrCreateDefault(MapPathInformation mapPathInfo)
        {
            ProjectSettingsSerializer serializer = new ProjectSettingsSerializer();
            ProjectSettings projectSettings;
            if (!File.Exists(mapPathInfo.settingsPath))
            {
                projectSettings = CreateDefault(mapPathInfo.mapPath);
                serializer.SerializeToFile(mapPathInfo.settingsPath, projectSettings);
            }
            else
            {
                projectSettings = serializer.DeserializeFromFile(mapPathInfo.settingsPath);
            }

            return projectSettings;
        }

        public static ProjectSettings CreateDefault(string mapPath)
        {
            ProjectSettings defaultSettings = new ProjectSettings
            {
                DiffuseMapName = Path.Combine(mapPath, "diffuse.bmp"),
                HeightMapName = Path.Combine(mapPath, "height.png"),
                GrassMapName = Path.Combine(mapPath, "grass.bmp"),
                MetalMapName = Path.Combine(mapPath, "metal.bmp"),
                StartLocations = Path.Combine(mapPath, @"mapconfig\map_startboxes.lua"),
                MinHeight = -50,
                MaxHeight = 200
            };

            return defaultSettings;
        }

        // Might allow enabling and disabling for map compilation
        //public bool useHeightMap;
        //public bool useDiffuseMap;
        //public bool useGrassMap;
        //public bool useOtherMap;
    }
}
