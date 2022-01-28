using OpenTK.Mathematics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MapCreationTool.NewRendering
{
    class NewCamera
    {
        private float yaw;
        private float pitch;
        private float roll;

        private Vector3 position;
        private Vector3 forward;
        private Vector3 left;
        private Vector3 up;

        public float Yaw
        {
            get => yaw; set
            {
                yaw = value;
            }
        }

        public float Pitch
        {
            get => pitch; set
            {
                pitch = value;
            }
        }

        public float Roll
        {
            get => roll; set
            {
                roll = value;
            }
        }

        public Vector3 Position
        {
            get => position; set
            {
                position = value;
            }
        }

        public Vector3 Forward
        {
            get => forward; set
            {
                forward = value;
            }
        }

        public Vector3 Left
        {
            get => left; set
            {
                left = value;
            }
        }

        public Vector3 Up
        {
            get => up; set
            {
                up = value;
            }
        }

        public NewCamera()
        {
            // Let's just assume some initial stuff
            this.position = new Vector3(0, 0, -3).Normalized();
            this.left = new Vector3(-1, 0, 0).Normalized();
            this.forward = new Vector3(0, 0, 1).Normalized();
            this.up = Vector3.Cross(left, forward);
        }

        public static Vector3 AngleAxis(Vector3 aAxis, float rad)
        {
            aAxis.Normalize();
            
            aAxis *= MathF.Sin(rad);
            Quaternion quat = new Quaternion(aAxis.X, aAxis.Y, aAxis.Z, rad);

            
            quat.ToAxisAngle(out Vector3 result, out float angle);
            return result;
        }
    }
}
