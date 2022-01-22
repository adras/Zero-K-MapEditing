using MapCreationTool.Configuration;
using MapCreationTool.Helpers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MapCreationTool.Models
{
    public class ProjectSettings
    {
        public const string DEFAULT_FILE_NAME = "MapCreationTool.xml";
        public const string GEO_VENT_FILE_NAME = "geovent.bmp";

        private string startLocations;


        private MapSizeDefinition mapSizeDefinition;

        public string StartLocations { get => startLocations; set => startLocations = value; }

        public MapSizeDefinition MapSizeDefinition { get => mapSizeDefinition; set => mapSizeDefinition = value; }


        #region CompilationSettings
        private string outSmfFilePath;
        private string heightMapName;
        private string diffuseMapName;
        private string grassMapName;
        private string metalMapName;
        private int minHeight;
        private int maxHeight;
        private string geoventDecalPath;
        private string featurePlacementFilePath;

        private bool useMetalMap;
        private bool useGeoventDecal;
        private bool useFeaturePlacement;

        public string OutSmfFilePath { get => outSmfFilePath; set => outSmfFilePath = value; }
        public string HeightMapName { get => heightMapName; set => heightMapName = value; }
        public string DiffuseMapName { get => diffuseMapName; set => diffuseMapName = value; }
        public string GrassMapName { get => grassMapName; set => grassMapName = value; }
        public string MetalMapName { get => metalMapName; set => metalMapName = value; }

        public int MinHeight { get => minHeight; set => minHeight = value; }
        public int MaxHeight { get => maxHeight; set => maxHeight = value; }
        public bool UseMetalMap { get => useMetalMap; set => useMetalMap = value; }
        public string GeoventDecalPath { get => geoventDecalPath; set => geoventDecalPath = value; }
        public bool UseGeoventDecal { get => useGeoventDecal; set => useGeoventDecal = value; }
        public string FeaturePlacementFilePath { get => featurePlacementFilePath; set => featurePlacementFilePath = value; }
        public bool UseFeaturePlacement { get => useFeaturePlacement; set => useFeaturePlacement = value; }
        #endregion

        internal static ProjectSettings OpenOrCreateDefault(MapPathInformation mapPathInfo)
        {
            ProjectSettingsSerializer serializer = new ProjectSettingsSerializer();
            ProjectSettings projectSettings;
            if (!File.Exists(mapPathInfo.settingsPath))
            {
                projectSettings = CreateDefault(mapPathInfo);
                serializer.SerializeToFile(mapPathInfo.settingsPath, projectSettings);
            }
            else
            {
                projectSettings = serializer.DeserializeFromFile(mapPathInfo.settingsPath);
            }

            return projectSettings;
        }

        public static ProjectSettings CreateDefault(MapPathInformation pathInfo)
        {
            string geoventPath = Path.Combine(PathHelper.GetApplicationToolsDirectory().FullName, GEO_VENT_FILE_NAME);

            ProjectSettings defaultSettings = new ProjectSettings
            {
                DiffuseMapName = Path.Combine(pathInfo.mapPath, "diffuse.bmp"),
                HeightMapName = Path.Combine(pathInfo.mapPath, "height.png"),
                GrassMapName = Path.Combine(pathInfo.mapPath, "grass.bmp"),
                MetalMapName = Path.Combine(pathInfo.mapPath, "metal.bmp"),
                StartLocations = Path.Combine(pathInfo.mapPath, @"mapconfig\map_startboxes.lua"),
                OutSmfFilePath = Path.Combine(pathInfo.mapPath, $@"{pathInfo.mapName}.smf"),
                GeoventDecalPath = geoventPath,
                MinHeight = -50,
                MaxHeight = 200,
                UseGeoventDecal = true,
                
            };

            return defaultSettings;
        }
    }
}
