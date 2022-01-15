using MapCreationTool.WPF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MapCreationTool.Tabs
{
    public abstract class TabBase : PropertyChangedBase
    {
        string header;
        object content;

        public abstract string Header { get; set; }
        public abstract object Content { get; set; }

    }

    internal class MapTab : TabBase
    {
        string header;
        object content;

        public override string Header
        {
            get => header;
            set
            {
                header = value;
                OnPropertyChanged();
            }
        }
        public override object Content 
        {
            get => content;
            set
            {
                content = value;
                OnPropertyChanged();
            }
        }

        public MapTab(string mapName, object content)
        {
            this.Header = mapName;
            this.content = content;
        }
    }
}
