using OpenTK.Mathematics;
using System;

namespace MapCreationTool.Terrain
{

    public class Camera
    {
        private Vector3 front = -Vector3.UnitZ;
        private Vector3 up = Vector3.UnitY;
        private Vector3 right = Vector3.UnitX;

        private float pitch;
        private float yaw = -MathHelper.PiOver2; // Without this, you would be started rotated 90 degrees right.
        private float fov = MathHelper.PiOver2;

        private Vector2 screenSize;

        public Camera(Vector3 position, int screenWidth, int screenHeight)
        {
            Position = position;
            ScreenSize = new Vector2(screenWidth, screenHeight);
        }

        public Vector3 Position { get; set; }

        public float AspectRatio
        {
            get
            {
                return ScreenSize.X / ScreenSize.Y;
            }
        }

        public Vector3 Front => front;

        public Vector3 Up => up;

        public Vector3 Right => right;

        public float Pitch
        {
            get => MathHelper.RadiansToDegrees(pitch);
            set
            {
                float angle = MathHelper.Clamp(value, -179f, 0f);
                pitch = MathHelper.DegreesToRadians(angle);
                UpdateVectors();
            }
        }

        public float Yaw
        {
            get => MathHelper.RadiansToDegrees(yaw);
            set
            {
                yaw = MathHelper.DegreesToRadians(value);
                UpdateVectors();
            }
        }

        public float Fov
        {
            get => MathHelper.RadiansToDegrees(fov);
            set
            {
                float angle = MathHelper.Clamp(value, 1f, 45f);
                fov = MathHelper.DegreesToRadians(angle);
            }
        }

        public Vector2 ScreenSize { get => screenSize; set => screenSize = value; }

        public Matrix4 GetViewMatrix()
        {
            return Matrix4.LookAt(Position, Position + front, up);
        }

        public Matrix4 GetProjectionMatrix()
        {
            return Matrix4.CreatePerspectiveFieldOfView(fov, AspectRatio, 0.01f, 1000f);
        }

        private void UpdateVectors()
        {
            // I made it, I don't understand it, I won't explain it :P
            // This is different from all other cameras, which might explain why pitch 0 is not looking down as described in the next article
            // https://support.pix4d.com/hc/en-us/articles/202558969-Yaw-Pitch-Roll-and-Omega-Phi-Kappa-angles
            // To fix this, use the calculation from the example camera. When pitch and yaw are not behaving correctly, rotate
            // The data created by the ImageLoader to match the way the camera works
            // Camera seems to be off, when it comes to pitch and yaw.
            // Correct implementation can probably be found here: https://learnopengl.com/Getting-started/Camera
            front.X = MathF.Sin(pitch) * MathF.Sin(yaw);
            front.Y = MathF.Sin(pitch) * MathF.Cos(yaw);
            front.Z = MathF.Cos(pitch);
            front.Normalize();

            right = Vector3.Cross(-Vector3.UnitZ, front).Normalized();
            up = Vector3.Cross(right, front).Normalized();
        }
    }
}