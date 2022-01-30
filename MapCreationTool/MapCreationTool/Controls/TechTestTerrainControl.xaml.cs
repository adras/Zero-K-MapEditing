using MapCreationTool.Models;
using MapCreationTool.NewRendering;
using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using OpenTK.Wpf;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Interop;
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


			renderer = new TerrainRenderer();
		}

		private void OpenTk_Render(TimeSpan obj)
		{
			renderer.Render();
		}

		private void openTkControl_Render(TimeSpan obj)
		{

		}

		private void UserControl_Loaded(object sender, RoutedEventArgs e)
		{
			// GLFWGraphicsContext context = new GLFWGraphicsContext()
			// Exception, memory corrupt when using this context
			//Window currentWindow = Window.GetWindow(this);
			//IntPtr handle = new WindowInteropHelper(currentWindow).Handle;
			//GLFWGraphicsContext context = null;
			//unsafe
			//{
			//	// Requires a OpenTK.Windowing.GraphicsLibraryFramework.Window
			//	context = new GLFWGraphicsContext((OpenTK.Windowing.GraphicsLibraryFramework.Window*)handle.ToPointer());
			//}



		}

		private void UserControl_PreviewKeyDown(object sender, KeyEventArgs e)
		{
			Vector3 delta = Vector3.Zero;
			switch (e.Key)
			{
				case Key.A:
					delta = -renderer.camera.Right * 2;
					break;
				case Key.D:
					delta = renderer.camera.Right * 2;
					break;
				case Key.W:
					delta = renderer.camera.Front * 2;
					break;
				case Key.S:
					delta = -renderer.camera.Front * 2;
					break;

			}
			renderer.Move(delta);
		}
		Vector3 lastPos = Vector3.Zero;
		private void UserControl_MouseMove(object sender, MouseEventArgs e)
		{
			Point mousePoint = e.GetPosition(openTk);
			Vector3 mouseVect = new Vector3((float)mousePoint.X, (float)mousePoint.Y, 0);
			Vector3 delta;
			if (lastPos != Vector3.Zero)
				delta = mouseVect - lastPos;
			else
				delta = Vector3.Zero;

			if (e.RightButton == MouseButtonState.Pressed)
			{
				renderer.MouseMove(delta);
			}

			lastPos = mouseVect;
		}

		private void UserControl_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
		{
			if ((bool)e.NewValue != true)
				return;

			GLWpfControlSettings settings = new GLWpfControlSettings
			{
				MajorVersion = 2,
				MinorVersion = 1,
				//ContextToUse = context
			};

			openTk.Start(settings);
			renderer.Startup(ProjectSettings.CompilationSettings.HeightMapName);
			openTk.Render += OpenTk_Render;

		}

  
    }
}
