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

namespace MapCreationTool.Rendering
{
	class Testing
	{
		Model3DGroup model3DGroup;
		DiffuseMaterial normalModelMaterial;
		MouseOrbitCamera mouseCamera;
		DeltaTime deltaTime;
		TerrainControl terrainControl;

		public Testing(TerrainControl terrainControl)
		{
			model3DGroup = new Model3DGroup();
			this.terrainControl = terrainControl;

			SetupView();
		}

		private void GenerateModels()
		{

			GeometryModel3D geometryModel = new GeometryModel3D();

			//geometryModel.Geometry = CreateHeightmapModel();
			// Dont forget assigning this instead of createheightmapmodel
			geometryModel.Geometry= LoadHeightmap("Rendering\\heightmap.bmp");


			geometryModel.Material = normalModelMaterial;
			geometryModel.BackMaterial = GetDefaultMaterial();
			//geometryModel.Transform = GetExampleTransform();

			// Add the geometry model to the model group.
			model3DGroup.Children.Add(geometryModel);

			// printer Bed
		}

		private MeshGeometry3D LoadHeightmap(string filePath)
		{
			MeshGeometry3D meshGeometry = new MeshGeometry3D();
			Point3DCollection vertices = new Point3DCollection();
			Int32Collection vertexIndices = new Int32Collection();


			// https://docs.sixlabors.com/articles/imagesharp/pixelbuffers.html

			// Code inspired by: https://www.codeproject.com/Articles/1194994/Terrain-Generator-and-3D-WPF-Representation

			// Note: Casting, depending on the image type, this needs to be resolved somehow
			// Ideally we want 48 bit type here
			ISharp.Image<Rgba32> heightImage = (ISharp.Image<Rgba32>)ISharp.Image.Load(filePath);
			double halfWidth = heightImage.Width / 2.0;
			double halfHeight = heightImage.Height / 2.0;

			for (int y = 0; y < heightImage.Height; y += 2)
			{
				Rgba32[]? test = heightImage.GetPixelRowMemory(y).ToArray();
				Rgba32[]? test2 = heightImage.GetPixelRowMemory(y + 1).ToArray();

				for (int x = 0; x < test.Length; x += 2)
				{
					double xPos = x-halfWidth;
					double yPos = y - halfHeight;
					double zPos = test[x].R - halfHeight;
					Point3D point = new Point3D(xPos, yPos, zPos);
					vertices.Add(point);
				}
			}
			Int32Collection triangleIndices = new Int32Collection();
			//defining triangles
			int ind1 = 0;
			int ind2 = 0;
			int xLenght = heightImage.Width;
			for (var y = 0; y < heightImage.Height - 1; y++)
			{
				for (var x = 0; x < heightImage.Width - 1; x++)
				{
					ind1 = x + y * (xLenght);
					ind2 = ind1 + (xLenght);

					//first triangle
					triangleIndices.Add(ind1);
					triangleIndices.Add(ind2 + 1);
					triangleIndices.Add(ind2);

					//second triangle
					triangleIndices.Add(ind1);
					triangleIndices.Add(ind1 + 1);
					triangleIndices.Add(ind2 + 1);
				}
			}


			meshGeometry.Positions = vertices;
			meshGeometry.TriangleIndices = triangleIndices;

			//meshGeometry.TriangleIndices = vertexIndices;

			return meshGeometry;
		}

		private MeshGeometry3D CreateHeightmapModel()
		{
			MeshGeometry3D meshGeometry = new MeshGeometry3D();
			Point3DCollection vertices = new Point3DCollection();
			Int32Collection vertexIndices = new Int32Collection();

			// For testing create a simple heightmap, which is a rectangle of 4 points on the same height
			List<List<int>> heightMap = new List<List<int>>()
			{
				new List<int> { 0, 0},
				new List<int> {0, 0}
			};

			// Just for testing create a simple triangle
			vertices.Add(new Point3D(0, 0, 0));
			vertices.Add(new Point3D(0, 1, 0));
			vertices.Add(new Point3D(-1, 1, 0));


			meshGeometry.Positions = vertices;
			//meshGeometry.TriangleIndices = vertexIndices;

			return meshGeometry;
		}

