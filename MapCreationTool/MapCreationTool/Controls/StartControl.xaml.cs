using MapCreationTool.Helpers;
using MapCreationTool.Models;
using MapCreationTool.Windows;
using System;
using System.Windows;
using System.Windows.Controls;
using Forms = System.Windows.Forms;

namespace MapCreationTool.Controls
{
    /// <summary>
    /// Interaction logic for StartControl.xaml
    /// </summary>
    public partial class StartControl : UserControl
    {
        public delegate void MapCreated(object sender, MapPathInformation mapPathInformation);
        public delegate void MapOpened(object sender, MapPathInformation mapPathInformation);


        public event MapCreated OnMapCreated;
        public event MapOpened OnMapOpened;
        public event EventHandler OnSettingsClicked;

        public StartControl()
        {
            InitializeComponent();
        }

        private void btnCreateMap_Click(object sender, RoutedEventArgs e)
        {
            CreateMap createMapWindow = new CreateMap();
            bool? result = createMapWindow.ShowDialog();

            if (result == true)
            {
                OnMapCreated(this, createMapWindow.ViewModel.MapPathInfo);
            }
        }

        private void btnOpenMap_Click(object sender, RoutedEventArgs e)
        {
            Forms.FolderBrowserDialog folderBrowser = new Forms.FolderBrowserDialog();
            Forms.DialogResult result = folderBrowser.ShowDialog();
            if (result != Forms.DialogResult.OK)
                return;

            string fullMapPath = folderBrowser.SelectedPath;

            MapPathInformation mapPathInfo = new MapPathInformation(fullMapPath);
            if (!PathHelper.IsMapDirectory(fullMapPath))
            {
                MessageBox.Show($"Could not open map directory. No mapinfo.lua exists at: {mapPathInfo.mapInfoPath}");
                return;
            }

            OnMapOpened(this, mapPathInfo);
        }


        private void btnSettings_Click(object sender, RoutedEventArgs e)
        {
            OnSettingsClicked?.Invoke(this, new EventArgs());
        }
    }
}
