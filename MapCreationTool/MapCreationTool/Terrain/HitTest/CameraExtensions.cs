using OpenTK.Mathematics;
using System.Windows;

namespace MapCreationTool.Terrain.HitTest
{
    public static class CameraExtensions
    {
        public static Vector3 ScreenToWorldPos(this Camera camera, Point pos)
        {
            Vector3 mouseVect = new Vector3((float)pos.X, (float)pos.Y, 0);

            Vector3 centeredMouse = new Vector3(mouseVect.X, mouseVect.Y, 0);
            Vector2 screenSize = camera.ScreenSize;
            centeredMouse.X = 2.0f * centeredMouse.X / screenSize.X - 1f;
            centeredMouse.Y = 2.0f * centeredMouse.Y / screenSize.Y - 1f;
            centeredMouse.Z = -1;

            return centeredMouse;
        }

        public static Ray ScreenToPointRay(this Camera camera, Point position)
        {
            Vector3 centeredMouse = camera.ScreenToWorldPos(position);

            Matrix4 proj = camera.GetProjectionMatrix();
            Matrix4 view = camera.GetViewMatrix();
            Matrix4 newMatrix = view * proj;

            newMatrix = Matrix4.Invert(newMatrix);

            // Base on: https://stackoverflow.com/questions/51116554/simple-opentk-raycasting
            Vector4 mouseNear = new Vector4(centeredMouse.X, -centeredMouse.Y, 0, 1);
            Vector4 mouseFar = new Vector4(centeredMouse.X, -centeredMouse.Y, 1, 1);
            Vector4 rayNear = mouseNear * newMatrix;
            Vector4 rayFar = mouseFar * newMatrix;

            rayNear = 1 / rayNear.W * rayNear;
            rayFar = 1 / rayFar.W * rayFar;


            Vector3 origin = new Vector3(rayNear);
            Vector3 target = new Vector3(rayFar);
            Ray ray = new Ray(origin, target);

            return ray;
        }
    }
}
