using MapCreationTool.Models;
using System.IO;
using System.Reflection;

namespace MapCreationTool.Helpers
{
    public class PathHelper
    {
        public const string MAP_INFO_NAME = "mapinfo.lua";
        public const string TOOL_DIRECTORY_NAME = "Tools";

        public static string GetMapDirectoryNameFromPath(string path)
        {
            DirectoryInfo mapDirInfo = new DirectoryInfo(path);
            string mapDirName = mapDirInfo.Name;


            return mapDirName;
        }

        internal static string? GetMapPath(string mapName, string workDir)
        {
            string path = Path.Combine(workDir, mapName);
            return path;
        }

        internal static string GetWorkDirFromFullMapPath(string fullMapPath)
        {
            DirectoryInfo mapDir = new DirectoryInfo(fullMapPath);
            string workDir = mapDir.Parent.FullName;
            return workDir;
        }

        internal static string? GetSettingsPath(string mapPath)
        {
            DirectoryInfo mapDirInfo = new DirectoryInfo(mapPath);
            string result = Path.Combine(mapDirInfo.FullName, ProjectSettings.DEFAULT_FILE_NAME);
            return result;
        }

        internal static string? GetMapInfoPath(string mapPath)
        {
            // Linux ain't not gonna like the casing of the created path, because it could be different
            DirectoryInfo mapFileInfo = new DirectoryInfo(mapPath);
            string result = Path.Combine(mapFileInfo.FullName, MAP_INFO_NAME);
            return result;
        }

        internal static string GetFilePathWithoutExtension(string filePath)
        {
            FileInfo fileInfo = new FileInfo(filePath);
            string directoryPath = fileInfo.Directory.FullName;
            string fileName = Path.GetFileNameWithoutExtension(filePath);
            string result = Path.Combine(directoryPath, fileName);

            return result;
        }

        /// <summary>
        /// Returns true if the given map path is actually containing a map
        /// </summary>
        /// <param name="fullMapPath"></param>
        /// <returns></returns>
        /// <remarks>Afaik, every map must have a maps subdirectory. This method checks for it's existence.
        /// No maps subdirectory -> Not a map
        /// A bit troublesome if the user stores their map in a related directory
        /// </remarks>
        internal static bool IsMapDirectory(string fullMapPath)
        {
            string mapsPath = Path.Combine(fullMapPath, "maps");
            if (!Directory.Exists(mapsPath))
                return false;

            return true;
        }

        public static DirectoryInfo GetApplicationDirectory()
        {
            string assemblyPath = Assembly.GetExecutingAssembly().Location;
            FileInfo assemblyInfo = new FileInfo(assemblyPath);
            DirectoryInfo result = assemblyInfo.Directory;
            return result;
        }

        public static DirectoryInfo GetApplicationToolsDirectory()
        {
            DirectoryInfo appDir = GetApplicationDirectory();
            string toolPath = Path.Combine(appDir.FullName, TOOL_DIRECTORY_NAME);
            DirectoryInfo result = new DirectoryInfo(toolPath);
            return result;
        }
    }
}
