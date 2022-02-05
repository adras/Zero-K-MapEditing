using OpenTK.Mathematics;

namespace MapCreationTool.Terrain
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
            vIdxA = idxA;
            vIdxB = idxB;
            vIdxC = idxC;
            this.hitLocation = hitLocation;
        }
    }
}
