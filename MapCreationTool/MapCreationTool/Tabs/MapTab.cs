using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MapCreationTool.Tabs
{
    internal class MapTab : TabBase
    {
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

        public MapTab(string mapName, object content)
        {
            this.header = mapName;
            this.content = content;
        }
    }
}
