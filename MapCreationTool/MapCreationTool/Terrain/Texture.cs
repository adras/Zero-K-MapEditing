using OpenTK.Graphics.OpenGL;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing;
using System;
using System.Collections.Generic;

namespace MapCreationTool.Terrain
{
    internal class Texture
    {
        int handle;

        public void Use(TextureUnit unit)
        {
            GL.ActiveTexture(unit);
            GL.BindTexture(TextureTarget.Texture2D, handle);
        }

        internal static Texture LoadImage(string diffusePath)
        {
            Texture texture = new Texture();

            texture.handle = GL.GenTexture();

            // Bind the handle
            GL.ActiveTexture(TextureUnit.Texture0);
            GL.BindTexture(TextureTarget.Texture2D, texture.handle);

            Image<Rgba32> diffuseImage = (Image<Rgba32>)Image.Load(diffusePath);

            // Our texture is read from top right, opengl however expected from bottom left
            // we need to rotate at one point
            // The operation we could do here is a flip on the y axis
            diffuseImage.Mutate(x => x.Flip(FlipMode.Vertical));

            // TODO: Replace this by an array to speed things up
            List<byte>? pixels = new List<byte>(4 * diffuseImage.Width * diffuseImage.Height);

            for (int y = 0; y < diffuseImage.Height; y++)
            {
                Span<Rgba32> row = diffuseImage.GetPixelRowSpan(y);

                for (int x = 0; x < diffuseImage.Width; x++)
                {
                    pixels.Add(row[x].R);
                    pixels.Add(row[x].G);
                    pixels.Add(row[x].B);
                    pixels.Add(row[x].A);
                }
            }

            // This should use RGBA instead of BGRA
            GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgba, diffuseImage.Width, diffuseImage.Height,
                   0, PixelFormat.Rgba, PixelType.UnsignedByte, pixels.ToArray());


            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)TextureMinFilter.Linear);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int)TextureMagFilter.Linear);

            // Now, set the wrapping mode. S is for the X axis, and T is for the Y axis.
            // We set this to Repeat so that textures will repeat when wrapped. Not demonstrated here since the texture coordinates exactly match
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapS, (int)TextureWrapMode.Repeat);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapT, (int)TextureWrapMode.Repeat);

            // Next, generate mipmaps.
            // Mipmaps are smaller copies of the texture, scaled down. Each mipmap level is half the size of the previous one
            // Generated mipmaps go all the way down to just one pixel.
            // OpenGL will automatically switch between mipmaps when an object gets sufficiently far away.
            // This prevents moiré effects, as well as saving on texture bandwidth.
            // Here you can see and read about the morié effect https://en.wikipedia.org/wiki/Moir%C3%A9_pattern
            // Here is an example of mips in action https://en.wikipedia.org/wiki/File:Mipmap_Aliasing_Comparison.png
            GL.GenerateMipmap(GenerateMipmapTarget.Texture2D);

            return texture;
        }
    }
}
