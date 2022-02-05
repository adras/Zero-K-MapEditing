﻿using MapCreationTool.Models;
using MapCreationTool.NewRendering;
using MapCreationTool.NewRendering.HitTest;
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
            typeof(TerrainControl),
            new PropertyMetadata(null)
        );

        public TerrainControl()
        {
            InitializeComponent();
            renderer = new TerrainRenderer();

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

            float width = (float)this.ActualWidth;
            float height = (float)this.ActualHeight;
            Point mousePos = e.GetPosition(this);
            Vector3 mouseVect = new Vector3((float)mousePos.X, (float)mousePos.Y, 0);

            Vector3 centeredMouse = new Vector3(mouseVect.X, mouseVect.Y, 0);
            Vector3 screenSize = new Vector3(width, height, 0);
            centeredMouse.X = 2.0f * centeredMouse.X / screenSize.X - 1f;
            centeredMouse.Y = 2.0f * centeredMouse.Y / screenSize.Y - 1f;
            centeredMouse.Z = -1;


            Matrix4 proj = renderer.camera.GetProjectionMatrix();
            Matrix4 view = renderer.camera.GetViewMatrix();
            Matrix4 newMatrix = view * proj;

            newMatrix = Matrix4.Invert(newMatrix);

            // Base on: https://stackoverflow.com/questions/51116554/simple-opentk-raycasting
            Vector4 mouseNear = new Vector4(centeredMouse.X, -centeredMouse.Y, 0, 1);
            Vector4 mouseFar = new Vector4(centeredMouse.X, -centeredMouse.Y, 1, 1);
            Vector4 rayNear = mouseNear * newMatrix;
            Vector4 rayFar = mouseFar * newMatrix;

            rayNear = (1 / rayNear.W) * rayNear;
            rayFar = (1 / rayFar.W) * rayFar;


            Vector3 origin = new Vector3(rayNear);
            Vector3 target = new Vector3(rayFar);
            Ray ray = new Ray(origin, target);
            Debug.WriteLine($"Ray: {ray}");

            HitInfo result = Madness.GetHit(renderer.imageData, ray);
            
            Debug.WriteLine($"Hit: {result?.hitLocation}");
            if (result == null)
                return;

            int vHitIdx = (int)result.vIdxA;

            // Do some rectangle manipulation
            // NOTE: TODO: Normals also need to be recalculated
            for (int x = 0; x < 8; x++)
            {
                for (int y = 0; y < 8; y++)
                {
                    int offset = (x + y * renderer.imageData.width) * (3 + 3 + 2);
                    int xIdx = vHitIdx + offset;

                    // Plus two, because we would like to set the Z-Coordinate
                    int targetIdx = xIdx + 2;
                    if (targetIdx >= renderer.imageData.vertices.Length || targetIdx < 0)
                        continue;

                    renderer.imageData.vertices[targetIdx] += 5.0f;
                }
            }

            renderer.UpdateImageData();
        }

        private void UserControl_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            renderer.camera.ScreenWidth = (int)e.NewSize.Width;
            renderer.camera.ScreenHeight = (int)e.NewSize.Height;
        }
    }
}
