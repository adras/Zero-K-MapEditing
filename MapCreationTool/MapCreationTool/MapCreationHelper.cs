using MapCreationTool.Configuration;
using MapCreationTool.Images;
using MapCreationTool.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace MapCreationTool
{
    internal class MapCreationHelper
    {
        const string MAP_BLUEPRINT_PATH = "Tools\\Map-Blueprint";
        const string BLUEPRINT_MAP_NAME = "mapcontainer.sdd";

        internal static MapCreationResult CreateMap(string mapName, string workDir, MapSizeDefinition mapSizeDef)
        {
            MapCreationResult result;
            MapPathInformation pathInfo = new MapPathInformation(mapName, workDir);

            result = VerifyDirectory(mapName, workDir);
            if (!result.success)
                return result;

            result = CreateMapBlueprint(mapName, workDir, pathInfo);
            if (!result.success)
                return result;

            result = RenameMapBlueprint(mapName, workDir);
            if (!result.success)
                return result;


            // TODO: Move to dedicated method
            ProjectSettingsSerializer settingsSerializer = new ProjectSettingsSerializer();
            ProjectSettings defaultSettings = settingsSerializer.CreateDefault();
            defaultSettings.mapSizeDefinition = mapSizeDef;

            settingsSerializer.SerializeToFile(pathInfo.settingsPath, defaultSettings);

            ImageTest.CreateImages(defaultSettings.mapSizeDefinition, pathInfo.mapPath);

            return new MapCreationResult("", pathInfo);
        }

        private static MapCreationResult CreateMapBlueprint(string mapName, string workDir, MapPathInformation pathInfo)
        {
            string type = "Copy Map Blueprint";
            DirectoryInfo blueprintDir = new DirectoryInfo(MAP_BLUEPRINT_PATH);
            if (!blueprintDir.Exists)
            {
                // TODO: Needs testing
                string programDirectory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
                return new MapCreationResult(type, pathInfo, $"Could not find Map blueprint @ {Path.Combine(programDirectory, blueprintDir.FullName)} ");
            }
            string newBlueprintPath = Path.Combine(workDir, BLUEPRINT_MAP_NAME);
            if (Directory.Exists(newBlueprintPath))
            {
                return new MapCreationResult(type, pathInfo, $"There is already a blueprint in the workdir. Creation aborted. Please delete {newBlueprintPath}");
            }

            foreach (FileInfo? file in blueprintDir.GetFiles("*.*", SearchOption.AllDirectories))
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


            return new MapCreationResult(type);
        }

        public static MapCreationResult RenameMapBlueprint(string mapName, string workDir)
        {
            MapCreationResult result;
            string type = "Rename Map Blueprint";
            string sourceDirPath = Path.Combine(workDir, BLUEPRINT_MAP_NAME);
            string targetDirPath = Path.Combine(workDir, $"{mapName}.sdd");
            //     Directory.Move(Path.Combine(workDir, BLUEPRINT_MAP_NAME), Path.Combine(workDir, $"{mapName}.sdd"));
            if (!Directory.Exists(targetDirPath))
            {
                Directory.Move(sourceDirPath, targetDirPath);
            }
            else
            {
                string message = $"Could not set name for map directory. Directory {targetDirPath} already exists. Please delete directory and try again";
                result = new MapCreationResult(type, null, message);
                return result;
            }
            result = new MapCreationResult(type);
            return result;
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
