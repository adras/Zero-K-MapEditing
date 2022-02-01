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

        public Ray(Vector3 origin, Vector3 target)
        {
            this.origin = origin;
            this.target = target;
        }
    }
}
