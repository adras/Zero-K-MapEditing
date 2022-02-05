using OpenTK.Mathematics;

namespace MapCreationTool.NewRendering.HitTest
{
    public class Ray
    {
        public Vector3 origin;
        public Vector3 target;
        public Vector3 direction;

        public Ray(Vector3 origin, Vector3 target)
        {
            this.origin = origin;
            this.target = target;
            direction = Vector3.Normalize(target - origin);
        }

        public override string ToString()
        {
            string result = $"Origin: {V2S(origin)} Target: {V2S(target)} Dir: {V2S(direction)}";
            return result;
        }

        private string V2S(Vector3 v)
        {
            string result = $"({v.X:F2}, {v.Y:F2}, {v.Z:F2})";
            return result;
        }
    }
}
