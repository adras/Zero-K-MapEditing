using MapCreationTool.Models;
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
	/// Interaction logic for TechTestTerrainControl.xaml
	/// </summary>
	public partial class TechTestTerrainControl : UserControl
	{
		public ProjectSettings ProjectSettings
		{
			get { return (ProjectSettings)GetValue(ProjectSettingsProperty); }
			set { SetValue(ProjectSettingsProperty, value); }
		}

		public static readonly DependencyProperty ProjectSettingsProperty = DependencyProperty.Register(
			nameof(ProjectSettings),
			typeof(ProjectSettings),
			typeof(TechTestTerrainControl),
			new PropertyMetadata(null)
		);

		public TechTestTerrainControl()
		{
			InitializeComponent();
		}
	}
}
