using OpenTK.Mathematics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MapCreationTool.Terrain.Lights
{
    public class SpotLight
    {
        public Vector3 position;
        public Vector3 direction;
        public float cutOff;
        public float outerCutOff;

        public Vector3 ambient;
        public Vector3 diffuse;
        public Vector3 specular;

        public float constant;
        public float linear;
        public float quadratic;
    }
}
