using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats.Bmp;
using SixLabors.ImageSharp.PixelFormats;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MapCreationTool.Images
{
    // Library overview: https://csharp-station.com/4-best-c-libraries-for-image-processing/
    // More libraries: https://stackoverflow.com/questions/5881031/image-processing-libraries-in-c-sharp
    // Maybe better than ImageProcessor: https://github.com/SixLabors/ImageSharp;

    class ImageTest
    {
        public void Test()
        {
            Image<Rgba32> blank = new Image<Rgba32>(400, 200);

            BmpEncoder encoder = new BmpEncoder();
            //encoder.Encode()
        }
    }
}
