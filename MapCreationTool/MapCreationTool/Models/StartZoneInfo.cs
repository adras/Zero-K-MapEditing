using MapCreationTool.WPF;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace MapCreationTool.Models
{
    public class StartZoneInfo : PropertyChangedBase
    {
        string name;
        string shortName;
        PointCollection boxCoords;

        public string Name
        {
            get => name;
            set
            {
                name = value; 
                OnPropertyChanged();
            }
        }

        public string ShortName
        {
            get => shortName;
            set
            {
                shortName = value;
                OnPropertyChanged();
            }
        }

        public PointCollection BoxCoords
        {
            get => boxCoords;
            set
            {
                boxCoords = value; 
                OnPropertyChanged();
            }
        }


        public StartZoneInfo()
        {
            BoxCoords = new PointCollection();
            
        }

        internal void AddPointNotify(Point mousePos)
        {
            BoxCoords.Add(mousePos);
            OnPropertyChanged(nameof(BoxCoords));
        }
    }
}
