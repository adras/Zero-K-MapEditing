using MapCreationTool.Models;
using MapCreationTool.WPF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MapCreationTool.ViewModels
{
    public class CreateMapViewModel : PropertyChangeBase
    {
        private string workDir = "";
        private string mapName = "Zero-Prime";
        private WidthHeight mapSize = new WidthHeight(16, 12);

        public string WorkDir
        {
            get => workDir; 
            set
            {
                workDir = value;
                OnPropertyChanged();
            }
        }
        public string MapName
        {
            get => mapName; 
            set
            {
                mapName = value;
                OnPropertyChanged();
            }
        }
        public WidthHeight MapSize
        {
            get => mapSize;
            set
            {
                mapSize = value;
                OnPropertyChanged();
            }
        }
    }
}
