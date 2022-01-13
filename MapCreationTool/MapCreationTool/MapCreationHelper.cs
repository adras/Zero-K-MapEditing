using MapCreationTool.Configuration;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MapCreationTool
{
    internal class MapCreationResult
    {
        public string type;
        public bool success;
        public string error;

        public MapCreationResult(string type, string error = null)
        {
            if (error == null)
            {
                success = true;
            }
            else
            {
                success = false;
                this.error = error;
            }
            this.type = type;
        }
    }

    internal class MapCreationHelper
    {
        const string MAP_BLUEPRINT_PATH = "Tools\\Map-Blueprint";
        const string BLUEPRINT_MAP_NAME = "mapcontainer.sdd";

        internal static MapCreationResult CreateMap(string mapName, string workDir)
        {
            MapCreationResult result;

            result = VerifyDirectory(mapName, workDir);
            if (!result.success)
                return result;

            result = CreateMapBlueprint(mapName, workDir);
            if (!result.success)
                return result;

            // TODO: Move to dedicated method
            ProjectSettingsSerializer settingsSerializer = new ProjectSettingsSerializer();
            ProjectSettings defaultSettings = settingsSerializer.CreateDefault();
            string settingsPath = Path.Combine(workDir, ProjectSettings.DEFAULT_FILE_NAME);
            settingsSerializer.SerializeToFile(settingsPath, defaultSettings);

            return new MapCreationResult("");
        }

        private static MapCreationResult CreateMapBlueprint(string mapName, string workDir)
        {
            string type = "Copy Map Blueprint";
            DirectoryInfo blueprintDir = new DirectoryInfo(MAP_BLUEPRINT_PATH);
            if (!blueprintDir.Exists)
            {
                // TODO: Needs testing
                string programDirectory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
                return new MapCreationResult(type, $"Could not find Map blueprint @ {Path.Combine(programDirectory, blueprintDir.FullName)} ");
            }
            if (Directory.Exists(Path.Combine(workDir, BLUEPRINT_MAP_NAME)))
            {
                return new MapCreationResult(type, $"There is already a blueprint in the workdir. Creation aborted");
            }

            foreach(FileInfo? file in blueprintDir.GetFiles("*.*", SearchOption.AllDirectories))
            {
                string relativeFilePath = GetRelativeFilePath(file.FullName);
                FileInfo targetFileInfo = new FileInfo(Path.Combine(workDir, relativeFilePath)); 
                if (!Directory.Exists(targetFileInfo.DirectoryName))
                {
                    Directory.CreateDirectory(targetFileInfo.DirectoryName);
                }
                File.Copy(file.FullName, targetFileInfo.FullName, true);
                Debug.WriteLine($"Copied {file.FullName} to {targetFileInfo.FullName}");
            }

            Directory.Move(Path.Combine(workDir, BLUEPRINT_MAP_NAME), Path.Combine(workDir, $"{mapName}.sdd"));

            return new MapCreationResult(type);
        }

        private static string GetRelativeFilePath(string fullFilePath)
        {
            string result = fullFilePath.ToLower();
            int startIdx = result.IndexOf(MAP_BLUEPRINT_PATH.ToLower());

            // Use filLFilePath to keep the original character casing
            // Use + 1 to get rid of a leading backslash
            result = fullFilePath.Substring(startIdx + MAP_BLUEPRINT_PATH.Length + 1);
            return result;
        }

        private static MapCreationResult VerifyDirectory(string mapName, string workDir)
        {
            if (!Directory.Exists(workDir))
            {
                try
                {
                    Directory.CreateDirectory(workDir);
                }
                catch
                {
                    return new MapCreationResult("Working Directory could not be created. Is the path valid? Permissions ok?");
                }
            }
            return new MapCreationResult("Create/Open WorkDirectory");
        }
    }
}
