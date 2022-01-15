using MapCreationTool.Models;
using MapCreationTool.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Forms = System.Windows.Forms;

namespace MapCreationTool.Windows
{
    /// <summary>
    /// Interaction logic for CreateMap.xaml
    /// </summary>
    public partial class CreateMap : Window
    {
        public CreateMapViewModel ViewModel { get; set; }

        public CreateMap()
        {
            InitializeComponent();
            DataContext = ViewModel = new CreateMapViewModel();
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
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
                ViewModel.WorkDir = dialog.SelectedPath;
            }
        }

        private void btnCreateMap_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(ViewModel.WorkDir))
            {
                MessageBox.Show("Please select or enter a path to a working directory for the new map");
                return;
            }
            if (string.IsNullOrEmpty(ViewModel.MapName))
            {
                MessageBox.Show("Please enter a map name");
            }

            string mapName = ViewModel.MapName;
            string workDir = ViewModel.WorkDir;

            int mapSizeX;
            int mapSizeY;

            if (ViewModel.MapSize.Width == 0)
            {
                MessageBox.Show("Could not create map. Invalid value for Map-Width. Make sure it's an integer value");
                return;
            }

            if (ViewModel.MapSize.Height == 0)
            {
                MessageBox.Show("Could not create map. Invalid value for Map-Width. Make sure it's an integer value");
                return;
            }

            MapSizeDefinition mapSizeDef = new MapSizeDefinition(ViewModel.MapSize);
            MapCreationResult result = MapCreationHelper.CreateMap(mapName, workDir, mapSizeDef);
            if (result.success == false)
            {
                MessageBox.Show(result.error);
                return;
            }

            //MessageBox.Show("Map creation successful");
            DialogResult = true;
            Close();
        }
    }
}
