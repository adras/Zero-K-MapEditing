using OpenTK.Mathematics;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Advanced;
using SixLabors.ImageSharp.PixelFormats;
using System;

namespace MapCreationTool.Terrain
{



    class ImageLoader
    {
        // Create data wich can be used for opengl buffers
        // https://opentk.net/learn/chapter1/2-hello-triangle.html

        public static VertexData LoadImage(string filePath, float minHeight, float maxHeight)
        {
            Image<Rgba32> heightImage = (Image<Rgba32>)Image.Load(filePath);

            float halfWidth = heightImage.Width / 2.0f;
            float halfHeight = heightImage.Height / 2.0f;
            int bit16 = 2 << 16 - 1;

            float colorScaleFactor = 10;

            Random r = new Random();

            float[] vertices = new float[heightImage.Width * heightImage.Height * VertexData.IDX_MUL];
            int vIdx = 0;
            // Although the tutorial uses floats, let's try with vectors
            // This could also be an array, since size isn't changed
            // size should be widht * height OR width * height * 3 if floats are used
            for (int y = 0; y < heightImage.Height; y++)
            {
                Rgba32[]? row = heightImage.GetPixelRowMemory(y).ToArray();

                for (int x = 0; x < row.Length; x++)
                {
                    float xPos = x - halfWidth;
                    float yPos = y - halfHeight;
                    float zPos = Rescale(0, bit16, minHeight, maxHeight, row[x].R * colorScaleFactor);

                    vertices[vIdx++] = xPos;
                    vertices[vIdx++] = -yPos; // Flip y axis
                    vertices[vIdx++] = zPos;

                    // normals will be generated with triangles, therefore just skip the index here
                    vIdx += 3;

                    // Set texture coordinate
                    float s = x / (float)heightImage.Width;
                    float t = y / (float)heightImage.Height;
                    vertices[vIdx++] = s;
                    vertices[vIdx++] = t;
                }
            }

            uint[] indices = new uint[heightImage.Width * heightImage.Height * 6];
            int iIdx = 0;
            int nIdx = 3;
            for (int y = 0; y < heightImage.Height - 1; y += 1)
                for (int x = 0; x < heightImage.Width - 1; x += 1)
                {
                    int idxA1 = x + y * heightImage.Width + 1;
                    int idxA2 = x + (y + 1) * heightImage.Width + 1;
                    int idxA3 = x + y * heightImage.Width;

                    int idxB1 = x + (y + 1) * heightImage.Width + 1;
                    int idxB2 = x + (y + 1) * heightImage.Width;
                    int idxB3 = x + y * heightImage.Width;

                    indices[iIdx++] = (uint)idxA1;
                    indices[iIdx++] = (uint)idxA2;
                    indices[iIdx++] = (uint)idxA3;

                    indices[iIdx++] = (uint)idxB1;
                    indices[iIdx++] = (uint)idxB2;
                    indices[iIdx++] = (uint)idxB3;
                    //iIdx +=3;
                }

            // Divide and capitulate to performance
            // Calculate normals
            for (int idx = 0; idx < indices.Length - 3; idx++)
            {
                // Get the startIndices for three vertices, this is the index of the x coordinate
                uint vIdx1 = indices[idx] * VertexData.IDX_MUL;
                uint vIdx2 = indices[idx + 1] * VertexData.IDX_MUL;
                uint vIdx3 = indices[idx + 2] * VertexData.IDX_MUL;

                // create three vectors
                Vector3 a = new Vector3(vertices[vIdx1], vertices[vIdx1 + 1], vertices[vIdx1 + 2]);
                Vector3 b = new Vector3(vertices[vIdx2], vertices[vIdx2 + 1], vertices[vIdx2 + 2]);
                Vector3 c = new Vector3(vertices[vIdx3], vertices[vIdx3 + 1], vertices[vIdx3 + 2]);

                // Create two deltas
                Vector3 deltaAB = a - b;
                Vector3 deltaBC = b - c;

                // Calculate normal
                Vector3 normal = Vector3.Cross(deltaAB, deltaBC);
                normal.Normalize();
                //normal = new Vector3((float)r.NextDouble(), (float)r.NextDouble(), (float)r.NextDouble());
                if (float.IsNaN(normal.X))
                {

                }

                // Assign normal to all three points
                vertices[vIdx1 + 3] = normal.X;
                vertices[vIdx1 + 4] = normal.Y;
                vertices[vIdx1 + 5] = normal.Z;

                vertices[vIdx2 + 3] = normal.X;
                vertices[vIdx2 + 4] = normal.Y;
                vertices[vIdx2 + 5] = normal.Z;

                vertices[vIdx3 + 3] = normal.X;
                vertices[vIdx3 + 4] = normal.Y;
                vertices[vIdx3 + 5] = normal.Z;
            }

            VertexData result = new VertexData { vertices = vertices, indices = indices, width = heightImage.Width, height = heightImage.Height };
            return result;
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

            float result = (val - oldMin) * newDelta / oldDelta + newMin;
            return result;
        }
    }
}