		//public static MeshGeometry3D CreateFromFacets()
		//{
		//    MeshGeometry3D myMeshGeometry3D = new MeshGeometry3D();

		//    Vector3DCollection normals = new Vector3DCollection();
		//    Point3DCollection vertices = new Point3DCollection();
		//    Int32Collection vertexIndices = new Int32Collection();

		//    int vertexIndex = 0;
		//    foreach (Facet facet in facets)
		//    {
		//        normals.Add(-VertexConverter.ConvertToVector3D(facet.Normal));
		//        foreach (Vertex vertex in facet.Vertices)
		//        {
		//            vertices.Add(VertexConverter.ConvertToPoint3D(vertex));
		//            vertexIndices.Add(vertexIndex);
		//            vertexIndex++;
		//        }
		//    }
		//    // Normal generation seems to be broken right now. WPF automatically generates normals when indices are set
		//    //myMeshGeometry3D.Normals = normals;

		//    myMeshGeometry3D.Positions = vertices;
		//    myMeshGeometry3D.TriangleIndices = vertexIndices;

		//    // get some information for debugging purposes
		//    double testMinX = vertices.Min(v => v.X);
		//    double testMinY = vertices.Min(v => v.Y);
		//    double testMinZ = vertices.Min(v => v.Z);

		//    double testMaxX = vertices.Max(v => v.X);
		//    double testMaxY = vertices.Max(v => v.Y);
		//    double testMaxZ = vertices.Max(v => v.Z);
		//    Rect3D bounds = myMeshGeometry3D.Bounds;


		//    return myMeshGeometry3D;
		//}


		private void SetupView()
		{
			// Declare scene objects.
			Viewport3D viewport3D = terrainControl.viewPortMain;
			ModelVisual3D modelVisual3D = new ModelVisual3D();
			// Defines the camera used to view the 3D object. In order to view the 3D object,
			// the camera must be positioned and pointed such that the object is within view
			// of the camera.

			SetupLighting(model3DGroup);
			SetupMaterials();

			GenerateModels();

			// Add the group of models to the ModelVisual3d.
			modelVisual3D.Content = model3DGroup;

			SetupCamera(viewport3D);

			//
			viewport3D.Children.Add(modelVisual3D);


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
			mouseCamera.XAngle = 50;
			mouseCamera.Zoom = 400;

			// Asign the camera to the viewport
			viewport.Camera = mouseCamera.perspectiveCamera;
		}

		private void SetupMaterials()
		{
			Color modelColor = Color.FromArgb(255, 100, 100, 250);
			Color transparentModelColor = Color.FromArgb(100, 100, 100, 250);

			normalModelMaterial = GetDiffuseMaterial(modelColor);
		}

		DiffuseMaterial GetDiffuseMaterial(Color color)
		{
			DiffuseMaterial material = new DiffuseMaterial(new SolidColorBrush(color));
			return material;
		}

		DiffuseMaterial GetDefaultMaterial()
		{
			// The material specifies the material applied to the 3D object. In this sample a
			// linear gradient covers the surface of the 3D object.

			// Create a horizontal linear gradient with four stops.
			LinearGradientBrush myHorizontalGradient = new LinearGradientBrush();
			myHorizontalGradient.StartPoint = new Point(0, 0.5);
			myHorizontalGradient.EndPoint = new Point(1, 0.5);
			myHorizontalGradient.GradientStops.Add(new GradientStop(Colors.Yellow, 0.0));
			myHorizontalGradient.GradientStops.Add(new GradientStop(Colors.Red, 0.25));
			myHorizontalGradient.GradientStops.Add(new GradientStop(Colors.Blue, 0.75));
			myHorizontalGradient.GradientStops.Add(new GradientStop(Colors.LimeGreen, 1.0));

			// Define material and apply to the mesh geometries.
			DiffuseMaterial myMaterial = new DiffuseMaterial(myHorizontalGradient);

			return myMaterial;
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
