using MapCreationTool.Helpers;

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

        public MapPathInformation()
        {
            // only for serialization purposes
        }

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
