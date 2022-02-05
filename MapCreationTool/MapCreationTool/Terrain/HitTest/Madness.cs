using OpenTK.Mathematics;

namespace MapCreationTool.Terrain.HitTest
{
    internal class Madness
    {
        private static readonly float EPSILON = 0.0000001f;

        public static Vector3? IntersectionWith(Ray ray, Vector3 vertex0, Vector3 vertex1, Vector3 vertex2)
        {
            Vector3 edge1;
            Vector3 edge2;
            Vector3 h = new Vector3();
            Vector3 s = new Vector3();
            Vector3 q = new Vector3();
            float a, f, u, v;

            edge1 = vertex1 - vertex0;
            edge2 = vertex2 - vertex0;

            Vector3 rayVector = ray.direction;

            h = Vector3.Cross(rayVector, edge2);

            a = Vector3.Dot(edge1, h);


            if (a > -EPSILON && a < EPSILON)
                return null;    // This ray is parallel to this triangle.

            f = 1.0f / a;
            s = ray.origin - vertex0;
            u = f * Vector3.Dot(s, h);
            if (u < 0.0 || u > 1.0)
                return null;

            q = Vector3.Cross(s, edge1);
            v = f * Vector3.Dot(rayVector, q);

            if (v < 0.0 || u + v > 1.0)
                return null;

            // At this stage we can compute t to find out where the intersection point is on the line.
            float t = f * Vector3.Dot(edge2, q);

            if (t > EPSILON) // ray intersection
            {
                Vector3 result = ray.origin + t * rayVector;

                // outIntersectionPoint.set(0.0, 0.0, 0.0);
                // outIntersectionPoint.scaleAdd(t, rayVector, rayOrigin);
                return result;
            }
            else // This means that there is a line intersection but not a ray intersection.
                return null;
        }

        public static HitInfo GetHit(VertexData data, Ray ray)
        {
            for (int i = 0; i < data.indices.Length; i += 3)
            {
                uint idx1 = data.indices[i];
                uint idx2 = data.indices[i + 1];
                uint idx3 = data.indices[i + 2];

                uint vIdx1 = idx1 * (3 + 3 + 2);
                uint vIdx2 = idx2 * (3 + 3 + 2);
                uint vIdx3 = idx3 * (3 + 3 + 2);


                Vector3 a = new Vector3(data.vertices[vIdx1], data.vertices[vIdx1 + 1], data.vertices[vIdx1 + 2]);
                Vector3 b = new Vector3(data.vertices[vIdx2], data.vertices[vIdx2 + 1], data.vertices[vIdx2 + 2]);
                Vector3 c = new Vector3(data.vertices[vIdx3], data.vertices[vIdx3 + 1], data.vertices[vIdx3 + 2]);

                Vector3? hit = IntersectionWith(ray, a, b, c);

                if (hit != null)
                {
                    HitInfo hitInfo = new HitInfo(vIdx1, vIdx2, vIdx3, hit.Value);
                    return hitInfo;
                }
            }
            return null;
        }


    }
}
