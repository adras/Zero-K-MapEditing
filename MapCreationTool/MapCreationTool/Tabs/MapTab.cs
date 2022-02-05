using MapCreationTool.Controls;
using MapCreationTool.Models;
using System;

namespace MapCreationTool.Tabs
{
    internal class MapTab : TabBase
    {
        private ProjectSettings projectSettings;
        public ProjectSettings ProjectSettings { get => projectSettings; set => projectSettings = value; }

        string header;
        object content;

        public override string Header
        {
            get => header;
        }

        public override object Content
        {
            get => content;
        }

        public MapTab()
        {
            this.content = new EditMapControl();
        }

        public void LoadProjectSettings(MapPathInformation pathInfo)
        {
            // Settings exist when map was opened previously, otherwise default settings will be created
            this.projectSettings = ProjectSettings.OpenOrCreateDefault(pathInfo);
        }

        internal void LoadMapInfo()
        {
            if (projectSettings == null)
                throw new NotSupportedException("MapInfo can only be loaded after Project Settings are loaded");

            projectSettings.MapInformation = MapInformation.LoadFrom(projectSettings.MapPathInformation.mapInfoPath);
            header = projectSettings.MapPathInformation.mapName;
        }
    }
}
