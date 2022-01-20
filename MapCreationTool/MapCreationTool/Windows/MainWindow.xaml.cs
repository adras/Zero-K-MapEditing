using MapCreationTool.Configuration;
using MapCreationTool.Lua;
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


			// Just for testing
			//try
			//{
			//	TestLuaEditor();
			//}
			//catch (Exception e)
			//{

			//}
		}

		private void TestLuaEditor()
		{
			LuaEditor editor = new LuaEditor();
			editor.Load(@"mapinfo.lua");

			int? test1 = editor.GetValue<int>("maphardness");
			bool? test2 = editor.GetValue<bool>("notDeformable");
			string test3 = editor.GetValue<string>("name");
			double test4 = editor.GetValue<double>("extractorRadius");

			editor.SetValue("maphardness", 99);
			editor.SetValue("notDeformable", true);
			editor.SetValue("name", "NewName");
			editor.SetValue("extractorRadius", 441.293);
			editor.Save("mapinfoTest.lua");
		}

		private void ctrlStart_OnMapOpened(object sender, MapPathInformation mapPathInfo)
		{
			ViewModel.Tabs.Add(new MapTab(mapPathInfo));
			ViewModel.SelectedTab = ViewModel.Tabs[ViewModel.Tabs.Count - 1];

			// Settings exist when map was opened previously, otherwise default settings will be created
			ViewModel.ProjectSettings = ProjectSettings.OpenOrCreateDefault(mapPathInfo);
		}



		private void ctrlStart_OnMapCreated(object sender, MapPathInformation mapPathInfo)
		{
			ViewModel.Tabs.Add(new MapTab(mapPathInfo));
			ViewModel.SelectedTab = ViewModel.Tabs[ViewModel.Tabs.Count - 1];


			// Settings don't exist so create default settings
			ViewModel.ProjectSettings = ProjectSettings.OpenOrCreateDefault(mapPathInfo);
		}

		private void ctrlStart_OnCalculatorClicked(object sender, EventArgs e)
		{
		}
	}
}
