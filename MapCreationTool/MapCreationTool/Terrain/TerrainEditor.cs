using OpenTK.Mathematics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MapCreationTool.Terrain
{
    internal class TerrainEditor
    {
        public VertexData vertexData;

        public void DrawSmoothBrush(HitInfo hit, VertexData vertexData)
        {

            // Do some rectangle manipulation
            // NOTE: TODO: Normals also need to be recalculated
            float brushStrength = 2f;
            int halfBrushWidth = 8;
            int halfBrushHeight = 8;
            Vector3 brushSize = new Vector3(halfBrushWidth, halfBrushHeight, 0);
            //Vector3 center = new Vector3(halfBrushWidth / 2f, halfBrushHeight / 2f, 0);
            Vector3 center = Vector3.Zero;
            for (int x = -halfBrushWidth; x < halfBrushWidth; x++)
            {
                for (int y = -halfBrushHeight; y < halfBrushHeight; y++)
                {
                    Vector3 current = new Vector3(x, y, 0);
                    Vector3 delta = center - current;

                    float height = 1f / Math.Max (1, delta.Length);


                    uint offset = (uint)(x + y * vertexData.width) * VertexData.IDX_MUL;
                    uint xIdx = hit.vIdxA + offset;

                    // Plus two, because we would like to set the Z-Coordinate
                    uint targetIdx = xIdx + 2;
                    if (targetIdx >= vertexData.vertices.Length || targetIdx < 0)
                        continue;

                    vertexData.vertices[targetIdx] += height * brushStrength;
                }
            }
        }
    }
}
