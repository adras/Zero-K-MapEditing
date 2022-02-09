using MapCreationTool.Models;
using MapCreationTool.Terrain;
using MapCreationTool.Terrain.HitTest;
using OpenTK.Mathematics;
using OpenTK.Wpf;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace MapCreationTool.Controls
{
    /// <summary>
    /// Interaction logic for TechTestTerrainControl.xaml
    /// </summary>
    public partial class TerrainControl : UserControl
    {
        TerrainRenderer renderer;
        TerrainEditor editor;
        private bool useTexture = true;

        BrushTypes selectedBrushType;
        
        public BrushTypes SelectedBrushType { get => selectedBrushType; set => selectedBrushType = value; }

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
            typeof(TerrainControl),
            new PropertyMetadata(null)
        );

        public TerrainControl()
        {
            InitializeComponent();
            renderer = new TerrainRenderer();
            editor = new TerrainEditor();

            GLWpfControlSettings settings = new GLWpfControlSettings
            {
                MajorVersion = 2,
                MinorVersion = 1,
            };

            openTk.Start(settings);
            openTk.Render += OpenTk_Render;
        }

        private void OpenTk_Render(TimeSpan obj)
        {
            renderer.Update();
            renderer.Render();
        }


        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {

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

            lastPos = mouseVect;
        }


        private void UserControl_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if ((bool)e.NewValue != true)
                return;

            renderer.Startup(ProjectSettings.CompilationSettings.HeightMapName, ProjectSettings.CompilationSettings.DiffuseMapName);
        }

        private void UserControl_MouseUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (e.ChangedButton != MouseButton.Left)
                return;

            Point mousePos = e.GetPosition(this);
            Ray ray = renderer.camera.ScreenToPointRay(mousePos);


            HitInfo result = Madness.GetHit(renderer.vertexData, ray);
            if (result == null)
                return;
            float brushStrength = (float)sliderStrength.Value;
            float brushSize = (float)sliderSize.Value;
            editor.DrawSmoothBrush(result, renderer.vertexData, brushSize, brushStrength);

            renderer.UpdateImageData();
        }

        private void UserControl_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            renderer.camera.ScreenSize = new Vector2((int)e.NewSize.Width, (int)e.NewSize.Height);
        }
    }
}
