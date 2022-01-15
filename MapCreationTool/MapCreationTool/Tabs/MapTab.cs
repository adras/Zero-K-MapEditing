using MapCreationTool.Controls;
using MapCreationTool.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace MapCreationTool.Tabs
{
    internal class MapTab : TabBase
    {
        private MapPathInformation mapPathInfo;

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

        public MapTab(MapPathInformation pathInfo)
        {
            this.mapPathInfo = pathInfo;
            this.header = mapPathInfo.mapName;
            this.content = new EditMapControl();
        }
    }
}
