using MapCreationTool.Models;
using MapCreationTool.NewRendering;
using MapCreationTool.NewRendering.HitTest;
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
        private bool useTexture = true;

        public ProjectSettings ProjectSettings
        {
            get { return (ProjectSettings)GetValue(ProjectSettingsProperty); }
            set { SetValue(ProjectSettingsProperty, value); }
        }

        public bool UseTexture
        {
            get
            {
                return useTexture;
            }

            set
            {
                useTexture = value;
                renderer.useTexture = value;
            }
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
            renderer.Update();
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
                lblPitch.Content = $"Pitch: {renderer.camera.Pitch}";
                lblYaw.Content = $"Yaw: {renderer.camera.Yaw}";
                lblPosition.Content = $"Pos: {renderer.camera.Position}";
                lblLookAt.Content = $"LookAt: {renderer.camera.Front}";
            }
            float width = (float)this.ActualWidth;
            float height = (float)this.ActualHeight;

            Vector3 centeredMouse = new Vector3(mouseVect.X, mouseVect.Y, 0);
            Vector3 screenSize = new Vector3(width, height, 0);
            centeredMouse.X = 2.0f * centeredMouse.X / screenSize.X - 1f;
            centeredMouse.Y = 2.0f * centeredMouse.Y / screenSize.Y - 1f;
            centeredMouse.Z = -1;
            //centeredMouse.Y *= -1;


            Matrix4 proj = renderer.camera.GetProjectionMatrix();
            Matrix4 view = renderer.camera.GetViewMatrix();
            //Matrix4 newMatrix = Matrix4.Invert(view * proj);
            //Matrix4 model = Matrix4.Identity;
            //Matrix4 view = Matrix4.LookAt(new Vector3(0, 0, 2), new Vector3(0, 0, 0), new Vector3(0, 1, 0));
            //Matrix4 proj = Matrix4.CreatePerspectiveFieldOfView(MathHelper.PiOver2, 4.0f / 3.0f, 0.01f, 10f);
            Matrix4 newMatrix = view * proj;

            newMatrix = Matrix4.Invert(newMatrix);

            // Base on: https://stackoverflow.com/questions/51116554/simple-opentk-raycasting
            Vector4 mouseNear = new Vector4(centeredMouse.X, centeredMouse.Y, 0, 1);
            Vector4 mouseFar = new Vector4(centeredMouse.X, centeredMouse.Y, 1, 1);
            Vector4 rayNear = mouseNear * newMatrix;
            Vector4 rayFar = mouseFar * newMatrix;

            rayNear = (1 / rayNear.W) * rayNear;
            rayFar = (1 / rayFar.W) * rayFar;

            // Debug.WriteLine($"centeredMouse: {centeredMouse} - Near: {rayNear}, Far: {rayFar}");
            // Debug.WriteLine($"TEST: {rayNear * (10.0f / 0.1f)}");

            lastPos = mouseVect;
        }

        bool initialized = false;
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

            if (!initialized)
            {
                openTk.Start(settings);
                renderer.Startup(ProjectSettings.CompilationSettings.HeightMapName, ProjectSettings.CompilationSettings.DiffuseMapName);
                openTk.Render += OpenTk_Render;
            }

            initialized = true;
        }

        private void UserControl_MouseUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (e.ChangedButton != MouseButton.Left)
                return;

            float width = (float)this.ActualWidth;
            float height = (float)this.ActualHeight;
            Point mousePos = e.GetPosition(this);
            Vector3 mouseVect = new Vector3((float)mousePos.X, (float)mousePos.Y, 0);

            Vector3 centeredMouse = new Vector3(mouseVect.X, mouseVect.Y, 0);
            Vector3 screenSize = new Vector3(width, height, 0);
            centeredMouse.X = 2.0f * centeredMouse.X / screenSize.X - 1f;
            centeredMouse.Y = 2.0f * centeredMouse.Y / screenSize.Y - 1f;
            centeredMouse.Z = -1;
            //centeredMouse.Y *= -1;


            Matrix4 proj = renderer.camera.GetProjectionMatrix();
            Matrix4 view = renderer.camera.GetViewMatrix();
            //Matrix4 newMatrix = Matrix4.Invert(view * proj);
            //Matrix4 model = Matrix4.Identity;
            //Matrix4 view = Matrix4.LookAt(new Vector3(0, 0, 2), new Vector3(0, 0, 0), new Vector3(0, 1, 0));
            //Matrix4 proj = Matrix4.CreatePerspectiveFieldOfView(MathHelper.PiOver2, 4.0f / 3.0f, 0.01f, 10f);
            Matrix4 newMatrix = view * proj;

            newMatrix = Matrix4.Invert(newMatrix);

            // Base on: https://stackoverflow.com/questions/51116554/simple-opentk-raycasting
            Vector4 mouseNear = new Vector4(centeredMouse.X, centeredMouse.Y, 0, 1);
            Vector4 mouseFar = new Vector4(centeredMouse.X, centeredMouse.Y, 1, 1);
            Vector4 rayNear = mouseNear * newMatrix;
            Vector4 rayFar = mouseFar * newMatrix;

            rayNear = (1 / rayNear.W) * rayNear;
            rayFar = (1 / rayFar.W) * rayFar;


            Vector3 origin = new Vector3(rayNear);
            Vector3 target = new Vector3(rayFar);
            Ray ray = new Ray(origin, target);
            Debug.WriteLine($"Ray: {ray}");

            Vector3? result = Madness.GetHit(renderer.imageData, ray);
            Debug.WriteLine($"Hit: {result}");
        }

        private void UserControl_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            renderer.camera.AspectRatio = (float)e.NewSize.Width / (float)e.NewSize.Height;
        }
    }
}
