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

        public void DrawSmoothBrush(HitInfo hit, VertexData vertexData, float brushSize, float brushStrength)
        {

            // Do some rectangle manipulation
            // NOTE: TODO: Normals also need to be recalculated

            int halfBrushWidth = (int)(brushSize / 2f);
            int halfBrushHeight = (int)(brushSize / 2f);
            Vector3 brushSizeV = new Vector3(halfBrushWidth, halfBrushHeight, 0);
            //Vector3 center = new Vector3(halfBrushWidth / 2f, halfBrushHeight / 2f, 0);
            Vector3 center = Vector3.Zero;
            List<uint> modifiedIIndices = new List<uint>();
            for (int x = -halfBrushWidth; x < halfBrushWidth; x++)
            {
                for (int y = -halfBrushHeight; y < halfBrushHeight; y++)
                {
                    Vector3 current = new Vector3(x, y, 0);
                    Vector3 delta = center - current;

                    float height = 1f / Math.Max(1, delta.Length);


                    uint offset = (uint)(x + y * vertexData.width) * VertexData.IDX_MUL;
                    uint vIdx = hit.vIdxA + offset;

                    // Plus two, because we would like to set the Z-Coordinate
                    uint targetIdx = vIdx + 2;
                    if (targetIdx >= vertexData.vertices.Length || targetIdx < 0)
                        continue;

                    vertexData.vertices[targetIdx] += height * brushStrength;

                    uint iIdx = vIdx / VertexData.IDX_MUL;
                    modifiedIIndices.Add(iIdx);
                }
            }

            UpdateNormals(vertexData, modifiedIIndices);

        }

        private void UpdateNormals(VertexData vertexData, List<uint> vIndices)
        {
            // TODO: Change this so that only the given veretx is updated
            // IEnumerable<uint> iIndices = vertexData.indices.Where(iIdx => vIndices.Contains(iIdx));
            // IEnumerable<uint> test = vertexData.indices.Where(iIdx => vIndices.Any(b => b == iIdx));
            // foreach (uint iIdx in test)
            // {
            //     UpdateNormals(vertexData, iIdx);
            // }

            UpdateAllNormals(vertexData, 0);
        }

        private void UpdateAllNormals(VertexData vertexData, uint vIdx)
        {
            // Since this is parallel, results may vary :D
            Parallel.For(0, vertexData.indices.Length - 3, (iIdx) => UpdateNormals(vertexData, (uint)iIdx));
        }

        private void UpdateNormals(VertexData vertexData, uint idx)
        {
            //for (int idx = 0; idx < vertexData.indices.Length - 3; idx++)
            {
                // Get the startIndices for three vertices, this is the index of the x coordinate
                uint vIdx1 = vertexData.indices[idx] * VertexData.IDX_MUL;
                uint vIdx2 = vertexData.indices[idx + 1] * VertexData.IDX_MUL;
                uint vIdx3 = vertexData.indices[idx + 2] * VertexData.IDX_MUL;

                // create three vectors
                Vector3 a = new Vector3(vertexData.vertices[vIdx1], vertexData.vertices[vIdx1 + 1], vertexData.vertices[vIdx1 + 2]);
                Vector3 b = new Vector3(vertexData.vertices[vIdx2], vertexData.vertices[vIdx2 + 1], vertexData.vertices[vIdx2 + 2]);
                Vector3 c = new Vector3(vertexData.vertices[vIdx3], vertexData.vertices[vIdx3 + 1], vertexData.vertices[vIdx3 + 2]);

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
                vertexData.vertices[vIdx1 + 3] = normal.X;
                vertexData.vertices[vIdx1 + 4] = normal.Y;
                vertexData.vertices[vIdx1 + 5] = normal.Z;

                vertexData.vertices[vIdx2 + 3] = normal.X;
                vertexData.vertices[vIdx2 + 4] = normal.Y;
                vertexData.vertices[vIdx2 + 5] = normal.Z;

                vertexData.vertices[vIdx3 + 3] = normal.X;
                vertexData.vertices[vIdx3 + 4] = normal.Y;
                vertexData.vertices[vIdx3 + 5] = normal.Z;
            }
        }
    }
}
