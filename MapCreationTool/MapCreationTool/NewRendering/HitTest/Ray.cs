using OpenTK.Mathematics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MapCreationTool.NewRendering.HitTest
{
    internal class Ray
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

        private string V2S (Vector3 v)
        {
            string result = $"({v.X:F2}, {v.Y:F2}, {v.Z:F2})";
            return result;
        }
    }
}
