﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MapCreationTool.Configuration
{
    public class ProgramSettings
    {
        private string gameDirectory;

        public string GameDirectory
        {
            get
            {
                return gameDirectory;
            }

            set
            {
                gameDirectory = value;
            }
        }

        public static ProgramSettings CreateDefault()
        {
            ProgramSettings defaultSettings = new ProgramSettings
            {

            };
            return defaultSettings;
        }
    }
}
