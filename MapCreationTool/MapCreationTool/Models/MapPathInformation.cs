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
    public class MapPathInformation
    {
        public string settingsPath;

        /// <summary>
        /// Gets the path of the maps .sdd directory
        /// </summary>
        public string mapPath;

        /// <summary>
        /// Gets the name of the map directory without .sdd
        /// </summary>
        public string mapName;

        /// <summary>
        /// Gets the name of the map including .sdd
        /// </summary>
        public string mapSddName;

        public MapPathInformation(string fullMapPath)
        {
            this.mapPath = fullMapPath;
            mapName = PathHelper.GetMapNameFromPath(fullMapPath);
            this.mapSddName = PathHelper.GetSddName(mapName);
            string workDir = PathHelper.GetWorkDirFromFullMapPath(fullMapPath);
            this.settingsPath = PathHelper.GetSettingsPath(workDir, mapName);
        }

        public MapPathInformation(string mapName, string workDir)
        {
            this.mapPath = PathHelper.GetMapPath(mapName, workDir);
            this.mapName = mapName;
            this.mapSddName = PathHelper.GetSddName(mapName);
            this.settingsPath = PathHelper.GetSettingsPath(workDir, mapName);
        }
    }
}
