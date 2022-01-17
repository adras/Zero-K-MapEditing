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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MapCreationTool.Controls
{
	/// <summary>
	/// Interaction logic for MapSettingsControl.xaml
	/// </summary>
	public partial class ProjectSettings : UserControl
	{
		public string SettingHeightMap
		{
			get { return (string)GetValue(SettingHeightMapProperty); }
			set { SetValue(SettingHeightMapProperty, value); }
		}

		public static readonly DependencyProperty SettingHeightMapProperty = DependencyProperty.Register(
			nameof(SettingHeightMap),
			typeof(string),
			typeof(CompileSetting),
			new PropertyMetadata("")
		);

		public string SettingDiffuseMap
		{
			get { return (string)GetValue(SettingDiffuseMapProperty); }
			set { SetValue(SettingDiffuseMapProperty, value); }
		}

		public static readonly DependencyProperty SettingDiffuseMapProperty = DependencyProperty.Register(
			nameof(SettingDiffuseMap),
			typeof(string),
			typeof(CompileSetting),
			new PropertyMetadata("")
		);


		public ProjectSettings()
		{
			InitializeComponent();
		}
	}
}
