using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using Forms = System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.IO;
using MapCreationTool.Models;

namespace MapCreationTool
{
    /// <summary>
    /// Interaction logic for CreateMapPage.xaml
    /// </summary>
    public partial class SizeCalculatorPage : Page
    {
        public SizeCalculatorPage()
        {
            InitializeComponent();
        }

        private void tbMapSizeX_TextChanged(object sender, TextChangedEventArgs e)
        {
            CalculateMapSize();
        }

        private void tbMapSizeY_TextChanged(object sender, TextChangedEventArgs e)
        {
            CalculateMapSize();
        }

        private void CalculateMapSize()
        {
            /*
## Heightmap
* Calculate the size of your heightmap image
* Formula: pixels = MapSize * 64 + 1

* Create an image with the calculated dimensions in your favorite program

## Metalmap
* Same steps as for the heightmap, but use: Formula: pixels = MapSize * 32 instead

## Diffusemap
* Same steps as for heightmap, but use Formula: pixels = MapSize * 512 instead
 */
            int mapSizeX;
            int mapSizeY;

            int.TryParse(tbMapSizeX.Text, out mapSizeX);
            int.TryParse(tbMapSizeY.Text, out mapSizeY);

            MapSizeDefinition mapSizeDef = new MapSizeDefinition(new WidthHeight(mapSizeX, mapSizeY));

            tbDiffuseMapSize.Text = $"{mapSizeDef.diffuseMapSize.width} / {mapSizeDef.diffuseMapSize.height}";
            tbHeightMapSize.Text = $"{mapSizeDef.heightMapSize.width} / {mapSizeDef.heightMapSize.height}";
            tbMetalMapSize.Text = $"{mapSizeDef.metalMapSize.width} / {mapSizeDef.metalMapSize.height}";
            tbGrassMapSize.Text = $"{mapSizeDef.grassMapSize.width} / {mapSizeDef.grassMapSize.height}";
        }

    }
}
