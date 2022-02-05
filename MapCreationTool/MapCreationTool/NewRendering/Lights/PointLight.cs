using OpenTK.Mathematics;

namespace MapCreationTool.NewRendering.Lights
{
    public class PointLight
    {
        public Vector3 position;
        public float constant;
        public float linear;
        public float quadratic;
        public Vector3 ambient;
        public Vector3 diffuse;
        public Vector3 specular;
    }
}
