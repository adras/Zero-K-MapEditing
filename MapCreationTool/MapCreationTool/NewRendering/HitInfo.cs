using OpenTK.Mathematics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MapCreationTool.NewRendering
{
    internal class HitInfo
    {
        // Triangle indices in ImageData vertex array, these are indices for the X-Coordinate
        public uint vIdxA;
        public uint vIdxB;
        public uint vIdxC;

        // Hit location in world space
        public Vector3 hitLocation;

        public HitInfo(uint idxA, uint idxB, uint idxC, Vector3 hitLocation)
        {
            this.vIdxA = idxA;
            this.vIdxB = idxB;
            this.vIdxC = idxC;
            this.hitLocation = hitLocation;
        }
    }
}
