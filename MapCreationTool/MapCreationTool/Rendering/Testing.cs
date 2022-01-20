using MapCreationTool.Controls;
using ISharp = SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Advanced;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Media3D;
using System.IO;

namespace MapCreationTool.Rendering
{
    class Testing
    {
        Model3DGroup model3DGroup;
        DiffuseMaterial frontMaterial;
        DiffuseMaterial backMaterial;
        MouseOrbitCamera mouseCamera;
        DeltaTime deltaTime;
        TerrainControl terrainControl;
        GeometryModel3D geometryModel;

        public Testing(TerrainControl terrainControl)
        {
            model3DGroup = new Model3DGroup();
            this.terrainControl = terrainControl;

            SetupView();
        }

        private void GenerateModels()
        {
            geometryModel = new GeometryModel3D();

            geometryModel.Material = frontMaterial;
            // Disabled for performance reasons
            //geometryModel.BackMaterial = backMaterial;

            // Add the geometry model to the model group.
            model3DGroup.Children.Add(geometryModel);
        }

        private MeshGeometry3D LoadHeightmap(string filePath)
        {
            MeshGeometry3D meshGeometry = new MeshGeometry3D();

            if (!File.Exists(filePath))
                return meshGeometry;

            Point3DCollection vertices = new Point3DCollection();

            // https://docs.sixlabors.com/articles/imagesharp/pixelbuffers.html
            // http://csharphelper.com/blog/2014/10/draw-surface-normals-on-a-3d-model-using-wpf-and-xaml/

            // Code inspired by: https://www.codeproject.com/Articles/1194994/Terrain-Generator-and-3D-WPF-Representation

            // Note: Casting, depending on the image type, this needs to be resolved somehow
            // Ideally we want 48 bit type here
            ISharp.Image<Rgba32> heightImage = (ISharp.Image<Rgba32>)ISharp.Image.Load(filePath);
            double halfWidth = heightImage.Width / 2.0;
            double halfHeight = heightImage.Height / 2.0;

            // To improve performance it would be nice to check if a point already exists
            // in that case it's coordinate could be reused instead of being added again
            // However this can become a pain when editing is later added

            int bit16 = 2 << (16 - 1);
            double minHeight = terrainControl.ProjectSettings.MinHeight;
            double maxHeight = terrainControl.ProjectSettings.MaxHeight;

            double colorScaleFactor = 10;

            for (int y = 0; y < heightImage.Height; y++)
            {
                Rgba32[]? row = heightImage.GetPixelRowMemory(y).ToArray();

                for (int x = 0; x < row.Length; x++)
                {
                    double xPos = x - halfWidth;
                    double yPos = y - halfHeight;
                    //double zPos = (test[x].R * 0.02) - halfHeight;
                    double zPos = Rescale(0, bit16, minHeight, maxHeight, row[x].R * colorScaleFactor);
                    Point3D point = new Point3D(xPos, yPos, zPos);
                    vertices.Add(point);
                }
            }

            Int32Collection vertexIndices = new Int32Collection();
            for (int y = 0; y < heightImage.Height - 1; y += 1)
            {
                for (int x = 0; x < heightImage.Width - 1; x += 1)
                {
                    int idx1 = x + y * heightImage.Width;
                    int idx2 = x + y * heightImage.Width;
                    int idx3 = x + (y + 1) * heightImage.Width;
                    vertexIndices.Add(idx1);
                    vertexIndices.Add(idx2 + 1);
                    vertexIndices.Add(idx3);

                    vertexIndices.Add(idx3 + 1);
                    vertexIndices.Add(idx3);
                    vertexIndices.Add(idx1 + 1);
                }
            }

            meshGeometry.Positions = vertices;
            meshGeometry.TriangleIndices = vertexIndices;

            return meshGeometry;
        }

