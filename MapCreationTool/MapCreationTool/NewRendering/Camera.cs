using OpenTK.Mathematics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MapCreationTool.NewRendering
{
    // Another camera can be found at: https://github.com/opentk/LearnOpenTK/blob/master/Common/Camera.cs
    public class Camera
    {
        public Vector3 cameraPos;
        public Vector3 cameraTarget;
        public Vector3 cameraDirection;
        public Vector3 up;
        public Vector3 cameraRight;
        public Vector3 cameraUp;

        public Matrix4 view;
        public Matrix4 projection;

        public float fov = 45;
        public float aspectRatio = 4.0f / 3.0f; // This is not correct and probably depends on the window size

        public Camera()
        {
            cameraPos = new Vector3(0, 0, 0.3f);
            cameraTarget = Vector3.Zero;
            cameraDirection = Vector3.Normalize(cameraPos - cameraTarget);
            up = Vector3.UnitY;
            cameraRight = Vector3.Normalize(Vector3.Cross(up, cameraDirection));
            cameraUp = Vector3.Cross(cameraDirection, cameraRight);

            //Matrix4 view = Matrix4.LookAt(new Vector3(0.0f, 0.0f, 3.0f),
            //     new Vector3(0.0f, 0.0f, 0.0f),
            //     new Vector3(0.0f, 1.0f, 0.0f));

        }

        public void CalculateViewMatrix()
        {
            view = Matrix4.LookAt(cameraPos, cameraTarget, cameraUp);
        }

        public void CalculateProjectionMatrix()
        {
            // Fov needs to be between 0 -> PI, makes 180 degrees
            projection = Matrix4.CreatePerspectiveFieldOfView(MathF.PI / 4, aspectRatio, 0.01f, 100f);
        }
    }
}
