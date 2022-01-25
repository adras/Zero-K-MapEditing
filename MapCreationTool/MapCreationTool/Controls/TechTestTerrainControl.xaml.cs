using MapCreationTool.Models;
using MapCreationTool.NewRendering;
using OpenTK.Windowing.Common;
using OpenTK.Wpf;
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
		TerrainRenderer renderer;

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

			GLWpfControlSettings settings = new GLWpfControlSettings
			{
				MajorVersion = 2,
				MinorVersion = 1
			};
			renderer = new TerrainRenderer();
			openTk.Start(settings);
			renderer.Ready();
			openTk.Render += OpenTk_Render;
		}

		private void OpenTk_Render(TimeSpan obj)
		{
			renderer.Render();
		}

		private void openTkControl_Render(TimeSpan obj)
		{

		}
	}
}
