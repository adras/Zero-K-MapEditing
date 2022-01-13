﻿using MapCreationTool.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MapCreationTool.Configuration
{
    class ProjectSettingsSerializer : ISettingsSerializer<ProjectSettings>
    {
        GenericSerializer<ProjectSettings> serializer;

        public ProjectSettingsSerializer()
        {
            serializer = new GenericSerializer<ProjectSettings>();
        }

        public ProjectSettings CreateDefault()
        {
            ProjectSettings defaultSettings = new ProjectSettings
            {
                diffuseMapName = "diffuse.png",
                heightMapName = "height.png",
                grassMapName = "grass.png",
                otherMapName = "other.png"
            };

            return defaultSettings;
        }

        public ProjectSettings DeserializeFromFile(string path)
        {
            ProjectSettings result = serializer.DeserializeFromFile(path);
            return result;
        }

        public void SerializeToFile(string path, ProjectSettings settings)
        {
            serializer.SerializeToFile(path, settings);
        }
    }
}