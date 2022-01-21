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
        /// Gets the path of the maps directory
        /// </summary>
        public string mapPath;

        /// <summary>
        /// Gets the name of the map directory
        /// </summary>
        public string mapName;

        /// <summary>
        /// Gets the directory the map directory is contained in
        /// </summary>
        public string workDir;

        /// <summary>
        /// Gets the path to the maps mapinfo.lua file
        /// </summary>
        public string mapInfoPath;


        public MapPathInformation(string fullMapPath)
        {
            this.mapPath = fullMapPath;
            this.mapName = PathHelper.GetMapDirectoryNameFromPath(fullMapPath);
            this.workDir = PathHelper.GetWorkDirFromFullMapPath(fullMapPath);
            this.settingsPath = PathHelper.GetSettingsPath(mapPath);
            this.mapInfoPath = PathHelper.GetMapInfoPath(mapPath);
        }

        public MapPathInformation(string mapName, string workDir)
        {
            this.mapPath = PathHelper.GetMapPath(mapName, workDir);
            this.mapName = mapName;
            this.workDir = workDir;
            this.settingsPath = PathHelper.GetSettingsPath(mapPath);
            this.mapInfoPath = PathHelper.GetMapInfoPath(mapPath);
        }
    }
}
