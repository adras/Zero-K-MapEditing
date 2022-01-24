using MapCreationTool.Helpers;
using MapCreationTool.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MapCreationTool.MapConverter
{
    internal class ProjectSettingsToCompilerSettingsConverter
    {
        private class SettingsAdder
        {
            PyMapCompilerSettings compilerSettings;

            internal SettingsAdder(PyMapCompilerSettings compilerSettings)
            {
                this.compilerSettings = compilerSettings;
            }

            internal bool AddSettingIfSet(string parameter, string value, bool isSet)
            {
                // Do not add a setting if the value is empty
                if (string.IsNullOrEmpty(value))
                    return false;

                // Make sure the setting is enabled
                if (isSet)
                {
                    compilerSettings.AddSetting(new PyMapCompilerSetting(parameter, value));
                    return true;
                }
                return false;
            }
        }

        public static PyMapCompilerSettings Convert(ProjectSettings projectSettings)
        {
            CompilationSettings compSettings = projectSettings.CompilationSettings;
            PyMapCompilerSettings settings = new PyMapCompilerSettings();
            SettingsAdder adder = new SettingsAdder(settings);

            adder.AddSettingIfSet("-o", compSettings.OutSmfFilePath, true);
            adder.AddSettingIfSet("-t", compSettings.DiffuseMapName, true);
            adder.AddSettingIfSet("-a", compSettings.HeightMapName, true);
            adder.AddSettingIfSet("-m", compSettings.MetalMapName, compSettings.UseMetalMap);
            adder.AddSettingIfSet("-x", compSettings.MaxHeight.ToString(), true);
            adder.AddSettingIfSet("-n", compSettings.MinHeight.ToString(), true);
            adder.AddSettingIfSet("-g", compSettings.GeoventDecalPath, compSettings.UseGeoventDecal);
            adder.AddSettingIfSet("-k", compSettings.FeaturePlacementFilePath, compSettings.UseFeaturePlacement);

            adder.AddSettingIfSet("-j", compSettings.FeatureListFilePath, compSettings.UseFeatureList);
            adder.AddSettingIfSet("-f", compSettings.FeatureMapFilePath, compSettings.UseFeatureMap);
            adder.AddSettingIfSet("-y", compSettings.TypeMapFilePath, compSettings.UseTypeMap);
            adder.AddSettingIfSet("-p", compSettings.MinimapFilePath, compSettings.UseMinimap);
            // Special format since these are parameters as well
            adder.AddSettingIfSet("-v", $"\"{compSettings.NvdxtOptions}\"", compSettings.UseNvdxt);
            adder.AddSettingIfSet("--highresheightmapfilter", compSettings.HighResMapFilter, compSettings.UseHighResMapFilter);
            adder.AddSettingIfSet("-c", compSettings.Dirty, compSettings.UseDirty);

            return settings;
        }

    }
}
