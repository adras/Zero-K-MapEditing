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
    public partial class CreateMapPage : Page
    {
        public CreateMapPage()
        {
            InitializeComponent();
        }

        private void btnSelectWorkingDir_Click(object sender, RoutedEventArgs e)
        {
            // Move this to dialogHelper.GetWorkDirectory method
            Forms.FolderBrowserDialog dialog = new Forms.FolderBrowserDialog();
            dialog.ShowNewFolderButton = true;
            dialog.AutoUpgradeEnabled = true;
            dialog.Description = "Select Map WorkDirectory";
            Forms.DialogResult result = dialog.ShowDialog();
            if (result == Forms.DialogResult.OK)
            {
                tbMapWorkingDir.Text = dialog.SelectedPath;
            }
        }

        private void btnCreateMap_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(tbMapWorkingDir.Text))
            {
                MessageBox.Show("Please select or enter a path to a working directory for the new map");
                return;
            }
            if (string.IsNullOrEmpty(tbMapName.Text))
            {
                MessageBox.Show("Please enter a map name");
            }

            string mapName = tbMapName.Text;
            string workDir = tbMapWorkingDir.Text;

            int mapSizeX;
            int mapSizeY;

            if (!int.TryParse(tbMapWidth.Text, out mapSizeX))
            {
                MessageBox.Show("Could not create map. Invalid value for Map-Width. Make sure it's an integer value");
                return;
            }

            if (!int.TryParse(tbMapHeight.Text, out mapSizeY))
            {
                MessageBox.Show("Could not create map. Invalid value for Map-Width. Make sure it's an integer value");
                return;
            }
            MapSizeDefinition mapSizeDef = new MapSizeDefinition(new WidthHeight(mapSizeX, mapSizeY));

            MapCreationResult result = MapCreationHelper.CreateMap(mapName, workDir, mapSizeDef);
            if (result.success == false)
            {
                MessageBox.Show(result.error);
                return;
            }

            MessageBox.Show("Map creation successful");
        }
    }
}
