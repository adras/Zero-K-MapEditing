﻿using MapCreationTool.Models;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Drawing.Processing;
using SixLabors.ImageSharp.Formats;
using SixLabors.ImageSharp.Formats.Bmp;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MapCreationTool.Images
{
	// Library overview: https://csharp-station.com/4-best-c-libraries-for-image-processing/
	// More libraries: https://stackoverflow.com/questions/5881031/image-processing-libraries-in-c-sharp
	// Maybe better than ImageProcessor: https://github.com/SixLabors/ImageSharp;
	/*
    Image formats
    Diffuse - multiple of 1024 - no alpha, bmp
    Height - 16 bit grayscale, bmp?
    metal - bmp? unknown color red
    featuremap - bmp? blue = grass, green 201-215 default tree - red ??
    grassmap - bmp?
    typemap- bmp? 
    minimap - 1024x1024
    */
	class ImageTest
	{

		public static void CreateImages(MapSizeDefinition mapSize, string mapDir)
		{
			//SixLabors.ImageSharp.PixelFormats.Rgb24
			Image<Rgb24> diffuse = new Image<Rgb24>(mapSize.diffuseMapSize.width, mapSize.diffuseMapSize.height);
			Image<Rgb24> grass = new Image<Rgb24>(mapSize.grassMapSize.width, mapSize.grassMapSize.height);
			Image<Rgb24> height = new Image<Rgb24>(mapSize.heightMapSize.width, mapSize.heightMapSize.height);
			Image<Rgb24> metal = new Image<Rgb24>(mapSize.metalMapSize.width, mapSize.metalMapSize.height);

			// Fill images with an arbitrary background color, otherwise images would be empty files
			diffuse.Mutate(x => x.Fill(Color.Aqua));
			grass.Mutate(x => x.Fill(Color.Purple));
			height.Mutate(x => x.Fill(Color.PeachPuff));
			metal.Mutate(x => x.Fill(Color.Sienna));

			BmpEncoder encoder = new BmpEncoder();

			WriteImage(encoder, diffuse, $"{mapDir}\\diffuse.bmp");
			WriteImage(encoder, grass, $"{mapDir}\\grass.bmp");
			WriteImage(encoder, height,$"{mapDir}\\height.bmp");
			WriteImage(encoder, metal, $"{mapDir}\\metal.bmp");
		}

		private static void WriteImage<T>(IImageEncoder encoder, Image<T> image, string fileName) where T: unmanaged, IPixel<T>
		{
			using (Stream stream = File.Create(fileName))
			{
				encoder.Encode<T>(image, stream);
			}
		}
	}
}
