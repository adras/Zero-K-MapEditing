using MapCreationTool.Models;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Drawing.Processing;
using SixLabors.ImageSharp.Formats;
using SixLabors.ImageSharp.Formats.Bmp;
using SixLabors.ImageSharp.Formats.Png;
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
    Height - 16 bit grayscale, bmp? - Should definitelly be 16-bit
    metal - bmp? unknown color red
    featuremap - bmp? blue = grass, green 201-215 default tree - red ??
    grassmap - bmp?
    typemap- bmp? 
    minimap - 1024x1024
    */
    class ImageCreator
	{

		public static void CreateImages(MapSizeDefinition mapSize, string mapDir, ProjectSettings projectSettings)
		{
			CompilationSettings settings = projectSettings.CompilationSettings;

			// Alternative to Imagesharp: https://csharp.hotexamples.com/examples/System.Windows.Media.Imaging/PngBitmapEncoder/-/php-pngbitmapencoder-class-examples.html
			// Type: PngBitmapEncoder -> Windows namespace

			Image<Rgb24> diffuse = new Image<Rgb24>(mapSize.DiffuseMapSize.Width, mapSize.DiffuseMapSize.Height);
			Image<Rgb24> grass = new Image<Rgb24>(mapSize.GrassMapSize.Width, mapSize.GrassMapSize.Height);
			Image<L16> height = new Image<L16>(mapSize.HeightMapSize.Width, mapSize.HeightMapSize.Height);
			Image<Rgb24> metal = new Image<Rgb24>(mapSize.MetalMapSize.Width, mapSize.MetalMapSize.Height);
			
			// SixLabors.ImageSharp.PixelFormats.HalfSingle
			
			// Fill images with an arbitrary background color, otherwise images would be empty files
			diffuse.Mutate(x => x.Fill(Color.Black));
			grass.Mutate(x => x.Fill(Color.Black));
			height.Mutate(x => x.Fill(Color.Black));
			metal.Mutate(x => x.Fill(Color.Black));


			WriteImage(diffuse, $"{settings.DiffuseMapName}");
			WriteImage(grass, $"{settings.GrassMapName}");
			WriteImage(height,$"{settings.HeightMapName}");
			WriteImage(metal, $"{settings.MetalMapName}");
		}

		private static void WriteImage<T>(Image<T> image, string fileName) where T: unmanaged, IPixel<T>
		{
			FileInfo fi = new FileInfo(fileName);
			IImageEncoder encoder;
			switch(fi.Extension.ToLower())
            {
				case ".bmp":
					encoder = new BmpEncoder();
					break;
				case ".png":
					encoder = new PngEncoder();
					break;
				default:
					throw new NotSupportedException("Right now only creation of .bmp and .png files are supported");
            }

			using (Stream stream = File.Create(fileName))
			{
				encoder.Encode<T>(image, stream);
			}
		}
	}
}