        /// <summary>
        /// Rescales the given val defined by the range oldMin->oldMax to be in the range newMin->newMax
        /// </summary>
        /// <param name="oldMin"></param>
        /// <param name="oldMax"></param>
        /// <param name="newMin"></param>
        /// <param name="newMax"></param>
        /// <param name="val"></param>
        /// <returns></returns>
        private static double Rescale(double oldMin, double oldMax, double newMin, double newMax, double val)
        {
            // https://stackoverflow.com/questions/929103/convert-a-number-range-to-another-range-maintaining-ratio
            double oldDelta = oldMax - oldMin;
            double newDelta = newMax - newMin;

            double result = (((val - oldMin) * newDelta) / oldDelta) + newMin;

            return result;
        }

        internal void SetMeshGeometry(MeshGeometry3D geom)
        {
            geometryModel.Geometry = geom;
        }

        internal MeshGeometry3D LoadHeightmap()
        {
            // Criminal hack when there are no projectsettings
            if (terrainControl.ProjectSettings == null)
                return new MeshGeometry3D();

            MeshGeometry3D geometry = LoadHeightmap(terrainControl.ProjectSettings.HeightMapName);
            return geometry;
        }

        private void SetupView()
        {
            // Declare scene objects.
            Viewport3D viewport3D = terrainControl.viewPortMain;

            // Defines the camera used to view the 3D object. In order to view the 3D object,
            // the camera must be positioned and pointed such that the object is within view
            // of the camera.

            SetupLighting(model3DGroup);
            SetupMaterials();

            GenerateModels();

            // Add the group of models to the ModelVisual3d.
            terrainControl.modelVisual3D.Content = model3DGroup;

            SetupCamera(viewport3D);


            deltaTime = new DeltaTime();
            CompositionTarget.Rendering += CompositionTarget_Rendering;
        }

        // Main rendering loop
        private void CompositionTarget_Rendering(object sender, EventArgs e)
        {
            deltaTime.Update();

            mouseCamera?.Update(deltaTime);
        }

        private void SetupCamera(Viewport3D viewport)
        {
            //camera = new KeyboardCamera();
            mouseCamera = new MouseOrbitCamera(terrainControl);
            mouseCamera.XAngle = 60;
            mouseCamera.YAngle = 183;
            mouseCamera.Zoom = 1200;

            // Asign the camera to the viewport
            viewport.Camera = mouseCamera.perspectiveCamera;
            mouseCamera.Update(deltaTime);
        }

        private void SetupMaterials()
        {
            Color frontColor = Color.FromArgb(255, 100, 100, 250);
            Color backColor = Color.FromArgb(100, 100, 255, 250);

            frontMaterial = GetDiffuseMaterial(frontColor);
            backMaterial = GetDiffuseMaterial(backColor);
        }

        DiffuseMaterial GetDiffuseMaterial(Color color)
        {
            DiffuseMaterial material = new DiffuseMaterial(new SolidColorBrush(color));
            return material;
        }

        private void SetupLighting(Model3DGroup model3DGroup)
        {
            AmbientLight ambient = new AmbientLight(Colors.White);
            ambient.Color = Color.FromArgb(255, 50, 50, 50);
            model3DGroup.Children.Add(ambient);
            //return;
            // Define the lights cast in the scene. Without light, the 3D object cannot
            // be seen. Note: to illuminate an object from additional directions, create
            // additional lights.

            model3DGroup.Children.Add(CreateLight(Color.FromArgb(255, 150, 150, 150), new Vector3D(-1, -1, -0.5)));
            model3DGroup.Children.Add(CreateLight(Color.FromArgb(255, 150, 150, 150), new Vector3D(1, 1, 0.5)));

            model3DGroup.Children.Add(CreateLight(Color.FromArgb(255, 150, 150, 150), new Vector3D(0, 0, -1)));

            //DirectionalLight myDirectionalLight2 = CreateLight(new Vector3D(0.61, -0.5, -0.61));
            //model3DGroup.Children.Add(myDirectionalLight2);

            //DirectionalLight myDirectionalLight3 = CreateLight(new Vector3D(0.61, -0.5, 0.61));
            //model3DGroup.Children.Add(myDirectionalLight3);
        }

        private static DirectionalLight CreateLight(Color color, Vector3D direction)
        {
            DirectionalLight myDirectionalLight = new DirectionalLight();
            myDirectionalLight.Color = color;
            myDirectionalLight.Direction = direction;
            return myDirectionalLight;
        }
    }
}
