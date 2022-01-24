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

		public MapPathInformation MapPathInformation { get; set; }
		public CompilationSettings CompilationSettings { get; set; }

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
				StartLocations = Path.Combine(pathInfo.mapPath, @"mapconfig\map_startboxes.lua"),
				MapPathInformation = pathInfo,
				CompilationSettings = new CompilationSettings
				{
					DiffuseMapName = Path.Combine(pathInfo.mapPath, "diffuse.bmp"),
					HeightMapName = Path.Combine(pathInfo.mapPath, "height.png"),
					GrassMapName = Path.Combine(pathInfo.mapPath, "grass.bmp"),
					MetalMapName = Path.Combine(pathInfo.mapPath, "metal.bmp"),
					OutSmfFilePath = Path.Combine(pathInfo.mapPath, $@"{pathInfo.mapName}.smf"),
					GeoventDecalPath = geoventPath,
					MinHeight = -50,
					MaxHeight = 200,
					NvdxtOptions = "-Sinc -quality_highest",
					HighResMapFilter = "lanczos",

					UseGeoventDecal = true,
					UseNvdxt = true,
					UseHighResMapFilter = true
				}
			};

			return defaultSettings;
		}
	}
}
