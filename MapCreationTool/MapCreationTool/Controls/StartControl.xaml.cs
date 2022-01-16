﻿using MapCreationTool.Windows;
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
using MapCreationTool.Models;

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

            OnMapOpened(this, mapPathInfo);
        }


        private void btnSettings_Click(object sender, RoutedEventArgs e)
        {
            OnSettingsClicked?.Invoke(this, new EventArgs());
        }
    }
}
