using MapCreationTool.Models;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats.Bmp;
using SixLabors.ImageSharp.PixelFormats;
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


            BmpEncoder encoder = new BmpEncoder();

            encoder.Encode(diffuse, File.Create($"{mapDir}\\diffuse.bmp"));
            encoder.Encode(grass, File.Create($"{mapDir}\\grass.bmp"));
            encoder.Encode(height, File.Create($"{mapDir}\\height.bmp"));
            encoder.Encode(metal, File.Create($"{mapDir}\\metal.bmp"));
        }
    }
}
