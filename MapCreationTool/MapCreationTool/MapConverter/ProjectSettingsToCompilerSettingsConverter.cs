using MapCreationTool.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MapCreationTool.MapConverter
{
    internal class ProjectSettingsToCompilerSettingsConverter
    {
        private class SettingsAdder
        {
            MapCompilerSettings compilerSettings;

            internal SettingsAdder(MapCompilerSettings compilerSettings)
            {
                this.compilerSettings = compilerSettings;
            }

            internal bool AddSettingIfSet(string parameter, string value, bool isSet)
            {
                if (isSet)
                {
                    compilerSettings.AddSetting(new MapCompilerSetting(parameter, value));
                    return true;
                }
                return false;
            }


        }

        public static MapCompilerSettings Convert(ProjectSettings projectSettings)
        {
            MapCompilerSettings settings = new MapCompilerSettings();
            SettingsAdder adder = new SettingsAdder(settings);
            adder.AddSettingIfSet("-o", projectSettings.OutSmfFilePath, true);
            adder.AddSettingIfSet("-t", projectSettings.DiffuseMapName, true);
            adder.AddSettingIfSet("-a", projectSettings.HeightMapName, true);
            adder.AddSettingIfSet("-m", projectSettings.MetalMapName, projectSettings.UseMetalMap);
            adder.AddSettingIfSet("-x", projectSettings.MaxHeight.ToString(), true);
            adder.AddSettingIfSet("-n", projectSettings.MinHeight.ToString(), true);
            adder.AddSettingIfSet("-g", projectSettings.GeoventDecalPath, projectSettings.UseGeoventDecal);
            adder.AddSettingIfSet("-k", projectSettings.FeaturePlacementFilePath, projectSettings.UseFeaturePlacement);

            // This is enough for now


            return settings;
        }

    }
}
