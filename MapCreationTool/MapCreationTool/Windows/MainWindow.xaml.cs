using MapCreationTool.Configuration;
using MapCreationTool.Models;
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
			ViewModel.Tabs.Add(new MapTab(mapPathInfo));

			ProjectSettingsSerializer serializer = new ProjectSettingsSerializer();
			ProjectSettings projectSettings;
			if (!File.Exists(mapPathInfo.settingsPath))
			{
				projectSettings = serializer.CreateDefault();
				serializer.SerializeToFile(mapPathInfo.settingsPath, projectSettings);
			}
			else
			{
				projectSettings = serializer.DeserializeFromFile(mapPathInfo.settingsPath);
			}
			ViewModel.ProjectSettings = projectSettings;
		}

		private void ctrlStart_OnMapCreated(object sender, MapPathInformation mapPathInfo)
		{
			ViewModel.Tabs.Add(new MapTab(mapPathInfo));

		}

		private void ctrlStart_OnCalculatorClicked(object sender, EventArgs e)
		{

		}
	}
}
