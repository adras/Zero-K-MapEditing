using MapCreationTool.Extensions;
using MapCreationTool.Models;
using MapCreationTool.Tabs;
using MapCreationTool.ViewModels;
using System;
using System.Windows;

namespace MapCreationTool.Windows
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {


        public MainWindowViewModel ViewModel { get; set; }

        public MainWindow()
        {
            InitializeComponent();
            DataContext = ViewModel = new MainWindowViewModel();


        }

        private void ctrlStart_OnMapOpened(object sender, MapPathInformation mapPathInfo)
        {
            MapTab newTab = new MapTab();
            newTab.LoadProjectSettings(mapPathInfo);
            newTab.LoadMapInfo();

            ViewModel.Tabs.Add(newTab);
            ViewModel.SelectedTab = ViewModel.Tabs[ViewModel.Tabs.Count - 1];
        }

        private void ctrlStart_OnMapCreated(object sender, MapPathInformation mapPathInfo)
        {
            MapTab newTab = new MapTab();
            newTab.LoadProjectSettings(mapPathInfo);
            newTab.LoadMapInfo();

            ViewModel.Tabs.Add(newTab);
            ViewModel.SelectedTab = ViewModel.Tabs[ViewModel.Tabs.Count - 1];
        }


        private void ctrlStart_OnSettingsClicked(object sender, EventArgs e)
        {
            SettingsWindow window = new SettingsWindow();
            window.Center(this);
            bool? result = window.ShowDialog();
            if (result == false)
                return;
        }

    }
}
