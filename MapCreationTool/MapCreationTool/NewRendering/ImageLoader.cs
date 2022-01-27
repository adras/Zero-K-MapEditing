﻿using OpenTK.Mathematics;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Advanced;
using SixLabors.ImageSharp.PixelFormats;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MapCreationTool.NewRendering
{
	class ImageData
	{

	}



	class ImageLoader
	{
		// Create data wich can be used for opengl buffers
		// https://opentk.net/learn/chapter1/2-hello-triangle.html

		void LoadImage(string filePath, float minHeight, float maxHeight)
		{
			Image<Rgba32> heightImage = (Image<Rgba32>)Image.Load(filePath);

			float halfWidth = heightImage.Width / 2.0f;
			float halfHeight = heightImage.Height / 2.0f;
			int bit16 = 2 << (16 - 1);

			float colorScaleFactor = 10;

			// Although the tutorial uses floats, let's try with vectors
			// This could also be an array, since size isn't changed
			// size should be widht * height OR width * height * 3 if floats are used
			List<Vector3> vectors = new List<Vector3>();
			for (int y = 0; y < heightImage.Height; y++)
			{
				Rgba32[]? row = heightImage.GetPixelRowMemory(y).ToArray();

				for (int x = 0; x < row.Length; x++)
				{
					float xPos = x - halfWidth;
					float yPos = y - halfHeight;
					//double zPos = (test[x].R * 0.02) - halfHeight;
					float zPos = Rescale(0, bit16, minHeight, maxHeight, row[x].R * colorScaleFactor);
					//Point3D point = new Point3D(xPos, yPos, zPos);
					//vertices.Add(point);
					Vector3 vector = new Vector3(xPos, yPos, zPos);
					vectors.Add(vector);
				}
			}
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
		private static float Rescale(float oldMin, float oldMax, float newMin, float newMax, float val)
		{
			// https://stackoverflow.com/questions/929103/convert-a-number-range-to-another-range-maintaining-ratio
			float oldDelta = oldMax - oldMin;
			float newDelta = newMax - newMin;

			float result = (((val - oldMin) * newDelta) / oldDelta) + newMin;
			return result;
		}


			//private MeshGeometry3D LoadHeightmap(string filePath)
			//{
			//	MeshGeometry3D meshGeometry = new MeshGeometry3D();

			//	if (!File.Exists(filePath))
			//		return meshGeometry;

			//	Point3DCollection vertices = new Point3DCollection();

			//	// https://docs.sixlabors.com/articles/imagesharp/pixelbuffers.html
			//	// http://csharphelper.com/blog/2014/10/draw-surface-normals-on-a-3d-model-using-wpf-and-xaml/

			//	// Code inspired by: https://www.codeproject.com/Articles/1194994/Terrain-Generator-and-3D-WPF-Representation

			//	// Note: Casting, depending on the image type, this needs to be resolved somehow
			//	// Ideally we want 48 bit type here
			//	heightImage = (ISharp.Image<Rgba32>)ISharp.Image.Load(filePath);
			//	double halfWidth = heightImage.Width / 2.0;
			//	double halfHeight = heightImage.Height / 2.0;

			//	// To improve performance it would be nice to check if a point already exists
			//	// in that case it's coordinate could be reused instead of being added again
			//	// However this can become a pain when editing is later added

			//	int bit16 = 2 << (16 - 1);
			//	double minHeight = terrainControl.ProjectSettings.CompilationSettings.MinHeight;
			//	double maxHeight = terrainControl.ProjectSettings.CompilationSettings.MaxHeight;

			//	double colorScaleFactor = 10;

			//	for (int y = 0; y < heightImage.Height; y++)
			//	{
			//		Rgba32[]? row = heightImage.GetPixelRowMemory(y).ToArray();

			//		for (int x = 0; x < row.Length; x++)
			//		{
			//			double xPos = x - halfWidth;
			//			double yPos = y - halfHeight;
			//			//double zPos = (test[x].R * 0.02) - halfHeight;
			//			double zPos = Rescale(0, bit16, minHeight, maxHeight, row[x].R * colorScaleFactor);
			//			Point3D point = new Point3D(xPos, yPos, zPos);
			//			vertices.Add(point);
			//		}
			//	}

			//	Int32Collection vertexIndices = new Int32Collection();
			//	for (int y = 0; y < heightImage.Height - 1; y += 1)
			//	{
			//		for (int x = 0; x < heightImage.Width - 1; x += 1)
			//		{
			//			int idx1 = x + y * heightImage.Width;
			//			int idx2 = x + y * heightImage.Width;
			//			int idx3 = x + (y + 1) * heightImage.Width;
			//			vertexIndices.Add(idx1);
			//			vertexIndices.Add(idx2 + 1);
			//			vertexIndices.Add(idx3);

			//			vertexIndices.Add(idx3 + 1);
			//			vertexIndices.Add(idx3);
			//			vertexIndices.Add(idx1 + 1);
			//		}
			//	}

			//	meshGeometry.Positions = vertices;
			//	meshGeometry.TriangleIndices = vertexIndices;

			//	return meshGeometry;
			//}
		}
	}