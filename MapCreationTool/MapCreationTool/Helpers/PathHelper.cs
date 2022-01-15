using MapCreationTool.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MapCreationTool.Helpers
{
    public class PathHelper
    {
        // TODO: Add methods which help to create the paths required to create a map
        // blueprint path, map folder, etc
        public static string GetMapNameFromPath(string path)
        {
            FileInfo fi = new FileInfo(path);
            string mapDirName = fi.FullName;

            int sddStart = fi.Name.IndexOf(".sdd");
            
            string result = fi.Name.Substring(0, sddStart);

            return result;
        }

        internal static string? GetMapPath(string mapName, string workDir)
        {
            string mapSddName = GetSddName(mapName);

            string path = Path.Combine(workDir, mapSddName);
            return path;
        }

        internal static string? GetSddName(string mapName)
        {
            string result = $"{mapName}.sdd";
            return result;
        }

        internal static string GetWorkDirFromFullMapPath(string fullMapPath)
        {
            DirectoryInfo mapDir = new DirectoryInfo(fullMapPath);
            string workDir = mapDir.Parent.FullName;
            return workDir;
        }

        internal static string? GetSettingsPath(string workDir, string mapName)
        {
            string sddName = GetSddName(mapName);
            string result = Path.Combine(workDir, sddName, ProjectSettings.DEFAULT_FILE_NAME);
            return result;
        }
    }
}
