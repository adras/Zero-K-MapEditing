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

        public NewCamera(Vector3 position, Vector3 lookAt)
        {
            this.position = position;
            Vector3 delta = lookAt- position;
            Forward = delta.Normalized();
        }


    }
}
