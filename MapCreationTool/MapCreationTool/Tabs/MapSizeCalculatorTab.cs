using MapCreationTool.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MapCreationTool.Tabs
{
    internal class MapSizeCalculatorTab : TabBase
    {
        MapSizeCalculator sizeCalculatorControl;

        public override string Header { get => "Map Size Calculator"; }
        public override object Content { get => sizeCalculatorControl; }

        public MapSizeCalculatorTab()
        {
            sizeCalculatorControl = new MapSizeCalculator();
        }
    }
}
