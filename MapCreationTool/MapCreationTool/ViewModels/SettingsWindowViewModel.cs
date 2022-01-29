using MapCreationTool.WPF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MapCreationTool.ViewModels
{
    internal class SettingsWindowViewModel : PropertyChangedBase
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
                OnPropertyChanged();
            }
        }
    }
}
