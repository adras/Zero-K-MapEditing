using MapCreationTool.Configuration;
using MapCreationTool.Lua;
using MapCreationTool.Models;
using MapCreationTool.NewRendering;
using MapCreationTool.Tabs;
using MapCreationTool.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

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

		private void ctrlStart_OnCalculatorClicked(object sender, EventArgs e)
		{
		}
	}
}
